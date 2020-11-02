<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Reprint_Dhan_Challan.aspx.cs" Inherits="IssueCenter_Reprint_Dhan_Challan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>

    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }

        .auto-style3 {
            text-decoration: underline;
        }
    </style>


    <%--For Calendar Controls--%>
    <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="../PaddyMilling/calendar/calendar.js"></script>

    <table align="center" style="width: 700px; border-style: solid; border-width: 1px; text-align: left" border="1" cellspacing="0" cellpadding="6">
        <tr>
            <td colspan="4" style="text-align: center; font-size: large; color: #CC6600; font-weight: bold" class="auto-style3">Reprint Paddy Challan</td>
            <input id="hdfCropYear" type="hidden" runat="server" />
        </tr>
        <tr>
           
            <td >Mill Name</td>
           
            <td >
                <asp:DropDownList ID="ddlMillName" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlMillName_SelectedIndexChanged"  >
                </asp:DropDownList>
            </td>
            <td >Agreement Number</td>
            <td>
                <asp:DropDownList ID="ddlAgrmtNo" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlAgrmtNo_SelectedIndexChanged"  >
                </asp:DropDownList>
            </td>
          
        </tr>

        <tr>
            <td colspan="3" ><strong Style="margin-left: 70px" >Challan Number</strong>
                <asp:DropDownList ID="ddlMvmtNumber" runat="server" Width="170px" Style="margin-left: 10px" >
                </asp:DropDownList>
            </td>
            <td>

                <asp:Button ID="btnPrint" runat="server" BackColor="#FF6937" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="25px" Text="Print" Width="115px" CssClass="ButtonClass" OnClick="btnPrint_Click" />

            </td>
        </tr>

    </table>
</asp:Content>

