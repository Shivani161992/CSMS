<%@ Page Language="C#" AutoEventWireup="true" CodeFile="print_Gatepass_OtherSource.aspx.cs" Inherits="print_Gatepass_OtherSource" Title =" Print Receipt Gate Pass" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
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
    <a style="cursor:pointer"  onclick ="javascript:CallPrint('printdiv')"> <img src="../Images/printerss.jpg"  alt ="print" style="width: 45px; height: 28px" /><strong >&nbsp;</strong></a>
    <div id="printdiv">
    <center >
    <table cellpadding ="0" cellspacing ="0"  border ="0">
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
                <td> </td>
                <td> </td>
                </tr>
        <tr>
        <td> 
            <asp:Label ID="lbldepott" runat="server" Text="Depot:">
                <asp:Label ID="lbldepot" runat="server" Text=""></asp:Label></asp:Label></td>
                <td> 
                    <asp:Label ID="lbldepon" runat="server" Text=""> </asp:Label></td>
                </tr>
  
    
    </table>
    </center>
    <center >
    <table >
    <tr> 
    <td> </td>
    <td> </td>
    <td> </td>
    <td> </td>
    <td> </td>
    
    </tr>
    <tr>
    <td> </td>
    <td> </td>
        <td align="left" colspan="2" style="color: maroon">
            Receipt&nbsp; Acknowledgement</td>
    <td> </td>
    </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td style="font-weight: bold; color: #003366; font-style: italic">
                Other Source</td>
            <td align="left">
            </td>
            <td>
            </td>
        </tr>
    <tr> 
    <td> Sr.No:</td>
    <td align="left"> <asp:Label ID="lblgno" runat="server" Text=""> </asp:Label></td>
    <td> </td>
    <td align ="left">Date:</td>
    <td align="left">
        :<asp:Label ID="lblgdtae" runat="server" Text=""> </asp:Label> </td>
    </tr>
        <tr>
            <td>
                1</td>
            <td align="left">
                Party Name</td>
            <td align="left" colspan="3">
                <asp:Label ID="lblparty" runat="server"></asp:Label></td>
        </tr>
     <tr> 
    <td> 
                2.</td>
    <td align ="left">Challan No. </td>
    <td align ="left"> :<asp:Label ID="lblchallanno" runat="server" Text=""> </asp:Label></td>
    <td align ="left"> Challan Date:</td>
    <td align ="left">
        :<asp:Label ID="lblchallandt" runat="server" Text=""> </asp:Label> </td>
    </tr>
    <tr> 
    <td> 3.</td>
    <td align ="left"> Commodity Name </td>
    <td align ="left"> :<asp:Label ID="lblcomdty" runat="server" Text=""> </asp:Label></td>
    <td align ="left"> Scheme Name:</td>
    <td align ="left">
        :<asp:Label ID="lblscheme" runat="server" Text=""> </asp:Label> </td>
    </tr>
    <tr> 
    <td> 4.</td>
    <td align ="left"> Transporter Name </td>
    <td align ="left"> :<asp:Label ID="lbltransp" runat="server" Text=""> </asp:Label></td>
    <td align ="left"> Vehicle No.:</td>
    <td align ="left">
        :<asp:Label ID="lblvicln" runat="server" Text=""> </asp:Label> </td>
    </tr>
    <tr> 
    <td> 5.</td>
    <td align ="left"> Arrival Date</td>
    <td align ="left"> :<asp:Label ID="lbladate" runat="server"></asp:Label></td>
    <td align="left"> Arrival Time </td>
    <td align="left"> :<asp:Label ID="lblatime" runat="server" Text=""> </asp:Label></td>
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
    <td> </td>
    <td> </td>
    <td style="font-size: 10pt; font-style: italic"> (Qty. in Qtls.)</td>
    </tr>
    <tr> 
    <td> 
        7.</td>
    <td align ="left"> WCM No. </td>
    <td align ="left"> <asp:Label ID="lblwcmno" runat="server" Text=""> </asp:Label></td>
    <td align="left"> Moisture % </td>
    <td align="left"> :<asp:Label ID="lblmoisture" runat="server" Text=""> </asp:Label></td>
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
