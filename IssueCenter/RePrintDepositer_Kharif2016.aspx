<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="RePrintDepositer_Kharif2016.aspx.cs" Inherits="IssueCenter_RePrintDepositer_Kharif2016" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>

    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }
    </style>


    <%--For Calendar Controls--%>
    <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="../PaddyMilling/calendar/calendar.js"></script>

    <table align="center" style="width: 600px; border-style: solid; border-width: 1px; text-align: left" border="1" cellspacing="0" cellpadding="6">
        <tr>
            <td colspan="4" style="text-align: center"><span style="font-size: large; text-decoration: underline; color: #CC6600; font-weight: bold">Reprint Depositer Form (Kharif-2016)</span></td>
        </tr>
        <tr>

            <td class="auto-style1">From Date</td>

            <td class="auto-style1">
                <asp:TextBox ID="txtFromDate" runat="server" Width="133px" ReadOnly="True"></asp:TextBox>
                <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtFromDate' ,'expiry=false')" /><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFromDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td class="auto-style2">To Date</td>
            <td class="auto-style3">
                <asp:TextBox ID="txtToDate" runat="server" Width="133px" ReadOnly="True"></asp:TextBox>
                <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtToDate' ,'expiry=false' )" /><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtToDate" ErrorMessage="RequiredFieldValidator" Font-Bold="False" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>

        </tr>
        <tr>

            <td class="auto-style1">Commodity
            </td>

            <td class="auto-style1">
                <asp:DropDownList ID="ddlCommodity" runat="server" Width="137" OnSelectedIndexChanged="ddlCommodity_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </td>
            <td class="auto-style2">Branch</td>
            <td class="auto-style3">
                <asp:DropDownList ID="ddlBranch" runat="server" Width="137" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </td>

        </tr>

        <tr>

            <td class="auto-style1">Godown</td>

            <td class="auto-style1" colspan="3">
                <asp:DropDownList ID="ddlGodown" runat="server" Width="248" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </td>

        </tr>


        <tr>

            <td class="auto-style1">
                <strong>Depositer Form No.</strong></td>

            <td colspan="3">
                <asp:DropDownList ID="ddlReceiptID" runat="server" Width="248px">
                </asp:DropDownList>

                <asp:Button ID="btnPrint" runat="server" BackColor="#FF6937" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="25px" Text="Print" Width="137px" CssClass="ButtonClass" OnClick="btnPrint_Click" Style="margin-left: 5px;" />

            </td>

        </tr>



    </table>
</asp:Content>


