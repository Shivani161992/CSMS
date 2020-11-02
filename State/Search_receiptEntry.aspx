<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="Search_receiptEntry.aspx.cs" Inherits="State_Search_receiptEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <center >    
    <table> 
        <tr>
            <td colspan="2" style="background-color: #cccccc">
    <asp:Label ID="Label2" runat="server" Text="Search Receipt Details(Procurement)" 
                    ForeColor="Black" Font-Bold="True"></asp:Label></td>
        </tr>
        <tr>
            <td align="center" 
                style="background-color: #cfdcc8; font-size: 10pt; position: static;">
                District</td>
            <td align="center" 
                
                style="background-color: #cfdcc8; font-size: 10pt; position: static; text-align: left;">
                <asp:DropDownList ID="ddl_district" runat="server" Width="180px" 
                    onselectedindexchanged="ddl_district_SelectedIndexChanged" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="center" 
                style="background-color: #cfdcc8; font-size: 10pt; position: static;">
                Purchase Centre</td>
            <td align="center" 
                
                style="background-color: #cfdcc8; font-size: 10pt; position: static; text-align: left;">
                <asp:DropDownList ID="ddl_purchasecentre" runat="server" Width="250px" 
                   >
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="center" 
                style="background-color: #cfdcc8; font-size: 10pt; position: static;">
                Tc No.</td>
            <td align="center" 
                
                style="background-color: #cfdcc8; font-size: 10pt; position: static; text-align: left;">
                <asp:TextBox ID="txt_tcno" runat="server" Width="180px" 
                  ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" 
                style="background-color: #cfdcc8; font-size: 10pt; position: static;">
                Receipt Date</td>
            <td align="center" 
                
                style="background-color: #cfdcc8; font-size: 10pt; position: static; text-align: left;">
                <asp:TextBox ID="txtDate" runat="server" Width="180px"></asp:TextBox>
                 <script type  ="text/javascript">
                     new tcal({
                         'formname': 'aspnetForm',
                         'controlname': 'ctl00_ContentPlaceHolder1_txtDate'
                     });
                     function TABLE1_onclick() {

                     }

	          </script>
            </td>
        </tr>
        <tr>
            <td align="center" 
                style="background-color: #cfdcc8; font-size: 10pt; position: static;">
                &nbsp;</td>
            <td align="center" 
                style="background-color: #cfdcc8; font-size: 10pt; position: static;">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center" colspan="2" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
                <asp:Button ID="btn_search" runat="server" onclick="btn_search_Click" 
                    Text="Search" />
            </td>
        </tr>
            <tr>
            <td  colspan="2" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Red"
                    Text="Label" Visible="False"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Width="657px" 
                    Height="368px" BorderColor="Black">
                
            <asp:GridView ID="dgridchallan" runat="server" AutoGenerateColumns="False" 
                        
                        OnRowDataBound="dgridchallan_RowDataBound" PageSize="15" 
                        PagerSettings-Visible ="true" ShowFooter = "True" CellPadding="4" 
                        ForeColor="#333333" GridLines="None" 
                        OnPageIndexChanging="dgridchallan_PageIndexChanging" Width="687px" 
                        OnPageIndexChanged="dgridchallan_PageIndexChanged" 
                        EnableModelValidation="True" BorderColor="Black" CellSpacing="1" 
                        style="font-size: small"  >
        <HeaderStyle  CssClass="gridheader" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"   />
        <Columns>
                       
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
            <asp:BoundField DataField="DepotName" HeaderText="Received IssueCentre" >
            </asp:BoundField>
            <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
            <asp:BoundField DataField="Receipt_Id" HeaderText="Recd.ID" SortExpression="Receipt_Id">
                <HeaderStyle CssClass="gridlarohead" />
                <ItemStyle CssClass="griditemlaro" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Acceptance Date">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("accsept") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" 
                       Text='<%# string.Format("{0}",string.IsNullOrEmpty(Eval("Acceptance_No").ToString())?"Not Created":Eval("Acceptance_Date", "{0:dd/MM/yyyy}")) %>'  ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Acceptance No.">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Font-Size="Small" 
                        Text='<%# string.Format("{0}",string.IsNullOrEmpty(Eval("Acceptance_No").ToString())?"Not Created":Eval("Acceptance_No")) %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Acceptence_No") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
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

