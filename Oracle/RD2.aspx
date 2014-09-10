<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RD2.aspx.cs" Inherits="Oracle_RD2" %>

<%@ Register Src="ControlLibrary/ucLegendCompactCheckBox.ascx" TagName="ucLegendCompact"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Untitled Page</title>
    <style type="text/css">
.tooltip
{
    color:blue;
    text-decoration:underline;
    cursor:hand;
}
#tooltip{
    font-family: Tahoma, Arial, Helvetica, sans-serif;
    font-size:8pt;
	position:absolute;
	border:1px solid #333;
	background:#f7f5d1;
	padding:2px 5px;
	color:#333;
	display:none;
	}	
</style>

    <script src="../jquery.js" type="text/javascript"></script>

    <script src="../tooltip.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <uc1:ucLegendCompact ID="UcLegendCompact1" runat="server" />
            <br />
            <asp:GridView ID="RadGridRDDiagnosisMain" runat="server" AutoGenerateColumns="False"
                OnRowCreated="RadGridRDDiagnosisMain_RowCreated" Width="100%" GridLines="Both">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lblExclamation" runat="server" ForeColor="Red" Font-Names="Tahoma"
                                Font-Size="9pt" Font-Bold="true" />
                        </ItemTemplate>
                        <HeaderStyle Font-Underline="false" />
                        <ItemStyle HorizontalAlign="center" BackColor="white" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Medical Content Index">
                        <ItemTemplate>
                            <asp:HyperLink ID="Details" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="70%" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="FirstDate" HeaderText="First Date" ControlStyle-Width="25px">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LastDate" HeaderText="Last Date" ControlStyle-Width="25px">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Found" HeaderText="F">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle BackColor="PowderBlue" />
                    </asp:BoundField>
                </Columns>
                <HeaderStyle Font-Names="Tahoma" Font-Size="11px" Font-Underline="True" HorizontalAlign="Center" />
                <RowStyle Font-Names="Tahoma,Arial" Font-Size="11px" />
                <SelectedRowStyle ForeColor="Yellow" />
                <AlternatingRowStyle Font-Names="Tahoma, Arial" Font-Size="11px" />
            </asp:GridView>
        </div>
    </form>
    <radW:RadWindowManager ID="RadWindowManager2" runat="server" Skin="Office2007" InitialBehavior="maximize"
        Behavior="Close" VisibleStatusbar="false" UseEmbeddedScripts="false">
    </radW:RadWindowManager>

    <script type="text/javascript">
    function GetRadWindow()
		{
			var oWindow = null;
			if (window.radWindow) oWindow = window.radWindow;
			else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
			return oWindow;
		} 
        function ShowRemarkableDiscipline(url){window.radopen(url, null); return false;}
//        function ShowRemarkableDisciplineDrillDown(id,modifierID,medCode,disType)
//                {window.radopen("RDDetails.aspx?ID="+id+"&ModifierID="+modifierID+"&medCode="+medCode+"&disType="+disType, null);return false;}
        function HideStatus(){var statusMsg ="Click to see the details..."; window.status = statusMsg; return true; } 
        function SetTitle(disName)
        {
            var oWnd = GetRadWindow(); if(oWnd)oWnd.SetTitle(disName);
        }
        function AddToUnvisitedDiscipline(relatedDiscList)
        {
            var rds=relatedDiscList.split(',');
            var rdWnd=GetRadWindow();
            var urlStr= rdWnd.BrowserWindow.location.href;
            if(urlStr.indexOf("RD.aspx")>=0)
            {
                rdWnd.BrowserWindow.AddToUnvisitedDiscipline(rds);
            }
        }
    </script>

</body>
</html>
