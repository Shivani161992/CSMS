<%@ Page Language="C#" MasterPageFile="~/MasterPage/DF_MPSCSC.master" AutoEventWireup="true" CodeFile="Pending_Permit_Print.aspx.cs" Inherits="DCCB_Pending_Permit_Print" Title="DCCB Pending Permit Order" %>
 
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="trans">
<div id="hedl"> Pending Permit  Order &nbsp;List </div>
    
    <div id="rate">
    <table style="border-right: olive thin solid; border-top: olive thin solid; border-left: olive thin solid; border-bottom: olive thin solid"> 
    <tr>
    <td class ="tdmarginro" style="width: 66px" >From Date</td>
    <td style="width: 138px" align="left">
  
        <asp:TextBox ID="DaintyDate1" runat="server"></asp:TextBox>
        
         <script type  ="text/javascript">
                                                	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate1'
	    });
	     </script>
 </td>
 <td class ="tdmarginro" style="width: 50px" >To Date </td>
 <td>
     <asp:TextBox ID="DaintyDate2" runat="server"></asp:TextBox>
  <script type  ="text/javascript">
                                                	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate2'
	    });
	     </script>
 </td>
    </tr>
    </table> 
   <table style="border-right: olive thin solid; border-top: olive thin solid; border-left: olive thin solid; border-bottom: olive thin solid"> 
       <tr>
           <td colspan="2">
               <asp:Label ID="Label1" runat="server" ForeColor="#C00000" Visible="False" Width="300px"></asp:Label></td>
       </tr>
    <tr>
    <td  colspan="2" >
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound"
         AllowPaging="True" PageSize="3" PagerSettings-Visible ="false" ShowFooter = "True" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None"  >
        <HeaderStyle  CssClass="gridheader" BackColor="Tan" Font-Bold="True"   />
         
        <Columns>
        
            <asp:CommandField ShowSelectButton="True">
                <ItemStyle CssClass="griditem" />
            </asp:CommandField>
            <asp:BoundField DataField="Permit_order_no" HeaderText="Permit No." ReadOnly="True"
                SortExpression="Permit_order_no" >
                <ItemStyle CssClass="griditem" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Challan Date">
     
                <ItemTemplate>
                <asp:Label ID="lblChallan" runat="server" 
                Text='<%# Eval("permit_date").ToString()%>'>
                </asp:Label>
                </ItemTemplate>
               <HeaderStyle CssClass="gridtotsize" />
                 </asp:TemplateField>
            <asp:BoundField DataField="LeadName" HeaderText="Lead Society" SortExpression="LeadName" >
                <ItemStyle Font-Names="Kruti Dev 010" Font-Size="Medium" HorizontalAlign="Left" />
            </asp:BoundField>
        </Columns>
        <FooterStyle BackColor="Tan" />
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
            <PagerSettings Visible="False" />
    
    </asp:GridView></td>
    </tr>
    </table> 
  </div>
    
    
    
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


