<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login1.aspx.cs" Inherits="WHP14_Login1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     <title>Wheat Procurement Monitoring System ::</title>
  
     <script type="text/javascript" src="js/chksql.js"></script>
    <script type="text/javascript" src="js/md5.js"></script>

    <script type="text/javascript">
    window.history.forward(0); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
      <div>
            <table style="width: 100%;" align="center">
                <tr>
                    <td align="center" style="vertical-align: middle; width: 1000px; height: 100px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 1000px; vertical-align:middle;" align="center">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; border-right: gray 1px solid; border-top: gray 1px solid; border-left: gray 1px solid; border-bottom: gray 1px solid;">
                            <tr>
                                <td colspan="4" style="width: 600pc; height: 35px; background-color: #6699ff; font-size: 14pt; font-family: Arial;" align="center">
                                        <asp:Label ID="lblLoginUserName" runat="server" ForeColor="WhiteSmoke" Font-Bold="True" Text="Login"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="width: 50px; height: 25px">
                                </td>
                                <td style="width: 200px; height: 25px">
                                </td>
                                <td style="width: 300px; height: 25px">
                                        <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="True" Font-Names="Bookman Old Style" Font-Size="12pt" ForeColor="indianred" PostBackUrl="~/WPMS2014/MainPage.aspx" Visible="False">पिछले पृष्ट पर जाए </asp:LinkButton>
                                        <asp:Label ID="lbllogintext" runat="server"></asp:Label></td>
                                <td style="width: 50px; height: 25px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50px; height: 25px">
                                </td>
                                <td style="width: 200px; height: 25px" align="right">
                                        <asp:Label ID="lblSociety" runat="server" ForeColor="Green" Font-Names="Bookman Old Style" Font-Bold="True" Text="खरीदी केंद्र चुनें " Font-Italic="False"></asp:Label>
                                    &nbsp;
                                </td>
                                <td style="width: 300px; height: 25px">
                                        <asp:TextBox ID="ddl_Society" runat="server" Height="25px" Width="250px" AutoComplete="off" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtpwd" ErrorMessage="Please Enter Society Code" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
                                <td style="width: 50px; height: 25px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50px; height: 25px">
                                </td>
                                <td style="width: 200px; height: 25px">
                                </td>
                                <td style="width: 300px; height: 25px">
                                </td>
                                <td style="width: 50px; height: 25px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50px; height: 25px">
                                </td>
                                <td style="width: 200px; height: 25px" align="right">
                                        <asp:Label ID="Label5" runat="server" ForeColor="Green" Font-Names="Bookman Old Style" Font-Bold="True" Text="पासवर्ड प्रविष्ट करें " Font-Italic="False"></asp:Label>
                                    &nbsp;
                                </td>
                                <td style="width: 300px; height: 25px">
                                        <asp:TextBox ID="txtpwd" runat="server" Font-Size="12pt" Font-Bold="true" Width="250px" Height="25px" BorderColor="indianred" BorderWidth="1px" BorderStyle="Solid" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="1" ErrorMessage="Please Enter Password" ControlToValidate="txtpwd">*</asp:RequiredFieldValidator></td>
                                <td style="width: 50px; height: 25px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50px; height: 10px">
                                </td>
                                <td style="width: 200px; height: 10px">
                                </td>
                                <td style="width: 300px; height: 10px">
                                </td>
                                <td style="width: 50px; height: 10px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50px; height: 25px">
                                </td>
                                <td style="width: 200px; height: 25px">
                                </td>
                                <td style="width: 300px; height: 25px">
                                        <asp:Button ID="btnlogin" OnClick="btnlogin_Click" runat="server" Width="250px" Height="30px" ValidationGroup="1" Text="Login"></asp:Button></td>
                                <td style="width: 50px; height: 25px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50px; height: 25px">
                                </td>
                                <td style="width: 200px; height: 25px">
                                </td>
                                <td style="width: 300px; height: 25px">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="1" ShowMessageBox="True"></asp:ValidationSummary>
                                </td>
                                <td style="width: 50px; height: 25px">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="width: 600px; height: 60px; border-top: gray 1px solid; font-weight: bold; font-size: 12px; color: white; font-family: Arial; background-color: #6699ff;" align="center">
                                   Designed, Developed and Hosted By: National Informatics Centre,
                                        <br />
                                        Madhya Pradesh, Ministry of Communications and Information Technology
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
