<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Rack_frmProcurement.aspx.cs" Inherits="ReportForms_District_Rpt_Rack_frmProcurement" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Received at Rack</title>
    <style type="text/css">
        .auto-style1 {
            width: 12%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="1" cellpadding="0" cellspacing="0" height="700px" width="100%">
                <tr>
                    <td valign="top">
                        <table cellpadding="0" cellspacing="0" style="background-color: #ecf5d5; width: 100%; border: double; border-color: #868f6f">
                            <tr>
                                <td colspan="7" style="font-weight: bold; font-size: 9pt; color: #595f4a; font-family: Verdana; height: 20px; text-align: center">
                                    <span style="font-size: 11pt">जिले में उपार्जन केन्द्र से रेल रैक के द्वारा भेजी गयी की जानकारी</span></td>
                            </tr>
                            <tr>
                                <td style="width: 178px">
                                    <span style="font-size: 10pt; color: #3300ff">



                                        <asp:Label ID="Label2" runat="server" Text="कमोडिटी" Width="150px"
                                            Style="text-align: center"></asp:Label>



                                    </span>
                                </td>
                                <td class="style3" style="font-weight: bold; width: 84px; font-family: Verdana; text-align: center">
                                    <span style="font-size: 10pt; color: #3300ff">



                                        <asp:DropDownList ID="ddlcrop" runat="server"
                                            Width="150px" Visible="true" OnSelectedIndexChanged="ddlcrop_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </span>
                                </td>
                                <td class="style1" style="font-weight: bold; font-size: 9pt; color: #595f4a; font-family: Verdana; text-align: center"></td>
                                <td style="font-weight: bold; font-size: 9pt; color: #595f4a; font-family: Verdana; text-align: right" class="auto-style1">Select Rack Number</td>
                                <td class="style4" style="font-weight: bold; font-size: 9pt; color: #595f4a; font-family: Verdana; text-align: left">
                                    <asp:DropDownList ID="ddlracknumber" runat="server" Width="250px" >
                                    </asp:DropDownList>
                                </td>
                                <td class="style7" style="font-weight: bold; font-size: 9pt; width: 65px; color: #595f4a; font-family: Verdana; text-align: left">
                                    <span style="font-size: 10pt">
                                        <asp:Button ID="Button1" runat="server" Text="Generate Report" Width="121px"
                                            Height="26px" OnClick="Button1_Click" /></span></td>
                                <td style="font-weight: bold; font-size: 9pt; color: #595f4a; font-family: Verdana; height: 20px; text-align: right">
                                    <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="True" Font-Size="10pt" Width="116px" OnClick="LinkButton1_Click">पिछले पृष्ठ पर जाये</asp:LinkButton></td>
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
        </div>
    </form>
</body>
</html>
