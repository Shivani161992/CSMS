<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DailyEntryIssueCenterWise.aspx.vb" Inherits="Report_IssueCenter_DailyEntryIssueCenterWise" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Daily Entry Status</title>
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
        <table border="1" cellpadding="0" cellspacing="0" style="border-style:double; border-width:3; padding:1; BORDER-COLLAPSE: collapse; border-color :Maroon  ; width: 823px; background-color: #ece9d8;" id="">
            <tr>
                <td align="center" colspan="5" style="font-weight: bolder; font-size: 15pt; color: teal">
                    Daily Entry Status</td>
            </tr>
            <tr>
                <td style="vertical-align: middle; width: 228px; height: 23px">
                    </td>
                <td style="width: 166px; height: 23px">
                                
                    </td>
                <td align="right" style="vertical-align: middle; width: 131px; height: 23px">
                    Issue Center</td>
                <td align="left" style="width: 192px">
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="MediumBlue"></asp:Label></td>
                <td align="right" style="width: 417px; height: 23px">
                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="False" Font-Size="Large"
                        NavigateUrl="~/IssueCenter/frmReports.aspx">Back</asp:HyperLink></td>
            </tr>
            <tr>
                <td style="width: 228px; height: 23px; vertical-align: middle;" align="right">
                    FromDate</td>
                <td style="width: 166px; height: 23px;">
                <asp:TextBox ID="tx_from_date" runat="server" Width="117px"></asp:TextBox>
                <script type  ="text/javascript">
                                                	new tcal ({
				'formname': 'form1',
				'controlname': 'tx_from_date'
	    });
	     </script>
                </td>
                <td align="right" style="width: 131px; height: 23px; vertical-align: middle;">
                    ToDate&nbsp;
                </td>
                <td style="width: 192px; height: 23px;">
                    <asp:TextBox ID="tx_to_date" runat="server" Width="117px"></asp:TextBox>
                    <script type  ="text/javascript">
                                                	new tcal ({
				'formname': 'form1',
				'controlname': 'tx_to_date'
	    });
	     </script>
                    </td>
                <td style="width: 417px; height: 23px;" align="right">
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
