<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Distwise_AllCommodity_Receipt_Accep.aspx.cs" Inherits="Reports_State_Distwise_AllCommodity_Receipt_Accep" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Compiled Report District Wise</title>
    
  
    
    
    <script type="text/javascript">
    window.history.forward(0); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    
     <center>
                <table border="1" cellpadding="0" cellspacing="0" style="border-style: double; border-width: 3;
                    padding: 1; border-collapse: collapse; border-color: Maroon; width: 100%; background-color:lavender;"
                    id="">
                    <tr>
                        <td align="right" colspan="6" style="font-weight: bolder; font-size: 12pt; color: black;
                            background-color: lavender; text-align: center">
                            <span style="font-weight: 700; color: #00008b; font-style: normal; text-decoration: none"></span><span style="font-size: 11pt; color: #00008b; font-family: Arial">Distirct wise Receiving of of Trucks and Their Acceptance Note Receiving from Procurement Centers</span></td>
                   
                    </tr>
                    
                    <tr style="font-size: 12pt">
                        <td align="right" style="width: 24px; font-size: 10pt; height: 43px;">
                            &nbsp;</td>
                        <td style="width: 170px; font-size: 10pt; height: 43px;" align="left">
                            
                            <script type  ="text/javascript">
                                                	new tcal ({
				'formname': 'form1',
				'controlname': 'txtDate'
	    });
	     </script>

                            &nbsp;
                           
                        </td>
                        <td align="right" style="width: 256px; font-size: 10pt; height: 43px;">
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
