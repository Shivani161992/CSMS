<%@ Page Language="VB" AutoEventWireup="false" CodeFile="graph_StAllocLift_rpt.aspx.vb" Inherits="graph_StAllocLift_rpt" %>
 
<%@ Register Assembly="CustomControlFreak" Namespace="CustomControlFreak" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>State Allocation vs Lifting</title>
    <meta http-equiv="refresh" content="10" />
</head>
<body>
    <form id="form1" runat="server">        
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/State/frmReports_State.aspx">Back</asp:HyperLink><br />
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="590px" ProcessingMode="Remote" Width="978px" Font-Names="Verdana" Font-Size="8pt" ShowCredentialPrompts="False">
            <ServerReport ReportServerUrl=""  />
        </rsweb:ReportViewer>    
    </form>
</body>
</html>


