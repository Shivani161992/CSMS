<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Form13A.aspx.vb" Inherits="ReportForms_District_Form13A" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Form 13 - A</title>

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

    <script type="text/javascript" src="../calendar_eu.js"></script>

    <link rel="stylesheet" href="../calendar.css" />
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <div>
                <table border="1" cellpadding="0" cellspacing="0" style="border-style: double; border-width: 3;
                    padding: 1; border-collapse: collapse; border-color: Maroon; width: 1000px;">
                    <tr style="background-color: #ece9d8;">
                        <td align="center" colspan="5">
                            <span style="font-weight: bold; font-size: 15pt; color: Navy;">Daily Stock Transfer
                                Information(Form-13(A))</span></td>
                    </tr>
                    <tr style="background-color: #ece9d8;">
                        <td style="width: 250px;" align="center">
                            District Logged In</td>
                        <td style="width: 250px;" align="center">
                            <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="MediumBlue"></asp:Label></td>
                        <td align="center" colspan="2" style="width: 250px;">
                            IssueCenter Logged In</td>
                        <td align="left" style="width: 250px;">
                            <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="MediumBlue"></asp:Label></td>
                    </tr>
                    <tr style="background-color: #ece9d8;">
                        <td align="center">
                            From Date</td>
                        <td align="left">
                            <asp:TextBox ID="tx_from_date" runat="server" Width="150px"></asp:TextBox>

                            <script type="text/javascript">
                                    new tcal ({
				                   'formname': 'form1',
				                   'controlname': 'tx_from_date'
	                                     });
                            </script>

                        </td>
                        <td align="center" style="width: 60px">
                            To Date</td>
                        <td align="left">
                            <asp:TextBox ID="tx_to_date" runat="server" Width="151px"></asp:TextBox>

                            <script type="text/javascript">
                                                	new tcal ({
				'formname': 'form1',
				'controlname': 'tx_to_date'
	                       });
                            </script>

                        </td>
                        <td align="center">
                            <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="False" Font-Size="Large"
                                NavigateUrl="~/IssueCenter/frmReports.aspx">Back</asp:HyperLink></td>
                    </tr>
                    <tr style="background-color: #ece9d8;">
                        <td align="center">
                            Commodity Name</td>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlcommodity" runat="server" Width="200px" Height="25px">
                            </asp:DropDownList></td>
                        <td align="center">
                            <asp:Button ID="Button1" runat="server" Text="View Report" /></td>
                    </tr>
                    <tr style="background-color: #ece9d8;">
                        <td colspan="5">
                            <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="5">
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="550px">
                            </rsweb:ReportViewer>
                        </td>
                    </tr>
                </table>
            </div>
        </center>
    </form>
</body>
</html>
