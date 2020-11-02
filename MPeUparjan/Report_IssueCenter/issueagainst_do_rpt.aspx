<%@ Page Language="VB" AutoEventWireup="false" CodeFile="issueagainst_do_rpt.aspx.vb"
    Inherits="issueagainst_do_rpt" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Delivery Order Report</title>

    <script type="text/javascript" src="../calendar_eu.js"></script>

    <link rel="stylesheet" href="../calendar.css" />

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

</head>
<body>
    <form id="form1" runat="server">
        <center>
            <div style="width: 800px;">
                <table border="1" cellpadding="0" cellspacing="0" style="border-style: double; border-width: 3;
                    padding: 1; border-collapse: collapse; border-color: Maroon; width: 800px;"
                    id="">
                    <tr style="background-color: #ece9d8;">
                        <td align="center" colspan="5" style="font-weight: bolder; font-size: 15pt; color: teal;
                            height: 23px;">
                            Issue Against Delivery Order Report</td>
                    </tr>
                    <tr style="background-color: #ece9d8;">
                        <td style="width: 150px;" align="center">
                            District Logged In</td>
                        <td style="width: 183px; height: 24px;" align="left">
                            <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="MediumBlue"></asp:Label></td>
                        <td align="center" style="width: 145px; height: 24px; vertical-align: middle;">
                            IssueCentre Logged In</td>
                        <td style="width: 150px;">
                            <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="MediumBlue"></asp:Label></td>
                        <td style="height: 24px;" align="center">
                            <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="False" Font-Size="Large"
                                NavigateUrl="~/IssueCenter/frmReports.aspx">Back</asp:HyperLink></td>
                    </tr>
                    <tr style="background-color: #ece9d8;">
                        <td align="center">
                            From Date</td>
                        <td style="height: 23px">
                            <asp:TextBox ID="tx_from_date" runat="server" Width="120px"></asp:TextBox>

                            <script type="text/javascript">
                                                	new tcal ({
				                              'formname': 'form1',
				                           'controlname': 'tx_from_date'
	                                                      });
                            </script>

                        </td>
                        <td align="center">
                            To Date</td>
                        <td style="height: 23px">
                            <asp:TextBox ID="tx_to_date" runat="server" Width="120px"></asp:TextBox>

                            <script type="text/javascript">
                                                	new tcal ({
				                      'formname': 'form1',
				                     'controlname': 'tx_to_date'
	                                    });
                            </script>

                        </td>
                        <td align="center">
                            <asp:Button ID="Button1" runat="server" Text="Get DO Number" />
                        </td>
                    </tr>
                    <tr style="background-color: #ece9d8;">
                        <td align="right" colspan="2">
                            Delivery Order No. -</td>
                        <td align="center" colspan="2">
                            <asp:DropDownList ID="ddl_do_no" runat="server" Height="25px" Width="200px" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td align="center">
                           </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="5">
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="550px" ProcessingMode="Remote"
                                Width="800px" Font-Names="Verdana" Font-Size="8pt" ShowCredentialPrompts="False">
                                <ServerReport ReportServerUrl="" />
                            </rsweb:ReportViewer>
                        </td>
                    </tr>
                </table>
            </div>
        </center>
    </form>
</body>
</html>
