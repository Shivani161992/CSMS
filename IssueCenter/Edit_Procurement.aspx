<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Edit_Procurement.aspx.cs" Inherits="IssueCenter_Edit_Procurement" Title="Edit Procurement Page " %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<center >    
    <table> 
        <tr>
            <td colspan="2" style="background-color: #cccccc">
    <asp:Label ID="Label2" runat="server" Text="Edit Receipt Details(Procurement)" ForeColor="Black" Font-Bold="True"></asp:Label></td>
        </tr>
        <tr>
            <td align="center" colspan="2" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
                Source of Arrival
    <asp:DropDownList ID="ddlsarrival" runat="server" Width="160px" OnSelectedIndexChanged="ddlsarrival_SelectedIndexChanged" AutoPostBack="True">
     
     </asp:DropDownList></td>
        </tr>
            <tr>
            <td  colspan="2" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Red"
                    Text="Label" Visible="False"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Width="633px" Height="550px">
                
            <asp:GridView ID="dgridchallan" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="dgridchallan_SelectedIndexChanged" OnRowDataBound="dgridchallan_RowDataBound" PageSize="15" PagerSettings-Visible ="true" ShowFooter = "True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="dgridchallan_PageIndexChanging" Width="633px" OnPageIndexChanged="dgridchallan_PageIndexChanged" Height="362px"  >
        <HeaderStyle  CssClass="gridheader" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"   />
        <Columns>
            <asp:CommandField HeaderText="Action" ShowSelectButton="True" >
                <ItemStyle CssClass="griditemlaro" ForeColor="#400040" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:CommandField>
                       
            <asp:BoundField DataField="TC_Number" HeaderText="TC No." ReadOnly="True" SortExpression="TC_Number" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Truck_Number" HeaderText="Truck No." ReadOnly="True"
                SortExpression="Truck_Number" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            
            <asp:TemplateField HeaderText="Receipt Date">
     
                <ItemTemplate>
                <asp:Label ID="lblChallan" runat="server" 
                Text='<%# Eval("Recd_Date").ToString()%>'>
                </asp:Label>
                </ItemTemplate>
               <HeaderStyle CssClass="gridlarohead" />
                <ItemStyle CssClass="griditemlaro" />
                 </asp:TemplateField>
            
            <asp:BoundField DataField="Recd_Bags" HeaderText="Bags" ReadOnly="True" SortExpression="Recd_Bags" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Recd_Qty" HeaderText="Qty" ReadOnly="True"
                SortExpression="Recd_Qty" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" ReadOnly="True" SortExpression="Commodity_Name" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="PurchaseCenterName" HeaderText="Purchase Center" SortExpression="PurchaseCenterName">
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Status_Deposit" HeaderText="Status of Deposit" ReadOnly="True" SortExpression="Status_Deposit" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Receipt_Id" HeaderText="Recd.ID" SortExpression="Receipt_Id">
                <HeaderStyle CssClass="gridlarohead" />
                <ItemStyle CssClass="griditemlaro" />
            </asp:BoundField>
        </Columns>
             <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
             <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="True" />
             <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
             <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <EditRowStyle BackColor="#999999" />
    </asp:GridView>
    </asp:Panel>
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
    
         </center>
</asp:Content>

