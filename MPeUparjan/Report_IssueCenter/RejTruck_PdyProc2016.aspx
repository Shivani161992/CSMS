<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RejTruck_PdyProc2016.aspx.cs" Inherits="Report_IssueCenter_RejTruck_PdyProc2016" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>
    <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="../PaddyMilling/calendar/calendar.js"></script>

    <style type="text/css">
        .auto-style1 {
            font-size: 20px;
            font-weight: bold;
            color: #0000FF;
        }

        .ButtonClass {
            cursor: pointer;
        }
    </style>



</head>
<body>
    <form id="form1" runat="server">
        <input id="hdfDistName" type="hidden" runat="server" />
        <input id="hdfICName" type="hidden" runat="server" />
        <div>

            <table border="1" cellpadding="0" cellspacing="0" style="border-style: double; border-width: 3; padding: 1; border-collapse: collapse; border-color: Maroon; width: 100%; background-color: lavender;"
                align="center">
                <tr style="text-align: center">
                    <td colspan="3" class="auto-style1" style="color: #CC6600; background-color: #FFCCFF">Rejected Truck Report (Procurement 2016)</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Style="font-weight: 700; font-size: large;" Text="Commodity"></asp:Label>
                        <asp:DropDownList ID="ddlComdty" runat="server" Width="173px" style="margin-left:20px;">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align:center">
                        <asp:Button ID="btnReport" runat="server" Text="View Report" OnClick="btnReport_Click" Style="font-weight: 700; font-size: small;" CssClass="ButtonClass" />
                    </td>

                    <td style="text-align: center">
                        <asp:LinkButton ID="lnkbtnBack" runat="server" Font-Bold="True" ForeColor="#CC3300" Style="text-align: center" OnClick="lnkbtnBack_Click">Back</asp:LinkButton></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Panel ID="pnlreport" runat="server" Visible="false">
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="520px" ProcessingMode="Remote"
                                Width="100%" Font-Names="Verdana" Font-Size="8pt" ShowCredentialPrompts="False"
                                ZoomMode="PageWidth" BackColor="CornflowerBlue" BorderColor="#E0E0E0"
                                LinkDisabledColor="Black" LinkActiveColor="Yellow" Style="margin-left: 0px; margin-right: 0px;"
                                ShowBackButton="true" EnableTheming="true">
                                <ServerReport ReportServerUrl="" />
                            </rsweb:ReportViewer>
                        </asp:Panel>
                    </td>
                </tr>
            </table>

        </div>

    </form>
</body>
</html>
