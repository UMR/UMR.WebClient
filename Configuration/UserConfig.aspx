<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserConfig.aspx.cs" Inherits="UserConfig" %>

<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>User Configuration Page</title>
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
            <asp:Label ID="Label1" runat="server" EnableViewState="False" Font-Bold="True" Font-Names="Tahoma"
                Font-Size="11pt" ForeColor="#ff00cc"></asp:Label>
            <asp:Label ID="Label2" runat="server" EnableViewState="False" Font-Bold="True" Font-Names="Tahoma"
                Font-Size="11pt" ForeColor="#00ff33"></asp:Label>
            <radG:RadGrid ID="grdUser" runat="server" AllowMultiRowEdit="false" AllowPaging="True"
                AutoGenerateColumns="False" EnableAJAX="true" EnableAJAXLoadingTemplate="true"
                PageSize="10" ShowStatusBar="true" Skin="Office2007" OnDeleteCommand="grdUser_DeleteCommand"
                OnInsertCommand="grdUser_InsertCommand" OnNeedDataSource="grdUser_NeedDataSource"
                OnUpdateCommand="grdUser_UpdateCommand" UseEmbeddedScripts="false">
                <MasterTableView CommandItemDisplay="TopAndBottom" DataKeyNames="UserID" EditMode="EditForms">
                    <RowIndicatorColumn Visible="False">
                        <HeaderStyle Width="18px" />
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn Visible="False">
                        <HeaderStyle Width="19px" />
                    </ExpandCollapseColumn>
                    <PagerStyle Mode="NextPrevAndNumeric" />
                    <CommandItemSettings AddNewRecordText="Add New User" />
                    <Columns>
                        <radG:GridEditCommandColumn ButtonType="ImageButton" CancelImageUrl="../App_Themes/Default/Images/Cancel.gif"
                            EditImageUrl="../App_Themes/Default/Images/Edit.gif" InsertImageUrl="../App_Themes/Default/Images/Insert.gif"
                            UniqueName="EditCommandColumn" UpdateImageUrl="../App_Themes/Default/Images/Update.gif">
                        </radG:GridEditCommandColumn>
                        <radG:GridBoundColumn DataField="UserID" HeaderText="ID" UniqueName="ID" />
                        <radG:GridBoundColumn DataField="FirstName" HeaderText="First Name" UniqueName="FName"
                            HeaderStyle-Wrap="false" />
                        <radG:GridBoundColumn DataField="LastName" HeaderText="Last Name" UniqueName="LName"
                            HeaderStyle-Wrap="false" />
                        <radG:GridTemplateColumn Display="true" HeaderText="Phone">
                            <ItemTemplate>
                                <%#(Eval("Phone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}",Double.Parse((string)Eval("Phone")))%>
                            </ItemTemplate>
                        </radG:GridTemplateColumn>
                        <radG:GridTemplateColumn Display="true" HeaderText="Fax">
                            <ItemTemplate>
                                <%#(Eval("Fax") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}",Double.Parse((string)Eval("Fax")))%>
                            </ItemTemplate>
                        </radG:GridTemplateColumn>
                        <radG:GridBoundColumn DataField="Email" HeaderText="Email" UniqueName="Email" />
                        <radG:GridBoundColumn DataField="Industry" HeaderText="Industry" UniqueName="Industry" />
                        <radG:GridBoundColumn DataField="Visibility" HeaderText="Visibility" UniqueName="Visibility" />
                        <radG:GridBoundColumn DataField="UserGroup" HeaderText="Group" UniqueName="Group" />
                        <radG:GridBoundColumn DataField="CreatedOn" HeaderText="Created On" UniqueName="CreatedOn" />
                         <radG:GridTemplateColumn Display="true" HeaderText="Active">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkActiveInactive" runat="server" Enabled="false" Checked='<%# Convert.ToInt32( Eval("Active")) == 1? true : false%>' />
                            </ItemTemplate>
                        </radG:GridTemplateColumn>
                        <radG:GridButtonColumn ButtonType="imageButton" Visible="false" CommandName="Delete" ConfirmText="[Please note that the delete operation might take longer than your regular requests.\nYou will be notified upon completion.]\n\nAre you sure you want to Delete this User?"
                            ImageUrl="../App_Themes/Default/Images/Delete.gif" UniqueName="DeleteColumn">
                            <HeaderStyle Width="20px" />
                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="center" />
                        </radG:GridButtonColumn>
                    </Columns>
                    <EditFormSettings EditFormType="WebUserControl" UserControlName="../Configuration/ConfigControlLibrary/ucConfigUser.ascx">
                        <EditColumn UniqueName="EditCommandColumn1">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <AJAXLoadingTemplate>
                    <asp:Image ID="Image1" runat="server" AlternateText="Loading..." ImageUrl="~/RadControls/Ajax/Skins/Default/Loading.gif" />
                </AJAXLoadingTemplate>
            </radG:RadGrid>
        </div>
        <div>
            <br /> 
            <asp:HyperLink ID="lnkHome" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                NavigateUrl="~/Default.aspx" Text="Home">
            </asp:HyperLink>
              &nbsp;
            <asp:HyperLink ID="linkSearch" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                NavigateUrl="~/PatientSearchDefault.aspx" Text="Patient Search Page">
            </asp:HyperLink>
            &nbsp;
            <asp:LinkButton ID="lbLogOut" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                OnClick="lbLogOut_Click" Text="LogOut">
            </asp:LinkButton>
        </div>
    </form>
</body>
</html>
