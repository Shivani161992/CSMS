﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_PaddyMillingStatus.aspx.cs" Inherits="Reports_State_Rpt_csms_distwise_compliedreport" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Compiled Report Dist Wise</title>
    
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
    
     <script type = "text/javascript" src="../calendar_eu.js">
</script><link rel="stylesheet" href="../calendar.css" />
<script type="text/javascript" src="../js/chksql.js"></script>

    <script type="text/javascript">
    window.history.forward(0); 
    </script>
    
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
                                font-family: Arial; text-decoration: none">Paddy Milling Status Report</span></td>
                        <td align="right" style="font-weight: bolder; font-size: 12pt; color: black; background-color: lavender;">
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">पीछे जाए</asp:LinkButton></td>
                   
                    </tr>
                    <tr style="font-size: 12pt">
                        <td align="right" style="width: 24px; font-size: 10pt; height: 43px;">
                            <asp:Label ID="lbl_date" runat="server" Text="Crop Year"  Width="100px"></asp:Label>
                          </td>
                        <td style="width: 170px; font-size: 10pt; height: 43px;" align="left">
                                            <asp:DropDownList ID="ddlcropyear" runat="server"  Width="161px" Visible="true" >
                                              
                                                <asp:ListItem>2015-2016</asp:ListItem>
                                              
                                                <asp:ListItem Value="02">2014-2015</asp:ListItem>
                                                <asp:ListItem Value="03">2013-2014</asp:ListItem>
                                                <asp:ListItem Value="04">2012-2013</asp:ListItem>
                                                <asp:ListItem Value="05">2011-2012</asp:ListItem>
                                                <asp:ListItem Value="06">2010-2011</asp:ListItem>
                                                <asp:ListItem Value="07">2009-2010</asp:ListItem>
                                                <asp:ListItem Value="08">2008-2009</asp:ListItem>
                                              
                                            </asp:DropDownList>
                          </td>
                        <td align="right" style="font-size: 10pt; height: 43px;" colspan="4">
                            &nbsp;</td>
                        
                    </tr>
                    <tr style="font-size: 12pt">
                        <td align="right" style="width: 24px; font-size: 10pt; height: 43px;">
                            <asp:Label ID="lbl_date0" runat="server" Text="दिनांक चुनें "  Width="100px"></asp:Label>
                          </td>
                        <td style="width: 170px; font-size: 10pt; height: 43px;" align="left">
                            <asp:TextBox ID="txtDate" runat="server" Width="100px" >01-04-2015</asp:TextBox>
                            
                            <script type  ="text/javascript">
                                new tcal({
                                    'formname': 'form1',
                                    'controlname': 'txtDate'
                                });
	     </script>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate"
                                    ErrorMessage="कृपया दिनांक कलेंडर से चुनें" ValidationGroup="1">*</asp:RequiredFieldValidator>
                           
                        </td>
                        <td align="right" style="width: 256px; font-size: 10pt; height: 43px;">
                            <asp:Label ID="lbl_dateTill" runat="server" Text="दिनांक तक " Width="75px" ></asp:Label></td>
                        <td align="left" style="font-size: 10pt; width: 167px; height: 43px;">
                            <asp:TextBox ID="txtDateTill" runat="server"  Width="100px"></asp:TextBox>
                     
               <script type="text/javascript">
                   new tcal({
                       'formname': 'form1',
                       'controlname': 'txtDateTill'
                   });
                                </script>

                           
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDateTill"
                                ErrorMessage="कृपया दिनांक कलेंडर से चुनें" ValidationGroup="2" Visible="False">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="height: 43px; width: 295px;">
                            <asp:Button ID="Button1" runat="server" Text="View Report" ValidationGroup="1" Width="147px"
                                OnClick="Button1_Click" /></td><td style="height: 43px">&nbsp;</td>
                        
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
