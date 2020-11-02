<%@ Page Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="SearchQry.aspx.cs" Inherits="State_SearchQry" Title="Search Query Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 1100px; height: 500px">
        <tr>
            <td style="height: 32px; text-align: left; background-color: #FFCC99; font-size: large;">
            
                Select&nbsp;&nbsp;&nbsp; *&nbsp;&nbsp; from&nbsp;&nbsp;&nbsp;&nbsp;
            
                <asp:TextBox ID="TextBox1" runat="server" Height="25px" Width="490px"></asp:TextBox>
                &nbsp;<asp:Button ID="Button1" runat="server" Text="Search" Width="115px" 
                    onclick="Button1_Click" />
            
            </td>
        </tr>
        <tr>
            <td style="height: 1px; text-align: left; background-color: Blue; font-size: large;">
            
                &nbsp;</td>
        </tr>
        <tr>
            <td valign = "top" style="height: 1px; text-align: left">

                <asp:Label ID="Label2" runat="server"></asp:Label>
                </td>
        </tr>
        <tr>
            <td valign = "top">

<asp:Panel ID = "pnl" runat = "server" ScrollBars = "Auto">
                <asp:GridView ID="GridView1" runat="server">
                </asp:GridView>
</asp:Panel>
                </td>
        </tr>
    </table>
</asp:Content>

