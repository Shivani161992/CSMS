<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="PDSMO_SDN.aspx.cs" Inherits="State_PDSMO_SDN" %>

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
    </style>


    <%--For Calendar Controls--%>
    <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="../PaddyMilling/calendar/calendar.js"></script>

    <table align="center" style="width: 620px; border-style: solid; border-width: 1px; text-align: left" border="1" cellspacing="0" cellpadding="6">
        <tr>
            <td style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600; font-weight: bold" colspan="4">Print SDN</td>
        </tr>
        <tr>

            <td class="auto-style1" rowspan="2">
                <asp:RadioButton ID="rdbSendDist" runat="server" GroupName="Dist" style="font-weight: 700; color: #0000FF;" Text="Send Dist" AutoPostBack="True" OnCheckedChanged="rdbSendDist_CheckedChanged" />
                <br />
                <asp:RadioButton ID="rdbRecdDist" runat="server" GroupName="Dist" style="font-weight: 700; color: #0000FF;" Text="Recd Dist" AutoPostBack="True" OnCheckedChanged="rdbRecdDist_CheckedChanged" />
            </td>

            <td class="auto-style1">From Date<br />
                <asp:TextBox ID="txtFromDate" runat="server" Width="100px" ReadOnly="True" ></asp:TextBox>
                <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtFromDate' ,'expiry=false')" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFromDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td class="auto-style2">To Date
                <br />
                <asp:TextBox ID="txtToDate" runat="server" Width="100px" ReadOnly="True"></asp:TextBox>
                <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtToDate' ,'expiry=false' )" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtToDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" BackColor="#FF6937" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="25px" Text="Search" Width="115px" CssClass="ButtonClass" OnClick="btnSearch_Click" /></td>

        </tr>
        <tr>
            <td>
                <strong>MO Number</strong>
                <asp:DropDownList ID="ddlMvmtNumber" runat="server" Width="150px"  AutoPostBack="True" OnSelectedIndexChanged="ddlMvmtNumber_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
            <td>
                <strong>Received Dist</strong>
                <asp:DropDownList ID="ddlRecdDist" runat="server" Width="150px" >
                </asp:DropDownList>

            </td>
            <td>

                <asp:Button ID="btnPrint" runat="server" BackColor="#FF6937" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="25px" Text="Print" Width="115px" CssClass="ButtonClass" OnClick="btnPrint_Click" />

            </td>
        </tr>

    </table>
</asp:Content>

