<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DailyReceipt_statement.aspx.vb" Inherits="Reports_State_DailyReceipt_statement" %>

 
 
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Daily Receipt Statement</title>


<script type = "text/javascript" src="../calendar_eu.js"></script><link rel="stylesheet" href="../calendar.css" /><script type="text/javascript" src="../js/chksql.js"></script></head>  
<body>
    <form id="form1" runat="server">
        <table border="1" cellpadding="0" cellspacing="0" style="border-style:double; border-width:3; padding:1; BORDER-COLLAPSE: collapse; border-color :Maroon  ; width: 887px; background-color: #ece9d8;" id="">
            <tr>
                <td align="center" colspan="5" style="font-weight: bolder; font-size: 15pt; color: teal">
                    Daily Receipt Statement</td>
            </tr>
            <tr>
                <td style="vertical-align: middle; width: 228px; height: 23px">
                    </td>
                <td style="width: 100px; height: 23px">
                    </td>
                <td align="right" style="vertical-align: middle; width: 93px; height: 23px">
                </td>
                <td align="left" style="width: 157px">
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="Maroon">Head Office</asp:Label></td>
                <td align="left" style="width: 417px; height: 23px">
                    </td>
            </tr>
            <tr>
                <td style="vertical-align: middle; width: 228px; height: 23px">
                    Division</td>
                <td style="width: 100px; height: 23px">
                    <asp:DropDownList ID="ddlregion" runat="server" Width="138px">
                    </asp:DropDownList></td>
                <td align="right" style="vertical-align: middle; width: 93px; height: 23px">
                </td>
                <td align="left" style="width: 157px">
                </td>
                <td align="left" style="width: 417px; height: 23px">
                </td>
            </tr>
            <tr>
                <td style="width: 228px; height: 23px; vertical-align: middle;">
                    From Date</td>
                <td>
                    <asp:TextBox ID="tx_from_date" runat="server" Width="111px"></asp:TextBox>
                                <script type  ="text/javascript">
                                                	new tcal ({
				'formname': 'form1',
				'controlname': 'tx_from_date'
	    });
	     </script>
                </td>
                <td align="right" style="width: 93px; height: 23px; vertical-align: middle;">
                    To Date&nbsp;
                </td>
                <td style="width: 157px;">
                     <asp:TextBox ID="tx_to_date" runat="server" Width="117px"></asp:TextBox>
                                <script type  ="text/javascript">
                                                	new tcal ({
				'formname': 'form1',
				'controlname': 'tx_to_date'
	    });
	     </script>
                </td>
                <td style="width: 417px; height: 23px;" align="right">
                    <asp:LinkButton ID="LinkButton1" runat="server">Back</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 228px; height: 25px;">
                    Source of Arrival</td>
                <td style="width: 100px; height: 25px;">
                    <asp:DropDownList ID="ddlsource" runat="server" Width="145px">
                    </asp:DropDownList></td>
                <td style="width: 93px; height: 25px;">
                </td>
                <td style="width: 157px; height: 25px;">
                </td>
                <td align="right" style="width: 417px; height: 25px;">
                    <asp:Button ID="Button1" runat="server" Text="View Report" /></td>
            </tr>
            <tr>
                <td colspan="5">
                    &nbsp;<asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label></td>
            </tr>
        </table>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="440px" ProcessingMode="Remote" Width="887px" Font-Names="Verdana" Font-Size="8pt" ShowCredentialPrompts="False">
            <ServerReport ReportServerUrl=""  />
        </rsweb:ReportViewer>
        &nbsp;
    </form>
</body>
</html>
