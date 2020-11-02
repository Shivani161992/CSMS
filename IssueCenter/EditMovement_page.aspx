<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="EditMovement_page.aspx.cs" Inherits="EditMovement_page" Title="Edit Movement Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div id="rate"> 
    <table style="width: 623px"> 
        <tr>
            <td colspan="2" style="background-color: #cccccc">
    <asp:Label ID="lblreceipt" runat="server" Text="Edit Receipt Details" Font-Bold="True"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="2" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
        <asp:Label ID="lblSorcePfArrival" runat="server" Text="Source of Arrival"></asp:Label>
    <asp:DropDownList ID="ddlsarrival" runat="server" Width="133px" OnSelectedIndexChanged="ddlsarrival_SelectedIndexChanged" AutoPostBack="True">
     
     </asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan="2" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
                <asp:Label ID="lbldisp" runat="server" Visible="False" Font-Italic="True" ForeColor="#C00000" Width="312px"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                <asp:Panel ID="Panel1" runat="server" Height="550px" ScrollBars="Vertical" Width="620px">
              
            <asp:GridView ID="dgridchallan" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="dgridchallan_SelectedIndexChanged" OnRowDataBound="dgridchallan_RowDataBound" PageSize="15" PagerSettings-Visible ="true " CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanged="dgridchallan_PageIndexChanged" OnPageIndexChanging="dgridchallan_PageIndexChanging" Width="614px"  >
        <HeaderStyle  CssClass="gridheader" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"   />
        <Columns>
            <asp:CommandField HeaderText="Update Challan" SelectText="Click To Edit" ShowSelectButton="True" >
                <ItemStyle CssClass="griditemlaro" ForeColor="#400040" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:CommandField>
                       
            <asp:BoundField DataField="challan_no" HeaderText="Challan No." ReadOnly="True" SortExpression="challan_no" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            
            <asp:TemplateField HeaderText="Challan Date">
     
                <ItemTemplate>
                <asp:Label ID="lblChallan" runat="server" 
                Text='<%# Eval("challan_date").ToString()%>'>
                </asp:Label>
                </ItemTemplate>
               <HeaderStyle CssClass="gridlarohead" />
                <ItemStyle CssClass="griditemlaro" />
                 </asp:TemplateField>
            
            <asp:BoundField DataField="Vehile_no" HeaderText="Truck No" ReadOnly="True" SortExpression="Vehile_no" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Receipt_id" HeaderText="Acknowledgement ID" ReadOnly="True"
                SortExpression="Receipt_id" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Source_Name" HeaderText="Source of Arrival" SortExpression="Source_Name" >
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
    
    
     <div>
        <table border ="0" cellpadding ="0" cellspacing ="0" class="gridfooter">
        <tr>
                        <td>
                            <div >
                                <asp:LinkButton ID="Firstbutton" Text='<img border="0" src="../images/firstpage.gif" align="absmiddle">'
                                    CommandArgument="0" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false"  />&nbsp;
                                <asp:LinkButton ID="Prevbutton" Text='<img border="0" src="../images/prevpage.gif" align="absmiddle">'
                                    CommandArgument="prev" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" OnClick="Prevbutton_Click" />&nbsp;
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

