<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="Del_AcceptanceNote.aspx.cs" Inherits="State_Del_AcceptanceNote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <center >    
    <table style="width: 710px"> 
        <tr>
            <td colspan="2" style="background-color: #cccccc">
    <asp:Label ID="Label2" runat="server" Text="Delete Receipt Details(Procurement)" 
                    ForeColor="Black" Font-Bold="True"></asp:Label></td>
        </tr>
        <tr>
            <td align="center" style="font-size: 10pt; position: static; background-color: #cfdcc8">
                <asp:Label ID="Label3" runat="server" Text="Select Commodity" Width="173px"></asp:Label></td>
            <td align="center" style="font-size: 10pt; width: 528px; position: static; background-color: #cfdcc8;
                text-align: left">
                <asp:DropDownList ID="ddlcommodtiy" runat="server" AutoPostBack="True" Width="221px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td align="center" 
                style="background-color: #cfdcc8; font-size: 10pt; position: static;">
                District</td>
            <td align="center" 
                
                
                style="background-color: #cfdcc8; font-size: 10pt; position: static; text-align: left; width: 528px;">
                <asp:DropDownList ID="ddl_district" runat="server" Width="180px" 
                    onselectedindexchanged="ddl_district_SelectedIndexChanged" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="center" 
                style="background-color: #cfdcc8; font-size: 10pt; position: static;">
                Issue Centre</td>
            <td align="center" 
                
                
                style="background-color: #cfdcc8; font-size: 10pt; position: static; text-align: left; width: 528px;">
                <asp:DropDownList ID="ddl_IssueCentre" runat="server" Width="180px" 
                    onselectedindexchanged="ddl_IssueCentre_SelectedIndexChanged" AutoPostBack="True" 
                   >
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="center" 
                style="background-color: #cfdcc8; font-size: 10pt; position: static;">
                Acceptance No.</td>
            <td align="center" 
                
                
                style="background-color: #cfdcc8; font-size: 10pt; position: static; text-align: left; width: 528px;">
                <asp:DropDownList ID="ddlAccptNumber" runat="server"  Width="254px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="center" 
                style="background-color: #cfdcc8; font-size: 10pt; position: static;" 
                colspan="2">
                <asp:Button ID="btn_search" runat="server" onclick="btn_search_Click" 
                    Text="View" />
            </td>
        </tr>
            <tr>
            <td  colspan="2" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Red"
                    Text="Label" Visible="False"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Width="716px" 
                    BorderColor="Black" Visible="False">
                
                    <table style="width: 100%" border="1">
                        <tr>
                            <td style="font-size: small">
                                Acceptance Date:</td>
                            <td>
                                <asp:Label ID="lbl_Accp_Date" runat="server" style="font-size: small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: small">
                                Purchase Centre:</td>
                            <td>
                                <asp:Label ID="lbl_Purchase" runat="server" style="font-size: small"></asp:Label>
                            </td>
                        </tr>
                    </table>
                
            <asp:GridView ID="dgridchallan" runat="server" AutoGenerateColumns="False" 
                        
                        OnRowDataBound="dgridchallan_RowDataBound" PageSize="15" 
                        PagerSettings-Visible ="true" ShowFooter = "True" CellPadding="4" 
                        ForeColor="#333333" GridLines="None" 
                        OnPageIndexChanging="dgridchallan_PageIndexChanging" Width="687px" 
                        OnPageIndexChanged="dgridchallan_PageIndexChanged" 
                        EnableModelValidation="True" BorderColor="Black" CellSpacing="1" 
                        style="font-size: small" 
                        DataKeyNames="Commodity_Id,Recd_Godown"  >
        <HeaderStyle  CssClass="gridheader" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"   />
        <Columns>
                       
            <asp:TemplateField HeaderText="Delete AN">
                <ItemTemplate>
                    <asp:CheckBox ID="chk_del" runat="server" 
                        oncheckedchanged="chk_del_CheckedChanged" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete Receipt">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chk_receipt" runat="server"  />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="TC_Number" HeaderText="TC No." ReadOnly="True" 
                SortExpression="TC_Number">
            <ItemStyle CssClass="griditemlaro" />
            <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="PurchaseCenterName" HeaderText="Purchase Centre" 
                Visible="False" />
            <asp:BoundField DataField="Truck_Number" HeaderText="Truck No." ReadOnly="True" 
                SortExpression="Truck_Number">
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
            <asp:BoundField DataField="Recd_Bags" HeaderText="Received Bags" 
                ReadOnly="True" SortExpression="Recd_Bags">
            <ItemStyle CssClass="griditemlaro" />
            <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Recd_Qty" HeaderText="Received Qty" ReadOnly="True" 
                SortExpression="Recd_Qty">
            <ItemStyle CssClass="griditemlaro" />
            <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" 
                ReadOnly="True" SortExpression="Commodity_Name">
            <ItemStyle CssClass="griditemlaro" />
            <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="DepotName" HeaderText="Received IssueCentre" />
            <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
            <asp:BoundField DataField="Receipt_Id" HeaderText="Recd.ID" SortExpression="Receipt_Id">
                <HeaderStyle CssClass="gridlarohead" />
                <ItemStyle CssClass="griditemlaro" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Commodity_" Visible="False">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Commodity_Id") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Commodity_Id") %>'></asp:Label>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Recd_Godown" HeaderText="Godown_id" ReadOnly="True" 
                Visible="False" />
            <asp:TemplateField HeaderText="Acceptance Date" Visible="False">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("accsept") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" 
                       Text='<%# string.Format("{0}",string.IsNullOrEmpty(Eval("Acceptance_No").ToString())?"Not Created":Eval("Acceptance_Date", "{0:dd/MM/yyyy}")) %>'  ></asp:Label>
                </ItemTemplate>
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
        <tr><td colspan="2" style="text-align: center">
        
            <asp:Button ID="bn_del" runat="server" onclick="bn_del_Click" Text="Delete" />
        </td></tr>
        <tr>
            <td colspan="2">
                <asp:HiddenField ID="hd_qty" runat="server" />
                <asp:HiddenField ID="hd_godown" runat="server" />
                <asp:HiddenField ID="hd_bag" runat="server" />
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
                             
                                <asp:Label ID="lbl_message" runat="server" Visible="False"></asp:Label>
                             
                                <asp:HiddenField ID="hd_commodity" runat="server" 
                                   />
                             
                            </div>
                        </td>
                    </tr>
        </table>
    
         </center>
</asp:Content>

