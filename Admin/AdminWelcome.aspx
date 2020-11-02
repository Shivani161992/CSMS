<%@ Page Language="C#" MasterPageFile="~/MasterPage/AdminLogin.master" AutoEventWireup="true" CodeFile="AdminWelcome.aspx.cs" Inherits="Admin_AdminWelcome" Title="Admin Welcome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="divHO">
<table class ="tdho">
<tr>
<td>
This Applicaion has been accessed .
</td>
<td style="width: 96px" align="left">
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
</td>
    <td>
        Times</td>
</tr>
<tr>
<td align="left">Number OF Users</td>
<td style="width: 96px" align="left"> 
    <asp:Label ID="Label2" runat="server" Text=""></asp:Label></td>
    <td>
    </td>
</tr>
    <tr>
        <td align="left">
            Number Of Active Connection</td>
        <td align="left" style="width: 96px">
            <asp:Label ID="Label3" runat="server"></asp:Label></td>
        <td>
        </td>
    </tr>
</table>
</div>
</asp:Content>

