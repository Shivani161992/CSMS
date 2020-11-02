<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Partyname_master.aspx.cs" Inherits="District_Partyname_master" Title="Untitled Page" %>
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
    }
    </script>

<script type="text/javascript">
function ConfirmationBox()
 {

var result = confirm('क्या आप सम्बंधित जानकारी निरस्त करना चाहते है?' );
if (result)
 {

return true;
}
else 
{
return false;
}
}
</script>

  <center>
    <table style="width: 693px; height: 380px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
        <tr>
            <td colspan="2" style="height: 1px; background-color: #ffff99">
                <asp:Label ID="Label1" runat="server" Text="कृपया ओपन सेल के पार्टी का नाम प्रविष्ट कर उनका सेल का प्रकार चुने एवं मिलर के नाम मिलर मास्टर से भरें" Font-Bold="True" ForeColor="Blue" Height="20px" Width="689px" Font-Size="Medium"></asp:Label>
                </td>
        </tr>
        <tr>
            <td style="width: 155px; height: 1px; text-align: center; border-bottom: black thin groove;">
                <asp:Label ID="Label2" runat="server" Text="Party Name" Width="111px"></asp:Label></td>
            <td style="width: 155px; height: 1px; text-align: left; border-bottom: black thin groove;">
                <asp:TextBox ID="txtname" runat="server" Width="209px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 155px; height: 1px; text-align: center; border-bottom: black thin groove;">
                <asp:Label ID="lbladd" runat="server" Text="Address" Width="111px"></asp:Label></td>
            <td style="width: 155px; height: 1px; text-align: left; border-bottom: black thin groove;">
                <asp:TextBox ID="txtaddress" runat="server" Width="209px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 155px; height: 1px; text-align: center; border-bottom: black thin groove;">
                <asp:Label ID="lblmob" runat="server" Text="Mobile" Width="111px"></asp:Label></td>
            <td style="width: 155px; height: 1px; text-align: left; border-bottom: black thin groove;">
                <asp:TextBox ID="txtmobile" runat="server" Width="209px" MaxLength="10"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 155px; text-align: center; border-bottom: black thin groove;">
                <asp:Label ID="LblType" runat="server" Text="Sale Type" Width="114px"></asp:Label></td>
            <td style="width: 155px; text-align: left; border-bottom: black thin groove;">
                <asp:DropDownList ID="ddltype" runat="server" Width="218px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <span style="color: #ff6666">कृपया मिलर के नाम मिलर मास्टर से जोड़े एवं अन्य पार्टी के
                    नाम यहाँ से जोड़े |</span></td>
        </tr>
        <tr>
            <td style="background-color: #cccc99;" colspan="2">
            </td>
        </tr>
        <tr>
            <td style="width: 155px; text-align: center; border-bottom: black thin groove;">
                <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" /></td>
            <td style="width: 155px; text-align: center; border-bottom: black thin groove;">
                <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" /></td>
        </tr>
        <tr>
            <td colspan="2">
                  <asp:GridView ID="gvDetails" DataKeyNames="PartyId,SaleTypeID" runat="server" 
        AutoGenerateColumns="False" HeaderStyle-BackColor="#61A6F8" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" 
        onrowcancelingedit="gvDetails_RowCancelingEdit" 
        onrowdeleting="gvDetails_RowDeleting" onrowediting="gvDetails_RowEditing" 
        onrowupdating="gvDetails_RowUpdating" OnRowDataBound = "gvDetails_RowDataBound" Width="682px">
     
<Columns>
<asp:TemplateField>
<EditItemTemplate>
    <asp:LinkButton ID="LinkButton1" CommandName ="Update"  runat="server" ToolTip="Update" Height="20px"  >Update</asp:LinkButton>

<asp:LinkButton ID="LinkButton2" CommandName ="Cancel"  runat="server" ToolTip="Cancel" Height="20px"  >Cancel</asp:LinkButton>


</EditItemTemplate>
<ItemTemplate>

<asp:LinkButton ID="LinkButton2" CommandName ="Edit"  runat="server" ToolTip="Cancel" Height="20px"  >Edit</asp:LinkButton>

<asp:LinkButton ID="LinkButton3" CommandName ="Delete"  runat="server" ToolTip="Cancel" Height="20px"  >Delete</asp:LinkButton>


</ItemTemplate>
 </asp:TemplateField>
 
 
<asp:TemplateField HeaderText="Party Name">
 <EditItemTemplate>
 <asp:TextBox ID="txtName" runat="server" Text='<%#Eval("PartyName") %>' />
 </EditItemTemplate>
 <ItemTemplate>
 <asp:Label ID="lblName" runat="server" Text='<%#Eval("PartyName") %>' />
 </ItemTemplate>
 </asp:TemplateField>
 
 
 
 
 <asp:TemplateField HeaderText="Mobile">
 <EditItemTemplate>
 <asp:TextBox ID="txtMobile" runat="server" Text='<%#Eval("Mobile") %>' />
 </EditItemTemplate>
 <ItemTemplate>
 <asp:Label ID="lblMobile" runat="server" Text='<%#Eval("Mobile") %>' />
 </ItemTemplate>
 </asp:TemplateField>
 
 <asp:TemplateField HeaderText="Address">
 <EditItemTemplate>
 <asp:TextBox ID="txtadd" runat="server" Text='<%#Eval("Address") %>' />
 </EditItemTemplate>
 <ItemTemplate>
 <asp:Label ID="lbladd" runat="server" Text='<%#Eval("Address") %>' />
 </ItemTemplate>
 </asp:TemplateField>
 
 <asp:TemplateField HeaderText="Sale Type">
<EditItemTemplate>
<asp:Label ID="lbleditsale" runat="server" Text='<%#Eval("SaleType") %>'/>
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="lblitemsale" runat="server" Text='<%#Eval("SaleType") %>'/>
</ItemTemplate>
</asp:TemplateField>

 </Columns>
    <HeaderStyle BackColor="#61A6F8" Font-Bold="True" ForeColor="White" />
</asp:GridView>
            </td>
        </tr>
    </table>
    </center>
</asp:Content>

