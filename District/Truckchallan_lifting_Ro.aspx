<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Truckchallan_lifting_Ro.aspx.cs" Inherits="District_Truckchallan_lifting_Ro" Title="Truck Movement Lifting Against Release Order" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="trans">


<div>
    &nbsp;</div>
<div id = "detail1" class ="detail1">

<center >
<table cellpadding ="0" cellspacing ="0" border ="0" >
    <tr>
        <td colspan="2" style="color: olive">
            <strong>Details of Truck Challan (Lifting against FCI&nbsp; RO)</strong></td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnaddnew" runat="server" Text="AddNew" OnClick="btnaddnew_Click1" /></td>
    </tr>
<tr>
<td align="left"><asp:Label ID="lbltname" runat="server" Text="Truck Challan No." Visible="False"></asp:Label></td>
<td align="left">
    <asp:TextBox ID="txtchallanno" runat="server" Visible="False"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtchallanno"
        ErrorMessage="Challan Number Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
</tr>
<tr>
<td align="left"><asp:Label ID="lbltadd" runat="server" Text="Truck No." Visible="False"></asp:Label></td>
<td align="left">
    <asp:TextBox ID="txttruckno" runat="server" Visible="False"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txttruckno"
        ErrorMessage="Truck No Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
</tr>
<tr>
<td>
    <asp:Button ID="btnadd" runat="server" Text="Add" OnClick="btnadd_Click" Width="68px" Visible="False" ValidationGroup="1" /><asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" Visible="False" /></td>
<td align="left">
    &nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" Width="81px" /></td>
</tr>
    <tr>
        <td colspan="2">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ValidationGroup="1" Width="267px" />
        </td>
    </tr>
</table>
</center>
</div>
</div>
 <div >
<table cellpadding ="0" cellspacing ="0" border ="0" class="laromargin">
<tr>
<td>
<asp:Label ID="Label1" runat="server" Visible ="False" ForeColor="#C00000" Width="246px" Font-Italic="True" ></asp:Label>
</td>
</tr>
<tr>
<td> 
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound"
          AllowPaging="True" PageSize="10" PagerSettings-Visible ="false" ShowFooter = "False" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" OnPageIndexChanged="GridView1_PageIndexChanged" OnPageIndexChanging="GridView1_PageIndexChanging"  >
        <HeaderStyle  CssClass="gridheader" BackColor="Tan" Font-Bold="True" />
        <Columns>
            <asp:CommandField HeaderText="Action" ShowSelectButton="True" />
            <asp:TemplateField HeaderText="Challan Date">
     
                <ItemTemplate>
                <asp:Label ID="lblChallan" runat="server" 
                Text='<%# Eval("Challan_Date").ToString()%>'>
                </asp:Label>
                </ItemTemplate>
               <HeaderStyle CssClass="gridtotsize" />
                 </asp:TemplateField>
            <asp:BoundField DataField="Truck_Challan" HeaderText="TC No." ReadOnly="True"
                SortExpression="Truck_Challan" />
            <asp:BoundField DataField="Truck_No" HeaderText="Truck No." ReadOnly="True"
                SortExpression="Truck_No" />
            <asp:BoundField DataField="Transporter" HeaderText="Transporter" ReadOnly="True"
                SortExpression="Transporter" />
            <asp:BoundField DataField="RO_No" HeaderText="RO No." ReadOnly="True" SortExpression="RO_No" />
            <asp:BoundField DataField="No_of_Bags" HeaderText="Bags" ReadOnly="True" SortExpression="No_of_Bags" />
            <asp:BoundField DataField="Qty_send" HeaderText="Qty" ReadOnly="True" SortExpression="Qty_send" />
             <asp:TemplateField HeaderText="Commodity">
     
                <ItemTemplate>
                <asp:Label ID="lblcomdty" runat="server" 
                Text='<%# Eval("Commodity").ToString()%>'>
                </asp:Label>
                </ItemTemplate>
               <HeaderStyle CssClass="gridtotsize" />
                 </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="Tan" />
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
    </asp:GridView>
    <div>
        <table border ="0" cellpadding ="0" cellspacing ="0" class="gridfooter">
        <tr>
                        <td>
                            <div >
                                <asp:LinkButton ID="Firstbutton" Text='<img border="0" src="../images/firstpage.gif" align="absmiddle">'
                                    CommandArgument="0" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false"  />&nbsp;
                                <asp:LinkButton ID="Prevbutton" Text='<img border="0" src="../images/prevpage.gif" align="absmiddle">'
                                    CommandArgument="prev" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" />&nbsp;
                                <asp:LinkButton ID="Nextbutton" Text='<img border="0" src="../images/nextpage.gif" align="absmiddle">'
                                    CommandArgument="next" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" />&nbsp;
                                <asp:LinkButton ID="Lastbutton" Text='<img border="0" src="../images/lastpage.gif" align="absmiddle">'
                                    CommandArgument="last" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" />&nbsp;&nbsp;
                             
                            </div>
                        </td>
                    </tr>
        </table>
        </div>
    
</td>

</tr>
</table>
</div> 

</asp:Content>

