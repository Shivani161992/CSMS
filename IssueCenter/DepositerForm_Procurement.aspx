<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="DepositerForm_Procurement.aspx.cs" Inherits="IssueCenter_DepositerForm_Procurement" Title="Depositer Form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <script type="text/javascript">


 function CheckCalDate(tx) 
 {
var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode > 0))
{
alert('Please Click on Calander Controll to Enter Date');
event.cancelBubble = true;
event.returnValue = false;
}
}
</script>

 <table style="width: 650px;" >
        <tr>
            <td colspan="4" style=" color: #000099; font-weight: 700">
                Depositer Form </td>
        </tr>
        <tr>
            <td colspan="4" style="color: #000099; font-weight: 700">
                <asp:Label ID="lblmsg" runat="server" ForeColor="#993300" Text=""  Font-Size="Small"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 370px; ">
                <asp:Label ID="Label3" runat="server" ForeColor="#003399" 
                    Text="Select Commodity"></asp:Label>
            </td>
            <td style="width: 119px; ">
                <asp:DropDownList ID="ddlcommodtiy" runat="server"  Width="120px">
                </asp:DropDownList>
            </td>
            <td  >
                <asp:Label ID="Label4" runat="server" ForeColor="#000099" 
                    Text="Select Date of Acceptance"></asp:Label>
            </td>
            <td  >
                <asp:TextBox ID="DaintyDate3" runat="server" Width="121px"></asp:TextBox>
                
           <script type  ="text/javascript">
                     new tcal({
                         'formname': '0',
                         'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate3'
                     });
	     </script>
	     
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="btngodown" runat="server" Text="Get Godown Name" onclick="btngodown_Click" />
            </td>
        </tr>
        <tr>
            <td style="width: 370px;">
                <asp:Label ID="Label5" runat="server" ForeColor="#003399" Text="गोदाम का नाम "></asp:Label>
            </td>
            <td colspan="2" >
                <asp:DropDownList ID="ddlgodown" runat="server" Width="295px" AutoPostBack="True" onselectedindexchanged="ddlgodown_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="width: 540px;">
                </td>
        </tr>
        <tr>
            <td style="width: 370px; ">
                </td>
            <td style="width: 119px; ">
                </td>
            <td style="width: 177px; ">
                &nbsp;</td>
            <td style="width: 540px;">
                </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None"  OnRowDataBound="GridView2_RowDataBound" ShowFooter="True" Width = "100%" >
                    <Columns>
              
             <asp:BoundField DataField="Society_Id" HeaderText="SocID." 
                            SortExpression="Society_Id" >
                            <ItemStyle CssClass="griditemlaro" Width = "50px" />
                            <HeaderStyle CssClass="gridlarohead" />
                        </asp:BoundField>
              
                        <asp:BoundField DataField="Society" HeaderText="Purchase Center" 
                            SortExpression="Society">
                            <ItemStyle CssClass="griditemlaro" Font-Size="9pt" Width="200px" />
                            <HeaderStyle CssClass="gridlarohead" />
                        </asp:BoundField>
                        <%--<asp:BoundField DataField="TC_Number" HeaderText="TC No" 
                            SortExpression="TC_Number">
                            <ItemStyle CssClass="griditemlaro" Font-Size="8pt" Width="30px" />
                            <HeaderStyle CssClass="gridlarohead" />
                        </asp:BoundField>--%>
                        
                     
                      <asp:TemplateField HeaderText="TC Number" SortExpression="TC_Number" ItemStyle-Width ="70px">
               <ItemTemplate>
              <ItemStyle />
                    <HeaderStyle/>
                    
                   <asp:Label ID="lbltc" runat="server" Text='<%# Bind("TC_Number") %>' Font-Size = "Small"> </asp:Label>
                     
                 </ItemTemplate>
                 
                 <FooterTemplate>
                                   
                    <asp:Label ID="lbl_tc" runat="server"  ForeColor="Black" Font-Names="Vani" Text = "Grand Total" Font-Size = "Small" Width = "50px"></asp:Label>
                    
                    </FooterTemplate>
                      <FooterStyle HorizontalAlign = "Center" />
                 <HeaderStyle Font-Size="9px" />
                </asp:TemplateField>
                     
                        
                     
                       
                        <%--<asp:BoundField DataField="Accept_Qty" HeaderText="Accept Qty" 
                            SortExpression="Accept_Qty">
                            <ItemStyle CssClass="griditemlaro" HorizontalAlign="Center" Width="30px" />
                            <HeaderStyle CssClass="gridlarohead" />
                        </asp:BoundField>--%>
                        
                        
                         <asp:TemplateField HeaderText="Accept Qty" SortExpression="Accept_Qty" ItemStyle-Width ="70px">
               <ItemTemplate>
              <ItemStyle />
                    <HeaderStyle/>
                    
                   <asp:Label ID="lblacqt" runat="server" Text='<%# Bind("Accept_Qty") %>' Font-Size = "Small" Width = "50px"> </asp:Label>
                     
                 </ItemTemplate>
                 
                 <FooterTemplate>
                                   
                    <asp:Label ID="lbl_acqt" runat="server"  ForeColor="Black" Font-Names="Vani" Font-Size = "Small"></asp:Label>
                    
                    </FooterTemplate>
                      <FooterStyle HorizontalAlign = "Center" />
                 <HeaderStyle Font-Size="9px" />
                </asp:TemplateField>
                        
                        
                        
                        
                        <%--<asp:BoundField DataField="Bags" HeaderText="Accept Bags" 
                            SortExpression="Bags">
                            <ItemStyle CssClass="griditemlaro" HorizontalAlign="Center" Width="30px" />
                            <HeaderStyle CssClass="gridlarohead" />
                        </asp:BoundField>--%>
                        
                        
            <asp:TemplateField HeaderText="Bags" SortExpression="Bags" ItemStyle-Width ="70px">
               <ItemTemplate>
              <ItemStyle />
                    <HeaderStyle/>
                    
                   <asp:Label ID="lblacbag" runat="server" Text='<%# Bind("Bags") %>' Font-Size = "Small" Width = "50px"> </asp:Label>
                     
                 </ItemTemplate>
                 
                 <FooterTemplate>
                                   
                    <asp:Label ID="lbl_acbag" runat="server"  ForeColor="Black" Font-Names="Vani" Font-Size = "Small"></asp:Label>
                    
                    </FooterTemplate>
                      <FooterStyle HorizontalAlign = "Center" />
                 <HeaderStyle Font-Size="9px" />
                </asp:TemplateField>
                      
                        <asp:BoundField DataField="IssueID" HeaderText="Receipt Id" 
                            SortExpression="IssueID">
                            <ItemStyle CssClass="griditemlaro" HorizontalAlign="Center" Width="70px" />
                            <HeaderStyle CssClass="gridlarohead" />
                        </asp:BoundField>
                        
                                        
                    </Columns>
                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#5D7B9D" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2" >
                <asp:Button ID="btnNew" runat="server" Text="New" Width="100px" 
                    onclick="btnNew_Click" />
            </td>
            <td style="width: 177px;">
                <asp:Button ID="btnsave" runat="server" Text="Submit" Width="100px" 
                    onclick="btnsave_Click" />
            </td>
            <td style=" width: 540px;">
                <asp:Button ID="btnclose" runat="server" Text="Close" Width="75px" 
                    onclick="btnclose_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: left; ">
                <asp:Label ID="whrreq" runat="server" ForeColor="#990000"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: left">
                <asp:Label ID="Label6" runat="server" Visible="False"></asp:Label>
            </td>
            <td colspan="2">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="#" Visible="False" >Print WHR Request</asp:HyperLink>
            </td>
        </tr>
        </table>
</asp:Content>

