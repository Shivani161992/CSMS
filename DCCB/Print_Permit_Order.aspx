<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_Permit_Order.aspx.cs" Inherits="District_Print_Permit_Order" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Permit  Order</title>
    <link rel="Stylesheet" href="../MyCss/Comon.css" type="text/css" />
    <link rel="stylesheet" href = "../MyCss/menu.css" type ="text/css" />
  
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
    <a style="cursor:pointer"  onclick ="javascript:CallPrint('printdiv')"> <img src=  "../Images/print.jpg" alt ="print" style="width: 40px; height: 32px" /><strong >&nbsp;</strong></a>
    <div id="printdiv">
    <center >
    <table cellpadding ="0" cellspacing ="0"  border ="0" width ="500px">
    <tr>
    <td colspan ="2" align="center"> 
        <asp:Label ID="Label2" runat="server" Text = "FORM OF RELEASE ORDER TO BE ISSUED" ForeColor ="Maroon" Font-Size ="13px" Font-Underline ="True" Font-Bold="True" ></asp:Label>
     </td>
     </tr> 
    <tr>
    <td colspan ="2"> 
        <asp:Label ID="title" runat="server" Text="DISTRICT CENTRAL COOPERATIVE BANK" ForeColor ="Maroon" Font-Size ="20px" Width="480px"></asp:Label>
     </td>
     </tr> 
       
      
                <tr>
                <td style="height: 19px" align="center"> District:
                    <asp:Label ID="lbldistrict" runat="server"></asp:Label></td>
                <td style="height: 19px"> &nbsp;</td>
                </tr>
                <tr>
                <td align="left"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;</td>
                <td> </td>
              </tr>
        
  
    
    </table>
    </center>
       
    
       <center >
           <table cellpadding ="0" cellspacing ="0" class ="permitfont"  border ="0"  >
    
    <tr>
        <td rowspan="3">
    
        <table border="0" cellpadding ="0" cellspacing ="0" class ="permitfont" style="width: 200px">
            <tr>
                <td align="left">
                    To,</td>
                <td align="left" style="width: 79px">
                    </td>
            </tr>
            <tr>
                <td align="left" >
                    MPSCSC Ltd.,</td>
                <td style="width: 79px" align="left" >
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left" style="height: 19px" >
                    <asp:Label ID="lbltodist" runat="server"></asp:Label></td>
                <td style="width: 79px; height: 19px;" align="left" >
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left" >
                    Permit No.</td>
                <td style="width: 79px" align="left" >
                    <asp:Label ID="lblper_no" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td align="left">
                    Allotment Month.</td>
                <td align="left" style="width: 79px">
                    <asp:Label ID="lblmonth" runat="server" Text="September"></asp:Label></td>
            </tr>
        </table cellpadding ="0" cellspacing ="0" class ="permitfont">
            &nbsp;</td>
        <td rowspan="1" colspan="3">
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        </td>
     <td>
     <table cellpadding ="0" cellspacing ="0" class ="permitfont">
     <tr>
     <td align="left" style="height: 19px"></td>
     <td style="height: 19px" align="center">
         </td>
     </tr>
     <tr>
     <td>Date Of Issue</td>
     <td align="center">
         <asp:Label ID="lblissudt" runat="server" Text=" " Font-Bold ="true"></asp:Label></td>
     </tr>
     <tr>
     <td>Validity Date</td>
     <td align="center">
         <asp:Label ID="lblvaliddt" runat="server" Text=" " Font-Bold ="true"></asp:Label></td>
     </tr>
     </table>
     </td>
     </tr> 
           
                
  
    
    </table>
    </center>

        <center >
        <table cellpadding ="0" cellspacing ="0" class ="permitfont" style="width: 555px" > 
        <tr>
        <td align="left" style="height: 15px">Please deliver to M/s.</td>
            <td colspan="3" align="left" style="height: 15px">
            <asp:Label ID="lblisname" runat="server" Text="Lead Name " Font-Names="Kruti Dev 010" Font-Bold ="True" Width="190px"></asp:Label></td>
            <td colspan="1" style="height: 15px">
            </td>
        
        </tr>
        <tr>
        <td style="height: 21px" align="left" colspan="4"> F.P. Shop dealer /Bulk consumer No.<asp:Label ID="lblcno" runat="server" Font-Bold ="True" Width="102px">.....................................</asp:Label>holding permit No.
                <asp:Label ID="lblper" runat="server"></asp:Label></td>
            <td colspan="1" style="height: 21px">
                </td>
        
        </tr>
        <tr>
        <td align="left" colspan="5"> Date:-<asp:Label ID="lblrodate" runat="server" Text="" Font-Bold ="true"> </asp:Label >
            the following quantityof food grain in accordancewith
        </td>
        
        </tr>
            <tr>
                <td colspan="4" align="left">
                    the usual term and condition governing the sale of food grain from the</td>
                <td colspan="1">
                </td>
            </tr>
            <tr>
                <td colspan="4" align="left">
                    corporation's Stock of.......................................................................................</td>
                <td colspan="1">
                </td>
            </tr>
        </table>
    
    </center> 
    
    <center >
    <table cellpadding ="0" cellspacing ="0" border="1"  class ="permitfont" >
    <tr>
    <td>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="558px" FooterStyle-ForeColor="green" ShowFooter="True"  OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:BoundField DataField="fps_name" HeaderText="Detail of  Fair Proce Shop" ReadOnly="True"
                    SortExpression="fps_name" FooterText="Total" >
                    <HeaderStyle CssClass="gridheader" />
                </asp:BoundField>
                <asp:BoundField DataField="commodity_name" HeaderText="Discription of Food grains/Sugar/Commidity"
                    ReadOnly="True" SortExpression="commodity_name" >
                    <HeaderStyle CssClass="gridheader" />
                </asp:BoundField>
                <asp:BoundField DataField="scheme_name" HeaderText="Sheme" ReadOnly="True" SortExpression="scheme_name" >
                    <HeaderStyle CssClass="gridtotsize" />
                </asp:BoundField>
                
                <asp:TemplateField HeaderText="Quntity  to be Issued (in Qtls)">
     
                <ItemTemplate>
                <asp:Label ID="lblQty" runat="server" 
                Text='<%# Eval("quantity").ToString()%>'>
                </asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                <asp:Label ID="lblTotalQty" runat="server"></asp:Label>
                </FooterTemplate>
                    <HeaderStyle CssClass="gridtotsize" />
                 </asp:TemplateField>
                
                
                
                <asp:BoundField DataField="rate_per_qtls" HeaderText="Rate per Quantal(Rs.)" ReadOnly="True"
                    SortExpression="rate_per_qtls" >
                    <HeaderStyle CssClass="gridtotsize" />
                </asp:BoundField>
              
                <asp:TemplateField HeaderText="Amount(Rs.)">
     
                <ItemTemplate>
                <asp:Label ID="lblAmount" runat="server" 
                Text='<%# Eval("Totalamt").ToString()%>'>
                </asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                <asp:Label ID="lblTotal" runat="server"></asp:Label>
                </FooterTemplate>
                    <HeaderStyle CssClass="gridtotsize" />
                 </asp:TemplateField>
             

            </Columns>
            
            <FooterStyle Font-Bold="True" ForeColor="Green" />
            
        </asp:GridView>
       
       </td>
    </tr>
    </table>
    </center>
    <center >
        <table style="width: 564px"  cellpadding ="0" cellspacing ="0" class ="permitfont"> 
        <tr>
            <td colspan="4" style="height: 21px" align="left">
                2.The retail selling price of the Wheat/Rice/Commodity is fixed as under:-</td>
           
        
        </tr>
        <tr>
            <td align="left" colspan="4" style="height: 21px">
                &nbsp;&nbsp; Retail sale will be done at the rate shown below :-</td>
           
        
        </tr>
        </table>
   </center>
    
    <center >
        
    <table cellpadding ="0" cellspacing ="0" class ="permitfont">
        <tr>
            <td style="height: 21px">
            </td>
            <td align="left" style="color: black; height: 21px; background-color: white">
            </td>
            <td align="left" colspan="3" style="color: black; height: 21px; background-color: white">
                Per Quantal</td>
            <td align="left" style="width: 7px; color: black; height: 21px; background-color: white">
            </td>
            <td align="left" style="color: black; height: 21px; background-color: white">
            </td>
            <td align="left" style="color: black; height: 21px; background-color: white">
            </td>
        </tr>
        <tr>
            <td style="height: 21px">
                1.</td>
            <td align="left" style="color: black; height: 21px; background-color: white">
            Price to be paid to FCI/SCTC/Apex MarkFED</td>
            <td align="left" style="color: black; height: 21px; background-color: white">
                @</td>
            <td align="left" style="color: black; height: 21px; background-color: white">
                Rs.</td>
            <td align="left" style="color: black; height: 21px; background-color: white">
                <asp:Label ID="lbldepositrs" runat="server" Text="..................."></asp:Label></td>
            <td align="left" style="width: 7px; color: black; height: 21px; background-color: white">
            </td>
            <td align="left" style="color: black; height: 21px; background-color: white">
            </td>
            <td align="left" style="color: black; height: 21px; background-color: white">
            </td>
        </tr>
    <tr>
    <td style="height: 21px"> 2.</td>
        <td style="color: black; height: 21px; background-color: white" align="left">
            Commision</td>
        <td align="left" style="color: black; height: 21px; background-color: white">
            @</td>
        <td align="left" style="color: black; height: 21px; background-color: white">
            Rs.</td>
        <td align="left" style="color: black; height: 21px; background-color: white">
            <asp:Label ID="Label14" runat="server" Text="..................."></asp:Label>.</td>
        <td align="left" style="width: 7px; color: black; height: 21px; background-color: white">
        </td>
        <td align="left" style="color: black; height: 21px; background-color: white">
        </td>
        <td align="left" style="color: black; height: 21px; background-color: white">
        </td>
    </tr>
    <tr>
    <td align="left">
        3.</td>
        <td align="left">
            Transportation Charges</td>
        <td align="left">
            @</td>
        <td align="left">
            Rs.</td>
        <td align="left">
            <asp:Label ID="Label15" runat="server" Text="..................."></asp:Label></td>
        <td align="left" style="width: 7px">
        </td>
        <td align="left">
        </td>
        <td align="left">
        </td>
    </tr>
        <tr>
            <td align="left">
                4.</td>
            <td align="left">
                Administration Charges</td>
            <td align="left">
                @</td>
            <td align="left">
                Rs.</td>
            <td align="left">
                <asp:Label ID="Label16" runat="server" Text="..................."></asp:Label></td>
            <td align="left" style="width: 7px">
                &nbsp;
            </td>
            <td align="left">
                Rs.</td>
            <td align="left">
                <asp:Label ID="Label19" runat="server" Text="....................."></asp:Label></td>
        </tr>
        <tr>
            <td align="left">
                5.</td>
            <td align="left">
                Add Charge for rounding figure</td>
            <td align="left">
                @</td>
            <td align="left">
                Rs.</td>
            <td align="left">
                <asp:Label ID="Label17" runat="server" Text="..................."></asp:Label></td>
            <td align="left" style="width: 7px">
                &nbsp;
            </td>
            <td align="left">
                &nbsp;Rs.</td>
            <td align="left">
                .<asp:Label ID="Label20" runat="server" Text="....................."></asp:Label></td>
        </tr>
        <tr>
            <td align="left">
                6.</td>
            <td align="left">
                Octroi</td>
            <td align="left">
                @</td>
            <td align="left">
                Rs.</td>
            <td align="left">
                <asp:Label ID="Label18" runat="server" Text="..................."></asp:Label></td>
            <td align="left" style="width: 7px">
            </td>
            <td align="left">
            </td>
            <td align="left">
            </td>
        </tr>
        <tr>
            <td align="left" colspan="8">
                ..........................................................................................................................................</td>
        </tr>
        <tr>
            <td align="left">
            </td>
            <td align="left">
                <strong>
                Total (Item 1 to 6)&nbsp;</strong></td>
            <td align="left">
            </td>
            <td align="left">
            </td>
            <td align="left" colspan="2">
                Total&nbsp; Rs. &nbsp;
            </td>
            <td align="left" colspan="2">
                <asp:Label ID="lbltotal" runat="server" Text=".............." Width="71px"></asp:Label></td>
        </tr>
    </table>
    
    
    
