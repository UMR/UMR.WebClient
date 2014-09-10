<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucDisclaimer.ascx.cs" Inherits="Oracle_ControlLibrary_ucDisclaimer" %>
<%@ OutputCache Duration="20" VaryByParam="none" %>

<table>
    <tr>
        <td>
            <br />
        </td>
    </tr>
    <tr>
        <td>
            <label id="lblDisclaimer" style="color: Red; font-size: 8pt; font-family: Tahoma, Verdana, Arial;
                font-weight: bold">
                DISCLAIMER</label>
            <label id="lblDisclaimerDetails" style="font-size: 8pt; font-family: Tahoma, Verdana, Arial">
                - UNIVERSAL MEDICAL RECORDS, does not warrant the completeness, accuracy or correctness
                of this report or any of the information contained herein, UNIVERSAL MEDICAL RECORDS,
                is not liable for any loss, damage or injury caused by the negligence or other
                act or failure of UNIVERSAL MEDICAL RECORDS, in procuring, collecting or communicating
                any such information. Reliance on any information contained herein shall be solely
                at the user's risk and shall constitute a waiver of any claim against and a release
                of UNIVERSAL MEDICAL RECORDS. This report if furnished in strict confidence for
                your exclusive use for legitimate medical purposes and for no other purpose, and
                shall not be reproduced in whole or in part in any manner whatsoever.
            </label>
        </td>
    </tr>
    <tr>
        <td>
            <label id="lbl2dis" style="font-size: 8pt; font-family: Tahoma, Verdana, Arial">
                Exceeds ASTM Standard F 1625-95, Standard Guide For Providing Essential Data Needed
                In Advance For Prehospital Emergency Medical Services
            </label>
        </td>
    </tr>
    <tr>
        <td>
            <label id="lblCR" style="font-size: 8pt; font-family: Tahoma, Verdana, Arial">
                &#169 1996 - <asp:Label ID="lblCurrentYear" runat="server" /> Universal Medical Records Ltd. All Rights Reserved, Patents Pending.
            </label>
        </td>
    </tr>
</table>
