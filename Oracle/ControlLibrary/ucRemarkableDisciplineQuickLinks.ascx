<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRemarkableDisciplineQuickLinks.ascx.cs" Inherits="Oracle_ControlLibrary_ucRemarkableDisciplineQuickLinks" %>

<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>


<%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Northwind %>"
    SelectCommand="SELECT P.ProductID, P.ProductName FROM Orders O
                    INNER JOIN [Order Details] D ON D.OrderID = O.OrderID
                    INNER JOIN Products P ON P.ProductID = D.ProductID
                    WHERE O.CustomerID = @CustomerID">
    <SelectParameters>
        <asp:QueryStringParameter Direction="Input" Name="CustomerID" QueryStringField="CustomerID"
            Type="string" />
    </SelectParameters>
</asp:SqlDataSource>--%>

<table border="0" cellpadding="5" cellspacing="0">
    <tr>
        <td>
            <asp:DataList ID="dlRDQuickLinks" runat="server" OnItemCreated="dlRDQuickLinks_ItemCreated"
                RepeatColumns="5" RepeatDirection="Horizontal" RepeatLayout="Table" 
                CellSpacing="5">
                <ItemStyle Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" VerticalAlign="Middle" />
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Black"></asp:LinkButton>
                </ItemTemplate>
            </asp:DataList>

            <script type="text/javascript">
            function ShowRemarkableDiscipline(patientKey,codeType,medCode,codeVersion,disCode)
            {
                window.radopen("ProductDetail.aspx?PatientKey=" + patientKey +
                                             "&CodeType=" + codeType + "&medCode=" + medCode
                                    + "&codeVersion=" + codeVersion + "&disCode=" + disCode, null);
                return false; 
            }
            </script>

        </td>
    </tr>
</table>
<radW:RadWindowManager ID="RadWindowManager1" runat="server" Height="500px" Modal="true"
    Skin="Office2007" Width="500px">
    <Windows>
        <radW:RadWindow ID="OrderDetailsWindow" runat="server" Modal="false" />
    </Windows>
</radW:RadWindowManager>
