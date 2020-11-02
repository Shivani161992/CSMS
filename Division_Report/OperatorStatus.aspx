<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OperatorStatus.aspx.cs" Inherits="Division_Report_OperatorStatus" %>



<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Operator Status Page</title>
</head>
<body>
    <form id="form1" runat="server">
   <div>
    <center>
                <table border="1" cellpadding="0" cellspacing="0" style="border-style: double; border-width: 3;
                    padding: 1; border-collapse: collapse; border-color: Maroon; width: 100%; background-color:lightsteelblue;"
                    id="">
                    <tr>
                        <td align="center" colspan="5" style="font-weight: bolder; font-size: 12pt; color:Black">
                            <span style="font-weight: 700; font-size: 11pt; color: #cc0033; font-style: normal;
                                font-family: Arial; text-decoration: none">Regional Report of District and Depot
                                Operators Status as per
                                their Last Activity</span></td>
                        <td align="right" style="width: 163px; height: 23px; text-align: left;">
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" ForeColor="Black">पीछे जाए</asp:LinkButton></td>
                   
                    </tr>
                    <tr>
                        <td align="left" style="height: 1px; font-size: 10pt;" colspan="4">
                            &nbsp;&nbsp; <span style="font-size: 12pt"></span>&nbsp;
                        </td>
                          <td align="left" style="font-size: 12pt; height: 1px;" >
                              &nbsp;</td>      
                    </tr>
                    <tr style="font-size: 12pt">
                        <td align="right" style="font-size: 10pt; height: 1px;" colspan="4">
                            
              
                          </td>
                                <td style="height: 1px;">
                            <asp:Button ID="Button1" runat="server" Text="View Report" ValidationGroup="1" Width="147px"
                                OnClick="Button1_Click" /></td><td style="height: 1px"></td>
                        
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
