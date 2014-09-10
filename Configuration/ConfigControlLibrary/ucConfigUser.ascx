<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucConfigUser.ascx.cs"
    Inherits="Oracle_ControlLibrary_ConfigControls_ucConfigUser" %>
<table id="Table2" border="1" cellpadding="1" cellspacing="2" rules="none" style="border-collapse: collapse"
    width="100%">
    <tr class="EditFormHeader">
        <td colspan="2">
            <b>User Details</b></td>
    </tr>
    <tr>
        <td>
            <table id="Table3" border="0" cellpadding="0" cellspacing="0" class="module" width="100%">
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        User ID:</td>
                    <td>
                        <asp:TextBox ID="txtID" runat="server" ReadOnly='<%# !(DataItem is Telerik.WebControls.GridInsertionObject) %>'
                            TabIndex="1" Text='<%# DataBinder.Eval( Container, "DataItem.UserID" ) %>'>
                        </asp:TextBox>
                        <asp:Label ID="Label2" runat="server" Text="*" ForeColor="red" Visible='<%# (DataItem is Telerik.WebControls.GridInsertionObject) %>'>
                        </asp:Label>
                        <asp:RequiredFieldValidator ID="RFVID" runat="server" ControlToValidate="txtID" Enabled='<%# (DataItem is Telerik.WebControls.GridInsertionObject) %>'
                            ErrorMessage="Input required" Font-Names="Tahoma" ForeColor="Red" SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPassword" runat="server" Text="Password:" Visible='<%# (DataItem is Telerik.WebControls.GridInsertionObject) %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" TabIndex="2" TextMode="Password" Visible='<%# (DataItem is Telerik.WebControls.GridInsertionObject) %>'>t</asp:TextBox>
                        <asp:Label ID="Label3" runat="server" ForeColor="red" Text="*" Visible='<%# (DataItem is Telerik.WebControls.GridInsertionObject) %>'>
                        </asp:Label>
                        <asp:RequiredFieldValidator ID="RFVPass" runat="server" ControlToValidate="txtPassword"
                            Enabled='<%# DataItem is Telerik.WebControls.GridInsertionObject %>' ErrorMessage="Input required"
                            Font-Names="Tahoma" ForeColor="Red" SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                        <asp:Label ID="Label1" runat="server" Text="Retype Password:" Visible='<%# (DataItem is Telerik.WebControls.GridInsertionObject) %>'>
                        </asp:Label>
                        <asp:TextBox ID="txtRePassword" runat="server" TabIndex="3" TextMode="Password" Visible='<%# (DataItem is Telerik.WebControls.GridInsertionObject) %>'>t</asp:TextBox>
                        <asp:Label ID="Label4" runat="server" ForeColor="red" Text="*" Visible='<%# (DataItem is Telerik.WebControls.GridInsertionObject) %>'>
                        </asp:Label>
                        <asp:RequiredFieldValidator ID="RFVRePass" runat="server" ControlToValidate="txtRePassword"
                            Enabled='<%# DataItem is Telerik.WebControls.GridInsertionObject %>' ErrorMessage="Input required"
                            Font-Names="Tahoma" ForeColor="Red" SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="PCV" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtRePassword"
                            ErrorMessage="Passwords do not match" SetFocusOnError="true">
                        </asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        First Name:</td>
                    <td>
                        <asp:TextBox ID="txtFName" runat="server" TabIndex="4" Text='<%# DataBinder.Eval( Container, "DataItem.FirstName") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Last Name:</td>
                    <td>
                        <asp:TextBox ID="txtLName" runat="server" TabIndex="5" Text='<%# DataBinder.Eval( Container, "DataItem.LastName") %>'>
                        </asp:TextBox>
                        <span style="color: Red">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLName"
                            ErrorMessage="This field is required" Font-Names="Tahoma" ForeColor="Red" SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Phone:</td>
                    <td>
                        <asp:TextBox ID="txtPhone" runat="server" TabIndex="6" Text='<%# DataBinder.Eval( Container, "DataItem.Phone") %>'>
                        </asp:TextBox>
                        <span style="color: Red">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPhone"
                            ErrorMessage="Input required" Font-Names="Tahoma" ForeColor="Red" SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid phone number"
                            ControlToValidate="txtPhone" ValidationExpression="^[0-9]{1,12}$">
                        </asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Fax:</td>
                    <td>
                        <asp:TextBox ID="txtFax" runat="server" TabIndex="7" Text='<%# DataBinder.Eval( Container, "DataItem.Fax") %>'></asp:TextBox>
                         <span style="color: Red">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFax"
                            ErrorMessage="Input required" Font-Names="Tahoma" ForeColor="Red" SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid fax number"
                            ControlToValidate="txtFax" ValidationExpression="^[0-9]{1,12}$">
                        </asp:RegularExpressionValidator>
                        
                        </td>
                </tr>
                <tr>
                    <td>
                        Email:</td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" TabIndex="8" Text='<%# DataBinder.Eval( Container, "DataItem.Email") %>'></asp:TextBox>
                        <span style="color: Red">* </span><span style="font-family: Tahoma; font-size: 8pt;
                            color: Green">[Will be used during Forgotten/Lost password recovery process] </span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEmail"
                            ErrorMessage="Input required" Font-Names="Tahoma" ForeColor="Red" SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid email address"
                            ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                        </asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Industry:</td>
                    <td>
                        <asp:TextBox ID="txtIndustry" runat="server" TabIndex="9" Text='<%# DataBinder.Eval( Container, "DataItem.Industry") %>'></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        Visibility:</td>
                    <td>
                        <asp:TextBox ID="txtVisibility" runat="server" TabIndex="10" Text='<%# (DataItem is Telerik.WebControls.GridInsertionObject)? 10 : DataBinder.Eval( Container, "DataItem.Visibility") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVVis" runat="server" ControlToValidate="txtVisibility"
                            ErrorMessage="Input required" Font-Names="Tahoma" ForeColor="Red" SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        User Group:</td>
                    <td>
                        <asp:TextBox ID="txtGroup" runat="server" TabIndex="11" Text='<%# (DataItem is Telerik.WebControls.GridInsertionObject)? 0 : DataBinder.Eval(Container, "DataItem.UserGroup") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVGroup" runat="server" ControlToValidate="txtGroup"
                            ErrorMessage="Input required" Font-Names="Tahoma" ForeColor="Red" SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="blbActive" runat="server" Text="Active:" Visible='<%# !(DataItem is Telerik.WebControls.GridInsertionObject) %>' />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkActive" runat="server"  Visible='<%# !(DataItem is Telerik.WebControls.GridInsertionObject) %>' Checked='<%# !(DataItem is Telerik.WebControls.GridInsertionObject)?( Convert.ToInt32( DataBinder.Eval(Container, "DataItem.Active")) == 1? true : false):true %>'  />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2">
            <asp:Button ID="btnUpdate" runat="server" CommandName="Update" TabIndex="12" Text="Update"
                Visible='<%# !(DataItem is Telerik.WebControls.GridInsertionObject) %>' />
            <asp:Button ID="btnInsert" runat="server" CommandName="PerformInsert" TabIndex="13"
                Text="Insert" Visible='<%# DataItem is Telerik.WebControls.GridInsertionObject %>' />
            &nbsp;
            <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                TabIndex="14" Text="Cancel" />
        </td>
    </tr>
</table>
