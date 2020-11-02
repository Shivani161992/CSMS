<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="mpscsc_transporter.aspx.cs" Inherits="mpscsc_transporter" Title="Transporter Master " %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="trans">
<div>
<center >
<table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse; border-right: green 3px double; border-top: green 3px double; border-left: green 3px double; border-bottom: green 3px double; width: 484px;" id="">
        <tr>
                <td colspan="3" style="height: 24px; color: white; background-color: transparent; background-image: url(Images/imgg2.jpg);" align="center">
                    Transporter Master</td>
            </tr>
        </table>
<table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: green 3px double; border-top: green 3px double; border-left: green 3px double; border-bottom: green 3px double;"  width="300">
<tr>
<td> 
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" Width="479px">
        <Columns>
            <asp:CommandField HeaderText="Action" ShowSelectButton="True" />
            <asp:BoundField DataField="Transporter_ID" HeaderText="Transporter ID" ReadOnly="True"
                SortExpression="Transporter_ID" />
            <asp:BoundField DataField="Transporter_Name" HeaderText="Transporter Name" ReadOnly="True"
                SortExpression="Transporter_Name" />
            <asp:BoundField DataField="MobileNo" HeaderText="Mobile Number" SortExpression="MobileNo" />
        </Columns>
        <FooterStyle BackColor="Tan" />
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
        <HeaderStyle BackColor="Tan" Font-Bold="True" />
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
    </asp:GridView>
</td>

</tr>
</table>
</center>
</div>
<div>
<table>
<tr>
<td><asp:Button ID="btnaddnew" runat="server" Text="AddNew" OnClick="btnaddnew_Click1" Visible="False" /> </td>
</tr>

</table>
</div>
<div id = "detail1" class ="detail1">

<center >
<table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: green 3px double; border-top: green 3px double; border-left: green 3px double; border-bottom: green 3px double;" id="Table1" width="300" >
    <tr>
        <td colspan="2">
            <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label></td>
    </tr>
<tr>
<td align="left"><asp:Label ID="lbltname" runat="server" Text="Transporter Name"></asp:Label></td>
<td align="left">
    <asp:TextBox ID="txttname" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txttname"
        ErrorMessage="Transporter Name Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
</tr>
<tr>
<td><asp:Label ID="lbltadd" runat="server" Text="Transporter Address" Width="139px"></asp:Label></td>
<td>
    <asp:TextBox ID="txttadd" runat="server"></asp:TextBox></td>
</tr>
    <tr>
        <td>
            Mobile Number &nbsp; &nbsp; &nbsp; &nbsp;
        </td>
        <td>
            <asp:TextBox ID="txtmobile" runat="server" MaxLength="12"></asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ValidationGroup="1" Width="263px" />
        </td>
    </tr>
<tr>
<td>
    <asp:Button ID="btnadd" runat="server" Text="Add" OnClick="btnadd_Click" Width="64px" ValidationGroup="1" /><asp:Button ID="btnclose" runat="server" Text="Close" Width="53px" /></td>
<td align="left">
    &nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Update" /></td>
</tr>
</table>
</center>
</div>
</div>
<script type ="text/javascript" language ="javascript"  >
function CheckIsNumeric(tx)
{
var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode < 46) || (AsciiCode > 57))
{
alert('Please Enter Only Numeric Value');
event.cancelBubble = true;
event.returnValue = false;
}


}
</script>
</asp:Content>

