<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Godown_master.aspx.cs" Inherits="IssueCenter_Godown_master" Title="Godown Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse; border-right: green 3px double; border-top: green 3px double; border-left: green 3px double; border-bottom: green 3px double; width: 328px;" id="">
    <tr>
        <td>
                    <asp:Label ID="lblGodownMaster" runat="server" Text="Godown Master"></asp:Label></td>
    </tr>
    <tr>
        <td>
            <asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" Height="250px">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PageSize="5" Width="322px" OnPageIndexChanged="GridView1_PageIndexChanged" OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
        <Columns>
            <asp:CommandField HeaderText="Action" ShowSelectButton="True" >
                <HeaderStyle Font-Size="12px" />
                <ItemStyle Font-Size="11px" />
            </asp:CommandField>
            <asp:BoundField DataField="Godown_ID" HeaderText="GID" SortExpression="Godown_ID">
                <ItemStyle Font-Size="11px" />
                <HeaderStyle Font-Underline="False" CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" ReadOnly="True"
                SortExpression="Godown_Name" >
                <ItemStyle Font-Size="11px" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Godown_Capacity" HeaderText="Godown Capacity" ReadOnly="True"
                SortExpression="Godown_Capacity" >
                <ItemStyle Font-Size="11px" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Current_Balance" HeaderText="Current Capacity">
                <HeaderStyle CssClass="gridlarohead" />
                <ItemStyle Font-Size="11px" />
            </asp:BoundField>
        </Columns>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="True" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <EditRowStyle BackColor="#999999" />
    </asp:GridView>
            </asp:Panel>
        </td>
    </tr>
<tr>
<td> &nbsp;<asp:Label ID="Label1" runat="server" Text="" Visible ="false" ></asp:Label>
</td>

</tr>
    <tr>
        <td style="height: 18px">
            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Maroon"
                Visible="False"></asp:Label>
            <asp:Button ID="btnaddnew" runat="server" Text="AddNew" OnClick="btnaddnew_Click1" Width="102px" /></td>
    </tr>
</table>



<div>
    &nbsp;</div>
<div id = "detail1" class ="detail1">

<center >
<asp:Panel ID="Panel1" runat="server" Height="59px" Width="319px" Visible="False">
<table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse; border-right: green 3px double; border-top: green 3px double; border-left: green 3px double; border-bottom: green 3px double; width: 328px;">
<tr>
<td align="left"><asp:Label ID="lblGodownNo" runat="server" Text="Godown Name" Font-Size="10pt"></asp:Label></td>
<td align="left">
    <asp:TextBox ID="txtgname" runat="server" AutoPostBack="True" OnTextChanged="txtgname_TextChanged"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtgname"
        ErrorMessage="Godown Name Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
</tr>
<tr>
<td align="left"><asp:Label ID="lblMaxCap" runat="server" Text="Capacity (Qtls.)" Font-Size="10pt"></asp:Label></td>
<td align="left">
    <asp:TextBox ID="txtcapacty" runat="server" MaxLength="11"></asp:TextBox></td>
</tr>
<tr>
<td align="left">
    <asp:Button ID="btnsubmit" runat="server" Text="Add" OnClick="btnsubmit_Click" Width="72px" ValidationGroup="1" />
    <asp:Button ID="btnupdate" runat="server" Text="Update" OnClick="btnupdate_Click" /></td>
<td align="left">
    &nbsp;<asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" Width="95px" /></td>
</tr>
</table>
</asp:Panel>
</center>
</div>

<script type="text/javascript">
function CheckIsNumeric(tx)
{
var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode < 46) || (AsciiCode > 57))
{
alert('Please enter only numbers.');
event.cancelBubble = true;
event.returnValue = false;
}

var num=tx.value;
var len=num.length;
var indx=-1;
indx=num.indexOf('.');
if (indx != -1)
{
var dgt=num.substr(indx,len);
var count= dgt.length;
//alert (count);
if (count > 5)  
{
 alert("Only 5 decimal digits allowed");
 event.cancelBubble = true;
 event.returnValue = false;
}
}

}



    </script>
</asp:Content>

