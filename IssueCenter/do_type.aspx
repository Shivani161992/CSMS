<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="do_type.aspx.cs" Inherits="IssueCenter_do_type" Title="Delivery Type" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 620px; border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid;">
        <tr>
            <td align="center" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
    <table style="background-color: #cfdcdc; border-right: navy 1px double; border-top: navy 1px double; font-size: 10pt; border-left: navy 1px double; border-bottom: navy 1px double; position: static;" border="1" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="3" style="color: #0066ff; font-style: italic; height: 36px; background-color: lightslategray;">
                <asp:Label ID="lbldotype" runat="server" Text="Please select Issued Type for Delivery Order" Font-Size="14px" Font-Bold="True" Font-Italic="True" ForeColor="White"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 100px; height: 53px;" align="right">
                <asp:Label ID="lbldoselect" runat="server" Text="Issued To" Font-Size="12px"></asp:Label></td>
            <td style="width: 100px; height: 53px;">
                <asp:DropDownList ID="ddl_issueto" runat="server" OnSelectedIndexChanged="ddl_issueto_SelectedIndexChanged">
                      <asp:ListItem Value="DS">Door Step</asp:ListItem>
                      <asp:ListItem Value="Pay">Door Step Payment Details</asp:ListItem>
                       <asp:ListItem Value="TO">Issue Against TO</asp:ListItem>
                    <asp:ListItem Value="LF">Lead Society with FPS</asp:ListItem>
                    <asp:ListItem Value="L">Only Lead Society</asp:ListItem>
                    <asp:ListItem Value="F">Only FPS</asp:ListItem>

                    <asp:ListItem Value="O">Others</asp:ListItem>
                    
                     <asp:ListItem Value="MP">MPSCSC</asp:ListItem>
                    
                </asp:DropDownList></td>
            <td style="width: 100px; height: 53px;">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 100px; height: 52px;">
                &nbsp;</td>
            <td align="center">
                <asp:Button ID="btnok" runat="server" OnClick="btnok_Click" Text="OK" Width="91px" /></td>
            <td style="width: 100px; height: 52px;">
                &nbsp;</td>
        </tr>
    </table>
            </td>
        </tr>
    </table>
</asp:Content>

