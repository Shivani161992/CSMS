<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="TruckChallan_Book.aspx.cs" Inherits="District_TruckChallan_Book" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>

    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }
    </style>

    <table align="center" style="width: 600px; border-style: solid; border-width: 1px; text-align: left;" border="1" cellspacing="0" cellpadding="6">
        <tr>
            <td colspan="4" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600"><strong>Truck Challan(T.C) Book For Rack</strong>

            </td>
        </tr>

        <tr>
            <td rowspan="3">&nbsp;</td>
            <td rowspan="">Book Number</td>
            <td>
                <asp:TextBox ID="txtBookNo" runat="server" Width="146px" AutoComplete="off" class="Number" MaxLength="8" Style="text-align: right" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtBookNo" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td rowspan="3">&nbsp;</td>
        </tr>
        <tr>
            <td>From Page</td>
            <td>
                <asp:TextBox ID="txtFrmPageNo" runat="server" Width="146px" AutoComplete="off" class="Number" MaxLength="3" Style="text-align: right" onfocus="this.select();" onmouseup="return false;" Enabled="False">1</asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFrmPageNo" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Upto Page</td>
            <td>
                <asp:TextBox ID="txtUptoPageNo" runat="server" Width="146px" AutoComplete="off" class="Number" MaxLength="3" Style="text-align: right" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUptoPageNo" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr style="text-align: center; font-size: large;">
            <td colspan="4">
                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                <asp:Button ID="btnRecptSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" OnClick="btnRecptSubmit_Click" />

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
            </td>
        </tr>

        <script lang="javascript" src="../PaddyMilling/Scripts/Number.js" type="text/javascript">       </script>
    </table>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

