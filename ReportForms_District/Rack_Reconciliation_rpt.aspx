<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Rack_Reconciliation_rpt.aspx.vb" Inherits="District_food_rpt_Rack_Reconciliation_rpt" %>

 
 
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Rack Reconciliation Report</title>


<script type = "text/javascript" src="../calendar_eu.js"></script><link rel="stylesheet" href="../calendar.css" /><script type="text/javascript" src="../js/chksql.js"></script></head>  
<body>
    <form id="form1" runat="server">
        <table border="1" cellpadding="0" cellspacing="0" style="border-style:double; border-width:3; padding:1; BORDER-COLLAPSE: collapse; border-color :Maroon  ; width: 849px; background-color: #ece9d8;" id="">
            <tr>
                <td align="center" colspan="5" style="font-weight: bolder; font-size: 15pt; color: teal">
                    Rack Reconciliation Report</td>
            </tr>
            <tr>
                <td style="vertical-align: middle; width: 228px; height: 24px">
                    District Logged In</td>
                <td style="width: 100px; height: 24px">
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="MediumBlue" Width="143px"></asp:Label></td>
                <td align="right" style="vertical-align: middle; width: 93px; height: 24px">
                </td>
                <td align="left" style="height: 24px">
                    </td>
                <td align="left" style="width: 417px; height: 24px">
                    </td>
            </tr>
            <tr>
                <td style="width: 228px; height: 23px; vertical-align: middle;">
                    Rack Number:</td>
                <td style="width: 100px; height: 23px;">
                    <asp:DropDownList ID="ddlrackno" runat="server" Width="138px">
                    </asp:DropDownList></td>
                <td align="right" style="width: 93px; height: 23px; vertical-align: middle;">
                    &nbsp;
                </td>
                <td style="width: 92px; height: 23px;">
                    &nbsp;</td>
                <td align="right" style="width: 417px; height: 23px">
                    <asp:LinkButton ID="LinkButton1" runat="server" style="font-weight: 700" Font-Bold="False" Font-Size="Large">Back</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 228px; height: 25px;">
                    </td>
                <td style="width: 100px; height: 25px;">
                    </td>
                <td style="width: 93px; height: 25px;">
                </td>
                <td style="width: 92px; height: 25px;">
                </td>
                <td align="right" style="width: 417px; height: 25px;">
                    <asp:Button ID="Button1" runat="server" Text="View Report" /></td>
            </tr>
            <tr>
                <td colspan="5">
                    &nbsp;<asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label></td>
            </tr>
        </table>
        &nbsp;&nbsp;
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="852px">
        </rsweb:ReportViewer>
    </form>
</body>
</html>

