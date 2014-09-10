<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucProviderDetails.ascx.cs"
    Inherits="Oracle_ControlLibrary_ucProviderDetails" %>
<%@ Register Src="ucLegendCompactCheckBox.ascx" TagName="ucLegendCompact" TagPrefix="uc1" %>
<%--<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>

<radg:radgrid id="RadGrid1" runat="server" allowpaging="True" enableajax="True" gridlines="None" EnableAJAXLoadingTemplate="true"
    pagesize="8" skin="Office2007" UseEmbeddedScripts="false" OnNeedDataSource="RadGrid1_NeedDataSource">
    <MasterTableView AutoGenerateColumns="false">
        <ExpandCollapseColumn Visible="False">
            <HeaderStyle Width="19px" />
        </ExpandCollapseColumn>
        <RowIndicatorColumn Visible="False">
            <HeaderStyle Width="20px" />
        </RowIndicatorColumn>
        <Columns>
            <radG:GridBoundColumn DataField="LastName" HeaderText="Last Name" UniqueName="LName" />
            <radG:GridBoundColumn DataField="FirstName" HeaderText="First Name" UniqueName="FName" />
            <radg:GridTemplateColumn HeaderText="Phone" UniqueName="HomePhone">
                <ItemTemplate>
                    <%#(Eval("Phone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Phone")))%>
                </ItemTemplate>
            </radg:GridTemplateColumn>
            <radG:GridTemplateColumn HeaderText="Fax" UniqueName="HomePhone">
                <ItemTemplate>
                    <%#(Eval("Fax") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Fax")))%>
                </ItemTemplate>
            </radG:GridTemplateColumn>
            <radG:GridTemplateColumn HeaderText="CellPhone" UniqueName="HomePhone">
                <ItemTemplate>
                    <%#(Eval("CellPhone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("CellPhone")))%>
                </ItemTemplate>
            </radG:GridTemplateColumn>
            <radG:GridTemplateColumn HeaderText="Pager" UniqueName="HomePhone">
                <ItemTemplate>
                    <%#(Eval("Pager") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Pager")))%>
                </ItemTemplate>
            </radG:GridTemplateColumn>
            <radG:GridBoundColumn DataField="InstitutionID" HeaderText="Institution" UniqueName="Institution" />
            <radG:GridBoundColumn DataField="Speciality" HeaderText="Speciality" UniqueName="Speciality" />
            <radG:GridBoundColumn DataField="WebSite" HeaderText="WebSite" UniqueName="WebSite" />
            <radG:GridBoundColumn DataField="Email" HeaderText="Email" UniqueName="Email" />
        </Columns>
    </MasterTableView>
    <AJAXLoadingTemplate>
        <asp:Image ID="Image1" runat="server" AlternateText="Loading..." ImageUrl="~/RadControls/Ajax/Skins/Default/Loading.gif" />
    </AJAXLoadingTemplate>
</radg:radgrid>
--%>
<%--<asp:GridView ID="grdProviderDetails" runat="server" AutoGenerateColumns="False"
    GridLines="None" Width="100%">
    <Columns>
        <asp:BoundField DataField="FirstName" HeaderText="First Name" />
        <asp:BoundField DataField="LastName" HeaderText="Last Name" />
        <asp:BoundField DataField="DoctorID" HeaderText="ID" ReadOnly="True" />
        <asp:TemplateField HeaderText="Business Ph" HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
                <%#(Eval("Phone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Phone")))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Mobile Ph" HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
                <%#(Eval("CellPhone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("CellPhone")))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Fax No" HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
                <%#(Eval("Fax") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Fax")))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Pager No" HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
                <%#(Eval("Pager") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Pager")))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="InstitutionID" HeaderText="Institution" />
       <asp:BoundField DataField="Discipline" HeaderText="Discipline" ItemStyle-HorizontalAlign="Center" />
        <asp:BoundField DataField="Speciality" HeaderText="Speciality" />
        <asp:BoundField DataField="WebSite" HeaderText="Website" />
        <asp:BoundField DataField="Email" HeaderText="Email" />
    </Columns>
    <HeaderStyle Font-Names="Tahoma" Font-Size="11px" Font-Underline="True" HorizontalAlign="Left" />
    <RowStyle Font-Names="Tahoma,Verdana,Arial" Font-Size="11px" />
