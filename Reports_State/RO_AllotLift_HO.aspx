<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RO_AllotLift_HO.aspx.vb" Inherits="Reports_State_RO_AllotLift_HO" title="Release Order Register" %>

 
 
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Release Order Registor</title>


<script type = "text/javascript" src="../calendar_eu.js"></script><link rel="stylesheet" href="../calendar.css" /><script type="text/javascript" src="../js/chksql.js"></script></head>  
<body>
    <form id="form1" runat="server">
        <table border="1" cellpadding="0" cellspacing="0" style="border-style:double; border-width:3; padding:1; BORDER-COLLAPSE: collapse; border-color :Maroon  ; width: 911px; background-color: #ece9d8;" id="">
            <tr>
                <td align="center" colspan="5" style="font-weight: bolder; font-size: 15pt; color: teal">
                    Release Order Register</td>
            </tr>
            <tr>
                <td style="vertical-align: middle; width: 228px; height: 23px">
                    </td>
                <td align="right" colspan="3">
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="Maroon">Head Office</asp:Label></td>
                <td align="left" style="width: 417px; height: 23px">
                    </td>
            </tr>
            <tr>
                <td style="width: 228px; height: 23px; vertical-align: middle;">
                    Month</td>
                <td style="width: 100px; height: 23px;">
                  <asp:DropDownList ID="ddlalotmm" runat="server"  Width ="136px">
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
                <td align="right" style="width: 93px; height: 23px; vertical-align: middle;">
                    Year&nbsp;
                </td>
                <td style="width: 92px; height: 23px;">
                     <asp:DropDownList ID="ddlallotyear" runat="server">
                                </asp:DropDownList>
                </td>
                <td style="width: 417px; height: 23px;" align="right">
                    <asp:LinkButton ID="LinkButton1" runat="server">Back</asp:LinkButton></td>
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
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="440px" ProcessingMode="Remote" Width="912px" Font-Names="Verdana" Font-Size="8pt" ShowCredentialPrompts="False">
            <ServerReport ReportServerUrl=""  />
        </rsweb:ReportViewer>
        &nbsp;
    </form>
</body>
</html>



