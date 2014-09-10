<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InstitutionDetails.aspx.cs"
    Inherits="Oracle_InstitutionDetails" %>

<%@ Register Src="ControlLibrary/ucLegendCompactCheckBox.ascx" TagName="ucLegendCompact"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Facility Locator</title>

    <script src="../jquery.js" type="text/javascript"></script>

   <script type="text/javascript" src="ControlLibrary/jQueryTooltip/jquery.tooltip.js"></script>
     <script type="text/javascript" src="ControlLibrary/jQueryTooltip/jquery.dimensions.js"></script>
   <script type="text/javascript">
        $(function() {
            $('.tooltip').tooltip({
	            track: true,
	            delay: 0,
	            showURL: false,
	            showBody: " - ",
	            fade: 250
            });
        });
    </script>

    <style type="text/css" media="print">
        .noprint 
        {
            display:none 
        }
    </style>
    <style type="text/css" media="screen">
        .tooltip
        {
            /*color:blue;
            text-decoration:none;
            cursor:hand;*/
        }
        #tooltip
        {
            font-family: Tahoma, Arial, Helvetica, sans-serif;
            font-size:8pt;
	        position:absolute;
	        border:1px solid #333;
	        background:#f7f5d1;
	        padding:2px 5px;
	        color:#333;
	        display:none;
	     }	
	    .lnkButton
	    {
	        font-family: Tahoma, Arial, Helvetica, sans-serif;
            color: black;
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
                        style="cursor: pointer;" />
                    <img alt="Print" src="../Oracle/images/print_button.jpg" onclick="return printButton_onclick()"
                        style="cursor: pointer;" />
                </td>
            </tr>
        </table>
        <div id="displayDiv">
            <div id="codeDiv" runat="server" visible="false">
                <uc2:ucLegendCompact ID="UcLegendCompact1" runat="server" />
                <asp:GridView ID="grdCode" AutoGenerateColumns="false" runat="server" GridLines="Horizontal"
                    OnRowDataBound="grdCode_RowDataBound" AllowSorting="true" OnSorting="grdCode_Sorting">
                    <Columns>
                        <asp:BoundField DataField="Code" SortExpression="Code" HeaderText="Code" ItemStyle-Width="75px" />
                        <asp:TemplateField HeaderText="Modifier" ItemStyle-Width="50px">
                            <ItemTemplate>
                                <asp:Literal ID="ltCodeModifier" runat="server" Text="">
                                </asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Type" SortExpression="Type" HeaderText="Type" ItemStyle-Width="50px" />
                        <asp:BoundField DataField="Version" SortExpression="Version" HeaderText="Version"
                            ItemStyle-Width="40px" />
                        <asp:TemplateField SortExpression="MedicalContentIndex" HeaderText="Medical Content Index">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkMedicalContentIndex" runat="server" ForeColor="#355E3B"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DateOfService" SortExpression="DateOfService" HeaderText="Service Date"
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px" />
                        <asp:BoundField DataField="ServiceTime" SortExpression="ServiceTime" HeaderText="Time (EST)"
                            ItemStyle-Width="70px" />
                        <%--<asp:BoundField DataField="PROVIDERID" SortExpression="PROVIDERID" HeaderText="Provider ID" />
                        <asp:BoundField DataField="PROVIDERNAME" SortExpression="DoctorsFirstName" HeaderText="Provider Name" />--%>
                        <asp:TemplateField SortExpression="DoctorsFirstName" HeaderText="Provider Name" HeaderImageUrl="~/Oracle/images/Unsortedlist.png">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkProviderName" runat="server"></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderTemplate>
                                <asp:LinkButton ID="btnProviderName" Text="Provider ID/Name" runat="server" CssClass="lnkButton" OnClick="btnProviderName_Click"></asp:LinkButton>
                            </HeaderTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="PROVIDERID" HeaderText="Provider ID" HeaderImageUrl="~/Oracle/images/Unsortedlist.png"
                            Visible="false">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkProviderID" runat="server"></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderTemplate>
                                <asp:LinkButton ID="btnProviderID" Text="Provider ID/Name" runat="server" CssClass="lnkButton"
                                    OnClick="btnProviderID_Click"></asp:LinkButton>
                            </HeaderTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle Font-Names="Tahoma" Font-Size="11px" Font-Underline="True" ForeColor="black"
                        HorizontalAlign="Left" />
                    <RowStyle Font-Names="Tahoma,Verdana,Arial" Font-Size="11px" HorizontalAlign="Left" />
                    <AlternatingRowStyle Font-Names="Tahoma,Verdana,Arial" Font-Size="11px" HorizontalAlign="Left" />
                </asp:GridView>
                <br />
            </div>
            <div id="codedivSingle" runat="server" visible="false">
                <uc2:ucLegendCompact ID="UcLegendCompact2" runat="server" />
                <asp:GridView ID="grdCodeSingle" AutoGenerateColumns="false" runat="server" GridLines="Horizontal"
                    OnRowDataBound="grdCodeSingle_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="Code" SortExpression="Code" HeaderText="Code" ItemStyle-Width="75px" />
                        <asp:TemplateField HeaderText="Modifier" ItemStyle-Width="50px">
                            <ItemTemplate>
                                <asp:Literal ID="ltCodeModifier" runat="server" Text="">
                                </asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Type" SortExpression="Type" HeaderText="Type" ItemStyle-Width="80px" />
                        <asp:BoundField DataField="Version" SortExpression="Version" HeaderText="Version"
                            ItemStyle-Width="40px" />
                        <asp:TemplateField SortExpression="MedicalContentIndex" HeaderText="Medical Content Index">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkMedicalContentIndex" runat="server" ForeColor="#355E3B"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DateOfService" SortExpression="DateOfService" HeaderText="Service Date"
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px" />
                        <asp:BoundField DataField="ServiceTime" SortExpression="ServiceTime" HeaderText="Time (EST)"
                            ItemStyle-Width="70px" />
                        <%--<asp:BoundField DataField="ProviderID" SortExpression="ProviderID" HeaderText="Provider ID" />
                        <asp:BoundField DataField="PROVIDERNAME" SortExpression="DoctorsFirstName" HeaderText="Provider Name" />--%>
                        <asp:TemplateField SortExpression="DoctorsFirstName" HeaderText="Provider Name" HeaderImageUrl="~/Oracle/images/Unsortedlist.png">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkProviderName" runat="server"></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderTemplate>
                                <asp:LinkButton ID="btnProviderName" Text="Provider ID/Name" runat="server" CssClass="lnkButton" OnClick="btnSingleProviderName_Click"></asp:LinkButton>
                            </HeaderTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="PROVIDERID" HeaderText="Provider ID" HeaderImageUrl="~/Oracle/images/Unsortedlist.png"
                            Visible="false">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkProviderID" runat="server"></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderTemplate>
                                <asp:LinkButton ID="btnProviderID" Text="Provider ID/Name" runat="server" CssClass="lnkButton" OnClick="btnSingleProviderID_Click"></asp:LinkButton>
                            </HeaderTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle Font-Names="Tahoma" Font-Size="11px" Font-Underline="True" ForeColor="black"
                        HorizontalAlign="Left" />
                    <RowStyle Font-Names="Tahoma,Verdana,Arial" Font-Size="11px" HorizontalAlign="Left" />
                    <AlternatingRowStyle Font-Names="Tahoma,Verdana,Arial" Font-Size="11px" HorizontalAlign="Left" />
                </asp:GridView>
                <br />
            </div>
            <div>
                <asp:DetailsView ID="dvwInstitution" runat="server" AutoGenerateRows="False" GridLines="None"
                    CellPadding="4" ForeColor="#333333">
                    <HeaderStyle Font-Names="Tahoma" Font-Size="11px" Font-Underline="True" HorizontalAlign="Center"
                        BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <RowStyle Font-Names="Tahoma,Arial" Font-Size="11px" BackColor="#F7F6F3" ForeColor="#333333" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
                    <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Fields>
                        <asp:BoundField DataField="InstitutionID" HeaderText="Healthcare Facility ID:" />
                        <asp:BoundField DataField="InstitutionName" HeaderText="Healthcare Facility Name:" />
                        <asp:TemplateField HeaderText="Facility Type:">
                            <ItemTemplate>
                                <asp:HyperLink ID="lblFType" ToolTip='<%# Eval("InstitutionTypeDesc") %>' Text='<%# Eval("InstitutionType") %>'
                                    CssClass="tooltip" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Phone Number:">
                            <ItemTemplate>
                                <%#String.IsNullOrEmpty(Eval("Phone").ToString().Trim()) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Phone")))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fax Number:">
                            <ItemTemplate>
                                <%#String.IsNullOrEmpty(Eval("Fax").ToString().Trim()) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Fax")))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Street" HeaderText="Address:" />
                        <asp:BoundField DataField="Website" HeaderText="Website:" />
                        <asp:BoundField DataField="Email" HeaderText="Email:" />
                        <asp:BoundField DataField="CityTown" HeaderText="City/Town:" />
                        <asp:BoundField DataField="County" HeaderText="County:" />
                        <asp:BoundField DataField="State" HeaderText="State:" />
                        <asp:BoundField DataField="ZipCode" HeaderText="Zip Code:" />
                    </Fields>
                </asp:DetailsView>
            </div>
        </div>
    </form>
    <form name="gotoEmailForm" action="Email.aspx" method="post">
        <input type="hidden" name="emailHidden" id="emailHidden" />
        <input type="hidden" name="subject" id="subject" value="Healthcare Facility Details" />
    </form>
    <form name="gotoFaxForm" action="Fax.aspx" method="post">
        <input type="hidden" name="faxHidden" id="faxHidden" />
        <input type="hidden" name="subjectFax" id="subjectFax" value="Healthcare Facility Details" />
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
