<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProviderCodeDateHistory.aspx.cs"
    Inherits="Oracle_ProviderCodeDateHistory" %>

<%@ Register Src="ControlLibrary/ucLegendCompactCheckBox.ascx" TagName="ucLegendCompact"
    TagPrefix="uc1" %>
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Service History</title>
    <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />

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

    <style type="text/css">
        .tooltip
        {
            /*color:blue;
            text-decoration:none;
            cursor:hand;*/
        }
        #tooltip{
            font-family: Tahoma, Arial, Helvetica, sans-serif;
            font-size:8pt;
	        position:absolute;
	        border:1px solid #333;
	        background:#f7f5d1;
	        padding:2px 5px;
	        color:#333;
	        display:none;
	        }	
	    .headerStyle
	    {
	        font-weight:bold;   
	        font-family:Tahoma;
	        font-size:8pt;
	        text-decoration:underline;
	        text-align:left;
            background:#5D7B9D;
            color:White;
	    }
	
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="width: 100%;">
            <tr>
                <td>
                    <uc1:ucLegendCompact ID="UcLegendCompact1" runat="server" />
                </td>
                <td align="right" class="noprint">
                    <img alt="Fax" src="../Oracle/images/fax_button.jpg" onclick="return faxButton_onclick()"
                        style="cursor: pointer;" />
                    <img alt="Email" src="../Oracle/images/email_button.jpg" onclick="return emailButton_onclick()"
                        style="cursor: pointer;" />
                    <img alt="Print" src="../Oracle/images/print_button.jpg" onclick="return printClick()"
                        style="cursor: pointer;" />
                </td>
            </tr>
        </table>
        <div style="font-size: 8pt;" id="displayDiv">
            <table>
                <tr>
                    <td align="left" valign="top">
                        <asp:GridView ID="GridView1" runat="server" CellPadding="4" DataKeyNames="CODEDATE"
                            AllowSorting="true" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False"
                            OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" OnSorting="GridView1_Sorting">
                            <RowStyle Font-Names="Tahoma,Arial" Font-Size="11px" BackColor="#F7F6F3" ForeColor="#333333" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <EditRowStyle BackColor="#999999" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" SelectText="" HeaderText=" " ShowHeader="false" />
                                <asp:TemplateField HeaderText="Date of Service" SortExpression="CODEDATE">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%#  DateTime.Parse(Eval("CODEDATE").ToString()).ToString("MM/dd/yyyy") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Time (EST)" SortExpression="CODEDATE">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTime" runat="server" Text='<%#  DateTime.Parse(Eval("CODEDATE").ToString()).ToString("hh:mm:ss tt") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle Font-Names="Tahoma" Font-Size="11px" Font-Underline="True" HorizontalAlign="Left"
                                BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                    </td>
                    <td align="left" valign="top">
                        <div id="filterDiv" runat="server" visible="false">
                            <asp:CheckBoxList ID="cblCodeOption" runat="server" Font-Names="Tahoma,Arial,Verdana"
                                Font-Size="8pt" RepeatColumns="5" RepeatDirection="Horizontal" RepeatLayout="flow"
                                ForeColor="Blue">
                                <asp:ListItem Text="CPT" Value="CPT-4" Selected="True" />
                                <asp:ListItem Text="HCPCS" Value="HCPCS" Selected="True" />
                                <asp:ListItem Text="ICD" Value="ICD-9-CM" Selected="True" />
                                <asp:ListItem Text="MDS" Value="MDS" Selected="False" />
                                <asp:ListItem Text="NDC" Value="NDC" Selected="True" />
                                <asp:ListItem Text="OASIS" Value="OASIS" Selected="False" />
                            </asp:CheckBoxList>
                            <asp:Button ID="btnApplyCodeFilter" runat="server" Text="Apply" Font-Names="Tahoma"
                                Font-Size="8pt" ForeColor="Blue" OnClick="btnApplyCodeFilter_Click" />
                        </div>
                        <radG:RadGrid ID="RadGridMPI" runat="server" EnableAJAXLoadingTemplate="true" GridLines="None"
                            UseEmbeddedScripts="false" EnableAJAX="true" AllowSorting="true" OnItemCreated="RadGridMPI_ItemCreated"
                            OnSortCommand="RadGridMPI_SortCommand">
                            <MasterTableView AutoGenerateColumns="False">
                                <Columns>
                                    <radG:GridBoundColumn DataField="Code" HeaderText="Code" UniqueName="Code" ItemStyle-Width="75px" />
                                    <radG:GridTemplateColumn HeaderText="Modifier" ItemStyle-Width="50px" >
                                        <ItemTemplate>
                                            <asp:Literal ID="ltCodeModifier" runat="server" Text="">
                                            </asp:Literal>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="false" />
                                    </radG:GridTemplateColumn>
                                    <radG:GridBoundColumn DataField="Type" HeaderText="Type" UniqueName="Type" ItemStyle-Width="50px"/>
                                    <radG:GridBoundColumn DataField="Version" HeaderText="Version" UniqueName="Version"  ItemStyle-Width="40px" />
                                    <radG:GridTemplateColumn SortExpression="MedicalContentIndex" HeaderText="Medical Content Index"
                                        UniqueName="MCI">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkMedicalContentIndex" runat="server" ForeColor="#355E3B"></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </radG:GridTemplateColumn>
                                    <%--
                                    <radG:GridTemplateColumn SortExpression="ProviderID" Display="true" HeaderText="Provider ID"
                                        UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="Provider" runat="server" ForeColor="Blue"></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </radG:GridTemplateColumn>
                                    <radG:GridTemplateColumn SortExpression="InstituteCode" Display="true" HeaderText="Healthcare Facility ID"
                                        UniqueName="IC">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="Institution" runat="server" ForeColor="Blue"></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </radG:GridTemplateColumn>
                                    --%>
                                    <radG:GridTemplateColumn SortExpression="ProviderID" HeaderImageUrl="../Oracle/images/Unsortedlist.png" ItemStyle-Width="110px"
                                        UniqueName="ProviderID">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="Provider" runat="server" ForeColor="Blue" ></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="btnProviderID" Text="Provider ID/Name" ForeColor="#FFFFFF" runat="server" OnClick="btnProviderID_Click"></asp:LinkButton>
                                        </HeaderTemplate>
                                    </radG:GridTemplateColumn>
                                    <radG:GridTemplateColumn SortExpression="DoctorsFirstName" HeaderImageUrl="../Oracle/images/Unsortedlist.png" ItemStyle-Width="110px"
                                        UniqueName="ProviderName">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlProviderName" runat="server" ForeColor="Blue"></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="btnProviderName" Text="Provider ID/Name" runat="server" ForeColor="#FFFFFF" OnClick="btnProviderName_Click"></asp:LinkButton>
                                        </HeaderTemplate>
                                    </radG:GridTemplateColumn>
                                    <radG:GridTemplateColumn SortExpression="InstituteCode" HeaderImageUrl="../Oracle/images/Unsortedlist.png" ItemStyle-Width="155px"
                                        UniqueName="IC">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="Institution" runat="server" ForeColor="Blue"></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="btnInstitutionID" Text="Healthcare Facility ID/Name" runat="server" ForeColor="#FFFFFF"
                                                OnClick="btnInstitutionID_Click"></asp:LinkButton>
                                        </HeaderTemplate>
                                    </radG:GridTemplateColumn>
                                    <radG:GridTemplateColumn SortExpression="InstitutionName" HeaderImageUrl="../Oracle/images/Unsortedlist.png" ItemStyle-Width="155px"
                                        UniqueName="InstitutionName">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlHealthcare" runat="server" ForeColor="Blue"></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="btnInstitutionName" Text="Healthcare Facility ID/Name" runat="server" ForeColor="#FFFFFF"
                                                OnClick="btnInstitutionName_Click"></asp:LinkButton>
                                        </HeaderTemplate>
                                    </radG:GridTemplateColumn>
                                </Columns>
                                <NoRecordsTemplate>
                                    <asp:Literal ID="NoText" runat="server" Text="<b>No Record to Display</b>"></asp:Literal>
                                </NoRecordsTemplate>
                                <PagerStyle Mode="NextPrevAndNumeric" />
                                <HeaderStyle Font-Names="Tahoma" Font-Size="11px" Font-Underline="True" HorizontalAlign="Left"
                                    Font-Bold="true" BackColor="#5D7B9D" ForeColor="White" />
                                <ItemStyle Font-Names="Tahoma,Verdana,Arial" Font-Size="8pt" />
                                <AlternatingItemStyle Font-Names="Tahoma" Font-Size="8pt" />
                                <RowIndicatorColumn Visible="False">
                                    <HeaderStyle Width="20px" />
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn Visible="False">
                                    <HeaderStyle Width="19px" />
                                </ExpandCollapseColumn>
                            </MasterTableView>
                            <AJAXLoadingTemplate>
                                <asp:Image ID="Image1" runat="server" AlternateText="Loading..." ImageUrl="~/RadControls/Ajax/Skins/Default/Loading.gif" />
                            </AJAXLoadingTemplate>
                            <HeaderStyle Font-Bold="True" />
                        </radG:RadGrid>
                    </td>
                </tr>
            </table>
        </div>
    </form>
    <form name="gotoEmailForm" action="Email.aspx" method="post">
        <input type="hidden" name="emailHidden" id="emailHidden" />
        <input type="hidden" name="subject" id="subject" value="Provider Service History" />
    </form>
    <form name="gotoFaxForm" action="Fax.aspx" method="post">
        <input type="hidden" name="faxHidden" id="faxHidden" />
        <input type="hidden" name="subjectFax" id="subjectFax" value="Provider Service History" />
    </form>
    <radW:RadWindowManager ID="RADWMMPI" UseEmbeddedScripts="false" runat="server" InitialBehavior="maximize"
        Behavior="Close" Skin="Office2007" />
</body>

<script type="text/javascript">
    function GetRadWindow()
    {
        var oWindow = null;
        if (window.radWindow) oWindow = window.radWindow;
        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
        return oWindow;
    } 
    function ShowProviderDetails(id,dispType,showCodeStr)
    {
        window.radopen("ProviderDetails.aspx?ID="+id+"&DispType="+dispType+"&"+showCodeStr, null);
        return false;
    }
    function ShowInstitutionDetails(code,patientKey,hasCodeStr)
    {
        window.radopen("InstitutionDetails.aspx?code="+code+"&PatientKey="+patientKey+"&"+hasCodeStr, null);
        return false;
    }
    function printClick() 
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
//    var wnd=GetRadWindow();
//    var urlStr= wnd.BrowserWindow.location.href;
//    if(urlStr.indexOf("MultiProviderListDetails.aspx")>=0)
//    {
//        wnd.BrowserWindow.SetNewWidth(875);
//    }
//    wnd.OnClientClose = function(wnd)
//    {
//        wnd.BrowserWindow.SetNewWidth(840);
//    }
</script>

</html>
