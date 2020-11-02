<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_CropYearWise_Stock_Detals.aspx.cs" Inherits="Reports_State_Ho_CropYear_IC" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Crop Year Wise Stock Details</title>
    
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
    
     <script type = "text/javascript" src="../calendar_eu.js">
</script><link rel="stylesheet" href="../calendar.css" />
<script type="text/javascript" src="../js/chksql.js"></script>

    <script type="text/javascript">
        window.history.forward(0); 
    </script>
    
    <style type="text/css">
        .style2
        {
            text-align: center;
            font-size: large;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
     <center>
                <table border="1" cellpadding="0" cellspacing="0" style="border: 3 double Maroon; padding: 1; border-collapse: collapse; width: 100%; background-color:#FFCC99;"
                    id="">
                    <tr>
                        <td align="right" colspan="5" class="style2">
                            <strong>&nbsp;Crop Year Wise Commodity Stock Details</strong></td>
                   
                    </tr>
                    <tr style="font-size: 12pt">
                       
                        <td align="right" style="font-size: 10pt; width: 160px; height: 28px">
                            <span style="font-size: 11pt"><strong>Crop Year</strong></span></td>
                        <td style="height: 28px; text-align: left;">
                            <asp:DropDownList ID="ddlcropyear" runat="server" Width="200px">
                            
              <asp:ListItem Value="0">--select--</asp:ListItem>
           <asp:ListItem Value="2015-2016">2015-2016</asp:ListItem>
                                <asp:ListItem Value="2014-2015">2014-2015</asp:ListItem>
                                <asp:ListItem Value="2013-2014">2013-2014</asp:ListItem>
                                <asp:ListItem Value="2012-2013">2012-2013</asp:ListItem>
                                <asp:ListItem Value="2011-2012">2011-2012</asp:ListItem>
                                    
                            </asp:DropDownList>
                            
                            </td>
                        <td style="height: 28px">
                            <asp:Button ID="Button1" runat="server" Text="View Report" ValidationGroup="1" Width="147px" OnClick="Button1_Click" />
                        </td>
                    </tr>
                    
                    <tr style="font-size: 12pt">
                                  <td align="right" style="font-size: 10pt; height: 43px;" colspan="4">
                                      &nbsp;</td>
                        <td style="height: 43px">
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">पीछे जाए</asp:LinkButton></td>
                        
                    </tr>
                                      <tr>
                        <td align="center" colspan="5" style="height: 4px">
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
