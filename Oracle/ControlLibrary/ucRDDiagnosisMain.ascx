<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRDDiagnosisMain.ascx.cs"
    Inherits="Oracle_ControlLibrary_ucRDDiagnosisMain" %>
<%--<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>--%>
<%--<radG:RadGrid ID="RadGridRDDiagnosisMain" runat="server" AllowPaging="false" EnableAJAX="false"
    GridLines="None" OnItemCreated="RadGridRDDiagnosisMain_ItemCreated" EnableAJAXLoadingTemplate="true"
    Skin="Office2007" OnNeedDataSource="RadGridRDDiagnosisMain_NeedDataSource" UseEmbeddedScripts="false">
    <MasterTableView AutoGenerateColumns="false" DataKeyNames="DETAIL,MedCode">
        <ExpandCollapseColumn Visible="False">
            <HeaderStyle Width="19px" />
        </ExpandCollapseColumn>
        <RowIndicatorColumn Visible="False">
            <HeaderStyle Width="20px" />
        </RowIndicatorColumn>
        <Columns>
            <radG:GridTemplateColumn Display="true" HeaderText="Medical Content Index" UniqueName="TemplateEditColumn">
                <ItemTemplate>
                    <asp:HyperLink ID="Details" runat="server" Text="Edit"></asp:HyperLink>
                </ItemTemplate>
            </radG:GridTemplateColumn>
            <radG:GridBoundColumn DataField="FIRSTDATE" HeaderText="First Date" UniqueName="FDate">
            </radG:GridBoundColumn>
            <radG:GridBoundColumn DataField="LASTDATE" HeaderText="Last Date" UniqueName="LDate">
            </radG:GridBoundColumn>
            <radG:GridBoundColumn DataField="FOUND" HeaderText="F" UniqueName="Found">
            </radG:GridBoundColumn>
        </Columns>
        <NoRecordsTemplate>
            <asp:Literal ID="NoTextDiagMain" runat="server" Text="<b>No Record to Display</b>"></asp:Literal>
        </NoRecordsTemplate>
    </MasterTableView>   
    <AJAXLoadingTemplate>
        <asp:Image ID="Image1" runat="server" AlternateText="Loading..." ImageUrl="~/RadControls/Ajax/Skins/Default/Loading.gif" />
    </AJAXLoadingTemplate>
