<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCommentsTop.ascx.cs"
    Inherits="Oracle_ControlLibrary_ucCommentsTop" %>


<table width="100%">
    <tr valign="top">
        <td colspan="3" style="width: 969px">
            <label style="font-size: 7pt; font-family: Tahoma, Verdana, Arial; color: Red; text-align: left">
                This Universal Medical Record Was Compiled On:
                <%= DateTime.Now.ToString()%>
                EST
            </label>
        </td>
    </tr>
    <tr>
        <td>
            
        </td>
    </tr>
    <tr valign="top">
        <td colspan="3">
            <label id="lblInfo" style="font-size: 8pt; font-family: Tahoma, Verdana, Arial; color: Red;
                text-align: center">
                THIS UNIVERSAL MEDICAL RECORD WAS COMPILED BY EITHER THE PATIENT AND/OR THE HEALTH
                CARE PROVIDER(S) AND/OR THE PAYOR AND IS CONSIDERED ACCURATE AND COMPLETE ACCORDING
                TO ALL PARTIES.
            </label>
        </td>
    </tr>
</table>
