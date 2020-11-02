<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StAllot_lift_mpsc.aspx.vb" Inherits="StAllot_lift_mpsc" %>
 
<%@ Register Assembly="CustomControlFreak" Namespace="CustomControlFreak" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Allotment & Lifting</title>


</head>
<body>
    <form id="form1" runat="server">
        <table border="1" cellpadding="0" cellspacing="0" style="border-style:double; border-width:3; padding:1; BORDER-COLLAPSE: collapse; border-color :Maroon  ; width: 823px; background-color: #ece9d8;" id="">
            <tr>
                <td align="center" colspan="5" style="font-weight: bolder; font-size: 15pt; color: teal">
                   Allotment and Lifting Details</td>
            </tr>
            <tr>
                <td style="width: 84px">
                    District</td>
                <td style="width: 84px">
                    <asp:DropDownList ID="ddl_dist" runat="server" Width="127px">
                    </asp:DropDownList></td>
                <td style="width: 77px" align="right">
                    Commodity</td>
                <td style="width: 6px">
                    <asp:DropDownList ID="ddl_commodity" runat="server" ValidationGroup="1">
                        <asp:ListItem Value="rice">Rice</asp:ListItem>
                        <asp:ListItem Value="wheat">Wheat</asp:ListItem>
                        <asp:ListItem Value="sks">Sugar, Kerosene &amp; Salt</asp:ListItem>
                    </asp:DropDownList></td>
                <td align="right">
                    <asp:LinkButton ID="LinkButton1" runat="server">Back</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 84px">
                    Month</td>
                <td style="width: 84px">
                    <asp:DropDownList ID="ddl_month" runat="server" AutoPostBack="True" Width="125px">
                        <asp:ListItem Value="1">January</asp:ListItem>
                        <asp:ListItem Value="2">February</asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4">April</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">June</asp:ListItem>
                        <asp:ListItem Value="7">July</asp:ListItem>
                        <asp:ListItem Value="8">August</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList></td>
                <td style="width: 77px;" align="right">
                    Year</td>
                <td style="width: 6px">
                    <asp:DropDownList ID="ddl_year" runat="server" Width="94px">
                    </asp:DropDownList></td>
                <td align="left">
                    <asp:Button ID="Button1" runat="server" Text="View Report" /></td>
            </tr>
        </table>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="440px" ProcessingMode="Remote" Width="824px" Font-Names="Verdana" Font-Size="8pt" ShowCredentialPrompts="False">
            <ServerReport ReportServerUrl=""  />
        </rsweb:ReportViewer>
    </form>
</body>
</html>


