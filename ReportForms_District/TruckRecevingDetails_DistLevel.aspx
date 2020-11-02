<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TruckRecevingDetails_DistLevel.aspx.cs" Inherits="ReportForms_District_TruckRecevingDetails_DistLevel" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Truck Receiving</title>
</head>
<body>
    <form id="form1" runat="server">
        <table border="1" cellpadding="0" cellspacing="0" height="700px" width="100%">
            <tr>
                <td valign="top">
                    <table cellpadding="0" cellspacing="0" style="background-color: #ecf5d5; width: 100%; border: double; border-color: #868f6f">
                        <tr>
                            <td style="width: 228px"></td>
                            <td colspan="2" style="font-weight: bold; font-size: medium; font-family: Verdana; text-align: center">उपार्जन केंद्र से ट्रक प्राप्ति की जानकारी</td>
                            <td style="font-weight: bold; font-size: 9pt; color: #595f4a; font-family: Verdana; text-align: left; width: 132px;"></td>
                            <td style="font-weight: bold; font-size: 9pt; color: #595f4a; font-family: Verdana; text-align: center"></td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold; font-family: Verdana; text-align: center; font-size: medium; height: 24px;">
                                <span style="font-size: 10pt; color: #3300ff">



                                    <asp:Label ID="Label2" runat="server" Text="कमोडिटी" Width="150px"
                                        Style="text-align: center"></asp:Label>



                                </span>
                            </td>
                            <td style="font-weight: bold; font-size: 9pt; color: #595f4a; font-family: Verdana; text-align: left; height: 24px;">
                                <span style="font-size: 10pt; color: #3300ff">



                                    <asp:DropDownList ID="ddlcrop" runat="server"
                                        Width="150px" Visible="true">
                                    </asp:DropDownList>
                                </span>
                            </td>
                            <td style="font-weight: bold; font-size: 9pt; color: #595f4a; font-family: Verdana; text-align: left; height: 24px;">
                                <span style="font-size: 10pt">
                                    <asp:Button ID="Button1" runat="server" Text="Generate Report" Width="121px"
                                        Height="26px" OnClick="Button1_Click" /></span>
                            </td>

                            <td style="font-weight: bold; font-size: 9pt; color: #595f4a; font-family: Verdana; text-align: center; height: 24px;">
                                <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="True" Font-Size="10pt" OnClick="LinkButton1_Click1">पिछले पृष्ठ पर जाये</asp:LinkButton></td>
                        </tr>
                    </table>

                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="650px" ProcessingMode="Remote" Width="100%" Font-Names="Verdana" Font-Size="8pt" ShowCredentialPrompts="False" SizeToReportContent="True" BackColor="#e9f3ce" BorderColor="#E9F3CE"
                        LinkDisabledColor="Black" LinkActiveColor="Yellow" Style="margin-left: 0px; margin-right: 0px;"
                        ShowBackButton="true" EnableTheming="true">
                        <ServerReport ReportServerUrl="" />
                    </rsweb:ReportViewer>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
