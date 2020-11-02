<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="DeleteOpeningBalance.aspx.cs" Inherits="District_DeleteOpeningBalance" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 807px; height: 413px">
        <tr>
            <td style="width: 830px">
             <center>   <asp:Label ID="lbldeleteopeningbalance" runat="server" Text="Delete Opening Balance" Width="221px" Font-Bold="True" ForeColor="#000099"></asp:Label>
              </center>
             </td>
            
        </tr>
        <tr>
            <td style="width: 830px">
                <span style="color: #cc0000"><strong>नोट : -
                कृपया इस पेज के लिए Internet Explorer ka
                    इस्तमाल ना करें!</strong></span></td>
        </tr>
        <tr>
            <td style="height: 25px; width: 830px;" >
                <span style="color: #993333; font-family: Verdana">
                Select Issue Center</span> &nbsp;
                &nbsp;&nbsp;
                <asp:DropDownList ID="ddlissuecentre" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlissuecentre_SelectedIndexChanged"
                    Width="197px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 830px; height: 247px">
            <asp:Panel ID = "pnl" runat = "server" ScrollBars="Vertical" Height="350px">
            
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
         ShowFooter = "True" CellPadding="4" ForeColor="#333333" GridLines="None"  Width="778px" Height="119px" DataKeyNames = "Source,Commodity_ID,Scheme_ID" >
                 <Columns>
                    
                    <asp:TemplateField HeaderText="चुने">
                      <ItemTemplate >
                      <asp:CheckBox ID="chkDelete" runat="server" Text='<%# bind("Godown") %>' Font-Size = "1px" />
                      </ItemTemplate>    
                      <HeaderStyle HorizontalAlign="Center"/>
                      <ItemStyle HorizontalAlign="Left"/>
                    </asp:TemplateField>           
                    
                     <asp:BoundField DataField="Godown_Name" HeaderText="Godown" SortExpression="Godown_Name">
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>             
                     <asp:BoundField DataField="Source_Name" HeaderText="Source Of Arrival" SortExpression="Source_Name" >
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>       
                     <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" SortExpression="Commodity_Name" >
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>            
                     <asp:BoundField DataField="Scheme_Name" HeaderText="Scheme" SortExpression="Scheme_Name" >
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Quantity" HeaderText="Opening Qty." SortExpression="Quantity" >
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>         
                     <asp:BoundField DataField="Bags" HeaderText="Opening Bags" SortExpression="Bags">
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>               
                     <asp:BoundField DataField="Current_Balance" HeaderText="Current Balance" SortExpression="Current_Balance" Visible = "false">
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>            
                     <asp:BoundField DataField="Current_Bags" HeaderText="Current Bags" SortExpression="Current_Bags" Visible = "false">
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>       
                     <asp:BoundField DataField="Crop_year" HeaderText="Crop_year" SortExpression="Crop_year">
                         <ItemStyle Font-Size="11px" />
                         <HeaderStyle Font-Size="11px" />
                     </asp:BoundField>
                   <asp:BoundField DataField="Source" HeaderText="SrcID" SortExpression="Source" >
                         <ItemStyle Font-Size="1px" />
                         <HeaderStyle Font-Size="1px" />
                     </asp:BoundField>     
                      <asp:BoundField DataField="Commodity_ID" HeaderText="CmID" SortExpression="Commodity_ID" >
                         <ItemStyle Font-Size="1px" />
                         <HeaderStyle Font-Size="1px" />
                     </asp:BoundField>            
                      <asp:BoundField DataField="Scheme_ID" HeaderText="ScID" SortExpression="Scheme_ID" >
                         <ItemStyle Font-Size="1px" />
                         <HeaderStyle Font-Size="1px" />
                     </asp:BoundField>        
                      <asp:BoundField DataField="Godown" HeaderText="GdID" SortExpression="Godown" >
                         <ItemStyle Font-Size="1px" />
                         <HeaderStyle Font-Size="1px" />
                     </asp:BoundField>
                 </Columns>
                 <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                 <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="True" />
                 <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                 <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                 <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
       <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
       <EditRowStyle BackColor="#999999" />
             </asp:GridView>
            </asp:Panel>
            </td>
        </tr>
        <tr>
            <td style="width: 830px">
                <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" />
                &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp;<asp:Button ID="btnDelete" runat="server" Text="Delete Selected" OnClick="btnDelete_Click" />
            
            </td>
        </tr>
    </table>
</asp:Content>

