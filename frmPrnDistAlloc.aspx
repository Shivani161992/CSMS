<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPrnDistAlloc.aspx.vb" Inherits="frmPrnDistAlloc" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>State Level Allocation Report</title>

<script language="javascript" type="text/javascript">
// <!CDATA[



// ]]>
</script>
</head>
<body>
    <form id="form1" runat="server">
    <table border="1" style="border-left-color: maroon; border-bottom-color: maroon;
            width: 744px; border-top-style: double; border-top-color: maroon; border-right-style: double;
            border-left-style: double; border-collapse: collapse; background-color: #ece9d8;
            border-right-color: maroon; border-bottom-style: double">
            <tr>
                <td style="width: 46px">
        <asp:LinkButton ID="LinkButton1" runat="server" Width="40px" PostBackUrl="~/frmStAlloc1.aspx">Back</asp:LinkButton></td>
                <td align="center" colspan="4" style="font-weight: bold; font-size: 15pt; color: teal">
                    State Level Allocation Details in MP</td>
                <td style="width: 100px">
        <asp:LinkButton ID="LinkButton2" runat="server">Logout</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 46px">
                    Month</td>
                <td style="width: 100px">
                    <asp:DropDownList ID="DDL_month" runat="server" meta:resourcekey="DDL_monthResource1" Width="123px">
                    </asp:DropDownList></td>
                <td style="width: 39px">
                    Year</td>
                <td style="width: 133px">
                    <asp:DropDownList ID="DDL_Year" runat="server" meta:resourcekey="DDL_YearResource1" Width="125px">
                    </asp:DropDownList></td>
                <td style="width: 422px">
                    </td>
                <td style="width: 100px">
                </td>
            </tr>
        <tr>
            <td style="width: 46px">
            </td>
            <td style="width: 100px">
            </td>
            <td style="width: 39px">
            </td>
            <td style="width: 133px">
            </td>
            <td style="width: 422px">
                    <asp:Button ID="Button1" runat="server" Text="State Report" Width="112px" /></td>
            <td style="width: 100px">
            </td>
        </tr>
        </table>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="440px" ProcessingMode="Remote" Width="744px" Font-Names="Verdana" Font-Size="8pt" ShowCredentialPrompts="False">
            <ServerReport ReportServerUrl=""  />
        </rsweb:ReportViewer>
    </form>
</body>
</html>
