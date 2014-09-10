using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Xml;


public class ScriptDeferFilter : Stream
{
    Stream responseStream;
    long position;
    bool captureScripts;
    StringBuilder scriptBlocks;
    Encoding encoding;
    char[] pendingBuffer = null;
    bool lastScriptTagIsPinned = false;
    bool scriptTagStarted = false;

    public ScriptDeferFilter(HttpResponse response)
    {
        this.encoding = response.Output.Encoding;
        this.responseStream = response.Filter;
        this.scriptBlocks = new StringBuilder(5000);
        this.captureScripts = true;
    }

    #region Filter overrides
    public override bool CanRead
    {
        get { return false; }
    }

    public override bool CanSeek
    {
        get { return false; }
    }

    public override bool CanWrite
    {
        get { return true; }
    }

    public override void Close()
    {
        this.FlushPendingBuffer();
        responseStream.Close();
    }

    private void FlushPendingBuffer()
    {
        if (null != this.pendingBuffer)
        {
            this.WriteOutput(this.pendingBuffer, 0, this.pendingBuffer.Length);
            this.pendingBuffer = null;
        }
    }

    public override void Flush()
    {
        this.FlushPendingBuffer();
        responseStream.Flush();
    }

    public override long Length
    {
        get { return 0; }
    }

    public override long Position
    {
        get { return position; }
        set { position = value; }
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        return responseStream.Seek(offset, origin);
    }

    public override void SetLength(long length)
    {
        responseStream.SetLength(length);
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        return responseStream.Read(buffer, offset, count);
    }
    #endregion

    public override void Write(byte[] buffer, int offset, int count)
    {
        if (!this.captureScripts)
        {
            this.responseStream.Write(buffer, offset, count);
            return;
        }

        char[] content;
        char[] charBuffer = this.encoding.GetChars(buffer, offset, count);

        if (null != this.pendingBuffer)
        {
            content = new char[charBuffer.Length + this.pendingBuffer.Length];
            Array.Copy(this.pendingBuffer, 0, content, 0, this.pendingBuffer.Length);
            Array.Copy(charBuffer, 0, content, this.pendingBuffer.Length, charBuffer.Length);
            this.pendingBuffer = null;
        }
        else
        {
            content = charBuffer;
        }

        int scriptTagStart = 0;
        int lastScriptTagEnd = 0;

        int pos;
        for (pos = 0; pos < content.Length; pos++)
        {
            // See if tag start
            char c = content[pos];
            if (c == '<')
            {

                if (pos + "script pin".Length > content.Length)
                {
                    this.pendingBuffer = new char[content.Length - pos];
                    Array.Copy(content, pos, this.pendingBuffer, 0, content.Length - pos);
                    break;
                }

                int tagStart = pos;


                if (content[pos + 1] == '/')
                {
                    pos += 2;
                    if (isScriptTag(content, pos))
                    {
                        if (this.lastScriptTagIsPinned)
                        {
                            this.lastScriptTagIsPinned = false;
                            pos++;
                        }
                        else
                        {
                            pos = pos + "script>".Length;
                            scriptBlocks.Append(content, scriptTagStart, pos - scriptTagStart);
                            scriptBlocks.Append(Environment.NewLine);
                            lastScriptTagEnd = pos;
                            scriptTagStarted = false;
                            pos--;
                            continue;
                        }
                    }
                    else if (isBodyTag(content, pos))
                    {

                        if (this.scriptBlocks.Length > 0)
                        {
                            this.WriteOutput(content, lastScriptTagEnd, tagStart - lastScriptTagEnd);
                            this.RenderAllScriptBlocks();
                            this.captureScripts = false;
                            this.WriteOutput(content, tagStart, content.Length - tagStart);
                            return;
                        }
                    }
                    else
                    {
                        pos++;
                    }
                }
                else
                {
                    if (isScriptTag(content, pos + 1))
                    {
                        this.lastScriptTagIsPinned = isPinned(content, pos + 1);

                        if (!this.lastScriptTagIsPinned)
                        {
                            scriptTagStart = pos;
                            this.WriteOutput(content, lastScriptTagEnd, scriptTagStart - lastScriptTagEnd);

                            pos += "<script".Length;
                            scriptTagStarted = true;
                        }
                        else
                        {
                            pos++;
                        }
                    }
                    else
                    {
                        pos++;
                    }
                }
            }
        }

        if (scriptTagStarted)
        {
            this.scriptBlocks.Append(content, scriptTagStart, pos - scriptTagStart);
        }
        else
        {
            this.WriteOutput(content, lastScriptTagEnd, pos - lastScriptTagEnd);
        }
    }


