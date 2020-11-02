<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Truck_Movement_Print.aspx.cs" Inherits="District_Truck_Movement_Print"  Title="Truck Movement Details"%>
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
    <a style="cursor:pointer"  onclick ="javascript:CallPrint('printdiv')"> <img src="../Images/Printer.png"  alt ="print" width="70" /><strong >&nbsp;</strong></a>
    <div id="printdiv">
    <center >
    <table cellpadding ="0" cellspacing ="0"  border ="0">
    <tr>
    <td colspan ="2"> 
        <asp:Label ID="title" runat="server" Text="Madhya Pradesh State Civil Supply Corporation" ForeColor ="maroon" Font-Size ="20px"></asp:Label>
     </td>
     </tr> 
       
      
                <tr>
                <td> District:<asp:Label ID="lbldistrict" runat="server">District:<asp:Label ID="lbldistt" runat="server"></asp:Label></asp:Label></td>
                <td> </td>
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
                <td> </td>
                <td> </td>
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
    <td> </td>
    
    </tr>
    <tr>
    <td> </td>
    <td> </td>
    <td class = "rgatepass" style="color :Maroon "> Truck Movement</td>
    <td> </td>
    <td> </td>
    </tr>
        <tr>
            <td align="left" colspan="2" style="color: maroon">
                Dispatch Details</td>
            <td class="rgatepass" style="color: maroon">
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            </td>
            <td class="rgatepass" style="color: maroon">
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    <tr> 
    <td> Sr.No:</td>
    <td> </td>
    <td> </td>
    <td align ="left">Date:</td>
    <td>
        :<asp:Label ID="lblgdtae" runat="server" Text=""> </asp:Label> </td>
    </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td align="left">
            </td>
            <td>
            </td>
        </tr>
    <tr> 
    <td> </td>
    <td align ="left">
        Dispatch From
    </td>
    <td align ="left" > :<asp:Label ID="lbldispdist" runat="server"></asp:Label></td>
    <td></td>
    <td> </td>
    </tr>
     <tr> 
    <td> </td>
    <td align ="left">
        Destination Distt.</td>
    <td align ="left"> :<asp:Label ID="lbldestination" runat="server"></asp:Label></td>
    <td align ="left"> Depot</td>
    <td align ="left">
        :<asp:Label ID="lbldestdepo" runat="server"></asp:Label> </td>
    </tr>
    <tr> 
    <td> </td>
    <td align ="left"> Dispatch Date&nbsp;</td>
    <td align ="left"> :<asp:Label ID="lbldispdate" runat="server"></asp:Label></td>
    <td align ="left"> </td>
    <td align ="left">
        &nbsp;</td>
    </tr>
        <tr>
            <td>
            </td>
            <td align="left">
                Dispatch Time</td>
            <td align="left">
                :<asp:Label ID="lbldisptime" runat="server"></asp:Label></td>
            <td align="left">
            </td>
            <td align="left">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td align="left">
                Truck Number</td>
            <td align="left">
                :<asp:Label ID="lbltruckno" runat="server"></asp:Label></td>
            <td align="left">
            </td>
            <td align="left">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td align="left">
                Challan Number</td>
            <td align="left">
                :<asp:Label ID="lblchallanno" runat="server"></asp:Label></td>
            <td align="left">
            </td>
            <td align="left">
            </td>
        </tr>
    <tr> 
    <td> </td>
    <td align ="left"> Transporter Name </td>
    <td align ="left"> :<asp:Label ID="lbltransp" runat="server" Text=""> </asp:Label></td>
    <td align ="left"> </td>
    <td align ="left">
        &nbsp;</td>
    </tr>
        <tr>
            <td>
            </td>
            <td align="left">
                Quantity Dispatched</td>
            <td align="left">
                :<asp:Label ID="lbldispqty" runat="server"></asp:Label>(Qtls.)</td>
            <td align="left">
            </td>
            <td align="left">
            </td>
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
            <td align="left" colspan="2" style="color: maroon">
                Receipt Details</td>
            <td align="left">
            </td>
            <td align="left">
            </td>
            <td align="left">
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2" style="color: maroon">
            </td>
            <td align="left">
                &nbsp; &nbsp;&nbsp;
            </td>
            <td align="left">
            </td>
            <td align="left">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td align="left" colspan="4">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="#C00000"
                    Visible="False"></asp:Label></td>
        </tr>
        <tr>
            <td>
            </td>
            <td align="left">
                <asp:Label ID="Label2" runat="server" Text="Receipt Date" Visible="False"></asp:Label></td>
            <td align="left">
                :<asp:Label ID="lblrecdate" runat="server" Visible="False"></asp:Label></td>
            <td align="left">
            </td>
            <td align="left">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td align="left">
                <asp:Label ID="Label3" runat="server" Text="Receipt Time" Visible="False"></asp:Label></td>
            <td align="left">
                :<asp:Label ID="lblatime" runat="server" Visible="False"></asp:Label></td>
            <td align="left">
            </td>
            <td align="left">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td align="left">
                <asp:Label ID="Label4" runat="server" Text="Quantity Received" Visible="False"></asp:Label></td>
            <td align="left">
                :<asp:Label ID="lblweight" runat="server" Visible="False"></asp:Label></td>
            <td align="left">
            </td>
            <td align="left">
            </td>
        </tr>
    <tr> 
    <td> </td>
    <td align ="left"> </td>
    <td align ="left"> </td>
    <td> </td>
    <td> </td>
    </tr>
    <tr> 
    <td> </td>
    <td align ="left"> </td>
    <td align ="left"> </td>
    <td align ="left"> </td>
    <td align ="left"> </td>
    </tr>
    <tr> 
    <td> </td>
    <td> </td>
    <td> </td>
    <td> </td>
    <td> </td>
    </tr>
    <tr> 
    <td> </td>
    <td align ="left"> </td>
    <td align ="left"> </td>
    <td> </td>
    <td> </td>
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
        </td>
    <td align="left">
        </td>
    <td> ...........................</td>
    <td>
        ........................</td>
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
    </td>
    <td> </td>
    <td> </td>
    <td> 
    </td>
    </tr>
    <tr> 
    <td> </td>
    <td> </td>
    <td> </td>
    <td> </td>
    <td> </td>
    </tr>
    </table>
    </center >
    </div>
    </form>
</body>
</html>
