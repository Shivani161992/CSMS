<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CategoryWise_WheatDeposit.aspx.cs" Inherits="Reports_State_CategoryWise_WheatDeposit" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Category Wise Wheat</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <center>
                <table border="1" cellpadding="0" cellspacing="0" style="border-style: double; border-width: 3;
                    padding: 1; border-collapse: collapse; border-color: Maroon; width: 100%; background-color:lavender;"
                    id="">
                    <tr>
                        <td align="center" colspan="4" 
                            style="font-weight: bolder; font-size: 12pt; color:Black; background-color: lavender; text-align: center;">
                            Category Wise Wheat Deposit Report (2015)</td>
                        <td align="right" style="font-weight: bolder; font-size: 12pt; color: black; background-color: lavender;">
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">पीछे जाए</asp:LinkButton></td>
                   
                    </tr>
                    <tr style="font-size: 12pt">
                        <td align="right" style="font-size: 10pt; height: 43px;">
                            
                          </td>
                        <td align="right" style="width: 256px; font-size: 10pt; height: 43px;">
                            <%--<asp:Label ID="lbl_dateTill" runat="server" Text="दिनांक तक " Width="74px" ></asp:Label>--%>
                            </td>
                        <td align="left" style="font-size: 10pt; width: 167px; height: 43px;">
                            <%--<asp:TextBox ID="txtDateTill" runat="server"  Width="100px"></asp:TextBox>
                     
               <script type="text/javascript">
                   new tcal({
                       'formname': 'form1',
                       'controlname': 'txtDateTill'
                   });
                                </script>--%>

                           
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDateTill"
                                ErrorMessage="कृपया दिनांक कलेंडर से चुनें" ValidationGroup="2" Visible="False">*</asp:RequiredFieldValidator>--%>
                               
                           
                                </td>
                                <td style="height: 43px; width: 295px;">
                            <asp:Button ID="Button1" runat="server" Text="View Report" ValidationGroup="1" 
                                        Width="147px" onclick="Button1_Click" /></td><td style="height: 43px"></td>
                        
                    </tr>
                                      <tr>
                        <td align="center" colspan="5" style="height: 4px">
                            &nbsp;</td>
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
