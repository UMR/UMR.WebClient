using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Xml;


public partial class Oracle_ControlLibrary_ucRDSetUpOptions : System.Web.UI.UserControl
{
    private long patientKey;
    private List<String> listRDExistingPreference = new List<string>();
    List<String> FullRDList;
    private XmlDocument document = new XmlDocument();
    private string strExistingPreference;

    protected void Page_Load(object sender, EventArgs e)
    {
       //Get the full list of RDs
        if (!IsPostBack)
        {
            FullRDList = PatientManager.GetAllRD();
            //Get The Name of the existing Disciplines already selected for the user...
            //*************
            string userName = HttpContext.Current.User.Identity.Name;
            string cacheKey = userName + "UserSettings";
            //********************************
            if (HttpContext.Current.Session[cacheKey] == null)
            //*******************************
            //if (HttpContext.Current.Session["UserSettings"] == null)
            {
                //Get the XML Setting for the user
                //string UserName = HttpContext.Current.User.Identity.Name;
                //Fetch the UserSettings from DB...
                strExistingPreference = PatientManager.GetUserSettings(userName);   //This method itself UPDATES the Session["UserSettings"] to keep it up to date
                //UpdateRDExistingPreferenceList(document, strExistingPreference, ref listRDExistingPreference);
            }
            else
            {
                //Fetch the Existing Preference from the Session
                //****************************
                strExistingPreference = Session[cacheKey].ToString();
                //*****************************
                //strExistingPreference = Session["UserSettings"].ToString();
                //UpdateRDExistingPreferenceList(document, strExistingPreference, ref listRDExistingPreference);
            }
            //Check to see if empty string or not... YES == 1st time visit
            if (string.IsNullOrEmpty(strExistingPreference))
            {
                //simply show all the items in the original list...
                ShowRDNamesAsIs();
            }
            else
            {
                UpdateRDExistingPreferenceList(document, strExistingPreference, ref listRDExistingPreference);
                ShowRDNamesWithCheckMark();
            }

            //****************************************************************
            // This is where we try to get the ID and ModifierID from QueryString
            //      and based on that push the RD list for this patient in the hidden field
            //****************************************************************
            if (Request.QueryString.Count > 0)
            {
                patientKey = Int64.Parse(Request.QueryString["PatientKey"].ToString());
                DataSet dsRDList = PatientManager.GetRemarkableDisciplineList(patientKey);
                string strRDlist ="";
                foreach (DataRow row in dsRDList.Tables[0].Rows)
                {
                    strRDlist += "#";
                    strRDlist += row["Detail"].ToString();
                }
                hfRDList.Value = strRDlist;
            }

            //****************************************************************
        }
    }

    private void UpdateRDExistingPreferenceList(XmlDocument doc, string XMLString, ref List<String> ExistingPreferenceList)
    {
        document.LoadXml(XMLString);
        XmlElement root = document.DocumentElement;
        foreach (XmlNode node in document.DocumentElement.ChildNodes)
        {
            ExistingPreferenceList.Add(node.Attributes["Id"].Value);
        }
    }

    private void ShowRDNamesAsIs()
    {
        foreach (string RD in FullRDList)
        {
            ListItem li = new ListItem(RD);
            //Add the item in the list
            cblRD.Items.Add(li);
        }
    }

    private void ShowRDNamesWithCheckMark()
    {
        //Now let the search and rescue begin...
        foreach (string RD in FullRDList)
        {
            ListItem li = new ListItem(RD);
            string[] fullRDName = RD.Split(new char[] { '(' });
            string rdName = fullRDName[0];
            //If exists in User's Existing Preference List, put the check mark
            li.Selected = listRDExistingPreference.Exists(delegate(string ExistingName)
                                            {
                                                string[] fullNameExisting = ExistingName.Split(new char[] { '(' });
                                                string nameExisting = fullNameExisting[0];
                                                return nameExisting == rdName;
                                            }
                                           );
            //Add the item in the list
            cblRD.Items.Add(li);
        }
    }
}