</asp:GridView>--%>
<div id="codeDiv" runat="server" visible="false">
    <uc1:ucLegendCompact ID="UcLegendCompact1" runat="server" />
    <asp:GridView ID="grdCode" AutoGenerateColumns="false" runat="server" GridLines="Horizontal"
        OnRowDataBound="grdCode_RowDataBound">
        <Columns>
            <asp:BoundField DataField="Code" HeaderText="Code" />
            <asp:TemplateField HeaderText="Modifier">
                <ItemTemplate>
                    <asp:Literal ID="ltCodeModifier" runat="server" Text="">
                    </asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Type" HeaderText="Type" />
            <asp:BoundField DataField="Version" HeaderText="Version" />
            <asp:TemplateField HeaderText="Medical Content Index">
                <ItemTemplate>
                    <asp:HyperLink ID="lnkMedicalContentIndex" runat="server" ForeColor="#355E3B"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="DateOfService" HeaderText="Service Date" />
            <asp:BoundField DataField="ServiceTime" HeaderText="Time (EST)" />
        </Columns>
        <HeaderStyle Font-Names="Tahoma" Font-Size="11px" Font-Underline="True" ForeColor="black"
            HorizontalAlign="Left" />
        <RowStyle Font-Names="Tahoma,Verdana,Arial" Font-Size="11px" HorizontalAlign="Left" />
        <AlternatingRowStyle Font-Names="Tahoma,Verdana,Arial" Font-Size="11px" HorizontalAlign="Left" />
    </asp:GridView>
    <br />
</div>
<asp:DetailsView ID="detailsProviderDetails" runat="server" AutoGenerateRows="False"
    GridLines="None" CellPadding="4" ForeColor="#333333">
    <Fields>
        <asp:BoundField DataField="DoctorID" HeaderText="Provider ID:" />
        <asp:BoundField DataField="FirstName" HeaderText="First Name:" />
        <asp:BoundField DataField="LastName" HeaderText="Last Name:" />
        <asp:TemplateField HeaderText="Facility Type:">
            <ItemTemplate>
                <asp:GridView ID="grdHFs" ShowHeader="false" BackColor="#F7F6F3" BorderColor="#FFFFFF"
                    OnRowDataBound="grdHFs_RowDataBound" BorderWidth="1px" CellPadding="4" runat="server"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="InstitutionType" />
                    </Columns>
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                    <RowStyle BackColor="#FFFFFF" ForeColor="#003399" />
                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                </asp:GridView>
            </ItemTemplate>
        </asp:TemplateField>
        
        <%--<asp:BoundField DataField="DoctorID" HeaderText="ID" ReadOnly="True" />--%>
        <%--<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>

<radg:radgrid id="RadGrid1" runat="server" allowpaging="True" enableajax="True" gridlines="None" EnableAJAXLoadingTemplate="true"
    pagesize="8" skin="Office2007" UseEmbeddedScripts="false" OnNeedDataSource="RadGrid1_NeedDataSource">
    <MasterTableView AutoGenerateColumns="false">
        <ExpandCollapseColumn Visible="False">
            <HeaderStyle Width="19px" />
        </ExpandCollapseColumn>
        <RowIndicatorColumn Visible="False">
            <HeaderStyle Width="20px" />
        </RowIndicatorColumn>
        <Columns>
            <radG:GridBoundColumn DataField="LastName" HeaderText="Last Name" UniqueName="LName" />
            <radG:GridBoundColumn DataField="FirstName" HeaderText="First Name" UniqueName="FName" />
            <radg:GridTemplateColumn HeaderText="Phone" UniqueName="HomePhone">
                <ItemTemplate>
                    <%#(Eval("Phone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Phone")))%>
                </ItemTemplate>
            </radg:GridTemplateColumn>
            <radG:GridTemplateColumn HeaderText="Fax" UniqueName="HomePhone">
                <ItemTemplate>
                    <%#(Eval("Fax") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Fax")))%>
                </ItemTemplate>
            </radG:GridTemplateColumn>
            <radG:GridTemplateColumn HeaderText="CellPhone" UniqueName="HomePhone">
                <ItemTemplate>
                    <%#(Eval("CellPhone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("CellPhone")))%>
                </ItemTemplate>
            </radG:GridTemplateColumn>
            <radG:GridTemplateColumn HeaderText="Pager" UniqueName="HomePhone">
                <ItemTemplate>
                    <%#(Eval("Pager") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Pager")))%><RADG:GRIDBOUNDCOLUMN 
