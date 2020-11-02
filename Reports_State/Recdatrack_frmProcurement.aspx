<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Recdatrack_frmProcurement.aspx.cs" Inherits="Reports_State_Recdatrack_frmProcurement" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Recd at Rack</title>
</head>
<body>
    <form id="form1" runat="server">
     <table border="1"  cellpadding="0" cellspacing="0" height="700px" width="100%">
    <tr> <td valign="top">
    <table cellpadding="0" cellspacing="0"   style="background-color:#ecf5d5; width:100%; border:double;border-color:#868f6f">
        <tr>
            <td colspan="7" style="font-weight: bold; font-size: 9pt; color: #595f4a; font-family: Verdana;
                height: 20px; text-align: center">
                <span style="font-size: 11pt">
        जिले वार खरीदी केन्द्र से भेजे गए ट्रक की रैक पॉइंट पर
                    प्राप्ति की जानकारी</span></td>
        </tr>
    <tr>
    <td style="width: 178px" > 
        <span style="font-size: 10pt; color: #3300ff"></span>
        </td>
    <td style="font-weight: bold;font-family: Verdana;text-align: center; width: 84px;" 
            class="style3"> 
        <span style="font-size: 10pt">
            <asp:Button ID="Button1" runat="server" Text="Generate Report" Width="121px" 
                Height="26px" onclick="Button1_Click" /></span></td>
    <td style="font-weight: bold;font-size: 9pt;color: #595f4a;font-family: Verdana;text-align: center; " 
                                  class="style1"> </td>
    <td style="font-weight: bold;font-size: 9pt;color: #595f4a;font-family: Verdana;text-align: right; width: 5%;" class="style6"> 
        <span class="style2" style="font-size: 10pt"></span> &nbsp;
    </td>
    <td style="font-weight: bold;font-size: 9pt;color: #595f4a;font-family: Verdana;text-align: left;" class="style4">
        </td>
        <td style="font-weight: bold; font-size: 9pt; color: #595f4a; font-family: Verdana;
            text-align: left; width: 65px;" class="style7">
            </td>
    
    <td style="font-weight: bold;font-size: 9pt;color: #595f4a;font-family: Verdana;height: 20px;text-align: left;">
    <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="True" Font-Size="10pt" Width="116px" OnClick="LinkButton1_Click" >पिछले पृष्ठ पर जाये</asp:LinkButton></td>
    </tr>
    </table>
    
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="650px" ProcessingMode="Remote" Width="100%" Font-Names="Verdana" Font-Size="8pt" ShowCredentialPrompts="False" SizeToReportContent="True"  BackColor="#e9f3ce" BorderColor="#E9F3CE"
                            LinkDisabledColor="Black" LinkActiveColor="Yellow" Style="margin-left: 0px; margin-right: 0px;"
                            ShowBackButton="true" EnableTheming="true" >
            <ServerReport ReportServerUrl="" />
        </rsweb:ReportViewer>
        </td></tr>
        </table>
    </form>
</body>
</html>
