<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ACNO_details.aspx.vb" Inherits="ACNO_details" %>

 
 
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Acceptence Note Details</title>
 <script type = "text/javascript" src="../calendar_eu.js"></script>  
	<link rel="stylesheet" href="../calendar.css" />
</head>
<body>
    <form id="form1" runat="server">
        <table border="1" cellpadding="0" cellspacing="0" style="border-style:double; border-width:3; padding:1; BORDER-COLLAPSE: collapse; border-color :Maroon  ; width: 1100px; background-color: #ece9d8;" id="">
            <tr>
                <td align="center" colspan="5" style="font-weight: bolder; font-size: 15pt; color: teal">
                    Acceptence Note Details</td>
            </tr>
            <tr>
                <td style="vertical-align: middle; width: 315px; height: 23px">
                    District Logged In</td>
                <td style="width: 273px; height: 23px">
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="MediumBlue"></asp:Label></td>
                <td align="left" colspan="2">
                    IssueCenter Logged In</td>
                <td align="left" style="width: 417px; height: 23px">
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="MediumBlue"></asp:Label></td>
            </tr>
            <tr>
                <td style="vertical-align: middle; width: 315px; height: 23px">
                    From Acceptance Date</td>
                <td style="height: 23px; width: 273px;">
                     <asp:TextBox ID="tx_from_date" runat="server" Width="133px"></asp:TextBox>
                                <script type  ="text/javascript">
                                                	new tcal ({
				'formname': 'form1',
				'controlname': 'tx_from_date'
	    });
	     </script>
                </td>
                <td align="right" style="vertical-align: middle; width: 251px; height: 23px">
                    To Acceptance Date</td>
                <td style="height: 23px; width: 214px;">
                     <asp:TextBox ID="tx_to_date" runat="server" Width="125px"></asp:TextBox>
                                <script type  ="text/javascript">
                                                	new tcal ({
				'formname': 'form1',
				'controlname': 'tx_to_date'
	    });
	     </script>
                </td>
                <td align="right" style="width: 417px; height: 23px">
                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="False" Font-Size="Large"
                        NavigateUrl="~/IssueCenter/frmReports.aspx">Back</asp:HyperLink></td>
            </tr>
            <tr>
                <td style="width: 315px; height: 25px;">
                    Select Crop Year</td>
                <td style="height: 25px;" colspan="3">
              <asp:DropDownList ID="ddlcropyear" runat="server"  Width="140px" >
                  <asp:ListItem>2016-2017</asp:ListItem>
                  <asp:ListItem>2015-2016</asp:ListItem>
                                               
            </asp:DropDownList>
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
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1100px">
        </rsweb:ReportViewer>
    </form>
</body>
</html>
