<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Delete_DoOpenSale.aspx.cs" Inherits="District_Delete_DoOpenSale" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: center">
    <center>
        <table style="width: 532px; height: 388px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
            <tr>
                <td style="height: 1px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; width: 33px; border-bottom: black thin groove; text-align: left;" colspan="2">
                    <span style="font-size: 11pt; color: #6600cc">जो डिलीवरी आर्डर को केन्द्र से जारी नहीं
                        किया गया है , केवल वही डिलीट होंगे</span></td>
            </tr>
            <tr>
                <td style="width: 33px; height: 1px; text-align: left; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                    <asp:Label ID="Label1" runat="server" Text="Select Issue Type" Width="159px"></asp:Label></td>
                <td style="width: 69px; height: 1px; text-align: left; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                    <asp:DropDownList ID="ddlIssueType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlIssueType_SelectedIndexChanged"
                        Width="158px">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 33px; height: 1px; text-align: left; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                    <asp:Label ID="Label2" runat="server" Text="Select DO Number" Width="158px"></asp:Label></td>
                <td style="width: 69px; height: 1px; text-align: left; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                    <asp:DropDownList ID="ddlDONumber" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDONumber_SelectedIndexChanged"
                        Width="164px">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 33px; height: 1px; text-align: left; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                    <asp:Label ID="Label3" runat="server" Text="DO Date" Width="121px"></asp:Label></td>
                <td style="width: 69px; height: 1px; text-align: left; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                    <asp:Label ID="lbldate" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 33px; height: 1px; text-align: left; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                    <asp:Label ID="Label4" runat="server" Text="DO Quantity" Width="121px"></asp:Label></td>
                <td style="width: 69px; height: 1px; text-align: left; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                    <asp:Label ID="lblQty" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 33px; height: 1px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; text-align: left;">
                    <asp:Label ID="Label5" runat="server" Text="Commodity"></asp:Label></td>
                <td style="width: 69px; height: 1px; text-align: left; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                    <asp:Label ID="lblcommodity" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="height: 1px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; width: 33px; border-bottom: black thin groove; text-align: left;" colspan="2">
                </td>
            </tr>
            <tr>
                <td style="width: 33px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 1px; text-align: center;">
                    <asp:Button ID="btnclose" runat="server" OnClick="btnclose_Click" Text="Close" /></td>
                <td style="width: 69px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 1px; text-align: center;">
                    <asp:Button ID="btndelete" runat="server" OnClick="btndelete_Click" Text="Delete" /></td>
            </tr>
        </table>
        
        </center>
    </div>
</asp:Content>

