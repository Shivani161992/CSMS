<%@ Page Language="VB" AutoEventWireup="false" CodeFile="multiple_DO_issueCentrewise_rpt.aspx.vb" Inherits="multiple_DO_issueCentrewise_rpt" %>
 
 
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Delivery Order Report</title>
<script type="text/javascript" src="../js/calendar_eu.js"></script>      
       <link rel="stylesheet" type="text/css" href="../CSS/calendar.css" />
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

</head>
<body>
    <form id="form1" runat="server">
        <table border="1" cellpadding="0" cellspacing="0" style="border-style:double; border-width:3; padding:1; BORDER-COLLAPSE: collapse; border-color :Maroon  ; width: 800px; background-color: #ece9d8;" id="">
            <tr>
                <td align="center" colspan="5" style="font-weight: bolder; font-size: 15pt; color: teal">
                    Multiple
                    Delivery Order Report</td>
            </tr>
            <tr>
                <td style="vertical-align: middle; width: 268px; height: 23px">
                    District Logged In</td>
                <td style="width: 100px; height: 23px">
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="MediumBlue"></asp:Label></td>
                <td align="right" style="vertical-align: middle; width: 192px; height: 23px">
                    Issue Centre</td>
                <td align="left" style="width: 217px">
                    <asp:DropDownList ID="ddl_issuename" runat="server" Width="192px">
                    </asp:DropDownList></td>
                <td align="right" style="width: 417px; height: 23px">
                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="False" Font-Size="Large"
                        NavigateUrl="~/DistrictFood/frmReports.aspx">Back</asp:HyperLink></td>
            </tr>
            <tr>
                <td style="width: 268px; height: 23px; vertical-align: middle;">
                    From Date</td>
                <td style="width: 100px; height: 23px;">
                    
                         <asp:TextBox ID="tx_from_date" runat="server"></asp:TextBox>
                    <script type  ="text/javascript">
                                                	new tcal ({
                                                	'formname': 'form1',
				'controlname': 'tx_from_date'
	    });
	     </script>
                </td>
                <td align="right" style="width: 192px; height: 23px; vertical-align: middle;">
                    To Date&nbsp;
                </td>
                <td style="width: 217px; height: 23px;">
                                        
                     <asp:TextBox ID="tx_to_date" runat="server"></asp:TextBox>
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
                    &nbsp;</td>
            </tr>
        </table>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="440px" ProcessingMode="Remote" Width="800px" Font-Names="Verdana" Font-Size="8pt" ShowCredentialPrompts="False">
            <ServerReport ReportServerUrl=""  />
        </rsweb:ReportViewer>
        &nbsp;
    </form>
</body>
</html>
