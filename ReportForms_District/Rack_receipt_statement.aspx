<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Rack_receipt_statement.aspx.vb" Inherits="District_food_rpt_Rack_receipt_statement" %>

 
 
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Rack Statement Receiving District</title>


<script type = "text/javascript" src="../calendar_eu.js"></script><link rel="stylesheet" href="../calendar.css" /><script type="text/javascript" src="../js/chksql.js"></script></head>  
<body>
    <form id="form1" runat="server">
        <table border="1" cellpadding="0" cellspacing="0" style="border-style:double; border-width:3; padding:1; BORDER-COLLAPSE: collapse; border-color :Maroon  ; width: 823px; background-color: #ece9d8;" id="">
            <tr>
                <td align="center" colspan="5" style="font-weight: bolder; font-size: 15pt; color: teal">
                    Rack Statement Receiving District</td>
            </tr>
            <tr>
                <td style="vertical-align: middle; width: 189px; height: 23px">
                    District Logged In</td>
                <td style="width: 100px; height: 23px">
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="MediumBlue"></asp:Label></td>
                <td align="right" style="vertical-align: middle; width: 111px; height: 23px">
                </td>
                <td align="left">
                    </td>
               <td align="right" style="width: 417px; height: 23px">
                    <asp:LinkButton ID="LinkButton1" runat="server" style="font-weight: 700" Font-Bold="False" Font-Size="Large">Back</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: middle; width: 189px; height: 23px">
                    From Date
                </td>
                <td style="width: 100px; height: 23px">
                   <asp:TextBox ID="tx_from_date" runat="server"></asp:TextBox>
                                <script type  ="text/javascript">
                                                	new tcal ({
				'formname': 'form1',
				'controlname': 'tx_from_date'
	    });
	     </script>
                </td>
                <td align="right" style="vertical-align: middle; width: 111px; height: 23px">
                    To Date
                </td>
                <td align="left">
                    <asp:TextBox ID="tx_to_date" runat="server"></asp:TextBox>
                                <script type  ="text/javascript">
                                                	new tcal ({
				'formname': 'form1',
				'controlname': 'tx_to_date'
	    });
	     </script>
                </td>
                <td align="left" style="width: 417px; height: 23px">
                </td>
            </tr>
            <tr>
                <td style="vertical-align: middle; width: 189px; height: 23px">
                    Rack Number</td>
                <td style="width: 100px; height: 23px">
                    <asp:DropDownList ID="ddlrackno" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlrackno_SelectedIndexChanged1"
                        Width="174px">
                    </asp:DropDownList></td>
                <td align="right" style="vertical-align: middle; width: 111px; height: 23px">
                </td>
                <td align="left">
                </td>
                <td align="left" style="width: 417px; height: 23px">
                    <asp:Button ID="Button1" runat="server" Text="View Report" Width="111px" /></td>
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

