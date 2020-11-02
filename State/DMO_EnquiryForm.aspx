<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Markfed_PDY.master" AutoEventWireup="true" CodeFile="DMO_EnquiryForm.aspx.cs" Inherits="State_DMO_EnquiryForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <style type="text/css">
        .auto-styleNSC {
            width: 500px;
        }

        .ButtonClass {
            cursor: pointer;
            text-align: center;
        }
    </style>

    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>

    <table align="center" style="width: 700px; border-style: solid; border-width: 1px; text-align: left" border="1" cellspacing="0" cellpadding="6">
        <tr>
            <td colspan="4" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600"><strong>DMO Enquiry Form</strong></td>
        </tr>
        <tr>
            <td rowspan="4" style="background-color: #CCCCCC">&nbsp;</td>
            <td>जिला</td>
            <td>
                <asp:DropDownList ID="ddlDist" runat="server" Width="140px" AutoPostBack="True" OnSelectedIndexChanged="ddlDist_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td rowspan="4" style="background-color: #CCCCCC">&nbsp;</td>
        </tr>
        <tr>
            <td>जिला विपणन अधिकारी का नाम</td>
            <td>
                <asp:TextBox ID="txtDMName" runat="server" Width="250px" MaxLength="50" autocomplete="off" Height="20px"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDMName" Display="Dynamic"
                    ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>जिला विपणन अधिकारी का ईमेल</td>
            <td>
                <asp:TextBox ID="txtDMMail" runat="server" Width="250px" MaxLength="35"></asp:TextBox>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDMMail" Display="Dynamic"
                    ErrorMessage="RegularExpressionValidator" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Invalid</asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDMMail" Display="Dynamic"
                    ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>जिला विपणन अधिकारी का मोबाइल न.</td>
            <td>
                <asp:TextBox ID="txtDMMobile0" runat="server" Enabled="False" Width="25px">+91</asp:TextBox>

                <asp:TextBox ID="txtDMMobile" runat="server" Width="100px" MaxLength="10" autocomplete="off"></asp:TextBox>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDMMobile" Display="Dynamic"
                    ErrorMessage="RegularExpressionValidator" ValidationExpression="[0-9]{10}">Invalid Number</asp:RegularExpressionValidator>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDMMobile" Display="Dynamic"
                    ErrorMessage="RequiredFieldValidator">**</asp:RequiredFieldValidator>

                 </td>
        </tr>
        <tr style="text-align: center">
            <td colspan="4">
                <asp:Button ID="btnSave" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Save" Width="115px" CssClass="ButtonClass" OnClick="btnSave_Click" />
                <asp:Button ID="btnClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnClose_Click" style="margin-left:20px" />
            </td>
        </tr>

        <%--Allow Only Number & One Decimal using class="NumberDecimal" --%>
        <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/NumberDecimal.js"></script>
    </table>

</asp:Content>

