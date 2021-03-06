<%@ Page Language="VB" AutoEventWireup="false" CodeFile="graphical_DO_liftRpt.aspx.vb" Inherits="liftingDetails_DO_divisionRpt" %>
 
 
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Delivery Order Report</title>


<script type = "text/javascript" src="../calendar_eu.js"></script><link rel="stylesheet" href="../calendar.css" /><script type="text/javascript" src="../js/chksql.js"></script></head>  
<body>
    <form id="form1" runat="server">
        <table border="1" cellpadding="0" cellspacing="0" style="border-style:double; border-width:3; padding:1; BORDER-COLLAPSE: collapse; border-color :Maroon  ; width: 808px; background-color: #ece9d8;" id="">
            <tr>
                <td align="center" colspan="5" style="font-weight: bolder; font-size: 15pt; color: teal; height: 23px;">
                    Graphical Representation of Lifted
                    Delivery Order</td>
            </tr>
            <tr>
                <td style="vertical-align: middle; width: 134px;">
                    Division Logged In</td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 24px">
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="#0000C0"></asp:Label></td>
                <td align="left" style="height: 24px">
                    </td>
                <td align="right" style="width: 417px; height: 24px">
                    <asp:LinkButton ID="LinkButton1" runat="server">Back</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="vertical-align: middle; width: 134px; height: 23px">
                    Month</td>
                <td style="width: 100px; height: 23px">
                    <asp:DropDownList ID="ddlalotmm" runat="server" Width="136px">
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
                <td align="right" style="vertical-align: middle; width: 93px; height: 23px">
                    Year&nbsp;
                </td>
                <td style="width: 92px; height: 23px">
                    <asp:DropDownList ID="ddlallotyear" runat="server">
                    </asp:DropDownList>
                </td>
                <td align="right" style="width: 417px; height: 23px">
                </td>
            </tr>
            <tr>
                <td style="width: 134px; height: 23px; vertical-align: middle;">
                    </td>
                <td style="width: 100px; height: 23px;">
                    &nbsp;</td>
                <td align="right" style="width: 110px; height: 23px; vertical-align: middle;">
                    &nbsp;
                </td>
                <td style="width: 92px; height: 23px;">
                    &nbsp;</td>
                <td style="width: 417px; height: 23px;" align="right">
                    <asp:Button ID="Button1" runat="server" Text="View Report" /></td>
            </tr>
        </table>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="470px" ProcessingMode="Remote" Width="808px" Font-Names="Verdana" Font-Size="8pt" ShowCredentialPrompts="False">
            <ServerReport ReportServerUrl=""  />
        </rsweb:ReportViewer>
        &nbsp;
    </form>
</body>
</html>
