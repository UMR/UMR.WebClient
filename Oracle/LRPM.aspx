<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LRPM.aspx.cs" Inherits="Oracle_LRPM" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Last Recorded Prescription/Medication</title>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%">
            <tr>
                <td align="right" class="noprint">
                    <img alt="Print Button" src="images/printbutton.png" onclick="return printButton_onclick()"
                        style="cursor: hand" />
                </td>
            </tr>
        </table>
        <div>
            <radG:RadGrid ID="RadGridLRPM" runat="server" AllowPaging="True" EnableAJAX="True" GridLines="None" EnableAJAXLoadingTemplate="true"
                OnItemCreated="RadGridLRPM_ItemCreated" PageSize="20" OnNeedDataSource="RadGridLRPM_NeedDataSource" UseEmbeddedScripts="false">
                <MasterTableView AutoGenerateColumns="false">
                    <ExpandCollapseColumn Visible="False">
                        <HeaderStyle Width="19px" />
                    </ExpandCollapseColumn>
                    <RowIndicatorColumn Visible="False">
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                    <Columns>
                        <radG:GridBoundColumn DataField="Code" HeaderText="Code" UniqueName="Code" />
                        <radG:GridBoundColumn DataField="Version" HeaderText="Version" UniqueName="Version" />
                        <radG:GridBoundColumn DataField="BrandName" HeaderText="Brand Name" UniqueName="BN" />
                        <radG:GridBoundColumn DataField="DateOfService" HeaderText="Date of Service" UniqueName="SDate" />
                        <radG:GridTemplateColumn Display="true" HeaderText="Provider ID" UniqueName="TemplateColumn">
                            <ItemTemplate>
                                <asp:HyperLink ID="Provider" runat="server" ForeColor="blue"></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </radG:GridTemplateColumn>
                        <radG:GridBoundColumn DataField="InstitutionCode" HeaderText="Institution" UniqueName="IC" />
                    </Columns>
                    <HeaderStyle Font-Names="Tahoma" Font-Size="8pt" Font-Underline="true" HorizontalAlign="Left" />
                    <ItemStyle Font-Names="Tahoma, Verdana, Arial" Font-Size="8pt" />
                    <AlternatingItemStyle Font-Names="Tahoma" Font-Size="8pt" />
                </MasterTableView>
                <AJAXLoadingTemplate>
                    <asp:Image ID="Image1" runat="server" AlternateText="Loading..." ImageUrl="~/RadControls/Ajax/Skins/Default/Loading.gif" />
                </AJAXLoadingTemplate>
            </radG:RadGrid>
        </div>
    </form>
    <radW:RadWindowManager ID="RADWMLRPM" runat="server" InitialBehavior="maximize" Skin="Office2007" />
</body>
<script type="text/javascript">
            function ShowProviderDetails(id,dispType)
            {
                window.radopen("ProviderDetails.aspx?ID="+id+"&DispType="+dispType, null);
                return false;
            }
             function printButton_onclick() 
            {
                    var queryString="<%= GetQueryString() %>";
                    window.open("LRPMPrint.aspx?"+queryString,'printWindow', 'resizable=no,width=5,height=5,status=no scrollbars=no,top=100,left=100');
            }
</script>

</html>
