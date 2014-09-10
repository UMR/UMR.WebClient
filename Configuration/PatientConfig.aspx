<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PatientConfig.aspx.cs" Inherits="Configuration_PatientConfig" %>

<%@ Register Assembly="RadTabStrip.Net2" Namespace="Telerik.WebControls" TagPrefix="radT" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>
<%@ Reference Control="~/Configuration/ConfigControlLibrary/ucConfigPatient.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Patient Configuration Page</title>
</head>
<body>
        <script type="text/javascript">
        function RowSelected(rowObject, eventObject)
            {
                alert("Row with index: " + rowObject.Index + " selected.");
                alert("Target element: " + eventObject.srcElement.innerHTML);
            }
        function RowClicked(rowIndex){alert (rowIndex);}
        </script>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" EnableViewState="False" Font-Bold="True" Font-Names="Tahoma"
                Font-Size="11pt" ForeColor="#ff00cc"></asp:Label>
            <asp:Label ID="Label2" runat="server" EnableViewState="False" Font-Bold="True" Font-Names="Tahoma"
                Font-Size="11pt" ForeColor="#00ff33"></asp:Label>
            <radG:RadGrid ID="grdPatient" runat="server" AllowMultiRowEdit="false" AllowPaging="True"
                AutoGenerateColumns="False" EnableAJAX="true" EnableAJAXLoadingTemplate="true"
                PageSize="10" ShowStatusBar="true" Skin="Office2007" OnDeleteCommand="grdPatient_DeleteCommand"
                OnInsertCommand="grdPatient_InsertCommand" OnNeedDataSource="grdPatient_NeedDataSource"
                OnUpdateCommand="grdPatient_UpdateCommand" OnEditCommand="grdPatient_EditCommand"
                OnItemCommand="grdPatient_ItemCommand">
                <MasterTableView CommandItemDisplay="TopAndBottom" DataKeyNames="ID,ModifierID" EditMode="EditForms">
                    <RowIndicatorColumn Visible="False">
                        <HeaderStyle Width="18px" />
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn Visible="False">
                        <HeaderStyle Width="19px" />
                    </ExpandCollapseColumn>
                    <PagerStyle Mode="NextPrevAndNumeric" />
                    <Columns>
                        <radG:GridEditCommandColumn ButtonType="ImageButton" CancelImageUrl="../App_Themes/Default/Images/Cancel.gif"
                            EditImageUrl="../App_Themes/Default/Images/Edit.gif" InsertImageUrl="../App_Themes/Default/Images/Insert.gif"
                            UniqueName="EditCommandColumn" UpdateImageUrl="../App_Themes/Default/Images/Update.gif">
                        </radG:GridEditCommandColumn>
                        <radG:GridBoundColumn DataField="ModifierID" UniqueName="ModifierID" Visible="false" />
                        <radG:GridBoundColumn DataField="ID" HeaderText="Patient ID" UniqueName="ID" />
                        <radG:GridBoundColumn DataField="DateOfBirth" HeaderText="Date of Birth" UniqueName="DateOfBirth" />
                        <radG:GridBoundColumn DataField="FirstName" HeaderText="First Name" UniqueName="FirstName" />
                        <radG:GridBoundColumn DataField="LastName" HeaderText="Last Name" UniqueName="LastName" />
                        <radG:GridButtonColumn ButtonType="imageButton" CommandName="Delete" ConfirmText="[Please note that the delete operation might take longer than your regular requests.\nYou will be notified upon completion.]\n\nAre you sure you want to Delete this User?"
                            ImageUrl="../App_Themes/Default/Images/Delete.gif" UniqueName="DeleteColumn">
                            <HeaderStyle Width="20px" />
                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="center" />
                        </radG:GridButtonColumn>
                    </Columns>
                    <EditFormSettings EditFormType="WebUserControl" UserControlName="../Configuration/ConfigControlLibrary/ucConfigPatient.ascx">
                        <EditColumn UniqueName="EditCommandColumn1">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings EnableClientKeyValues="true">
                    <ClientEvents OnRowSelected="RowSelected" OnRowClick="RowClicked" />
                </ClientSettings>
                <AJAXLoadingTemplate>
                    <asp:Image ID="Image1" runat="server" AlternateText="Loading..." ImageUrl="~/RadControls/Ajax/Skins/Default/Loading.gif" />
                </AJAXLoadingTemplate>
            </radG:RadGrid>
        </div>
        <div>
            <br />
            <asp:HyperLink ID="linkSearch" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                NavigateUrl="~/PatientSearchDefault.aspx" Text="Patient Search Page">
            </asp:HyperLink>
            &nbsp;
            <asp:HyperLink ID="linkConfig" runat="server" NavigateUrl="~/Configuration/UserConfig.aspx"
                Text="User Configuration Page" Font-Names="Tahoma" Font-Size="10pt">
            </asp:HyperLink>
            &nbsp;
            <asp:LinkButton ID="lbLogOut" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                OnClick="lbLogOut_Click" Text="LogOut">
            </asp:LinkButton>
        </div>
        
        
    </form>
</body>
</html>
