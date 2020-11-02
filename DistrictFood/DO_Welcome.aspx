<%@ Page Language="C#" MasterPageFile="~/MasterPage/DF_MPSCSC.master" AutoEventWireup="true" CodeFile="DO_Welcome.aspx.cs" Inherits="DO_Welcome" Title="Welcome District Food Office (MPSCSC)" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/nnn.jpg" Width="617px" />
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

