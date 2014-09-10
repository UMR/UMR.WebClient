<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CommonDiagnosesBySexAndAge.aspx.cs"
    Inherits="Oracle_Analyzer_CommonDiagnosesBySexAndAge" %>

<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Common Diagnosis for a sex group with specific age range</title>
    <style type="text/css">
        body
        {
            font-family:Tahoma, Arial, Verdana, Sans-serif;
            font-size:11px;
        }
    </style>
    <style type="text/css" media="print">
        .noprint 
        {
            display:none 
        }
    </style>

    <script type="text/javascript">
    
        function printButton_onclick() 
        {
            window.print();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table style="width: 100%;">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Navy" Font-Bold="true"
                        Text="Find Most Common Diagnosis for a sex group with specific age range"></asp:Label>
                </td>
                <td align="right" class="noprint">
                    <img alt="Print" src="../images/print_button.jpg" onclick="return printButton_onclick()"
                        style="cursor: pointer;" />
                </td>
            </tr>
        </table>
        <br />
        <div style="border: solid 1px #9EB6CE; padding: 5px; margin-bottom: 10px;">
            <table>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="Navy" Text="Search Criteria"
                            Font-Underline="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Sex:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSex" runat="server">
                            <asp:ListItem Text="Both" Value="B"></asp:ListItem>
                            <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                            <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Age:</td>
                    <td>
                        <asp:TextBox ID="txtFrom" runat="server" Width="40px" ControlToValidate="txtFrom"
                            Type="Integer"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFrom"
                            ErrorMessage="From age is required" Text="*"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtFrom"
                            ErrorMessage="Invalid From Age" Type="Integer" Text="*" Operator="DataTypeCheck"></asp:CompareValidator>
                        - &nbsp; &nbsp; &nbsp;
                        <asp:TextBox ID="txtTo" runat="server" Width="40px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTo"
                            ErrorMessage="To age is required" Text="*"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtTo"
                            ErrorMessage="Invalid To Age" Type="Integer" Text="*" Operator="DataTypeCheck"></asp:CompareValidator>
                        &nbsp;[ex. 5-25]
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" /></td>
                </tr>
            </table>
        </div>
        <radG:RadGrid ID="RadGridReport" runat="server" AllowMultiRowEdit="false" AllowPaging="true"
            AllowCustomPaging="true" PageSize="20" AutoGenerateColumns="False" EnableAJAX="true"
            EnableAJAXLoadingTemplate="true" Skin="Office2007" UseEmbeddedScripts="false"
            OnNeedDataSource="RadGridReport_NeedDataSource" OnDetailTableDataBind="RadGridReport_DetailTableDataBind">
            <MasterTableView AutoGenerateColumns="false" DataKeyNames="CODE_TYPE,MEDCODE,CODE_VERSION">
                <Columns>
                    <radG:GridBoundColumn DataField="CODE_TYPE" HeaderText="Code Type" UniqueName="CODE_TYPE"
                        HeaderStyle-HorizontalAlign="Left" />
                    <radG:GridBoundColumn DataField="MEDCODE" HeaderText="Medcode" UniqueName="MEDCODE"
                        HeaderStyle-HorizontalAlign="Left" />
                    <radG:GridBoundColumn DataField="CODE_VERSION" HeaderText="Version" UniqueName="CODE_VERSION"
                        HeaderStyle-HorizontalAlign="Left" />
                    <radG:GridBoundColumn DataField="Freq" HeaderText="Frequency" UniqueName="Freq" HeaderStyle-HorizontalAlign="Left" />
                </Columns>
                <DetailTables>
                    <radG:GridTableView Name="InnerGrid" BorderStyle="Solid" BorderColor="Gray" BorderWidth="1px"
                        AutoGenerateColumns="false" AllowPaging="True" PageSize="20" AllowCustomPaging="True">
                        <Columns>
                            <radG:GridBoundColumn DataField="FIRST_NAME" HeaderText="First Name" UniqueName="FIRST_NAME"
                                HeaderStyle-HorizontalAlign="Left" />
                            <radG:GridBoundColumn DataField="LAST_NAME" HeaderText="Last Name" UniqueName="LAST_NAME" />
                            <radG:GridBoundColumn DataField="Age" HeaderText="Age" UniqueName="Age" />
                        </Columns>
                    </radG:GridTableView>
                </DetailTables>
                <NoRecordsTemplate>
                    <asp:Literal ID="NoText" runat="server" Text="<b>No Record to Display</b>"></asp:Literal>
                </NoRecordsTemplate>
            </MasterTableView>
            <AJAXLoadingTemplate>
                <asp:Image ID="Image1" runat="server" AlternateText="Loading..." ImageUrl="~/RadControls/Ajax/Skins/Default/Loading.gif" />
            </AJAXLoadingTemplate>
        </radG:RadGrid>
    </form>
</body>
</html>
