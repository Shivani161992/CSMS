<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="TruckReciept_master.aspx.cs" Inherits="IssueCenter_Godown_master" Title="Truck Receipt Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id ="head">
<center >
<table>
<tr>
<td> 
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:CommandField HeaderText="Action" ShowSelectButton="True" />
            <asp:BoundField DataField="Godown_ID" HeaderText="Godown Name" ReadOnly="True"
                SortExpression="Godown_ID" />
            <asp:BoundField DataField="Godown_Capacity" HeaderText="Godown Capacity" ReadOnly="True"
                SortExpression="Godown_Capacity" />
        </Columns>
    </asp:GridView>
    <asp:Label ID="Label1" runat="server" Text="" Visible ="false" ></asp:Label>
</td>

</tr>
</table>
</center>
</div>
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
<td><asp:Label ID="lbltname" runat="server" Text="Gowdn Name"></asp:Label></td>
<td>
    <asp:TextBox ID="txtgname" runat="server"></asp:TextBox></td>
</tr>
<tr>
<td><asp:Label ID="lbltadd" runat="server" Text="Capacity (Qtls.)"></asp:Label></td>
<td>
    <asp:TextBox ID="txtcapacty" runat="server"></asp:TextBox></td>
</tr>
<tr>
<td>
    <asp:Button ID="btnadd" runat="server" Text="Add" OnClick="btnadd_Click" Width="48px" /><asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" /></td>
<td>
    &nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Update" /></td>
</tr>
</table>
</center>
</div>
</asp:Content>

