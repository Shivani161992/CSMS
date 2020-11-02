<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="CMRDO_OtherDist.aspx.cs" Inherits="PaddyMilling_CMRDO_OtherDist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">
        function validate() {
                return confirm('क्या आपने सही जानकारी भरी है? यदि हाँ, तो OK पर क्लिक करे अन्यथा Cancel पर क्लिक करे|');
        }
    </script>


    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }

        .auto-style1 {
            color: #0000FF;
        }

        .auto-style2 {
            text-decoration: underline;
        }

        .hiddencol {
            display: none;
        }
    </style>

    <table align="center" style="width: 630px; border-style: solid; border-width: 1px; text-align: left; font-size: small" border="1" cellspacing="0" cellpadding="5">
        <tr>
            <td colspan="6" style="text-align: center; font-size: large; color: #CC6600"><strong><span class="auto-style2">CMR Deposit Order</span> <span class="auto-style1">(Other District Mapping)</span></strong>
                
                <input id="hdfMappingData" type="hidden" runat="server" />
            </td>
        </tr>

        <tr>
            <td rowspan="6">&nbsp;</td>
            <td colspan="4" style="text-align: center; background-color: #999999">
                <asp:Label ID="Label2" runat="server" Style="text-align: center; font-weight: 700;" ForeColor="Blue" Visible="False"></asp:Label>
            </td>
            <td rowspan="6"></td>
        </tr>

        <tr>
            <td>Agrmt District</td>
            <td>
                <asp:DropDownList ID="ddlFrmDist" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlFrmDist_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
            <td>Crop Year</td>
            <td>

                <asp:TextBox ID="txtYear" runat="server" Width="120px" ReadOnly="True" Enabled="False"></asp:TextBox>

            </td>
        </tr>

        <tr>
            <td>मिल का नाम</td>
            <td colspan="3">
                <asp:DropDownList ID="ddlMillName" runat="server" Width="462px" AutoPostBack="True" OnSelectedIndexChanged="ddlMillName_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>


        <tr>
            <td>अनुबंध नंबर</td>
            <td>
                <asp:DropDownList ID="ddlAgtmtNumber" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlAgtmtNumber_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
            <td>Agreement Qty<strong>(Qtls)</strong></td>
            <td>
                <asp:TextBox ID="txtAgrmtQty" runat="server" Width="120px" ReadOnly="True" Enabled="False"></asp:TextBox>
            </td>
        </tr>


        <tr>
            <td>CMR District</td>
            <td>
                <asp:DropDownList ID="ddlCMRDist" runat="server" Width="141px" AutoPostBack="true" OnSelectedIndexChanged="ddlCMRDist_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
            <td>&nbsp;</td>
            <td>

                &nbsp;</td>
        </tr>


        <tr>
            <td colspan="4" style="text-align: center; background-color: #999999" ></td>
        </tr>

        <tr>
            <td colspan="6" style="text-align: center; font-size: large;">
                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                <asp:Button ID="btnRecptSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" OnClientClick="return validate();" Enabled="false" OnClick="btnRecptSubmit_Click1" />

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

