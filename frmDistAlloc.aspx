<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDistAlloc.aspx.vb" Inherits="frmDistAlloc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>District Allocation</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="z-index: 101; left: 8px; width: 624px; color: maroon; font-family: 'Arial Black';
            position: absolute; top: 8px; height: 24px; text-align: center">
            District Level Allocation</div>
        <br />
        <table>
            <tr>
                <td style="width: 17px; height: 21px">
                    DC</td>
                <td style="width: 100px; height: 21px">
                    District
                </td>
                <td colspan="3" style="height: 21px; text-align: center">
                    Wheat
                </td>
                <td colspan="3" style="height: 21px; text-align: center">
                    Rice</td>
                <td style="width: 100px; height: 21px">
                    Sugar</td>
                <td style="width: 100px; height: 21px">
                    Kerosene</td>
                <td style="width: 100px; height: 21px">
                    Salt</td>
            </tr>
            <tr>
                <td style="width: 17px">
                <asp:Label ID="Label1" runat="server"></asp:Label></td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="width: 17px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="lblmsg" runat="server" Style="z-index: 102; left: 40px; position: absolute;
            top: 432px"></asp:Label>
    
    </div>
    </form>
</body>
</html>
