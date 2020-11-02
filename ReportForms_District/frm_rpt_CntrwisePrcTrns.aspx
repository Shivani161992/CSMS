 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_rpt_CntrwisePrcTrns.aspx.cs" Inherits="ReportForms_District_TestReprt" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Daily Entry DistriWise</title>
 <script type = "text/javascript" src="../calendar_eu.js"></script>  
	<link rel="stylesheet" href="../calendar.css" />
<script type="text/javascript">
function CheckCalDate(tx){
var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode > 0))
{
alert('Please Click on Calander Controll to Enter Date');
event.cancelBubble = true;
event.returnValue = false;
}
}
</script>
</head>
<body>
    <form id="form1" runat="server">
        <table border="1" cellpadding="0" cellspacing="0" style="border-style:double; border-width:3; padding:1; BORDER-COLLAPSE: collapse; border-color :Maroon  ; width: 100%; background-color: #ece9d8;" id="">
            <tr>
                <td align="center" colspan="5" style="font-weight: bolder; font-size: 15pt; color: teal">
                    eUparjan Procurement of Wheat Rabi 2013-14
                </td>
            </tr>
            <tr>
                <td style="vertical-align: middle; width: 228px; height: 23px">
                    District Logged In</td>
                <td style="width: 166px; height: 23px">
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="MediumBlue"></asp:Label></td>
                <td align="right" style="vertical-align: middle; width: 131px; height: 23px">
                    <asp:Button ID="Button1" runat="server" Text="View Report"  OnClick="Button1_Click"/></td>
                <td align="left" style="width: 192px">
                    </td>
                <td align="left" style="width: 417px; height: 23px">
                    <%--<asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="False" Font-Size="Large"
                        NavigateUrl="~/District/frmReports.aspx">Back</asp:HyperLink>--%>
                        
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Back</asp:LinkButton></td>
            </tr>
            <tr>
                <td colspan="5">
                    &nbsp;<asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label></td>
            </tr>
        </table>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="600px" ZoomMode="PageWidth" ProcessingMode="Remote" Width="100%" Font-Names="Verdana" Font-Size="8pt" ShowCredentialPrompts="False">
            <ServerReport ReportServerUrl="" />
        </rsweb:ReportViewer>
        &nbsp;
    </form>
</body>
</html>
