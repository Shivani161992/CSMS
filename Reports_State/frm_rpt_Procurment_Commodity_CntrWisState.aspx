<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_rpt_Procurment_Commodity_CntrWisState.aspx.cs" Inherits="Reports_State_frm_rpt_Procurment_Commodity_CntrWisState" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Daily Entry DistriWise</title>
 <script type = "text/javascript" src="../calendar_eu.js"></script>  
	<link rel="stylesheet" href="../calendar.css" />
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
</head>
<body>
    <form id="form1" runat="server">
        <table border="1" cellpadding="0" cellspacing="0" style="border-style:double; border-width:3; padding:1; BORDER-COLLAPSE: collapse; border-color :Maroon  ; width: 100%; background-color: #ece9d8;" id="">
            <tr>
                <td align="left" colspan="3" style="font-weight: bolder; font-size: 15pt; color: teal; height: 23px; text-align: center; width: 525px;">
                    eUparjan Procurement 2013-14</td><td style="width: 687px; height: 23px;"></td>
                    <td style="height: 23px; width:70px">                        
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" >Back</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 185px; height: 15px" colspan="2">
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="MediumBlue"></asp:Label></td>
                    
                
                <td align="left" colspan="3" style="font-size: 16px; color: blue">
                   <asp:RadioButton ID="RadioButton1" runat="server" GroupName="Rtype" Text="Paddy"
                                AutoPostBack="True"  Checked="True" OnCheckedChanged="RadioButton1_CheckedChanged" />
                            <asp:RadioButton ID="RadioButton2" runat="server" GroupName="Rtype" Text="Coarse Grain"
                                AutoPostBack="True" OnCheckedChanged="RadioButton2_CheckedChanged"  />
                    </td>
                
            </tr>
            
        </table>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="600px" ZoomMode="PageWidth" ProcessingMode="Remote" Width="100%" Font-Names="Verdana" Font-Size="8pt" ShowCredentialPrompts="False">
            <ServerReport ReportServerUrl="" />
        </rsweb:ReportViewer>
        &nbsp;
    </form>
</body>
</html>
