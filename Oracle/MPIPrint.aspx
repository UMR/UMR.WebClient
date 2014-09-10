<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MPIPrint.aspx.cs" Inherits="Oracle_MPIPrint" %>

<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Master Patient Code Indexes</title>

    <script type="text/javascript">
       window.print();
       window.onfocus = function() { window.close(); }
    </script>

    <style type="text/css" media="screen">
        #printDiv
        {
            display:none;
        }
        #displayDiv
        {
         font-family: 'trebuchet MS'; 
         font-size: large; 
         font-weight: bold; 
         font-style: normal; 
         color: #336699   
        }
    </style>
    <style type="text/css" media="print">
        #displayDiv
        {
            display:none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="displayDiv">
            Printing...
        </div>
        <div id="printDiv">
            <radG:RadGrid ID="RadGridMPI" runat="server" AllowPaging="false" EnableAJAX="True"
                EnableAJAXLoadingTemplate="true" GridLines="None" OnItemCreated="RadGridMPI_ItemCreated"
                OnNeedDataSource="RadGridMPI_NeedDataSource">
                <MasterTableView AutoGenerateColumns="false">
                    <%--<ExpandCollapseColumn Visible="False">
                        <HeaderStyle Width="19px" />
                    </ExpandCollapseColumn>
                    <RowIndicatorColumn Visible="False">
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>--%>
                    <Columns>
                        <radG:GridBoundColumn DataField="Code" HeaderText="Code" UniqueName="Code" />
                        <radG:GridBoundColumn DataField="Type" HeaderText="Type" UniqueName="Type" />
                        <radG:GridBoundColumn DataField="Version" HeaderText="Version" UniqueName="Version" />
                        <radG:GridBoundColumn DataField="MedicalContentIndex" HeaderText="Medical Content Index"
                            UniqueName="MCI" />
                        <radG:GridBoundColumn DataField="DateOfService" HeaderText="Service Date" UniqueName="SDate" />
                        <radG:GridBoundColumn DataField="ServiceTime" HeaderText="Time (EST)" UniqueName="STime" />
                        <%--<radG:GridTemplateColumn Display="true" HeaderText="Provider ID" UniqueName="TemplateColumn">
                            <ItemTemplate>
                                <asp:HyperLink ID="Provider" runat="server" ForeColor="Blue"></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left"/>
                        </radG:GridTemplateColumn>--%>
                        <radG:GridBoundColumn DataField="ProviderID" HeaderText="Provider ID" UniqueName="PI" />
                        <radG:GridBoundColumn DataField="InstituteCode" HeaderText="Healthcare Facility ID"
                            UniqueName="IC" />
                    </Columns>
                    <NoRecordsTemplate>
                        <asp:Literal ID="NoText" runat="server" Text="<b>No Record to Display</b>"></asp:Literal>
                    </NoRecordsTemplate>
                    <HeaderStyle Font-Names="Tahoma" Font-Size="8pt" Font-Underline="true" HorizontalAlign="Left" />
                    <ItemStyle Font-Names="Tahoma, Verdana, Arial" Font-Size="8pt" />
                    <AlternatingItemStyle Font-Names="Tahoma" Font-Size="8pt" />
                </MasterTableView>
                <AJAXLoadingTemplate>
                    <asp:Image ID="Image1" runat="server" AlternateText="Loading..." ImageUrl="~/RadControls/Ajax/Skins/Default/Loading.gif" />
                </AJAXLoadingTemplate>
            </radG:RadGrid>
            <%--<asp:GridView ID="grdMPI" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                GridLines="None" Width="100%" 
                PageSize="14" OnRowCreated="grdMPI_RowCreated" >
                <HeaderStyle Font-Names="Tahoma" Font-Size="11px" Font-Underline="True" HorizontalAlign="Center" />
                <Columns>
                    <asp:BoundField DataField="Code" HeaderText="Code" />
                    <asp:BoundField DataField="Type" HeaderText="Type" />
                    <asp:BoundField DataField="Version" HeaderText="Version" />
                    <asp:BoundField DataField="MedicalContentIndex" HeaderText="Medical Content Index" />
                    <asp:BoundField DataField="DateOfService" HeaderText="Date of Service" />
                    <asp:TemplateField ShowHeader="true" HeaderText="Provider ID">
                        <ItemTemplate>
                            <asp:HyperLink ID="Provider" runat="server"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="InstituteCode" HeaderText="Institution" />
                </Columns>
                <RowStyle Font-Names="Tahoma,Verdana,Arial" Font-Size="11px" />
            </asp:GridView>--%>
        </div>
    </form>
    <radW:RadWindowManager ID="RADWMMPI" runat="server" InitialBehavior="maximize" Skin="Office2007" />
</body>

<script type="text/javascript">
            function ShowProviderDetails(id,dispType)
            {
                window.radopen("ProviderDetails.aspx?ID="+id+"&DispType="+dispType, null);
                return false;
            }
</script>

</html>
