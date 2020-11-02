<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Expire_RO_Details.aspx.cs" Inherits="District_Expire_RO_Details" Title="Details of Expried Release Order" %>
 
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="hedl"> &nbsp;</div>
    
    <div id="rate">
    <table> 
    <tr>
    <td >
        </td>
    <td >
       <asp:DropDownList ID="ddldistrict" runat="server" Width="150px" Visible="False">
        </asp:DropDownList></td>
 <td class ="tdmarginro" style="width: 50px" >
     &nbsp;</td>
 <td></td>
    </tr>
    </table> 
  </div>
    <table style="width: 601px; border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid;" >
        <tr>
            <td style="color: black; background-color: #cccccc">
                Details of Expired Release Order</td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnGet" runat="server" Text="Get Data" Width="143px" OnClick="btnGet_Click" Visible="False" />
                <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" Width="137px" /></td>
        </tr>
    <tr>
    <td>
        <asp:Label ID="lblmsg" runat="server" Font-Italic="True" ForeColor="#C00000" Width="291px"></asp:Label></td>
    </tr>
    <tr>
    <td><asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound"  AllowPaging="True" PagerSettings-Visible  ="True" OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None" Width="606px"  >
         
        <Columns>
            <asp:BoundField DataField="RO_No" HeaderText="RO No." SortExpression="RO_No" >
                <ItemStyle CssClass="griditem" />
                <HeaderStyle CssClass="gridtotsize" />
            </asp:BoundField>
          <asp:TemplateField HeaderText="RO Date">
     
                <ItemTemplate>
                <asp:Label ID="lblChallan" runat="server" 
                Text='<%# Eval("RO_date").ToString()%>'>
                </asp:Label>
                </ItemTemplate>
               <HeaderStyle CssClass="gridtotsize" />
              <ItemStyle CssClass="griditem" />
                 </asp:TemplateField>
            <asp:TemplateField HeaderText="RO Validity">
     
                <ItemTemplate>
                <asp:Label ID="lbrovalidity" runat="server" 
                Text='<%# Eval("RO_Validity").ToString()%>'>
                </asp:Label>
                </ItemTemplate>
               <HeaderStyle CssClass="gridtotsize" />
              <ItemStyle CssClass="griditem" />
                 </asp:TemplateField>
            <asp:BoundField DataField="RO_qty" HeaderText="RO Qty" SortExpression="RO_qty">
                <ItemStyle CssClass="griditem" />
                <HeaderStyle CssClass="gridtotsize" />
            </asp:BoundField>
            <asp:BoundField DataField="Balance_Qty" HeaderText="Balance Qty." SortExpression="Balance_Qty" >
                <ItemStyle CssClass="griditem" />
                <HeaderStyle CssClass="gridtotsize" />
            </asp:BoundField>
            <asp:CommandField HeaderText="Action" SelectText="Update" ShowSelectButton="True" />
        </Columns>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="True" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#999999" />
    
    </asp:GridView></td>
    </tr>
    </table>
    
    
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
    
</asp:Content>

