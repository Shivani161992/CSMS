<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Session_Expire_Dist.aspx.cs" Inherits="Session_Expire_Dist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Session Expired </title>
    
     <link rel="stylesheet" href = "../MyCSS/menu.css" ="text/css" />
   
</head>
<body>
    <form id="form1" runat="server">
    
    </form>
    <div id="sessionexp">
    <table style="border-right: olive 3px double; border-top: olive 3px double; border-left: olive 3px double; border-bottom: olive 3px double; z-index: 100; left: 174px; position: absolute; top: 172px;" class="sessionexptab" >
<tr>
<td style="color: white; font-family: Verdana; background-color: maroon" align="center">Your Login Session has been Expired </td>
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
<td style="color: white; font-family: Verdana; height: 20px; background-color: maroon" align="center"> 
    <asp:HyperLink ID="HyperLink1" runat="server" ForeColor="White" NavigateUrl="~/MainLogin.aspx">Go to the login page</asp:HyperLink></td>
</tr>

</table>
    </div>
</body>
</html>
