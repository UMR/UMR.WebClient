<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucConfigProvider.ascx.cs" Inherits="Oracle_ControlLibrary_ConfigControls_ucConfigProvider" %>


<table id="Table2" border="1" cellpadding="1" cellspacing="2" rules="none" style="border-collapse: collapse"
    width="100%">
    <tr class="EditFormHeader">
        <td colspan="2">
            <b>Provider Details</b></td>
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
                        ID:</td>
                    <td>
                        <asp:TextBox ID="txtID" runat="server" ReadOnly='<%# !(DataItem is Telerik.WebControls.GridInsertionObject) %>'
                            Text='<%# DataBinder.Eval( Container, "DataItem.ProviderID" ) %>' TabIndex="0">
                        </asp:TextBox>
                        <span style="color: Red" visible='<%# !(DataItem is Telerik.WebControls.GridInsertionObject) %>'>*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtID"
                            Enabled='<%# DataItem is Telerik.WebControls.GridInsertionObject %>' ErrorMessage="This field is required"
                            Font-Names="Tahoma" ForeColor="Red" SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="REVId" runat="server" 
                            ControlToValidate="txtID"
                            ErrorMessage="Invalid ID. Valid ex. 1234567890" 
                            ValidationExpression="^[0-9]$">
                        </asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        First Name:</td>
                    <td>
                        <asp:TextBox ID="txtFName" runat="server" TabIndex="1" Text='<%# DataBinder.Eval( Container, "DataItem.FirstName") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Last Name:</td>
                    <td>
                        <asp:TextBox ID="txtLName" runat="server" TabIndex="2" Text='<%# DataBinder.Eval( Container, "DataItem.LastName") %>' />
                        <span style="color: Red">*</span>
                        <asp:RequiredFieldValidator ID="RFVLName" runat="server" ControlToValidate="txtLName"
                            ErrorMessage="This field is required" Font-Names="Tahoma" ForeColor="Red"
                            SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Home Phone:</td>
                    <td>
                        <asp:TextBox ID="txtPhone" runat="server" TabIndex="3" Text='<%# DataBinder.Eval( Container, "DataItem.Phone") %>'/>
                        <span style="color:Red">*</span>
                        <asp:RequiredFieldValidator ControlToValidate="txtPhone" runat="server" 
                        ErrorMessage="This field is required" SetFocusOnError="true" ForeColor="Red" Font-Names="Tahoma"/>
                        <asp:RegularExpressionValidator ID="REVPhone" runat="server" 
                            ErrorMessage="Invalid Phone Number. Valid ex. 5556780123"
                            ControlToValidate="txtPhone"
                            ValidationExpression="^[0-9]{10,10}$">
                        </asp:RegularExpressionValidator>
                    </td>
                    
                </tr>
                <tr>
                    <td>
                        Fax:</td>
                    <td>
                        <asp:TextBox ID="txtFax" runat="server" TabIndex="4" Text='<%# DataBinder.Eval( Container, "DataItem.Fax") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Cell Phone:</td>
                    <td>
                        <asp:TextBox ID="txtCell" runat="server" TabIndex="5" Text='<%# DataBinder.Eval( Container, "DataItem.CellPhone") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Pager:</td>
                    <td>
                        <asp:TextBox ID="txtPager" runat="server" TabIndex="6" Text='<%# DataBinder.Eval( Container, "DataItem.Pager") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Institution:</td>
                    <td>
                        <asp:TextBox ID="txtInst" runat="server" TabIndex="7" Text='<%# DataBinder.Eval( Container, "DataItem.InstitutionID") %>' />
                        <span style="color: Red">*</span>
                        <asp:RequiredFieldValidator ID="RFVInst" runat="server" ControlToValidate="txtInst"
                            ErrorMessage="This field is required" Font-Names="Tahoma" ForeColor="Red"
                            SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Discipline:</td>
                    <td>
                        <asp:DropDownList ID="ddlDiscipline" runat="server" TabIndex="8" OnDataBinding="ddlDiscipline_DataBinding"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        WebSite:</td>
                    <td>
                        <asp:TextBox ID="txtWeb" runat="server" TabIndex="9" Text='<%# DataBinder.Eval( Container, "DataItem.WebSite") %>' />
                        <asp:RegularExpressionValidator ID="REVWeb" runat="server" 
                            ErrorMessage="Invalid website address. Valid ex. http://Google.com"
                            ControlToValidate="txtWeb"
                            ValidationExpression="^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$">
                        </asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Email:</td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" TabIndex="10" Text='<%# DataBinder.Eval( Container, "DataItem.Email") %>' />
                        <asp:RegularExpressionValidator ID="REVEmail" runat="server" ErrorMessage="Invalid email address"
                            ControlToValidate="txtEmail"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                        </asp:RegularExpressionValidator>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2">
            <asp:Button ID="btnUpdate" runat="server" CommandName="Update" Text="Update" Visible='<%# !(DataItem is Telerik.WebControls.GridInsertionObject) %>' TabIndex="11" />
            <asp:Button ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Insert" Visible='<%# DataItem is Telerik.WebControls.GridInsertionObject %>' TabIndex="12" />
            &nbsp;
            <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" TabIndex="13" />
        </td>
    </tr>
</table>
