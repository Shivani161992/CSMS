<%@ Page Language="VB" AutoEventWireup="false" CodeFile="multiple_DO_issueCentrewise_rpt.aspx.vb" Inherits="multiple_DO_issueCentrewise_rpt" %>
 
 
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Delivery Order Report</title>

<script type = "text/javascript" src="../calendar_eu.js"></script>  
	<link rel="stylesheet" href="../calendar.css" />
    <style type="text/css">
        .auto-style1
        {
            width: 144px;
        }
        .auto-style2
        {
            height: 47px;
            width: 144px;
        }
        .auto-style3
        {
            width: 648px;
            height: 47px;
        }
        .auto-style4
        {
            width: 100px;
            height: 47px;
        }
        .auto-style5
        {
            width: 919px;
            height: 47px;
        }
        .auto-style6
        {
            width: 417px;
            height: 47px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table border="1" cellpadding="0" cellspacing="0" style="border-style:double; border-width:3; padding:1; BORDER-COLLAPSE: collapse; border-color :Maroon  ; width: 800px; background-color: #ece9d8;" id="" Width="1100px">
            <tr>
                <td align="center" colspan="5" style="font-weight: bolder; font-size: 15pt; color: teal">
                    Multiple
                    Delivery Order Report</td>
            </tr>
            <tr>
                <td style="vertical-align: middle; width: 648px; height: 23px">
                    District Logged In</td>
                <td style="width: 100px; height: 23px">
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="MediumBlue"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 919px; height: 23px">
                    Issue Centre Logged In</td>
                <td align="left" class="auto-style1">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="MediumBlue"></asp:Label></td>
                <td align="right" style="width: 417px; height: 23px">
                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="False" Font-Size="Large"
                        NavigateUrl="~/IssueCenter/frmReports.aspx">Back</asp:HyperLink></td>
            </tr>
            <tr>
                <td style="vertical-align: middle;" class="auto-style3">
                    From Date</td>
                <td class="auto-style4">
                    <asp:TextBox ID="tx_from_date" runat="server" Width="133px"></asp:TextBox>
                                <script type  ="text/javascript">
                                                	new tcal ({
				'formname': 'form1',
				'controlname': 'tx_from_date'
	    });
	     </script>
                </td>
                <td align="right" style="vertical-align: middle;" class="auto-style5">
                    To Date&nbsp;
                </td>
                <td class="auto-style2">
                 <asp:TextBox ID="tx_to_date" runat="server" Width="125px"></asp:TextBox>
                                <script type  ="text/javascript">
                                                	new tcal ({
				'formname': 'form1',
				'controlname': 'tx_to_date'
	    });
	     </script>
                </td>
                <td align="right" class="auto-style6">
                    <asp:Button ID="Button1" runat="server" Text="View Report" /></td>
            </tr>
            <tr>
                <td colspan="5">
                    &nbsp;</td>
            </tr>
        </table>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" ProcessingMode="Remote" Font-Names="Verdana" Font-Size="8pt" ShowCredentialPrompts="False" Width="1100px">
            <ServerReport ReportServerUrl=""  />
        </rsweb:ReportViewer>
        &nbsp;
    </form>
</body>
</html>
