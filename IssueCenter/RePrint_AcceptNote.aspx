<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RePrint_AcceptNote.aspx.cs" Inherits="IssueCenter_RePrint_AcceptNote" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Print Acceptance Note Number</title>
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
    
</head>
<body>
    <form id="form1" runat="server">
    <a style="cursor:pointer"  onclick ="javascript:CallPrint('printdiv')"> <img src="../Images/printerss.jpg"alt="print" style="width: 34px" id="IMG1" /><strong >&nbsp;</strong></a>
    <div id="printdiv">
    <center >
    <table cellpadding ="0" cellspacing ="0"  border ="0">
    <tr>
    <td colspan ="2" style="height: 25px"> 
        <asp:Label ID="title" runat="server" Text="Madhya Pradesh State Civil Supply Corporation" ForeColor ="maroon" Font-Size ="20px"></asp:Label>
     </td>
     </tr> 
        <tr>
        <td> 
            प्राप्तकर्ता डिपो 
            <asp:Label ID="lbldepott" runat="server" Text="Depot:">
                <asp:Label ID="lbldepot" runat="server" Text=""></asp:Label></asp:Label></td>
                <td> 
            <asp:Label ID="Label1" runat="server" Text="District:">
                <asp:Label ID="lbldistt" runat="server" Text=""></asp:Label></asp:Label></td>
                </tr>
  
    
    </table>
    </center>
    <center >
    <table width="70%" cellspacing="3" style="font-size:14px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
        <tr>
            <td style="width: 1px; border-right: black thin groove; border-bottom: black thin groove;">
            </td>
            <td style="width: 113px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="Select Crop"
                    Width="80px"></asp:Label></td>
            <td style="width: 91px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                <asp:DropDownList ID="ddlcrop" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcrop_SelectedIndexChanged"
                    Width="107px">
                </asp:DropDownList></td>
            <td style="width: 122px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 18px;">
            </td>
            <td style="width: 218px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
        <asp:HyperLink ID="hypback" runat="server" NavigateUrl = "~/IssueCenter/issue_welcome.aspx" Font-Bold="True" Width="109px">वापस जायें</asp:HyperLink></td>
        </tr>
        <tr>
            <td style="width: 1px; border-right: black thin groove; border-bottom: black thin groove;">
            </td>
            <td style="width: 113px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="Select Acceptance Number"
                    Width="191px"></asp:Label></td>
            <td colspan="2" style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; width: 91px; border-bottom: black thin groove">
                <asp:DropDownList ID="ddlAccptNumber" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAccptNumber_SelectedIndexChanged"
                    Width="254px">
                </asp:DropDownList></td>
            <td style="width: 218px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; text-align: left;">
                <asp:Label ID="lblSessionDist" runat="server" Visible="False" ></asp:Label>
                <asp:Label ID="lblSessionDepot" runat="server" Visible="False" ></asp:Label></td>
        </tr>
        <tr>
                <td style="border: thin groove black;" colspan="5" class="style4"> 
        स्वीकृति पत्रक</td>
        </tr>
    <tr> 
    <td style="width: 1px; border-right: black thin groove; border-bottom: black thin groove;"> Sr.No:</td>
    <td align="left" style="width: 113px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
     <asp:Label ID="lblgno" runat="server" Text=""> </asp:Label></td>
    <td style="width: 91px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> </td>
    <td align ="left" style="width: 122px; height: 18px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
        Date/Time</td>
    <td align="left" style="width: 218px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
        :<asp:Label ID="lblgdtae" runat="server" Text=""> </asp:Label></td>
    </tr>
    <tr> 
    <td style="width: 1px; border-right: black thin groove; border-bottom: black thin groove;"> 1.</td>
    <td align ="left" style="width: 113px; height: 22px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">Name Of Depot </td>
    <td align ="left" style="width: 91px; height: 22px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;" > :<asp:Label ID="lbldepon" runat="server" Text=""> </asp:Label></td>
    <td align ="left" style="width: 122px; height: 18px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> Crop Year</td>
    <td align ="left" style="width: 218px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> :<asp:Label ID="lblcrop" runat="server"></asp:Label></td>
    </tr>
        <tr>
            <td style="width: 1px; border-right: black thin groove; border-bottom: black thin groove;">
            2.</td>
            <td align="left" style="width: 113px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                Sending District</td>
            <td align="left" style="width: 91px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                :<asp:Label ID="lblsenddist" runat="server"></asp:Label></td>
            <td align="left" style="width: 122px; height: 18px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                </td>
            <td align="left" style="width: 218px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                <asp:Label ID="lblgodown" runat="server" Visible = "false"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 1px; border-right: black thin groove; border-bottom: black thin groove;">
                3.</td>
            <td align="left" style="width: 113px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                Purchase Center</td>
            <td align="left" colspan="3" style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; width: 91px; border-bottom: black thin groove">
                <asp:Label ID="lblpccenter" runat="server" Width="550px"></asp:Label></td>
        </tr>
     
    <tr> 
    <td style="width: 1px; border-right: black thin groove; border-bottom: black thin groove;"> 4.</td>
    <td align ="left" style="width: 113px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> Commodity Name </td>
    <td align ="left" style="width: 91px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> :<asp:Label ID="lblcomdty" runat="server" Text=""> </asp:Label></td>
    <td align ="left" style="width: 122px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 18px;"> AN Date</td>
    <td align ="left" style="width: 218px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
        :
        <asp:Label ID="lblmoisture" runat="server" Text=""> </asp:Label></td>
    </tr>
    
   
    <tr> 
    <td style="width: 1px; border-right: black thin groove; border-bottom: black thin groove;"> 7.</td>
    <td align ="left" style="width: 113px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> Acceptance No.</td>
    <td align ="left" style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; width: 91px; border-bottom: black thin groove;" colspan="2">:<asp:Label ID="lblwcmno" runat="server" Text="" Font-Bold="true" Font-Size="14px"></asp:Label></td>
    <td align ="left" style="width: 218px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> 
        <strongQuantity in Qtls</strong></td>
    </tr></table>
    <table style="font-size:13px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; width: 70%;">
    <tr> 
    <td valign = "top" >
    <asp:GridView Width="99%" ID="grd_viewDepot" runat="server" 
            AutoGenerateColumns="False" onrowdatabound="grd_viewDepot_RowDataBound" 
            ShowFooter="True">
                      
                        <Columns>
                        
                             <asp:BoundField DataField="GodownNO" HeaderText="Godown No"> </asp:BoundField>
                            <asp:BoundField DataField="TruckChalanNo" HeaderText="Challan No"> </asp:BoundField>
                            <asp:BoundField DataField="DateOfIssue1" HeaderText="Challan Date"></asp:BoundField>
                            <asp:BoundField DataField="Transporter_Name" HeaderText="Transporter Name"></asp:BoundField>
                            
                            <asp:TemplateField HeaderText="Truck No">
               <ItemTemplate>
              <ItemStyle  />
                    <HeaderStyle  />
                   <asp:Label ID="txttruck" runat="server" Text='<%# Bind("TruckNo") %>' BorderColor="Black" Width="60px" >           
                 </asp:Label>
                 </ItemTemplate>
                    <FooterTemplate>
                                   
                    <asp:Label ID="lbl_truck" runat="server"  ForeColor="White" Font-Names="Vani" Text = "Grand Total"></asp:Label>
                    
                    </FooterTemplate>
                      <FooterStyle CssClass="FooterStyle" />
                 <HeaderStyle Font-Size="10px" />
                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                </asp:TemplateField>
                            
              <asp:TemplateField HeaderText="Send Bags">
               <ItemTemplate>
              <ItemStyle  />
                    <HeaderStyle  />
                   <asp:Label ID="txtSB" runat="server" Text='<%# Bind("Bags") %>' BorderColor="Black" >           
                 </asp:Label>
                 </ItemTemplate>
                    <FooterTemplate>
                                   
                    <asp:Label ID="lbl_SB" runat="server"  Visible = "true" Text = "Total" ></asp:Label>
                    
                    </FooterTemplate>
                      <FooterStyle CssClass="FooterStyle" />
                </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Send Quantity">
               <ItemTemplate>
              <ItemStyle  />
                    <HeaderStyle  />
                   <asp:Label ID="txtrcq" runat="server" Text='<%# Bind("QtyTransffer") %>' BorderColor="Black" Width="60px" >           
                 </asp:Label>
                 </ItemTemplate>
                    <FooterTemplate>
                                   
                    <asp:Label ID="lbl_SQty" runat="server"  ForeColor="White" Font-Names="Vani"></asp:Label>
                    
                    </FooterTemplate>
                      <FooterStyle CssClass="FooterStyle" />
                 <HeaderStyle Font-Size="10px" />
                </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Accept Bags" SortExpression="Acc_Bag">
               <ItemTemplate>
              <ItemStyle  />
                    <HeaderStyle  />
                   <asp:Label ID="txtAB" runat="server" Text='<%# Bind("Acc_Bag") %>' BorderColor="Black" Width="60px" >           
                 </asp:Label>
                 </ItemTemplate>
                    <FooterTemplate>
                                   
                    <asp:Label ID="lbl_AB" runat="server"  ForeColor="White" Font-Names="Vani"></asp:Label>
                    
                    </FooterTemplate>
                      <FooterStyle CssClass="FooterStyle" />
                 <HeaderStyle Font-Size="10px" />
                </asp:TemplateField>
                
                
                 <asp:TemplateField HeaderText="Accept Qty" >
               <ItemTemplate>
              <ItemStyle  />
                    <HeaderStyle  />
                   <asp:Label ID="txtAQ" runat="server" Text='<%# Bind("Accept_Qty") %>' BorderColor="Black" Width="55px" >           
                 </asp:Label>
                 </ItemTemplate>
                    <FooterTemplate>
                                   
                    <asp:Label ID="lbl_AQ" runat="server"  ForeColor="White" Font-Names="Vani"></asp:Label>
                    
                    </FooterTemplate>
                      <FooterStyle CssClass="FooterStyle" />
                </asp:TemplateField>
                                      
                            
                             <asp:TemplateField HeaderText="Rejected Bags">
                      <ItemTemplate>
        <asp:Label ID="TxtNetAmt" runat="server" Text='<%# Convert.ToDecimal(Eval("Bags").ToString()) - Convert.ToDecimal(Eval("Acc_Bag").ToString()) %>' >
                      </asp:Label>
                 </ItemTemplate>
                 <FooterTemplate>
                                   
                    <asp:Label ID="lbl_RjB" runat="server"  ForeColor="White" Font-Names="Vani"></asp:Label>
                    
                    </FooterTemplate>
                      <FooterStyle CssClass="FooterStyle" />
                 
           </asp:TemplateField>
                 
                            
                           <asp:TemplateField HeaderText="Rejected Qty">
                      <ItemTemplate>
        <asp:Label ID="TxtNetQ" runat="server" Text = "">
                      </asp:Label>
                 </ItemTemplate>
                 
                 <FooterTemplate>
                                   
                    <asp:Label ID="lbl_RjQ" runat="server"  ForeColor="White" Font-Names="Vani"></asp:Label>
                    
                    </FooterTemplate>
                      <FooterStyle CssClass="FooterStyle" />
                 
           </asp:TemplateField>
                            
                            
                        </Columns>
                        
                    </asp:GridView>
     </td>
    
    </tr></table>
    <table style="border: thin groove black; font-size:14px; width: 68%;">
        <tr>
            <td style="height: 4px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; width: 14px;">
                &nbsp;</td>
            <td align="left" colspan="4" style="height: 4px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                <table style="width: 903px">
                    <tr>
                        <td style="width: 140px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 22px;">
                            Stiching Bags Good</td>
                        <td style="width: 60px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 22px;">
                            <asp:Label ID="LblStechingGood" runat="server"></asp:Label></td>
                        <td style="width: 121px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 22px;">
                            Stiching Bags Bad</td>
                        <td style="width: 70px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 22px;">
                            <asp:Label ID="LblStechingBad" runat="server" Width="95px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 140px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                            Stencile Bags Good</td>
                        <td style="width: 60px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                            <asp:Label ID="LblStencileGood" runat="server"></asp:Label></td>
                        <td style="width: 121px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                            Stencile Bags Bad</td>
                        <td style="width: 70px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                            <asp:Label ID="LblStencileBad" runat="server" Width="86px"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
    <tr> 
    <td class="style1">
        9.</td>
    <td align="left" class="style1" colspan="4">
        Description : <span class="style2">&nbsp;मेरे द्वारा म प्र स्टेट सिविल सप्लाइज 
        कर्पोरेशन के प्रतिनिधि की हैसियत से ऊपर दर्शय गए माल का निरक्षण किया गया ।</span></td>
    </tr>
  <tr> 
    <td class="style1">
        &nbsp;</td>
    <td align="left" class="style3" colspan="4">
        उक्त माल एफ ए क्यू गुणवत्ता एवं वजन में सही पाया गया |</td>
    </tr>
    <tr> 
    <td style="width: 14px"></td>
    <td colspan="2" style="text-align: left">
        <asp:Label ID="lbleror" runat="server" Text="" Visible="false"></asp:Label>
        &nbsp;
    </td>
    <td></td>
    <td> </td>
    </tr>
    <tr> 
    <td style="width: 14px"></td>
    <td>
        हस्ताक्षर ट्रक ड्राइवर</td>
    <td> हस्ताक्षर समिति प्रभारी</td>
    <td> </td>
    <td> हस्ताक्षर प्राप्तकर्ता अधिकारी</td>
    </tr>
    <tr> 
    <td style="width: 14px"> </td>
    <td> &nbsp;</td>
    <td>नाम</td>
    <td> </td>
    <td> नाम</td>
    </tr>
    <tr> 
    <td style="width: 14px"> &nbsp;</td>
    <td> &nbsp;</td>
    <td>पद / सील</td>
    <td> &nbsp;</td>
    <td> पद / सील</td>
    </tr>
    </table>
    </center >
    </div>
    </form>
</body>
</html>
