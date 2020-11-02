<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="PaddyStock_Position.aspx.cs" Inherits="PaddyMilling_PaddyStock_Position" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function validate() {
            if (Page_ClientValidate())
                return confirm('क्या आपने सही Balance भरा है? यदि हाँ, तो OK पर क्लिक करे अन्यथा Cancel पर क्लिक करे');
        }
    </script>

    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>

    <%--Allow Only 2 Digit After Decimal--%>
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/Number2D.js"></script>


    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }

        .auto-style1 {
            color: #FF0000;
        }
        .auto-style2 {
            color: #0000FF;
        }
        .auto-style3 {
            font-size: small;
        }
    </style>

    <table align="center" style="width: 740px; border-style: solid; text-align: left; font-size: medium" border="1" cellspacing="0" cellpadding="2">
        <tr>
            <td colspan="6" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600"><strong>Paddy Stock Available In Godown</strong>
                <input id="hdfAgrmtDist" type="hidden" runat="server" />

                <input id="hdfCount" type="hidden" runat="server" />

            </td>
        </tr>

        <tr>
            <td rowspan="9">&nbsp;</td>
            <td colspan="4" style="text-align: center; background-color: #CCFF99" class="auto-style1">कृपया सही जानकारी भरें, एक बार जानकारी भरने के बाद इसमें कोई भी बदलाव नहीं किया जाएगा|</td>
            <td rowspan="9"></td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <asp:Label ID="Label2" runat="server" Style="text-align: center; font-weight: 700; color: #FF0000;" ForeColor="Blue" Visible="False"></asp:Label>
            </td>
        </tr>



        <tr>
            <td>Crop Year</td>
            <td>
                <asp:TextBox ID="txtYear" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtYear" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td>Commodity</td>
            <td>

                <asp:DropDownList ID="ddlCommodity" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlCommodity_SelectedIndexChanged"  >
                </asp:DropDownList>
            </td>
        </tr>



        <tr>
            <td>District</td>
            <td>

                <asp:DropDownList ID="ddlDistrict" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" >
                </asp:DropDownList>
            </td>
            <td>Issue Center</td>
            <td>

                <asp:DropDownList ID="ddlIC" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlIC_SelectedIndexChanged" >
                </asp:DropDownList>
            </td>
        </tr>



        <tr>
            <td>Branch</td>
            <td>

                <asp:DropDownList ID="ddlBranch" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" style="height: 22px">
                </asp:DropDownList>
            </td>
            <td>Godown</td>
            <td>

                <asp:DropDownList ID="ddlGodam" runat="server" Width="141px" OnSelectedIndexChanged="ddlGodam_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>


        <tr>
            <td colspan="3">
                <asp:Label ID="lblYear" runat="server" Text="" style="color: #0000FF"></asp:Label> <span class="auto-style3">में</span> <asp:Label ID="lblCommodity" runat="server" Text="" style="color: #0000FF"></asp:Label> <span class="auto-style3">की मिलिंग के लिए गोदाम में कितना</span> <strong>Balance</strong> <span class="auto-style3">हैं?</span> <span class="auto-style2"> <strong>(Qtls)</strong></span></td>
            <td>
                <asp:TextBox ID="txtOpeningBal" runat="server" Width="137px" Style="text-align: right" MaxLength="12" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;" Enabled="False"></asp:TextBox>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="NAN" ControlToValidate="txtOpeningBal" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOpeningBal" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
        </tr>


        <tr>
            <td colspan="3">
                <asp:Label ID="lblYear1" runat="server" Text="" style="color: #0000FF"></asp:Label> <span class="auto-style3">में</span> <asp:Label ID="lblCommodity1" runat="server" Text="" style="color: #0000FF"></asp:Label> <span class="auto-style3">की मिलिंग के लिए गोदाम में कितने </span> <strong>Bags</strong> <span class="auto-style3">हैं?</span> <span class="auto-style2"></span></td>
            <td>
                <asp:TextBox ID="txtBags" runat="server" AutoComplete="off" class="Number" MaxLength="8" onfocus="this.select();" onmouseup="return false;" Style="text-align: right" Width="137px" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="txtBags" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
        </tr>


        <tr>
            <td colspan="4">
                </td>
        </tr>

          <script lang="javascript" src="Scripts/Number.js" type="text/javascript"> </script>

        <tr>
            <td colspan="4" style="background-color: #CCFF99; text-align: center;" class="auto-style1">कृपया सही जानकारी भरें, एक बार जानकारी भरने के बाद इसमें कोई भी बदलाव नहीं किया जाएगा|</td>
        </tr>

        <tr>
            <td colspan="6" style="text-align: center; font-size: large;">
                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                <asp:Button ID="btnRecptSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" OnClientClick="return validate();" Enabled="False" Visible="false" OnClick="btnRecptSubmit_Click1" />

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
                <asp:Button ID="btnRecptUpdate" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Update" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" Enabled="false" Visible="false" OnClick="btnRecptUpdate_Click" />
                <asp:Button ID="btnRecptSave" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Save" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" Enabled="False" Visible="false" OnClick="btnRecptSave_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

