<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stencile_Acceptance.aspx.cs" Inherits="Report_IssueCenter_Stencile_Acceptance" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Stencile and Steching Bags</title>
</head>
<body>
    <form id="form1" runat="server">
      <div>
     <center>
                <table border="1" cellpadding="0" cellspacing="0" style="border-style: double; border-width: 3;
                    padding: 1; border-collapse: collapse; border-color: Maroon; width: 100%; background-color:lightsteelblue;"
                    id="">
                    <tr>
                        <td align="center" colspan="5" style="font-weight: bolder; font-size: 12pt; color:Black">
                            <span style="font-weight: 700; font-size: 11pt; color: #00008b; font-style: normal;
                                font-family: Arial; text-decoration: none"> Stencile and Steching Bags Details Accoding
                                to Acceptance Note Details</span></td>
                        <td align="right" style="width: 163px; height: 23px; text-align: left;">
                            </td>
                   
                    </tr>
                    <tr>
                        <td align="left" style="height: 1px; font-size: 10pt; width: 52px;" colspan="2">
                            &nbsp;&nbsp; <span style="font-size: 12pt">Select Year</span></td>
                                <td align="right" style="vertical-align: middle; width: 1px; height: 1px; font-size: 12pt;">
                                    <asp:DropDownList ID="ddlyear" runat="server" Width="195px">
                                        <asp:ListItem>2016</asp:ListItem>
                                        <asp:ListItem>2015</asp:ListItem>
                                       
                                    </asp:DropDownList>&nbsp;
                        </td>
                        <td style="width: 300px; font-size: 12pt; height: 1px;" align="left">
                            &nbsp;</td>
                          <td align="left" style="font-size: 12pt; height: 1px;" >
                              &nbsp;</td>      
                    </tr>
                    <tr style="font-size: 12pt">
                        <td align="right" style="font-size: 10pt; height: 1px;" colspan="3">
                            <asp:Button ID="Button1" runat="server" Text="View Report" ValidationGroup="1" Width="147px"
                                OnClick="Button1_Click" /></td>
                        <td align="left" style="font-size: 10pt; width: 300px; height: 1px;">
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Font-Bold="True" ForeColor="#CC0033" Height="21px" Width="88px">पीछे जाए</asp:LinkButton>
                     
               <script type="text/javascript">
	             new tcal ({
				'formname': 'form1',
				'controlname': 'txtDateTill'
	                      });
                                </script>
                                </td>
                                <td style="height: 1px;">
                            </td><td style="height: 1px"></td>
                        
                    </tr>
                                      <tr>
                        <td align="center" colspan="6" style="height: 1px">
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
