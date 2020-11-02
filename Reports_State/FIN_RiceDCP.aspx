<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FIN_RiceDCP.aspx.cs" Inherits="Reports_State_FIN_RiceDCP" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Rice DCP</title>
  
</head>
<body>
    <form id="form1" runat="server">
      <table border="1"  cellpadding="0" cellspacing="0" height="700px" width="100%">
    <tr> <td valign="top">
    <table cellpadding="0" cellspacing="0"   style="background-color:#ecf5d5; width:100%; border:double;border-color:#868f6f">
        <tr>
            <td style="width: 228px">
            </td>
            <td colspan="4" style="font-weight: bold; font-size: medium; font-family: Verdana;
                text-align: center">
        संभाग वार जिले की स्टॉक की जानकारी</td>
            <td style="font-weight: bold; font-size: 9pt; color: #595f4a; font-family: Verdana;
                text-align: left; width: 132px;">
            </td>
            <td style="font-weight: bold; font-size: 9pt; color: #595f4a; font-family: Verdana;
                text-align: center">
            </td>
        </tr>
    <tr>
    <td style="width: 228px" >
    <span style="font-size: 10pt; color: #3300ff">वर्ष चुने</span>
        <asp:DropDownList ID="ddlYear" runat="server" Width="141px" Height="25px">
            <asp:ListItem>2013-14</asp:ListItem>
            <asp:ListItem>2012-13</asp:ListItem>
            <asp:ListItem>2011-12</asp:ListItem>
            <asp:ListItem>2010-11</asp:ListItem>
        </asp:DropDownList>
    
     </td>
    <td style="font-weight: bold;font-family: Verdana;text-align: center; font-size: medium;" > 
        संभाग चुनें</td>
    <td style="font-weight: bold;font-size: 9pt;color: #595f4a;font-family: Verdana;text-align: center;"> 
        <asp:DropDownList ID="ddlDivision" runat="server" Width="195px" Height="25px">
        
        </asp:DropDownList></td>
    <td style="font-weight: bold;font-size: 9pt;color: #595f4a;font-family: Verdana;text-align: right;"> 
        <span style="font-size: medium" >माह चुनें</span> &nbsp;
    </td>
    <td style="font-weight: bold;font-size: 9pt;color: #595f4a;font-family: Verdana;text-align: left; width: 195px;" >
        <asp:DropDownList ID="ddlMonth" runat="server" Width="180px" Height="25px">
        
          <asp:ListItem Value="0">--Select--</asp:ListItem>
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
        <td style="font-weight: bold; font-size: 9pt; color: #595f4a; font-family: Verdana;
            text-align: left; width: 132px;" >
            <asp:Button ID="Button1" runat="server" Text="Generate Report" Width="125px" 
                Height="24px" style="font-weight: 700" onclick="Button1_Click" /></td>
    
    <td style="font-weight: bold;font-size: 9pt;color: #595f4a;font-family: Verdana;text-align: center;" >
    <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="True" Font-Size="10pt" OnClick="LinkButton1_Click1" >पिछले पृष्ठ पर जाये</asp:LinkButton></td>
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
