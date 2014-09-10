<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserDetails.aspx.cs" Inherits="Oracle_UserDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Accessor Details</title>
    <style type="text/css" media="print">
        .noprint 
        {
            display:none 
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%">
            <tr>
                <td align="right" class="noprint">
                    <img alt="Fax" src="../Oracle/images/fax_button.jpg" onclick="return faxButton_onclick()"
                        style="cursor: pointer;" />
                    <img alt="Email" src="../Oracle/images/email_button.jpg" onclick="return emailButton_onclick()"
                        style="cursor: hand" />
                    <img alt="Print" src="../Oracle/images/print_button.jpg" onclick="return printButton_onclick()"
                        style="cursor: hand" />
                </td>
            </tr>
        </table>
        <div id="displayDiv">
            <asp:DetailsView ID="detailsUserDetails" runat="server" AutoGenerateRows="False"
                GridLines="None" CellPadding="4" ForeColor="#333333">
                <Fields>
                    <asp:BoundField DataField="UserID" HeaderText="User ID:" />
                    <asp:BoundField DataField="FirstName" HeaderText="First Name:" />
                    <asp:BoundField DataField="LastName" HeaderText="Last Name:" />
                    <asp:TemplateField HeaderText="Phone Numbe:r" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <%#(Eval("Phone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Phone")))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fax Number:" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <%#(Eval("Fax") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Fax")))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Email" HeaderText="Email:" />
                    <asp:BoundField DataField="Industry" HeaderText="Industry:" />
                    <asp:TemplateField HeaderText="Website:" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <%#(Eval("Website") == DBNull.Value) ? "N/A" : String.Format("<a href=\"{0}\" target=\"_blank\">{0}</a>", Eval("Website").ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Fields>
                <HeaderStyle Font-Names="Tahoma" Font-Size="11px" Font-Underline="True" HorizontalAlign="Center"
                    BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <RowStyle Font-Names="Tahoma,Arial" Font-Size="11px" BackColor="#F7F6F3" ForeColor="#333333" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
                <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:DetailsView>
        </div>
    </form>
    <form name="gotoEmailForm" action="Email.aspx" method="post">
        <input type="hidden" name="emailHidden" id="emailHidden" />
        <input type="hidden" name="subject" id="subject" value="User Details" />
    </form>
    <form name="gotoFaxForm" action="Fax.aspx" method="post">
        <input type="hidden" name="faxHidden" id="faxHidden" />
        <input type="hidden" name="subjectFax" id="subjectFax" value="User Details" />
    </form>

    <script type="text/javascript">
        function printButton_onclick() 
        {
            window.print();
        }
        function emailButton_onclick()
        {
            var emailText= document.getElementById("displayDiv").innerHTML;   
            document.getElementById("emailHidden").value =emailText;
            document.gotoEmailForm.submit();
        }
        function faxButton_onclick() 
        {
            var faxText= document.getElementById("displayDiv").innerHTML;   
            document.getElementById("faxHidden").value =faxText;
            document.gotoFaxForm.submit();
        }
    </script>

</body>
</html>
