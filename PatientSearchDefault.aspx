<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PatientSearchDefault.aspx.cs"
    Inherits="PatientSearchDefault" %>

<%@ Register Src="~/Oracle/ControlLibrary/ucSearchGrid.ascx" TagName="crtlSearchGrid"
    TagPrefix="uc1" %>
<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>
<%@ Register Assembly="RadDock.Net2" Namespace="Telerik.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="RadDock.Net2" Namespace="Telerik.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<%@ Register Assembly="RadPanelbar.Net2" Namespace="Telerik.WebControls" TagPrefix="radP" %>
<%@ Register Assembly="RadSplitter.Net2" Namespace="Telerik.WebControls" TagPrefix="radspl" %>
<%@ Register Assembly="RadTabStrip.Net2" Namespace="Telerik.WebControls" TagPrefix="radTS" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/tr/xhtml11/dtd/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" style="height: 100%; margion: 0px">
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Patient Search Screen</title>
    <%--<link href="../App_Themes/Default/Client.css" rel="stylesheet" type="text/css" />
    <link href="RadControls/Ajax/Scripts/1_6_2/RadAjaxNamespace.js" type="text/javascript" />
    <link href="RadControls/Ajax/Scripts/1_6_2/RadAjax.js" type="text/javascript" />
    <link href="RadControls/Grid/Scripts/4_5_2/RadAjaxNamespace.js" type="text/javascript" />
    <link href="RadControls/Grid/Scripts/4_5_2/RadGrid.js" type="text/javascript" />--%>

    <script type="text/javascript" src="jquery.js"></script>

    <script type="text/javascript" src="ui.datepicker.js"></script>
   
    <style type="text/css">
                        /* Main Style Sheet for jQuery UI date picker */
            #ui-datepicker-div, .ui-datepicker-inline {
	            font-family: Arial, Helvetica, sans-serif;
	            font-size: 14px;
	            padding: 0;
	            margin: 0;
	            background: aliceblue;
	            width: 185px;
            }
            #ui-datepicker-div {
	            display: none;
	            border: 1px solid #777;
	            z-index: 100; /*must have*/
            }
            .ui-datepicker-inline {
	            float: left;
	            display: block;
	            border: 0;
            }
            .ui-datepicker-rtl {
	            direction: rtl;
            }
            .ui-datepicker-dialog {
	            padding: 5px !important;
	            border: 4px ridge aliceblue !important;
            }
            .ui-datepicker-disabled {
	            position: absolute;
	            z-index: 100;
	            background-color: white;
	            opacity: 0.5;
            }
            button.ui-datepicker-trigger {
	            width: 25px;
            }
            img.ui-datepicker-trigger {
	            margin: 2px;
	            vertical-align: middle;
            }
            .ui-datepicker-prompt {
	            float: left;
	            padding: 2px;
	            background: aliceblue;
	            color: #000;
            }
            * html .ui-datepicker-prompt {
	            width: 185px;
            }
            .ui-datepicker-control, .ui-datepicker-links, .ui-datepicker-header, .ui-datepicker {
	            clear: both;
	            float: left;
	            width: 100%;
	            color: #fff;
            }
            .ui-datepicker-control {
	            background: LightSteelBlue;
	            padding: 2px 0px;
            }
            .ui-datepicker-links {
	            background: #6688bd;
	            padding: 2px 0px;
            }
            .ui-datepicker-control, .ui-datepicker-links {
	            font-weight: bold;
	            font-size: 80%;
            }
            .ui-datepicker-links label { /* disabled links */
	            padding: 2px 5px;
	            color: #888;
            }
            .ui-datepicker-clear, .ui-datepicker-prev {
	            float: left;
	            width: 34%;
            }
            .ui-datepicker-rtl .ui-datepicker-clear, .ui-datepicker-rtl .ui-datepicker-prev {
	            float: right;
	            text-align: right;
            }
            .ui-datepicker-current {
	            float: left;
	            width: 30%;
	            text-align: center;
            }
            .ui-datepicker-close, .ui-datepicker-next {
	            float: right;
	            width: 34%;
	            text-align: right;
            }
            .ui-datepicker-rtl .ui-datepicker-close, .ui-datepicker-rtl .ui-datepicker-next {
	            float: left;
	            text-align: left;
            }
            .ui-datepicker-header {
	            padding: 1px 0 3px;
	            background: #6699cc;
	            text-align: center;
	            font-weight: bold;
	            height: 1.3em;
            }
            .ui-datepicker-header select {
	            background: #6699cc;
	            color: #fff;
	            border: 0px;
	            font-weight: bold;
            }
            .ui-datepicker {
	            background: #e6e8fb;
	            text-align: center;
	            font-size: 100%;
            }
            .ui-datepicker a {
	            display: block;
	            width: 100%;
            }
            .ui-datepicker-title-row {
	            background: #777;
            }
            .ui-datepicker-days-row {
	            background: #fff;
	            color: #666;
            }
            .ui-datepicker-week-col {
	            background: #777;
	            color: #fff;
            }
            .ui-datepicker-days-cell {
	            color: #000;
	            border: 1px solid aliceblue;
            }
            .ui-datepicker-days-cell a{
	            display: block;
            }
            .ui-datepicker-week-end-cell {
	            background:aliceblue;
            }
            .ui-datepicker-title-row .ui-datepicker-week-end-cell {
	            background: #777;
            }
            .ui-datepicker-days-cell-over {
	            background: #fff;
	            border: 1px solid #777;
            }
            .ui-datepicker-unselectable {
	            color: #888;
            }
            .ui-datepicker-today {
	            background:#6495ed !important;
            }
            .ui-datepicker-current-day {
	            background: #999 !important;
            }
            .ui-datepicker-status {
	            background: #ddd;
	            width: 100%;
	            font-size: 80%;
	            text-align: center;
            }

            /* ________ Datepicker Links _______

            ** Reset link properties and then override them with !important */
            #ui-datepicker-div a, .ui-datepicker-inline a {
	            cursor: pointer;
	            margin: 0;
	            padding: 0;
	            background: none;
	            color: #000;
            }
            .ui-datepicker-inline .ui-datepicker-links a {
	            padding: 0 5px !important;
            }
            .ui-datepicker-control a, .ui-datepicker-links a {
	            padding: 2px 5px !important;
	            color: #fff !important;
            }
            .ui-datepicker-title-row a {
	            color: #fff !important;
            }
            .ui-datepicker-control a:hover {
	            background: #fdd !important;
	            color: #333 !important;
            }
            .ui-datepicker-links a:hover, .ui-datepicker-title-row a:hover {
	            background: #ddd !important;
	            color: #333 !important;
            }

            /* ___________ MULTIPLE MONTHS _________*/

            .ui-datepicker-multi .ui-datepicker {
	            border: 1px solid #777;
            }
            .ui-datepicker-one-month {
	            float: left;
	            width: 185px;
            }
            .ui-datepicker-new-row {
	            clear: left;
            }

            /* ___________ IE6 IFRAME FIX ________ */

            .ui-datepicker-cover {
                display: none; /*sorry for IE5*/
                display/**/: block; /*sorry for IE5*/
                position: absolute; /*must have*/
                z-index: -1; /*must have*/
                filter: mask(); /*must have*/
                top: -4px; /*must have*/
                left: -4px; /*must have*/
                width: 200px; /*must have*/
                height: 200px; /*must have*/
            }
            /**************************************************************************/
            /*------------------------------------------------------------------------*/
            #divFirstName
            {
                background-color: #F6FAFB;
                position: absolute;
                left:308px;
                top: 39px;
                width: 300px;
                z-index: 1000;
                border:solid 1px #D3DBE9;
            }
            #divLastName
            {
                background-color: #F6FAFB;
                position: absolute;
                left: 477px;
                top: 39px;
                width: 300px;
                z-index: 1000;
                border:solid 1px #D3DBE9;
            }
            .dataStyle
            {
                font-family: Tahoma;
                font-size: 8pt;
                margin: 0;
                color: black;
            }
            .dataStyle li
            {
                margin: 1px;
                padding: 5px;
                list-style-type: none;
                background-color: #EEF3F7;
                cursor: default;
                clear: both;
            }
            .dataStyle li:hover
            {
                background-color: #D3DBE9;
            }
            .dataStyle li span
            {
                display:block;
                margin: 0px;
                padding: 0px;
                float: left;
            }
            .dataStyle li img
            {
                width:16px;
                height:16px;
                display:block;
                margin: 0px;
                padding: 0px;
                float: right;
                visibility: hidden;
            }
            .dataStyle li img:hover
            {
                cursor: hand;
            }
            .pagerStyle
            {
                font-family: Tahoma;
                font-size: 7pt;
                padding: 0;
                margin: 5px;
                color: blck;
                white-space: nowrap;
            }
            .pagerStyle li
            {
                margin: 1px;
                padding: 3px;
                list-style-type: none;
                cursor:default;
                display: inline;
                color:#3B5A82;
            }
            .pagerStyle li a
            {
                padding-left:1px;
                color:#3B5A82;
                cursor: hand;
            }
            .pagerStyle li a:hover
            {
                background-color: #AFBED0;
            }
            .pagerStyle li span
            {
               padding-left:10px;
               cursor:default;
            }
            /*------------------------------------------------------------------------*/
     </style>
