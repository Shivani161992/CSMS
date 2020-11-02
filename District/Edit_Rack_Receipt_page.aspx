<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Edit_Rack_Receipt_page.aspx.cs" Inherits="Edit_Rack_Receipt_page" Title="Edit Rack Receipt Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="hedl"> 
    <asp:Label ID="lblreceipt" runat="server" Text="Edit Rack Receipt Details"></asp:Label>&nbsp;</div>
     <center >
         &nbsp;</center>
 <div id="rate"> 
    <table> 
        <tr>
            <td colspan="2">
                <asp:Label ID="lbldisp" runat="server" Visible="False" Font-Italic="True" ForeColor="#C00000" Width="312px"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="2">
            <asp:GridView ID="dgridchallan" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="dgridchallan_SelectedIndexChanged" OnRowDataBound="dgridchallan_RowDataBound"
          AllowPaging="True" PageSize="15" PagerSettings-Visible ="true " BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" OnPageIndexChanged="dgridchallan_PageIndexChanged" OnPageIndexChanging="dgridchallan_PageIndexChanging"  >
        <HeaderStyle  CssClass="gridheader" BackColor="Tan" Font-Bold="True"   />
        <Columns>
            <asp:CommandField HeaderText="Update Details" SelectText="Click To Edit" ShowSelectButton="True" >
                <ItemStyle CssClass="griditem" />
            </asp:CommandField>
                       
            <asp:BoundField DataField="Rack_No" HeaderText="Rack No." ReadOnly="True" SortExpression="Rack_No" >
                <ItemStyle CssClass="griditem" />
            </asp:BoundField>
            <asp:BoundField DataField="Challan_No" HeaderText="Challan No." SortExpression="Challan_No" />
            
            <asp:TemplateField HeaderText="Challan Date">
     
                <ItemTemplate>
                <asp:Label ID="lblChallan" runat="server" 
                Text='<%# Eval("challan_date").ToString()%>'>
                </asp:Label>
                </ItemTemplate>
               <HeaderStyle CssClass="gridtotsize" />
                 </asp:TemplateField>
            
            <asp:BoundField DataField="Truck_No" HeaderText="Truck No" ReadOnly="True" SortExpression="Truck_No" >
                <ItemStyle CssClass="griditem" />
            </asp:BoundField>
            <asp:BoundField DataField="DepotName" HeaderText="Issue Center" ReadOnly="True"
                SortExpression="DepotName" >
                <ItemStyle CssClass="griditem" />
            </asp:BoundField>
            <asp:BoundField DataField="RailHead_Name" HeaderText="Recd. RailHead" SortExpression="RailHead_Name" />
            <asp:BoundField DataField="IsReceived" HeaderText="Status" SortExpression="IsReceived" />
        </Columns>
                <FooterStyle BackColor="Tan" />
                <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                <AlternatingRowStyle BackColor="PaleGoldenrod" />
    </asp:GridView>
            </td>
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
      </div> 
        
</asp:Content>

