<%@ Page Language="VB" AutoEventWireup="false" CodeFile="liftingDetails_DO_divisionRpt.aspx.vb" Inherits="liftingDetails_DO_divisionRpt" %>
 
 
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Delivery Order Report</title>


<script type = "text/javascript" src="../calendar_eu.js"></script><link rel="stylesheet" href="../calendar.css" /><script type="text/javascript" src="../js/chksql.js"></script></head>  
<body>
    <form id="form1" runat="server">
        <table border="1" cellpadding="0" cellspacing="0" style="border-style:double; border-width:3; padding:1; BORDER-COLLAPSE: collapse; border-color :Maroon  ; width: 808px; background-color: #ece9d8;" id="">
            <tr>
                <td align="center" colspan="5" style="font-weight: bolder; font-size: 15pt; color: teal; height: 23px;">
                    &nbsp;Lifting Details of
                    Delivery Order</td>
            </tr>
            <tr>
                <td style="vertical-align: middle; width: 97px;">
                    Division</td>
                <td style="width: 100px; height: 24px">
                    <asp:DropDownList ID="ddl_division" runat="server" Width="160px">
                    </asp:DropDownList></td>
                <td align="right" style="vertical-align: middle; width: 110px; height: 24px">
                </td>
                <td align="left" style="height: 24px; width: 179px;">
                    </td>
                <td align="right" style="width: 417px; height: 24px">
                    <asp:LinkButton ID="LinkButton1" runat="server">Back</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 97px; height: 23px; vertical-align: middle;">
                    From Date</td>
                <td>
                    <asp:TextBox ID="tx_from_date" runat="server" Width="129px"></asp:TextBox>
                                <script type  ="text/javascript">
                                                	new tcal ({
				'formname': 'form1',
				'controlname': 'tx_from_date'
	    });
	     </script>
                </td>
                <td align="right" style="width: 110px; height: 23px; vertical-align: middle;">
                    To Date&nbsp;
                </td>
                <td>
                      <asp:TextBox ID="tx_to_date" runat="server" Width="126px"></asp:TextBox>
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
                    <asp:Label ID="Label2" runat="server"></asp:Label></td>
            </tr>
        </table>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="440px" ProcessingMode="Remote" Width="808px" Font-Names="Verdana" Font-Size="8pt" ShowCredentialPrompts="False">
            <ServerReport ReportServerUrl=""  />
        </rsweb:ReportViewer>
        &nbsp;
    </form>
</body>
</html>
