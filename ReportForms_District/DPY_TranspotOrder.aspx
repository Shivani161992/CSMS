﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DPY_TranspotOrder.aspx.cs" Inherits="ReportForms_District_DPY_TranspotOrder" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Transpot Order</title>
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
                            <span style="font-weight: 700; font-size: 11pt; color: #00008b; font-style: normal;
                                font-family: Arial; text-decoration: none">Transport Order Details</span></td>
                        <td align="right" style="font-weight: bolder; font-size: 12pt; color: black; background-color: lavender;">
                            </td>
                   
                    </tr>
                    <tr>
                        <td align="left" style="height: 24px; font-size: 12pt; width: 110px; text-align: center; vertical-align: middle; background-color: lavender;" colspan="2">
                            <span style="font-size: 12pt">Select Month</span></td>
                                <td align="right" style="vertical-align: middle; width: 236px; height: 24px; font-size: 12pt; background-color: lavender; text-align: left;">
                                    &nbsp;&nbsp;
                                    
                                    <script type="text/javascript">
	             new tcal ({
				'formname': 'form1',
				'controlname': 'txtDate'
	                      });
                                </script>

                                    <asp:DropDownList ID="ddlmonth" runat="server" Width="180px">
                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
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
                                    </asp:DropDownList></td>
                                    
                                    
                        <td style="width: 101px; font-size: 12pt; height: 24px; vertical-align: middle; background-color: lavender;" align="left">
                            &nbsp;Select Year</td>
                          <td align="left" style="font-size: 12pt; height: 24px; width: 193px; vertical-align: middle; background-color: lavender;" >
                              &nbsp;
                                
                                <asp:DropDownList ID="ddlyear" runat="server" Width="170px">
                                 <asp:ListItem Value="0">-Select-</asp:ListItem>
                                  <asp:ListItem Value="2014">2014</asp:ListItem>
                                   <asp:ListItem Value="2015">2015</asp:ListItem>
                              </asp:DropDownList></td>      
                    </tr>
                    <tr style="font-size: 12pt">
                        <td align="right" style="width: 24px; font-size: 10pt; height: 1px;">
                            &nbsp;</td>
                        <td style="font-size: 10pt; height: 1px;" align="left" colspan="4">
                            &nbsp;
                     
               

                            &nbsp;
                            <asp:Button ID="Button1" runat="server" Text="View Report" ValidationGroup="1" Width="147px"
                                OnClick="Button1_Click" /></td>
                        <td style="height: 1px">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Font-Bold="True" ForeColor="#000066">पीछे जाए</asp:LinkButton></td>
                        
                    </tr>
                                      <tr>
                        <td align="center" colspan="6" style="height: 1px">
                            &nbsp;</td>
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
