<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="RecFromRack.aspx.cs" Inherits="District_RecFromRack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>

    <%--For Calendar Controls--%>
    <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="../js/calendarMvmt.js"></script>

    <%--Allow Only 2 Digit After Decimal--%>
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/Number2D.js"></script>

    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }
    </style>

    <table align="center" style="width: 760px; border-style: solid; border-width: 1px; text-align: left" border="1" cellspacing="0" cellpadding="6">
        <tr>
            <td colspan="6" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600"><strong>Received From Rack</strong>
                <input id="hdfFrm_RackDist" type="hidden" runat="server"/>
                <input id="hdfCommodity" type="hidden" runat="server"/>
            </td>
        </tr>
        <tr>
            <td rowspan="5">&nbsp;</td>
            <td>MO Number</td>
            <td>
                <asp:DropDownList ID="ddlTONo" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlTONo_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
            <td>Rack Sending Dist</td>
            <td>
                <asp:TextBox ID="txtSendDist" runat="server" Enabled="False" Width="146px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSendDist" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
            <td rowspan="5">&nbsp;</td>
        </tr>

        <tr>
            <td>Commodity</td>
            <td>
                <asp:TextBox ID="txtComdty" runat="server" Enabled="False" Width="146px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtComdty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
            <td>Received Date</td>
            <td>
                <asp:TextBox ID="txtRecdDate" runat="server" Width="126px" ReadOnly="True"></asp:TextBox>
                <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtRecdDate' , 'restrict=true,expiry=false,instance=single,close=true')" />

                <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txtRecdDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td>Received Qty </td>
            <td>
                <asp:TextBox ID="txtRecdQty" runat="server" Width="146px" Style="text-align: right" MaxLength="15" AutoComplete="off"
                    onblur="extractNumber(this,5,true);"
                    onkeyup="extractNumber(this,5,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                <strong>&nbsp;</strong><asp:Label ID="lblQtls" runat="server" style="font-weight: 700" ></asp:Label>
&nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="NAN" ControlToValidate="txtRecdQty" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,5})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtRecdQty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
            <td>Received Bags</td>
            <td>
                <asp:TextBox ID="txtRecdBags" runat="server" Width="146px" AutoComplete="off" class="Number" MaxLength="6" Style="text-align: right" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtRecdBags" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td>Consinment No</td>
            <td>
                <asp:TextBox ID="txtConsNo" runat="server" Width="146px" AutoComplete="off" Class="TruckNumber" MaxLength="15" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtConsNo" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
            <td>Received Rail Head</td>
            <td>
                
                <asp:DropDownList ID="ddlFrmRack" runat="server" Width="150px" AutoPostBack="True">
                </asp:DropDownList>

            </td>
        </tr>


        <tr>
            <td>MO Balance Qty</td>
            <td>
               <asp:TextBox ID="txtRemMOQty" runat="server" Enabled="False" Width="146px"> </asp:TextBox> <strong>&nbsp;<asp:Label ID="lblQtls0" runat="server" style="font-weight: 700" ></asp:Label>
                </strong></td>
            <td>Types of Gunny</td>
            <td>
               <asp:TextBox ID="txtGunnyType" runat="server" Enabled="False" Width="146px"> </asp:TextBox> </td>
        </tr>


        <tr>
            <td colspan="6" style="text-align: center; font-size: large;">
                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                <asp:Button ID="btnRecptSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit" Width="115px" CssClass="ButtonClass" Style="margin-left: 20px" OnClick="btnRecptSubmit_Click" />

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 20px" OnClick="btnRecptClose_Click" />
            </td>
        </tr>

        <script lang="javascript" src="../PaddyMilling/Scripts/Number.js" type="text/javascript">       </script>

        <%--Allow Only Alphabets using class="alphaNumericWithSpecial" --%>
        <script lang="javascript" src="../PaddyMilling/Scripts/TruckNo.js" type="text/javascript">      </script>

    </table>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

