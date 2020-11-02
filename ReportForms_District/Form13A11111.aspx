<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Form13A11111.aspx.vb" Inherits="ReportForms_District_Form13A" %>

 
 
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Form 13 - A</title>


</head>
<body>
    <form id="form1" runat="server">
        <table border="1" cellpadding="0" cellspacing="0" style="border-style:double; border-width:3; padding:1; BORDER-COLLAPSE: collapse; border-color :Maroon  ; width: 800px; background-color: #ece9d8;" id="">
            <tr>
                <td align="center" colspan="5" style="font-weight: bolder; font-size: 15pt; color: teal">
                    Daily Stock Transfer Position(Form-13(A))</td>
            </tr>
            <tr>
                <td style="vertical-align: middle; width: 228px; height: 23px">
                    District Logged In</td>
                <td style="width: 100px; height: 23px">
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="MediumBlue"></asp:Label></td>
                <td align="right" style="vertical-align: middle; width: 93px; height: 23px">
                </td>
                <td align="left">
                    </td>
                <td align="left" style="width: 417px; height: 23px">
                    </td>
            </tr>
            <tr>
                <td style="width: 228px; height: 23px; vertical-align: middle;">
                    &nbsp;Date:</td>
                <td style="width: 100px; height: 23px;">
                    &nbsp;<asp:TextBox ID="tx_from_date" runat="server" AutoPostBack="True" Width="171px"></asp:TextBox></td>
                <td align="right" style="width: 93px; height: 23px; vertical-align: middle;">
                    &nbsp;
                </td>
                <td style="width: 92px; height: 23px;">
                    &nbsp;</td>
                <td style="width: 417px; height: 23px;" align="right">
                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="False" Font-Size="Large"
                        NavigateUrl="~/frmReports.aspx">Back</asp:HyperLink></td>
            </tr>
            <tr>
                <td style="width: 228px; height: 25px;">
                    Challan No</td>
                <td style="width: 100px; height: 25px;">
                    <asp:DropDownList ID="ddlchallanno" runat="server" Width="179px">
                    </asp:DropDownList></td>
                <td style="width: 93px; height: 25px;">
                </td>
                <td style="width: 92px; height: 25px;">
                </td>
                <td align="right" style="width: 417px; height: 25px;">
                    <asp:Button ID="Button1" runat="server" Text="View Report" /></td>
            </tr>
            <tr>
                <td colspan="5">
                    &nbsp;<asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label></td>
            </tr>
        </table>
        &nbsp;&nbsp;
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="800px">
        </rsweb:ReportViewer>
    </form>
</body>
</html>
