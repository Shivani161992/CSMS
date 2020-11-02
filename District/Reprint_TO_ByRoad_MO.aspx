<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Reprint_TO_ByRoad_MO.aspx.cs" Inherits="District_Reprint_TO_ByRoad_MO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>

    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }
        .auto-style1 {
            width: 228px;
        }
        .auto-style2 {
            width: 210px;
        }
        .auto-style3 {
            width: 108px;
            text-align:center;
        }
    </style>


    <%--For Calendar Controls--%>
    <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="../PaddyMilling/calendar/calendar.js"></script>

    <table align="center" style="width: 600px; border-style: solid; border-width: 1px; text-align: left" border="1" cellspacing="0" cellpadding="6">
        <tr>
            <td colspan="3" style="text-align:center"><span style=" font-size: large; text-decoration: underline; color: #CC6600; font-weight: bold">Reprint Transport Order Against HO PDS Movement Order</span> <span style="font-size: large; color: #CC6600; font-weight: bold">(By Road)</span></td>
        </tr>
        <tr>
           
            <td class="auto-style1">From Date<asp:TextBox ID="txtFromDate" runat="server" Width="100px" ReadOnly="True" Style="margin-left: 10px"></asp:TextBox>
                <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtFromDate' ,'expiry=false')" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFromDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td class="auto-style2">To Date<asp:TextBox ID="txtToDate" runat="server" Width="100px" ReadOnly="True" Style="margin-left: 10px"></asp:TextBox>
                <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtToDate' ,'expiry=false' )" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtToDate" ErrorMessage="RequiredFieldValidator" Font-Bold="False" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td class="auto-style3">
                <asp:Button ID="btnSearch" runat="server" BackColor="#FF6937" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="25px" Text="Search" Width="115px" CssClass="ButtonClass" OnClick="btnSearch_Click" /></td>
          
        </tr>
        <tr>
            <td colspan="2" style="text-align: center"  >
                <strong>TO Number</strong>
                   <asp:DropDownList ID="ddlTONumber" runat="server" Width="150px" Style="margin-left: 10px">
                </asp:DropDownList>
            </td>
            <td class="auto-style3">

                <asp:Button ID="btnPrint" runat="server" BackColor="#FF6937" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="25px" Text="Print" Width="115px" CssClass="ButtonClass" OnClick="btnPrint_Click" />

            </td>
        </tr>



    </table>
</asp:Content>