</radG:RadGrid>--%>
<style type="text/css">
.tooltip
{
    color:blue;
    text-decoration:underline;
    cursor:hand;
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
</style>
<style type="text/css" media="print">
    #showAllRelatedDisipline
    {        
        display:none;
    }
    
</style>
<asp:Panel ID="pnlDesc" Visible="false" GroupingText="Description" ForeColor="Black"
    runat="server" Width="100%" Height="50%" Font-Names="Tahoma,Arial" Font-Size="8pt"
    Font-Bold="true">
    <asp:Label ID="lblDesc" runat="server" Text="" ForeColor="Green" Font-Names="Tahoma,Arial"
        Font-Size="8pt" Font-Bold="false">
        Please point the mouse over a Medical Content Index link to see the full description...
    </asp:Label>
</asp:Panel>
<asp:Panel ID="pnlRelatedDisc" GroupingText="Related Disciplines" ForeColor="Black"
    runat="server" Width="100%" Height="50%" Font-Names="Tahoma,Arial" Font-Size="8pt"
    Font-Bold="true">
    <table>
        <tr>
            <td>
                <div id="otherRemarkableDisipline">
                </div>
            </td>
            <td>
                <div id="showAllRelatedDisipline">
                </div>
            </td>
        </tr>
    </table>
</asp:Panel>
<br />
<asp:GridView ID="RadGridRDDiagnosisMain" runat="server" AutoGenerateColumns="False"
    AllowSorting="true" OnRowCreated="RadGridRDDiagnosisMain_RowCreated" Width="100%"
    GridLines="Both" OnSorting="RadGridRDDiagnosisMain_Sorting">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="lblExclamation" runat="server" ForeColor="Red" Font-Names="Tahoma"
                    Font-Size="9pt" Font-Bold="true" />
            </ItemTemplate>
            <HeaderStyle Font-Underline="false" />
            <ItemStyle HorizontalAlign="center" BackColor="white" />
        </asp:TemplateField>
        <asp:BoundField DataField="CodeType" SortExpression="CodeType" HeaderText="Code Type">
            <HeaderStyle HorizontalAlign ="Left" Width="100px" />
            <ItemStyle HorizontalAlign="Left" Width="100px"/>
        </asp:BoundField>
        <asp:TemplateField SortExpression="DETAIL" HeaderText="Medical Content Index">
            <ItemTemplate>
                <asp:HyperLink ID="Details" runat="server" />
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" Width="70%" />
        </asp:TemplateField>
        <asp:BoundField DataField="FirstDate" SortExpression="FirstDate" HeaderText="First Date"
            ControlStyle-Width="25px">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="LastDate" SortExpression="LastDate" HeaderText="Last Date"
            ControlStyle-Width="25px">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Found" SortExpression="Found" HeaderText="F">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Right" />
            <FooterStyle BackColor="PowderBlue" />
        </asp:BoundField>
    </Columns>
    <HeaderStyle Font-Names="Tahoma" Font-Size="11px" Font-Underline="True" ForeColor="black" HorizontalAlign="Center" />
    <RowStyle Font-Names="Tahoma,Arial" Font-Size="11px" />
    <SelectedRowStyle ForeColor="Yellow" />
    <AlternatingRowStyle Font-Names="Tahoma, Arial" Font-Size="11px" />
</asp:GridView>

<script src="../jquery.js" type="text/javascript"></script>

<script src="../tooltip.js" type="text/javascript"></script>

<script type="text/javascript">
//            function ShowRemarkableDisciplineDrillDown(id,modifierID,codeType,medCode,codeVersion,disCode,disType)
//            {window.radopen("RDDetails.aspx?ID="+id+"&ModifierID="+modifierID+"&CodeType="+codeType+"&medCode="+medCode+"&codeVersion="+codeVersion+"&disCode="+disCode +"&disType="+disType, null);return false;}
//              function ShowRemarkableDisciplineDrillDown(id,modifierID,medCode,disType)
//            {window.radopen("RDDetails.aspx?ID="+id+"&ModifierID="+modifierID+"&medCode="+medCode+"&disType="+disType, null);return false;}
                function ShowDescription(desc,text,showAllhtml,linkID,relatedDiscList)
                {
                   var lblDesc = document.getElementById('<%= lblDesc.ClientID %>');
                    //if(lblDesc)
                    //{
                        //lblDesc.innerHTML = desc;
                        ShowOtherRemarkableDesciplene(text);
                        PrepareRelatedDisciplineLink(showAllhtml);
                    //}
                    
                    ChangeStyle(linkID);
                    AddToList(relatedDiscList);
                  }
                   function AddToList(relatedDiscList)
                   {
                        var rds=relatedDiscList.split(',');
		                for (var i = 0; i < rds.length; i++) 
		                {
			                if (rds[i].length > 0) 
			                {
				                AddDiscipline(rds[i]);
				                Decorate(rds[i]);
			                }
		                }
                   }
                  
                  function AddToVisitedList(relatedDiscList)
                  {
                        var rds=relatedDiscList.split(',');
		                for (var i = 0; i < rds.length; i++) 
		                {
			                if (rds[i].length > 0) 
			                {
				                RemoveDiscipline(rds[i]);
			                }
		                }
                  }
                  
                  var previousSelectedLink;
                  function ChangeStyle(linkID)
                  {
                    if(previousSelectedLink)
                    {
                      // restore previous selected link style
                      var prevLink= document.getElementById(previousSelectedLink);
                      if(prevLink)
                      {
                        prevLink.style.fontWeight='Normal';
                      }
                    }
                    //apply style to current link
                    var curLink=document.getElementById(linkID);
                    if(curLink)
                    {
                        curLink.style.fontWeight='Bold';
                    }
                  
                    previousSelectedLink=linkID;
                  }
              function ShowOtherRemarkableDesciplene(text)
              {
                   var otherRMDiscDiv=document.getElementById('otherRemarkableDisipline');
                   if(otherRMDiscDiv)
                    {                        
                        if(text!='')
                        {                            
                            //var lnk='<a href='+text+' onclick=alert()>text</a>'
                            otherRMDiscDiv.innerHTML=text;
                        }
                        else
                        {
                             otherRMDiscDiv.innerHTML="";
                        }
                    }
                    
              }
              function PrepareRelatedDisciplineLink(text)
              {
                 var lnkAllRelDis = document.getElementById('showAllRelatedDisipline');
                 lnkAllRelDis.innerHTML=text;
              }
              function ShowAllRelatedDiscipline(url)
              {
                    var urlStr= GetRadWindow().BrowserWindow.location.href;
                    if(urlStr.indexOf("Result.aspx")<0)
                    {
                        GetRadWindow().BrowserWindow.MaximizeParentWindow();
                    }
               
                    var oWnd = GetRadWindow();
                    oWnd.Maximize();
                    var allDisWindow= window.radopen(url,null);
                    allDisWindow.OnClientClose = function()
                    {
                        //alert(GetRadWindow().GetUrl());
                    
                        if(urlStr.indexOf("Result.aspx")<0)
                        {
                            GetRadWindow().BrowserWindow.RestoreParentWindow();
                        }
                        else
                        {
                            if(oWnd.GetUrl().indexOf("PrintPreview.aspx")<0)
                            {
                                oWnd.Restore();
                            }
                            else
                            {
                                oWnd.Maximize();
                            }
                        }
                     }
              }
              function MaximizeParentWindow()
              {
                    var urlStr= GetRadWindow().BrowserWindow.location.href;
                    if(urlStr.indexOf("Result.aspx")<0)
                    {
                        GetRadWindow().BrowserWindow.MaximizeParentWindow();
                    }
                    GetRadWindow().Maximize();
              }
              function RestoreParentWindow()
              {
                    var urlStr= GetRadWindow().BrowserWindow.location.href;
                    if(urlStr.indexOf("Result.aspx")<0)
                    {
                        GetRadWindow().BrowserWindow.RestoreParentWindow();
                    }
                    else
                    {
                        if(GetRadWindow().GetUrl().indexOf("PrintPreview.aspx")<0)
                        {
                            GetRadWindow().Restore();
                        }
                        else
                        {
                            GetRadWindow().Maximize();
                        }
                    }
              }
</script>

