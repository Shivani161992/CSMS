<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PDS_TruckMovmt.aspx.cs" Inherits="Reports_State_PDS_TruckMovmt" %>

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
        <div>

            <table border="1" cellpadding="0" cellspacing="0" style="border-style: double; border-width: 3; padding: 1; border-collapse: collapse; border-color: Maroon; width: 100%; background-color: lavender;"
                align="center">
                <tr style="text-align: center">
                    <td colspan="9" class="auto-style1" style="color: #CC6600; background-color:#FFCCFF">PDS Truck Movement (Month Wise)</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Style="font-weight: 700; font-size: large;" Text="Commodity"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlComdty" runat="server" Width="173px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="13">Paddy-Common</asp:ListItem>
                            <asp:ListItem Value="3">Rice-Raw-Common</asp:ListItem>
                            <asp:ListItem Value="22">Wheat(PSS)</asp:ListItem>
                            <asp:ListItem Value="12">Maizei(Makka)</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Style="font-weight: 700; font-size: large;" Text="CropYear"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCropYear" runat="server" Width="173px">

                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Style="font-weight: 700; font-size: large;" Text="Month"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMonth" runat="server" Width="173px">
                            <asp:ListItem Value="01">January</asp:ListItem>
                            <asp:ListItem Value="02">February</asp:ListItem>
                            <asp:ListItem Value="03">March</asp:ListItem>
                            <asp:ListItem Value="04">April</asp:ListItem>
                            <asp:ListItem Value="05">May</asp:ListItem>
                            <asp:ListItem Value="06">June</asp:ListItem>
                            <asp:ListItem Value="07">July</asp:ListItem>
                            <asp:ListItem Value="08">August</asp:ListItem>
                            <asp:ListItem Value="09">September</asp:ListItem>
                            <asp:ListItem Value="10">October</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">December</asp:ListItem>

                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnReport" runat="server"  Text="View Report" OnClick="btnReport_Click" style="font-weight: 700;  font-size: small;" CssClass="ButtonClass" />
                    </td>
               
                    <td style="text-align: center">
                        <asp:LinkButton ID="lnkbtnBack" runat="server" Font-Bold="True" ForeColor="#CC3300" Style="text-align: center" OnClick="lnkbtnBack_Click">Back</asp:LinkButton></td>
                </tr>
                <tr>
                    <td colspan="9">
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
