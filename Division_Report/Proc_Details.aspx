<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Proc_Details.aspx.cs" Inherits="Division_Report_Proc_Details" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Procurement Details</title>
    
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

    
    <style type="text/css">
        .style1
        {
            height: 32px;
        }
        .style2
        {
            width: 100%;
        }
        .style3
        {
            height: 30px;
            text-align: left;
        }
        .style4
        {
            height: 30px;
            width: 149px;
        }
        .style5
        {
            height: 21px;
            width: 1048px;
        }
        .style6
        {
            height: 32px;
            width: 1048px;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center>
                <table border="1" cellpadding="0" cellspacing="0" style="border-style: double; border-width: 3;
                    padding: 1; border-collapse: collapse; border-color: Maroon; width: 100%; background-color: #ece9d8;"
                    id="">
                    <tr>
                        <td align="center" style="font-weight: bolder; font-size: 10pt; color: black;
                            background-color: #ece9d8;" class="style5">
                            Procurement Details between two Dates</td>
                        <td style="background-color: #ece9d8;">
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Back</asp:LinkButton></td>
                    </tr>
                    
                    <tr>
                        <td align="center" style="font-weight: bolder; font-size: 10pt; color: black;
                            background-color: #ece9d8;" class="style5">
                            <table class="style2">
                                <tr>
                                    <td class="style3">
                                        Select From Date
                                    </td>
                                    <td class="style3">
                                        <asp:TextBox ID="frmdate" runat="server">26-03-2015</asp:TextBox>
                                        
                                        <script type  ="text/javascript">
                                            new tcal({
                                                'formname': 'form1',
                                                'controlname': 'frmdate'
                                            });
	     </script>
                                        
                                    </td>
                                    <td class="style3">
                                        Select To Date
                                    </td>
                                    <td class="style3">
                                        <asp:TextBox ID="todate" runat="server"></asp:TextBox>
                                        
                                        <script type  ="text/javascript">
                                            new tcal({
                                                'formname': 'form1',
                                                'controlname': 'todate'
                                            });
	     </script>
                                    </td>
                                    <td class="style4">
                                        Select Commodity</td>
                                    <td class="style3">
                                        <asp:DropDownList ID="ddlcommmodity" runat="server" Height="24px" Width="134px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="background-color: #ece9d8;">
                            &nbsp;</td>
                    </tr>
                    
                    <tr>
                        <td align="center" style="font-weight: bolder; font-size: 10pt; color: black;
                            background-color: #ece9d8;" class="style6">
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Show" 
                                Width="106px" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td style="background-color: #ece9d8;" class="style1">
                            </td>
                    </tr>
                    
                </table>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="1000px" ProcessingMode="Remote"
                    Width="100%" Font-Names="Verdana" Font-Size="8pt" ShowCredentialPrompts="False"
                    ZoomMode="PageWidth" ZoomPercent="50"  BorderColor="#E0E0E0"
                         LinkDisabledColor="Black" LinkActiveColor="Yellow" Style="margin-left: 0px; margin-right: 0px;"
                            ShowBackButton="true" EnableTheming="true">
                    <ServerReport ReportServerUrl="" />
                </rsweb:ReportViewer>
            </center>
        </div>
    </form>
</body>
</html>
