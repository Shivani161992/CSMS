<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_transportorderwise_detail.aspx.cs" Inherits="Report_IssueCenter_TotalSummary" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Transport Order wise Detail Report</title>
    
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


    <style type="text/css">
        .style1
        {
            font-size: medium;
        }
        .style2
        {
            height: 1px;
            width: 34px;
        }
        .style3
        {
            font-size: large;
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
                        <td align="center" colspan="6" style="font-weight: bolder; color:Black" 
                            class="style3">
                            Transport Order Detail Report</td>
                        <td align="right" style="width: 163px; height: 23px; text-align: left;">
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">पीछे जाए</asp:LinkButton></td>
                   
                    </tr>
                    <tr>
                        <td align="left" style="height: 1px; text-align: center;" colspan="2" 
                            class="style1">
                            &nbsp;&nbsp;&nbsp;&nbsp; IssueCenter</td>
                                <td align="right" style="vertical-align: middle; width: 1px; height: 1px; font-size: 12pt;">
                                    &nbsp;</td>
                        <td style="width: 300px; font-size: 12pt; height: 1px; text-align: left;" 
                            align="left">
                <asp:DropDownList ID="ddlissuecenter" runat="server" Width="194px" AutoPostBack="True" 
                                onselectedindexchanged="ddlissuecenter_SelectedIndexChanged" >
                    
                    
                </asp:DropDownList></td>
                          <td align="left" style="font-size: 12pt; height: 1px;" >
                              Transport Order</td>      
                          <td align="left" style="font-size: 12pt; height: 1px;" >
                                    <asp:DropDownList ID="ddl_Transportorder" runat="server" Width="195px">
                                    </asp:DropDownList>
                        </td>      
                    </tr>
                    <tr style="font-size: 12pt">
                        <td align="right" style="width: 24px; font-size: 10pt; height: 1px;">
                         </td>
                        <td style="font-size: 10pt; " align="left" class="style2">
                            
                                &nbsp;</td>
                        <td align="right" style="width: 1px; font-size: 10pt; height: 1px;">
                            &nbsp;</td>
                        <td align="left" style="font-size: 10pt; width: 300px; height: 1px;">
                            &nbsp;</td>
                                <td style="height: 1px;" colspan="2">
                            <asp:Button ID="Button1" runat="server" Text="View Report" ValidationGroup="1" Width="147px"
                                OnClick="Button1_Click" /></td><td style="height: 1px"></td>
                        
                    </tr>
                                      <tr>
                        <td align="center" colspan="7" style="height: 1px">
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
