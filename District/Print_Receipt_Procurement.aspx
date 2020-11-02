<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_Receipt_Procurement.aspx.cs" Inherits="District_Print_Receipt_Procurement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Print Gate Pass</title>
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
    <a style="cursor:pointer"  onclick ="javascript:CallPrint('printdiv')"> <img src="../Images/printerss.jpg"alt="print" style="width: 34px" /><strong >&nbsp;</strong></a>
    <div id="printdiv">
    <center >
    <table cellpadding ="0" cellspacing ="0"  border ="0" style="font-size:13px;" >
    <tr>
    <td colspan ="2"> 
        <asp:Label ID="title" runat="server" Text="Madhya Pradesh State Civil Supply Corporation" ForeColor ="maroon" Font-Size ="20px"></asp:Label>
     </td>
     </tr> 
       
      
                <tr>
                    <td colspan="2">
                        Receipt at Rack Point</td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 19px">
                    </td>
                </tr>
        <tr>
            <td colspan="2">
            <asp:Label ID="Label1" runat="server" Text="District:">
                <asp:Label ID="lbldistt" runat="server" Text=""></asp:Label></asp:Label></td>
                </tr>
  
    
    </table>
    </center>
    <center >
    <table  cellspacing="2" style="font-size:13px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
    <tr> 
    <td style="border-right: black thin groove; height: 17px" > </td>
    <td style="border-right: black thin groove"> </td>
    <td style="border-right: black thin groove; height: 17px"> </td>
    <td style="border-right: black thin groove; height: 17px"> </td>
    <td> </td>
    
    </tr>
    <tr>
    <td style="border-right: black thin groove; border-bottom: black thin groove; height: 17px"> </td>
    <td style="border-right: black thin groove; border-bottom: black thin groove; height: 17px"> </td>
        <td align="left" colspan="2" style="border-right: black thin groove; border-bottom: black thin groove; height: 17px;">
            Receipt Acknowledgement</td>
    <td style="border-right: black thin groove; border-bottom: black thin groove; height: 17px"> </td>
    </tr>
    <tr> 
    <td style="border-right: black thin groove; border-bottom: black thin groove; height: 17px;"> Sr.No:</td>
    <td align="left" style="border-right: black thin groove; border-bottom: black thin groove; height: 17px"> <asp:Label ID="lblgno" runat="server" Text="" Font-Size="12px"> </asp:Label></td>
    <td style="border-right: black thin groove; border-bottom: black thin groove; height: 17px"> </td>
    <td align ="left" style="border-right: black thin groove; border-bottom: black thin groove; height: 17px">
        Date/Time</td>
    <td align="left" style="border-right: black thin groove; border-bottom: black thin groove; height: 17px">
        :<asp:Label ID="lblgdtae" runat="server" Text="" Font-Size="12px"> </asp:Label> </td>
    </tr>
    <tr> 
    <td style="height: 17px; border-right: black thin groove; border-bottom: black thin groove;"> 1.</td>
    <td align ="left" style="height: 17px; border-right: black thin groove; border-bottom: black thin groove;"> 
    </td>
    <td align ="left" style="height: 17px; border-right: black thin groove; border-bottom: black thin groove;" > :</td>
    <td align ="left" style="height: 17px; border-right: black thin groove; border-bottom: black thin groove;"> 
                Sending District</td>
    <td align ="left" style="height: 17px; border-right: black thin groove; border-bottom: black thin groove;"> :<asp:Label ID="lblsenddist" runat="server"></asp:Label></td>
    </tr>
        <tr>
            <td style="border-right: black thin groove; height: 17px">
            </td>
            <td align="left" style="border-right: black thin groove">
            </td>
            <td align="left" style="border-right: black thin groove; height: 17px">
            </td>
            <td align="left" style="border-right: black thin groove; height: 17px">
            </td>
            <td align="left">
            </td>
        </tr>
        <tr>
            <td style="border-right: black thin groove; border-bottom: black thin groove">
            2.</td>
            <td align="left" style="border-right: black thin groove; border-bottom: black thin groove">
                Purchase Center</td>
            <td align="left" colspan="3" style="border-right: black thin groove; border-bottom: black thin groove">
                :<asp:Label ID="lblpccenter" runat="server" Font-Size="12px"></asp:Label></td>
        </tr>
        <tr>
            <td style="border-right: black thin groove; height: 17px">
            </td>
            <td align="left" style="border-right: black thin groove">
            </td>
            <td align="left" style="border-right: black thin groove; height: 17px">
            </td>
            <td align="left" style="border-right: black thin groove; height: 17px">
            </td>
            <td align="left">
            </td>
        </tr>
     <tr> 
    <td style="border-right: black thin groove; border-bottom: black thin groove; height: 17px"> 3.</td>
    <td align ="left" style="border-right: black thin groove; border-bottom: black thin groove; height: 17px">Challan No. </td>
    <td align ="left" style="border-right: black thin groove; border-bottom: black thin groove; height: 17px"> :<asp:Label ID="lblchallanno" runat="server" Text=""> </asp:Label></td>
    <td align ="left" style="border-right: black thin groove; border-bottom: black thin groove; height: 17px"> Challan Date</td>
    <td align ="left" style="border-right: black thin groove; border-bottom: black thin groove; height: 17px">
        :<asp:Label ID="lblchallandt" runat="server" Text=""> </asp:Label> </td>
    </tr>
        <tr>
            <td style="border-right: black thin groove; height: 17px">
            </td>
            <td align="left" style="border-right: black thin groove">
            </td>
            <td align="left" style="border-right: black thin groove; height: 17px">
            </td>
            <td align="left" style="border-right: black thin groove; height: 17px">
            </td>
            <td align="left">
            </td>
        </tr>
    <tr> 
    <td style="border-right: black thin groove; border-bottom: black thin groove"> 4.</td>
    <td align ="left" style="border-right: black thin groove; border-bottom: black thin groove"> Commodity Name </td>
    <td align ="left" style="border-right: black thin groove; border-bottom: black thin groove"> :<asp:Label ID="lblcomdty" runat="server" Text=""> </asp:Label></td>
    <td align ="left" style="border-right: black thin groove; border-bottom: black thin groove"> Crop Year</td>
    <td align ="left" style="border-right: black thin groove; border-bottom: black thin groove">
        :<asp:Label ID="lblcrop" runat="server"></asp:Label> </td>
    </tr>
        <tr>
            <td style="border-right: black thin groove; height: 17px">
            </td>
            <td align="left" style="border-right: black thin groove">
            </td>
            <td align="left" style="border-right: black thin groove; height: 17px">
            </td>
            <td align="left" style="border-right: black thin groove; height: 17px">
            </td>
            <td align="left">
            </td>
        </tr>
    <tr> 
    <td style="border-right: black thin groove; border-bottom: black thin groove; height: 17px"> 5.</td>
    <td align ="left" style="border-right: black thin groove; border-bottom: black thin groove; height: 17px"> Transporter Name </td>
    <td align ="left" style="border-right: black thin groove; border-bottom: black thin groove; height: 17px"> :<asp:Label ID="lbltransp" runat="server" Text=""> </asp:Label></td>
    <td align ="left" style="border-right: black thin groove; border-bottom: black thin groove; height: 17px"> Truck&nbsp; No.</td>
    <td align ="left" style="border-right: black thin groove; border-bottom: black thin groove; height: 17px">
        :<asp:Label ID="lblvicln" runat="server" Text=""> </asp:Label> </td>
    </tr>
        <tr>
            <td style="border-right: black thin groove; height: 17px">
            </td>
            <td align="left" style="border-right: black thin groove">
            </td>
            <td align="left" style="border-right: black thin groove; height: 17px">
            </td>
            <td align="left" style="border-right: black thin groove; height: 17px">
            </td>
            <td align="left">
            </td>
        </tr>
    <tr> 
    <td style="border-right: black thin groove; border-bottom: black thin groove; height: 17px"> </td>
    <td align ="left" style="border-right: black thin groove; border-bottom: black thin groove; height: 17px"> 
        <asp:Label ID="lblsendBags" runat="server" Text="Sending Bags"></asp:Label></td>
    <td align ="left" style="border-right: black thin groove; border-bottom: black thin groove; height: 17px"> 
        <asp:Label ID="lblsend_bagsNum" runat="server"></asp:Label></td>
    <td style="border-right: black thin groove; border-bottom: black thin groove; height: 17px"> 
        <asp:Label ID="lblsendQty" runat="server" Text="Sending Quantity"></asp:Label></td>
    <td style="border-right: black thin groove; border-bottom: black thin groove; height: 17px"> 
        <asp:Label ID="lblSend_Qtydisplay" runat="server"></asp:Label></td>
    </tr>
        <tr>
            <td style="border-right: black thin groove; height: 17px">
            </td>
            <td align="left" style="border-right: black thin groove">
            </td>
            <td align="left" style="border-right: black thin groove; height: 17px">
            </td>
            <td style="border-right: black thin groove; height: 17px">
            </td>
            <td>
                <asp:Label ID="lblmeasqty" runat="server" Text="Qty in Qtls"></asp:Label></td>
        </tr>
    <tr> 
    <td style="border-right: black thin groove; border-bottom: black thin groove"> 6.</td>
    <td align ="left" style="border-right: black thin groove; border-bottom: black thin groove"> Number Of Bags</td>
    <td align ="left" style="border-right: black thin groove; border-bottom: black thin groove"> :<asp:Label ID="lblbagno" runat="server" Text=""> </asp:Label></td>
    <td align ="left" style="border-right: black thin groove; border-bottom: black thin groove">Wieght: </td>
    <td align ="left" style="border-right: black thin groove; border-bottom: black thin groove"> :<asp:Label ID="lblweight" runat="server" Text=""> </asp:Label></td>
    </tr>
    <tr> 
    <td style="border-right: black thin groove; height: 17px"> </td>
    <td style="border-right: black thin groove"> </td>
    <td style="border-right: black thin groove; height: 17px"> </td>
    <td style="border-right: black thin groove; height: 17px"> </td>
    <td> (Qty. in Qtls.)</td>
    </tr>
        <tr>
            <td style="border-right: black thin groove; height: 17px">
            </td>
            <td style="border-right: black thin groove">
            </td>
            <td style="border-right: black thin groove; height: 17px">
            </td>
            <td style="border-right: black thin groove; height: 17px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="border-right: black thin groove; height: 17px">
            </td>
            <td style="border-right: black thin groove">
                </td>
            <td style="border-right: black thin groove; height: 17px">
                </td>
            <td style="border-right: black thin groove; height: 17px">
                </td>
            <td>
                </td>
        </tr>
    <tr> 
    <td style="border-right: black thin groove; border-bottom: black thin groove"> 7.</td>
    <td align ="left" style="border-right: black thin groove; border-bottom: black thin groove"> Acceptance No.</td>
    <td align ="left" style="border-right: black thin groove; border-bottom: black thin groove"> :<asp:Label ID="lblwcmno" runat="server" Text=""></asp:Label></td>
    <td align ="left" style="border-right: black thin groove; border-bottom: black thin groove" > Recd. Date</td>
    <td align ="left" style="border-right: black thin groove; border-bottom: black thin groove"> :<asp:Label ID="lblmoisture" runat="server" Text=""> </asp:Label></td>
    </tr>
    <tr> 
    <td style="border-right: black thin groove; height: 19px"> </td>
    <td align ="left" style="border-right: black thin groove; height: 19px;"> </td>
    <td align ="left" style="border-right: black thin groove; height: 19px"> </td>
    <td align ="left" style="border-right: black thin groove; height: 19px"> </td>
    <td align ="left" style="height: 19px">
        &nbsp;</td>
    </tr>
    <tr> 
    <td style="height: 17px;">
        8.</td>
    <td align="left" style="height: 17px;">
        Discription</td>
    <td colspan="2" style="height: 17px;"> ...................................................</td>
    <td style="height: 17px;"> </td>
    </tr>
    <tr> 
    <td style="border-right: black thin groove; height: 17px"></td>
    <td style="border-right: black thin groove"></td>
    <td style="border-right: black thin groove; height: 17px"> &nbsp; &nbsp; &nbsp;
    </td>
    <td style="border-right: black thin groove; height: 17px"></td>
    <td></td>
    </tr>
    <tr> 
    <td style="border-right: black thin groove; height: 17px"></td>
    <td style="border-right: black thin groove">
        <asp:Label ID="lbleror" runat="server" Text="" Visible="false"></asp:Label></td>
    <td style="border-right: black thin groove; height: 17px"> &nbsp;&nbsp;
    </td>
    <td style="border-right: black thin groove; height: 17px"></td>
    <td> </td>
    </tr>
    <tr> 
    <td style="border-right: black thin groove; height: 17px"></td>
    <td style="border-right: black thin groove"></td>
    <td style="border-right: black thin groove; height: 17px"> &nbsp;
    </td>
    <td style="border-right: black thin groove; height: 17px"></td>
    <td> </td>
    </tr>
    <tr> 
    <td style="border-right: black thin groove; height: 17px"></td>
    <td style="border-right: black thin groove">
        Signature Of
    </td>
    <td style="border-right: black thin groove; height: 17px"> Signature Of&nbsp;</td>
    <td style="border-right: black thin groove; height: 17px"> </td>
    <td> Signature Of
    </td>
    </tr>
    <tr> 
    <td style="border-right: black thin groove; height: 17px"> </td>
    <td style="border-right: black thin groove"> Truck Driver</td>
    <td style="border-right: black thin groove; height: 17px">Incharge </td>
    <td style="border-right: black thin groove; height: 17px"> </td>
    <td> Branch Manager</td>
    </tr>
    </table>
    </center >
    </div>
    </form>
</body>
</html>
