<%@ Page Language="C#" MasterPageFile="~/MasterPage/AdminLogin.master" AutoEventWireup="true" CodeFile="Expire_RO_Details.aspx.cs" Inherits="Admin_Expire_RO_Details" Title="Details of Expried Release Order" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="hedl"> Details of Expried Release Order</div>
    
    <div id="rate">
    <table> 
    <tr>
    <td >
        District Name</td>
    <td >
       <asp:DropDownList ID="ddldistrict" runat="server" Width="150px">
        </asp:DropDownList></td>
 <td class ="tdmarginro" style="width: 50px" >
     &nbsp;</td>
 <td></td>
    </tr>
    </table> 
  </div>
    <table style="width: 625px" >
        <tr>
            <td>
                <asp:Button ID="btnGet" runat="server" Text="Get Data" Width="143px" OnClick="btnGet_Click" />
                <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" Width="137px" /></td>
        </tr>
    <tr>
    <td>
        <asp:Label ID="lblmsg" runat="server" Font-Italic="True" ForeColor="#C00000" Width="304px"></asp:Label></td>
    </tr>
    <tr>
    <td><asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound"  AllowPaging="True" PagerSettings-Visible  ="True" OnPageIndexChanging="GridView1_PageIndexChanging" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" Width="620px"  >
         
        <Columns>
            <asp:BoundField DataField="Distt_Id" HeaderText="Did" SortExpression="Distt_Id">
                <ItemStyle CssClass="griditem" />
                <HeaderStyle CssClass="gridtotsize" />
            </asp:BoundField>
            <asp:BoundField DataField="District_Name" HeaderText="District Name" ReadOnly="True"
                SortExpression="District_Name" >
                <ItemStyle CssClass="griditem" />
                <HeaderStyle CssClass="gridtotsize" />
            </asp:BoundField>
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
        <FooterStyle BackColor="Tan" />
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
    
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

