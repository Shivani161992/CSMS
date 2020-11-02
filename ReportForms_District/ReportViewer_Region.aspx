<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportViewer_Region.aspx.cs" Inherits="IssueCenterLevel_Storage_ReportViewer_Region" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Reports</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <rsweb:reportviewer id="ReportViewer_Region" runat="server" SizeToReportContent="True" Width="100%"  ProcessingMode="Remote"
    ZoomMode="PageWidth" Height="600px"></rsweb:reportviewer>
        <asp:Label ID="District" runat="server" Text="Label" Visible="False"></asp:Label><asp:Label
            ID="Depot" runat="server" Text="Label" Visible="False"></asp:Label><br />
    </div>
    </form>
</body>
</html>
