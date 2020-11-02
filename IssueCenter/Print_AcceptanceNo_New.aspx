<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_AcceptanceNo_New.aspx.cs" Inherits="IssueCenter_Print_AcceptanceNo_New" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Print Acceptance No</title>
    <link rel="Stylesheet" href="../MyCss/Comon.css" type="text/css" />
    <link rel="stylesheet" href ="../MyCss/menu.css" type ="text/css" />
    
    <script language="javascript"  type="text/javascript">
    
function CallPrint(strid)
{
 var prtContent = document.getElementById(strid);
 var WinPrint = window.open('','','letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');
 WinPrint.document.write(prtContent.innerHTML);
 WinPrint.document.close();		
 WinPrint.focus();
 WinPrint.print();
 WinPrint.close();
 prtContent.innerHTML=strOldOne;
}
</script>
    
    <style type="text/css">
        .style9
        {
            height: 25px;
        }
        .style10
        {
            height: 23px;
            text-align: left;
        }
        .style11
        {
            height: 9px;
            text-align: left;
        }
        .style15
        {
            height: 23px;
        }
        .style17
        {
            height: 1px;
        }
        .style18
        {
            height: 10px;
            text-align: right;
        }
        .style19
        {
            height: 2px;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <a style="cursor:pointer"  onclick ="javascript:CallPrint('printdiv')"> <img src="../Images/printerss.jpg"alt="print" style="width: 34px" /><strong >&nbsp;</strong></a>
    <div id="printdiv">

    <center >
 
        <table style="border: thin groove #808080; width: 870px; height: 428px" >
            <tr>
            
                <td colspan="2" class="style19" ><center style="font-weight: 700; font-size: large">
                   <asp:Label ID="lblscsc" 
                        runat="server" Text=" MP State Civil Supplies Corporation Limited" Visible = "false"></asp:Label>
                         <asp:Label ID="lblmarkfed" 
                        runat="server" Text="M.P.State Cooperative Marketing Federation Ltd" Visible = "false"></asp:Label>
                        </center></td>
            </tr>
            <tr>
                <td class="style15" 
                    style="text-align: left; border-bottom-style: groove; border-bottom-width: thin; border-bottom-color: #999999;" 
                    colspan="2" >
                    District:&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lbldist" runat="server" style="font-weight: 700"></asp:Label>
                    &nbsp;&nbsp
                    Issue Center:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblissCen" runat="server" style="font-weight: 700"></asp:Label>
                    </td>
            </tr>
            <tr>
                <td colspan="2" class="style17" 
                    style="border-bottom-color: #999999; border-bottom-width: thin; border-bottom-style: groove">
                    Statement of Receiving and Acceptance&nbsp; at Godown: 
                    <asp:Label ID="lblgodown" 
                        runat="server"></asp:Label>
                    </td>
            </tr>
            <tr>
                <td class="style9" colspan="2" 
                    style="text-align: left; border-bottom-style: groove; border-bottom-width: thin; border-bottom-color: #999999;">
                    Purchase Center Name : 
                    <asp:Label ID="lblpurchaseCent" runat="server"></asp:Label></td>
            </tr>
            <tr style="border-bottom-style: groove; border-bottom-width: thin; border-bottom-color: #999999">
                <td class="style11" style="text-align: left">
                    Acceptance Number: 
                    <asp:Label ID="lblacceptnum" 
                        runat="server"></asp:Label></td>
                <td class="style11">
                    Commodity <asp:Label ID="lblcomm" runat="server"></asp:Label>
                        </td>
            </tr>
            <tr style="border-bottom-style: groove; border-bottom-width: thin; border-bottom-color: #999999">
                <td class="style10">
                    Recd. Date: <asp:Label ID="lblrecddate" runat="server"></asp:Label></td>
                <td class="style10">
                        &nbsp;Acceptance Date : <asp:Label ID="lblacceptdate" runat="server"></asp:Label>
                        </td>
            </tr>
            <tr style="border-bottom-style: groove; border-bottom-width: thin; border-bottom-color: #999999">
                <td class="style18" colspan="2" align = "right">
                    Quantity in Qtls.km.gms</td>
            </tr>
            <tr>
                <td colspan="2" valign ="top" align="right" >
                    <asp:GridView ID="grd_viewDepot" runat="server" AutoGenerateColumns="False"  OnRowDataBound="GridView2_RowDataBound" 
                 ShowFooter="True" HorizontalAlign = "Center"  RowStyle-HorizontalAlign = "Center">
<RowStyle HorizontalAlign="Center"></RowStyle>
                        <Columns>
                           <%-- <asp:BoundField DataField="GodownNO" HeaderText="Godown No"> </asp:BoundField>--%>
                            <asp:BoundField DataField="Receipt_Id" HeaderText="Issue Id" >
                                <HeaderStyle Font-Size="Small" />
                                <ItemStyle Font-Size="Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TC_Number" HeaderText="TC" >
                           
                                <HeaderStyle Font-Size="Small" />
                                <ItemStyle Font-Size="Small" />
                            </asp:BoundField>
                           
                            <asp:BoundField DataField="Truck_Number" HeaderText="Truck No." >
                            
                                <HeaderStyle Font-Size="Small" />
                                <ItemStyle Font-Size="Small" />
                            </asp:BoundField>
                            
                             <%--<asp:BoundField DataField="DateOfIssue1" HeaderText="Dispatch Date" />--%>
                             
                             <asp:TemplateField HeaderText="Dispatch Date" SortExpression="DateOfIssue1">
               <ItemTemplate>
              <ItemStyle />
                    <HeaderStyle/>
                    
                   <asp:Label ID="lblcomm" runat="server" Text='<%# Bind("DateOfIssue1") %>' Font-Size = "Small"> </asp:Label>
                     
                 </ItemTemplate>
                 
                 <FooterTemplate>
                                   
                    <asp:Label ID="lbl_comm" runat="server"  ForeColor="Black" Font-Names="Vani" Text = "Total" Font-Size = "Small"></asp:Label>
                    
                    </FooterTemplate>
                      <FooterStyle HorizontalAlign = "Center" />
                 <HeaderStyle Font-Size="Small" />
                                 <ItemStyle Font-Size="Small" />
                </asp:TemplateField>
                             
                             
                           <%-- <asp:BoundField DataField="sendbags" HeaderText="Sent Bags" />--%>
                            
                             <asp:TemplateField HeaderText="Sent Bags" SortExpression="sendbags">
               <ItemTemplate>
              <ItemStyle />
                    <HeaderStyle/>
                    
                   <asp:Label ID="lblsenbag" runat="server" Text='<%# Bind("sendbags") %>' Font-Size = "Small"> </asp:Label>
                     
                 </ItemTemplate>
                 
                 <FooterTemplate>
                                   
                    <asp:Label ID="lbl_totsenbag" runat="server"  ForeColor="Black" Font-Names="Vani" Font-Size = "Small"></asp:Label>
                    
                    </FooterTemplate>
                      <FooterStyle  HorizontalAlign = "Center"/>
                 <HeaderStyle Font-Size="Small" />
                                 <ItemStyle Font-Size="Small" />
                </asp:TemplateField>
                            
                            
                          <%--  <asp:BoundField DataField="sendqty" HeaderText="Dispatch Quantity" />--%>
                            
                            <asp:TemplateField HeaderText="Dispatch Quantity" SortExpression="sendqty">
               <ItemTemplate>
              <ItemStyle />
                    <HeaderStyle/>
                    
                   <asp:Label ID="lblsenqty" runat="server" Text='<%# Bind("sendqty") %>'> </asp:Label>
                     
                 </ItemTemplate>
                 
                 <FooterTemplate>
                                   
                    <asp:Label ID="lbl_totsenqty" runat="server"  ForeColor="Black" Font-Names="Vani" Font-Size = "Small"></asp:Label>
                    
                    </FooterTemplate>
                      <FooterStyle HorizontalAlign = "Center"/>
                 <HeaderStyle Font-Size="Small" />
                                <ItemStyle Font-Size="Small" />
                </asp:TemplateField>
                            
                             <asp:BoundField DataField="TaulParchi" HeaderText="WCM No" >
                            
                                 <HeaderStyle Font-Size="Small" />
                                 <ItemStyle Font-Size="Small" />
                            </asp:BoundField>
                            
                            <%--<asp:BoundField DataField="Acc_Bag" HeaderText="Recd Bags" />--%>
                            
                             <asp:TemplateField HeaderText="Recd Bags" SortExpression="Acc_Bag">
               <ItemTemplate>
              <ItemStyle />
                    <HeaderStyle/>
                    
                   <asp:Label ID="lblrecbags" runat="server" Text='<%# Bind("Acc_Bag") %>'> </asp:Label>
                     
                 </ItemTemplate>
                 
                 <FooterTemplate>
                                   
                    <asp:Label ID="lbl_totrecbag" runat="server"  ForeColor="Black" Font-Names="Vani" Font-Size = "Small"></asp:Label>
                    
                    </FooterTemplate>
                      <FooterStyle HorizontalAlign = "Center"/>
                 <HeaderStyle Font-Size="Small" />
                                 <ItemStyle Font-Size="Small" />
                </asp:TemplateField>
                            
                            
                           <%-- <asp:BoundField DataField="Accept_Qty" HeaderText="Recd. Qty" />--%>
                            
                             <asp:TemplateField HeaderText="Recd Qty" SortExpression="Accept_Qty">
               <ItemTemplate>
              <ItemStyle />
                    <HeaderStyle/>
                    
                   <asp:Label ID="lblrecqty" runat="server" Text='<%# Bind("Accept_Qty") %>'> </asp:Label>
                     
                 </ItemTemplate>
                 
                 <FooterTemplate>
                                   
                    <asp:Label ID="lbl_totrecqty" runat="server"  ForeColor="Black" Font-Names="Vani" Font-Size = "Small"></asp:Label>
                    
                    </FooterTemplate>
                      <FooterStyle  HorizontalAlign = "Center"/>
                 <HeaderStyle Font-Size="Small" />
                                 <ItemStyle Font-Size="Small" />
                </asp:TemplateField>
                            
                            
                            
                             <%--<asp:BoundField DataField="Stiching_bags" HeaderText="Poor Stiching" />--%>
                             
                             
                             <asp:TemplateField HeaderText="Poor Stiching" SortExpression="Stiching_bags">
               <ItemTemplate>
              <ItemStyle />
                    <HeaderStyle/>
                    
                   <asp:Label ID="lblstibags" runat="server" Text='<%# Bind("Stiching_bags") %>'> </asp:Label>
                     
                 </ItemTemplate>
                 
                 <FooterTemplate>
                                   
                    <asp:Label ID="lbl_totstichbag" runat="server"  ForeColor="Black" Font-Names="Vani" Font-Size = "Small"></asp:Label>
                    
                    </FooterTemplate>
                      <FooterStyle HorizontalAlign = "Center"/>
                 <HeaderStyle Font-Size="Small" />
                                 <ItemStyle Font-Size="Small" />
                </asp:TemplateField>
                             
                             
                             <%-- <asp:BoundField DataField="Stencile_bags" HeaderText="Poor Stencile" />--%>
                              
                              
                               <asp:TemplateField HeaderText="Poor Stencile" SortExpression="Stencile_bags">
               <ItemTemplate>
              <ItemStyle />
                    <HeaderStyle/>
                    
                   <asp:Label ID="lblstenbags" runat="server" Text='<%# Bind("Stencile_bags") %>'> </asp:Label>
                     
                 </ItemTemplate>
                 
                 <FooterTemplate>
                                   
                    <asp:Label ID="lbl_totstencilebag" runat="server"  ForeColor="Black" Font-Names="Vani" Font-Size = "Small"></asp:Label>
                    
                    </FooterTemplate>
                      <FooterStyle HorizontalAlign = "Center"/>
                 <HeaderStyle Font-Size="Small" />
                                   <ItemStyle Font-Size="Small" />
                </asp:TemplateField>
                          
                          <asp:BoundField DataField="Moisture" HeaderText="Moisture Percentage" >
                            
                              <HeaderStyle Font-Size="Small" />
                            </asp:BoundField>
                            
                            
                            <asp:BoundField DataField="category" HeaderText="category" >
                            
                              <HeaderStyle Font-Size="Small" />
                            </asp:BoundField>
                            
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td >
                   </td>
                   
                   <td valign = "bottom" style="text-align: right" >
                       Signature (Issue Center Incharge)</td>
            </tr>
            </table>
 
    </center >
    </div>
    </form>
</body>
</html>
