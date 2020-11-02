<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Reprint_CMRDO_IC.aspx.cs" Inherits="PaddyMilling_Reprint_CMRDO_IC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }

        .auto-style3 {
            text-decoration: underline;
        }
    </style>

    <table align="center" style="width: 700px; border-style: solid; border-width: 1px; text-align: left" border="1" cellspacing="0" cellpadding="6">
        <tr>
            <td colspan="4" style="text-align: center; font-size: large; color: #CC6600; font-weight: bold"><span class="auto-style3">Reprint CMR Deposit Order</span></td>
            <input id="hdfCropYear" type="hidden" runat="server" />
        </tr>
        <tr>

            <td>Mill Name</td>

            <td>
                <asp:DropDownList ID="ddlMillName" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlMillName_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>Agreement Number</td>
            <td>
                <asp:DropDownList ID="ddlAgrmtNo" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlAgrmtNo_SelectedIndexChanged">
                </asp:DropDownList>
            </td>

        </tr>

        <tr>
            <td colspan="3"><strong style="margin-left: 70px">CMR Deposit Order No</strong>
                <asp:DropDownList ID="ddlMvmtNumber" runat="server" Width="170px" Style="margin-left: 10px">
                </asp:DropDownList>
            </td>
            <td>

                <asp:Button ID="btnPrint" runat="server" BackColor="#FF6937" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="25px" Text="Print" Width="115px" CssClass="ButtonClass" OnClick="btnPrint_Click" />

            </td>
        </tr>

    </table>
</asp:Content>

