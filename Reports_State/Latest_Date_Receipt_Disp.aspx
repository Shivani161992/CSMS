<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Latest_Date_Receipt_Disp.aspx.vb" Inherits="Reports_State_Latest_Date_Receipt_Disp" %>

 
 
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Summary Report(Issue Center Wise)</title>


<script type = "text/javascript" src="../calendar_eu.js"></script><link rel="stylesheet" href="../calendar.css" /><script type="text/javascript" src="../js/chksql.js"></script></head>  
<body>
    <form id="form1" runat="server">
        <table border="1" cellpadding="0" cellspacing="0" style="border-style:double; border-width:3; padding:1; BORDER-COLLAPSE: collapse; border-color :Maroon  ; width: 823px; background-color: #ece9d8;" id="">
            <tr>
                <td align="center" colspan="5" style="font-weight: bolder; font-size: 15pt; color: teal">
                    Madhya Pradesh State Civil Supplies Corporation
                </td>
            </tr>
            <tr>
                <td style="vertical-align: middle; width: 228px; height: 23px">
                    </td>
                <td align="center" colspan="4" style="height: 23px; color: teal;">
                    <strong>Summary Report(Issue Center Wise) &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; </strong>
                </td>
            </tr>
            <tr>
                <td style="width: 228px; height: 23px; vertical-align: middle;">
                    </td>
                <td style="width: 100px; height: 23px;">
                    &nbsp;</td>
                <td align="right" style="width: 93px; height: 23px; vertical-align: middle;">
                    &nbsp;
                </td>
                <td style="width: 92px; height: 23px; color: maroon;">
                    &nbsp;Head Office
                </td>
                <td style="width: 417px; height: 23px;" align="right">
                    <asp:LinkButton ID="LinkButton1" runat="server" Width="46px">Back</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 228px; height: 25px;" align="right">
                    </td>
                <td style="width: 100px; height: 25px;">
                    <asp:CheckBox ID="chkan" runat="server" Visible="False" /></td>
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
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="440px" ProcessingMode="Remote" Width="824px" Font-Names="Verdana" Font-Size="8pt" ShowCredentialPrompts="False">
            <ServerReport ReportServerUrl=""  />
        </rsweb:ReportViewer>
        &nbsp;
    </form>
</body>
</html>

