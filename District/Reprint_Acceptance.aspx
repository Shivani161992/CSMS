<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reprint_Acceptance.aspx.cs" Inherits="District_Reprint_Acceptance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Print Acceptance</title>
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
    <td colspan ="2" style="height: 22px"> 
        <asp:Label ID="title" runat="server" Text="Madhya Pradesh State Civil Supply Corporation" ForeColor ="maroon" Font-Size ="20px"></asp:Label>
     </td>
     </tr> 
       
      
                <tr>
                <td style="height: 19px"> </td>
                <td style="height: 19px"> </td>
                </tr>
        <tr>
        <td> </td>
                <td> 
            <asp:Label ID="Label1" runat="server" Text="District:">
                <asp:Label ID="lbldistt" runat="server" Text=""></asp:Label></asp:Label></td>
                </tr>
  
    
    </table>
    </center>
    <center >
    <table cellspacing="3" style="font-size:14px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; width: 70%;">
    <tr> 
    <td style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 1px;" colspan="5"> 
        <strong>
            Receipt Acknowledgement (Acceptance Note) &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:LinkButton ID="lnkback" runat="server" Font-Size="12pt" OnClick="lnkback_Click"
                Width="111px">वापस जाये </asp:LinkButton></strong></td>
    
    </tr>
        <tr>
            <td style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove;
                width: 1px; border-bottom: black thin groove">
            </td>
            <td align="left" colspan="2" style="border-right: black thin groove; border-top: black thin groove;
                border-left: black thin groove; border-bottom: black thin groove">
                <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="Select Crop"
                    Width="80px"></asp:Label></td>
            <td align="left" colspan="2" style="border-right: black thin groove; border-top: black thin groove;
                border-left: black thin groove; border-bottom: black thin groove; text-align: left">
                <asp:DropDownList ID="ddlcrop" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcrop_SelectedIndexChanged"
                    Width="107px">
                </asp:DropDownList>&nbsp;
                <asp:Label ID="lblSessionDist" runat="server" Visible="False"></asp:Label></td>
        </tr>
        <tr>
            <td style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove;
                width: 1px; border-bottom: black thin groove">
            </td>
            <td align="left" colspan="2" style="border-right: black thin groove; border-top: black thin groove;
                border-left: black thin groove; border-bottom: black thin groove">
                <span style="color: #6600ff"><strong>
                Select Acceptance Note Number</strong></span></td>
            <td align="left" colspan="2" style="border-right: black thin groove; border-top: black thin groove;
                border-left: black thin groove; border-bottom: black thin groove; text-align: left">
                <asp:DropDownList ID="ddlAccptNumber" runat="server" Width="280px" AutoPostBack="True" OnSelectedIndexChanged="ddlAccptNumber_SelectedIndexChanged">
                </asp:DropDownList></td>
        </tr>
    <tr> 
    <td style="width: 1px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> Sr.No:</td>
    <td align="left" style="width: 127px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> <asp:Label ID="lblgno" runat="server" Text=""> </asp:Label></td>
    <td style="width: 101px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> </td>
    <td align ="left" style="width: 133px; text-align: center; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
        Date/Time</td>
    <td align="left" style="width: 184px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 18px;">
        :<asp:Label ID="lblgdtae" runat="server" Text=""> </asp:Label> </td>
    </tr>
    <tr> 
    <td style="width: 1px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> 1.</td>
    <td align ="left" style="width: 127px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> Crop Year</td>
    <td align ="left" style="width: 101px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;" > :<asp:Label ID="lblcrop" runat="server"></asp:Label></td>
    <td align ="left" style="width: 133px; text-align: center; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> 
        <asp:Label ID="districtid" runat="server" Visible="False"></asp:Label></td>
    <td align ="left" style="width: 184px; height: 18px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> :</td>
    </tr>
        <tr>
            <td style="width: 1px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
            2.</td>
            <td align="left" style="width: 127px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                Sending District</td>
            <td align="left" style="width: 101px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                :<asp:Label ID="lblsenddist" runat="server"></asp:Label></td>
            <td align="left" style="width: 133px; text-align: center; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                Purchase Center</td>
            <td align="left" style="width: 184px; height: 18px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                <asp:Label ID="lblpccenter" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 1px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
            </td>
            <td align="left" style="width: 127px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
            </td>
            <td align="left" style="width: 101px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
            </td>
            <td align="left" style="width: 133px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; text-align: center;">
            </td>
            <td align="left" style="width: 184px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 18px;">
            </td>
        </tr>
     
    <tr> 
    <td style="width: 1px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; text-align: left;"> 
                3.</td>
    <td align ="left" style="width: 127px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> Commodity Name </td>
    <td align ="left" style="width: 101px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> :<asp:Label ID="lblcomdty" runat="server" Text=""> </asp:Label></td>
    <td align ="left" style="width: 133px; text-align: center; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> AN Date</td>
    <td align ="left" style="width: 184px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 18px;">
        :
        <asp:Label ID="lblmoisture" runat="server" Text=""> </asp:Label></td>
    </tr>
        <tr>
            <td style="width: 1px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
            </td>
            <td align="left" style="width: 127px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
            </td>
            <td align="left" style="width: 101px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
            </td>
            <td align="left" style="width: 133px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; text-align: center;">
            </td>
            <td align="left" style="width: 184px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 18px;">
            </td>
        </tr>
    
   
    <tr> 
    <td style="width: 1px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; text-align: left;"> 4.</td>
    <td align ="left" style="width: 127px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> Acceptance No.</td>
    <td align ="left" style="border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; width: 91px; border-bottom: black thin groove;" colspan="2"> :<asp:Label ID="lblwcmno" runat="server" Text="" Font-Bold="true" Font-Size="14px"></asp:Label></td>
    <td align ="left" style="width: 184px; height: 18px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;"> </td>
    </tr></table>
    <table width="70%" style="font-size:13px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
    <tr> 
    <td>
    <asp:GridView Width="99%" ID="grd_viewDepot" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" onrowdatabound="grd_viewDepot_RowDataBound">
                      
                        <Columns>
                           
                            <asp:BoundField DataField="TruckChalanNo" HeaderText="Challan No"> </asp:BoundField>
                            <asp:BoundField DataField="DateOfIssue1" HeaderText="Challan Date"></asp:BoundField>
                            <asp:BoundField DataField="Transporter_Name" HeaderText="Transporter Name"></asp:BoundField>
                            <asp:BoundField DataField="TruckNo" HeaderText="Truck No."></asp:BoundField>
                            <asp:BoundField DataField="Bags" HeaderText="Send Bags."></asp:BoundField>
                            <asp:BoundField DataField="QtyTransffer" HeaderText="Send Quantity"></asp:BoundField>
                            <asp:BoundField DataField="Acc_Bag" HeaderText="Accept Bags"></asp:BoundField>
                            <asp:BoundField DataField="Accept_Qty" HeaderText="Accept Qty"></asp:BoundField>
                         <asp:TemplateField HeaderText="Rejected Bags">
                      <ItemTemplate>
        <asp:Label ID="TxtNetAmt" runat="server" Text='<%# Convert.ToDecimal(Eval("Bags").ToString()) - Convert.ToDecimal(Eval("Acc_Bag").ToString()) %>' Width="55px">
                      </asp:Label>
                 </ItemTemplate>
           </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rejected Qty">

                      <ItemTemplate>

        <asp:Label ID="TxtNetQty"  runat="server" Width="55px">

                      </asp:Label>

                 </ItemTemplate>