UniqueName="Institution" HeaderText="Institution" DataField="InstitutionID" 
/><RADG:GRIDBOUNDCOLUMN UniqueName="Speciality" HeaderText="Speciality" 
DataField="Speciality" /><RADG:GRIDBOUNDCOLUMN UniqueName="WebSite" 
HeaderText="WebSite" DataField="WebSite" /><RADG:GRIDBOUNDCOLUMN 
UniqueName="Email" HeaderText="Email" DataField="Email" /><AJAXLOADINGTEMPLATE 
/><asp:Image id="Image1" ImageUrl="~/RadControls/Ajax/Skins/Default/Loading.gif" AlternateText="Loading..." runat="server"></asp:Image> 
--%&gt; <%--<asp:GridView ID="grdProviderDetails" runat="server" AutoGenerateColumns="False"
    GridLines="None" Width="100%">
    <Columns>
        <asp:BoundField DataField="FirstName" HeaderText="First Name" />
        <asp:BoundField DataField="LastName" HeaderText="Last Name" />
        <asp:BoundField DataField="DoctorID" HeaderText="ID" ReadOnly="True" />
        <asp:TemplateField HeaderText="Business Ph" HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
                <%#(Eval("Phone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Phone")))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Mobile Ph" HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
                <%#(Eval("CellPhone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("CellPhone")))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Fax No" HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
                <%#(Eval("Fax") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Fax")))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Pager No" HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
                <%#(Eval("Pager") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Pager")))%><ASP:BOUNDFIELD 
HeaderText="Institution" DataField="InstitutionID" /><ASP:BOUNDFIELD 
HeaderText="Discipline" DataField="Discipline" 
ItemStyle-HorizontalAlign="Center" /><ASP:BOUNDFIELD HeaderText="Speciality" 
DataField="Speciality" /><ASP:BOUNDFIELD HeaderText="Website" 
DataField="WebSite" /><ASP:BOUNDFIELD HeaderText="Email" DataField="Email" 
/><HEADERSTYLE HorizontalAlign="Left" Font-Underline="True" Font-Size="11px" 
Font-Names="Tahoma" /><ROWSTYLE Font-Size="11px" 
Font-Names="Tahoma,Verdana,Arial" />--%&gt; <DIV id="code" runat="server" 
__designer:dtid="1970324836974592" visible="false"><asp:GridView id="GridView1" runat="server" ForeColor="#333333" CellPadding="4" GridLines="None" __designer:dtid="1970324836974593">
<FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></FooterStyle>

<RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>

<PagerStyle HorizontalAlign="Center" BackColor="#284775" ForeColor="White"></PagerStyle>

<SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

<HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>

<EditRowStyle BackColor="#999999"></EditRowStyle>

<AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
</asp:GridView> </DIV><asp:DetailsView id="detailsProviderDetails" runat="server" ForeColor="#333333" CellPadding="4" GridLines="None" AutoGenerateRows="False" __designer:dtid="1970324836974594">
    <Fields __designer:dtid="1970324836974595">
        <asp:BoundField __designer:dtid="1970324836974596" DataField="FirstName" HeaderText="First Name:" ></asp:BoundField>
        <asp:BoundField __designer:dtid="1970324836974597" DataField="LastName" HeaderText="Last Name:" ></asp:BoundField>
        <%--<asp:BoundField DataField="DoctorID" HeaderText="ID" ReadOnly="True" />--%>
        <%--<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>

