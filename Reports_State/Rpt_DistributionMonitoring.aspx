<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_DistributionMonitoring.aspx.cs" Inherits="Reports_State_Ho_CropYear_IC" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Distribution Monitoring  Report</title>
    
  
    
    
    <script type="text/javascript">
    window.history.forward(0); 
    </script>
    <style type="text/css">
        .style2
        {
            font-weight: 700;
            font-size: 12pt;
            color: #FF3300;
            text-align: center;
            font-style: normal;
            text-decoration: none;
            background-color: lavender;
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
                        <td align="right" colspan="6" class="style2">
                            Distribution Monitoring Report</td>
                   
                    </tr>
                    <tr style="font-size: 12pt">
                        <td align="right" 
                            style="font-size: medium; width: 161px; height: 28px; font-weight: 700;">
                            Month</td>
                        <td align="left" style="font-size: 10pt; width: 105px; height: 28px">
                                <asp:DropDownList ID="ddl_allot_month" runat="server" 
                                           
                                Width="153px">
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
                        <td align="right" style="font-size: 10pt; width: 160px; height: 28px">
                            <span style="font-size: 11pt"><strong>Year</strong></span></td>
                        <td colspan="2" style="height: 28px; text-align: left;">
                            <asp:DropDownList ID="ddlcropyear" runat="server" Width="200px">
                            
              <asp:ListItem Value="0">--select--</asp:ListItem>
             <asp:ListItem Value="2015">2015</asp:ListItem>
             <asp:ListItem Value="2014">2014</asp:ListItem>
             <asp:ListItem Value="2013">2013</asp:ListItem>
             <asp:ListItem Value="2012">2012</asp:ListItem>
                                    
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
