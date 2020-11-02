<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_Gatepass_Procurement.aspx.cs" Inherits="IssueCenter_Print_Gatepass_Procurement" Title =" Print Gate Pass (Procurement)" %>
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
    
    <style type="text/css">
        .style1
        {
            height: 25px;
        }
        .style2
        {
            height: 29px;
        }
        .style3
        {
            height: 24px;
        }
    </style>
    
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
                <td> </td>
                <td> </td>
                </tr>
                <tr>
                <td style="height: 19px"> </td>
                <td style="height: 19px"> </td>
                </tr>
        <tr>
        <td> 
            <asp:Label ID="lbldepott" runat="server" Text="Depot:">
                <asp:Label ID="lbldepot" runat="server" Text=""></asp:Label></asp:Label></td>
                <td> 
            <asp:Label ID="Label1" runat="server" Text="District:">
                <asp:Label ID="lbldistt" runat="server" Text=""></asp:Label></asp:Label></td>
                </tr>
  
    
    </table>
    </center>
    <center >
    <table  cellspacing="2" style="font-size:13px;">
    <tr> 
    <td > </td>
    <td> </td>
    <td> </td>
    <td> </td>
    <td> </td>
    
    </tr>
    <tr>
    <td> </td>
    <td> </td>
        <td align="left" colspan="2" style="color: maroon">
            Receipt Acknowledgement</td>
    <td> </td>
    </tr>
    <tr> 
    <td style="font-size:13px;"> Sr.No:</td>
    <td align="left"> <asp:Label ID="lblgno" runat="server" Text="" Font-Size="12px"> </asp:Label></td>
    <td> </td>
    <td align ="left">
        Date/Time</td>
    <td align="left">
        :<asp:Label ID="lblgdtae" runat="server" Text="" Font-Size="12px"> </asp:Label></td>
    </tr>
    <tr> 
    <td> 1.</td>
    <td align ="left"> IssueCenter
    </td>
    <td align ="left" > :<asp:Label ID="lbldepon" runat="server" Text=""> </asp:Label></td>
    <td align ="left"> 
                Sending District</td>
    <td align ="left"> :<asp:Label ID="lblsenddist" runat="server"></asp:Label></td>
    </tr>
        <tr>
            <td>
            </td>
            <td align="left">
            </td>
            <td align="left">
            </td>
            <td align="left">
            </td>
            <td align="left">
            </td>
        </tr>
        <tr>
            <td>
            2.</td>
            <td align="left">
                Purchase Center</td>
            <td align="left" colspan="3">
                :<asp:Label ID="lblpccenter" runat="server" Font-Size="12px"></asp:Label></td>
        </tr>
        <tr>
            <td>
            </td>
            <td align="left">
            </td>
            <td align="left">
            </td>
            <td align="left">
            </td>
            <td align="left">
            </td>
        </tr>
     <tr> 
    <td> 3.</td>
    <td align ="left">Challan No. </td>
    <td align ="left"> :<asp:Label ID="lblchallanno" runat="server" Text=""> </asp:Label></td>
    <td align ="left"> Challan Date</td>
    <td align ="left">
        :<asp:Label ID="lblchallandt" runat="server" Text=""> </asp:Label></td>
    </tr>
        <tr>
            <td>
            </td>
            <td align="left">
            </td>
            <td align="left">
            </td>
            <td align="left">
            </td>
            <td align="left">
            </td>
        </tr>
    <tr> 
    <td> 4.</td>
    <td align ="left"> Commodity Name </td>
    <td align ="left"> :<asp:Label ID="lblcomdty" runat="server" Text=""> </asp:Label></td>
    <td align ="left"> Crop Year</td>
    <td align ="left">
        :<asp:Label ID="lblcrop" runat="server"></asp:Label></td>
    </tr>
        <tr>
            <td>
            </td>
            <td align="left">
            </td>
            <td align="left">
            </td>
            <td align="left">
            </td>
            <td align="left">
            </td>
        </tr>
    <tr> 
    <td> 5.</td>
    <td align ="left"> Transporter Name </td>
    <td align ="left"> :<asp:Label ID="lbltransp" runat="server" Text=""> </asp:Label></td>
    <td align ="left"> Truck&nbsp; No.</td>
    <td align ="left">
        :<asp:Label ID="lblvicln" runat="server" Text=""> </asp:Label></td>
    </tr>
        <tr>
            <td class="style1">
            </td>
            <td align="left" class="style1">
            </td>
            <td align="left" class="style1">
            </td>
            <td align="left" class="style1">
            </td>
            <td align="left" class="style1">
            </td>
        </tr>
    <tr> 
    <td class="style3"> </td>
    <td align ="left" class="style3"> 
        <asp:Label ID="lblsendBags" runat="server" Text="Sending Bags"></asp:Label></td>
    <td align ="left" class="style3"> 
        <asp:Label ID="lblsend_bagsNum" runat="server"></asp:Label></td>
    <td class="style3"> 
        <asp:Label ID="lblsendQty" runat="server" Text="Sending Quantity"></asp:Label></td>
    <td class="style3"> 
        <asp:Label ID="lblSend_Qtydisplay" runat="server"></asp:Label></td>
    </tr>
        <tr>
            <td class="style2">
            </td>
            <td align="left" class="style2">
            </td>
            <td align="left" class="style2">
                (Bags in Number)</td>
            <td class="style2">
            </td>
            <td class="style2">
                <asp:Label ID="lblmeasqty" runat="server" Text="Qty in Qtls"></asp:Label></td>
        </tr>
    <tr> 
    <td> 6.</td>
    <td align ="left"> Number Of Bags</td>
    <td align ="left"> :<asp:Label ID="lblbagno" runat="server" Text=""> </asp:Label></td>
    <td align ="left">Wieght: </td>
    <td align ="left"> :<asp:Label ID="lblweight" runat="server" Text=""> </asp:Label></td>
    </tr>
    <tr> 
    <td> </td>
    <td> </td>
    <td> (Bags in Number)</td>
    <td> </td>
    <td> (Qty. in Qtls.)</td>
    </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                </td>
            <td>
                </td>
            <td>
                </td>
            <td>
                </td>
        </tr>
    <tr> 
    <td> 7.</td>
    <td align ="left"> Acceptance No.</td>
    <td align ="left"> :<asp:Label ID="lblwcmno" runat="server" Text=""></asp:Label></td>
    <td align ="left" > Recd. Date</td>
    <td align ="left"> :<asp:Label ID="lblmoisture" runat="server" Text=""> </asp:Label></td>
    </tr>
    <tr> 
    <td> </td>
    <td align ="left"> </td>
    <td align ="left"> </td>
    <td align ="left"> </td>
    <td align ="left">
        &nbsp;</td>
    </tr>
    <tr> 
    <td>
        8.</td>
    <td align="left">
        Discription</td>
    <td colspan="2"> ...................................................</td>
    <td> </td>
    </tr>
    <tr> 
    <td></td>
    <td></td>
    <td> &nbsp; &nbsp; &nbsp;
    </td>
    <td></td>
    <td></td>
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
    <td>Incharge </td>
    <td> </td>
    <td> Branch Manager</td>
    </tr>
    </table>
    </center >
    </div>
    </form>
</body>
</html>
