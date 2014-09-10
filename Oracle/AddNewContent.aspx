<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddNewContent.aspx.cs" Inherits="SecuredPages_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Add New Content</title>
    <link href="../App_Themes/Default/Client.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    Generals
                </td>
                <td>
                </td>
               
            </tr>
             <tr>
                <td>
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                        <asp:ListItem>Demographic Information</asp:ListItem>
                        <asp:ListItem>Contact Information</asp:ListItem>
                        <asp:ListItem>Master Pataients Index</asp:ListItem>
                        <asp:ListItem>Insurance Information</asp:ListItem>
                        <asp:ListItem>Provider Informaton</asp:ListItem>
                    </asp:CheckBoxList></td>
                
            </tr>
            <tr>
                <td>
                    Disciplines
                </td>
                <td>
                </td>
                
            </tr>
             <tr>
                <td>
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList2" runat="server">
                        <asp:ListItem>Neorology</asp:ListItem>
                        <asp:ListItem>Dermitolozy</asp:ListItem>
                        <asp:ListItem>dfgdfgdf</asp:ListItem>
                        <asp:ListItem>dfgdfgdfg</asp:ListItem>
                        <asp:ListItem>dfgdfgdfg</asp:ListItem>
                        <asp:ListItem>dfgdfgdfg</asp:ListItem>
                        <asp:ListItem>dfgdfgfdgfdg</asp:ListItem>
                    </asp:CheckBoxList></td>
                
            </tr>
        </table>
        
    </div>
    </form>
</body>
</html>
