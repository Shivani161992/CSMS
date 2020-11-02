<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="RO_Edit_Page.aspx.cs" Inherits="District_RO_Edit_Page" Title="Edit Release Order Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   
    <div>
<center >
<table style="border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid">
    <tr>
        <td style="font-weight: bold; background-color: #cccccc">
            Edit Release Order 
        </td>
    </tr>
    <tr>
        <td>
    <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#C00000" Visible="False"></asp:Label></td>
    </tr>
<tr>
<td>
<asp:Label ID="lblmsg" runat="server" Font-Italic="True" ForeColor="#C00000" Width="370px"></asp:Label>
</td>
</tr>
<tr>
 <td>
     <asp:GridView ID="dgridchallan" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="dgridchallan_SelectedIndexChanged" OnRowDataBound="dgridchallan_RowDataBound"  AllowPaging="True" PagerSettings-Visible  ="True" OnPageIndexChanging="dgridchallan_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None" Width="599px"  >
        <HeaderStyle  CssClass="gridheader" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"   />
        <Columns>
            <asp:CommandField HeaderText="Update RO" SelectText="Click To Edit" ShowSelectButton="True" >
                <ItemStyle Font-Size="11px" />
            </asp:CommandField>
                       
            <asp:BoundField DataField="RO_No" HeaderText="Ro No." ReadOnly="True" SortExpression="RO_No" >
                <ItemStyle Font-Size="11px" />
            </asp:BoundField>
            
            <asp:TemplateField HeaderText="RO  Date">
     
                <ItemTemplate>
                <asp:Label ID="lblChallan" runat="server" 
                Text='<%# Eval("RO_date").ToString()%>'>
                </asp:Label>
                </ItemTemplate>
               <HeaderStyle CssClass="gridtotsize" />
                <ItemStyle Font-Size="11px" />
                 </asp:TemplateField>
            
             <asp:TemplateField HeaderText="Commodity">
     
                <ItemTemplate>
                <asp:Label ID="lblcomdty" runat="server" 
                Text='<%# Eval("Commodity").ToString()%>'>
                </asp:Label>
                </ItemTemplate>
               <HeaderStyle CssClass="gridtotsize" />
                 <ItemStyle Font-Size="11px" />
                 </asp:TemplateField>
            <asp:BoundField DataField="Scheme_Name" HeaderText="Scheme" SortExpression="Scheme_Name">
                <ItemStyle Font-Size="11px" />
            </asp:BoundField>
            <asp:BoundField DataField="RO_qty" HeaderText="Quantity" ReadOnly="True"
                SortExpression="RO_qty" >
                <ItemStyle Font-Size="11px" />
            </asp:BoundField>
            <asp:BoundField DataField="district_name" HeaderText="For District" SortExpression="district_name">
                <ItemStyle Font-Size="11px" />
            </asp:BoundField>
            <asp:BoundField DataField="RO_district" HeaderText="RD" SortExpression="RO_district">
                <HeaderStyle Font-Size="1px" />
                <ItemStyle Font-Size="1px" />
            </asp:BoundField>
        </Columns>
         <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
         <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="True" />
         <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
         <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
         <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
         <EditRowStyle BackColor="#999999" />
    </asp:GridView>
</td>

</tr>
</table>

</center>

        <table border ="0" cellpadding ="0" cellspacing ="0" class="gridfooter">
        <tr>
                        <td style="width: 117px">
                            <div >
                                <asp:LinkButton ID="Firstbutton" Text='<img border="0" src="../images/firstpage.gif" align="absmiddle">'
                                    CommandArgument="0" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" OnClick="Firstbutton_Click1"  />&nbsp;
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
   </asp:Content>

