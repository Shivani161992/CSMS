<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="SearchId.aspx.cs" Inherits="IssueCenter_SearchId" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<center>
    <table style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; width: 625px; border-bottom: black thin groove; height: 482px">
        <tr>
            <td style="width: 225px; height: 1px">
                CropCrop Type</td>
            <td style="width: 79px; height: 1px; text-align: left">
                <asp:DropDownList ID="ddlcroptype" runat="server" Width="178px">
                    <asp:ListItem Value="0">Wheat(गेहूँ)</asp:ListItem>
                    <asp:ListItem Value="1">Paddy(धान)</asp:ListItem>
                    <asp:ListItem Value="2">Coarse Grain(मोटा आनाजं)</asp:ListItem>
                </asp:DropDownList></td>
            <td style="width: 55px; height: 1px">
            </td>
        </tr>
        <tr>
            <td style="width: 225px; height: 1px;">
                जारी क्रमांक भरिये</td>
            <td style="width: 79px; height: 1px; text-align: left;">
                <asp:TextBox ID="txtId" runat="server" Width="224px"></asp:TextBox></td>
            <td style="width: 55px; height: 1px;">
                <asp:Button ID="search" runat="server" OnClick="search_Click" Text="Search" /></td>
        </tr>
        <tr>
            <td style="width: 225px; height: 1px;">
                Dispatch Date>
            <td style="width: 79px; height: 1px; text-align: left;">
                <asp:Label ID="dispdate" runat="server" ForeColor="#0000C0"></asp:Label></td>
            <td style="width: 55px; height: 1px;">
            </td>
        </tr>
        <tr>
            <td style="width: 225px; height: 1px">
                Sending Procurement</td>
            <td style="width: 79px; height: 1px; text-align: left">
                <asp:Label ID="senproc" runat="server"></asp:Label></td>
            <td style="width: 55px; height: 1px">
            </td>
        </tr>
        <tr>
            <td style="width: 225px; height: 1px">
                Send Bags</td>
            <td style="width: 79px; height: 1px; text-align: left">
                <asp:Label ID="sendbags" runat="server"></asp:Label></td>
            <td style="width: 55px; height: 1px">
            </td>
        </tr>
        <tr>
            <td style="width: 225px; height: 1px">
                Send Quantity</td>
            <td style="width: 79px; height: 1px; text-align: left">
                <asp:Label ID="sendQty" runat="server"></asp:Label></td>
            <td style="width: 55px; height: 1px">
            </td>
        </tr>
        <tr>
            <td style="width: 225px; height: 1px;">
                Receiving Date</td>
            <td style="width: 79px; height: 1px; text-align: left;">
                <asp:Label ID="Recdate" runat="server" ForeColor="#0000C0"></asp:Label></td>
            <td style="width: 55px; height: 1px;">
            </td>
        </tr>
        <tr>
            <td style="width: 225px; height: 1px;">
                Challan Number</td>
            <td style="width: 79px; height: 1px; text-align: left;">
                <asp:Label ID="challan" runat="server"></asp:Label></td>
            <td style="width: 55px; height: 1px;">
            </td>
        </tr>
        <tr>
            <td style="width: 225px; height: 1px;">
                Receiving District</td>
            <td style="width: 79px; height: 1px; text-align: left;">
                <asp:Label ID="Recdist" runat="server" ForeColor="Green"></asp:Label></td>
            <td style="width: 55px; height: 1px;">
            </td>
        </tr>
        <tr>
            <td style="width: 225px; height: 1px;">
                Receiving Issue center</td>
            <td style="width: 79px; height: 1px; text-align: left;">
                <asp:Label ID="RecIssuecenter" runat="server" ForeColor="Green"></asp:Label></td>
            <td style="width: 55px; height: 1px;">
            </td>
        </tr>
        <tr>
            <td style="width: 225px; height: 1px">
                Recd Bags</td>
            <td style="width: 79px; height: 1px; text-align: left">
                <asp:Label ID="recbags" runat="server"></asp:Label></td>
            <td style="width: 55px; height: 1px">
            </td>
        </tr>
        <tr>
            <td style="width: 225px; height: 1px">
                Recd Qty</td>
            <td style="width: 79px; height: 1px; text-align: left">
                <asp:Label ID="RecQty" runat="server"></asp:Label></td>
            <td style="width: 55px; height: 1px">
            </td>
        </tr>
        <tr>
            <td style="width: 225px; height: 1px">
                Acceptance in WPMS</td>
            <td style="width: 79px; height: 1px; text-align: left">
                <asp:Label ID="wpmaccept" runat="server" ForeColor="#003300"></asp:Label></td>
            <td style="width: 55px; height: 1px">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 225px; height: 1px">
                Acceptance Number(CSMS)</td>
            <td style="width: 79px; height: 1px; text-align: left">
                <asp:Label ID="Accepnum" runat="server" ForeColor="#C00000"></asp:Label></td>
            <td style="width: 55px; height: 1px">
            </td>
        </tr>
        <tr>
            <td style="width: 225px; height: 1px">
                Acceptance Date</td>
            <td style="width: 79px; height: 1px; text-align: left">
                <asp:Label ID="AccepDate" runat="server" ForeColor="#C00000"></asp:Label></td>
            <td style="width: 55px; height: 1px">
            </td>
        </tr>
        <tr>
            <td style="width: 225px; height: 1px">
                WHR Number</td>
            <td style="width: 79px; height: 1px; text-align: left">
                <asp:Label ID="Whrnum" runat="server" ForeColor="#C00000"></asp:Label></td>
            <td style="width: 55px; height: 1px">
            </td>
        </tr>
        <tr>
            <td style="width: 225px; height: 1px">
                WHR Date</td>
            <td style="width: 79px; height: 1px; text-align: left">
                <asp:Label ID="WhrDate" runat="server" ForeColor="#C00000"></asp:Label></td>
            <td style="width: 55px; height: 1px">
            </td>
        </tr>
        <tr>
            <td style="width: 225px; height: 1px">
                Found in CSMS</td>
            <td style="width: 79px; height: 1px; text-align: left">
                <asp:Label ID="incsms" runat="server"></asp:Label></td>
            <td style="width: 55px; height: 1px">
            </td>
        </tr>
        <tr>
            <td style="width: 225px; height: 1px">
                Found in Procurement</td>
            <td style="width: 79px; height: 1px; text-align: left">
                <asp:Label ID="inwpms" runat="server"></asp:Label></td>
            <td style="width: 55px; height: 1px">
            </td>
        </tr>
    </table>
    </center>


</asp:Content>

