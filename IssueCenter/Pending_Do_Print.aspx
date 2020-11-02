<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Pending_Do_Print.aspx.cs" Inherits="IssueCenter_Pending_Do_Print" Title="Pending Delivery Order for Print" %>
 
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="trans">
<div id="hedl"> Delivery Order &nbsp;List for Print</div>
    
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
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDeleting="GridView1_RowDeleting"
         AllowPaging="True" PageSize="15" PagerSettings-Visible ="true" ShowFooter = "True" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" Width="516px" OnPageIndexChanged="GridView1_PageIndexChanged" OnPageIndexChanging="GridView1_PageIndexChanging"  >
        <HeaderStyle  CssClass="gridheader" BackColor="Tan" Font-Bold="True"   />
         
        <Columns>
        
            <asp:CommandField ShowSelectButton="True">
                <ItemStyle CssClass="griditem" />
            </asp:CommandField>
            <asp:BoundField DataField="delivery_order_no" HeaderText="DO No." ReadOnly="True"
                SortExpression="delivery_order_no" >
                <ItemStyle CssClass="griditem" />
            </asp:BoundField>
            <asp:BoundField DataField="do_date" HeaderText="DO Date" SortExpression="do_date" />
            <asp:BoundField DataField="issue_name" HeaderText="Lead Society" SortExpression="issue_name" />
            <asp:BoundField DataField="permit_no" HeaderText="Bank Permit No." SortExpression="permit_no" />
        </Columns>
        <FooterStyle BackColor="Tan" />
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
    
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
                                    CommandArgument="last" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" OnClick="Lastbutton_Click" />&nbsp;&nbsp;
                             
                            </div>
                        </td>
                    </tr>
        </table>
        </div>
        </div> 
</asp:Content>

