<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="RackReceived.aspx.cs" Inherits="District_RackReceived" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>

    <script type="text/javascript" lang="javascript" src="../js/calendarMvmt.js"></script>

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
            <td colspan="6" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600"><strong>Rack Point Entry By Receiving District</strong>

            </td>
        </tr>
        <tr>
            <td rowspan="7">&nbsp;</td>
            <td>Rack Number</td>
            <td>
                <asp:DropDownList ID="ddlRackNo" runat="server" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="ddlRackNo_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
            <td>Challan No</td>
            <td>
                <asp:DropDownList ID="ddlChallanNo" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlChallanNo_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
            <td rowspan="7">&nbsp;</td>
        </tr>
        <tr>
            <td>MO Number</td>
            <td>
                <asp:TextBox ID="txtMoNO" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtMoNO" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
            <td>Sending District</td>
            <td>
                <asp:TextBox ID="txtSendingDist" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Commodity</td>
            <td>
                <asp:TextBox ID="txtCommodity" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtCommodity" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
            <td>Crop-Year</td>
            <td>
                <asp:TextBox ID="txtCropYear" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtCropYear" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Sending Qty </td>
            <td>
                <asp:TextBox ID="txtSendingQty" runat="server" Width="146px" Style="text-align: right" ReadOnly="True" Enabled="False"></asp:TextBox>
                <strong>&nbsp;(Qtls)</strong>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtSendingQty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                </td>
            <td>Sending Bags</td>
            <td>
                <asp:TextBox ID="txtSendingBags" runat="server" Width="146px" Style="text-align: right" ReadOnly="True" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtSendingBags" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Receiving Qty </td>
            <td>
                <asp:TextBox ID="txtRecdQty"  runat="server" Width="146px" Style="text-align: right" MaxLength="10" AutoComplete="off"
                        onblur="extractNumber(this,2,true);"
                        onkeyup="extractNumber(this,2,true);"
                        onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                <strong>&nbsp;(Qtls)</strong>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="NAN" ControlToValidate="txtRecdQty" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtRecdQty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
            <td>Receiving Bags</td>
            <td>
                <asp:TextBox ID="txtRecdBags"  runat="server" Width="146px" AutoComplete="off" class="Number" MaxLength="3" Style="text-align: right" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtRecdBags" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>
            <td>Sending Date</td>
            <td>
                <asp:TextBox ID="txtSendingDate" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
            </td>
            <td>Received Date</td>
            <td>
                <asp:TextBox ID="txtRecdDate" runat="server" Width="126px" ReadOnly="True"></asp:TextBox>
                <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtRecdDate' , 'restrict=true,expiry=false,instance=single,close=true')" />
            
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txtRecdDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:HiddenField ID="hdfSubMO" runat="server" />
                <asp:HiddenField ID="hdfSendDist" runat="server" />
               <asp:HiddenField ID="hdfComdty" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="6" style="text-align: center; font-size: large;">
                    <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                    <asp:Button ID="btnRecptSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit" Width="115px" CssClass="ButtonClass" Style="margin-left: 20px" OnClick="btnRecptSubmit_Click" />

                    <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 20px" OnClick="btnRecptClose_Click" />
                </td>
        </tr>

         <script lang="javascript" src="../PaddyMilling/Scripts/Number.js" type="text/javascript">       </script>
    </table>


</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