    private void RenderAllScriptBlocks()
    {
        string output = CombineScripts.CombineScriptBlocks(this.scriptBlocks.ToString());
        output = CombineScripts.CombineScriptBlocksforaxd(output);

        byte[] scriptBytes = this.encoding.GetBytes(output);
        this.responseStream.Write(scriptBytes, 0, scriptBytes.Length);
    }

    private void WriteOutput(char[] content, int pos, int length)
    {
        if (length == 0) return;

        byte[] buffer = this.encoding.GetBytes(content, pos, length);
        this.responseStream.Write(buffer, 0, buffer.Length);
    }
    private void WriteOutput(string content)
    {
        byte[] buffer = this.encoding.GetBytes(content);
        this.responseStream.Write(buffer, 0, buffer.Length);
    }

    private bool isScriptTag(char[] content, int pos)
    {
        if (pos + 5 < content.Length)
            return ((content[pos] == 's' || content[pos] == 'S')
                && (content[pos + 1] == 'c' || content[pos + 1] == 'C')
                && (content[pos + 2] == 'r' || content[pos + 2] == 'R')
                && (content[pos + 3] == 'i' || content[pos + 3] == 'I')
                && (content[pos + 4] == 'p' || content[pos + 4] == 'P')
                && (content[pos + 5] == 't' || content[pos + 5] == 'T'));
        else
            return false;

    }

    private bool isPinned(char[] content, int pos)
    {
        if (pos + 5 + 3 < content.Length)
            return ((content[pos + 7] == 'p' || content[pos + 7] == 'P')
                && (content[pos + 8] == 'i' || content[pos + 8] == 'I')
                && (content[pos + 9] == 'n' || content[pos + 9] == 'N'));
        else
            return false;
    }

    private bool isBodyTag(char[] content, int pos)
    {
        if (pos + 3 < content.Length)
            return ((content[pos] == 'b' || content[pos] == 'B')
                && (content[pos + 1] == 'o' || content[pos + 1] == 'O')
                && (content[pos + 2] == 'd' || content[pos + 2] == 'D')
                && (content[pos + 3] == 'y' || content[pos + 3] == 'Y'));
        else
            return false;
    }
}
public class CombineScripts
{
    //string a = ;<script.*?src=(?:'|"")(.*?)(?:'|"").*?></script>


    private static Regex _FindScriptTags = new Regex(@"<script.*?src=(?:'|"")(.*?)(?:'|"").*?></script>", RegexOptions.Compiled);

    private static Regex _FindScriptTagsaxd = new Regex(@"<script\s*src\s*=\s*""(?<url>.[^""]+)"".[^>]*>\s*</script>", RegexOptions.Compiled);

    private static readonly string SCRIPT_VERSION_NO = ConfigurationManager.AppSettings["ScriptVersionNo"];



    public static string CombineScriptBlocks(string scripts)
    {
        List<UrlMapSet> sets = LoadSets();
        string output = scripts;

        foreach (UrlMapSet mapSet in sets)
        {
            int setStartPos = -1;
            List<string> names = new List<string>();




            output = _FindScriptTags.Replace(output, new MatchEvaluator(delegate(Match match)
            {
                string url = match.Value;

                System.Diagnostics.Debug.WriteLine("Hi s" + " " + url);

                UrlMap urlMatch = mapSet.Urls.Find(
                    new Predicate<UrlMap>(
                        delegate(UrlMap map)
                        {
                            System.Diagnostics.Debug.WriteLine(map.Url.ToString() + " hi  " + url.ToString());
                            return url.Contains(map.Url);

                        }));

                if (null != urlMatch)
                {

                    if (setStartPos < 0) setStartPos = match.Index;

                    names.Add(urlMatch.Name);
                    return string.Empty;
                }
                else
                {
                    return match.Value;
                }

            }));

            if (setStartPos >= 0)
            {
                string setName = string.Empty;

                if (mapSet.IsIncludeAll)
                {
                    setName = string.Empty;
                }
                else
                {
                    names.Sort();
                    setName = string.Join(",", names.ToArray());
                }

                string urlPrefix = HttpContext.Current.Request.Path.Substring(0, HttpContext.Current.Request.Path.IndexOf('/', 2) + 1);

                string newScriptTag = "<script type=\"text/javascript\" src=\"" + HttpContext.Current.Request.ApplicationPath + "/UMRScriptHandler.ashx?" + HttpUtility.UrlEncode(mapSet.Name) + "=" + HttpUtility.UrlEncode(setName) + "&" + HttpUtility.UrlEncode(urlPrefix) + "&" + HttpUtility.UrlEncode(SCRIPT_VERSION_NO) + "\"></script>";

                output = output.Insert(setStartPos, newScriptTag);
            }
        }

        return output;
    }


