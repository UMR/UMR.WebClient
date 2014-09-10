using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Xml;
using System.Collections;
using Telerik.WebControls;

namespace DockPreferenceSerializationUtiliy
{
    public class DockPreferenceSerializationManager
    {
        const string INNER_CONTROL_PATH = "~/Oracle/ControlLibrary/ucRDShell.ascx";
        const string ZONE_LEFT = "ZoneRightBottomLeft";
        const string ZONE_RIGHT = "ZoneRightBottomRight";

        public static string ZONE_LEFT_NAME{get { return ZONE_LEFT; } }
        public static string ZONE_RIGHT_NAME{get { return ZONE_RIGHT; }}

        public DockPreferenceSerializationManager(){}

        public void SerializeState(string UserID)
        {
            List<DockState> list = new List<DockState>();
            foreach (DockState state in ObjectsState)
            {
                list.Add(state);
            }
            SerializeState(UserID, list);
        }

        private bool SerializeState(string UserID, List<DockState> list)
        {
            //Create the XML Document
            XmlDocument document = new XmlDocument();
            //Create the Root Element
            XmlNode root = document.CreateElement("UserSettings");
            document.AppendChild(root);
            //********************************************************
            ////Now Create the User Node
            //XmlNode nodeUser = document.CreateElement("User");
            //XmlAttribute userID = document.CreateAttribute("UserID");
            //userID.Value = HttpContext.Current.User.Identity.Name;  //"UserName";
            ////Add the userID Attrib
            //nodeUser.Attributes.Append(userID);
            ////Add the nodeUser as a child under root
            //root.PrependChild(nodeUser);
            //Now Create the DockState Nodes under User Node
            //********************************************************
            foreach (DockState state in list)
            {
                XmlNode serializedState = document.CreateElement("State");
                serializedState.Attributes.Append(CreateAttribute(document, "Id", state.Id));
                serializedState.Attributes.Append(CreateAttribute(document, "Text", state.Text));
                serializedState.Attributes.Append(CreateAttribute(document, "ParentZoneId", state.ParentZoneId));
                serializedState.Attributes.Append(CreateAttribute(document, "Closed", state.Closed.ToString().ToLower()));
                serializedState.Attributes.Append(CreateAttribute(document, "InnerControlPath", state.InnerControlPath));
                root.AppendChild(serializedState);
            }
            try
            {
                //Now Save the document
                string strXml = document.InnerXml;
                PatientManager.SaveUserSettings(UserID, strXml);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        private XmlAttribute CreateAttribute(XmlDocument document, string attributeName, string attributeValue)
        {
            XmlAttribute newAttribute = document.CreateAttribute(attributeName);
            newAttribute.Value = attributeValue;
            return newAttribute;
        }

        //private string GetStateXmlPath()
        //{
        //    return HttpContext.Current.Server.MapPath("~/Oracle/Dockstate.xml");
        //}

        public bool SaveUserDockStatePreference(string UserID, string[] opName)
        {
            //Create the list to hold the dockState objects
            List<DockState> listDockState = new List<DockState>();

            //int len = opName[1].Length;
            //string test = opName[1].Substring(1);
            string[] listItems = (opName[1].Substring(1)).Split(new char[] { '#' });
            int i;
            DockState objDockState;
            string abv, id, name, parentZone;
            //Control path will always remain same. == We might take it off altogether in the future
            string innerControlPath = INNER_CONTROL_PATH;
            //1st Time closed will always be false, so init...
            bool closed = false;

            for (i = 0; i < listItems.Length; i++)
            {
                name = listItems[i].Substring(0);
                //Get rid of the ABV part for the ID
                id = (name.Substring(0).Split(new char[] { '(' }))[0].ToString();
                //Set the parent Zones equally based on # of Docks that the user has selected
                if ((i % 2) == 0)
                    parentZone = ZONE_RIGHT;
                else
                    parentZone = ZONE_LEFT;
                //Set up the DockState Container Object
                objDockState = new DockState(id, name, parentZone, closed, innerControlPath);
                //Now add it into the List<>
                listDockState.Add(objDockState);
            }

            //Now Serialize the dockStates
            return (SerializeState(UserID, listDockState));
        }

        #region Deserialize

        private ArrayList ObjectsState
        {
            get
            {
                ArrayList state = (ArrayList)HttpContext.Current.Session["ObjectsState"];
                if (state == null)
                {
                    HttpContext.Current.Session["ObjectsState"] = state = new ArrayList();
                }
                return state;
            }
            set
            {
                HttpContext.Current.Session["ObjectsState"] = value;
            }
        }

        public void DeserializeState(string UserName)
        {
            XmlDocument document = new XmlDocument();
            //Get the XML Setting for the user
            string userSettings = PatientManager.GetUserSettings(UserName);
            if (!String.IsNullOrEmpty(userSettings))
            {
                document.LoadXml(userSettings);
                ObjectsState = new ArrayList();
                //Get the states//document.Load(GetStateXmlPath());document.LoadXml(SavedState);Find the Node for the User//First get the Root element
                XmlElement root = document.DocumentElement;            //Now get to the child == User//foreach (XmlElement userElement in root.ChildNodes)//{////Compare the Attribute value to make sure this is the record we want...//if (userElement.Attributes["UserID"].Value == UserName)//{//Now get the values for the childNodes
                foreach (XmlNode node in document.DocumentElement.ChildNodes)
                {
                    //string id = node.Attributes["Id"].Value;
                    DockState dockState = new DockState(
                    node.Attributes["Id"].Value,
                    node.Attributes["Text"].Value,
                    node.Attributes["ParentZoneId"].Value,
                    bool.Parse(node.Attributes["Closed"].Value),
                    node.Attributes["InnerControlPath"].Value);
                    //Now add
                    ObjectsState.Add(dockState);
                }
            }
        }
        #endregion
    }

    public class DockState
    {
        public string Id;
        public string Text;
        public string ParentZoneId;
        public bool Closed;
        public string InnerControlPath;

        public DockState(string id, string text, string parentZoneId, bool closed, string innerControlPath)
        {
            Id = id;
            Text = text;
            Closed = closed;
            ParentZoneId = parentZoneId;
            InnerControlPath = innerControlPath;
        }
    }
}



