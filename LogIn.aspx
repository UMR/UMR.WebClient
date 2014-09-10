<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogIn.aspx.cs" Inherits="LogIn" %>
<%--<%@ OutputCache Duration="20" VaryByParam="*"%>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Log In Page</title>
    <link href="App_Themes/Default/Default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" height="100%" width="100%">
            <tr height="100px">
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr height="2px" style="background-color:Gray">
                            <td>
                            </td>
                        </tr>
                        <tr style="background-color:Silver; color:Black">
                            <td>
                                <table cellpadding="0" cellspacing="0" height="30%" width="100%">
                                    <tr>
                                        <td align="center" valign="middle" width="55%">
                                            Welcome to
                                            <br />
                                            Universal Medical Records</td>
                                        <td width="5%">
                                        </td>
                                        <td width="40%">
                                            <table border="0" cellpadding="1" cellspacing="0" style="border-collapse: collapse">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0">
                                                            <tr>
                                                                <td align="center" colspan="3">
                                                                    Log In</td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="txtUserName">User Name:</asp:Label></td>
                                                                <td align="right">
                                                                    <asp:TextBox ID="txtUserName" Width="150px" runat="server"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtUserName"
                                                                        ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="txtPassword">Password:</asp:Label></td>
                                                                <td align="right">
                                                                    <asp:TextBox ID="txtPassword" runat="server" Width="150px" TextMode="Password">q</asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtPassword"
                                                                        ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <%--<tr>
                                                                <td align="right">
                                                                    <asp:CheckBox ID="RememberMe" runat="server" Text="Remember Me" />
                                                                </td>
                                                                
                                                            </tr>--%>
                                                            <tr>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" OnClick="LoginButton_Click"
                                                                        Text="Log In" ValidationGroup="Login1" />
                                                                </td>
                                                                <td style="color: red" align="left">
                                                                    <asp:Label ID="lblErrMsg" runat="server" EnableViewState="False" ForeColor="red"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr height="2px" style="background-color: Gray">
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr height="400px">
                <td>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
