<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NurseNotes.aspx.cs" Inherits="Oracle_NurseNotes" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Provider Alert Network </title>
    <link href="../Content/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
    <link href="../Content/themes/base/jquery.ui.datepicker.css" rel="stylesheet" type="text/css" />
    <link href="../Content/themes/base/jquery.ui.timepicker.css" rel="stylesheet" type="text/css" />

    <script src="../Scripts/jquery-1.5.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.5.1.min.js" type="text/javascript"></script>

    <script src="../Scripts/jquery-ui-1.8.11.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui-1.8.11.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.timepicker.js" type="text/javascript"></script>

    <%--<script src="../jquery.js" type="text/javascript"></script>
    <link href="../ui.datepicker.css" rel="stylesheet" type="text/css" />
    <script src="../ui.datepicker.js" type="text/javascript"></script>--%>
        <script type="text/javascript">

            $(document).ready(function () {
                $("#<% =txtDate.ClientID %>").datepicker();
                $("#<% =txtTime.ClientID %>").timepicker({ defaultTime: 'now' });
            });
        </script>
    <%--<script type="text/javascript">
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
        var pageSizeLast = 15;
        var currentPageLast = 1;
        var totalPageLast = 0;
        var queryLast = "";
        $(document).ready(function () {
            /*InitializeAutoCompleteBox();
            BindEvents();

            InitializeAutoCompleteBoxLast();
            BindEventsLast();
            $("#<%= txtFirstName.ClientID %>").keyup(function (event) {
                if (event.keyCode == 37 || event.keyCode == 38 || event.keyCode == 39 || event.keyCode == 40) {
                    //do nothing
                }
                else if (event.keyCode == TABKEY) {
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
            $("#txtFirstName").keydown(function (event) {
                if (event.keyCode == 9) {
                    HideAutoCompleteBox();
                }
            });

            $("#<%= txtLastName.ClientID %>").keyup(function (event) {
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
            $("#txtLastName").keydown(function (event) {
                if (event.keyCode == 9) {
                    HideAutoCompleteBoxLast();
                }
            });*/
            $("#<% =txtDate.ClientID %>").datepicker();
        });

        function Client_ValidateEmail(source, arguments) {
            var text = $("#<%= txtEmails.ClientID %>").val();
            if (text.length > 0) {
                var es = text.split(",");
                arguments.IsValid = true;
                for (i = 0; i < es.length; i++) {
                    var patt = /\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/g;
                    var result = patt.exec(es[i]);
                    if (result == null) {
                        arguments.IsValid = false;
                        break;
                    }
                }
            }
        }

        /*function InitializeAutoCompleteBox() {
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
            $("#<%= txtFirstName.ClientID %>").blur(function () {
                textBoxFocus = false;
            });
            $("#divFirstName").blur(function () {
                autoCompleteBoxFocus = false;
            });
            $("#<%= txtFirstName.ClientID %>").focus(function () {
                textBoxFocus = true;
            });
            $("#divFirstName").focus(function () {
                autoCompleteBoxFocus = true;
            });
            $("#divFirstName").click(function () {
                autoCompleteBoxFocus = true;
            });
            $(document).click(function () {
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
            $.ajax({ url: "AutoCompleteNurseHandler.ashx?pagesize=" + pageSize + "&type=count&query=" + query + "&option=1",
                success: function (msg) {
                    totalPage = msg;
                    PopulatePaging(totalPage);
                }
            });
        }
        function GetRecordCount(query) {
            $.ajax({ url: "AutoCompleteNurseHandler.ashx?type=recordcount&query=" + query + "&option=1",
                success: function (msg) {
                    PopulateRecordCount(msg);
                }
            });
        }
        function GetResult(query) {
            $.ajax({ url: "AutoCompleteNurseHandler.ashx?pagesize=" + pageSize + "&type=data&query=" + query + "&pageno=" + currentPage + "&option=1",
                success: function (msg) {
                    resultData = msg.split("|");
                    PopulateList();
                }
            });
        }
        function PopulateList() {
            $("#dataListFirstName li").remove();
            for (var i = 0; i < resultData.length; i++) {
                var arr = resultData[i].split(";");
                var key = arr[0];
                var firstName = arr[1];
                var lastName = arr[2];
                if (key != null && key.length > 0) {
                    //do nothing
                }
                else { continue; }
                $("#dataListFirstName").append("<li onMouseover=\"ShowGoIcon('img_" + i + "') \" onMouseout=\"HideGoIcon('img_" + i + "') \" onClick=\"SelectItem('" + firstName + "','" + lastName + "')\"><span>" + firstName + " " + lastName + " </span><img id='img_" + i + "' onClick=\"Go('" + key + "','" + firstName + "','" + lastName + "')\" src=\"bullet_go.png\" /></li>");
            }
        }
        function PopulatePaging(pageCount) {
            $("#pageIndexListFirstName li").remove();
            if (pageCount == 0) return;

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
        function PopulateRecordCount(recordcount) {
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
            if (lastName != null) {
                $("#<%= txtLastName.ClientID %>").val(lastName);
            }
            ////////////////////////////////////////
        }
        function Go(patientKey, patientFName, patientLName) {
            itemSelected = true;
            ShowOrHideAutoCompleteBox();
            $("#<%= txtNurseName.ClientID %>").val(patientFName + ' ' + patientLName);
            $("#<%= hnfNurseName.ClientID %>").val(patientKey);
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
            $("#<%= txtLastName.ClientID %>").blur(function () {
                textBoxFocusLast = false;
            });
            $("#divLastName").blur(function () {
                autoCompleteBoxFocusLast = false;
            });
            $("#<%= txtLastName.ClientID %>").focus(function () {
                textBoxFocusLast = true;
            });
            $("#divLastName").focus(function () {
                autoCompleteBoxFocusLast = true;
            });
            $("#divLastName").click(function () {
                autoCompleteBoxFocusLast = true;
            });
            $(document).click(function () {
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
            $.ajax({ url: "AutoCompleteNurseHandler.ashx?pagesize=" + pageSizeLast + "&type=count&query=" + queryLast + "&option=2",
                success: function (msg) {
                    totalPageLast = msg;
                    PopulatePagingLast(totalPageLast);
                }
            });
        }
        function GetRecordCountLast(queryLast) {
            $.ajax({ url: "AutoCompleteNurseHandler.ashx?type=recordcount&query=" + queryLast + "&option=2",
                success: function (msg) {
                    PopulateRecordCountLast(msg);
                }
            });
        }
        function GetResultLast(queryLast) {
            $.ajax({ url: "AutoCompleteNurseHandler.ashx?pagesize=" + pageSizeLast + "&type=data&query=" + queryLast + "&pageno=" + currentPageLast + "&option=2",
                success: function (msg) {
                    resultDataLast = msg.split("|");
                    PopulateListLast();
                }
            });
        }
        function PopulateListLast() {
            $("#dataListLastName li").remove();
            for (var i = 0; i < resultDataLast.length; i++) {
                var arr = resultDataLast[i].split(";");
                var key = arr[0];
                var firstName = arr[1];
                var lastName = arr[2];

                if (key != null && key.length > 0) {
                    //do nothing
                }
                else { continue; }

                $("#dataListLastName").append("<li onMouseover=\"ShowGoIconLast('img_last_" + i + "') \" onMouseout=\"HideGoIconLast('img_last_" + i + "') \" onClick=\"SelectItemLast('" + firstName + "','" + lastName + "')\"><span>" + firstName + " " + lastName + "</span> <img id='img_last_" + i + "' onClick=\"GoLast('" + key + "','" + firstName + "','" + lastName + "')\" src=\"bullet_go.png\" /></li>");
            }
        }
        function PopulatePagingLast(pageCount) {
            $("#pageIndexListLastName li").remove();
            if (pageCount == 0) return;

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
        function PopulateRecordCountLast(recordcount) {
            $("#pageIndexListLastName").append("<li><span>Total patient(s) found: " + recordcount + "</span></li>");
        }
        function ChangePageLast(pageNo) {
            currentPageLast = pageNo;
            PopulatePagingLast(totalPageLast);
            GetRecordCountLast(queryLast);
            GetResultLast(queryLast);
        }
        function SelectItemLast(firstName, lastName) {
            itemSelectedLast = true;
            ShowOrHideAutoCompleteBoxLast();
            ///////////////////////////////////////
            $("#<%= txtFirstName.ClientID %>").val(firstName);
            if (lastName != null) {
                $("#<%= txtLastName.ClientID %>").val(lastName);
            }
            ////////////////////////////////////////
        }
        function GoLast(patientKey, patientFName, patientLName) {
            itemSelectedLast = true;
            ShowOrHideAutoCompleteBoxLast();
            $("#<%= txtNurseName.ClientID %>").val(patientFName + ' ' + patientLName);
            $("#<%= hnfNurseName.ClientID %>").val(patientKey);
        }*/
    </script>--%>
    <style type="text/css">
        body
        {
            font-family: Tahoma;
            font-size: 8pt;
        }
        textarea
        {
            font-family: Tahoma;
            font-size: 8pt;
        }
        .caption
        {
            font-weight: bold;
            display: block;
            padding-bottom:2px;
        }
        
        /**************************************************************************/
        /*------------------------------------------------------------------------*/
        .numeric
        {
           text-align:right;
        }
        #divFirstName
        {
            background-color: #F6FAFB;
            position: absolute;
            left:80px;
            top: 145px;
            width: 300px;
            z-index: 1000;
            border:solid 1px #D3DBE9;
        }
        #divLastName
        {
            background-color: #F6FAFB;
            position: absolute;
            left: 250px;
            top: 145px;
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
            color: black;
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
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout ="360000">
 
    </asp:ScriptManager>
    <script type="text/javascript">
        function Client_ValidateEmail(source, arguments) {
            var text = $("#<%= txtEmails.ClientID %>").val();
            if (text.length > 0) {
                var es = text.split(",");
                arguments.IsValid = true;
                for (i = 0; i < es.length; i++) {
                    var patt = /\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/g;
                    var result = patt.exec(es[i]);
                    if (result == null) {
                        arguments.IsValid = false;
                        break;
                    }
                }
            }
        }
    </script>
    <table>
        <tr>
            <td align="left" valign="top" style="width:900px">
                <div id="dvVitalSigns" style="padding: 10px;margin:0px;">
                    
                    <h2><asp:Label ID="lblPatientName" runat="server"/></h2>
                    <br />
                    <table style="border-spacing:0px;border-width:0px;">
                        <tr>
                            <td>
                                <%--<h2>Vital Signs</h2>--%>
                                <asp:Panel ID="pnlNurse" runat="server" >
                                    <div style="margin:0px;">
                                         <h3>Nurse's S.O.A.P.</h3>
                                    </div>
                                    <div style="margin:0px;">
                                        <p style="margin:0px;">
                                            <span style="font-weight:bold;">Instruction:</span> Please select the nurse who will record her S.O.A.P. information in the following form. Once completed, please click on “Plan Request - Provider Alert Network” button at the bottom of the form to dispatch email to the primary provider and other (optional) recipients.
                                        </p>
                                    </div>
                                    <%--<br />
                                    <div>
                                        <span class="caption">Select Nurse</span>
                                        <table>
                                            <tr>
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
                                            </tr>
                                        </table>
                                    </div>--%>
                                    <br />
                                    <div style="margin:0px;">
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <div style="width: 400px;height:71px;">
                                                        <span class="caption">Selected Nurse</span>
                                                        <asp:DropDownList ID="ddlNurse" runat="server" Width="300px">
                                                        </asp:DropDownList>
                                                        <%--<asp:TextBox ID="txtNurseName" runat="server" Text="" 
                                                        Font-Names="Tahoma, Arial, Verdana" Font-Size="8pt" ReadOnly="true" 
                                                        Width="300px"></asp:TextBox>--%>
                                                        <asp:RequiredFieldValidator ID="rfvNurse" runat="server" Text="*" ErrorMessage="Please select a nurse."
                                                            ValidationGroup="vgNurse" ControlToValidate="ddlNurse"></asp:RequiredFieldValidator>
                                                        <asp:HiddenField ID="hnfNurseName" runat="server" />
                                                    </div>
                                                    <div>
                                                        <span class="caption">Subjective</span>
                                                        <asp:TextBox ID="txtNurseSubjective" runat="server" Height="50px" TextMode="MultiLine"
                                                            Width="300px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvSubject" runat="server" ErrorMessage="Subjective can not be empty." Text="*" 
                                                            ValidationGroup="vgNurse" ControlToValidate="txtNurseSubjective"></asp:RequiredFieldValidator>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlPhysician" runat="server" Visible="false">
                                    <div style="margin:0px;">
                                         <h3>Provider's S.O.A.P.</h3>
                                    </div>
                                    <div style="margin:0px;">
                                        <p style="margin:0px;">
                                            <span style="font-weight:bold;">Instruction:</span> Please enter the Provider’s Assessment and select a plan. Complete this step by clicking the “Save” button at the bottom of the screen.
                                        </p>
                                    </div>
                                    <br />
                                    <div style="margin:0px 0px 5px 0px;">
                                        <span class="caption">Provider Name</span>
                                        <asp:Label ID="lblPhysicianName" runat="server" ></asp:Label>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlETRF" runat="server" Visible="false">
                                    <div style="margin:0px;">
                                        <p style="margin:0px;">
                                            <span style="font-weight:bold;">Instruction:</span> All information is ready regarding the S.O.A.P. notes. Please alert the target hospital by selecting it from the form at your right. You can also select the next to kin or any other dependent party/parties by providing the email address(es) an click “Send Email”
                                        </p>
                                    </div>
                                    <br />
                                    <div style="margin:0px;">
                                        <span class="caption">Provider</span>
                                        <asp:Label ID="lblPhysicianNameETRF" runat="server" ></asp:Label>
                                    </div>
                                </asp:Panel>
                                <br />
                            </td>
                        </tr>
                    </table>
                    <table style="border-spacing:0px;border-width:0px;">
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width:500px">
                                            <asp:Panel ID="pnlVital" runat="server" BackColor="White">
                                            
								  			    <fieldset id="fldDiag" style="margin:5px 0px 0px 0px;width:375px">
												    <legend class="caption">Objective</legend>
                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td style="width:200px;">
                                                                    <h3 style="margin: 0px;">Vital Signs</h3>
                                                                </td>
                                                                <td>
                                                                    <div style="float: none;">
                                                                        <span class="caption">Date</span>
                                                                        <asp:TextBox ID="txtDate" runat="server" Width="100px"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvDate" runat="server" Text="*" ErrorMessage="Date can not be empty." 
                                                                                ValidationGroup="vgNurse" ControlToValidate="txtDate"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div style="float: none;">
                                                                        <span class="caption">Time</span>
                                                                        <asp:TextBox ID="txtTime" runat="server" Width="100px"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvTime" runat="server" Text="*" ErrorMessage="Time can not be empty." 
                                                                                ValidationGroup="vgNurse" ControlToValidate="txtTime"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <div style="margin: 0px 0px 5px 0px;">
                                                            <span class="caption">Primary Four</span>
                                                            <div style="padding-top: 5px;">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            Temperature
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtTemperature" runat="server" Width="40px"></asp:TextBox>
                                                                            <asp:DropDownList ID="ddlTemperatureUnit" runat="server">
                                                                                <asp:ListItem Text="°C" Value="C" />
                                                                                <asp:ListItem Text="°F" Value="F" />
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            Blood Pressure
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtBloodPressureSystolic" runat="server" Width="40px"></asp:TextBox>
                                                                            systolic
                                                                            <asp:TextBox ID="txtBloodPressureDiastolic" runat="server" Width="40px"></asp:TextBox>
                                                                            diastolic
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            Pulse
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtPulse" runat="server" Width="40px"></asp:TextBox>
                                                                            <span>beats per minute</span>
                                                                            <asp:RadioButton ID="rdoPulseRegular" Text="Regular" runat="server" GroupName="g1"
                                                                                Checked="true" />
                                                                            <asp:RadioButton ID="rdoPulseIrregular" Text="Irregular" runat="server" GroupName="g1" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            Respiratory Rate
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtRespiratoryRate" runat="server" Width="40px"></asp:TextBox>
                                                                            <span>breaths per minute</span>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            BMI
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td style="width:50px;">
                                                                                        <span>Weight </span>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtWeight" runat="server" Width="40px"></asp:TextBox>
                                                                                        <asp:DropDownList ID="ddlWeightUnit" runat="server">
                                                                                            <asp:ListItem Text="lbs" Value="lbs" />
                                                                                            <asp:ListItem Text="kg" Value="kg" />
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">

                                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td style="width:50px;">
                                                                                        <span>Height </span>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:UpdatePanel runat="server" ID="upnHeightUnit" UpdateMode="Conditional">
                                                                                            <ContentTemplate>
                                                                                                <asp:TextBox ID="txtHeight" runat="server" Width="40px"></asp:TextBox>
                                                                                                <asp:DropDownList ID="ddlHeightUnit" runat="server" AutoPostBack="true" 
                                                                                                    onselectedindexchanged="ddlHeightUnit_SelectedIndexChanged">
                                                                                                    <asp:ListItem Text="in" Value="in" />
                                                                                                    <asp:ListItem Text="cm" Value="cm" />
                                                                                                    <asp:ListItem Text="ft" Value="ft" />
                                                                                                </asp:DropDownList>
                                                                                                <asp:TextBox ID="txtHeightInch" runat="server" Width="40px" Visible="false"></asp:TextBox>
                                                                                                <asp:Label ID="lblHeightInch" runat="server" Text=" in" Visible="false"></asp:Label>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <span class="caption">Fifth Sign</span>
                                                            <div style="padding-top: 5px;">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            Pain Scale
                                                                        </td>
                                                                        <td>
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <span>Scale</span>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="ddlScale" runat="server">
                                                                                            <asp:ListItem Text="0" Value="0" />
                                                                                            <asp:ListItem Text="1" Value="1" />
                                                                                            <asp:ListItem Text="2" Value="2" />
                                                                                            <asp:ListItem Text="3" Value="3" />
                                                                                            <asp:ListItem Text="4" Value="4" />
                                                                                            <asp:ListItem Text="5" Value="5" />
                                                                                            <asp:ListItem Text="6" Value="6" />
                                                                                            <asp:ListItem Text="7" Value="7" />
                                                                                            <asp:ListItem Text="8" Value="8" />
                                                                                            <asp:ListItem Text="9" Value="9" />
                                                                                            <asp:ListItem Text="10" Value="10" />
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td>
                                                                                        <span>Location</span>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtLocation" runat="server" Width="100px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            Pupil Size
                                                                        </td>
                                                                        <td>
                                                                            (R)<asp:TextBox ID="txtPupilSizeRight" runat="server" Width="40"></asp:TextBox>mm
                                                                            &nbsp;&nbsp; (L)<asp:TextBox ID="txtPupilSizeLeft" runat="server" Width="40"></asp:TextBox>mm
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            Blood Glucose Level
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtBloodGlucoseLevel" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                </fieldset>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlVitalDoc" runat="server" Visible="false">
                                                <%--<div style="float: left; width: 400px">--%>
                                          		<fieldset id="Fieldset1" style="margin:5px 0px 0px 0px;width:375px">
												    <legend class="caption">Provider Subjective</legend>    
                                                    <div style="margin:5px 0px 5px 0px;">
                                                        <span class="caption">Nurse Name</span>
                                                        <asp:Label ID="lblNurseName" runat="server" Text=""></asp:Label>
                                                    </div>
                                                    <div style="margin:0px 0px 5px 0px;">
                                                        <span class="caption">Nurse Subjective</span>
                                                        <asp:Label ID="lblNurseSubjective" runat="server" Text=""></asp:Label>
                                                    </div>
                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="width:200px;">
                                                                <h3 style="margin: 0px;">Vital Signs</h3>
                                                            </td>
                                                            <td>
                                                                <div style="padding-top: 5px;">
                                                                    <span class="caption">Date</span>
                                                                    <asp:Label ID="lblDate" runat="server" Width="100px" CssClass="numeric"></asp:Label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <div style="margin: 0px;">
                                                        <span class="caption">Primary Four</span>
                                                        <div style="padding-top: 5px;">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        Temperature
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblTemperature" runat="server" Width="40px" CssClass="numeric"></asp:Label>
                                                                        <asp:Label ID="lblTemperatureUnit" runat="server" Width="40px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Blood Pressure
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblBloodPressureSystolic" runat="server" Width="40px" CssClass="numeric"></asp:Label>
                                                                        systolic
                                                                        <asp:Label ID="lblBloodPressureDiastolic" runat="server" Width="40px" CssClass="numeric"></asp:Label>
                                                                        diastolic
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Pulse
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblPulseRate" runat="server" Width="40px" CssClass="numeric"></asp:Label>
                                                                        <span>beats per minute</span>
                                                                        <asp:Label ID="lblPulse" runat="server" Width="40px" CssClass="numeric"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Respiratory Rate
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblRespiratoryRate" runat="server" Width="40px" CssClass="numeric"></asp:Label>
                                                                        <span>breaths per minute</span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        BMI
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <span>Weight</span>
                                                                        <asp:Label ID="lblWeight" runat="server" Width="40px" CssClass="numeric"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <span>Height</span>
                                                                        <asp:Label ID="lblHeight" runat="server" Width="40px" CssClass="numeric"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <span class="caption">Fifth Sign</span>
                                                        <div style="padding-top: 5px;">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        Pain Scale
                                                                    </td>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <span>Scale</span>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblPainScale" runat="server" Text="Label" CssClass="numeric"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <span>Location</span>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblLocation" runat="server" Width="100px" CssClass="numeric"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Pupil Size
                                                                    </td>
                                                                    <td>
                                                                        (R)<asp:Label ID="lblPupilSizeRight" runat="server" Width="40" CssClass="numeric"></asp:Label>mm
                                                                        &nbsp;&nbsp; (L)<asp:Label ID="lblPupilSizeLeft" runat="server" Width="40" CssClass="numeric"></asp:Label>mm
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Blood Glucose Level
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblBloodGlucoseLevel" runat="server" CssClass="numeric" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <br />
                                                        <div style="padding-top: 5px;">
                                                            <asp:CheckBox ID="chkPatientNotExaminedByProvider" runat="server" Text="Patient not examined by provider"/>
                                                        </div>
                                                    </div>
                                                </fieldset>
                                                <br />
                                                <span class="caption">Objective</span>
                                                <div style="padding-top: 5px;">
                                                    <asp:Label ID="lblNurseObjective" runat="server" Text=""></asp:Label>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="margin:0px;width:500px;border-width:0px;border-style:none;">
                                                <asp:Panel ID="pnlNurseNote" runat="server">
                                                    <div>
                                                        <span class="caption">Assessment</span>
                                                        <asp:TextBox ID="txtNurseAssessment" runat="server" Height="50px" TextMode="MultiLine"
                                                            Width="300px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvAssesent" runat="server" Text="*" ErrorMessage="Assessment can not be empty." 
                                                            ValidationGroup="vgNurse" ControlToValidate="txtNurseAssessment"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div>
                                                        <span class="caption">Plan</span>
                                                        <asp:TextBox ID="txtNurseNote" runat="server" Height="50px" TextMode="MultiLine"
                                                            Width="300px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvNurseNote" runat="server" Text="*" ErrorMessage="Notes can not be empty." 
                                                            ValidationGroup="vgNurse" ControlToValidate="txtNurseNote"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <br />
                                                    <div style="float:none;">
                                                        <br />
                                                        <br />
                                                        <asp:CheckBox ID="chkSenSMS" runat="server" Text="Send SMS to provider (optional paid service.)" />
                                                        <br />
                                                        <br />
                                                        <asp:Button ID="btnDAN" runat="server" Text="Plan Request - Provider Alert Network" OnClick="btnDAN_Click" ValidationGroup="vgNurse"/>
                                                        <p style="margin:5px 0px 0px 0px;">
                                                           <span style="font-weight:bold;">Hint:</span> Clicking this button will dispatch the alert email and take you to the Provider’s S.O.A.P. form.
                                                        </p>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlPhysicianPlan" runat="server" Visible="false">
                                                    <div style="margin:0px;width:500px;border-width:0px;border-style:none;">
                                                        <div >
                                                            <span class="caption">Provider's Assessment</span>
                                                            <div style="margin:5px 0px 5px 0px;width:480px">
                                                                <span class="caption">Problem/Dx</span>
                                                                <asp:UpdatePanel ID="upnProviderAssesment" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <span class="caption">Code Search/Free Text</span>
                                                                                    <asp:TextBox ID="txtProviderAssesmentSrch" runat="server" AutoPostBack="True" OnTextChanged="txtProviderAssesmentSrch_TextChanged"
                                                                                        Width="145px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <span class="caption">Result</span>
                                                                                    <asp:DropDownList ID="cboProvidersAssesment" runat="server" Width="145px" AutoPostBack="True"
                                                                                        DataTextField="RETURN_CODE" DataValueField="RETURN_CODE" OnSelectedIndexChanged="cboProvidersAssesment_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                        <div style="margin:0px;">
                                                            <h3>Plan</h3>
                                                        </div>
											            <div style="margin:0px;">
								  			                <fieldset id="DiagTest" style="margin:5px 0px 0px 0px;width:480px">
												                <legend class="caption">Diagnostic Test</legend>
												                <table>
												                <tr>
													                <td class="caption" style="float:left;width:150px;height:21px;vertical-align:middle">
													     	            &nbsp;</td>
													                 <td>
														                <table border="0" cellpadding="0" cellspacing="0">
	                                                                     <tr>
		
		                                                                    <td style="width:160px;height:21px;">
		                                                                        <span class="caption" >Code Search/Free Text</span>
		                                                                    </td>
		                                                                    <td style="width:100px;height:21px;">
		                                                                        <span class="caption">Result</span>
		                                                                    </td>
	                                                                    </tr>
	                                                                    </table>
                                                                    </td>
												                </tr>
												                </table>
	                                                            <div style="margin:0px;width:480px;float:none">
	                                             	            <table>
												                <tr>
													                <td class="caption" style="float:left;width:150px;height:21px;vertical-align:middle">
													     	            Radiology
													                 </td>
													                 <td>
                                                                        <asp:UpdatePanel ID="upnRadiology" runat="server" UpdateMode="Conditional">
                                                                            <ContentTemplate>
                                                                                <table border="0" cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtRadiologySrch" runat="server" AutoPostBack="True" Width="145px"
                                                                                                OnTextChanged="txtRadiologySrch_TextChanged"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList runat="server" ID="ddlRadiology" Width="145px" DataTextField="RETURN_CODE"
                                                                                                DataValueField="RETURN_CODE" AutoPostBack="True" 
                                                                                                OnSelectedIndexChanged="ddlRadiology_SelectedIndexChanged">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </td>
												                </tr>
												                </table>
	                                                            </div>
	                                                            <div style="margin:0px;">
	                                                            <table>
												                <tr>
													                <td class="caption" style="float:left;width:150px;height:21px;vertical-align:middle">
													     	            Pathology and Laboratory
													                 </td>
													                 <td>
                                                                        <asp:UpdatePanel ID="upnLab" runat="server" UpdateMode="Conditional">
                                                                            <ContentTemplate>
                                                                                <table border="0" cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtLabSrch" runat="server" AutoPostBack="True" Width="145px" 
                                                                                                OnTextChanged="txtLabSrch_TextChanged"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList runat="server" ID="ddlLab" Width="145px" AutoPostBack="True" DataTextField="RETURN_CODE"
                                                                                                DataValueField="RETURN_CODE" 
                                                                                                OnSelectedIndexChanged="ddlLab_SelectedIndexChanged">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </td>
												                </tr>
												                </table>
	                                                            </div>
	                                                            <div style="margin:0px;width:480px;">
	                                                            <table>
												                <tr>
													                <td class="caption" style="float:left;width:150px;height:21px;vertical-align:middle">
													     	            Procedures/Injections
													                 </td>
													                 <td>
                                                                        <asp:UpdatePanel ID="upnProcedure" runat="server" UpdateMode="Conditional">
                                                                            <ContentTemplate>
                                                                                <table border="0" cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtProceduresSrch" runat="server" AutoPostBack="True" Width="145px"
                                                                                                OnTextChanged="txtProceduresSrch_TextChanged"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList runat="server" ID="ddlProcedures" Width="145px" DataTextField="RETURN_CODE"
                                                                                                DataValueField="RETURN_CODE" AutoPostBack="True" 
                                                                                                OnSelectedIndexChanged="ddlProcedures_SelectedIndexChanged">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </td>
												                </tr>
												                </table>
	                                                            </div>
	                                                            <div style="margin:0px;">
	                                                			<table>
												                <tr>
													                <td class="caption" style="float:left;width:150px;height:21px;vertical-align:middle">
													     	            Performance Measure
													                 </td>
													                 <td>
                                                                        <asp:UpdatePanel ID="upnPerformance" runat="server" UpdateMode="Conditional">
                                                                            <ContentTemplate>
                                                                                <table border="0" cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtPerformanceSrch" runat="server" AutoPostBack="True" Width="145px"
                                                                                                OnTextChanged="txtPerformanceSrch_TextChanged"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList runat="server" ID="ddlPerformance" Width="145px" AutoPostBack="True"
                                                                                                DataTextField="RETURN_CODE" DataValueField="RETURN_CODE" 
                                                                                                OnSelectedIndexChanged="ddlPerformance_SelectedIndexChanged">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </td>
												                </tr>
												                </table>
	                                                            </div>
	                                                            <div style="margin:0px;width:480px;">
	                                                			<table>
												                <tr>
													                <td class="caption" style="float:left;width:150px;vertical-align:middle">
													     	            Emerging Technology Services and Procedures
													                 </td>
													                 <td>
                                                                        <asp:UpdatePanel ID="upnEmerging" runat="server" UpdateMode="Conditional">
                                                                            <ContentTemplate>
                                                                                <table border="0" cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtEmergingSrch" runat="server" AutoPostBack="True" Width="145px"
                                                                                                OnTextChanged="txtEmergingSrch_TextChanged"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList runat="server" ID="ddlEmerging" Width="145px" DataTextField="RETURN_CODE"
                                                                                                DataValueField="RETURN_CODE" AutoPostBack="True" 
                                                                                                OnSelectedIndexChanged="ddlEmerging_SelectedIndexChanged">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </td>
												                </tr>
												                </table>
	                                                            </div>
	                                                            <div style="margin:0px;">
	                                                            <table>
												                <tr>
													                <td class="caption" style="float:left;width:150px;height:21px;vertical-align:middle">
													     	            Other Test
													                 </td>
													                 <td>
                                                                        <asp:UpdatePanel ID="upnOtherTest" runat="server" UpdateMode="Conditional">
                                                                            <ContentTemplate>
                                                                                <table border="0" cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtOtherSrch" runat="server" AutoPostBack="True" Width="145px" 
                                                                                                OnTextChanged="txtOtherSrch_TextChanged"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList runat="server" ID="ddlOther" Width="145px" DataTextField="RETURN_CODE"
                                                                                                DataValueField="RETURN_CODE" AutoPostBack="True" 
                                                                                                OnSelectedIndexChanged="ddlOther_SelectedIndexChanged">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </td>
												                </tr>
												                </table>
	                                                            </div>

											                </fieldset>
                                                        </div>
                                          
                                                        <div style="margin:0px;">
                                                        	<fieldset style="margin:5px 0px 0px 0px;width:480px;">
												                <legend class="caption">Disposition Status</legend>
                                                                <asp:RadioButtonList ID="rblPlan" runat="server" DataValueField="UMR_PLAN_ID" DataTextField="PlanText">
                                                                </asp:RadioButtonList>
                                                            </fieldset>
                                                            <div style="margin:5px 0px 0px 0px;width:480px;">
                                                                <table>
												                <tr >
                                                                    <td class="caption" style="float:left;width:150px;height:21px;vertical-align:middle">
                                                                    Patient Educations
                                                                    </td>
                                                                    <td>
                                                                    <asp:TextBox ID="txtPatEducation" runat="server" Width="200px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                </table>
                                                            </div>
                                                            <div style="margin:5px 0px 0px 0px;">
                                                
	                                                            <table>
												                <tr >
                                                                    <td class="caption" style="float:left;width:150px;height:21px;vertical-align:middle">
                                                                    </td>
                                                                    <td>
                                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td style="width:160px;height:21px;">
                                                                                <span class="caption">
																	            Name 
																	            Search/Free 
																	            Text</span>
                                                                            </td>
                                                                            <td style="width:100px;height:21px;">
                                                                                <span class="caption">Result</span>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    </td>
                                                                </tr>
                                                                </table>
                                                            </div>
                                                            <div style="margin:0px;width:450px">
                                                	            <table>
												                <tr >
                                                                    <td class="caption" style="float:left;width:150px;height:21px;vertical-align:middle">
                                                                    Respond To
                                                                    </td>
                                                                    <td>
                                                                    <asp:UpdatePanel ID="upnRespondTo" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtRespondToSrch" runat="server" AutoPostBack="True" Width="145px"
                                                                                            OnTextChanged="txtRespondToSrch_TextChanged"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="ddlRespondTo" runat="server" Width="145px" AutoPostBack="True"
                                                                                            OnSelectedIndexChanged="ddlRespondTo_SelectedIndexChanged" DataTextField="FULLNAME"
                                                                                            DataValueField="FULLNAME">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                    </td>
                                                                </tr>
                                                                </table>
                                                            </div>
                                                            <div style="margin:0px;width:450px">
                                                	            <table>
												                <tr >
                                                                    <td class="caption" style="float:left;width:150px;height:21px;vertical-align:middle">
                                                                    Refer To
                                                                    </td>
                                                                    <td>
                                                                    <asp:UpdatePanel ID="upnReferTo" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtReferToSrch" runat="server" AutoPostBack="True" Width="145px"
                                                                                            OnTextChanged="txtReferToSrch_TextChanged"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="ddlReferTo" runat="server" Width="145px" AutoPostBack="True"
                                                                                            OnSelectedIndexChanged="ddlReferTo_SelectedIndexChanged" DataTextField="FULLNAME"
                                                                                            DataValueField="FULLNAME">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                    </td>
                                                                </tr>
                                                                </table>
                                                            </div>
                                                            <%--<div style="margin:0px;">
                                                                <div class="caption" style="float:left;width:150px;height:21px;vertical-align:middle">Alert Notification List</div>
                                                                <div style="padding-left:150px">
                                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:TextBox ID="txtAlertNotificationList" runat="server" AutoPostBack="True" 
                                                                                    Width="99px" ontextchanged="txtAlertNotificationList_TextChanged" ></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlAlertNotificationList" runat="server" Width="100px" 
                                                                                    AutoPostBack="True" 
                                                                                    onselectedindexchanged="ddlAlertNotificationList_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>--%>
											                <div style="margin:0px;">
                                                	            <table>
												                <tr >
                                                                    <td class="caption" style="float:left;width:150px;height:21px;vertical-align:middle">
                                                                    Immunization
                                                                    </td>
                                                                    <td>

                                                                    <asp:TextBox ID="txtImmunization" runat="server" Width="200px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                </table>
                                                            </div>
                                                            <div style="margin:0px;">
                                                                <table>
												                <tr >
                                                                    <td class="caption" style="float:left;width:150px;height:21px;vertical-align:middle">
                                                                    Follow Up
                                                                    </td>
                                                                    <td>
                                                                    <asp:TextBox ID="txtFollowUp" runat="server" Width="200px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                </table>
                                                            </div>
                                                            <div style="margin:0px;width:480px;">
                                                                <table>
												                <tr >
                                                                    <td class="caption" style="float:left;width:150px;height:21px;vertical-align:middle">
                                                                    Other
                                                                    </td>
                                                                    <td>
                                                                    <asp:TextBox ID="txtOther" runat="server" Width="200px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                        <div style="margin:5px 0px 0px 0px;">
                                                            <span class="caption">RX Prescription and Medication</span>
                                                            <div>
                                                                <asp:TextBox ID="txtPrescription" runat="server" Height="50px" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                                            </div>
                                                        </div>
                                            
                                                        <div >
                                                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                                                            <p style="margin:5px 0px 0px 0px;">
                                                                <span style="font-weight:bold;">Hint:</span> Clicking this button will save the Provider’s S.O.A.P. and take you to the Patient Emergency Transfer Network screen.
                                                            </p>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlETFRNotes" runat="server" Visible="false">
                                                    <div style="float: left; width: 400px">
                                                        <div >
                                                            <span class="caption">Provider's Assessment</span>
                                                            <asp:Label ID="txtDocAssETFR" runat="server" Height="50px" TextMode="MultiLine" Width="300px"></asp:Label>
                                                        </div>                                            
                                                        <div style="margin:0px;">
                                                            <div class="caption">Problem/Dx</div>
                                                            <div>
                                                                <asp:Label ID="litProb" runat="server" Height="50px" TextMode="MultiLine" Width="300px"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div style="margin:0px;">
                                                            <span class="caption">RX Prescription and Medication</span>
                                                            <div>
                                                                <asp:Label ID="litPrescriptRx" runat="server" Height="50px" TextMode="MultiLine" Width="300px"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div style="margin:0px;">
                                                            <span class="caption">Provider's Plan</span>
                                                            <asp:Label ID="lblDocPlan" runat="server"></asp:Label>
                                                        </div>
                                                        <div style="margin:0px;">
                                                        <table>
                                                            <tr>
                                                                <td class="caption" style="float:left;width:150px;">
                                                                Diagnostic Test
                                                                </td>
                                                                <td>
                                                                <asp:Label ID="litDiagonsTest" runat="server" Width="200px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="caption" style="float:left;width:150px;">
                                                                Lab
                                                                </td>
                                                                <td>
                                                                <asp:Label ID="litLab" runat="server" Width="200px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="caption" style="float:left;width:150px;">
                                                                Procedures/Injections
                                                                </td>
                                                                <td>
                                                                <asp:Label ID="litProcedures" runat="server" Width="200px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="caption" style="float:left;width:150px;">
                                                                Immunization
                                                                </td>
                                                                <td>
                                                                <asp:Label ID="litImmu" runat="server" Width="200px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="caption" style="float:left;width:150px;">
                                                                Patient Educations
                                                                </td>
                                                                <td>
                                                                <asp:Label ID="litPatientEdu" runat="server" Width="200px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="caption" style="float:left;width:150px;">
                                                                Respond To
                                                                </td>
                                                                <td>
                                                                <asp:Label ID="litRespond" runat="server" Width="200px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="caption" style="float:left;width:150px;">
                                                                Refer To
                                                                </td>
                                                                <td>
                                                                <asp:Label ID="litReferTo" runat="server" Width="200px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width:375px" valign="top">
                                <table>
									<tr>
                                        <td style="width: 375px;height:250px;">
                                            <div >
                                                <span class="caption">S.O.A.P Image (only .jpg images supported)</span>
                                            </div>
                                            <br />          
                                            <div id="dvImageUpLoader" runat="server" style="margin:0px;">
                                                <asp:FileUpload ID="fudPatientImage" runat="server" />
                                            </div>
                                            <div id="dvImageGrid" runat="server" style="height:200px;width:310px;">
                                                <asp:GridView ID="gdvPhoto" runat="server" AutoGenerateColumns="false" Width="300px" Height="150px">
                                                    <Columns>
                                                        <asp:ImageField DataImageUrlField="ID" DataImageUrlFormatString="PatientImage.aspx?VitalSignID={0}"
                                                            AlternateText="No Image Has been uploaded">
                                                            <ItemStyle Width="290px" Height="140px"/>
                                                        </asp:ImageField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                    <br />
                    <div >
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </div>
                </div>
            </td>
            <td style="width: 1px; background-color: #ddd; padding: 0px;">
            </td>
            <td align="left" valign="top">
                <div id="dvMail" style="padding: 10px;" runat="server">
                    <div id="dvHeader" style="padding: 10px;" runat="server">
                        <h2>
                            Send Email To</h2>
                    </div>
                    <br />
                    <asp:Panel ID="pnlNurseMail" runat="server">
                        <div>
                            <h3>
                                Primary Care Provider
                            </h3>
                            <asp:CheckBox ID="chkProviderEmail" runat="server" Checked="true" />
                            <asp:LinkButton ID="lnkPrimaryProvider" runat="server" />
                            <div id="dvMissingEmails" style="margin:0px;" runat="server" visible="false">
                                <p style="margin:5px 0px 0px 0px;">
                                    <span style="font-weight:bold;color:Red;">Email missing for the Primary Care Provider.</span>
                                    <span style="margin:5px 0px 0px 0px;font-weight:bold;color:Red;">Must enter a valid email address below.</span>
                                </p>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlEtrfnMail" Visible="false">
                        <div>
                            <h3>
                                Hospital
                            </h3>
                            <asp:GridView ID="grdInstitutions" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                ForeColor="#333333" GridLines="None" DataKeyNames="InstitutionID" AllowSorting="True"
                                ShowHeader="False" EnableModelValidation="True">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnEmail" runat="server" Value='<%# Eval("Email") %>' />
                                            <asp:HiddenField ID="hdnPhone" runat="server" Value='<%# Eval("Phone") %>' />
                                            <asp:CheckBox ID="chkEmail" runat="server" />
                                            <asp:LinkButton ID="lnkHospital" runat="server" Text='<%# Eval("InstitutionName") %>'
                                                ToolTip='<%# Eval("Email") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Label ID="lblNoInst" runat="server"></asp:Label>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlPhysicianMail" runat="server" Visible="false">

                        <div>
                            <h3>
                                Family Members
                            </h3>
                            <asp:GridView ID="grdEmeractgencyCont" runat="server" AutoGenerateColumns="False"
                                CellPadding="4" ForeColor="#333333" GridLines="None" AllowSorting="True" ShowHeader="False"
                                EnableModelValidation="True">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnEmail" runat="server" Value='<%# Eval("ContactEmail") %>' />
                                            <asp:HiddenField ID="hdnPhone" runat="server" Value='<%# Eval("ContactHomePhone") %>' />
                                            <asp:CheckBox ID="chkEmail" runat="server" />
                                            <asp:LinkButton ID="lnkFamilyMember" runat="server" Text='<%# Eval("ContactFirstName").ToString()+" "+Eval("ContactLastName").ToString() %>'
                                                ToolTip='<%# Eval("ContactEmail") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            
                            <asp:Label ID="lblNoFam" runat="server"></asp:Label>
                            <div id="divNoFam" style="margin:0px;" runat="server" visible="false">
                                <p style="margin:5px 0px 0px 0px;">
                                    <span style="font-weight:bold;color:Red;">Email missing for the Family Members.</span>
                                    <span style="margin:5px 0px 0px 0px;font-weight:bold;color:Red;">May enter a valid email address below.</span>
                                </p>
                            </div>
                        </div>
                    </asp:Panel>
                    <br />
                    <div id="dvTxtMailTo" style="padding: 10px;" runat="server">
                        <br />
                        Enter Email Address(es) (use  comma‭ (<span style="color: Navy;">,</span>) to separate multiple
                        email addresses)
                        <br />
                        <asp:TextBox ID="txtEmails" runat="server" TextMode="MultiLine" Height="57px" Width="294px"></asp:TextBox>
                          <asp:CustomValidator id="csvEmail"
                               ControlToValidate="txtEmails"
                               ClientValidationFunction="Client_ValidateEmail"
                               OnServerValidate="Server_ValidateEmails"
                               Text="*"
                               ErrorMessage="Invalid Email Address Found"
                               runat="server" ValidationGroup="vgNurse">
                          </asp:CustomValidator>
                          <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Email Address can not be empty." Text="*" 
                                ValidationGroup="vgNurse" ControlToValidate="txtEmails" Enabled="false">
                          </asp:RequiredFieldValidator>

                        <br />
                    </div>
                    <br />
                    <div id="dvSendReportTo" style="padding: 10px;" runat="server" visible="false">
                        <asp:Button ID="btnSend" runat="server" Text="Send Email" OnClick="btnSend_Click"
                            Visible="true" />
                        <p style="margin:5px 0px 0px 0px;">
                            <span style="font-weight:bold;">Hint:</span> Dispatches alert to selected party/parties and takes you to the first step of the Provider Alert Network process (e.g. Nurse Alert Network.).
                        </p>
                    </div>
                    <%--<div id="divFirstName">
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
                    </div>--%>
                </div>
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="vlsNurse" runat="server" ShowMessageBox="True" ShowSummary="False"  ValidationGroup="vgNurse"/>
    </form>
</body>
</html>