    public static string CombineScriptBlocksforaxd(string scripts)
    {
        List<UrlMapSet> sets = LoadSets();
        string output = scripts;

        foreach (UrlMapSet mapSet in sets)
        {
            int setStartPos = -1;
            List<string> names = new List<string>();




            output = _FindScriptTagsaxd.Replace(output, new MatchEvaluator(delegate(Match match)
                  {
                      string url = match.Groups["url"].Value;

                      UrlMap urlMatch = mapSet.Urls.Find(
                          new Predicate<UrlMap>(
                              delegate(UrlMap map)
                              {
                                  return map.Url == url;
                              }));

                      if (null != urlMatch)
                      {

                          if (setStartPos < 0) setStartPos = match.Index;

                          names.Add(urlMatch.Name);
                          return string.Empty;
                      }
                      else
                      {
                          return match.Value;
                      }

                  }));

            if (setStartPos >= 0)
            {
                string setName = string.Empty;

                if (mapSet.IsIncludeAll)
                {
                    setName = string.Empty;
                }
                else
                {
                    names.Sort();
                    setName = string.Join(",", names.ToArray());
                }

                string urlPrefix = HttpContext.Current.Request.Path.Substring(0, HttpContext.Current.Request.Path.IndexOf('/', 2) + 1);
                string newScriptTag = "<script type=\"text/javascript\" src=\"" + HttpContext.Current.Request.ApplicationPath + "/UMRScriptHandler.ashx?" + HttpUtility.UrlEncode(mapSet.Name) + "=" + HttpUtility.UrlEncode(setName) + "&" + HttpUtility.UrlEncode(urlPrefix) + "&" + HttpUtility.UrlEncode(SCRIPT_VERSION_NO) + "\"></script>";

                output = output.Insert(setStartPos, newScriptTag);
            }
        }

        return output;
    }

    //public static List<UrlMapSet> LoadSets()
    //{
    //    List<UrlMapSet> sets = new List<UrlMapSet>();


    //    return sets;
    //}

    public static List<UrlMapSet> LoadSets()
    {
        List<UrlMapSet> sets = new List<UrlMapSet>();

        using (XmlReader reader = new XmlTextReader(new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/FileMapper.xml"))))
        {
            reader.MoveToContent();
            while (reader.Read())
            {
                if ("set" == reader.Name)
                {
                    string setName = reader.GetAttribute("name");
                    string isIncludeAll = reader.GetAttribute("includeAll");

                    UrlMapSet mapSet = new UrlMapSet();
                    mapSet.Name = setName;
                    if (isIncludeAll == "true")
                        mapSet.IsIncludeAll = true;

                    while (reader.Read())
                    {
                        if ("url" == reader.Name)
                        {
                            string urlName = reader.GetAttribute("name");
                            string url = (string)reader.ReadInnerXml();
                            //  url = url.Replace('*', '&');
                            mapSet.Urls.Add(new UrlMap(urlName, url));
                        }
                        else if ("set" == reader.Name)
                            break;
                    }

                    sets.Add(mapSet);
                }
            }
        }

        return sets;
    }
}

public class UrlMapSet
{
    public string Name;
    public bool IsIncludeAll;
    public List<UrlMap> Urls = new List<UrlMap>();
}

public class UrlMap
{
    public string Name;
    public string Url;

    public UrlMap(string name, string url)
    {
        this.Name = name;
        this.Url = url;
    }
}