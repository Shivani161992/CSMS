<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Print_Tender_PurchasebyRoad-(Sugar).aspx.cs" Inherits="IssueCenter_Print_Tender_PurchasebyRoad__Sugar_" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Print Gate Pass</title>
    <link rel="Stylesheet" href="../MyCss/Comon.css" type="text/css" />
    <link rel="stylesheet" href ="../MyCss/menu.css" type ="text/css" />
    
    <script language="javascript"  type="text/javascript">

        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            prtContent.innerHTML = strOldOne;
        }
</script>
    
</head>
<body>
    <form id="form1" runat="server">
        <a href="../IssueCenter/print_Gatepass.aspx">../IssueCenter/print_Gatepass.aspx</a>
    <a style="cursor:pointer"  onclick ="javascript:CallPrint('printdiv')"> <img src="../Images/Printer.png"  alt ="print" style="width: 45px; height: 28px" /><strong >&nbsp;</strong></a>
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
        <td> 
            <asp:Label ID="lbldepott" runat="server" Text="Depot:">
                <asp:Label ID="lbldepot" runat="server" Text=""></asp:Label></asp:Label></td>
                <td> 
            <asp:Label ID="Label1" runat="server" Text="District:">
                <asp:Label ID="lbldistt" runat="server" Text=""></asp:Label></asp:Label></td>
                </tr>
                <tr>
                <td> </td>
                <td> 
                    <asp:Label ID="lbldepon" runat="server" Visible="False"></asp:Label></td>
                </tr>
                <tr>
                <td> </td>
                <td> </td>
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
    <td style="width: 104px"> </td>
    
    </tr>
    <tr>
    <td> </td>
    <td> </td>
        <td align="left" colspan="2" style="color: maroon">
            Receipt&nbsp; Acknowledgement</td>
    <td style="width: 104px"> </td>
    </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td align="center" colspan="2" style="font-size: 10pt; color: maroon">
                Tender Purchase(by Road)-Sugar/Salt</td>
            <td style="width: 104px">
            </td>
        </tr>
    <tr> 
    <td> Sr.No:</td>
    <td align="left"> <asp:Label ID="lblgno" runat="server" Text=""> </asp:Label></td>
    <td> </td>
    <td align ="left">Date:</td>
    <td style="width: 104px" align="left"><asp:Label ID="lblgdtae" runat="server" Text=""> </asp:Label> </td>
    </tr>
        <tr>
            <td style="font-size: 12pt">
                1.</td>
            <td align="left" style="font-size: 12pt">
                District</td>
            <td align="left" style="font-size: 12pt">
                :<asp:Label ID="lbldistric" runat="server"></asp:Label></td>
            <td align="left" style="font-size: 12pt">
                IssueCenter</td>
            <td align="left" style="font-size: 12pt">
                :<asp:Label ID="lblissue" runat="server"></asp:Label></td>
        </tr>
     <tr> 
    <td style="font-size: 12pt"> 2.</td>
    <td align ="left" style="font-size: 12pt">Challan No. </td>
    <td align ="left" style="font-size: 12pt"> :<asp:Label ID="lblchallanno" runat="server" Text=""> </asp:Label></td>
    <td align ="left" style="font-size: 12pt"> Challan Date</td>
    <td align ="left" style="font-size: 12pt">
        :<asp:Label ID="lblchallandt" runat="server" Text=""> </asp:Label> </td>
    </tr>
    <tr> 
    <td style="font-size: 12pt"> 3.</td>
    <td align ="left" style="font-size: 12pt"> Commodity Name </td>
    <td align ="left" style="font-size: 12pt"> :<asp:Label ID="lblcomdty" runat="server" Text=""> </asp:Label></td>
    <td align ="left" style="font-size: 12pt"> Scheme Name</td>
    <td align ="left" style="font-size: 12pt">
        :<asp:Label ID="lblscheme" runat="server" Text=""> </asp:Label> </td>
    </tr>
    <tr> 
    <td style="font-size: 12pt"> 4.</td>
    <td align ="left" style="font-size: 12pt"> &nbsp;Vehicle No.</td>
    <td align ="left" style="font-size: 12pt"> :<asp:Label ID="lblvicln" runat="server" Text=""> </asp:Label> </td>
    <td align ="left" style="font-size: 12pt" colspan="2"> &nbsp;</td>
    </tr>
    <tr> 
    <td style="font-size: 12pt"> 5.</td>
    <td align ="left" style="font-size: 12pt"> &nbsp;Arrival Date</td>
    <td align ="left" style="font-size: 12pt"> :<asp:Label ID="lbladate" runat="server"></asp:Label></td>
    <td style="font-size: 12pt" align="left"> Arrival Time</td>
    <td style="font-size: 12pt" align="left"> :<asp:Label ID="lblatime" runat="server" Text=""> </asp:Label></td>
    </tr>
    <tr> 
    <td style="font-size: 12pt"> 6.</td>
    <td align ="left" style="font-size: 12pt"> Number Of Bags</td>
    <td align ="left" style="font-size: 12pt"> :<asp:Label ID="lblbagno" runat="server" Text=""> </asp:Label></td>
    <td align ="left" style="font-size: 12pt">
        Wieght
    </td>
    <td align ="left" style="font-size: 12pt"> :<asp:Label ID="lblweight" runat="server" Text=""> </asp:Label></td>
    </tr>
    <tr> 
    <td style="font-size: 12pt">
        7.</td>
    <td align="left" style="font-size: 12pt">
        Discription</td>
    <td style="font-size: 12pt" colspan="2"> ...................................................</td>
    <td style="font-size: 12pt"> </td>
    </tr>
    <tr> 
    <td></td>
    <td></td>
    <td> &nbsp; &nbsp; &nbsp;
    </td>
    <td></td>
    <td style="width: 104px"></td>
    </tr>
    <tr> 
    <td></td>
    <td></td>
    <td> &nbsp;&nbsp;
    </td>
    <td></td>
    <td style="width: 104px"> </td>
    </tr>
    <tr> 
    <td></td>
    <td></td>
    <td> &nbsp;&nbsp;
    </td>
    <td></td>
    <td style="width: 104px"> </td>
    </tr>
    <tr> 
    <td></td>
    <td></td>
    <td> &nbsp;
    </td>
    <td></td>
    <td style="width: 104px"> </td>
    </tr>
    <tr> 
    <td></td>
    <td>
        Signature Of
    </td>
    <td> Signature Of&nbsp;</td>
    <td> </td>
    <td style="width: 104px"> Signature Of
    </td>
    </tr>
        <tr>
            <td>
            </td>
            <td>
                Truck Driver</td>
            <td>
                Incharge</td>
            <td>
            </td>
            <td style="width: 104px">
                Branch Manager</td>
        </tr>
    </table>
    </center >
    </div>
    </form>
</body>
</html>


