<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmergencyTransferFormRequest.aspx.cs"
    Inherits="Oracle_EmergencyTransferFormRequest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Emergency Transfer Form Request </title>
    <script src="../jquery.js" type="text/javascript"></script>
    <link href="../ui.datepicker.css" rel="stylesheet" type="text/css" />
    <script src="../ui.datepicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<% =txtDate.ClientID %>").datepicker();
        });
    </script>
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
            padding-left: 2px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td align="left" valign="top">
                <div id="dvVitalSigns" style="padding: 10px;">
                    <h2><asp:Label ID="lblPatientName" runat="server"/></h2>
                    <br />
                    <div>
                        <span class="caption">Patient Image</span>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <asp:FileUpload ID="fudPatientImage" runat="server" />
                                </td>
                            </tr>
                            <%--<tr>
                                <td>
                                </td>
                            </tr>--%>
                        </table>
                    </div>
                    <br />
                    <%--<h2>Subjective description from patient in detail</h2>--%>
                    <h2>Subjective</h2>
                    <br />
                    <div>
                        <span class="caption">Chief Complaint</span>
                        <asp:TextBox ID="txtChiefComplain" runat="server" Height="50px" TextMode="MultiLine"
                            Width="300px"></asp:TextBox>
                    </div>
                    <br />
                    <h2>Vital Signs</h2>
                    <div>
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
                                    <td>
                                        BMI
                                    </td>
                                    <td>
                                        <span>Weight</span>
                                        <asp:TextBox ID="txtWeight" runat="server" Width="40px"></asp:TextBox>
                                        <span>Height</span>
                                        <asp:TextBox ID="txtHeight" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
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
                        <br />
                        <div>
                            <span class="caption">Assessment</span>
                            <asp:TextBox ID="txtAssessment" runat="server" Height="50px" TextMode="MultiLine"
                                Width="300px"></asp:TextBox>
                        </div>
                        <br />
                        <div>
                            <table>
                                <tr>
                                    <td>
                                        Date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDate" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </div>
            </td>
            <td style="width: 1px; background-color: #ddd; padding: 0px;">
            </td>
            <td align="left" valign="top">
                <div id="dvSendReportTo" style="padding: 10px;" runat="server" visible="false">
                    <h2>
                        Send Report To</h2>
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
                    </div>
                    <div>
                        <h3>
                            Primary Care Doctor
                        </h3>
                        <asp:CheckBox ID="chkProviderEmail" runat="server" />
                        <asp:LinkButton ID="lnkPrimaryProvider" runat="server" />
                    </div>
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
                    </div>
                    <div>
                        <br />
                        Enter Email Address(use <span style="color: Navy;">,</span> to separate multiple
                        email address)
                        <br />
                        <asp:TextBox ID="txtEmails" runat="server" TextMode="MultiLine" Height="57px" Width="294px"></asp:TextBox>
                        <br />
                        <asp:Button ID="btnSend" runat="server" Text="Send Email" OnClick="btnSend_Click" />
                        <br />
                    </div>
                    <div>
                        <br />
                        Enter Phone No(use <span style="color: Navy;">,</span> to separate multiple Phone)
                        <br />
                        <asp:TextBox ID="txtPhones" runat="server" TextMode="MultiLine" Height="57px" Width="294px"></asp:TextBox>
                        <br />
                        <br />
                    </div>
                    <asp:Button ID="btnSendSMS" runat="server" Text="Send SMS" OnClick="btnSendSMS_Click"
                        Visible="true" />
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
