<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_storagewiseDetail.aspx.cs" Inherits="ReportForms_District_rpt_storagewiseDetail" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Storage wise Deposit Report Dist Wise</title>

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

    <script type="text/javascript" src="../calendar_eu.js">
    </script>
    <link rel="stylesheet" href="../calendar.css" />
    <script type="text/javascript" src="../js/chksql.js"></script>

    <script type="text/javascript">
        window.history.forward(0);
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center>
                <table border="1" cellpadding="0" cellspacing="0" style="border-style: double; border-width: 3; padding: 1; border-collapse: collapse; border-color: Maroon; width: 100%; background-color: lightsteelblue;"
                    id="">
                    <tr>
                        <td align="center" colspan="5" style="font-weight: bolder; font-size: 12pt; color: Black">
                            <span style="font-weight: 700; font-size: 11pt; color: #00008b; font-style: normal; font-family: Arial; text-decoration: none">Society wise Deposit Report</span></td>
                        <td align="right" style="width: 163px; height: 23px; text-align: left;">
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">पीछे जाए</asp:LinkButton></td>

                    </tr>
                    <tr>
                        <td align="center" colspan="5" style="font-weight: bolder; font-size: 12pt; color: Black">



                            <asp:Label ID="Label2" runat="server" Text="कमोडिटी" Width="130px"
                                Style="text-align: center"></asp:Label>



                            <asp:DropDownList ID="ddlcrop" runat="server"
                                Width="100px" Visible="true">
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Select Issue Center
                            <asp:DropDownList ID="ddlissue" runat="server" AutoPostBack="True"
                                Height="25px" OnSelectedIndexChanged="ddlissue_SelectedIndexChanged"
                                Width="160px">
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Select Godown
                            <asp:DropDownList ID="ddlgodown" runat="server" AutoPostBack="True"
                                Height="25px" Width="270px">
                            </asp:DropDownList>
                        </td>
                        <td align="right" style="width: 163px; height: 23px; text-align: left;">&nbsp;</td>

                    </tr>
                    <tr style="font-size: 12pt">
                        <td align="right" style="width: 18px; font-size: 10pt; height: 1px;">
                            <asp:Label ID="lbl_date" runat="server" Text="दिनांक चुनें " Width="100px"></asp:Label>
                        </td>
                        <td style="width: 1px; font-size: 10pt; height: 1px;" align="left">
                            <asp:TextBox ID="txtDate" runat="server" Width="113px"></asp:TextBox>

                            <script type="text/javascript">
                                new tcal({
                                    'formname': 'form1',
                                    'controlname': 'txtDate'
                                });
                            </script>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate"
                                ErrorMessage="कृपया दिनांक कलेंडर से चुनें" ValidationGroup="1">*</asp:RequiredFieldValidator>

                        </td>
                        <td align="right" style="width: 1px; font-size: 10pt; height: 1px;">
                            <asp:Label ID="lbl_dateTill" runat="server" Text="दिनांक तक " Width="74px"
                                Style="margin-left: 1px"></asp:Label></td>
                        <td align="left" style="font-size: 10pt; width: 300px; height: 1px;">
                            <asp:TextBox ID="txtDateTill" runat="server" Width="123px"></asp:TextBox>

                            <script type="text/javascript">
                                new tcal({
                                    'formname': 'form1',
                                    'controlname': 'txtDateTill'
                                });
                            </script>


                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDateTill"
                                ErrorMessage="कृपया दिनांक कलेंडर से चुनें" ValidationGroup="2" Visible="False">*</asp:RequiredFieldValidator>
                        </td>
                        <td style="height: 1px;">
                            <asp:Button ID="Button1" runat="server" Text="View Selection" ValidationGroup="1" Width="147px"
                                OnClick="Button1_Click" Style="height: 26px" /></td>
                        <td style="height: 1px"></td>

                    </tr>
                    <tr>
                        <td align="center" colspan="6" style="height: 1px">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                ValidationGroup="1" Width="210px" Height="2px" />
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="pnlreport" runat="server" Visible="false">
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="520px" ProcessingMode="Remote"
                        Width="100%" Font-Names="Verdana" Font-Size="8pt" ShowCredentialPrompts="False"
                        ZoomMode="PageWidth" BackColor="CornflowerBlue" BorderColor="#E0E0E0"
                        LinkDisabledColor="Black" LinkActiveColor="Yellow" Style="margin-left: 0px; margin-right: 0px;"
                        ShowBackButton="true" EnableTheming="true">
                        <ServerReport ReportServerUrl="" />
                    </rsweb:ReportViewer>
                </asp:Panel>
            </center>
        </div>
    </form>
</body>
</html>
