<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Pending_TruckChallan.aspx.cs" Inherits="IssueCenter_Pending_TruckChallan" Title="Pending Truck Challan for Print" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table style="width: 495px; border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid;"> 
        <tr>
            <td colspan="2" style="background-color: #cccccc">
                Truck Challan for Print</td>
        </tr>
    <tr>
    <td colspan="2">
        <asp:Label ID="Label1" runat="server" Font-Italic="True" ForeColor="#C00000"></asp:Label>
    
     </td>
    </tr>
        <tr>
            <td colspan="2">
            <asp:GridView ID="dgridchallan" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="dgridchallan_SelectedIndexChanged" OnRowDataBound="dgridchallan_RowDataBound"
          AllowPaging="True" PageSize="5" PagerSettings-Visible ="True" ShowFooter = "True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="dgridchallan_PageIndexChanging" Width="499px"  >
        <HeaderStyle  CssClass="gridheader" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"   />
        <Columns>
            <asp:CommandField ShowSelectButton="True" >
                <ItemStyle CssClass="griditem" />
            </asp:CommandField>
                       
            <asp:BoundField DataField="Challan_No" HeaderText="Challan No." ReadOnly="True" SortExpression="Challan_No" >
                <ItemStyle CssClass="griditem" />
            </asp:BoundField>
            
            <asp:TemplateField HeaderText="Challan Date">
     
                <ItemTemplate>
                <asp:Label ID="lblChallan" runat="server" 
                Text='<%# Eval("challan_date").ToString()%>'>
                </asp:Label>
                </ItemTemplate>
               <HeaderStyle CssClass="gridtotsize" />
                 </asp:TemplateField>
            
            <asp:BoundField DataField="Truck_no" HeaderText="Vehile No" ReadOnly="True" SortExpression="Truck_no" >
                <ItemStyle CssClass="griditem" />
            </asp:BoundField>
            <asp:BoundField DataField="Dispatch_id" HeaderText="Dispatch ID" ReadOnly="True"
                SortExpression="Dispatch_id" >
                <ItemStyle CssClass="griditem" />
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

</asp:Content>


