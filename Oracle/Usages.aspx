<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Usages.aspx.cs" Inherits="Oracle_Usages" %>

<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Usages</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="height:25px;"> 
            <asp:HyperLink ID="linkHome" runat="server" NavigateUrl="~/Default.aspx" Text="Home"
                Font-Names="Tahoma" Font-Size="8pt">
            </asp:HyperLink>&nbsp;&nbsp;&nbsp;
            <asp:HyperLink ID="linkSearch" runat="server" NavigateUrl="~/PatientSearchDefault.aspx"
                Text="Patient Search Page" Font-Names="Tahoma" Font-Size="8pt">
            </asp:HyperLink>
        </div>
        <div>
            <radG:RadGrid ID="RadGridUsages" runat="server" AllowMultiRowEdit="false" AllowPaging="True"
                AutoGenerateColumns="False" EnableAJAX="true" EnableAJAXLoadingTemplate="true"
                PageSize="40" Skin="Office2007" UseEmbeddedScripts="false" OnNeedDataSource="RadGridUsages_NeedDataSource"
                OnDetailTableDataBind="RadGridUsages_DetailTableDataBind">
                <MasterTableView AutoGenerateColumns="false" DataKeyNames="USER_ID">
                    <Columns>
                        <radG:GridBoundColumn DataField="First_Name" HeaderText="First Name" UniqueName="First_Name"
                            HeaderStyle-HorizontalAlign="Left" />
                        <radG:GridBoundColumn DataField="LAST_Name" HeaderText="Last Name" UniqueName="LAST_Name"
                            HeaderStyle-HorizontalAlign="Left" />
                        <radG:GridBoundColumn DataField="INDUSTRY" HeaderText="Industry" UniqueName="INDUSTRY"
                            HeaderStyle-HorizontalAlign="Left" />
                        <radG:GridTemplateColumn HeaderText="Created On" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%# DateTime.Parse( Eval("CREATED_ON").ToString()).ToString("MM-dd-yyyy") %>
                            </ItemTemplate>
                        </radG:GridTemplateColumn>
                        <radG:GridTemplateColumn HeaderText="Last Login Time" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%# DateTime.Parse( Eval("LASTLOGINTIME").ToString()).ToString("MM-dd-yyyy hh:mm:ss tt") %>
                            </ItemTemplate>
                        </radG:GridTemplateColumn>
                        <radG:GridBoundColumn DataField="LOGINCOUNT" HeaderText="Total Login" UniqueName="LOGINCOUNT"
                            HeaderStyle-HorizontalAlign="Left" />
                    </Columns>
                    <DetailTables>
                        <radG:GridTableView Name="InnerGrid" BorderStyle="Solid" BorderColor="Gray" BorderWidth="1px"
                            AutoGenerateColumns="false" AllowPaging="True" PageSize="10">
                            <Columns>
                                <radG:GridTemplateColumn HeaderText="Login Time" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <%# DateTime.Parse( Eval("LoginTime").ToString()).ToString("MM-dd-yyyy hh:mm:ss tt") %>
                                    </ItemTemplate>
                                </radG:GridTemplateColumn>
                                <radG:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName" />
                            </Columns>
                            <PagerStyle Mode="NextPrevAndNumeric" />
                        </radG:GridTableView>
                    </DetailTables>
                    <NoRecordsTemplate>
                        <asp:Literal ID="NoText" runat="server" Text="<b>No Record to Display</b>"></asp:Literal>
                    </NoRecordsTemplate>
                    <PagerStyle Mode="NextPrevAndNumeric" />
                </MasterTableView>
                <AJAXLoadingTemplate>
                    <asp:Image ID="Image1" runat="server" AlternateText="Loading..." ImageUrl="~/RadControls/Ajax/Skins/Default/Loading.gif" />
                </AJAXLoadingTemplate>
            </radG:RadGrid>
        </div>
    </form>
</body>
</html>
