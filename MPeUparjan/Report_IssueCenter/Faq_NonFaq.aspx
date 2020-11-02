<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Faq_NonFaq.aspx.cs" Inherits="Report_IssueCenter_Faq_NonFaq" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>Faq List</title>
 <script type = "text/javascript" src="../calendar_eu.js"></script>  
	<link rel="stylesheet" href="../calendar.css" />
     <style type="text/css">
         .style1
         {
             height: 13px;
         }
         .style2
         {
             height: 23px;
             width: 105px;
         }
         .style3
         {
             height: 25px;
             width: 105px;
         }
     </style>
</head>
<body>
    <form id="form1" runat="server">
   <table border="1" cellpadding="0" cellspacing="0" style="border-style:double; border-width:3; padding:1; BORDER-COLLAPSE: collapse; border-color :Maroon  ; background-color: #ece9d8;" id="">
            <tr>
                <td align="center" colspan="5" 
                    style="font-weight: bolder; font-size: 15pt; color: teal" class="style1">
                    Quantity Receiving from Procurement</td>
            </tr>
            <tr>
                <td style="vertical-align: middle; width: 315px; height: 23px">
                    From Date</td>
                <td style="height: 23px; width: 273px;">
                     <asp:TextBox ID="tx_date" runat="server" Width="133px"></asp:TextBox>
                                <script type  ="text/javascript">
                                                	new tcal ({
				'formname': 'form1',
				'controlname': 'tx_date'
	    });
	     </script>
                </td>
                <td align="right" style="vertical-align: middle; " class="style2">
                    To Date</td>
                <td style="height: 23px; width: 214px;">
                                
                    <asp:TextBox ID="tx_todate" runat="server" Width="133px"></asp:TextBox>
                                
                </td>
                <td align="right" style="width: 417px; height: 23px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="vertical-align: middle; width: 315px; height: 23px">
                    Select Purchase Center</td>
                <td style="height: 23px; " colspan="2">
                     <asp:DropDownList ID="ddlsociety" runat="server" Height="16px" Width="300px">
                     </asp:DropDownList>
                </td>
                <td style="height: 23px; width: 214px;">
                                
                    &nbsp;</td>
                <td align="right" style="width: 417px; height: 23px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 315px; height: 25px;">
                    </td>
                <td style="width: 273px; height: 25px;">
                    <asp:Button ID="Button1" runat="server" Text="View Report" 
                        onclick="Button1_Click" />
                    </td>
                <td class="style3">
                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="False" Font-Size="Large"
                        NavigateUrl="~/IssueCenter/frmReports.aspx">Back</asp:HyperLink>
                </td>
                <td style="width: 214px; height: 25px;">
                </td>
                <td align="right" style="width: 417px; height: 25px;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="5">
                    &nbsp;</td>
            </tr>
        </table>
        &nbsp;&nbsp;
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1100px">
        </rsweb:ReportViewer>
    </form>
</body>
</html>
