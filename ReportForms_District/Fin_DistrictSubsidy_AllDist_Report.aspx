<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Fin_DistrictSubsidy_AllDist_Report.aspx.cs" Inherits="ReportForms_District_Fin_DistrictSubsidy_AllDist_Report" Title="Untitled Page" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 500px; height: 20px">
        </td>
        <td align="left" style="width: 500px">
        </td>
    </tr>
    <tr>
        <td style="height: 20px; font-size: 14px;" colspan="2">
            Information Of DCP Stock</td>
    </tr>
<tr>
<td style="width:500px; height: 25px;">
    Year</td>
<td style="width:500px;" align="left">
    <asp:DropDownList ID="ddlcropyear" runat="server" Width="200px">
    </asp:DropDownList></td>
</tr>
    <tr>
        <td style="width: 500px; height: 25px">
            Month</td>
        <td align="left" style="width: 500px">
            <asp:DropDownList ID="ddlmonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlmonth_SelectedIndexChanged"
                Width="200px">
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td style="width: 500px; height: 25px;">
            Crop</td>
        <td align="left" style="width: 500px">
            <asp:DropDownList ID="ddlcrop" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlcrop_SelectedIndexChanged">
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td style="width: 500px; height: 20px;">
        </td>
        <td align="left" style="width: 500px">
        </td>
    </tr>
    <tr>
        <td colspan="2" style="width: 1000px;">
      <asp:Panel ID="pnlreport" runat="server" Visible="false">
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="520px" ProcessingMode="Remote"
                        Width="100%" Font-Names="Verdana" Font-Size="8pt" ShowCredentialPrompts="False"
                        ZoomMode="PageWidth" BackColor="CornflowerBlue" BorderColor="#E0E0E0"
                         LinkDisabledColor="Black" LinkActiveColor="Yellow" Style="margin-left: 0px; margin-right: 0px;"
                            ShowBackButton="true" EnableTheming="true">
                        <ServerReport ReportServerUrl="" />
                    </rsweb:ReportViewer>
                </asp:Panel>
            </td>
    </tr>
    <tr>
        <td colspan="2" style="width: 1000px; height: 25px">
        
    </tr>
    <tr>
        <td colspan="2" style="width: 1000px">
           </td>
    </tr>
</table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

