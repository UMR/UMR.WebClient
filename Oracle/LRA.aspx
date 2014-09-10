<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LRA.aspx.cs" Inherits="Oracle_LRA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Last Record Accessed Details</title>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%">
            <tr>
                <td align="right" class="noprint">
                    <img alt="Fax" src="../Oracle/images/fax_button.jpg" onclick="return faxButton_onclick()"
                        style="cursor: pointer;" />
                    <img alt="Email" src="../Oracle/images/email_button.jpg" onclick="return emailButton_onclick()"
                        style="cursor: pointer;" />
                    <img alt="Print" src="../Oracle/images/print_button.jpg" onclick="return printRangeButton_onclick()"
                        style="cursor: pointer;" />
                </td>
            </tr>
        </table>
        <div>
            <radG:RadGrid ID="RadGridLRA" runat="server" AllowPaging="True" EnableAJAX="True"
                AllowSorting="true" EnableAJAXLoadingTemplate="true" GridLines="None" OnNeedDataSource="RadGridLRA_NeedDataSource"
                PageSize="20" OnItemCreated="RadGridLRA_ItemCreated" OnSortCommand="RadGridLRA_SortCommand">
                <MasterTableView AutoGenerateColumns="false">
                    <ExpandCollapseColumn Visible="False">
                        <HeaderStyle Width="19px" />
                    </ExpandCollapseColumn>
                    <RowIndicatorColumn Visible="False">
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                    <Columns>
                        <radG:GridTemplateColumn SortExpression="AccessTime" Display="true" HeaderText="Access Time (EST)">
                            <ItemTemplate>
                                <%# String.Format("{0:MM-dd-yyyy hh:mm:ss tt}", DateTime.Parse(Eval("AccessTime").ToString()))%>
                            </ItemTemplate>
                        </radG:GridTemplateColumn>
                        <radG:GridTemplateColumn SortExpression="RecordUpdateTime" Display="true" HeaderText="Record Update Time (EST)">
                            <ItemTemplate>
                                <%# (Eval("RecordUpdateTime") == DBNull.Value) ? "N/A" :  GetDateString(Eval("UserName").ToString(), Eval("RecordUpdateTime").ToString())%>
                            </ItemTemplate>
                        </radG:GridTemplateColumn>
                         <radG:GridTemplateColumn   Display="true" HeaderText="Login Duration">
                            <ItemTemplate>
                                <%# GetLoginDuration(Eval("AccessTime"), Eval("RecordUpdateTime"))%>
                            </ItemTemplate>
                        </radG:GridTemplateColumn>
                        <radG:GridTemplateColumn SortExpression="UserName" Display="true" HeaderText="Accessor">
                            <ItemTemplate>
                                <asp:HyperLink ForeColor="blue" ID="lnkAccessorName" runat="server"></asp:HyperLink>
                            </ItemTemplate>
                        </radG:GridTemplateColumn>
                        <radG:GridTemplateColumn Display="true" HeaderText="Details">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDetail" runat="server" ImageUrl="~/Oracle/images/DownArrow.png" />
                            </ItemTemplate>
                            <HeaderStyle Font-Underline="false" />
                            <ItemStyle HorizontalAlign="center" />
                        </radG:GridTemplateColumn>
                        <%--<radG:GridBoundColumn SortExpression="Industry" DataField="Industry" HeaderText="Industry"
                            UniqueName="INDT" />--%>
                        <%--<radG:GridBoundColumn DataField="Phone" HeaderText="Phone" UniqueName="Phone" />--%>
                        <%--<radG:GridTemplateColumn SortExpression="Phone" Display="true" HeaderText="Phone">
                            <ItemTemplate>
                                <%#(Eval("Phone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Phone")))%>
                            </ItemTemplate>
                        </radG:GridTemplateColumn>--%>
                        <%--<radG:GridBoundColumn DataField="Fax" HeaderText="Fax" UniqueName="Fax" />--%>
                        <%--<radG:GridTemplateColumn SortExpression="Fax" Display="true" HeaderText="Fax">
                            <ItemTemplate>
                                <%#(Eval("Fax") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Fax")))%>
                            </ItemTemplate>
                        </radG:GridTemplateColumn>--%>
                    </Columns>
                     <PagerStyle Mode="NextPrevAndNumeric" />
                    <HeaderStyle Font-Names="Tahoma" Font-Size="8pt" Font-Underline="true" HorizontalAlign="Left" />
                    <ItemStyle Font-Names="Tahoma, Verdana, Arial" Font-Size="8pt" />
                    <AlternatingItemStyle Font-Names="Tahoma" Font-Size="8pt" />
                </MasterTableView>
                <AJAXLoadingTemplate>
                    <asp:Image ID="Image1" runat="server" AlternateText="Loading..." ImageUrl="~/RadControls/Ajax/Skins/Default/Loading.gif" />
                </AJAXLoadingTemplate>
            </radG:RadGrid>
            <radW:RadWindowManager ID="RADWMMPI" runat="server" InitialBehavior="maximize" Skin="Office2007"
                Behavior="Close" />
        </div>
    </form>

    <script type="text/javascript">
        function printButton_onclick() 
        {
                var queryString="<%= GetQueryString() %>";
                window.open("LRAPrint.aspx?"+queryString,'printWindow', 'resizable=no,width=5,height=5,status=no scrollbars=no,top=100,left=100');
        }
        function printRangeButton_onclick()
        {
             var queryString="<%= GetQueryStringWithPageCount() %>";
             window.open("LRAPrintRange.aspx?"+queryString,'printWindow', 'resizable=no,width=370,height=150,status=no scrollbars=no,top=300,left=200');
        }
        function emailButton_onclick()
        {
             var queryString="<%= GetQueryStringWithPageCount() %>";
             window.radopen("LRAEmail.aspx?"+queryString,null);
        }
        function faxButton_onclick() 
        {
            var queryString="<%= GetQueryStringWithPageCount() %>";
            window.radopen("LRAFax.aspx?"+queryString,null);
        }
        function ShowAccessorDetails(userName)
        {
             window.radopen("UserDetails.aspx?UserName="+userName, null);
             return false;
        }
    </script>

</body>
</html>
