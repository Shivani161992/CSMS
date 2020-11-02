<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master"
    AutoEventWireup="true" CodeFile="DeliveryChallan_IssuedCentre.aspx.cs" Inherits="IssueCenter_DeliveryChallan_IssuedCentre" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>
    <script type="text/javascript" lang="javascript" src="../js/calendarMvmt.js"></script>
    <%--For Calendar Controls--%>
    <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="../js/calendarMvmt.js"></script>
    <%--Allow Only 2 Digit After Decimal--%>
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/Number2D.js"></script>
    <style type="text/css">
        .ButtonClass
        {
            cursor: pointer;
        }
    </style>
    <style type="text/css">
        .hiddencol
        {
            display: none;
        }
    </style>
    <script type="text/javascript">
        window.onload = function () {
            var div = document.getElementById("dvScroll");
            var div_position = document.getElementById("div_position");
            var position = parseInt('<%=Request.Form["div_position"] %>');
            if (isNaN(position)) {
                position = 0;
            }
            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            };
        };
    </script>
    <div id="dvScroll" style="overflow-y: scroll; height: 500px;">
        <table align="center" style="width: 760px; border-style: solid; border-width: 1px;
            text-align: left;" border="1" cellspacing="0" cellpadding="6">
            <tr>
                <td colspan="6" style="text-align: center; font-size: large; text-decoration: underline;
                    color: #CC6600">
                    <strong>Delivery Challan(Issue Centre)</strong>
                    <input id="hdfGunnyType" type="hidden" runat="server" />
                    <input id="hdfGridIC_ID" type="hidden" runat="server" />
                </td>
            </tr>
            <tr>
                <td rowspan="22">
                </td>
                <td colspan="4" style="text-align: center; background-color: #CCCCCC">
                    <asp:Label ID="lblchallanno" runat="server" Style="text-align: center; font-weight: 700;"
                        ForeColor="Blue" Visible="False"></asp:Label>
                </td>
                <td rowspan="22">
                </td>
            </tr>
            <tr>
                <td>
                    Crop Year
                </td>
                <td>
                    <asp:DropDownList ID="ddlCropYear" runat="server" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="ddlCropYear_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    Transport Order
                </td>
                <td>
                    <asp:DropDownList ID="ddlTONumber" runat="server" Width="150px" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlTONumber_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:right">
                   Receiving Issued Centre
                </td>
                <td colspan="2" style="text-align:left">
                    <asp:DropDownList ID="ddlissuecentre" runat="server" Width="150px" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlissuecentre_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Quantity (Qts)
                </td>
                <td>
                    <asp:TextBox ID="txtquantity" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtquantity"
                        ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                </td>
                <td>
                    Date Of Transport Order
                </td>
                <td>
                    <asp:TextBox ID="txtdatetransportorder" runat="server" Width="146px" ReadOnly="True"
                        Enabled="False"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtdatetransportorder"
                        ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Commodity
                </td>
                <td>
                    <asp:TextBox ID="txtCommodity" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCommodity"
                        ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                </td>
                <td>
                    Dispatch Mode
                </td>
                <td>
                    <asp:TextBox ID="txtdispatchmode" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtdispatchmode"
                        ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="6" style="text-align: center; font-size: large; text-decoration: underline;
                    color: #0000ff">
                    Sending Details
                </td>
            </tr>
            <tr>
                <td>
                    Transporter Name
                </td>
                <td>
                    <asp:DropDownList ID="ddltansportername" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <td>
                    Quantity Issue
                </td>
                <td>
                    <asp:TextBox ID="txtquntityissue" runat="server" Width="146px" AutoPostBack="true"
                        OnTextChanged="txtquntityissue_TextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtquntityissue"
                        ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Issued No. of Bags
                </td>
                <td>
                    <asp:TextBox ID="txtIssuedBags" runat="server" Width="146px" AutoComplete="off" class="Number"
                        MaxLength="4" Style="text-align: right" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtIssuedBags"
                        ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                </td>
                <td>
                    Type of Bags
                </td>
                <td>
                    <asp:DropDownList ID="ddltypeofbags" runat="server" Width="150px">
                        <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="New Jute" Value="New Jute"></asp:ListItem>
                        <asp:ListItem Text="Old Jute" Value="Old Jute"></asp:ListItem>
                        <asp:ListItem Text="PP" Value="PP"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Truck Number
                </td>
                <td>
                    <asp:TextBox ID="txtTCNo" runat="server" Width="146px" AutoComplete="off" Class="TruckNumber"
                        MaxLength="14" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtTCNo"
                        ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                </td>
                <%--<td>
                    Branch
                </td>
                <td>
                    <asp:DropDownList ID="ddlbranch" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>--%>
            </tr>
            <tr>
                <td>
                    Godown
                </td>
                <td>
                    <asp:DropDownList ID="ddlgodown" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <td>
                    Date of Issue
                </td>
                <td>
                    <asp:TextBox ID="txtIssuedDate" runat="server" Width="146px" ReadOnly="True"></asp:TextBox>
                    <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtIssuedDate' , 'expiry=true,elapse=-240,restrict=true,instance=single,close=true')" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtIssuedDate"
                        ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Style="font-size: x-small">**</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="text-align: center; font-size: large;">
                <td colspan="6">
                    <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300"
                        BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False"
                        Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False"
                        OnClick="btnRecptNew_Click" />
                    <asp:Button ID="btnRecptSubmit" runat="server" BackColor="Silver" BorderColor="#333300"
                        BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False"
                        Height="30px" Text="Submit" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px"
                        OnClick="btnRecptSubmit_Click" />

                    <asp:Button ID="btnPrint" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Print" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" Enabled="False" CausesValidation="False" OnClick="btnPrint_Click" />

                    <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300"
                        BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False"
                        Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False"
                        Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
                </td>
            </tr>
            <%--Allow Only Alphabets using class="alphaNumericWithSpecial" --%>
            <script lang="javascript" src="../PaddyMilling/Scripts/TruckNo.js" type="text/javascript">      </script>
            <script lang="javascript" src="../PaddyMilling/Scripts/Number.js" type="text/javascript">       </script>
</asp:Content>
