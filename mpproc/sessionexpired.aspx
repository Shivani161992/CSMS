<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sessionexpired.aspx.cs" Inherits="sessionexpired" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;
        <table class="sessionexptab" style="border-right: olive 3px double; border-top: olive 3px double;
            z-index: 100; left: 174px; border-left: olive 3px double; border-bottom: olive 3px double;
            position: absolute; top: 172px">
            <tr>
                <td align="center" style="color: #b22222; font-family: Verdana; background-color: #cccc66">
                    Your Login Session has been Expired
                </td>
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
                <td align="center" style="color: #b22222; font-family: Verdana; height: 20px; background-color: #cccc66">
                    <asp:HyperLink ID="HyperLink1" runat="server" ForeColor="Maroon" NavigateUrl="~/mpproc/frmLogin.aspx">Go to the login page</asp:HyperLink></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
