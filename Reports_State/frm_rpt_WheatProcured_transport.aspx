<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_rpt_WheatProcured_transport.aspx.cs" Inherits="Reports_State_frm_rpt_WheatProcured_transport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Wheat2015 Procurement Monitoring System</title>

    <script type="text/javascript" src="../../../js/calendar_eu.js"></script>

    <link rel="stylesheet" type="text/css" href="../../CSS/calendar.css" />

    <script type="text/javascript" src="../../js/AllCommonfunction.js"></script>

    <script type="text/javascript">
    window.history.forward(0); 
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center>
                <table border="1" cellpadding="0" cellspacing="0" style="border-style: double; border-width: 3;
                    padding: 1; border-collapse: collapse; border-color: Maroon; width: 100%; background-color: #ece9d8;"
                    id="">
                    <tr>
                        <td align="center" colspan="4" style="font-weight: bolder; font-size: 10pt; color: black;
                            height: 21px; background-color: #ece9d8;">
                          eUparjan Procurement of Wheat Rabi 2015-16
                        </td>
                        <td style="background-color: #ece9d8;">
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Back</asp:LinkButton></td>
                    </tr>
                    
                </table>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="1000px" ProcessingMode="Remote"
                    Width="100%" Font-Names="Verdana" Font-Size="8pt" ShowCredentialPrompts="False"
                    ZoomMode="PageWidth" ZoomPercent="50"  BorderColor="#E0E0E0"
                         LinkDisabledColor="Black" LinkActiveColor="Yellow" Style="margin-left: 0px; margin-right: 0px;"
                            ShowBackButton="true" EnableTheming="true">
                    <ServerReport ReportServerUrl="" />
                </rsweb:ReportViewer>
            </center>
        </div>
    </form>
</body>
</html>
