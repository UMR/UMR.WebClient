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

public partial class Oracle_ControlLibrary_PrintPreview_ucRDQuickLinksPreview : System.Web.UI.UserControl
{
    private long patientKey;
    private string strFilter = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        patientKey = Int64.Parse(Request.QueryString["PatientKey"].ToString());
        //We changed the query so need to do a distinct on the source table ...Now query returns Multiple Orthopedics for example... cause Orthopedics both has ICD-9 and CPT-4
        DataSet dsRD = PatientManager.GetRemarkableDisciplineList(patientKey);
        DataTable dtRDDistinct = dsRD.Tables[0].DefaultView.ToTable(true, new string[] { "Detail", "DisCode", "DisciplineType" });
        //***************************************************************************************
        //dlRDQuickLinks.DataSource = PatientManager.GetRemarkableDisciplineList(id, modifierID);
        //***************************************************************************************
        //Now Sort the List //May 5th 2008
        DataView dvwSortedRDList = new DataView(dtRDDistinct, "", "Detail", DataViewRowState.CurrentRows);
        if (dvwSortedRDList != null)
            dlRDQuickLinks.DataSource = dvwSortedRDList;
        
        //dlRDQuickLinks.DataSource = dtRDDistinct;
        dlRDQuickLinks.DataBind();

    }

    protected void dlRDQuickLinks_ItemCreated(object sender, DataListItemEventArgs e)
    {
        #region Old Stuff
        //if (e.Item is DataListItem)
        //{
        //    LinkButton editLink = e.Item.FindControl("LinkButton1") as LinkButton;
        //    if (editLink != null)
        //    {
        //        editLink.Attributes["href"] = "#";
        //    }
        //    DataRowView drv;
        //    if (e.Item.DataItem != null)
        //    {
        //        drv = (DataRowView)e.Item.DataItem;
        //        if (drv != null && drv["Detail"] != DBNull.Value || drv["Detail"].ToString() != string.Empty)
        //        {
        //            editLink.Text = drv["Detail"].ToString();
        //        }
        //        else
        //        {
        //            editLink.Text = "N/A";
        //        }
        //        //editLink.Attributes["onclick"] = string.Format("return ShowRemarkableDiscipline('{0}','{1}','{2}','{3}','{4}','{5}', '{6}');",
        //        //                                                            new object[] { id, modifierID, drv["CodeType"],drv["MedCode"],drv["CodeVersion"], drv["DisCode"], drv["DisciplineType"] });


        //        editLink.Attributes["onclick"] = string.Format("return ShowRemarkableDiscipline('{0}','{1}','{2}','{3}');",
        //                                                                    new object[] { id, modifierID, drv["DisCode"], drv["DisciplineType"] });
        //        editLink.Attributes["onMouseOver"] = string.Format("return HideStatus()");
        //    }
        //}
        #endregion
        if (e.Item is DataListItem)
        {
            LinkButton editLink = e.Item.FindControl("LinkButton1") as LinkButton;
            if (editLink != null)
            {
                //editLink.Attributes["href"] = "#";

                DataRowView drv;
                if (e.Item.DataItem != null)
                {
                    drv = (DataRowView)e.Item.DataItem;
                    if (drv != null && drv["Detail"] != DBNull.Value || drv["Detail"].ToString() != string.Empty)
                    {
                        editLink.Text = drv["Detail"].ToString();
                    }
                    else
                    {
                        editLink.Text = "N/A";
                    }
                    // We are Adding the drv[Detail] at the end of the query string so that RD Page can use it to set its window title...
                    string strHRef = "RD.aspx?PatientKey=" + patientKey + "&DisCode=" + drv["DisCode"].ToString() + "&DisType=" + drv["DisciplineType"].ToString() + "&DisName=" + drv["Detail"].ToString();
                    //Now add the strFilter(if not empty) to the querystring
                    if (!string.IsNullOrEmpty(strFilter))
                    {
                        strHRef += "&CodeType=" + strFilter.Replace("'", "@"); //RD.aspx Page will forward this CodeType filter to ucDiagnosisMain.ascx for filtering data
                    }
                    //Now set the editLink Attributes
                    editLink.Attributes["href"] = strHRef;
                    //editLink.Attributes["onclick"] = string.Format("return ShowRemarkableDiscipline('{0}')", editLink.Attributes["href"].ToString());
                }
            }
        }
    }

    //protected void btnApplyCodeFilter_Click(object sender, EventArgs e)
    //{
    //    id = Request.QueryString["ID"].ToString();
    //    modifierID = Request.QueryString["ModifierID"].ToString();
    //    //Get the Original DisciplineList
    //    DataSet dsRD = PatientManager.GetRemarkableDisciplineList(id, modifierID);
    //    //Now prepare the filter string based on CheckBoxes selected...
    //    string strFilterOriginal = "CodeType=";
    //    ArrayList strArrCode = new ArrayList();
    //    foreach (ListItem chkCodeItem in cblCodeOption.Items)
    //    {
    //        if (chkCodeItem.Selected)
    //        {
    //            strArrCode.Add(chkCodeItem.Value);
    //        }
    //    }
    //    for (int j = 0; j < strArrCode.Count; j++)
    //    {
    //        if (j == 0)
    //            strFilter = strFilterOriginal + "'" + strArrCode[j].ToString() + "'";
    //        else
    //            strFilter += " OR " + strFilterOriginal + "'" + strArrCode[j].ToString() + "'";
    //    }
    //    //Get the Original Table and apply row filter
    //    DataTable dtRD = dsRD.Tables[0].Copy();
    //    DataView dvwRD = dtRD.DefaultView;
    //    dvwRD.RowFilter = strFilter;
    //    DataTable dtRDNew = dvwRD.ToTable(true, new string[] { "Detail", "DisCode", "DisciplineType" });
    //    //Now bind the new Table to the DataList
    //    dlRDQuickLinks.DataSource = dtRDNew;
    //    dlRDQuickLinks.DataBind();
    //}
}
