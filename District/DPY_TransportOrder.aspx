<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="DPY_TransportOrder.aspx.cs" Inherits="District_DPY_TransportOrder" Title="Transport Order Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <script type="text/javascript">
function ConfirmationBox() 
{

var result = confirm('एस एम् एस भेजने के बाद डाटा में संसोधन नहीं होगा, क्या आप एस एम् एस भेजना चाहते है ?' );
if (result)
 {
   return true;
 }
else
 {
    return false;
 }
 
}
function TABLE1_onclick() {

}

</script>

  
    <table style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove;
        width: 769px; border-bottom: black thin groove; height: 220px" id="TABLE1" onclick="return TABLE1_onclick()">
        <tr>
            <td colspan="4">
                <span style="color: #0000cc">Door Step Transport Order for Transporter</span></td>
        </tr>
        <tr>
            <td colspan="4">
                <span style="color: #cc0000">
                परिवहन आदेश जारी करने के पूर्व रूट चार्ट की प्रविष्टि कर ले |</span></td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Label ID="msgAllotment" runat="server" Font-Bold="True" ForeColor="Blue"
                    Visible="False"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="4" style="color: #800000">
                <b>डाटा समग्र पोर्टल में उपलब्ध नेट आवंटन के अनुसार रहेगा |</b></td>
        </tr>
        <tr>
            <td style="width: 160px">
            </td>
            <td style="width: 174px">
            </td>
            <td style="width: 160px">
            </td>
            <td style="width: 190px">
            </td>
        </tr>
        <tr>
            <td style="width: 160px">
                <asp:Label ID="Label1" runat="server" Text="Select Issue Center" Width="140px"></asp:Label></td>
            <td colspan="2" style="text-align: left">
                <asp:DropDownList ID="ddlissueCenter" runat="server" Width="200px">
                </asp:DropDownList></td>
            <td style="width: 190px">
            </td>
        </tr>
        <tr>
            <td style="width: 160px">
                <asp:Label ID="Label4" runat="server" Text="Select Transporter Name" Width="182px"></asp:Label></td>
            <td style="width: 174px; text-align: left">
                <asp:DropDownList ID="ddltransporter" runat="server" Width="240px">
                </asp:DropDownList></td>
            <td style="width: 160px">
            </td>
            <td style="width: 190px; text-align: left">
            </td>
        </tr>
        <tr>
            <td style="width: 160px; height: 24px">
                <asp:Label ID="Label2" runat="server" Text="Allotment Month" Width="117px"></asp:Label></td>
            <td style="width: 174px; height: 24px; text-align: left">
                <asp:DropDownList ID="ddlmonth" runat="server" Width="170px">
                    <asp:ListItem Value="1">January</asp:ListItem>
                    <asp:ListItem Value="2">February</asp:ListItem>
                    <asp:ListItem Value="3">March</asp:ListItem>
                    <asp:ListItem Value="4">April</asp:ListItem>
                    <asp:ListItem Value="5">May</asp:ListItem>
                    <asp:ListItem Value="6">June</asp:ListItem>
                    <asp:ListItem Value="7">July</asp:ListItem>
                    <asp:ListItem Value="8">August</asp:ListItem>
                    <asp:ListItem Value="9">September</asp:ListItem>
                    <asp:ListItem Value="10">October</asp:ListItem>
                    <asp:ListItem Value="11">November</asp:ListItem>
                    <asp:ListItem Value="12">December</asp:ListItem>
                </asp:DropDownList></td>
            <td style="width: 160px; height: 24px">
                <asp:Label ID="Label3" runat="server" Text="Allotment Year"></asp:Label></td>
            <td style="width: 190px; height: 24px; text-align: left">
                <asp:DropDownList ID="ddlyear" runat="server" Width="125px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 160px; height: 24px">
                <asp:Label ID="Label5" runat="server" Text="Transport Order Type" Width="169px"></asp:Label></td>
            <td style="width: 174px; height: 24px; text-align: left">
            
                <asp:DropDownList ID="ddlTOType" runat="server" Width="200px">
                
                 <asp:ListItem Value="MTO">Main Transport Order</asp:ListItem>
                 
                <%--  <asp:ListItem Value="STO">Supplimentary Transport Order</asp:ListItem>--%>
                
                </asp:DropDownList></td>
            <td style="height: 24px" colspan="2">
                <asp:Label ID="lblto" runat="server" ForeColor="#C00000"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 160px; height: 24px">
            </td>
            <td>
            </td>
            <td style="width: 190px; height: 24px">
                <asp:Button ID="btnadd" runat="server" OnClick="btnadd_Click" Text="Get details"
                    Width="100px"/></td>
            <td style="width: 190px; height: 24px">
                <span style="color: #990033"><strong>मात्रा क्विंटल में</strong></span></td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Panel ID="Panel2" runat="server" Visible="False">
                    <center>
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="#CCCCCC"
                            BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" 
                            CellPadding="4"  OnRowDataBound="GridView2_RowDataBound" 
                 ShowFooter="True" CellSpacing="2"
                            DataKeyNames="fps_code" Font-Bold="False" Font-Size="Small" 
                            ForeColor="Black" Width="700px">
                            <RowStyle BackColor="White" />
                            <Columns>
                            <asp:BoundField DataField="fps_code" HeaderText="FPS Code" SortExpression="fps_code">
                                    <ItemStyle Font-Bold="False" />
                           </asp:BoundField>
                                
                           <%--<asp:BoundField DataField="fps_Uname" HeaderText="FPS Name" SortExpression="fps_Uname">
                           <ItemStyle Font-Bold="False" />
                           </asp:BoundField>--%>
                           
                            <asp:TemplateField HeaderText="Fps Name" SortExpression="fps_Uname">
               <ItemTemplate>
              <ItemStyle />
                    <HeaderStyle/>
                    
                   <asp:Label ID="lblfps" runat="server" Text='<%# Bind("fps_Uname") %>'> </asp:Label>
                     
                 </ItemTemplate>
                 
                 <FooterTemplate>
                                   
                    <asp:Label ID="lbl_fps" runat="server"  ForeColor="Black" Font-Names="Vani" Text = "Grand Total"></asp:Label>
                    
                    </FooterTemplate>
                      <FooterStyle CssClass="FooterStyle" HorizontalAlign = "Center" />
                 <HeaderStyle Font-Size="12px" />
                </asp:TemplateField>
                           
                           
                       <%-- <asp:BoundField DataField="wheat" HeaderText="Wheat Alloc" SortExpression="wheat">
                           <ItemStyle Font-Bold="False" />
                           </asp:BoundField>--%>
                           
               <asp:TemplateField HeaderText="Wheat Alloc" SortExpression="wheat">
               <ItemTemplate>
              <ItemStyle />
                    <HeaderStyle/>
                    
                   <asp:Label ID="lblwheat" runat="server" Text='<%# Bind("wheat") %>'> </asp:Label>
                     
                 </ItemTemplate>
                 
                 <FooterTemplate>
                                   
                    <asp:Label ID="lbl_wheat" runat="server"  ForeColor="Black" Font-Names="Vani"></asp:Label>
                    
                    </FooterTemplate>
                      <FooterStyle CssClass="FooterStyle" HorizontalAlign = "Center"/>
                 <HeaderStyle Font-Size="12px" />
                </asp:TemplateField>
                           
                                                      
                         <%-- <asp:BoundField DataField="rice" HeaderText="Rice Alloc" SortExpression="rice">
                           <ItemStyle Font-Bold="False" />
                           </asp:BoundField>--%>
                           
              <asp:TemplateField HeaderText="Rice Alloc" SortExpression="rice">
               <ItemTemplate>
              <ItemStyle />
                    <HeaderStyle/>
                    
                   <asp:Label ID="lblrice" runat="server" Text='<%# Bind("rice") %>'> </asp:Label>
                     
                 </ItemTemplate>
                 
                 <FooterTemplate>
                                   
                    <asp:Label ID="lbl_rice" runat="server"  ForeColor="Black" Font-Names="Vani"></asp:Label>
                    
                    </FooterTemplate>
                      <FooterStyle CssClass="FooterStyle" HorizontalAlign = "Center"/>
                 <HeaderStyle Font-Size="12px" />
                </asp:TemplateField>
                           
                           
                          <%-- <asp:BoundField DataField="sugar_alloc" HeaderText="Sugar Alloc" SortExpression="sugar_alloc">
                           <ItemStyle Font-Bold="False" />
                           </asp:BoundField>--%>
                           
               <asp:TemplateField HeaderText="Sugar Alloc" SortExpression="sugar_alloc">
               <ItemTemplate>
              <ItemStyle />
                    <HeaderStyle/>
                    
                   <asp:Label ID="lblsugar" runat="server" Text='<%# Bind("sugar_alloc") %>'> </asp:Label>
                     
                 </ItemTemplate>
                 
                 <FooterTemplate>
                                   
                    <asp:Label ID="lbl_sugar" runat="server"  ForeColor="Black" Font-Names="Vani"></asp:Label>
                    
                    </FooterTemplate>
                      <FooterStyle CssClass="FooterStyle" HorizontalAlign = "Center"/>
                 <HeaderStyle Font-Size="12px" />
                </asp:TemplateField>
                           
                    
             <%--  <asp:BoundField DataField="salt_alloc" HeaderText="Salt Alloc" SortExpression="salt_alloc">
                           <ItemStyle Font-Bold="False" />
                           </asp:BoundField>--%>
                           
              <asp:TemplateField HeaderText="Salt Alloc" SortExpression="salt_alloc">
               <ItemTemplate>
              <ItemStyle />
                    <HeaderStyle/>
                    
                   <asp:Label ID="lblSalt" runat="server" Text='<%# Bind("salt_alloc") %>'> </asp:Label>
                     
                 </ItemTemplate>
                 
                 <FooterTemplate>
                                   
                    <asp:Label ID="lbl_Salt" runat="server"  ForeColor="Black" Font-Names="Vani"></asp:Label>
                    
                    </FooterTemplate>
                      <FooterStyle CssClass="FooterStyle" HorizontalAlign = "Center"/>
                 <HeaderStyle Font-Size="12px" />
                </asp:TemplateField>
                           
                                             
             <asp:TemplateField HeaderText="Maize Alloc" SortExpression="Maize">
               <ItemTemplate>
              <ItemStyle />
                    <HeaderStyle/>
                    
                   <asp:Label ID="lblMaize" runat="server" Text='<%# Bind("Maize") %>'> </asp:Label>
                     
                 </ItemTemplate>
                 
                 <FooterTemplate>
                                   
                    <asp:Label ID="lbl_Maize" runat="server"  ForeColor="Black" Font-Names="Vani"></asp:Label>
                    
                    </FooterTemplate>
                      <FooterStyle CssClass="FooterStyle" HorizontalAlign = "Center"/>
                 <HeaderStyle Font-Size="12px" />
                </asp:TemplateField>
                           
                           
                               
                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" />
                            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="Black" Font-Bold="True" Font-Size="Small" ForeColor="White" />
                        </asp:GridView>
                    </center>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td style="width: 160px">
                <asp:Button ID="btnNew" runat="server"  Text="New" Width="70px" OnClick="btnNew_Click" /></td>
            <td colspan="2">
                <asp:Button ID="btnSave" runat="server" Enabled="False" Text="Save"
                    Width="100px" OnClick="btnSave_Click" /></td>
            <td style="width: 190px">
                <asp:Button ID="btnCLose" runat="server" Text="Close" Width="60px" OnClick="btnCLose_Click" /></td>
        </tr>
        <tr>
            <td colspan="2"><asp:Button ID="btnsms_transp" runat="server" Visible = "False" Text="Send SMS to Transporter"
                    Width="208px" OnClick="btnsms_transp_Click" OnClientClick = "javascript:return ConfirmationBox()" />
                    </td>
            <td style="width: 160px">
            
            <asp:Button ID="btnsms" runat="server" Visible = "False" Text="Send SMS to FPS"
                    Width="121px" OnClick="btnsms_Click" OnClientClick = "javascript:return ConfirmationBox()" /></td>
            <td style="width: 190px">
                <asp:HyperLink ID="hlinkpdo" runat="server" Font-Size="11pt" NavigateUrl="#" Visible="False"
                    Width="102px">Print This</asp:HyperLink></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lbl_res" runat="server"></asp:Label></td>
            <td colspan="2">
                <asp:Label ID="lbl_status" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="3">
            
            <asp:ListBox ID="ddl_fps_name" runat="server" Width="192px" Visible="False">
                </asp:ListBox>
                <asp:HiddenField ID="hd_fps" runat="server" />
            
            </td>
            <td style="width: 190px">
            </td>
        </tr>
    </table>
</asp:Content>

