<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RejectDetails.aspx.cs" Inherits="ReportForms_District_RejectDetails" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Rejection Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <center>
                <table border="1" cellpadding="0" cellspacing="0" style="border-style: double; border-width: 3;
                    padding: 1; border-collapse: collapse; border-color: Maroon; width: 100%; background-color:lavender;"
                    id="">
                    <tr>
                        <td align="center" colspan="5" style="font-weight: bolder; font-size: 12pt; color:Black; background-color: lavender; text-align: center;">
                            Rejection Details for Procurement 2016</td>
                        <td align="right" style="font-weight: bolder; font-size: 12pt; color: black; background-color: lavender;">
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">पीछे जाए</asp:LinkButton></td>
                   
                    </tr>
                    <tr>
                        <td align="left" style="height: 24px; font-size: 12pt; width: 139px; text-align: center; vertical-align: middle; background-color: lavender;" colspan="2">
                            <span style="font-size: 12pt">Select Commodity</span></td>
                                <td align="right" style="vertical-align: middle; width: 256px; height: 24px; font-size: 12pt; background-color: lavender;">
                                    <asp:DropDownList ID="ddlCommodity" runat="server" Width="200px">
                                    </asp:DropDownList>&nbsp;
                        </td>
                        <td style="width: 167px; font-size: 12pt; height: 24px; vertical-align: middle; background-color: lavender;" align="left">
                            Select Reject Type</td>
                          <td align="left" style="font-size: 12pt; height: 24px; width: 295px; vertical-align: middle; background-color: lavender;" >
                                    <asp:DropDownList ID="ddlrejtype" runat="server" Width="200px">
                                        <asp:ListItem Value="0">आंशिक</asp:ListItem>
                                        <asp:ListItem Value="1">पूर्ण</asp:ListItem>
                                    </asp:DropDownList>
                         </td>      
                    </tr>
                    <tr style="font-size: 12pt">
                        <td align="right" style="width: 24px; font-size: 10pt; height: 43px;">
                            &nbsp;</td>
                        <td style="width: 170px; font-size: 10pt; height: 43px;" align="left">
                                                           
                           
                        </td>
                        <td align="right" style="width: 256px; font-size: 10pt; height: 43px;">
                            &nbsp;</td>
                        <td align="left" style="font-size: 10pt; width: 167px; height: 43px;">
                                                       
                            <asp:Button ID="Button1" runat="server" Text="View Report" ValidationGroup="1" Width="147px"
                                OnClick="Button1_Click" />
                                                       
                                </td>
                                <td style="height: 43px; width: 295px;">
                                    &nbsp;</td><td style="height: 43px"></td>
                        
                    </tr>
                                      <tr>
                        <td align="center" colspan="6" style="height: 4px">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                ValidationGroup="1" Width="168px" />
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
