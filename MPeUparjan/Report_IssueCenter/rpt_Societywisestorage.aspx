<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_Societywisestorage.aspx.cs" Inherits="Report_IssueCenter_rpt_Societywisestorage" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

 
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Society Wise Report</title>
    
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
            height: 43px;
            width: 215px;
        }
        .style2
        {
            height: 43px;
            width: 131px;
        }
        .style3
        {
            height: 43px;
            width: 35px;
        }
        .style4
        {
            height: 43px;
            width: 142px;
        }
    </style>
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
                                font-family: Arial; text-decoration: none">Storage Wise Deposite Report</span></td>
                        <td align="right" style="font-weight: bolder; font-size: 12pt; color: black; background-color: lavender;">
                            <span style="font-weight: 700; font-size: 11pt; color: #00008b; font-style: normal;
                                font-family: Arial; text-decoration: none">
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">पीछे जाए</asp:LinkButton></span>
                            </td>
                   
                    </tr>
                    <tr style="font-size: 12pt">
                        <td align="right" style="font-size: 10pt; " class="style2">
                            Select Crop</td>
                        <td align="left" style="font-size: 10pt; " class="style4">
                            <asp:DropDownList ID="ddlcomdty" runat="server" AutoPostBack="True" 
                                Height="23px" onselectedindexchanged="ddlcomdty_SelectedIndexChanged" 
                                Width="137px">
                            </asp:DropDownList>
                        </td>
                        <td align="left" colspan="2" style="font-size: 10pt; height: 43px">
                            &nbsp;</td>
                        <td class="style1">
                            <span style="font-size: 11pt"><strong>Select Procurement Name</strong></span></td>
                        <td style="height: 43px; text-align: left;">
                            <asp:DropDownList ID="ddlsociety" runat="server" Width="450px" Height="18px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="font-size: 12pt">
                        <td align="right" style="font-size: 10pt; " class="style2">
                            <asp:Label ID="lbl_date" runat="server" Text="दिनांक चुनें "  Width="100px"></asp:Label>
                          </td>
                        <td style="font-size: 10pt; " align="left" class="style4">
                            <asp:TextBox ID="txtDate" runat="server" Width="100px" ></asp:TextBox>
                            
                            <script type  ="text/javascript">
                                new tcal({
                                    'formname': 'form1',
                                    'controlname': 'txtDate'
                                });
	     </script>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate"
                                    ErrorMessage="कृपया दिनांक कलेंडर से चुनें" ValidationGroup="1">*</asp:RequiredFieldValidator>
                           
                        </td>
                        <td align="right" style="width: 201px; font-size: 10pt; height: 43px; text-align: right;">
                            <asp:Label ID="lbl_dateTill" runat="server" Text="दिनांक तक " Width="74px" ></asp:Label></td>
                        <td align="left" style="font-size: 10pt; " class="style3">
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
                                <td class="style1">
                            <asp:Button ID="Button1" runat="server" Text="View Selections" ValidationGroup="1" 
                                        Width="147px" onclick="Button1_Click"
                              /></td><td style="height: 43px"></td>
                        
                    </tr>
                                      <tr>
                        <td align="center" colspan="6" style="height: 3px">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                ValidationGroup="1" Width="168px" />
                        </td>
                    </tr>
                </table>
              <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="520px" ProcessingMode="Remote"
                        Width="100%" Font-Names="Verdana" Font-Size="8pt" ShowCredentialPrompts="False"
                        ZoomMode="PageWidth" BackColor="CornflowerBlue" BorderColor="#E0E0E0"
                         LinkDisabledColor="Black" LinkActiveColor="Yellow" Style="margin-left: 0px; margin-right: 0px;"
                            ShowBackButton="true" EnableTheming="true">
                        <ServerReport ReportServerUrl="" />
                    </rsweb:ReportViewer>
            </center>
    </div>
    </form>
</body>
</html>
