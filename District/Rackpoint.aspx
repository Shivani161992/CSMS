<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Rackpoint.aspx.cs" Inherits="District_Rackpoint" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>

        <table style="width: 538px; height: 154px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
            <tr>
                <td style="height: 1px; border-right: black thin groove; border-top-style: groove; border-bottom: black thin groove; border-left-style: groove;" colspan="2">Please Choose One Option</td>
            </tr>
            <tr>
                <td style="height: 1px; border-right: black thin groove; border-top-style: groove; border-bottom: black thin groove; border-left-style: groove;">
                    <asp:RadioButton ID="rdfrmCenter" runat="server" Text="Received From Issue Center" GroupName="1" /></td>
                <td style="width: 150px; height: 1px; border-right: black thin groove;">
                    <asp:RadioButton ID="rdfrmuparjan" runat="server" Width="254px" Text="Received From Procurement" GroupName="1" /></td>
            </tr>
            <tr>
                <td style="height: 1px; border-right: black thin groove; border-top-style: groove; border-bottom: black thin groove; border-left-style: groove;">
                    <asp:RadioButton ID="rdbFrmRack" runat="server" Text="Received From Rack" GroupName="1" /></td>
                <td style="width: 150px; height: 1px; border-right: black thin groove;">
                   </td>
            </tr>
            <tr>
                <td style="height: 1px; border-right: black thin groove; border-top-style: groove; border-bottom: black thin groove; border-left-style: groove;">
                    <asp:Button ID="btnok" runat="server" Text="O.K." OnClick="btnok_Click" /></td>
                <td style="width: 150px; height: 1px; border-right: black thin groove; border-top: black thin groove;">
                    <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" /></td>
            </tr>
        </table>
    </center>
</asp:Content>


