<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDOptionPage.aspx.cs" Inherits="RDOptionPage" %>

<%@ Register Assembly="RadDock.Net2" Namespace="Telerik.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="RadPanelbar.Net2" Namespace="Telerik.WebControls" TagPrefix="radP" %>
<%@ Register Src="~/Oracle/ControlLibrary/ucRDSetUpOptions.ascx" TagName="RDSetUpOptions" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Set Remarkable Discipline Preference Page</title>
</head>
<body>
    <form id="form1" runat="server">

        <script type="text/javascript">
            function GetRadWindow()
			{   var oWindow = null;if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
				else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;//IE (and Moz az well)				
				return oWindow;}
            
            function CallFunctionOnParentPage(fnName)
			{	var oWindow = GetRadWindow(); var arg = new String();
				if (oWindow.BrowserWindow[fnName] && typeof(oWindow.BrowserWindow[fnName]) == "function")
				{   var opName = "DisciplineList;"; arg = arg.concat(opName);
				    cbl = document.getElementById("RDListAll_cblRD");
				    if(cbl)
				    {   elements = cbl.getElementsByTagName('input');
				        for(var i = 0, j = elements.length; i < j; i++)
				        {   id = "RDListAll_cblRD_" + i;
				            var input = document.getElementById(id);
				            if((document.form1.elements[id].checked))
				            {   var itemName = document.form1.elements[id].nextSibling.firstChild.nodeValue;
				                arg = arg.concat('#');
				                arg = arg.concat(itemName);} }
				        oWindow.BrowserWindow[fnName](arg);  }  }
				GetRadWindow().Close(); }
			
			function ValidateOptionSelection()
			{   cbl = document.getElementById("RDListAll_cblRD");
			    if(cbl)
			    {   elements = cbl.getElementsByTagName('input');
				    for(var i = 0, j = elements.length; i < j; i++)
				        {   id = "RDListAll_cblRD_" + i; var input = document.getElementById(id);
				            if((document.form1.elements[id].checked)){return true;} }
				alert("Please Select atleast 1 Discipline before clicking the Save button !!!"); return false;}}
			
			function RDSelectAll()
			{   
			    //debugger
			    var cbl = document.getElementById("RDListAll_cblRD");
			    if (cbl)
			    {   var elements = cbl.getElementsByTagName('input');
				    for(var i = 0, j = elements.length; i < j; i++){elements[i].checked= true;}
				    var chkSelNone = document.getElementById("chkSelNone");
				    if(chkSelNone)chkSelNone.checked = false; 
				    var chkSelRD = document.getElementById("chkSelRD");
				    if(chkSelRD)chkSelRD.checked = false;
				    return true;
				}
			}
			function RDSelectNone()
			{   
			    //debugger;
			    var cbl = document.getElementById("RDListAll_cblRD");
			    if (cbl)
			    {   var elements = cbl.getElementsByTagName('input');
				    for(var i = 0, j = elements.length; i < j; i++){elements[i].checked= false;} 
				    var chkSelAll = document.getElementById("chkSelAll");
				    if(chkSelAll)chkSelAll.checked = false; 
				    var chkSelRD = document.getElementById("chkSelRD");
				    if(chkSelRD)chkSelRD.checked = false;
				    return true; 
				}
			}
			function RDSelectRD()
			{
			   //first clear all
			   var bl = document.getElementById("RDListAll_cblRD");
			   if (bl)
			   {    var elements = bl.getElementsByTagName('input');
				    for(var i = 0, j = elements.length; i < j; i++){elements[i].checked= false;}
			    } 
			    //debugger;
			    var hf = document.getElementById('RDListAll_hfRDList');
			    if(hf){
			        var strRD = hf.value;
			        var arr = new Array();
                    arr = strRD.split('#');
                    //debugger;
                    if(arr.length > 0)
                    {
                        for(var i = 1, j = arr.length; i < j; i++) //loop the String Array
                        {
                            var item = arr[i];
                            cbl = document.getElementById("RDListAll_cblRD");
				            if(cbl)
				            {   
				                elements = cbl.getElementsByTagName('input');
				                for(var k = 0, l = elements.length; k < l; k++)
				                {   
				                    var id = "RDListAll_cblRD_" + k;
				                    var input = document.getElementById(id);
				                    if(input)
				                    { 
				                        var itemName = document.form1.elements[id].nextSibling.firstChild.nodeValue;
				                        if(item === itemName)
				                        {
				                            input.checked = true;
				                            break;
				                        }
				                    }//end if(input) 
				                }// end for(var i = 0, j = elements.length; i < j; i++)
				             }//end if(cbl)
                        }//end for(var i = 1, j = arr.length; i < j; i++) //loop the String Array
                    }//end if(arr.length > 0)
                    var chkSelAll = document.getElementById("chkSelAll");
				    if(chkSelAll)chkSelAll.checked = false;
				    var chkSelNone = document.getElementById("chkSelNone");
				    if(chkSelNone)chkSelNone.checked = false;
				    return true;
			    }//end if(hf)
		    }//end function
        </script>

        <div>
            <table style="width: 100%">
                <tr>
                    <td colspan="3">
                        <asp:Panel ID="pnlMain" runat="server" ScrollBars="Auto" 
                             Font-Names="Tahoma, Arial, Verdana" Font-Size="Small"
                             GroupingText="Please Select the Disciplines You Prefer to See in the Patient Details Page" >
                            <uc:RDSetUpOptions ID="RDListAll" runat="server" />
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <input type="checkbox" onclick="return RDSelectAll()" id="chkSelAll"/>
                        <label for="chkSelAll" style="font-size: 10pt; font-family: Tahoma, Arial, Verdana;">Select All</label>
                        &nbsp
                        <input type="checkbox" onclick="return RDSelectNone()" id="chkSelNone" />
                        <label for="chkSelNone" style="font-size:10pt; font-family:Tahoma, Arial, Verdana;">Select None</label>
                        &nbsp
                        <input type="checkbox" onclick="return RDSelectRD()" id="chkSelRD" />
                        <label for="chkSelRD" style="font-size:10pt; font-family:Tahoma, Arial, Verdana;">Select Remarkable Disciplines</label>
                        &nbsp
                        <asp:Button ID="btnSubmit" runat="server" OnClientClick="return ValidateOptionSelection()" OnClick="btnSubmit_Click" Text="Save Preferece" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:Label ID="InjectScriptLabel" runat="server"></asp:Label>
    </form>
</body>
</html>
