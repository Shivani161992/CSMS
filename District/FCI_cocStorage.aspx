<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FCI_cocStorage.aspx.cs" Inherits="District_FCI_cocStorage" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Storage</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellpadding="0" cellspacing="0"   style="background-color:#ecf5d5; width:100%; border:double;border-color:#868f6f">
        <tr>
            <td colspan="7" style="font-weight: bold; font-family: Verdana;
                text-align: center" class="style9">
                Carry Over Charges (Storage)</td>
        </tr>
        
    <tr>
    <td class="style10" > 
        Select Crop</td>
    <td style="font-weight: bold;font-family: Verdana;text-align: center; "  > 
        
        
                                        <asp:DropDownList ID="ddlcropyear" runat="server" Height="24px" Width="134px" >
                                        </asp:DropDownList>
        
        
        </td>
    <td style="font-weight: bold;font-size: 9pt;color: #595f4a;font-family: Verdana;text-align: center; " 
                                  class="style8" colspan="2"> Month</td>
        <td style="font-weight: bold; font-size: 9pt; color: #595f4a; font-family: Verdana;
            text-align: left;" class="style12">
           <asp:DropDownList ID="ddl_month" runat="server" Width="117px" Height="16px">
                    
                    <asp:ListItem Value="01">January</asp:ListItem>
                    <asp:ListItem Value="02">February</asp:ListItem>
                    <asp:ListItem Value="03">March</asp:ListItem>
                    <asp:ListItem Value="04">April</asp:ListItem>
                    <asp:ListItem Value="05">May</asp:ListItem>
                    <asp:ListItem Value="06">June</asp:ListItem>
                    <asp:ListItem Value="07">July</asp:ListItem>
                    <asp:ListItem Value="08">August</asp:ListItem>
                    <asp:ListItem Value="09">September</asp:ListItem>
                    <asp:ListItem Value="10">October</asp:ListItem>
                    <asp:ListItem Value="11">November</asp:ListItem>
                    <asp:ListItem Value="12">December</asp:ListItem>
                </asp:DropDownList>
           </td>
        <td style="font-weight: bold; font-size: 9pt; color: #595f4a; font-family: Verdana;
            text-align: left" class="style8">
            &nbsp;</td>
    
    <td style="font-weight: bold;font-size: 9pt;color: #595f4a;font-family: Verdana;text-align: left;" 
            class="style13">
                                        &nbsp;</td>
    </tr>
        
    <tr>
    <td class="style11" > 
        <span style="font-size: 10pt; color: #3300ff"></span>
        </td>
    <td style="font-weight: bold;font-family: Verdana;text-align: center; width: 174px;" 
            class="style3"> 
        &nbsp;</td>
    <td style="font-weight: bold;font-size: 9pt;color: #595f4a;font-family: Verdana;text-align: center; " 
                                  class="style1"> </td>
    <td style="font-weight: bold;font-size: 9pt;color: #595f4a;font-family: Verdana;text-align: right; " 
            class="style6"> 
        <span style="font-size: 10pt">
            <asp:Button ID="Button1" runat="server" Text="Generate Report" Width="121px" 
                Height="26px" onclick="Button1_Click" /></span>
    </td>
        <td style="font-weight: bold; font-size: 9pt; color: #595f4a; font-family: Verdana;
            text-align: left;" class="style12">
            </td>
        <td style="font-weight: bold; font-size: 9pt; color: #595f4a; font-family: Verdana;
            height: 20px; text-align: left">
        </td>
    
    <td style="font-weight: bold;font-size: 9pt;color: #595f4a;font-family: Verdana;text-align: left;" 
            class="style14">
    <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="True" Font-Size="10pt" Width="165px" OnClick="LinkButton1_Click" >पिछले पृष्ठ पर जाये</asp:LinkButton></td>
    </tr>
    </table>
    
     <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="650px" ProcessingMode="Remote" Width="100%" Font-Names="Verdana" Font-Size="8pt" ShowCredentialPrompts="False" SizeToReportContent="True"  BackColor="#e9f3ce" BorderColor="#E9F3CE"
                            LinkDisabledColor="Black" LinkActiveColor="Yellow" Style="margin-left: 0px; margin-right: 0px;"
                            ShowBackButton="true" EnableTheming="true" >
            <ServerReport ReportServerUrl="" />
        </rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