</center>
   <center >
    <table cellpadding ="0" cellspacing ="0" class ="permitfont">
    <tr>
    <td style="height: 21px; width: 3px;"> </td>
        <td style="color: black; height: 21px; background-color: white" align="left">
            <strong>
            Deposited Rs.</strong></td>
        <td style="color: black; height: 21px; background-color: white">
            Rs.<asp:Label ID="Label1" runat="server" Text=".............."></asp:Label></td>
    </tr>
        <tr>
            <td style="width: 3px; height: 21px">
            </td>
            <td align="left" style="color: black; height: 21px; background-color: white">
                <strong>Vide Challan No.</strong></td>
            <td style="color: black; height: 21px; background-color: white" align="left">
                &nbsp;<asp:Label ID="lblvidechallan" runat="server" Text=".............."></asp:Label></td>
        </tr>
    <tr>
    <td align="left" style="width: 3px; height: 15px;">
    </td>
        <td align="left" style="height: 15px">
            <strong>Date </strong>
        </td>
        <td align="left" style="height: 15px">
            &nbsp;<asp:Label ID="lbldate" runat="server" Text=".............."></asp:Label></td>
    </tr>
        <tr>
            <td align="left" style="width: 3px">
            </td>
            <td align="left">
                <strong>
                Excess Amount Rs.</strong></td>
            <td align="left">
                Rs
                <asp:Label ID="lblexcessamt" runat="server" Text=".............."></asp:Label></td>
        </tr>
        <tr>
            <td align="left" style="width: 3px">
            </td>
            <td align="left">
                <strong>
                (if any)</strong></td>
            <td align="left">
                </td>
        </tr>
        <tr>
            <td align="left" style="width: 3px">
            </td>
            <td align="left">
                <strong>Vide Challan No.</strong></td>
            <td align="left">
                &nbsp;<asp:Label ID="lblvidechalan2" runat="server" Text=".............."></asp:Label></td>
        </tr>
        <tr>
            <td align="left" style="width: 3px; height: 21px">
            </td>
            <td align="left" style="height: 21px">
                <strong>Date </strong>
            </td>
            <td align="left" style="height: 21px">
                &nbsp;<asp:Label ID="lbldate2" runat="server" Text=".............." Width="40px"></asp:Label></td>
        </tr>
    </table>
   </center>
   <center >
   </center>
   <center>
   <table cellpadding ="0" cellspacing ="0" class ="permitfont">
        <tr>
            <td style="height: 21px" colspan="8" align="center">
                <strong>
                Please Return the counter foil indicating whether issue has been made.</strong></td>
        </tr>
       <tr>
           <td style="height: 21px">
           </td>
           <td align="left" colspan="7" style="color: black; height: 21px; background-color: white">
               The price in respect to this Release Order has been made wide Demand Draft Number<asp:Label ID="lbldraft" runat="server" Text="............" Font-Bold="True"></asp:Label></td>
       </tr>
       <tr>
           <td style="height: 21px">
           </td>
           <td align="left" style="color: black; height: 21px; background-color: white;" colspan="7">
               Date:
               <asp:Label ID="lbldt" runat="server" Text="............" Font-Bold="True"></asp:Label>
               <asp:Label ID="Label3" runat="server"></asp:Label>issued by
               <asp:Label ID="lblbankname" runat="server" Text="......................................................................" Width="214px"></asp:Label>
               Bank.</td>
       </tr>
       <tr>
           <td style="height: 21px">
           </td>
           <td align="left" style="color: black; height: 21px; background-color: white; width: 452px;">
           </td>
           <td align="left" style="color: black; height: 21px; background-color: white" colspan="6">
           </td>
       </tr>
       <tr>
           <td style="height: 21px">
           </td>
           <td align="left" style="color: black; height: 21px; background-color: white; width: 452px;">
               For the FPS Shop-Keeper/Retailer</td>
           <td align="left" style="color: black; height: 21px; background-color: white" colspan="6">
           </td>
       </tr>
       <tr>
           <td style="height: 21px">
           </td>
           <td align="right" style="color: black; height: 21px; background-color: white; width: 452px;">
           </td>
           <td align="left" colspan="6" style="color: black; height: 21px; background-color: white">
               Branch Manager</td>
       </tr>
       <tr>
           <td style="height: 21px">
           </td>
           <td align="left" style="color: black; height: 21px; background-color: white; width: 452px;">
           </td>
           <td align="left" colspan="6" style="color: black; height: 21px; background-color: white">
               For Collector</td>
       </tr>
       <tr>
           <td style="height: 21px">
           </td>
           <td align="left" style="color: black; height: 21px; background-color: white; width: 452px;">
           </td>
           <td align="left" style="color: black; height: 21px; background-color: white" colspan="6">
               Distt.<asp:Label ID="lblbranch_dist" runat="server" Width="66px" Font-Bold="True"></asp:Label></td>
       </tr>
        </table> 
       &nbsp;</center>
   
  
    </div>
    </form>
</body>
</html>