<radg:radgrid id="RadGrid1" runat="server" allowpaging="True" enableajax="True" gridlines="None" EnableAJAXLoadingTemplate="true"
    pagesize="8" skin="Office2007" UseEmbeddedScripts="false" OnNeedDataSource="RadGrid1_NeedDataSource">
    <MasterTableView AutoGenerateColumns="false">
        <ExpandCollapseColumn Visible="False">
            <HeaderStyle Width="19px" />
        </ExpandCollapseColumn>
        <RowIndicatorColumn Visible="False">
            <HeaderStyle Width="20px" />
        </RowIndicatorColumn>
        <Columns>
            <radG:GridBoundColumn DataField="LastName" HeaderText="Last Name" UniqueName="LName" />
            <radG:GridBoundColumn DataField="FirstName" HeaderText="First Name" UniqueName="FName" />
            <radg:GridTemplateColumn HeaderText="Phone" UniqueName="HomePhone">
                <ItemTemplate>
                    <%#(Eval("Phone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Phone")))%>
                </ItemTemplate>
            </radg:GridTemplateColumn>
            <radG:GridTemplateColumn HeaderText="Fax" UniqueName="HomePhone">
                <ItemTemplate>
                    <%#(Eval("Fax") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Fax")))%>
                </ItemTemplate>
            </radG:GridTemplateColumn>
            <radG:GridTemplateColumn HeaderText="CellPhone" UniqueName="HomePhone">
                <ItemTemplate>
                    <%#(Eval("CellPhone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("CellPhone")))%>
                </ItemTemplate>
            </radG:GridTemplateColumn>
            <radG:GridTemplateColumn HeaderText="Pager" UniqueName="HomePhone">
                <ItemTemplate>
                    <%#(Eval("Pager") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Pager")))%><RADG:GRIDBOUNDCOLUMN 
UniqueName="Institution" HeaderText="Institution" DataField="InstitutionID" 
/><RADG:GRIDBOUNDCOLUMN UniqueName="Speciality" HeaderText="Speciality" 
DataField="Speciality" /><RADG:GRIDBOUNDCOLUMN UniqueName="WebSite" 
HeaderText="WebSite" DataField="WebSite" /><RADG:GRIDBOUNDCOLUMN 
UniqueName="Email" HeaderText="Email" DataField="Email" /><AJAXLOADINGTEMPLATE 
/><asp:Image id="Image1" ImageUrl="~/RadControls/Ajax/Skins/Default/Loading.gif" AlternateText="Loading..." runat="server"></asp:Image> 
--%&gt; <%--<asp:GridView ID="grdProviderDetails" runat="server" AutoGenerateColumns="False"
    GridLines="None" Width="100%">
    <Columns>
        <asp:BoundField DataField="FirstName" HeaderText="First Name" />
        <asp:BoundField DataField="LastName" HeaderText="Last Name" />
        <asp:BoundField DataField="DoctorID" HeaderText="ID" ReadOnly="True" />
        <asp:TemplateField HeaderText="Business Ph" HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
                <%#(Eval("Phone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Phone")))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Mobile Ph" HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
                <%#(Eval("CellPhone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("CellPhone")))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Fax No" HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
                <%#(Eval("Fax") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Fax")))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Pager No" HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
                <%#(Eval("Pager") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Pager")))%><ASP:BOUNDFIELD 
HeaderText="Institution" DataField="InstitutionID" /><ASP:BOUNDFIELD 
HeaderText="Discipline" DataField="Discipline" 
ItemStyle-HorizontalAlign="Center" /><ASP:BOUNDFIELD HeaderText="Speciality" 
DataField="Speciality" /><ASP:BOUNDFIELD HeaderText="Website" 
DataField="WebSite" /><ASP:BOUNDFIELD HeaderText="Email" DataField="Email" 
/><HEADERSTYLE HorizontalAlign="Left" Font-Underline="True" Font-Size="11px" 
Font-Names="Tahoma" /><ROWSTYLE Font-Size="11px" 
Font-Names="Tahoma,Verdana,Arial" />--%&gt; <DIV id="code" runat="server" 
__designer:dtid="1970324836974592" visible="false"><asp:GridView id="GridView1" runat="server" ForeColor="#333333" CellPadding="4" GridLines="None" __designer:dtid="1970324836974593">
<FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></FooterStyle>

<RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>

<PagerStyle HorizontalAlign="Center" BackColor="#284775" ForeColor="White"></PagerStyle>

<SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

<HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>

<EditRowStyle BackColor="#999999"></EditRowStyle>

<AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
</asp:GridView> </DIV><asp:DetailsView id="detailsProviderDetails" runat="server" ForeColor="#333333" CellPadding="4" GridLines="None" AutoGenerateRows="False" __designer:dtid="1970324836974594">
    <Fields __designer:dtid="1970324836974595">
        <asp:BoundField __designer:dtid="1970324836974596" DataField="FirstName" HeaderText="First Name:" ></asp:BoundField>
        <asp:BoundField __designer:dtid="1970324836974597" DataField="LastName" HeaderText="Last Name:" ></asp:BoundField>
        <%--<asp:BoundField DataField="DoctorID" HeaderText="ID" ReadOnly="True" />--%>
        <%--<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>

<radg:radgrid id="RadGrid1" runat="server" allowpaging="True" enableajax="True" gridlines="None" EnableAJAXLoadingTemplate="true"
    pagesize="8" skin="Office2007" UseEmbeddedScripts="false" OnNeedDataSource="RadGrid1_NeedDataSource">
    <MasterTableView AutoGenerateColumns="false">
        <ExpandCollapseColumn Visible="False">
            <HeaderStyle Width="19px" />
        </ExpandCollapseColumn>
        <RowIndicatorColumn Visible="False">
            <HeaderStyle Width="20px" />
        </RowIndicatorColumn>
        <Columns>
            <radG:GridBoundColumn DataField="LastName" HeaderText="Last Name" UniqueName="LName" />
            <radG:GridBoundColumn DataField="FirstName" HeaderText="First Name" UniqueName="FName" />
            <radg:GridTemplateColumn HeaderText="Phone" UniqueName="HomePhone">
                <ItemTemplate>
                    <%#(Eval("Phone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Phone")))%>
                </ItemTemplate>
            </radg:GridTemplateColumn>
            <radG:GridTemplateColumn HeaderText="Fax" UniqueName="HomePhone">
                <ItemTemplate>
                    <%#(Eval("Fax") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Fax")))%>
                </ItemTemplate>
            </radG:GridTemplateColumn>
            <radG:GridTemplateColumn HeaderText="CellPhone" UniqueName="HomePhone">
                <ItemTemplate>
                    <%#(Eval("CellPhone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("CellPhone")))%>
                </ItemTemplate>
            </radG:GridTemplateColumn>
            <radG:GridTemplateColumn HeaderText="Pager" UniqueName="HomePhone">
                <ItemTemplate>
                    <%#(Eval("Pager") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Pager")))%><RADG:GRIDBOUNDCOLUMN 
UniqueName="Institution" HeaderText="Institution" DataField="InstitutionID" 
/><RADG:GRIDBOUNDCOLUMN UniqueName="Speciality" HeaderText="Speciality" 
DataField="Speciality" /><RADG:GRIDBOUNDCOLUMN UniqueName="WebSite" 
HeaderText="WebSite" DataField="WebSite" /><RADG:GRIDBOUNDCOLUMN 
UniqueName="Email" HeaderText="Email" DataField="Email" /><AJAXLOADINGTEMPLATE 
/><asp:Image id="Image1" ImageUrl="~/RadControls/Ajax/Skins/Default/Loading.gif" AlternateText="Loading..." runat="server"></asp:Image> 
--%&gt; <%--<asp:GridView ID="grdProviderDetails" runat="server" AutoGenerateColumns="False"
    GridLines="None" Width="100%">
    <Columns>
        <asp:BoundField DataField="FirstName" HeaderText="First Name" />
        <asp:BoundField DataField="LastName" HeaderText="Last Name" />
        <asp:BoundField DataField="DoctorID" HeaderText="ID" ReadOnly="True" />
        <asp:TemplateField HeaderText="Business Ph" HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
                <%#(Eval("Phone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Phone")))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Mobile Ph" HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
                <%#(Eval("CellPhone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("CellPhone")))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Fax No" HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
                <%#(Eval("Fax") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Fax")))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Pager No" HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
                <%#(Eval("Pager") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Pager")))%><ASP:BOUNDFIELD 
