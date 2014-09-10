<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucMDSPreview.ascx.cs" Inherits="Oracle_ControlLibrary_PrintPreview_ucMDSPreview" %>
<asp:Panel ID="pnlMain" runat="server" ScrollBars="Vertical" Height="300px">
    <asp:GridView ID="RadGridMDSMain" runat="server" AutoGenerateColumns="False" Width="99%" GridLines="Both" >
        <Columns>
            <%--<asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblExclamation" runat="server" ForeColor="Red" Font-Names="Tahoma"
                        Font-Size="8pt" Font-Bold="true" />
                </ItemTemplate>
                <HeaderStyle Font-Underline="false" />
                <ItemStyle HorizontalAlign="center" BackColor="white" />
            </asp:TemplateField>--%>
            <%--<asp:TemplateField HeaderText="MEDCODE">
                <ItemTemplate>
                    <asp:HyperLink ID="Details" runat="server" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" Width="60%" />
            </asp:TemplateField>--%>
            <asp:BoundField DataField="MEDCODE" HeaderText="Code">
                <HeaderStyle Font-Underline="false" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <%--<asp:BoundField DataField="CODE_TYPE" HeaderText="Type">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>--%>
            <%--<asp:BoundField DataField="MEDICAL_CONTENT_INDEX" HeaderText="Medical Content">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>--%>
            <asp:BoundField DataField="DETAIL" HeaderText="Detail">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="VALUE" HeaderText="Value">
                <HeaderStyle Font-Underline="false" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="CODEDATE" HeaderText="Service Date">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <%--<asp:BoundField DataField="Found" HeaderText="F">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
                <FooterStyle BackColor="PowderBlue" />
            </asp:BoundField>--%>
        </Columns>
        <HeaderStyle Font-Names="Tahoma" Font-Size="8pt" Font-Underline="True" HorizontalAlign="Center" />
        <RowStyle Font-Names="Tahoma,Arial" Font-Size="8pt" />
        <SelectedRowStyle ForeColor="Yellow" />
        <AlternatingRowStyle Font-Names="Tahoma, Arial" Font-Size="8pt" />
    </asp:GridView>
</asp:Panel>