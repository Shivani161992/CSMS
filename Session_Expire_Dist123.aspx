<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="Session_Expire_Dist123.aspx.cs" Inherits="Session_Expire_Dist" Title="Session Expired" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table >
<tr>
<td style="color: white; font-family: Verdana; background-color: maroon">Your Login Session has been Expired </td>
</tr>
<tr>
<td> 
    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/sessionexp.jpg" /></td>
</tr>
    <tr>
        <td>
        </td>
    </tr>
    <tr>
        <td>
        </td>
    </tr>
<tr>
<td style="color: white; font-family: Verdana; height: 20px; background-color: maroon"> 
    <asp:HyperLink ID="HyperLink1" runat="server" ForeColor="White" NavigateUrl="~/MainLogin.aspx">Go to the login page</asp:HyperLink></td>
</tr>

</table>
</asp:Content>

