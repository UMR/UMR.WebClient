<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HealthcareFacilities.aspx.cs"
    Inherits="Oracle_HealthcareFacilities" %>

<%@ Register Src="ControlLibrary/ucLegendCompactCheckBox.ascx" TagName="ucLegendCompact"
    TagPrefix="uc1" %>
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Healthcare Facilities</title>
    <style type="text/css" media="print">
        .noprint 
        {
            display:none 
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
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
                <div class="noprint">
                    <uc1:ucLegendCompact ID="UcLegendCompact1" runat="server" />
                </div>
                <table style="width: 100%;">
                    <tr>
                        <td align="left" valign="top">
                            <asp:GridView ID="grdInstitutions" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                ForeColor="#333333" GridLines="None" DataKeyNames="InstitutionID" AllowSorting="True"
                                OnSorting="grdInstitutions_Sorting" OnRowDataBound="grdInstitutions_RowDataBound" PageSize="100"
                                OnRowCommand="grdInstitutions_RowCommand" AllowPaging="true" OnPageIndexChanging="grdInstitutions_PageIndexChanging">
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" Font-Names="Tahoma" Font-Size="11px"
                                    Font-Underline="True" ForeColor="White" HorizontalAlign="Left" />
                                <RowStyle BackColor="#F7F6F3" Font-Names="Tahoma,Arial" Font-Size="11px" ForeColor="#333333" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:ButtonField CommandName="Details" HeaderText=" ">
                                        <ItemStyle HorizontalAlign="Center" Width="0px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:ButtonField>
                                    <asp:BoundField DataField="InstitutionID" SortExpression="InstitutionID" HeaderText="ID" />
                                    <asp:BoundField DataField="InstitutionName" SortExpression="InstitutionName" HeaderText="Facility Name" />
                                    <asp:TemplateField HeaderText="Facility Type" SortExpression="InstitutionType">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lblFType" ToolTip='<%# Eval("InstitutionTypeDesc") %>' Text='<%# Eval("InstitutionType") %>'
                                                CssClass="tooltip" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="First Date" SortExpression="FirstCodeDate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFirstDate" Text='<%#  DateTime.Parse(Eval("FirstCodeDate").ToString()).ToString("MM/dd/yyyy") %>'
                                                runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Last Date" SortExpression="LastCodeDate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblServiceDate" Text='<%#  DateTime.Parse(Eval("LastCodeDate").ToString()).ToString("MM/dd/yyyy") %>'
                                                runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="F" SortExpression="CodeFreq">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFreq" runat="server" Text='<%# GetFreqLabelText(Eval("CodeFreq").ToString(),Eval("InstitutionID").ToString()) %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <span style="font-size: 8pt; font-family: Tahoma; font-weight: bold;">No Healtcare Facilities
                                    </span>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                        <td align="left" valign="top">
                            <asp:DetailsView ID="dvwHF" runat="server" AutoGenerateRows="False" DataKeyNames="InstitutionID"
                                GridLines="None" CellPadding="4" ForeColor="#333333" Width="250px">
                                <Fields>
                                    <asp:BoundField DataField="InstitutionID" HeaderText="Healthcare Facility ID:" />
                                    <asp:BoundField DataField="InstitutionName" HeaderText="Facility Name:" />
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
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
    <form name="gotoEmailForm" action="Email.aspx" method="post">
        <input type="hidden" name="emailHidden" id="emailHidden" />
        <input type="hidden" name="subject" id="subject" value="Healthcare Facilities" />
    </form>
    <form name="gotoFaxForm" action="Fax.aspx" method="post">
        <input type="hidden" name="faxHidden" id="faxHidden" />
        <input type="hidden" name="subjectFax" id="subjectFax" value="Healthcare Facilities" />
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
       function GetRadWindow()
	    {
	        var oWindow = null;
	        if (window.radWindow) oWindow = window.radWindow;
	        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
	        return oWindow;
	    } 
        GetRadWindow().SetWidth(890);
        function ShowInstitutionDrilldown(institutionID,patientKey)
        {
             window.radopen("InstitutionProvidersCodes.aspx?code="+institutionID+"&PatientKey="+patientKey, null);
             return false;
        }
    </script>

    <radW:RadWindowManager ID="RADWMHF" UseEmbeddedScripts="false" runat="server" InitialBehavior="maximize"
        Behavior="Close" Skin="Office2007" />
</body>
</html>
