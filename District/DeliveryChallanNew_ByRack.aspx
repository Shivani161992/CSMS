<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="DeliveryChallanNew_ByRack.aspx.cs" Inherits="District_DeliveryChallanNew_ByRack" %>

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

    <style type="text/css">
        .hiddencol {
            display: none;
        }
    </style>

    <table align="center" style="width: 760px; border-style: solid; border-width: 1px; text-align: left;" border="1" cellspacing="0" cellpadding="6">
        <tr>
            <td colspan="6" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600"><strong>New Delivery Challan Against Transport Order [By Rack To Road]</strong>
                <input id="hdfSTO_No" type="hidden" runat="server"/>
            </td>
        </tr>
        <tr>
            <td rowspan="11">&nbsp;</td>
            <td>परिवहन आदेश क्रमांक</td>
            <td>
                <asp:DropDownList ID="ddlTONo" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlTONo_SelectedIndexChanged">
                </asp:DropDownList>


            </td>
            <td>मुख्यालय एम ओ क्रमांक</td>
            <td>
                <asp:TextBox ID="txtMONo" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtMONo" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
            <td rowspan="11">&nbsp;</td>
        </tr>
        <tr>
            <td>परिवहनकर्ता का नाम</td>
            <td>
                <asp:TextBox ID="txtTransporterName" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTransporterName" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
            <td>कमोडिटी</td>
            <td>
                <asp:TextBox ID="txtCommodity" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCommodity" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>जिला से</td>
            <td>
                <asp:TextBox ID="txtFrmDist" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
            </td>
            <td>जिला तक</td>
            <td>
                <asp:TextBox ID="txtToDist" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>Consinment No</td>
            <td>
                <asp:DropDownList ID="ddlConsinmentNo" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlConsinmentNo_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
            <td>रेल हेड</td>
            <td>
                <asp:TextBox ID="txtRailHead" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtRailHead" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Balance Qty <strong>(Qtls)</strong></td>
            <td>
                <asp:TextBox ID="txtBalQtyInRack" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtBalQtyInRack" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

            </td>
            <td>Balance Bags</td>
            <td>
                <asp:TextBox ID="txtBalBagInRack" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtBalBagInRack" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Issued Qty <strong>(Qtls)</strong></td>
            <td>
                <asp:TextBox ID="txtIssuedQty" runat="server" Width="146px" Style="text-align: right" MaxLength="10" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="NAN" ControlToValidate="txtIssuedQty" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtIssuedQty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
            <td>Issued No. of Bags</td>
            <td>
                <asp:TextBox ID="txtIssuedBags" runat="server" Width="146px" AutoComplete="off" class="Number" MaxLength="3" Style="text-align: right" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtIssuedBags" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>
            <td>Type of Bags</td>
            <td>
                <asp:RadioButton ID="rdbSBT" runat="server" GroupName="BagsGroup" Text="SBT" />
                <asp:RadioButton ID="rdbHDPE" runat="server" GroupName="BagsGroup" Text="HDPE" Style="margin-left: 5px;" />
            </td>
            <td>Truck Number</td>
            <td>
                <asp:TextBox ID="txtTCNo" runat="server" Width="146px" AutoComplete="off" Class="TruckNumber" MaxLength="14" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtTCNo" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>
            <td>Date of Issue</td>
            <td>
                <asp:TextBox ID="txtIssuedDate" runat="server" Width="128px" ReadOnly="True"></asp:TextBox>
                <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtIssuedDate' , 'expiry=true,elapse=-14,restrict=true,instance=single,close=true')" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtIssuedDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td>Receiving Issue Centre</td>
            <td>
                <asp:DropDownList ID="ddlSendIC" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlSendIC_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
        </tr>
        <tr>
            <td>Receiving Branch</td>
            <td>

                <asp:DropDownList ID="ddlSendBranch" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlSendBranch_SelectedIndexChanged">
                </asp:DropDownList>

            </td>

            <td>Receiving Godown</td>
            <td>
                <asp:DropDownList ID="ddlSendGodown" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlSendGodown_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
        </tr>
        <tr>
            <td colspan="3">मूवमेंट प्लान या ट्रांसपोर्ट आर्डर के अनुसार बची हुई मात्रा <strong>(Qtls)</strong></td>
            <td>

                <asp:TextBox ID="txtBalQtyInSendIC" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td colspan="3">प्रेक्षणकर्ता रेल हेड से प्राप्तकर्ता गोदाम के बीच की अनुमानित दुरी <strong>(Km)</strong></td>
            <td>
                <asp:TextBox ID="txtDistance" runat="server" Width="146px" AutoComplete="off" class="Number" MaxLength="5" Style="text-align: right" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                </td>
        </tr>
        <tr style="text-align: center; font-size: large;">
            <td colspan="6">
                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                <asp:Button ID="btnRecptSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" OnClick="btnRecptSubmit_Click" />

                <asp:Button ID="btnPrint" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Print" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" Enabled="False" CausesValidation="False" OnClick="btnPrint_Click" />

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
            </td>
        </tr>

        <%--Allow Only Alphabets using class="alphaNumericWithSpecial" --%>
        <script lang="javascript" src="../PaddyMilling/Scripts/TruckNo.js" type="text/javascript">      </script>

        <script lang="javascript" src="../PaddyMilling/Scripts/Number.js" type="text/javascript">       </script>

    </table>

    <input type="hidden" id="div_position" name="div_position" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

