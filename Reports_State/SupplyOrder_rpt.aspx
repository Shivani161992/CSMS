<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SupplyOrder_rpt.aspx.cs" Inherits="Reports_State_SupplyOrder_rpt" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
     <title>Supply Order details</title>
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
                                font-family: Arial; text-decoration: none">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;Statement of Zone
                                Wise - Supply order (Dispatch to Issue Centers)</span></td>
                        <td align="right" style="font-weight: bolder; font-size: 12pt; color: black; background-color: lavender;">
                            </td>
                   
                    </tr>
                    <tr>
                        <td align="left" style="height: 24px; font-size: 12pt; width: 139px; text-align: center; vertical-align: middle; background-color: lavender;" colspan="2">
                            <span style="font-size: 12pt">
                                <asp:Label ID="Label1" runat="server" Text="Select Order No:"></asp:Label></span></td>
                                <td align="right" style="vertical-align: middle; width: 201px; height: 24px; font-size: 12pt; background-color: lavender; text-align: left;">
                                    &nbsp;
                                    <asp:DropDownList ID="ddlOrderno" runat="server" Width="168px">
                                    </asp:DropDownList></td>
                        <td style="width: 167px; font-size: 12pt; height: 24px; vertical-align: middle; background-color: lavender;" align="left">
                            &nbsp;</td>
                          <td align="left" style="font-size: 12pt; height: 24px; width: 295px; vertical-align: middle; background-color: lavender;" >
                              &nbsp;<asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Font-Bold="True" ForeColor="#000066">पीछे जाए</asp:LinkButton></td>      
                    </tr>
                    <tr style="font-size: 12pt">
                        <td align="right" style="width: 24px; font-size: 10pt; height: 43px;">
                            <asp:Label ID="lbl_date" runat="server" Text="दिनांक चुनें "  Width="100px"></asp:Label>
                          </td>
                        <td style="width: 182px; font-size: 10pt; height: 43px;" align="left">
                            <asp:TextBox ID="txtDate" runat="server" Width="100px"></asp:TextBox>
                            
                            <script type  ="text/javascript">
                                                	new tcal ({
				'formname': 'form1',
				'controlname': 'txtDate'
	    });
	     </script>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate"
                                    ErrorMessage="कृपया दिनांक कलेंडर से चुनें" ValidationGroup="1">*</asp:RequiredFieldValidator>
                           
                        </td>
                        <td align="right" style="width: 201px; font-size: 10pt; height: 43px; text-align: right;">
                            <asp:Label ID="lbl_dateTill" runat="server" Text="दिनांक तक " Width="74px" ></asp:Label></td>
                        <td align="left" style="font-size: 10pt; width: 167px; height: 43px;">
                            <asp:TextBox ID="txtDateTill" runat="server"  Width="100px"></asp:TextBox>
                     
               <script type="text/javascript">
	             new tcal ({
				'formname': 'form1',
				'controlname': 'txtDateTill'
	                      });
                                </script>

                           
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDateTill"
                                ErrorMessage="कृपया दिनांक कलेंडर से चुनें" ValidationGroup="2" Visible="False">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="height: 43px; width: 295px;">
                            <asp:Button ID="Button1" runat="server" Text="View Report" ValidationGroup="1" Width="147px"
                                OnClick="Button1_Click" /></td><td style="height: 43px"></td>
                        
                    </tr>
                                      <tr>
                        <td align="center" colspan="6" style="height: 3px">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                ValidationGroup="1" Width="168px" />
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="pnlreport" runat="server" Visible="false">
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="742px" ProcessingMode="Remote"
                        Width="1288px" Font-Names="Verdana" Font-Size="8pt" ShowCredentialPrompts="False"
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

