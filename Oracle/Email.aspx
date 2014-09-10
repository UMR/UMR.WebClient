<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Email.aspx.cs" Inherits="Oracle_Email"
    ValidateRequest="false" %>
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Send Email</title>
</head>
<body>
    <form id="form1" runat="server">
        <img src="../Oracle/images/back.png" onclick="return BackButton_onclick()" alt="back"
            style="cursor: hand;" />
        <input type="hidden" runat="server" id="postbackCount" name="postbackCount" value="0" />
        <div style="font-size: 8pt; font-family: Tahoma;">
            Email Address:
            <asp:TextBox ID="txtEmailAddress" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmailAddress"
                ErrorMessage="Email Address Required">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                    ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmailAddress"
                    ErrorMessage="Invalid Email Address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
            <asp:Button ID="btnSendEmail" runat="server" Text="Send" OnClick="btnSendEmail_Click" />
            <asp:Label ID="lblMessage" runat="server"></asp:Label><br />
            &nbsp;<br />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        </div>
    </form>
    <radW:RadWindowManager ID="RadWindowManager3" runat="server" Skin="Office2007" InitialBehavior="maximize"
        Behavior="Close" UseEmbeddedScripts="false">
    </radW:RadWindowManager>
    <script language="javascript" type="text/javascript">
        function GetRadWindow()
		{
			var oWindow = null;
			if (window.radWindow) oWindow = window.radWindow;
			else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
			return oWindow;
		} 
        function BackButton_onclick() 
        {
            var postBackCount=parseInt(document.getElementById("postbackCount").value)+1;
            history.go(-postBackCount)
        }
        var wnd=GetRadWindow();
        wnd.OnClientClose = function(wnd)
        {
           //do nothing
        }
    </script>

</body>
</html>
