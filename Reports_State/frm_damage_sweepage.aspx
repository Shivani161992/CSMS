﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_damage_sweepage.aspx.cs" Inherits="Reports_State_Ho_CropYear_IC" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Crop Year Wise Damage/sweepage Report</title>
    
  
    
    
    <script type="text/javascript">
    window.history.forward(0); 
    </script>
    <style type="text/css">
        .style2
        {
            color: #000099;
        }
        .style3
        {
            height: 18px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <center>
                <table border="1" cellpadding="0" cellspacing="0" style="border-style: double; border-width: 3;
                    padding: 1; border-collapse: collapse; border-color: Maroon; width: 100%; background-color:lavender;"
                    id="">
                    <tr>
                        <td align="right" colspan="6" style="font-weight: bolder; font-size: 12pt; color: black;
                            background-color: lavender; text-align: center" class="style3">
                            <span style="font-weight: 700; color: #00008b; font-style: normal; text-decoration: none"></span>
                            <span style="font-size: 11pt; font-family: Arial" class="style2">&nbsp;</span><span 
                                class="style2">Damage sweepage Report Crop year wise</span></td>
                   
                    </tr>
                    <tr style="font-size: 12pt">
                        <td align="right" style="font-size: 10pt; width: 161px; height: 28px">
                            <span style="font-size: 11pt"><strong>Select Commodity</strong></span></td>
                        <td align="left" style="font-size: 10pt; width: 105px; height: 28px">
                            <asp:DropDownList ID="ddlcommodity" runat="server" Width="200px">
                            </asp:DropDownList></td>
                        <td align="right" style="font-size: 10pt; width: 160px; height: 28px">
                            <span style="font-size: 11pt"><strong>Select Crop Year</strong></span></td>
                        <td colspan="2" style="height: 28px">
                            <asp:DropDownList ID="ddlcropyear" runat="server" Width="200px">
                            
              <asp:ListItem Value="0">--select--</asp:ListItem>
             <asp:ListItem Value="2014-2015">2014-2015</asp:ListItem>
             <asp:ListItem Value="2013-2014">2013-2014</asp:ListItem>
             <asp:ListItem Value="2012-2013">2012-2013</asp:ListItem>
             <asp:ListItem Value="2011-2012">2011-2012</asp:ListItem>
                                    
                            </asp:DropDownList>
                            
                            </td>
                        <td style="height: 28px">
                        </td>
                    </tr>
                    
                    <tr style="font-size: 12pt">
                        <td align="right" style="width: 161px; font-size: 10pt; height: 43px;">
                            &nbsp;</td>
                        <td style="width: 105px; font-size: 10pt; height: 43px;" align="left">
                            
                            <script type  ="text/javascript">
                                                	new tcal ({
				'formname': 'form1',
				'controlname': 'txtDate'
	    });
	     </script>

                            &nbsp;
                           
                        </td>
                        <td align="right" style="width: 160px; font-size: 10pt; height: 43px;">
                            </td>
                        <td colspan="2" style="height: 43px">
                            &nbsp;
                            <asp:Button ID="Button1" runat="server" Text="View Report" ValidationGroup="1" Width="147px" OnClick="Button1_Click" /></td>
                        <td style="height: 43px">
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">पीछे जाए</asp:LinkButton></td>
                        
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
    </form>
</body>
</html>
