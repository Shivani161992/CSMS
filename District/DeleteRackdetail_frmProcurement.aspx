<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="DeleteRackdetail_frmProcurement.aspx.cs" Inherits="District_DeleteRackdetail_frmProcurement" Title="Del Rack frm Procurement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<center>
    <table style="width: 690px; height: 350px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
        <tr>
            <td colspan="4" style="border-right: black thin groove; width: 100px; text-align: center; background-color: #cccc99; height: 1px;">
                <asp:Label ID="Label1" runat="server" Text="Delete Receipt frm Procurement" Width="357px"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="4" style="border-right: black thin groove; width: 700px; border-bottom: black thin groove; text-align: center; height: 1px;">
                <span style="font-size: 11pt; color: #ff0000">
                सम्बंधित एंट्री को डिलीट करने की पूर्ण जवाबदारी जिला प्रबंधक की रहेगी, कृपया जांच
                कर ही डिलीट करें</span></td>
        </tr>
        <tr>
            <td style="height: 1px; border-right: black thin groove; border-bottom: black thin groove;" colspan="2">
                <asp:Label ID="Label2" runat="server" Text="Select Rack Number" Width="163px"></asp:Label></td>
            <td style="height: 1px; border-right: black thin groove; border-bottom: black thin groove;" colspan="2">
                <asp:DropDownList ID="ddlracknumber" runat="server" Width="166px" AutoPostBack="True" OnSelectedIndexChanged="ddlracknumber_SelectedIndexChanged">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan="2" style="border-right: black thin groove; border-bottom: black thin groove;
                height: 1px">
                <asp:Label ID="Label10" runat="server" Text="Select TC" Width="137px"></asp:Label></td>
            <td colspan="2" style="border-right: black thin groove; border-bottom: black thin groove;
                height: 1px">
                <asp:DropDownList ID="ddlchallan" runat="server" Width="142px" OnSelectedIndexChanged="ddlchallan_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="height: 1px; border-right: black thin groove; border-bottom: black thin groove;" colspan="2">
                <asp:Label ID="Label3" runat="server" Text="Select Issue ID" Width="147px"></asp:Label></td>
            <td style="height: 1px; border-right: black thin groove; border-bottom: black thin groove;" colspan="2">
                <asp:DropDownList ID="ddlIssueID" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlIssueID_SelectedIndexChanged"
                    Width="261px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 127px; border-right: black thin groove; border-bottom: black thin groove; text-align: left; height: 1px;">
                <asp:Label ID="Label4" runat="server" Text="Received Date"></asp:Label></td>
            <td style="width: 109px; text-align: left; border-right: black thin groove; border-bottom: black thin groove; height: 1px;">
                <asp:Label ID="lblRecDate" runat="server"></asp:Label></td>
            <td style="width: 88px; border-right: black thin groove; border-bottom: black thin groove; text-align: left; height: 1px;">
                <asp:Label ID="Label6" runat="server" Text="Truck Number" Width="102px"></asp:Label></td>
            <td style="width: 132px; text-align: left; border-right: black thin groove; border-bottom: black thin groove; height: 1px;">
                <asp:Label ID="lblTruckNo" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 127px; border-right: black thin groove; border-bottom: black thin groove; text-align: left; height: 1px;">
                <asp:Label ID="Label8" runat="server" Text="Received Bags"></asp:Label></td>
            <td style="width: 109px; text-align: left; border-right: black thin groove; border-bottom: black thin groove; height: 1px;">
                <asp:Label ID="lblRecBags" runat="server"></asp:Label></td>
            <td style="width: 88px; border-right: black thin groove; border-bottom: black thin groove; text-align: left; height: 1px;">
                <asp:Label ID="Label9" runat="server" Text="Received Qty"></asp:Label></td>
            <td style="width: 132px; text-align: left; border-right: black thin groove; border-bottom: black thin groove; height: 1px;">
                <asp:Label ID="lblRecQty" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="border-right: black thin groove; width: 127px; border-bottom: black thin groove;
                height: 1px; text-align: left">
                <asp:Label ID="lblwp_oper" runat="server" Visible="False"></asp:Label></td>
            <td style="border-right: black thin groove; width: 109px; border-bottom: black thin groove;
                height: 1px; text-align: left">
                <asp:Label ID="lbloperation" runat="server" Visible="False"></asp:Label></td>
            <td style="border-right: black thin groove; width: 88px; border-bottom: black thin groove;
                height: 1px; text-align: left">
                <asp:Label ID="lblgdnId" runat="server" Visible="False"></asp:Label></td>
            <td style="border-right: black thin groove; width: 132px; border-bottom: black thin groove;
                height: 1px; text-align: left">
            </td>
        </tr>
        <tr>
            <td style="border-right: black thin groove; border-bottom: black thin groove; text-align: left; height: 1px;" colspan="4">
                <asp:Label ID="lblerror" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 127px; border-right: black thin groove; border-bottom: black thin groove; text-align: left; height: 1px;">
                <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" Font-Bold="True" ForeColor="Blue" Width="68px" /></td>
            <td style="width: 109px; border-right: black thin groove; border-bottom: black thin groove; text-align: left; height: 1px;">
                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" Font-Bold="True" ForeColor="Blue" Width="86px" /></td>
            <td style="width: 88px; border-right: black thin groove; border-bottom: black thin groove; text-align: left; height: 1px;">
                <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" Font-Bold="True" ForeColor="Blue" Width="73px" /></td>
            <td style="width: 132px; border-right: black thin groove; border-bottom: black thin groove; text-align: left; height: 1px;">
            </td>
        </tr>
    </table>
    </center>

</asp:Content>

