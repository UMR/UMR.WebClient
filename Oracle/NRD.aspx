<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NRD.aspx.cs" Inherits="Oracle_NRD" %>
<%@ Register Src="~/Oracle/ControlLibrary/ucUnRD.ascx" TagName="UnRemarkableDisciplines" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Unremarkable Disciplines</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc:UnRemarkableDisciplines ID="UnRD" runat="server" />
    </div>
    </form>
</body>
</html>
