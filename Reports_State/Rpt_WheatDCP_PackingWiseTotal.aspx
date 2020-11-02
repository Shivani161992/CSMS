<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_WheatDCP_PackingWiseTotal.aspx.cs" Inherits="Admin_HOMPSCSC_Rpt_WheatDCP" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Wheat DCP RMS</title>
    <style type="text/css">
        .style1
        {
            height: 20px;
            width: 15px;
        }
        .style2
        {
            color: #0033CC;
            font-size: medium;
        }
        .style3
        {
            color: #0033CC;
            font-size: medium;
            height: 20px;
            width: 143px;
        }
        .style4
        {
            height: 20px;
            width: 157px;
        }
        .style5
        {
            height: 20px;
            width: 435px;
            font-weight: bold;
            font-size: small;
            color: #595f4a;
            font-family: Verdana;
            text-align: center;
        }
        .style6
        {
            height: 20px;
            width: 6%;
        }
        .style7
        {
            width: 58px;
        }
    </style>
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
        संभाग वार जिले की स्टॉक की जानकारी (गेहूँ)</span></td>
        </tr>
    <tr>
    <td style="width: 178px" > 
        <span style="font-size: 10pt; color: #3300ff">वर्ष चुने</span>
        <asp:DropDownList ID="ddlYear" runat="server" Width="114px" Height="19px">
            <asp:ListItem>2013-14</asp:ListItem>
            <asp:ListItem>2012-13</asp:ListItem>
            <asp:ListItem>2011-12</asp:ListItem>
        </asp:DropDownList></td>
    <td style="font-weight: bold;font-family: Verdana;text-align: center; width: 84px;" 
            class="style3"> 
        <span style="font-size: 10pt">संभाग चुनें</span></td>
    <td style="font-weight: bold;font-size: 9pt;color: #595f4a;font-family: Verdana;text-align: center; " 
                                  class="style1"> 
        <asp:DropDownList ID="ddlDivision" runat="server" Width="170px" Height="19px">
        
        </asp:DropDownList></td>
    <td style="font-weight: bold;font-size: 9pt;color: #595f4a;font-family: Verdana;text-align: right; width: 5%;" class="style6"> 
        <span class="style2" style="font-size: 10pt">माह चुनें</span> &nbsp;
    </td>
    <td style="font-weight: bold;font-size: 9pt;color: #595f4a;font-family: Verdana;text-align: left;" class="style4">
        <asp:DropDownList ID="ddlMonth" runat="server" Width="170px" Height="18px">
        
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
            text-align: left; width: 65px;" class="style7">
            <asp:Button ID="Button1" runat="server" Text="Generate Report" Width="109px" 
                Height="26px" onclick="Button1_Click" /></td>
    
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
