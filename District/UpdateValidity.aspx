<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="UpdateValidity.aspx.cs" Inherits="District_UpdateValidity" Title="Update RO Validity" %>
 
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="hedl"> Details of Expired Release Order</div>
    
    <div id="rate">
    <table> 
    <tr>
    <td >
        District Name</td>
    <td style="color: #003333" >
        <strong>:</strong></td>
 <td class ="tdmarginro" style="width: 50px" >
     &nbsp;<asp:Label ID="lbldistrict" runat="server" Width="156px" Font-Bold="True" Font-Italic="True" Font-Size="14px" ForeColor="#004000"></asp:Label></td>
 <td></td>
    </tr>
    </table> 
  </div>
    <table style="width: 625px" >
        <tr>
            <td style="border-right: #663333 thin solid; border-top: #663333 thin solid; border-left: #663333 thin solid; border-bottom: #663333 thin solid;" colspan="4">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound"  AllowPaging="True" PagerSettings-Visible  ="True" OnPageIndexChanging="GridView1_PageIndexChanging" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" Width="605px" PageSize="1"  >
         
        <Columns>
            <asp:BoundField DataField="District_Name" HeaderText="District Name" ReadOnly="True"
                SortExpression="District_Name" >
                <ItemStyle CssClass="griditem" />
                <HeaderStyle CssClass="gridtotsize" />
            </asp:BoundField>
            <asp:BoundField DataField="RO_No" HeaderText="RO No." SortExpression="RO_No" >
                <ItemStyle CssClass="griditem" />
                <HeaderStyle CssClass="gridtotsize" />
            </asp:BoundField>
          <asp:TemplateField HeaderText="RO Date">
     
                <ItemTemplate>
                <asp:Label ID="lblChallan" runat="server" 
                Text='<%# Eval("RO_date").ToString()%>'>
                </asp:Label>
                </ItemTemplate>
               <HeaderStyle CssClass="gridtotsize" />
              <ItemStyle CssClass="griditem" />
                 </asp:TemplateField>
            <asp:TemplateField HeaderText="RO Validity">
     
                <ItemTemplate>
                <asp:Label ID="lbrovalidity" runat="server" 
                Text='<%# Eval("RO_Validity").ToString()%>'>
                </asp:Label>
                </ItemTemplate>
               <HeaderStyle CssClass="gridtotsize" />
              <ItemStyle CssClass="griditem" />
                 </asp:TemplateField>
            <asp:BoundField DataField="RO_qty" HeaderText="RO Qty" SortExpression="RO_qty">
                <ItemStyle CssClass="griditem" />
                <HeaderStyle CssClass="gridtotsize" />
            </asp:BoundField>
            <asp:BoundField DataField="Balance_Qty" HeaderText="Balance Qty." SortExpression="Balance_Qty" >
                <ItemStyle CssClass="griditem" />
                <HeaderStyle CssClass="gridtotsize" />
            </asp:BoundField>
        </Columns>
        <FooterStyle BackColor="Tan" />
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
    
    </asp:GridView>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Label ID="lblmsg" runat="server"></asp:Label></td>
        </tr>
    <tr>
    <td style="border-right: #663333 thin solid; border-top: #663333 thin solid; border-left: #663333 thin solid; border-bottom: #663333 thin solid">Select Date &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    </td>
    <td style="border-right: #663333 thin solid; border-top: #663333 thin solid; border-left: #663333 thin solid; border-bottom: #663333 thin solid">
    

     <asp:TextBox ID="DaintyDate2" runat="server"></asp:TextBox>
                                <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate2'
	    });
	     </script>
    </td>
    
    <td style="border-right: #663333 thin solid; border-top: #663333 thin solid; border-left: #663333 thin solid; border-bottom: #663333 thin solid"> <asp:Button ID="btnGet" runat="server" Text="Update Validity" Width="143px" OnClick="btnGet_Click" /></td>
    <td style="border-right: #663333 thin solid; border-top: #663333 thin solid; border-left: #663333 thin solid; border-bottom: #663333 thin solid"><asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" Width="137px" /></td>
    </tr>
    </table>
    <table >
   
    </table>
    
    <div>
        &nbsp;</div>
    
</asp:Content>



