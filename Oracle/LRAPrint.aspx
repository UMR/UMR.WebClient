<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LRAPrint.aspx.cs" Inherits="Oracle_LRAPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Last Record Access Details</title>

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
            <radG:RadGrid ID="RadGridLRA" runat="server" EnableAJAX="True" EnableAJAXLoadingTemplate="true"
                GridLines="None" OnNeedDataSource="RadGridLRA_NeedDataSource" PageSize="20">
                <MasterTableView AutoGenerateColumns="False">
                    <ExpandCollapseColumn Visible="False">
                        <HeaderStyle Width="19px" />
                    </ExpandCollapseColumn>
                    <RowIndicatorColumn Visible="False">
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                    <Columns>
                        <radG:GridBoundColumn DataField="AccessTime" HeaderText="Access Time" UniqueName="AT" />
                         <radG:GridTemplateColumn SortExpression="RecordUpdateTime" Display="true" HeaderText="Record Update Time (EST)">
                            <ItemTemplate>
                                <%# (Eval("RecordUpdateTime") == DBNull.Value) ? "N/A" : String.Format("{0:MM-dd-yyyy hh:mm:ss tt}", DateTime.Parse(Eval("RecordUpdateTime").ToString()))%>
                            </ItemTemplate>
                        </radG:GridTemplateColumn>
                        <radG:GridBoundColumn DataField="UserName" HeaderText="Accessor" UniqueName="User" />
                        <radG:GridBoundColumn DataField="Industry" HeaderText="Industry" UniqueName="INDT" />                        
                        <radG:GridTemplateColumn HeaderText="Phone" UniqueName="TemplateColumn">
                            <ItemTemplate>
                                <%#(Eval("Phone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Phone")))%>
                            </ItemTemplate>
                        </radG:GridTemplateColumn>
                        <radG:GridTemplateColumn HeaderText="Fax" UniqueName="TemplateColumn1">
                            <ItemTemplate>
                                <%#(Eval("Fax") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Fax")))%>
                            </ItemTemplate>
                        </radG:GridTemplateColumn>
                    </Columns>
                    <HeaderStyle Font-Names="Tahoma" Font-Size="8pt" Font-Underline="True" HorizontalAlign="Left" />
                    <ItemStyle Font-Names="Tahoma,Verdana,Arial" Font-Size="8pt" />
                    <AlternatingItemStyle Font-Names="Tahoma" Font-Size="8pt" />
                </MasterTableView>
                <AJAXLoadingTemplate>
                    <asp:Image ID="Image1" runat="server" AlternateText="Loading..." ImageUrl="~/RadControls/Ajax/Skins/Default/Loading.gif" />
                </AJAXLoadingTemplate>
            </radG:RadGrid>
        </div>
    </form>
</body>
</html>
