<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Print_TO.aspx.cs" Inherits="District_Print_TO" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<center>
<table>
    <tr>
        <td style="width: 208px">
            <asp:Label ID="Label1" runat="server" Text="Select Issue Center"></asp:Label></td>
        <td style="width: 297px">
            <asp:DropDownList ID = "ddlissueCenter" runat = "server" OnSelectedIndexChanged="ddlissueCenter_SelectedIndexChanged" Width="185px" AutoPostBack="True">
            </asp:DropDownList></td>
    </tr>
<tr>

<td style="width: 208px">
<asp:Label ID = "lbl" runat = "server" Text = "Select Transport Order"></asp:Label>

</td>

<td style="width: 297px">
<asp:DropDownList ID = "ddlto" runat = "server" OnSelectedIndexChanged="ddlto_SelectedIndexChanged" Width="256px" AutoPostBack="True"></asp:DropDownList>
</td>


</tr>
    <tr>
        <td style="width: 208px">
        </td>
        <td style="width: 297px">
        </td>
    </tr>
    <tr>
        <td style="width: 208px">
        </td>
        <td style="width: 297px">
            &nbsp;<asp:HyperLink ID="hlinkpdo" runat="server" Font-Size="11pt" NavigateUrl="#"
                Visible="False" Width="102px">Print This</asp:HyperLink></td>
    </tr>

</table>
</center>


</asp:Content>

