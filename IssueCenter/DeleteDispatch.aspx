<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="DeleteDispatch.aspx.cs" Inherits="IssueCenter_DeleteDispatch" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <center>
    <table style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; width: 615px; border-bottom: black thin groove; height: 434px">
        <tr>
            <td colspan="4" style="border-right: black thin groove; border-top: black thin groove;
                border-left: black thin groove; border-bottom: black thin groove; height: 1px">
                <span style="font-size: 16pt">Delete Dispatch Details</span></td>
        </tr>
        <tr>
            <td colspan="4" style="border-right: black thin groove; border-top: black thin groove;
                border-left: black thin groove; border-bottom: black thin groove; height: 1px">
                <span style="color: #ff0000; font-family: Verdana">कृपया सावधानी पूर्वक डिलीट करे, डिलीट
                    होने के बाद डाटा की रिकवरी नहीं हो सकेगी</span></td>
        </tr>
        <tr>
            <td style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 1px;" colspan="2">
                <asp:RadioButton ID="RdbyRoad" runat="server" AutoPostBack="True" Font-Bold="True"
                    ForeColor="#0000C0" GroupName="1" OnCheckedChanged="RdbyRoad_CheckedChanged"
                    Text="Dispatch By Road" Width="181px" /></td>
            <td style="width: 100px; border-right: black thin ridge; border-top: black thin ridge; border-left: black thin ridge; border-bottom: black thin ridge; height: 2px;" colspan="2">
                <asp:RadioButton ID="RdRack" runat="server" AutoPostBack="True" Font-Bold="True"
                    ForeColor="#0000C0" GroupName="1" OnCheckedChanged="RdRack_CheckedChanged" Text="Dispatch By Rack"
                    Width="177px" /></td>
        </tr>
        <tr>
            <td style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 1px;">
                <asp:Label ID="lblroadchallan" runat="server" Text="Select Truck Challan" Width="131px"></asp:Label></td>
            <td style="width: 63px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 1px;">
                <asp:DropDownList ID="ddlroadchallan" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlroadchallan_SelectedIndexChanged"
                    Width="161px">
                </asp:DropDownList></td>
            <td style="width: 112px; border-right: black thin ridge; border-top: black thin ridge; border-left: black thin ridge; border-bottom: black thin ridge; height: 1px;">
                <asp:Label ID="lblracknum" runat="server" Text="Select Rack Number" Width="142px"></asp:Label></td>
            <td style="width: 112px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 1px;">
                <asp:DropDownList ID="ddlracknumber" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlracknumber_SelectedIndexChanged"
                    Width="230px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove;
                border-bottom: black thin groove; height: 1px">
                <asp:Label ID="LblSource" runat="server" Font-Bold="True" ForeColor="Blue" Visible="False"></asp:Label></td>
            <td style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove;
                width: 63px; border-bottom: black thin groove; height: 1px">
                <asp:Label ID="lbldispatchgodown" runat="server" Font-Bold="True" ForeColor="Blue"
                    Visible="False"></asp:Label></td>
            <td style="border-right: black thin ridge; border-top: black thin ridge; border-left: black thin ridge;
                width: 112px; border-bottom: black thin ridge; height: 1px">
                <asp:Label ID="lblrackchallan" runat="server" Text="Select Challan Number" Width="142px"></asp:Label></td>
            <td style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove;
                width: 112px; border-bottom: black thin groove; height: 1px">
                <asp:DropDownList ID="ddlrackChallan" runat="server" OnSelectedIndexChanged="ddlrackChallan_SelectedIndexChanged"
                    Width="222px" AutoPostBack="True">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 1px; width: 63px; background-color: #ccccff;">
                <asp:Label ID="lbldisCenetr" runat="server" Text="Send To" Width="127px" Font-Bold="True" ForeColor="#0000FF"></asp:Label></td>
            <td style="width: 63px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 1px; background-color: #ccccff;">
                <asp:Label ID="Label3" runat="server" Width="160px" Font-Bold="True" ForeColor="Maroon"></asp:Label></td>
            <td style="width: 63px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 1px; background-color: #ccccff;">
                <asp:Label ID="lbldepot" runat="server" Width="140px" Font-Bold="True" ForeColor="#3300FF">From Depot</asp:Label></td>
            <td style="width: 112px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 1px; background-color: #ccccff;">
                <asp:Label ID="lblDepotName" runat="server" Width="232px" Font-Bold="True" ForeColor="Maroon"></asp:Label></td>
        </tr>
        <tr>
            <td style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 1px; width: 63px; background-color: #ccccff;">
                <asp:Label ID="lbl5" runat="server" Text="Commodity" Width="130px" Font-Bold="True" ForeColor="#0000FF"></asp:Label></td>
            <td style="width: 63px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 1px; background-color: #ccccff;">
                <asp:Label ID="LblCommodity" runat="server" Width="159px" Font-Bold="True" ForeColor="Maroon"></asp:Label></td>
            <td style="width: 63px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 1px; background-color: #ccccff;">
                <asp:Label ID="Lbl6" runat="server" Text="Scheme" Width="132px" Font-Bold="True" ForeColor="#0000FF"></asp:Label></td>
            <td style="width: 112px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 1px; background-color: #ccccff;">
                <asp:Label ID="LblScheme" runat="server" Font-Bold="True" ForeColor="Maroon" Width="231px"></asp:Label></td>
        </tr>
        <tr>
            <td style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 1px; width: 63px; background-color: #ccccff;">
                <asp:Label ID="Lbl8" runat="server" Text="Send Quantity" Font-Bold="True" ForeColor="#0000FF" Width="127px"></asp:Label></td>
            <td style="width: 63px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 1px; background-color: #ccccff;">
                <asp:Label ID="LblQty" runat="server" Font-Bold="True" ForeColor="Maroon" Width="159px"></asp:Label></td>
            <td style="width: 63px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 1px; background-color: #ccccff;">
                <asp:Label ID="Lbl9" runat="server" Text="Send Bags" Width="136px" Font-Bold="True" ForeColor="#0000FF"></asp:Label></td>
            <td style="width: 112px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 1px; background-color: #ccccff;">
                <asp:Label ID="LblBags" runat="server" Font-Bold="True" ForeColor="Maroon" Width="232px"></asp:Label></td>
        </tr>
        <tr>
            <td style="border-right: black thin ridge; border-top: black thin ridge; border-left: black thin ridge;
                border-bottom: black thin ridge; height: 1px; width: 112px; background-color: #ccccff;">
                <asp:Label ID="lbldte" runat="server" Text="Label" Font-Bold="True" ForeColor="#0000FF"></asp:Label></td>
            <td style="border-right: black thin ridge; border-top: black thin ridge; border-left: black thin ridge;
                width: 112px; border-bottom: black thin ridge; height: 1px; background-color: #ccccff;">
                <asp:Label ID="lbldate" runat="server" Font-Bold="True" ForeColor="Maroon" Width="159px"></asp:Label></td>
            <td style="border-right: black thin ridge; border-top: black thin ridge; border-left: black thin ridge;
                width: 112px; border-bottom: black thin ridge; height: 1px; background-color: #ccccff;">
                <asp:Label ID="lbTrucknum" runat="server" Text="Truck Number" Width="138px" Font-Bold="True" ForeColor="#0000FF"></asp:Label></td>
            <td style="border-right: black thin ridge; border-top: black thin ridge; border-left: black thin ridge;
                width: 112px; border-bottom: black thin ridge; height: 1px; background-color: #ccccff;">
                <asp:Label ID="Label4" runat="server" Width="231px" Font-Bold="True" ForeColor="Maroon"></asp:Label></td>
        </tr>
        <tr>
            <td style="border-right: black thin ridge; border-top: black thin ridge; border-left: black thin ridge;
                width: 112px; border-bottom: black thin ridge; height: 1px; background-color: #ccccff">
                <asp:Label ID="lbldistype" runat="server" Font-Bold="True" ForeColor="Blue" Text="Dispatch Type"
                    Width="138px"></asp:Label></td>
            <td style="border-right: black thin ridge; border-top: black thin ridge; border-left: black thin ridge;
                width: 112px; border-bottom: black thin ridge; height: 1px; background-color: #ccccff">
                <asp:Label ID="LblDispatch" runat="server" Font-Bold="True" ForeColor="Maroon" Width="159px"></asp:Label></td>
            <td style="border-right: black thin ridge; border-top: black thin ridge; border-left: black thin ridge;
                width: 112px; border-bottom: black thin ridge; height: 1px; background-color: #ccccff">
                <asp:Label ID="comid" runat="server" Visible="False" Width="1px"></asp:Label>
                <asp:Label ID="schId" runat="server" Visible="False" Width="37px"></asp:Label></td>
            <td style="border-right: black thin ridge; border-top: black thin ridge; border-left: black thin ridge;
                width: 112px; border-bottom: black thin ridge; height: 1px; background-color: #ccccff">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Maroon" Visible="False"
                    Width="226px"></asp:Label></td>
        </tr>
        <tr>
            <td style="border-top-width: thin; border-left-width: thin; border-left-color: black; border-bottom-width: thin; border-bottom-color: black; border-top-color: black; height: 1px; border-right-width: thin; border-right-color: black; background-color: #999966;" colspan="4">
            </td>
        </tr>
        <tr>
            <td style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove;
                border-bottom: black thin groove; height: 1px">
            </td>
            <td style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove;
                width: 63px; border-bottom: black thin groove; height: 1px">
                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" /></td>
            <td style="border-right: black thin ridge; border-top: black thin ridge; border-left: black thin ridge;
                width: 112px; border-bottom: black thin ridge; height: 1px">
                <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" /></td>
            <td style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove;
                width: 112px; border-bottom: black thin groove; height: 1px">
            </td>
        </tr>
        <tr>
            <td style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove;
                border-bottom: black thin groove; height: 1px">
            </td>
            <td style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove;
                width: 63px; border-bottom: black thin groove; height: 1px">
            </td>
            <td style="border-right: black thin ridge; border-top: black thin ridge; border-left: black thin ridge;
                width: 112px; border-bottom: black thin ridge; height: 1px">
            </td>
            <td style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove;
                width: 112px; border-bottom: black thin groove; height: 1px">
            </td>
        </tr>
        <tr>
            <td style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove;
                border-bottom: black thin groove; height: 1px">
            </td>
            <td style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove;
                width: 63px; border-bottom: black thin groove; height: 1px">
            </td>
            <td style="border-right: black thin ridge; border-top: black thin ridge; border-left: black thin ridge;
                width: 112px; border-bottom: black thin ridge; height: 1px">
            </td>
            <td style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove;
                width: 112px; border-bottom: black thin groove; height: 1px">
            </td>
        </tr>
        <tr>
            <td style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove;
                border-bottom: black thin groove; height: 1px">
            </td>
            <td style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove;
                width: 63px; border-bottom: black thin groove; height: 1px">
            </td>
            <td style="border-right: black thin ridge; border-top: black thin ridge; border-left: black thin ridge;
                width: 112px; border-bottom: black thin ridge; height: 1px">
            </td>
            <td style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove;
                width: 112px; border-bottom: black thin groove; height: 1px">
            </td>
        </tr>
        <tr>
            <td style="border-top-width: thin; border-left-width: thin; border-left-color: black; border-bottom-width: thin; border-bottom-color: black; border-top-color: black; height: 4px; border-right-width: thin; border-right-color: black;" colspan="4">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="border-top-width: thin; border-left-width: thin; border-left-color: black;
                border-bottom-width: thin; border-bottom-color: black; border-top-color: black;
                height: 2px; border-right-width: thin; border-right-color: black">
            </td>
        </tr>
    </table>
    </center>
</asp:Content>

