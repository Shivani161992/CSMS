<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="RailHead_Entry.aspx.cs" Inherits="DistrictFood_RailHead_Entry" Title="Rail Head Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="transCP">
<center >
<div id="transCPm"> 
<table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse; border-right: green 3px double; border-top: green 3px double; border-left: green 3px double; border-bottom: green 3px double; width: 322px;" id="">
        <tr>
                <td colspan="3" style="height: 24px; color: white; background-color: transparent; background-image: url(../Images/imgg2.jpg);" align="center">
                    Rail Head Master</td>
            </tr>
        </table>
<table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: green 3px double; border-top: green 3px double; border-left: green 3px double; border-bottom: green 3px double; width: 324px;"  >
    <tr>
        <td colspan="2" style="height: 21px">
            <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="2">
        </td>
    </tr>
    <tr>
        <td align="left">
            District Name</td>
        <td align="left">
    <asp:TextBox ID="txtdistrict" runat="server" Width="175px" Font-Italic="True" Font-Names="Verdana" ForeColor="Navy"></asp:TextBox></td>
    </tr>
    <tr>
        <td align="left" >
            Rail Head Name</td>
        <td align="left" >
            <asp:TextBox ID="txtrailhead" runat="server" Width="174px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtrailhead"
                ErrorMessage="Rail Head  Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
    </tr>
<tr>
<td align="left">
    </td>
<td align="left">
    &nbsp;</td>
</tr>
<tr>
<td></td>
<td align="left">
    </td>
</tr>
    <tr>
        <td>
            &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:Button ID="btnadd" runat="server" Text="Save" OnClick="btnadd_Click" Width="74px" ValidationGroup="1" /></td>
        <td align="left">
           
            <asp:Button ID="btnupdate" runat="server" Text="Update" Width="65px" OnClick="btnupdate_Click" Visible="False" />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Close" /></td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ValidationGroup="1" Width="263px" />
        </td>
    </tr>
<tr>
<td>
    </td>
<td align="left">
    &nbsp;</td>
</tr>
</table>
<table>
<tr>
<td>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Width="323px">
        <FooterStyle BackColor="Tan" />
        <Columns>
            <asp:CommandField HeaderText="Action" ShowSelectButton="True">
                <HeaderStyle Font-Size="12px" />
                <ItemStyle Font-Size="11px" />
            </asp:CommandField>
            <asp:BoundField DataField="RailHead_Code" HeaderText="Rail Head ID" SortExpression="RailHead_Code">
                <ItemStyle Font-Size="11px" />
                <HeaderStyle Font-Size="12px" />
            </asp:BoundField>
            <asp:BoundField DataField="RailHead_Name" HeaderText="RailHead Name" SortExpression="RailHead_Name">
                <ItemStyle Font-Size="11px" />
                <HeaderStyle Font-Size="12px" />
            </asp:BoundField>
        </Columns>
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
        <HeaderStyle BackColor="Tan" Font-Bold="True" />
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
    </asp:GridView>
</td>
</tr>
</table>
</div>
</center>
</div> 
</asp:Content>

