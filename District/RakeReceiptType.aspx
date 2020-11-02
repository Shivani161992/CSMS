<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="RakeReceiptType.aspx.cs" Inherits="District_RakeReceiptType" Title="Select Rake Receipt Type" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 620px; border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid;">
        <tr>
            <td align="center" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
    <table style="background-color: #cfdcdc; border-right: navy 1px double; border-top: navy 1px double; font-size: 10pt; border-left: navy 1px double; border-bottom: navy 1px double; position: static;" border="1" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="3" style="color: #0066ff; font-style: italic; height: 36px; background-color: lightslategray;">
                <asp:Label ID="lbldotype" runat="server" Text="Please Select Type of Rake Receipt" Font-Size="14px" Font-Bold="True" Font-Italic="True" ForeColor="White"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 100px; height: 53px;" align="right">
                <asp:Label ID="lbldoselect" runat="server" Text="Rake Receipt From" Font-Size="12px"></asp:Label></td>
            <td style="width: 100px; height: 53px;">
                <asp:DropDownList ID="ddlraketype" runat="server" OnSelectedIndexChanged="ddlraketype_SelectedIndexChanged">
                    <asp:ListItem Value="S">Sugar Factory</asp:ListItem>
                    <asp:ListItem Value="O">Own District</asp:ListItem>                  
                </asp:DropDownList></td>
            <td style="width: 100px; height: 53px;">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 100px; height: 52px;">
                &nbsp;</td>
            <td align="center">
                <asp:Button ID="btnok" runat="server" OnClick="btnok_Click" Text="OK" Width="94px" /></td>
            <td style="width: 100px; height: 52px;">
                &nbsp;</td>
        </tr>
    </table>
            </td>
        </tr>
    </table>
</asp:Content>

