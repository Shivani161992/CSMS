<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaddyMillingAgreement_Rpt.aspx.cs" Inherits="ReportForms_District_PaddyMillingAgreement_Rpt" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style type="text/css">
        .auto-style1 {
            font-size: large;
            font-weight: bold;
            color: #0000FF;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>

            <table border="1" cellpadding="0" cellspacing="0" style="border-style: double; border-width: 3; padding: 1; border-collapse: collapse; border-color: Maroon; width: 100%; background-color: lavender;"
                align="center">
                <tr style="text-align: center">
                    <td colspan="2" class="auto-style1">Crop Year Wise, Paddy Milling Details</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" style="font-weight: 700" Text="Crop Year"></asp:Label>
                        &nbsp;
                            <asp:DropDownList ID="ddlCropYear" runat="server" Width="173px">
                            </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblDistName" runat="server" style="font-weight: 700" Text="Dist. Name" Visible="False"></asp:Label>
&nbsp;
                        <asp:DropDownList ID="ddlDistName" runat="server" Visible="False" Width="150px">
                        </asp:DropDownList>
&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnReport" runat="server" Text="View Report" Font-Bold="True" OnClick="btnReport_Click" />
                    </td>
                    <td style="text-align: center">
                        <asp:LinkButton ID="lnkbtnBack" runat="server" Font-Bold="True" ForeColor="#CC3300" Style="text-align: center" OnClick="lnkbtnBack_Click">Back</asp:LinkButton></td>
                </tr>
                <tr>
                    <td colspan="2">
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
