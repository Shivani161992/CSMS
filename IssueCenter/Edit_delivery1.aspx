<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master"  AutoEventWireup="true" CodeFile="Edit_delivery1.aspx.cs" Inherits="Edit_delivery1" Title="Edit Delivery Order" %>
 
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">




<script type="text/javascript">
function CheckIsNumeric(e,tx)
    {         
        var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;                        
        if ((AsciiCode < 46 && AsciiCode != 8) || (AsciiCode > 57 ) || (AsciiCode == 47 ))
        {
            alert('Please enter only numbers.');
            return false;
        }                
        var num=tx.value;        
        var len=num.length;
        var indx=-1;
        indx=num.indexOf('.');
        if (indx != -1)
        {
            if ((AsciiCode == 46 ))
            {
                alert('Point must be apear only one time.');
                return false;
            }
            var dgt=num.substr(indx,len);
            var count= dgt.length;
            //alert (count);
            if (count > 5 && AsciiCode != 8)  
            {
                alert("Only 5 decimal digits allowed.");
                return false;
            }
        }
    }
    </script>
	
    <div>
    <asp:Panel ID="panel_do" runat ="server" >
        <table>
        
        </table>
        <asp:DropDownList ID="DropDownList1" runat="server" Visible="False">
            <asp:ListItem Value="1">Rice APL</asp:ListItem>
            <asp:ListItem Value="2">Rice BPL</asp:ListItem>
            <asp:ListItem Value="3">Rice AAY</asp:ListItem>
            <asp:ListItem Value="4">Wheat APL</asp:ListItem>
            <asp:ListItem Value="5">Wheat BPL</asp:ListItem>
            <asp:ListItem Value="6">Wheat AAY</asp:ListItem>
            <asp:ListItem Value="7">Sugar</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" Visible="False" /></asp:Panel>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <table>
            <tr>
                <td style="width: 100px">
                    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="GetData" /></td>
                <td style="width: 100px">
                    <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Update" /></td>
                <td style="width: 100px">
                    <asp:Button ID="Button7" runat="server" OnClick="Button7_Click" Text="Get FPS" />
                    <asp:Button ID="Button8" runat="server" Text="Update FPS" OnClick="Button8_Click" />
                    <asp:Button ID="Button9" runat="server" OnClick="Button9_Click" Text="Update Status" /></td>
            </tr>
            <tr>
                <td align="left" style="width: 100px">
                    <asp:DropDownList ID="ddlissue" runat="server" AutoPostBack="True">
                    </asp:DropDownList></td>
                <td style="width: 100px">
                    <asp:Button ID="Button10" runat="server" OnClick="Button10_Click" Text="Get Balance IC" /></td>
                <td style="width: 100px">
                    <asp:Button ID="Button11" runat="server" OnClick="Button11_Click" Text="Update Balance IC" /></td>
            </tr>
            <tr>
                <td align="left" colspan="3">
                    <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                        <RowStyle ForeColor="#000066" />
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="GetData Balance" /></td>
                <td style="width: 100px">
                    <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="Update Balance" /></td>
                <td style="width: 100px">
                    <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="new" /></td>
            </tr>
        </table>
    </div>

</asp:Content>

