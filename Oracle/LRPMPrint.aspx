<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LRPMPrint.aspx.cs" Inherits="Oracle_LRPMPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Last Recorded Prescription/Medication</title>

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
            <radG:RadGrid ID="RadGridLRPM" runat="server" AllowPaging="False" EnableAJAX="True"
                GridLines="None" EnableAJAXLoadingTemplate="true" OnItemCreated="RadGridLRPM_ItemCreated"
                PageSize="20" OnNeedDataSource="RadGridLRPM_NeedDataSource" UseEmbeddedScripts="false">
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
</script>

</html>
