<%@ Page Language="C#" MasterPageFile="~/MasterPage/Regional_Office.master" AutoEventWireup="true" CodeFile="Welcome_RO.aspx.cs" Inherits="Regional_Office_Welcome_RO" Title="WelCome Regional Office MPSCSC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/nnn.jpg" Width="623px" />
    <script type="text/javascript">
// <![CDATA[
var myMenu;
window.onload = function() {
myMenu = new SDMenu("my_menu");
myMenu.init();
};
// ]]>
</script>
</asp:Content>

