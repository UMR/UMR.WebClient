<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TiledRD.aspx.cs" Inherits="Oracle_TiledRD" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Related Disciplines</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
    <radW:RadWindowManager ID="RadWindowManager2" runat="server" Skin="Office2007" OnClientPageLoad="OnClientPageLoad"
        Behavior="None" VisibleStatusbar="false" UseEmbeddedScripts="false">
    </radW:RadWindowManager>

    <script type="text/javascript">
        var queryStr="<%= GetDisCodes() %>";
        var idStr="<%= GetID() %>";
        var desCodes=queryStr.split(",");
        for(var i=0;i<desCodes.length;i++)
        {
           var wnd=window.radopen("RD2.aspx?disCode="+desCodes[i]+"&PatientKey="+idStr,null);
        }
  
        function OnClientPageLoad(radWindow)
        {
           GetRadWindowManager().Tile();
           //radWindow.TogglePin();
        }
        function GetRadWindow()
		{
			var oWindow = null;
			if (window.radWindow) oWindow = window.radWindow;
			else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
			return oWindow;
		} 
        function AddToUnvisitedDiscipline(discArr)
        {
            var rdWnd=GetRadWindow();
            var urlStr= rdWnd.BrowserWindow.location.href;
            if(urlStr.indexOf("RD.aspx")>=0)
            {
                for(var i=0;i<discArr.length;i++)
                {
                    rdWnd.BrowserWindow.AddDiscipline(discArr[i]);
                }
            }
        }
    </script>

</body>
</html>
