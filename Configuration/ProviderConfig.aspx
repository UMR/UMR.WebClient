<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProviderConfig.aspx.cs" Inherits="ProviderConfig" %>


<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Provider Configuration Page</title>
    <!-- custom head section -->
    <style type="text/css">
        .MyImageButton { cursor: hand; } 
        div.qsf_wrapper { width:1250px; }
        .EditFormHeader td
        {
            background: #dadec8;
            padding: 5px 0px;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" EnableViewState="False" Font-Names="Tahoma" Font-Size="11pt" Font-Bold="True" ForeColor="#ff00cc"></asp:Label>
            <asp:Label ID="Label2" runat="server" EnableViewState="False" Font-Bold="True" Font-Names="Tahoma" Font-Size="11pt" ForeColor="#00ff33"></asp:Label>
            
            <radG:RadGrid ID="grdProvider" runat="server" AllowAutomaticDeletes="false" AllowAutomaticInserts="true"
                AllowAutomaticUpdates="false" AllowPaging="True" AutoGenerateColumns="False"
                EnableAJAX="true" GridLines="None" PageSize="10" 
                ShowStatusBar="true" Skin="Office2007" 
                OnInsertCommand="grdProvider_InsertCommand" OnNeedDataSource="grdProvider_NeedDataSource" 
                OnUpdateCommand="grdProvider_UpdateCommand" OnDeleteCommand="grdProvider_DeleteCommand">
                <MasterTableView CommandItemDisplay="TopAndBottom" EditMode="EditForms" DataKeyNames="ProviderID">
                    <RowIndicatorColumn Visible="False">
                        <HeaderStyle Width="18px" />
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn Visible="False">
                        <HeaderStyle Width="19px"/>
                    </ExpandCollapseColumn>
                    <PagerStyle Mode="NextPrevAndNumeric" />
                    <Columns>
                        <radG:GridEditCommandColumn ButtonType="ImageButton" CancelImageUrl="App_Themes/Default/Images/Cancel.gif"
                            EditImageUrl="App_Themes/Default/Images/Edit.gif" InsertImageUrl="App_Themes/Default/Images/Insert.gif"
                            UniqueName="EditCommandColumn" UpdateImageUrl="App_Themes/Default/Images/Update.gif">
                        </radG:GridEditCommandColumn>
                        <radG:GridBoundColumn DataField="ProviderID" HeaderText="ID" UniqueName="ProviderID">
                        </radG:GridBoundColumn>
                        <radG:GridBoundColumn DataField="FirstName" HeaderText="First Name" UniqueName="FName">
                            <HeaderStyle Wrap="false" />
                        </radG:GridBoundColumn>
                        <radG:GridBoundColumn DataField="LastName" HeaderText="Last Name" UniqueName="LName">
                            <HeaderStyle Wrap="false" />
                        </radG:GridBoundColumn>
                        <radG:GridTemplateColumn Display="true" HeaderText="Phone">
                            <ItemTemplate>
                                <%#(Eval("Phone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}",Double.Parse((string)Eval("Phone")))%>
                            </ItemTemplate>
                        </radG:GridTemplateColumn>
                        <radG:GridTemplateColumn Display="true" HeaderText="Fax">
                            <ItemTemplate>
                                <%#(Eval("Fax") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Fax")))%>
                            </ItemTemplate>
                        </radG:GridTemplateColumn>
                        <radG:GridTemplateColumn Display="true" HeaderText="Cell">
                            <ItemTemplate>
                                <%#(Eval("CellPhone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("CellPhone")))%>
                            </ItemTemplate>
                        </radG:GridTemplateColumn>
                        <radG:GridTemplateColumn Display="true" HeaderText="Pager">
                            <ItemTemplate>
                                <%#(Eval("Pager") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Pager")))%>
                            </ItemTemplate>
                        </radG:GridTemplateColumn>
                        <radG:GridBoundColumn DataField="InstitutionID" UniqueName="InstitutionID" Visible="true" HeaderText="Institution" />
                        <radG:GridBoundColumn DataField="DisciplineCode" UniqueName="DiscCode" Visible="false" />
                        <radG:GridBoundColumn DataField="InstitutionName" HeaderText="Institution Name" UniqueName="InstitutionName" Visible="false" />
                        <radG:GridDropDownColumn DataField="DisciplineCode" DataSourceID="ODSDiscipline"
                            HeaderText="Discipline" ListTextField="Discipline" ListValueField="DisciplineCode"
                            UniqueName="DisciplineCodeDropDown">
                        </radG:GridDropDownColumn>
                        <%--<radG:GridBoundColumn DataField="Discipline" HeaderText="Discipline" UniqueName="Discipline" />--%>
                        <radG:GridBoundColumn DataField="WebSite" HeaderText="WebSite" UniqueName="WebSite">
                        </radG:GridBoundColumn>
                        <radG:GridBoundColumn DataField="Email" HeaderText="Email" UniqueName="Email">
                        </radG:GridBoundColumn>
                        <radG:GridButtonColumn ButtonType="imageButton" CommandName="Delete" ConfirmText="Are you sure you want to delete the Provider record?"
                            ImageUrl="App_Themes/Default/Images/Delete.gif" UniqueName="DeleteColumn">
                            <HeaderStyle Width="20px" />
                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="center" />
                        </radG:GridButtonColumn>
                    </Columns>
                    <EditFormSettings EditFormType="WebUserControl" UserControlName="Oracle/ControlLibrary/ConfigControls/ucConfigProvider.ascx">
                        <EditColumn UniqueName="EditCommandColumn1">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
            </radG:RadGrid>
        </div>
    </form>
</body>
</html>
