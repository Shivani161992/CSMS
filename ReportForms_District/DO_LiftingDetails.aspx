<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DO_LiftingDetails.aspx.cs" Inherits="ReportForms_District_DO_LiftingDetails" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Door Step Lifting Details</title>
    <style type="text/css">
        .style1
        {
            height: 1px;
            width: 111px;
        }
        .style2
        {
            height: 1px;
            width: 137px;
        }
        .style3
        {
            height: 1px;
            width: 104px;
        }
    </style>
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
                            <span style="font-weight: 700; font-size: 11pt; color: #00008b; font-style: normal;
                                font-family: Arial; text-decoration: none">Door Step Delivery Order and Lifting Details/span></td>
                        <td align="right" style="width: 163px; height: 23px; text-align: left;">
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">पीछे जाए</asp:LinkButton></td>
                   
                    </tr>
                    <tr style="font-size: 12pt">
                        <td align="right" style="font-size: 10pt; " class="style1">
                            Select Month
                          </td>
                        <td style="width: 1px; font-size: 10pt; height: 1px;" align="left">
                            
                            &nbsp;</td>
                        <td align="right" style="font-size: 10pt; " class="style2">
                            <asp:DropDownList ID="ddlmonth" runat="server" Height="16px" Width="140px">
                                <asp:ListItem Value="1">January</asp:ListItem>
                                <asp:ListItem Value="2">February</asp:ListItem>
                                <asp:ListItem Value="3">March</asp:ListItem>
                                <asp:ListItem Value="4">April</asp:ListItem>
                                <asp:ListItem Value="5">May</asp:ListItem>
                                <asp:ListItem Value="6">June</asp:ListItem>
                                <asp:ListItem Value="7">July</asp:ListItem>
                                <asp:ListItem Value="8">August</asp:ListItem>
                                <asp:ListItem Value="9">September</asp:ListItem>
                                <asp:ListItem Value="10">October</asp:ListItem>
                                <asp:ListItem Value="11">November</asp:ListItem>
                                <asp:ListItem Value="12">December</asp:ListItem>
                            </asp:DropDownList>
                            </td>
                        <td align="left" style="font-size: 10pt; " class="style3">
                     
                            Select Year</td>
                                <td style="height: 1px; text-align: left;">
                                    <asp:DropDownList ID="ddlyear" runat="server" Height="16px" Width="138px">
                                    </asp:DropDownList>
                        </td><td style="height: 1px">&nbsp;</td>
                        
                    </tr>
                    <tr style="font-size: 12pt">
                        <td align="right" style="font-size: 10pt; " class="style1">
                          </td>
                        <td style="width: 1px; font-size: 10pt; height: 1px;" align="left">
                            
                            
                           
                        </td>
                        <td align="right" style="font-size: 10pt; " class="style2">
                            </td>
                        <td align="left" style="font-size: 10pt; " class="style3">
                     
               

                           
                                </td>
                                <td style="height: 1px;">
                            <asp:Button ID="Button1" runat="server" Text="View Report" ValidationGroup="1" Width="147px"
                                OnClick="Button1_Click" /></td><td style="height: 1px"></td>
                        
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
