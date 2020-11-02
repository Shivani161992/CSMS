<%@ Page Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="Sugar_Factory.aspx.cs" Inherits="State_Sugar_Factory" Title="Sugar Factory Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<center >
<table border="1" cellpadding="0" cellspacing="0" style="padding:1; margin-top :50px;  BORDER-COLLAPSE: collapse;  border-right: green 3px double; border-top: green 3px double; border-left: green 3px double; border-bottom: green 3px double; width: 372px;">
    <tr>
        <td colspan="2" style="color: white; background-color: lightslategray">
            <strong>
                    Sugar Factory Master</strong></td>
        <td colspan="1" style="color: white; background-color: lightslategray">
        </td>
    </tr>
    <tr>
        <td colspan="2" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
            <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
            <asp:TextBox ID="txtplid" runat="server" Visible="False" Width="56px"></asp:TextBox>
            <asp:TextBox ID="txtfid" runat="server" Visible="False" Width="56px"></asp:TextBox></td>
        <td colspan="1" rowspan="1" valign="top" style="font-size: 10pt; position: static; background-color: #cfdcdc">
            <strong><em>
            Factory
            Name</em></strong></td>
    </tr>
    <tr>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
            <asp:Label ID="Label4" runat="server" Text="State"></asp:Label></td>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">
            <asp:DropDownList ID="ddlstate" runat="server" Width="178px" AutoPostBack="True" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged">
            </asp:DropDownList></td>
        <td colspan="1" rowspan="6" valign="top" style="font-size: 10pt; position: static; background-color: #cfdcdc">
            <asp:ListBox ID="ListBox1" runat="server" Height="136px" Width="220px" BackColor="LightSteelBlue"></asp:ListBox></td>
    </tr>
    <tr>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">
            <asp:Label ID="Label3" runat="server" Text="Place of Sugar Factory"></asp:Label></td>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">
            <asp:DropDownList ID="ddlplace" runat="server" Width="178px" AutoPostBack="True" OnSelectedIndexChanged="ddlplace_SelectedIndexChanged">
                <asp:ListItem Value="01">--Select--</asp:ListItem>
                <asp:ListItem Value="New Place">New Place</asp:ListItem>
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">
            <asp:Label ID="lblplace" runat="server" Text="Place Name" Visible="False"></asp:Label></td>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">
            <asp:TextBox ID="txtplace" runat="server" Width="172px" Visible="False"></asp:TextBox></td>
    </tr>
    <tr>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
            <asp:Label ID="Label2" runat="server" Text="Name of Sugar Factory" Width="150px"></asp:Label></td>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">
    <asp:TextBox ID="txtplacename" runat="server" Width="173px"></asp:TextBox>
            </td>
    </tr>
    <tr>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
            &nbsp;<asp:Button ID="btnadd" runat="server" Text="Save" OnClick="btnadd_Click" Width="143px" ValidationGroup="1" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="65px" OnClick="btnUpdate_Click" Visible="False" /></td>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">
            <asp:Button ID="btnclose" runat="server" Text="Close" Width="156px" OnClick="btnclose_Click" /></td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;</td>
    </tr>
</table>
</center>

</asp:Content>