</asp:TemplateField>

                            
                            
                        </Columns>
                        
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="Gainsboro" />
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    </asp:GridView>
     </td>
    
    </tr></table>
    <table width="70%" style="font-size:14px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
        <tr>
            <td style="height: 5px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                8</td>
            <td align="left" colspan="4" style="height: 5px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                <table style="width: 812px">
                    <tr>
                        <td style="width: 118px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                            Stiching Bags Good</td>
                        <td style="width: 100px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                            <asp:Label ID="LblStechingGood" runat="server"></asp:Label></td>
                        <td style="width: 131px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                            Stiching Bags Bad</td>
                        <td style="width: 100px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
                            <asp:Label ID="LblStechingBad" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 118px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 18px;">
                            Stencile Bags Good</td>
                        <td style="width: 114px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 18px;">
                            <asp:Label ID="LblStencileGood" runat="server"></asp:Label></td>
                        <td style="width: 131px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 18px;">
                            Stencile Bags Bad</td>
                        <td style="width: 114px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; height: 18px;">
                            <asp:Label ID="LblStencileBad" runat="server"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
    <tr> 
    <td>
        9.</td>
    <td align="left">
        Discription</td>
    <td colspan="2"> ...................................................</td>
    <td style="text-align: right"> (Qty. in Qtls.)</td>
    </tr>
    <tr> 
    <td style="height: 21px"></td>
    <td style="height: 21px"></td>
    <td > &nbsp; 
        
        &nbsp;&nbsp; &nbsp;
    </td>
    <td style="height: 21px"></td>
    <td style="height: 21px"></td>
    </tr>
    <tr> 
    <td></td>
    <td>
        <asp:Label ID="lbleror" runat="server" Text="" Visible="false"></asp:Label></td>
    <td> &nbsp;&nbsp;
    </td>
    <td></td>
    <td> </td>
    </tr>
    <tr> 
    <td></td>
    <td></td>
    <td> &nbsp;
    </td>
    <td></td>
    <td> </td>
    </tr>
    <tr> 
    <td></td>
    <td>
        Signature Of
    </td>
    <td> Signature Of&nbsp;</td>
    <td> </td>
    <td> Signature Of
    </td>
    </tr>
    <tr> 
    <td> </td>
    <td> Truck Driver</td>
    <td>Incharge (Society)</td>
    <td> </td>
    <td> Incharge (NAN)</td>
    </tr>
    </table>
    </center >
    </div>
    </form>
</body>
</html>
