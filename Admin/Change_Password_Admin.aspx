<%@ Page Language="C#" MasterPageFile="~/MasterPage/AdminLogin.master" AutoEventWireup="true" CodeFile="Change_Password_Admin.aspx.cs" Inherits="Admin_Change_Password_Admin" Title="Change Password Data Administrator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="transCP">
<center >
<div id="transCPm"> 
<table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse; border-right: green 3px double; border-top: green 3px double; border-left: green 3px double; border-bottom: green 3px double; width: 302px;" id="">
        <tr>
                <td colspan="3" style="height: 24px; color: white; background-color: transparent; background-image: url(../Images/imgg2.jpg);" align="center">
                    Change Password</td>
            </tr>
        </table>
<table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: green 3px double; border-top: green 3px double; border-left: green 3px double; border-bottom: green 3px double;"  width="300" >
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
            Old Password</td>
        <td align="left">
    <asp:TextBox ID="txtoldpwd" runat="server" Width="123px" TextMode="Password"></asp:TextBox></td>
    </tr>
    <tr>
        <td align="left" >
            New Password</td>
        <td align="left" >
            <asp:TextBox ID="txtnewpwd" runat="server" Width="120px" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtnewpwd"
                ErrorMessage="New Password Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
    </tr>
<tr>
<td align="left">
    Confirm Password</td>
<td align="left">
    <asp:TextBox ID="txtconfpwd" runat="server" Width="119px" TextMode="Password"></asp:TextBox>
    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtnewpwd"
        ControlToValidate="txtconfpwd" ErrorMessage="Password not Match" ValidationGroup="1">*</asp:CompareValidator></td>
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
            <asp:Button ID="btnclose" runat="server" Text="Close" Width="65px" OnClick="btnclose_Click" /></td>
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
</div> 
</center>
</div> 
</asp:Content>

