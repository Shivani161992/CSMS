﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Gdnwise_ReceiptProc.aspx.cs" Inherits="Report_IssueCenter_Gdnwise_ReceiptProc" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>Check List</title>
 
 <script type = "text/javascript" src="../calendar_eu.js"></script>  
	<link rel="stylesheet" href="../calendar.css" />
<script type="text/javascript">
    function CheckCalDate(tx) {
        var AsciiCode = event.keyCode;
        var txt = tx.value;
        var txt2 = String.fromCharCode(AsciiCode);
        var txt3 = txt2 * 1;
        if ((AsciiCode > 0)) {
            alert('Please Click on Calander Controll to Enter Date');
            event.cancelBubble = true;
            event.returnValue = false;
        }
    }
</script>
 
 
     <style type="text/css">
         .style1
         {
             height: 23px;
             width: 205px;
         }
         .style2
         {
             height: 25px;
             width: 205px;
         }
     </style>
</head>
<body>
    <form id="form1" runat="server">
    <table border="1" cellpadding="0" cellspacing="0" style="border-style:double; border-width:3; padding:1; BORDER-COLLAPSE: collapse; border-color :Maroon  ; width: 1100px; background-color: #ece9d8;" id="">
            <tr>
                <td align="center" colspan="5" style="font-weight: bolder; font-size: 15pt; color: teal">
                    Details Receiving from Procurement</td>
            </tr>
            <tr>
                <td style="vertical-align: middle; width: 315px; height: 23px">
                    From&nbsp; Date</td>
                <td class="style1">
                     <asp:TextBox ID="tx_date" runat="server" Width="133px"></asp:TextBox>
                                <script type  ="text/javascript">
                                                	new tcal ({
				'formname': 'form1',
				'controlname': 'tx_date'
	    });
	     </script>
                </td>
                <td align="right" style="vertical-align: middle; height: 23px">
                    To Date </td>
                <td style="height: 23px; width: 214px;">
                                
                    <asp:TextBox ID="tx_todate" runat="server"></asp:TextBox>
                     <script type  ="text/javascript">
                         new tcal({
                             'formname': 'form1',
                             'controlname': 'tx_todate'
                         });
	     </script>
                                
                </td>
                <td align="right" style="width: 417px; height: 23px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="vertical-align: middle; width: 315px; height: 23px">
                    Select Godown</td>
                <td style="height: 23px; " colspan="2">
                     <asp:DropDownList ID="ddlgodown" runat="server" Height="16px" Width="290px">
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
                <td class="style2">
                    <asp:Button ID="Button1" runat="server" Text="View Report" onclick="Button1_Click" />
                    </td>
                <td style="width: 251px; height: 25px;">
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