HeaderText="Institution" DataField="InstitutionID" /><ASP:BOUNDFIELD 
HeaderText="Discipline" DataField="Discipline" 
ItemStyle-HorizontalAlign="Center" /><ASP:BOUNDFIELD HeaderText="Speciality" 
DataField="Speciality" /><ASP:BOUNDFIELD HeaderText="Website" 
DataField="WebSite" /><ASP:BOUNDFIELD HeaderText="Email" DataField="Email" 
/><HEADERSTYLE HorizontalAlign="Left" Font-Underline="True" Font-Size="11px" 
Font-Names="Tahoma" /><ROWSTYLE Font-Size="11px" 
Font-Names="Tahoma,Verdana,Arial" />--%&gt; <DIV id="code" runat="server" 
__designer:dtid="1970324836974592" visible="false"><asp:GridView id="GridView1" runat="server" ForeColor="#333333" CellPadding="4" GridLines="None" __designer:dtid="1970324836974593">
<FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></FooterStyle>

<RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>

<PagerStyle HorizontalAlign="Center" BackColor="#284775" ForeColor="White"></PagerStyle>

<SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

<HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>

<EditRowStyle BackColor="#999999"></EditRowStyle>

<AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
</asp:GridView> </DIV><asp:DetailsView id="detailsProviderDetails" runat="server" ForeColor="#333333" CellPadding="4" GridLines="None" AutoGenerateRows="False" __designer:dtid="1970324836974594">
    <Fields __designer:dtid="1970324836974595">
        <asp:BoundField __designer:dtid="1970324836974596" DataField="FirstName" HeaderText="First Name:" ></asp:BoundField>
        <asp:BoundField __designer:dtid="1970324836974597" DataField="LastName" HeaderText="Last Name:" ></asp:BoundField>
        <%--<asp:BoundField DataField="DoctorID" HeaderText="ID" ReadOnly="True" />--%>
        
        <asp:TemplateField HeaderText="Business Phone:" HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
                <%#(Eval("Phone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Phone")))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Mobile Phone:" HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
                <%#(Eval("CellPhone") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("CellPhone")))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Fax Number:" HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
                <%#(Eval("Fax") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Fax")))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Pager Number:" HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
                <%#(Eval("Pager") == DBNull.Value) ? "N/A" : String.Format("{0:(###) ###-####}", Double.Parse((string)Eval("Pager")))%>
            </ItemTemplate>
        </asp:TemplateField>
        <%--<asp:BoundField HeaderText="Healthcare Facility ID" />--%>
        <%--<asp:TemplateField HeaderText="Health Care Facilities:">
            <ItemTemplate>
                <asp:GridView ID="grdHFs" ShowHeader="false" BackColor="#F7F6F3" BorderColor="#F7F6F3"
                    OnRowDataBound="grdHFs_RowDataBound" BorderWidth="1px" CellPadding="4" runat="server"
                    AutoGenerateColumns="false">
                    <Columns>
                       <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkHF" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="InstitutionName" /> 
                    </Columns>
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#003399" />
                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                </asp:GridView>
            </ItemTemplate>
        </asp:TemplateField>--%>
        <%--<asp:BoundField DataField="Discipline" HeaderText="Discipline" ItemStyle-HorizontalAlign="Center" />--%>
        <asp:BoundField DataField="Speciality" HeaderText="Specialty:" />
        <asp:BoundField DataField="WebSite" HeaderText="Website:" />
        <asp:BoundField DataField="Email" HeaderText="Email:" />
        <asp:BoundField DataField="Address" HeaderText="Address:" />
        <asp:BoundField DataField="CityTown" HeaderText="City/Town:" />
        <asp:BoundField DataField="County" HeaderText="County:" />
        <asp:BoundField DataField="State" HeaderText="State:" />
        <asp:BoundField DataField="Zip" HeaderText="Zip Code:" />
        <asp:BoundField DataField="Country" HeaderText="Country:" />
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
