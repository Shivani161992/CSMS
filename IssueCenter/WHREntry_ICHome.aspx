<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="WHREntry_ICHome.aspx.cs" Inherits="IssueCenter_WHREntry_ICHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 620px; border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid;">
        <tr>
            <td align="center" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
                <table style="background-color: #cfdcdc; border-right: navy 1px double; border-top: navy 1px double; font-size: 10pt; border-left: navy 1px double; border-bottom: navy 1px double; position: static;" border="1" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="2" style="color: #0066ff; font-style: italic; height: 36px; background-color: lightslategray;">
                            <asp:Label ID="lbldotype" runat="server" Text="Please select Commodity" Font-Size="14px" Font-Bold="True" Font-Italic="True" ForeColor="White"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 140px; height: 53px;" align="right">
                            <asp:Label ID="lblComdty" runat="server" Text="Commodity"
                                Font-Size="12px" Style="font-weight: 700; font-size: medium"
                                Width="170px"></asp:Label></td>
                        <td style="width: 139px; height: 53px;">
                            <asp:DropDownList ID="ddlcomdty" runat="server" Height="23px" Width="150px">
                            </asp:DropDownList></td>

                    </tr>
                    <tr>
                        <td style="height: 52px; text-align: center" colspan="2">
                            <asp:Button ID="btnok" runat="server" OnClick="btnok_Click" Text="OK" Width="91px" /></td>

                    </tr>
                </table>
            </td>
        </tr>
    </table>

</asp:Content>

