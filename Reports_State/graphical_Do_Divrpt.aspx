<%@ Page Language="VB" AutoEventWireup="false" CodeFile="graphical_Do_Divrpt.aspx.vb" Inherits="graphical_Do_Divrpt" %>
 
 
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Delivery Order Report</title>

<script type = "text/javascript" src="../calendar_eu.js"></script><link rel="stylesheet" href="../calendar.css" /><script type="text/javascript" src="../js/chksql.js"></script></head>   
    <form id="form1" runat="server">
        <table border="1" cellpadding="0" cellspacing="0" style="border-style:double; border-width:3; padding:1; BORDER-COLLAPSE: collapse; border-color :Maroon  ; width: 808px; background-color: #ece9d8;" id="">
            <tr>
                <td align="center" colspan="5" style="font-weight: bolder; font-size: 15pt; color: teal; height: 23px;">
                    Graphical Representation of
                    Delivery Order</td>
            </tr>
            <tr>
                <td style="vertical-align: middle; width: 80px;">
                    Division</td>
                <td style="width: 100px; height: 24px">
                    <asp:DropDownList ID="ddl_division" runat="server" Width="160px">
                    </asp:DropDownList></td>
                <td align="right" style="vertical-align: middle; width: 202px; height: 24px">
                </td>
                <td align="left" style="height: 24px; width: 1363px;">
                    </td>
                <td align="right" style="width: 417px; height: 24px">
                    <asp:LinkButton ID="LinkButton1" runat="server">Back</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="vertical-align: middle; width: 80px; height: 23px">
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
                <td align="right" style="vertical-align: middle; width: 202px; height: 23px">
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
                <td style="height: 23px; vertical-align: middle;" align="left" colspan="4">
                    <asp:Label ID="Label2" runat="server"></asp:Label></td>
                <td style="width: 417px; height: 23px;" align="right">
                    <asp:Button ID="Button1" runat="server" Text="View Report" /></td>
            </tr>
        </table>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="440px" ProcessingMode="Remote" Width="808px" Font-Names="Verdana" Font-Size="8pt" ShowCredentialPrompts="False">
            <ServerReport ReportServerUrl=""  />
        </rsweb:ReportViewer>
        &nbsp;
    </form>
</body>
</html>
