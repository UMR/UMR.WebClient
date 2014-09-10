<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LegendEdit.aspx.cs" Inherits="Oracle_LegendEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=7" />

    <script type="text/javascript" src="../jquery.js"></script>

    <script type="text/javascript" src="ControlLibrary/jQueryTooltip/aeldatevalidator.js"></script>

    <script type="text/javascript" src="../ui.datepicker.js"></script>

    <title>Edit Legend</title>
    <link type="text/css" rel="Stylesheet" href="../ui.datepicker.css" />

    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtDay1").datepicker({
                showOn: "button",
			    buttonImage: "images/calendar.png",
			    buttonImageOnly: true,
                minDate: new Date(1950, 1 - 1, 1),
                maxDate: new Date(AddDate(new Date(), -1)),
                onClose: txtDay1_onClose
            });
            $("#txtDay1").blur(function() 
            { 
                if (!aelIsValidDate($('#txtDay1').val())) 
                {
//                    $('#txtDay1').focus();
                    alert('Date for color 1 is not in correct format.');
                }
                else
                {
                    var date1 = $('#txtDay1').val();
                    date1 = new Date(date1);
                    txtDay1_onClose(date1, 1);
                }
            }); 
            $("#txtDay2").datepicker({
                showOn: "button",
			    buttonImage: "images/calendar.png",
			    buttonImageOnly: true,
                minDate: new Date(1950, 1 - 1, 1),
                maxDate: new Date(AddDate(new Date(), -1)),
                onClose: txtDay2_onClose
            });
            $("#txtDay2").blur(function() 
            { 
                if (!aelIsValidDate($('#txtDay2').val())) 
                {
//                    $('#txtDay2').focus();
                    alert('Date for color 2 is not in correct format.');
                }
                else
                {
                    var date1 = $('#txtDay2').val();
                    date1 = new Date(date1);
                    txtDay2_onClose(date1, 1);
                }
            });
            $("#txtDay3").datepicker({
                showOn: "button",
			    buttonImage: "images/calendar.png",
			    buttonImageOnly: true,
                minDate: new Date(1950, 1 - 1, 1),
                maxDate: new Date(AddDate(new Date(), -1)),
                onClose: txtDay3_onClose
            });
            $("#txtDay3").blur(function() 
            { 
                if (!aelIsValidDate($('#txtDay3').val())) 
                {
//                    $('#txtDay3').focus();
                    alert('Date for color 3 is not in correct format.');
                }
                else
                {
                    var date1 = $('#txtDay3').val();
                    date1 = new Date(date1);
                    txtDay3_onClose(date1, 1);
                }
            });
            function txtDay1_onClose(date2, inst) {
                var date1 = new Date();
                date1 = date1.toDateString();
                if (date2 == '' || date2 == null) return;

                var diff = DateDiff(date1, date2.toDateString(), 'days');
                if (diff >= 0) {
//                    alert('Ending date for color 1 needs to be smaller than the ending date for color today.\n\nPlease choose a date that is smaller than the ending date for color "' + DateFormat(date2) + '"');
                    var onedayBefore = AddDate(date1, -1);
                    $('#txtDay1').val(DateFormat(onedayBefore));
                    $('#txtDay1').focus();
                }
                $('#lblDateDiff1').text(diff * -1);

                var onedayBefore = AddDate(date2, -1);
                $('#lblFromDate2').text(DateFormat(onedayBefore));
                CalculateLegend2(onedayBefore, false);
            }
            function txtDay2_onClose(date2, inst) {
                CalculateLegend2(date2, true);
            }
            function txtDay3_onClose(date2, inst) {
                var date1 = $('#lblFromDate3').text();
                if ($.trim(date1) == 'Now') date1 = new Date();
                date1 = new Date(date1);
                date1 = date1.toDateString();
                if (date2 == '' || date2 == null) return;

                var diff = DateDiff(date1, date2.toDateString(), 'days');
                if (diff >= 0) {
                    var onedayBefore = AddDate(date1, -1);
                    $('#txtDay3').val(DateFormat(onedayBefore));
                    $('#lblDateDiff3').text('1');
                    diff = 0;
                    alert('Ending date for color 3 needs to be smaller than the ending date for color 2.\n\nPlease choose a date that is smaller than the ending date for color 2.');
                    $('#txtDay3').focus();
                }
                else
                {
                    $('#lblDateDiff3').text(diff * -1);
                }
            }
            function CalculateLegend3(date, reset, closeEvent) {
                if (reset) {
                    $('#lblFromDate3').text(DateFormat(date));
                    $('#txtDay3').val(DateFormat(date));
                    $('#lblDateDiff3').text('0');
                }
                else {
                    if (date == '' || date == null) return;

                    var toDate = null;
                    var fromDate = null;

                    if (closeEvent) {
                        fromDate = $('#lblFromDate2').text();
                        toDate = date;
                    }
                    else {
                        fromDate = date;
                        toDate = $('#txtDay2').val();
                    }


                    if (fromDate == '' || fromDate == null) return;

                    fromDate = new Date(fromDate);
                    var toDate = $('#txtDay3').val();

                    $('#lblFromDate3').text(DateFormat(fromDate));

                    if ($.trim(toDate) == '') {
                        var onedayBefore = AddDate(fromDate, -1);
                        $('#txtDay3').val(DateFormat(onedayBefore));
                        $('#lblDateDiff3').text('1');
                    }
                    else {
                        toDate = new Date(toDate);
                        var diff = DateDiff(fromDate, toDate.toDateString(), 'days');
                        if (diff >= 0) {
                            diff = 0;

                            if (reset) {
                                alert('Ending date for color 3 needs to be smaller than the ending date for color 2.\n\nPlease choose a date that is smaller than the ending date for color 2.');
                            }

                            var onedayBefore = AddDate(fromDate, -1);
                            $('#txtDay3').val(DateFormat(onedayBefore));
                            $('#lblDateDiff3').text('1');

                            $('#txtDay3').focus();
                        }
                        else {
                            $('#lblDateDiff3').text(diff * -1);
                        }
                    }
                }
            }
            function CalculateLegend2(date, closeEvent) {
                if (date == '' || date == null) return;

                var toDate = null;
                var fromDate = null;

                if (closeEvent) {
                    fromDate = $('#lblFromDate2').text();
                    toDate = date;
                }
                else {
                    fromDate = date;
                    toDate = $('#txtDay2').val();
                }

                fromDate = new Date(fromDate);
                toDate = new Date(toDate);
                var reset = false;

                //                if ($.trim(toDate) == '') {
                if (isNaN(toDate)) {
                    $('#txtDay2').val(DateFormat(AddDate($('#lblFromDate2').text(), -1)));
                    $('#lblDateDiff2').text('1');
                }
                else {

                    var diff = DateDiff(fromDate.toDateString(), toDate.toDateString(), 'days');
                    if (diff >= 0) {
                        diff = 0;

                        alert('Ending date for color 2 needs to be smaller than the ending date for color 1.\n\nPlease choose a date that is smaller than the ending date for color 1.');

                        var onedayBefore = AddDate($('#lblFromDate2').text(), -1);
                        $('#txtDay2').val(DateFormat(onedayBefore));
                        $('#lblDateDiff2').text('1');
                        reset = true;


                        CalculateLegend3(onedayBefore, reset, false);
                        $('#txtDay2').focus();
                    }
                    else {
                        $('#lblDateDiff2').text(diff * -1);
                        var onedayBefore = AddDate($('#txtDay2').val(), -1);

                        CalculateLegend3(onedayBefore, reset, false);
                    }
                }
            }

            function AddDate(date, days) {
                var myDate = new Date(date);
                myDate.setDate(myDate.getDate() + days);
                return myDate.toDateString();
            }
            function PaddingLeft(number, length) {
                var str = '' + number;
                while (str.length < length) {
                    str = '0' + str;
                }

                return str;

            }
            function DateFormat(date) {
                var today = new Date(date);
                var dd = today.getDate();
                var mm = today.getMonth() + 1; //January is 0!
                var yyyy = today.getFullYear();
                if (dd < 10) { dd = '0' + dd }
                if (mm < 10) { mm = '0' + mm }

                return mm + '/' + dd + '/' + yyyy;
            }

            function DateDiff(date1, date2, interval) {
                //http://stackoverflow.com/questions/542938/how-do-i-get-the-number-of-days-between-two-dates-in-jquery
                var second = 1000, minute = second * 60, hour = minute * 60, day = hour * 24, week = day * 7;
                date1 = new Date(date1);
                date2 = new Date(date2);
                var timediff = date2 - date1;
                if (isNaN(timediff))
                    return NaN;
                switch (interval) {
                    case "years": return date2.getFullYear() - date1.getFullYear();
                    case "months": return ((date2.getFullYear() * 12 + date2.getMonth()) - (date1.getFullYear() * 12 + date1.getMonth())
                );
                    case "weeks": return Math.floor(timediff / week);
                    case "days": return Math.floor(timediff / day);
                    case "hours": return Math.floor(timediff / hour);
                    case "minutes": return Math.floor(timediff / minute);
                    case "seconds": return Math.floor(timediff / second);
                    default: return undefined;
                }
            }

            function IsValidDateRange() {
                var flag = false;
                if ($.trim($('#txtDay1').val()) == '') {
                    $('#txtDay1').focus()
                    alert('Date for color1 is empty.');
                    return flag;
                }
                else if ($.trim($('#txtDay2').val()) == '') {
                    $('#txtDay2').focus()
                    alert('Date for color2 is empty.');
                    return flag;
                }
                else if ($.trim($('#txtDay3').val()) == '') {
                    $('#txtDay3').focus()
                    alert('Date for color3 is empty.');
                    return flag;
                }
                else if (!aelIsValidDate($('#txtDay1').val())) {
                    alert('Date for color 1 is not in correct format.');
                    $('#txtDay1').focus();
                    return flag;
                }
                else if (!aelIsValidDate($('#txtDay2').val())) {
                    alert('Date for color 2 is not in correct format.');
                    $('#txtDay2').focus();
                    return flag;
                }
                else if (!aelIsValidDate($('#txtDay3').val())) {
                    alert('Date for color 3 is not in correct format.');
                    $('#txtDay3').focus();
                    return flag;
                }

                var date1 = new Date($('#lblFromDate2').text());
                var date2 = new Date($('#txtDay2').val());

                var datediff1 = DateDiff(date1.toDateString(), date2.toDateString(), 'days')
                if (datediff1 > 0) {
                    $('#txtDay2').focus();
                    flag = false;
                    alert('Ending date for color 2 needs to be smaller than the ending date for color 1.\n\nPlease choose a date that is smaller than the ending date for color 1.');
                }
                else {
                    date1 = new Date($('#lblFromDate3').text());
                    date2 = new Date($('#txtDay3').val());

                    var datediff2 = DateDiff(date1.toDateString(), date2.toDateString(), 'days')
                    if (datediff2 < 0)
                        flag = true;
                    else {
                        $('#txtDay3').focus();
                        alert('Ending date for color 3 needs to be smaller than the ending date for color 2.\n\nPlease choose a date that is smaller than the ending date for color 2');
                        flag = false;
                    }
                }

                return flag;
            }

            $('#btnSaveLegend').click(function () {
                var flag = true;
                if (!IsValidDateRange()) {
                    flag = false;
                }
                return flag;
            });

        });
    </script>

    <style type="text/css">
    .tableLegend
    {
	    width: 100%;
	    border-top: solid 1px gray; /* #000000; */
	    border-right: solid 1px gray;
    }

    .tdLegendFirst
    {
	    width: 25%;
	    font-family: Tahoma;
	    font-size: 8pt;
	    border-left: solid 1px gray;
	    border-bottom: solid 1px gray;
    }
    .tdLegendSecond
    {
        width: 33%;
	    font-family: Tahoma;
	    font-size: 8pt;
	    border-bottom: solid 1px gray;
    }
    .tdLegendThird
    {
        width: 7%;
	    font-family: Tahoma;
	    font-size: 8pt;
	    border-bottom: solid 1px gray;
    }
    .tdLegendFourth
    {
        width: 2%;
	    font-family: Tahoma;
	    font-size: 8pt;
	    border-bottom: solid 1px gray;
    }
    .tdLegendFifth
    {
        width: 11%;
	    font-family: Tahoma;
	    font-size: 8pt;
	    border-bottom: solid 1px gray;
    }
    .tdLegendFifth image
    {
       margin-top:-5px !important;
    }
    .tdLegendSixth
    {
        width: 23%;
	    font-family: Tahoma;
	    font-size: 8pt;
	    border-bottom: solid 1px gray;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="font-size: 8pt; font-family: Tahoma;">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 60px">
                            First Date:</td>
                        <td style="width: 100px">
                            <asp:Label ID="lblFirstDate" runat="server" /></td>
                        <td style="width: 60px">
                            Last Date:</td>
                        <td>
                            <asp:Label ID="lblLastDate" runat="server" /></td>
                        <td style="width: 60px">
                            Color Hint:</td>
                        <td style="width: 20px">
                            <asp:Panel ID="Panel1" runat="server" Height="15px" Width="15px" BackColor="#ff8282">
                            </asp:Panel>
                        </td>
                        <td style="width: 100px">
                            <asp:Label ID="lblLegenReddHint" runat="server" /></td>
                    </tr>
                </table>
            </div>
            <table cellpadding="2" cellspacing="2" class="tableLegend" width="100%">
                <tr>
                    <td style="width: 60%" class="tdLegendFirst">
                        <table class="tableLegend" width="100%" cellpadding="2" cellspacing="0">
                            <tr>
                                <td style="text-align: left; font-weight: bold; width: 25%" class="tdLegendFirst">
                                    Symbol</td>
                                <td style="text-align: center; font-weight: bold; width: 75%" class="tdLegendFirst"
                                    colspan="5">
                                    Definition</td>
                            </tr>
                            <tr>
                                <td class="tdLegendFirst" style="background-color: #ff8282">
                                </td>
                                <%--<td class="tdLegendFirst"> 
                                </td>--%>
                                <td class="tdLegendSecond">
                                    Data sensitive to color legend of the time range that includes</td>
                                <td class="tdLegendThird">
                                    Present&nbsp;</td>
                                <td class="tdLegendFourth">
                                    to</td>
                                <td class="tdLegendFifth" align="left" valign="top">
                                    <asp:TextBox ID="txtDay1" runat="server" Width="60px" Font-Names="Tahoma" MaxLength="10"
                                        Font-Size="8pt"></asp:TextBox>
                                    <label id="lblDay1Display">
                                    </label>
                                </td>
                                <td class="tdLegendSixth">
                                    Number of Day(s): 
                                    <asp:Label ID="lblDateDiff1" runat="server" Font-Names="Tahoma" Font-Size="8pt" Text="0"></asp:Label></td>
                                <%--<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtDay1"
                                        ErrorMessage="Invalid Number" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>--%>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDay1"
                                        ErrorMessage="Value Required"></asp:RequiredFieldValidator>--%>
                            </tr>
                            <tr>
                                <td class="tdLegendFirst" style="background-color: #f4a460">
                                </td>
                                <%--<td class="tdLegendFirst"></td>--%>
                                <td class="tdLegendSecond">
                                    Data sensitive to color legend of the time range next from</td>
                                <td class="tdLegendThird">
                                    <asp:Label ID="lblFromDate2" Font-Names="Tahoma" Font-Size="8pt" runat="server" Text=""></asp:Label>
                                </td>
                                <td class="tdLegendFourth">
                                    to</td>
                                <td class="tdLegendFifth">
                                    <asp:TextBox ID="txtDay2" runat="server" Width="60px" Font-Size="8pt" MaxLength="10"
                                        Font-Names="Tahoma"></asp:TextBox></td>
                                <td class="tdLegendSixth">
                                    Number of Day(s): 
                                    <asp:Label ID="lblDateDiff2" runat="server" Font-Names="Tahoma" Font-Size="8pt" Text="0"></asp:Label></td>
                                <%--<asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtDay2"
                                        ErrorMessage="Invalid Number" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>--%>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDay2"
                                        ErrorMessage="Value Required"></asp:RequiredFieldValidator>--%>
                            </tr>
                            <tr>
                                <td class="tdLegendFirst" style="background-color: #82ff82" />
                                <%--<td class="tdLegendFirst"></td>--%>
                                <td class="tdLegendSecond">
                                    Data sensitive to color legend of the time range next from</td>
                                <td class="tdLegendThird">
                                    <asp:Label ID="lblFromDate3" Font-Names="Tahoma" Font-Size="8pt" runat="server" Text=""></asp:Label></td>
                                <td class="tdLegendFourth">
                                    to</td>
                                <td class="tdLegendFifth">
                                    <asp:TextBox ID="txtDay3" runat="server" Width="60px" Font-Size="8pt" MaxLength="10"
                                        Font-Names="Tahoma"></asp:TextBox></td>
                                <td class="tdLegendSixth">
                                    Number of Day(s): 
                                    <asp:Label ID="lblDateDiff3" runat="server" Font-Names="Tahoma" Font-Size="8pt" Text="0"></asp:Label></td>
                                <%--<asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtDay3"
                                        ErrorMessage="Invalid Number" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDay3"
                                        ErrorMessage="Value Required"></asp:RequiredFieldValidator>--%>
                            </tr>
                            <tr>
                                <td class="tdLegendFirst" style="background-color: #8cdaff" />
                                <td class="tdLegendFirst" colspan="5">
                                    Data sensitive to color legend of the time range next
                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <div style="font-size: 8pt; font-family: Tahoma;">
                                        <asp:CheckBox ID="chkDateRange" runat="server" Text="Show reords for last" />
                                        <asp:TextBox ID="txtDateRange" runat="server" Width="40px" MaxLength="10" Font-Names="Tahoma"
                                            Font-Size="8pt"></asp:TextBox>
                                        <asp:DropDownList ID="DropDownList1" runat="server" Font-Names="Tahoma" Font-Size="8pt">
                                            <asp:ListItem>Day</asp:ListItem>
                                            <asp:ListItem>Week</asp:ListItem>
                                            <asp:ListItem>Month</asp:ListItem>
                                            <asp:ListItem>Year</asp:ListItem>
                                        </asp:DropDownList>
                                        only
                                        <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtDateRange"
                                            ErrorMessage="Invalid Number" Operator="DataTypeCheck" Type="Integer">
                                        </asp:CompareValidator>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblMessage" runat="server" Visible="False" Font-Bold="True"></asp:Label>
                                </td>
                                <td style="text-align: right;" colspan="5">
                                    <asp:ImageButton ID="btnSaveLegend" ImageUrl="~/Oracle/images/save_button.png" runat="server"
                                        OnClick="btnSaveLegend_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <input type="hidden" runat="server" id="issaved" name="issaved" value="NotSaved" />
    </form>

    <script type="text/javascript">
        function GetRadWindow()
	    {
	        var oWindow = null;
	        if (window.radWindow) oWindow = window.radWindow;
	        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
	        return oWindow;
	    } 
	    
	    var wnd=GetRadWindow();
	    wnd.OnClientClose = function(wnd)
        {
            var parent= GetParentWindowForRefresh(wnd);
            var issaved=document.getElementById("issaved").value;
            if(issaved=='saved')
            {
                var parentUrl=parent.location.href;
                if(parentUrl.indexOf("reloaded=true")<0)
                {
                    parentUrl+="&reloaded=true";
                }
                parent.location.href= parentUrl;
            }
        }
        
        function GetParentWindowForRefresh(wnd)
        {   
            var urlStr= wnd.BrowserWindow.location.href;
            if(urlStr.indexOf("Legend.aspx")>=0)
            {
               return wnd.BrowserWindow.GetParentWindow();
            }
            else
            {   
                return wnd.BrowserWindow;
            }
        }
        
        function IsSaved()
        {
            var issaved=document.getElementById("issaved").value;
            if(issaved=='saved') return true;
            else return false;
        }
    </script>

</body>
</html>
