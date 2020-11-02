<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Truck_Movement_Details.aspx.cs" Inherits="IssueCenter_Truck_Movement_Details" Title="Truck Movement Details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="trans">
<div id="hedl"> Truck Movement Details</div>
    <table> 
    <tr>
    <td class ="tdmarginro" style="width: 119px" ></td>
    <td style="width: 138px" align="left">
        &nbsp;</td>
    </tr>
        <tr>
            <td  colspan="2" align="center">
                <asp:Label ID="lbldisp" runat="server" Visible="False" Font-Italic="True" ForeColor="#C00000"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="2">
            <asp:GridView ID="dgridchallan" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="dgridchallan_SelectedIndexChanged" OnRowDataBound="dgridchallan_RowDataBound"
          AllowPaging="True" PageSize="5" PagerSettings-Visible ="false" ShowFooter = "True" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None"  >
        <HeaderStyle  CssClass="gridheader" BackColor="Tan" Font-Bold="True"   />
        <Columns>
            <asp:CommandField SelectText="Preview" ShowSelectButton="True" >
                <ItemStyle CssClass="griditem" />
            </asp:CommandField>
                       
            <asp:BoundField DataField="challan_no" HeaderText="Challan No." ReadOnly="True" SortExpression="challan_no" >
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
            
            <asp:BoundField DataField="Truck_no" HeaderText="Truck  No" ReadOnly="True" SortExpression="Truck_no" >
                <ItemStyle CssClass="griditem" />
            </asp:BoundField>
            <asp:BoundField DataField="IsDeposit" HeaderText="Status Of Receipt" SortExpression="IsDeposit" />
        </Columns>
         <PagerSettings Visible ="False"/>
                <FooterStyle BackColor="Tan" />
                <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                <AlternatingRowStyle BackColor="PaleGoldenrod" />
    </asp:GridView>
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



