<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="TruckReciept_master.aspx.cs" Inherits="IssueCenter_Godown_master" Title="Truck Receipt Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="trans">
<center >
<table>
<tr>
<td> 
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged1">
        <Columns>
            <asp:CommandField HeaderText="Action" ShowSelectButton="True" />
            <asp:BoundField DataField="Truck_Challan" HeaderText="Truck Challan No." ReadOnly="True"
                SortExpression="Truck_Challan" />
            <asp:BoundField DataField="Truck_No" HeaderText="Truck Number" ReadOnly="True"
                SortExpression="Truck_No" />
        </Columns>
    </asp:GridView>
    <asp:Label ID="Label1" runat="server" Visible ="False" ForeColor="#C04000" Width="250px" ></asp:Label>
</td>

</tr>
</table>
</center>

<div>
<table>
<tr>
<td><asp:Button ID="btnaddnew" runat="server" Text="AddNew" OnClick="btnaddnew_Click1" /> </td>
</tr>

</table>
</div>
<div id = "detail1" class ="detail1">

<center >
<table cellpadding ="0" cellspacing ="0" border ="0" >
<tr>
<td><asp:Label ID="lbltname" runat="server" Text="Truck Challan No." Visible="False"></asp:Label></td>
<td>
    <asp:TextBox ID="txtchallanno" runat="server" Visible="False"></asp:TextBox></td>
</tr>
<tr>
<td align="left"><asp:Label ID="lbltadd" runat="server" Text="Truck No." Visible="False"></asp:Label></td>
<td>
    <asp:TextBox ID="txttruckno" runat="server" Visible="False"></asp:TextBox></td>
</tr>
<tr>
<td>
    <asp:Button ID="btnadd" runat="server" Text="Add" OnClick="btnadd_Click" Width="48px" Visible="False" /><asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" Visible="False" /></td>
<td align="left">
    &nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" Width="81px" /></td>
</tr>
</table>
</center>
</div>
</div>
</asp:Content>

