<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRD2.ascx.cs" Inherits="Oracle_ControlLibrary_ucRD2" %>
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>


<div>
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
<radW:RadWindowManager ID="RadWindowManager2" runat="server" Skin="Office2007"
    VisibleStatusbar="false" UseEmbeddedScripts="false">
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
</script>

