<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DockRD.aspx.cs" Inherits="Oracle_DockRD" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="RadDock.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Related Disciplines</title>
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

    <script src="../jquery.js" type="text/javascript"></script>

    <script src="../tooltip.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
        <radW:RadDockingManager ID="RadDockingManager1" runat="server" Skin="Office2007"></radW:RadDockingManager>
        <div>
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        </div>
    </form>
</body>
</html>
