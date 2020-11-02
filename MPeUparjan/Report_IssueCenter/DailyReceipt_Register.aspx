<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DailyReceipt_Register.aspx.cs" Inherits="Report_IssueCenter_DailyReceipt_Register" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Receipt Register</title>
     <script type = "text/javascript" src="../calendar_eu.js"></script>  
	<link rel="stylesheet" href="../calendar.css" />
</head>
<body>
    <form id="form1" runat="server">
    <table border="1" cellpadding="0" cellspacing="0" style="border-style:double; border-width:3; padding:1; BORDER-COLLAPSE: collapse; border-color :Maroon  ; width: 1000px; background-color: #ece9d8;" id="">
        <tr>
            <td align="center" colspan="5" style="font-weight: bolder; font-size: 15pt; color: teal">
                    &nbsp;Receipt Register Procurement 2016</td>
        </tr>
        <tr>
            <td style="vertical-align: middle; width: 228px; height: 23px">
                    District Logged In</td>
            <td style="width: 166px; height: 23px">
                <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="MediumBlue"></asp:Label>
            </td>
            <td align="right" style="vertical-align: middle; width: 131px; height: 23px">
                    Issue Center</td>
            <td align="left" style="width: 192px">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="MediumBlue"></asp:Label>
            </td>
            <td align="left" style="width: 417px; height: 23px">
                <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="False" Font-Size="Large"
                        NavigateUrl="~/IssueCenter/frmReports.aspx">Back</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td style="width: 228px; height: 23px; vertical-align: middle;">
                    From Date</td>
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
                    To Date&nbsp;
                </td>
            <td style="width: 192px; height: 23px;">
                <asp:TextBox ID="tx_to_date" runat="server" Width="135px"></asp:TextBox>
                                <script type  ="text/javascript">
                                                	new tcal ({
				'formname': 'form1',
				'controlname': 'tx_to_date'
	    });
	     </script>
                </td>
            <td style="width: 417px; height: 23px;" align="right">
                <asp:Button ID="Button1" runat="server" Text="View Report" 
                    onclick="Button1_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="5">
                    &nbsp;<asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label></td>
        </tr>
    </table>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="440px" ProcessingMode="Remote" Width="1000px" Font-Names="Verdana" Font-Size="8pt" ShowCredentialPrompts="False">
        <ServerReport ReportServerUrl=""  />
    </rsweb:ReportViewer>
    </form>
</body>
</html>
