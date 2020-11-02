<%@ Page Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="SectorMaster.aspx.cs" Inherits="State_SectorMaster" Title="Sector Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<center>
<table style="width: 456px; height: 346px">
<tr>
<td colspan="2" style="height: 22px; background-color: #CCCCFF;"><center>
<asp:Label ID = "lbl1" runat = "server" Text = "Sector Master" 
        style="color: #800000; font-weight: 700" ></asp:Label></center>
</td>
</tr>
<tr>
<td style="width: 177px; height: 26px;">
    Transportation Type</td>
<td style="height: 26px">
            <asp:DropDownList ID="ddltranstype" runat="server" Width="153px" 
                AutoPostBack="True" 
               >
                <asp:ListItem Value="RD">By Road</asp:ListItem>
                <asp:ListItem Value="RK">By Rail</asp:ListItem>
            </asp:DropDownList>
</td>
</tr>
<tr>
<td style="width: 177px; height: 26px;">
<asp:Label ID = "Lbl2" runat = "server" Text = "Select District Name" ></asp:Label>
</td>
<td style="height: 26px">
<asp:DropDownList ID = "ddldistrict" runat = "server" Height="25px" Width="150px" 
        AutoPostBack="True" onselectedindexchanged="ddldistrict_SelectedIndexChanged"></asp:DropDownList>
</td>
</tr>
<tr>
<td style="width: 177px; height: 26px;">
<asp:Label ID = "lbl3" runat = "server" Text = "Select Issue Center" ></asp:Label>
</td>
<td style="height: 26px">
<asp:DropDownList ID = "ddlissuecenter" runat = "server" Height="26px" 
        Width="175px"></asp:DropDownList>
</td>
</tr>
<tr>
<td style="width: 177px; height: 26px;">
<asp:Label ID = "lbl4" runat = "server" Text = "Enter Sector Name" ></asp:Label>
</td>
<td style="height: 26px">
<asp:TextBox ID = "txtsector" runat = "server" Width="215px" Height="24px" ></asp:TextBox>
</td>
</tr>
<tr>
<td colspan="2" style="height: 2px">

</td>
</tr>

<tr>
<td style="width: 177px; height: 26px;">
<asp:Button ID = "btnnew" runat = "server" Text = "New" Width="86px" 
        onclick="btnnew_Click" />
</td>
<td style="height: 26px">
<asp:Button ID = "btnsave" runat = "server" Text = "Save This" 
        onclick="btnsave_Click" Width="100px" />
</td>
</tr>

<tr>
<td style="height: 100px;" colspan="2">
    </td>
</tr>

</table>


</center>


</asp:Content>