</head>
<body scroll="no" style="margin: 0px; height: 100%; overflow: hidden;">
    <form id="form1" runat="server" style="height: 100%;">
        <div style="height: 100%">
            <%--<cc1:RadDockingManager ID="RadDockingManager1" runat="server" Skin="Web20" UseEmbeddedScripts="false" />--%>
            <radspl:RadSplitter ID="RadSplitter1" runat="server" BorderStyle="None" FullScreenMode="true"
                Height="100%" Orientation="Horizontal" PanesBorderSize="0" ResizeWithBrowserWindow="true"
                UseEmbeddedScripts="false" Width="100%">
                <radspl:RadPane ID="s" runat="server" Height="12px" UseEmbeddedScripts="false">
                    <a href="javascript:ToggleCollapsePane('Radpane12')" style="float: right; vertical-align: top;
                        font: Tahoma; font-size: 10px; font-weight: bold; color: Black;">Click to Show/Hide
                        Search Screen</a>

                    <script type="text/javascript">    
                    function ToggleCollapsePane(paneID){var splitter=<%= RadSplitter1.ClientID%>;var pane=splitter.GetPaneById(paneID);if (!pane) return;if (pane.IsCollapsed()){pane.Expand();}else{pane.Collapse();}}       
                    </script>

                </radspl:RadPane>
                <radspl:RadPane ID="Radpane12" runat="server" Height="40px" Scrolling="None" UseEmbeddedScripts="false"
                    Width="100%">
                    <table>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="Label2" runat="server" Font-Names="Tahoma, Arial, Verdana" Font-Size="8pt"
                                    Text="ID"></asp:Label>
                            </td>
                            <td valign="top">
                                <asp:TextBox ID="txtID" runat="server" Font-Names="Tahoma, Arial, Verdana" Font-Size="8pt"
                                    Width="70px"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Invalid ID"
                                    Text="*" ControlToValidate="txtID" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                            </td>
                            <td valign="top">
                                <asp:Label ID="Label1" runat="server" Font-Names="Tahoma,Arial,Verdana" Font-Size="8pt"
                                    Text="Modifier" Width="45px"></asp:Label>
                            </td>
                            <td valign="top">
                                <asp:DropDownList ID="cmbModifier" runat="server" AppendDataBoundItems="true" Font-Names="Tahoma,Verdana"
                                    Font-Size="8pt" OnDataBound="cmbModifier_DataBound" Width="86px">
                                </asp:DropDownList>
                            </td>
                            <td valign="top">
                                <asp:Label ID="Label3" runat="server" Font-Names="Tahoma,Arial,Verdana" Font-Size="8pt"
                                    Text="First Name" Width="53px"></asp:Label>
                            </td>
                            <td valign="top">
                                <asp:TextBox ID="txtFirstName" runat="server" Font-Names="Tahoma, Arial, Verdana"
                                    Font-Size="8pt" Width="100px"></asp:TextBox>
                            </td>
                            <td valign="top">
                                <asp:Label ID="Label4" runat="server" Font-Names="Tahoma,Arial,Verdana" Font-Size="8pt"
                                    Text="Last Name" Width="54px"></asp:Label>
                            </td>
                            <td valign="top">
                                <asp:TextBox ID="txtLastName" runat="server" Font-Names="Tahoma, Arial, Verdana"
                                    Font-Size="8pt" Height="12px" Width="100px"></asp:TextBox>
                            </td>
                            <td valign="top">
                                <asp:Label ID="Label5" runat="server" Font-Names="Tahoma,Arial,Verdana" Font-Size="8pt"
                                    Text="DOB" Width="25px"></asp:Label>
                            </td>
                            <td valign="top">
                                <asp:TextBox ID="txtDOB" Width="70px" runat="server" Font-Names="Tahoma, Arial, Verdana"
                                    Font-Size="8pt"></asp:TextBox>
                                <asp:Label ID="lblInvalidDate" runat="server" Text="Invalid DOB" Visible="false"
                                    ForeColor="red"></asp:Label>
                            </td>
                            <td style="width: 20px;">
                            </td>
                            <td valign="top" style="font-size: 8pt; font-family: Tahoma;">
                                <asp:Button ID="Button1" runat="server" Font-Names="Tahoma,Verdana" Font-Size="8pt"
                                    OnClick="btnSearch_Click" Text="Search" Width="64px" />
                            </td>
                            <td valign="top" style="font-size: 8pt; font-family: Tahoma;">
                                <asp:Button ID="ButtonReset" runat="server" Font-Names="Tahoma,Verdana" Font-Size="8pt"
                                    OnClientClick="return ResetSearchCirteriaInputs()" Text="Clear" UseSubmitBehavior="false"
                                    Width="64px" />
                            </td>
                            <td style="width: 20px;">
                            </td>
                            <td valign="top" style="font-size: 8pt; font-family: Tahoma;">
                                <asp:Button ID="btnSignOut" runat="server" Text="Signout All" Font-Names="Tahoma"
                                    Font-Size="8pt" Width="64px" OnClick="btnSignOut_Click" />
                            </td>
                            <td valign="top" style="font-size: 8pt; font-family: Tahoma;">
                                <asp:Button ID="btnLogout" runat="server" Text="Logoff" Font-Names="Tahoma" Font-Size="8pt"
                                    OnClick="btnLogOut_Click" Width="64px" />
                            </td>
                        </tr>
                    </table>
                </radspl:RadPane>
                <radspl:RadPane ID="radPaneGridResult" runat="server" Height="600px" UseEmbeddedScripts="false"
                    Width="100%">
                    <uc1:crtlSearchGrid ID="ucSearchGrid" runat="server" />
                </radspl:RadPane>
            </radspl:RadSplitter>
            <radA:RadAjaxManager ID="RadAjaxManagerDefault" runat="server" DefaultLoadingPanelID="AjaxLoadingPanel1"
                OnAjaxRequest="RadAjaxManagerDefault_AjaxRequest" UseEmbeddedScripts="False">
            </radA:RadAjaxManager>

            <script type="text/javascript">
                             
                function CallBackFunction(OptionList)
                {   
                   var ajaxManager = <%= RadAjaxManagerDefault.ClientID %> 
                    ajaxManager.AjaxRequest(OptionList); 
                }
			    function HideStatus()
			    {   window.status = "Click to see the details of the patient..."; return true;}
			    function ResetSearchCirteriaInputs()
			    { txtDOB=document.getElementById("txtDOB");if(txtDOB)txtDOB.value="";  txtID=document.getElementById("txtID");if(txtID)txtID.value="";txtFirstName=document.getElementById("txtFirstName");if(txtFirstName)txtFirstName.value="";txtLastName=document.getElementById("txtLastName");if(txtLastName)txtLastName.value="";cmbModifier=document.getElementById("cmbModifier");if(cmbModifier){if(cmbModifier.selectedIndex>0)cmbModifier.selectedIndex=0;} return false;}
            </script>

            <radA:AjaxLoadingPanel ID="AjaxLoadingPanel1" runat="server" Height="75px" Transparency="10"
                Width="75px">
                <asp:Image ID="Image1" runat="server" AlternateText="Saving Your Preference..." BackColor="Transparent"
                    ImageUrl="~/RadControls/Ajax/Skins/Default/Loading.gif" />
            </radA:AjaxLoadingPanel>
        </div>
        <div id="divFirstName">
            <ul id="dataListFirstName">
            </ul>
            <ul id="pageIndexListFirstName">
            </ul>
        </div>
        <div id="divLastName">
            <ul id="dataListLastName">
            </ul>
            <ul id="pageIndexListLastName">
            </ul>
        </div>

        <script type="text/javascript">
            var TABKEY = 9;
            var textBoxFocus = false;
            var autoCompleteBoxFocus = false;
            var itemSelected = false;
            var resultData = new Array();
            var pageSize = 15;
            var currentPage = 1;
            var totalPage = 0;
            var query = "";
            
            var textBoxFocusLast = false;
            var autoCompleteBoxFocusLast = false;
            var itemSelectedLast = false;
            var resultDataLast = new Array();
            var pageSizeLast =15;
            var currentPageLast = 1;
            var totalPageLast = 0;
            var queryLast = "";
        

            $(document).ready(function() {
                InitializeAutoCompleteBox();
                BindEvents();
                
                InitializeAutoCompleteBoxLast();
                BindEventsLast();
                
                $("#<%= txtFirstName.ClientID %>").keyup(function(event) {
                    if (event.keyCode == 37 || event.keyCode == 38 || event.keyCode == 39 || event.keyCode == 40) {
                        //do nothing
                    }
                    else if( event.keyCode == TABKEY){
                        HideAutoCompleteBox();
                    }
                    else {
                        itemSelected = false;
                        var text = $("#<%= txtFirstName.ClientID %>").val();
                        if (text.length > 0) {
                            ShowAutoCompleteBox();
                            SetAutoCompleteQuery(text);
                        }
                        else {
                            HideAutoCompleteBox();
                        }
                    }
                });
                $("#txtFirstName").keydown(function(event)
                {
                  if (event.keyCode == 9 ) {
                        HideAutoCompleteBox();
                    }
                });
                
                $("#<%= txtLastName.ClientID %>").keyup(function(event) {
                    if (event.keyCode == 37 || event.keyCode == 38 || event.keyCode == 39 || event.keyCode == 40) {
                        //do nothing
                    }
                    else {
                        itemSelectedLast = false;
                        var text = $("#<%= txtLastName.ClientID %>").val();
                        if (text.length > 0) {
                            ShowAutoCompleteBoxLast();
                            SetAutoCompleteQueryLast(text);
                        }
                        else {
                            HideAutoCompleteBoxLast();
                        }
                    }
                });
                $("#txtLastName").keydown(function(event)
                {
                  if (event.keyCode == 9 ) {
                        HideAutoCompleteBoxLast();
                    }
                });
            });

            function InitializeAutoCompleteBox() {
                $("#divFirstName").css("visibility", "hidden");
                $("#pageIndexListFirstName").addClass("pagerStyle");
                $("#dataListFirstName").addClass("dataStyle");
            }
            function ShowAutoCompleteBox() {
                $("#divFirstName").css("visibility", "visible");
            }
            function HideAutoCompleteBox() {
                $("#divFirstName").css("visibility", "hidden");
            }
            function SetAutoCompleteQuery(q) {
                query = q;
                currentPage = 1;
                GetPageCount(query);
                GetRecordCount(query);
                GetResult(query);
            }
            function BindEvents() {
                $("#<%= txtFirstName.ClientID %>").blur(function() {
                    textBoxFocus = false;
                });
                $("#divFirstName").blur(function() {
                    autoCompleteBoxFocus = false;
                });
                $("#<%= txtFirstName.ClientID %>").focus(function() {
                    textBoxFocus = true;
                });
                $("#divFirstName").focus(function() {
                    autoCompleteBoxFocus = true;
                });
                $("#divFirstName").click(function() {
                    autoCompleteBoxFocus = true;
                });
                $(document).click(function() {
                    ShowOrHideAutoCompleteBox();
                });
            }
            function ShowOrHideAutoCompleteBox() {
                if (autoCompleteBoxFocus == false && textBoxFocus == false) {
                    HideAutoCompleteBox();
                }
                else if (itemSelected == true) {
                    HideAutoCompleteBox();
                }
                else {
                    var text = $("#<%= txtFirstName.ClientID %>").val();
                    if (text.length > 0) {
                        ShowAutoCompleteBox();
                    }
                }
            }
            function ShowGoIcon(imageId) {
                $("#" + imageId).css("visibility", "visible");
            }
            function HideGoIcon(imageId) {
                $("#" + imageId).css("visibility", "hidden");
            }

            function GetPageCount(query) {
                $.ajax({ url: "AutoCompleteHandler.ashx?pagesize="+pageSize+"&type=count&query=" + query+"&option=1" ,
                    success: function(msg) {
                        totalPage = msg;
                        PopulatePaging(totalPage);
                    }
                });
            }
             function GetRecordCount(query) {
                $.ajax({ url: "AutoCompleteHandler.ashx?type=recordcount&query=" + query+"&option=1" ,
                    success: function(msg) {
                        PopulateRecordCount(msg);
                    }
                });
            }
            function GetResult(query) {
                $.ajax({ url: "AutoCompleteHandler.ashx?pagesize="+pageSize+"&type=data&query=" + query + "&pageno=" + currentPage+ "&option=1" ,
                    success: function(msg) {
                        resultData = msg.split("|");
                        PopulateList();
                    }
                });
            }
            function PopulateList() {
                $("#dataListFirstName li").remove();
                for (var i = 0; i < resultData.length; i++) {
                    var arr= resultData[i].split(";");
                    var key=arr[0];
                    var firstName=arr[1];
                    var lastName=arr[2];
                    if(key!=null && key>0){
                    //do nothing
                    }
                    else {continue;}
                    $("#dataListFirstName").append("<li onMouseover=\"ShowGoIcon('img_" + i + "') \" onMouseout=\"HideGoIcon('img_" + i + "') \" onClick=\"SelectItem('" + firstName + "','" + lastName + "')\"><span>" + firstName + " " + lastName + " </span><img id='img_" + i + "' onClick=\"Go('" + key + "')\" src=\"bullet_go.png\" /></li>");
                }
            }
            function PopulatePaging(pageCount) {
                $("#pageIndexListFirstName li").remove();
                if( pageCount == 0) return;
                
                var start = currentPage - pageSize / 2; var end;
                if (start < 0) start = 0;

                if (currentPage - 1 < Math.ceil((9 / 2))) {
                    start = 1;
                    end = 9;
                }
                else if (pageCount - currentPage < Math.ceil(9 / 2)) {
                    start = pageCount - 8;
                    if (start < 1) start = 1;
                    end = pageCount;
                }
                else {
                    start = currentPage - Math.ceil(9 / 2) + 1;
                    end = currentPage + Math.ceil(9 / 2) - 1;
                }
                for (var i = start; i <= end; i++) {
                    var pageNo = i;

                    if (pageNo == currentPage) {
                        $("#pageIndexListFirstName").append("<li>" + pageNo + "</li>");
                    }
                    else {
                        $("#pageIndexListFirstName").append("<li><a href='#' onClick=\"ChangePage(" + pageNo + ")\">" + pageNo + "</a></li>");

                    }
                    if (pageNo >= pageCount) break;
                }
            }
            function PopulateRecordCount(recordcount)
            {
                  $("#pageIndexListFirstName").append("<li><span>Total patient(s) found: " + recordcount + "</span></li>");
            }
            function ChangePage(pageNo) {
                currentPage = pageNo;
                PopulatePaging(totalPage);
                GetRecordCount(query);
                GetResult(query);
            }
            function SelectItem(firstName, lastName) {
                itemSelected = true;
                ShowOrHideAutoCompleteBox();
                ///////////////////////////////////////
                $("#<%= txtFirstName.ClientID %>").val(firstName);
                if( lastName != null){
                    $("#<%= txtLastName.ClientID %>").val(lastName);
                }
                ////////////////////////////////////////
            }
            function Go(patientKey) {
                itemSelected = true;
                ShowOrHideAutoCompleteBox();
                ///////////////////////////////////////////////
                window.open("Oracle/Result.aspx?PatientKey="+patientKey,null);
                ///////////////////////////////////////////
            }
            
            
            
            
            function InitializeAutoCompleteBoxLast() {
                $("#divLastName").css("visibility", "hidden");
                $("#pageIndexListLastName").addClass("pagerStyle");
                $("#dataListLastName").addClass("dataStyle");
            }
            function ShowAutoCompleteBoxLast() {
                $("#divLastName").css("visibility", "visible");
            }
            function HideAutoCompleteBoxLast() {
                $("#divLastName").css("visibility", "hidden");
            }
            function SetAutoCompleteQueryLast(q) {
                queryLast = q;
                currentPageLast = 1;
                GetPageCountLast(queryLast);
                GetRecordCountLast(queryLast);
                GetResultLast(queryLast);
            }
            function BindEventsLast() {
                $("#<%= txtLastName.ClientID %>").blur(function() {
                    textBoxFocusLast = false;
                });
                $("#divLastName").blur(function() {
                    autoCompleteBoxFocusLast = false;
                });
                $("#<%= txtLastName.ClientID %>").focus(function() {
                    textBoxFocusLast = true;
                });
                $("#divLastName").focus(function() {
                    autoCompleteBoxFocusLast = true;
                });
                $("#divLastName").click(function() {
                    autoCompleteBoxFocusLast = true;
                });
                $(document).click(function() {
                    ShowOrHideAutoCompleteBoxLast();
                });
            }
            function ShowOrHideAutoCompleteBoxLast() {
                if (autoCompleteBoxFocusLast == false && textBoxFocusLast == false) {
                    HideAutoCompleteBoxLast();
                }
                else if (itemSelectedLast == true) {
                    HideAutoCompleteBoxLast();
                }
                else {
                    var text = $("#<%= txtLastName.ClientID %>").val();
                    if (text.length > 0) {
                        ShowAutoCompleteBoxLast();
                    }
                }
            }
            function ShowGoIconLast(imageId) {
                $("#" + imageId).css("visibility", "visible");
            }
            function HideGoIconLast(imageId) {
                $("#" + imageId).css("visibility", "hidden");
            }

            function GetPageCountLast(queryLast) {
                $.ajax({ url: "AutoCompleteHandler.ashx?pagesize="+pageSizeLast+"&type=count&query=" + queryLast+"&option=2" ,
                    success: function(msg) {
                        totalPageLast = msg;
                        PopulatePagingLast(totalPageLast);
                    }
                });
            }
            function GetRecordCountLast(queryLast)
            {
                $.ajax({ url: "AutoCompleteHandler.ashx?type=recordcount&query=" + queryLast+"&option=2" ,
                    success: function(msg) {
                        PopulateRecordCountLast(msg);
                    }
                });
            }
            function GetResultLast(queryLast) {
                $.ajax({ url: "AutoCompleteHandler.ashx?pagesize="+pageSizeLast+"&type=data&query=" + queryLast + "&pageno=" + currentPageLast+ "&option=2" ,
                    success: function(msg) {
                        resultDataLast = msg.split("|");
                        PopulateListLast();
                    }
                });
            }
            function PopulateListLast() {
                $("#dataListLastName li").remove();
                for (var i = 0; i < resultDataLast.length; i++) 
                {
                    var arr= resultDataLast[i].split(";");
                    var key=arr[0];
                    var firstName=arr[1];
                    var lastName=arr[2];
                    
                    if(key!=null && key>0){
                    //do nothing
                    }
                    else {continue;}
                    
                    $("#dataListLastName").append("<li onMouseover=\"ShowGoIconLast('img_last_" + i + "') \" onMouseout=\"HideGoIconLast('img_last_" + i + "') \" onClick=\"SelectItemLast('" + firstName + "','"+lastName+"')\"><span>" + firstName+" "+lastName + "</span> <img id='img_last_" + i + "' onClick=\"GoLast('" + key + "')\" src=\"bullet_go.png\" /></li>");
                }
            }
            function PopulatePagingLast(pageCount) {
                $("#pageIndexListLastName li").remove();
                if( pageCount == 0) return;
                
                var start = currentPageLast - pageSizeLast / 2; var end;
                if (start < 0) start = 0;

                if (currentPageLast - 1 < Math.ceil((9 / 2))) {
                    start = 1;
                    end = 9;
                }
                else if (pageCount - currentPageLast < Math.ceil(9 / 2)) {
                    start = pageCount - 8;
                    if (start < 1) start = 1;
                    end = pageCount;
                }
                else {
                    start = currentPageLast - Math.ceil(9 / 2) + 1;
                    end = currentPageLast + Math.ceil(9 / 2) - 1;
                }
                for (var i = start; i <= end; i++) {
                    var pageNo = i;

                    if (pageNo == currentPageLast) {
                        $("#pageIndexListLastName").append("<li>" + pageNo + "</li>");
                    }
                    else {
                        $("#pageIndexListLastName").append("<li><a href='#' onClick=\"ChangePageLast(" + pageNo + ")\">" + pageNo + "</a></li>");

                    }
                    if (pageNo >= pageCount) break;
                }
            }
            function PopulateRecordCountLast(recordcount)
            {
                  $("#pageIndexListLastName").append("<li><span>Total patient(s) found: " + recordcount + "</span></li>");
            }
            function ChangePageLast(pageNo) {
                currentPageLast = pageNo;
                PopulatePagingLast(totalPageLast);
                GetRecordCountLast(queryLast);
                GetResultLast(queryLast);
            }
            function SelectItemLast(firstName,lastName) {
                itemSelectedLast = true;
                ShowOrHideAutoCompleteBoxLast();
                ///////////////////////////////////////
                $("#<%= txtFirstName.ClientID %>").val(firstName);
                if( lastName != null){
                    $("#<%= txtLastName.ClientID %>").val(lastName);
                }
                ////////////////////////////////////////
            }
            function GoLast(patientKey) {
                itemSelectedLast = true;
                ShowOrHideAutoCompleteBoxLast();
                ///////////////////////////////////////////////
                window.open("Oracle/Result.aspx?PatientKey="+patientKey,null);
                ///////////////////////////////////////////
            }

            function openEmergencyTransferFormRequest(patientKey) {
                //var wnd = window.radopen("Oracle/EmergencyTransferFormRequest.aspx?PatientKey=" + patientKey, null); 
                // wnd.Maximize();
                window.open("Oracle/EmergencyTransferFormRequest.aspx?PatientKey=" + patientKey);
            }

            function openDoctorAlertNetwork(patientKey) {
                //var wnd = window.radopen("Oracle/EmergencyTransferFormRequest.aspx?PatientKey=" + patientKey, null); 
                // wnd.Maximize();
                //window.open("Oracle/DoctorAlertNetwork.aspx?PatientKey=" + patientKey);
                window.open("Oracle/NurseNotes.aspx?PatientKey=" + patientKey);
            }
        </script>

    </form>
    <radW:RadWindowManager ID="WMDefault" runat="server" Height="450px" Skin="Office2007"
        UseEmbeddedScripts="false" Width="600px">

    </radW:RadWindowManager>

    <script type="text/javascript">
        $("#<% =txtDOB.ClientID %>").datepicker();
    </script>

</body>
</html>
