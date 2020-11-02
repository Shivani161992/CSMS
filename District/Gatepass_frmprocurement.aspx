<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Gatepass_frmprocurement.aspx.cs" Inherits="District_Gatepass_frmprocurement" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <table style="border-right: olive thin solid; border-top: olive thin solid; border-left: olive thin solid; border-bottom: olive thin solid"> 
        <tr>
            <td align="center"  colspan="4" style="background-color: #cccccc">
                <asp:Label ID="Label1" runat="server" Text="Pending Receipt Acknowledgement List  (For Print)"
                    Width="357px" Font-Bold="True"></asp:Label></td>
        </tr>
    <tr>
    <td class ="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;" >From Date</td>
    <td style="background-color: #cfdcdc; font-size: 10pt; position: static;" align="left">
   
     <asp:TextBox ID="DaintyDate1" runat="server"></asp:TextBox>
                      <script type  ="text/javascript">
                                                	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate1'
	    });
	     </script>
    
    
 </td>
 <td class ="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;" >To Date </td>
 <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
 <asp:TextBox ID="DaintyDate2" runat="server"></asp:TextBox>
                      <script type  ="text/javascript">
                                                	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate2'
	    });
	     </script>
 </td>
    </tr>
        <tr>
            <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; height: 24px;">
            </td>
            <td align="right" style="background-color: #cfdcdc; font-size: 10pt; position: static; height: 24px;">
                Source of Arrival</td>
            <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; height: 24px;">
                ---------</td>
            <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; height: 24px;">
    <asp:DropDownList ID="ddlsarrival" runat="server" Width="120px">
      
     </asp:DropDownList></td>
        </tr>
        <tr>
            <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
            </td>
            <td align="right" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
                <asp:Button ID="btnGet" runat="server" Text="Get Data" Width="163px" OnClick="btnGet_Click" /></td>
            <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
            </td>
            <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Close" Width="151px" /></td>
        </tr>
        <tr>
            <td class="tdmarginro" colspan="4">
        <asp:Label ID="lblmsg" runat="server" Font-Italic="True" ForeColor="#C00000" Width="304px"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDeleting="GridView1_RowDeleting"
         AllowPaging="True" PagerSettings-Visible ="false" ShowFooter = "True" CellPadding="4" 
                    ForeColor="#333333" GridLines="None" Width="569px" PageSize="15"  >
        <HeaderStyle  CssClass="gridheader" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"   />
         
        <Columns>
        
            <asp:CommandField HeaderText="Receipt Acknowledgement" SelectText="Print Acknowledgement" ShowSelectButton="True">
                <ItemStyle CssClass="griditem" />
            </asp:CommandField>
            <asp:BoundField DataField="Receipt_Id" HeaderText="Acknowledgement No." ReadOnly="True"
                SortExpression="Receipt_Id" >
                <ItemStyle CssClass="griditem" />
            </asp:BoundField>
            <asp:CommandField DeleteText="Cancel" HeaderText="Cancel Acknowledgement" ShowDeleteButton="True" >
                <ItemStyle CssClass="griditem" />
            </asp:CommandField>
        </Columns>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="True" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <PagerSettings Visible="False" />
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

