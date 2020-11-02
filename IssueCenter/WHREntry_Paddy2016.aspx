<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="WHREntry_Paddy2016.aspx.cs" Inherits="IssueCenter_WHREntry_Paddy2016" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>

    <script type="text/javascript" language="javascript">
        function validate() {
            if (Page_ClientValidate())
                return confirm('क्या आपने सही जानकारी भरी हैं? यदि हाँ, तो OK पर क्लिक करे अन्यथा Cancel पर क्लिक करे तथा सही जानकारी भरें|');
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("input").focus(function () {
                $(this).css("background-color", "#cccccc");
            });
            $("input").blur(function () {
                $(this).css("background-color", "#ffffff");
            });
        });
    </script>

    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
            font-size: small;
        }

        .Qtls {
            font-size: small;
            color: #FF0000;
        }

        .hover_row {
            background-color: #A1DCF2;
        }
    </style>

    <table align="center" style="border-style: solid; border-width: 1px; text-align: left; font-size: medium; width: 850px" border="1" cellspacing="0" cellpadding="3">
        <tr>
            <td colspan="6" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600"><strong>Depositer Form <span style="font-size:medium"> के विरुद्ध </span>  Procurement 2016 <span style="font-size:medium"> के WHR की प्रविष्टि हेतु </span></strong>
            </td>
        </tr>

        <tr>
            <td align="center" colspan="6"
                style="background-color: #cccccc; font-size: small; color: #CC0000;">
                <span style="color: #000099; font-size: small">
                    <span style="font-family: Verdana; text-align: left"><b>केवल एफ सी आई(FCI) एवं अन्य गोदाम के सन्दर्भ में मैनुअल एंट्री करे, वेयरहाउस 
             सॉफ्टवेयर के WHR की जानकरी स्वतः CSMS में आ जायेगी, अतः इसकी एंट्री ना करें|</b></span></span>
                <asp:Image ID="Image3" runat="server" Height="16px" ImageUrl="~/IssueCenter/img/blinking_new.gif" Width="39px" />
            </td>
        </tr>

        <tr>
            <td rowspan="6">&nbsp;</td>
            <td colspan="4" style="text-align: center; background-color: #CCFF99">
                <asp:Label ID="Label2" runat="server" Style="text-align: center; font-weight: 700; color: #FF0000;" ForeColor="Blue" Visible="False"></asp:Label>
            </td>
            <td rowspan="6"></td>
        </tr>




        <tr>
            <td>Issue Center</td>
            <td>
                <asp:TextBox ID="txtissue" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="txtissue" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
            <td>Crop Year</td>
            <td>
                <asp:TextBox ID="txtYear" runat="server" Width="137px" ReadOnly="True" Enabled="False">2016-2017</asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="txtYear" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td>Commodity</td>
            <td>
                <asp:DropDownList ID="ddlcommodtiy" runat="server" Width="141px" OnSelectedIndexChanged="ddlcommodtiy_SelectedIndexChanged" AutoPostBack="true" Height="24px">
                </asp:DropDownList>

            </td>
            <td>Depositer Form No.</td>
            <td>
                <asp:DropDownList ID="ddldepositer" runat="server" Height="24px" Width="170px"
                    AutoPostBack="True" OnSelectedIndexChanged="ddldepositer_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
        </tr>


        <tr>
            <td>Total Bags</td>
            <td>

                <asp:TextBox ID="txtBags" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="txtBags" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
            <td>Total Quantity</td>
            <td>

                <asp:TextBox ID="txtQty" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="txtQty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td>No. of Trucks</td>
            <td>

                <asp:TextBox ID="txttruck" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="txttruck" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
            <td>Enter WHR Number</td>
            <td>

                <asp:TextBox ID="txtWHRNo" runat="server" Width="137px" MaxLength="20" autocomplete="off"></asp:TextBox>

            </td>
        </tr>

        <tr>
            <td colspan="4" style="background-color: #CCFF99;"></td>
        </tr>

        <tr>
            <td colspan="6" style="text-align: center; font-size: large;">
                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                <asp:Button ID="btnRecptSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" OnClientClick="return validate();" OnClick="btnRecptSubmit_Click" />

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
