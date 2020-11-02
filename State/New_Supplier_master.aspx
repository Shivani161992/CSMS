<%@ Page Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="New_Supplier_master.aspx.cs" Inherits="State_New_Supplier_master" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="transCP">
<center >
<div id="transCPm"> 
<table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse; border-right: green 3px double; border-top: green 3px double; border-left: green 3px double; border-bottom: green 3px double; width: 328px;" id="">
        <tr>
                <td colspan="3" style="height: 24px; color: white; background-color: transparent; background-image: url(../Images/imgg2.jpg);" align="center">
                    Supplier Master</td>
            </tr>
        </table>
<table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: green 3px double; border-top: green 3px double; border-left: green 3px double; border-bottom: green 3px double; width: 324px;" >
    <tr>
        <td colspan="2" style="height: 21px">
            <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label></td>
    </tr>
    <tr>
        <td align="left">
            <asp:Label ID="lblsupplierName" runat="server" Text="Supplier Name"></asp:Label></td>
        <td align="left">
    <asp:TextBox ID="txtsuppliername" runat="server" Width="173px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtsuppliername"
                ErrorMessage="Supplier Name Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td align="left" style="height: 27px" >
            <asp:Label ID="lblPlace" runat="server" Text="Place"></asp:Label></td>
        <td align="left" style="height: 27px" >
            <asp:TextBox ID="txtplace" runat="server" Width="172px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtplace"
                ErrorMessage="Place Name Required" ValidationGroup="1">*</asp:RequiredFieldValidator>
            </td>
    </tr>
    <tr>
        <td align="left" >
            <asp:Label ID="lblCommodity" runat="server" Text="Commodity"></asp:Label></td>
        <td align="left" >
        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                        RepeatDirection="Horizontal" Width="184px">
                        <asp:ListItem Value="Sugar">Sugar</asp:ListItem>
                        <asp:ListItem Value="Salt">Salt</asp:ListItem>
                    </asp:RadioButtonList></td>
        
    </tr>
     <tr>
        <td align="left" >
            <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label></td>
        <td align="left" >
        <asp:RadioButtonList ID="RadioButtonList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged"
                        RepeatDirection="Horizontal" Width="184px">
                        <asp:ListItem Value="Active">Active</asp:ListItem>
                        <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                    </asp:RadioButtonList></td>
        
    </tr>
    <tr>
        <td align="left">
            &nbsp;<asp:Button ID="btnadd" runat="server" Text="Save" OnClick="btnadd_Click" Width="58px" ValidationGroup="1" />
            </td>
        <td align="left">
            <asp:Button ID="btnclose" runat="server" Text="Close" Width="65px" OnClick="btnclose_Click" /></td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ValidationGroup="1" Width="254px" Height="1px" />
        </td>
    </tr>
</table>
</div> 
<table>
<tr>
<td>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound"
           PageSize ="3"  AllowPaging="True" PagerSettings-Visible  ="True" OnPageIndexChanging="GridView1_PageIndexChanging" BackColor="White" BorderColor="#999999" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="394px" BorderStyle="None"  >
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <Columns>
            
            
            <asp:BoundField DataField="Name" HeaderText="Supplier Name" SortExpression="Name">
                <ItemStyle Font-Size="13px" />
                <HeaderStyle Font-Size="13px" />
            </asp:BoundField>
        <asp:BoundField DataField="Place" HeaderText="Place" SortExpression="Place">
                <ItemStyle Font-Size="13px" />
                <HeaderStyle Font-Size="13px" />
            </asp:BoundField>
         <asp:BoundField DataField="Commodity" HeaderText="Commodity" SortExpression="Commodity">
                <ItemStyle Font-Size="13px" />
                <HeaderStyle Font-Size="13px" />
            </asp:BoundField>
        </Columns>
        <SelectedRowStyle BackColor="#008A8C" ForeColor="White" Font-Bold="True" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="Gainsboro" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
    </asp:GridView>
</td>
</tr>
</table>
</center>
</div> 
</asp:Content>
