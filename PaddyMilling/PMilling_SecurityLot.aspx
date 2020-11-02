<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="PMilling_SecurityLot.aspx.cs" Inherits="PaddyMilling_PMilling_SecurityLot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function validate() {
            if (Page_ClientValidate())
                return confirm('क्या आपने सिक्यूरिटी राशि का लॉट सही चुना है? यदि हाँ, तो OK पर क्लिक करे अन्यथा Cancel पर क्लिक करे तथा सही लॉट की संख्या चुने|');
        }
    </script>

    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }
    </style>

    <table align="center" style="width: 800px; border-style: solid; border-width: 1px; text-align: left; font-size: medium" border="1" cellspacing="0" cellpadding="2">
        <tr>
            <td colspan="6" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600"><strong>Update Miller Agreement Security Lot</strong>
                <input id="hdfAgrmtID" type="hidden" runat="server" />
                <input id="hdfMillCode" type="hidden" runat="server" />
                <input id="hdfMillingType" type="hidden" runat="server" />
                <input id="hdfDhanLot" type="hidden" runat="server" />
                <input id="hdfSecurityLot" type="hidden" runat="server" />
            </td>
        </tr>

        <tr>
            <td rowspan="20">&nbsp;</td>
            <td colspan="4" style="text-align: center; background-color: #999999">
                <asp:Label ID="Label2" runat="server" Style="text-align: center; font-weight: 700;" ForeColor="Blue" Visible="False"></asp:Label>
            </td>
            <td rowspan="20"></td>
        </tr>
        <tr>
            <td>District</td>
            <td>
                <asp:TextBox ID="txtDistManager" runat="server" Width="137px" Style="margin-left: 0px" ReadOnly="True" Enabled="False"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDistManager" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

            </td>
            <td>Crop Year</td>
            <td>

                <asp:TextBox ID="txtYear" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>

            </td>
        </tr>

        <tr>
            <td>मिल का नाम</td>
            <td>
                <asp:DropDownList ID="ddlMillName" runat="server" Height="27px" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="ddlMillName_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlMillName" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

            </td>
            <td>अनुबंध नंबर</td>
            <td>
                <asp:DropDownList ID="ddlAgtmtNumber" runat="server" Height="27px" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlAgtmtNumber_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlAgtmtNumber" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>
            <td>अनुबंध दिनांक से</td>
            <td>
                <asp:TextBox ID="txtAgrmtFrmDate" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox></td>
            <td>अनुबंध दिनांक तक</td>
            <td>

                <asp:TextBox ID="txtAgrmtToDate" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="3">मिलर के साथ अनुबंधित धान का लॉट</td>
            <td>

                <asp:TextBox ID="txtAgrmtLot" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="3">अनुबंध के अनुसार अभी तक जमा की गयी सिक्यूरिटी राशि का लॉट</td>
            <td>

                <asp:TextBox ID="txtAgrmtSecurityLot" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox></td>
        </tr>
        <tr style="background-color: #CCFF99;">
            <td colspan="3">अनुबंध के अनुसार मिलर कितने लॉट की सिक्यूरिटी राशि जमा करना चाहता है?</td>
            <td>

                <asp:DropDownList ID="ddlSecurityLot" runat="server" Height="27px" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlSecurityLot_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="background-color: #999999; color: #0000FF;"></td>
        </tr>

        <tr>
            <td colspan="6" style="text-align: center; font-size: large;">
                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                <asp:Button ID="btnRecptSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Update" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px"  OnClientClick="return validate();" Enabled="False" OnClick="btnRecptSubmit_Click1" />

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

