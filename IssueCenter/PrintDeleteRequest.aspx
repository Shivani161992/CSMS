<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintDeleteRequest.aspx.cs" Inherits="IssueCenter_PrintDeleteRequest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Print Delete Request</title>
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
    
    <style type="text/css">
        .style1
        {
            color: black;
            height: 19px;
        }
        .style2
        {
            font-size: medium;
        }
        .style3
        {
            font-size: small;
        }
        .style4
        {
            height: 19px;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <a style="cursor:pointer"  onclick ="javascript:CallPrint('printdiv')"> <img src="../Images/printerss.jpg"alt="print" style="width: 34px" /><strong >&nbsp;<asp:HiddenField 
        ID="hd_pcid" runat="server" />
    </strong></a>
    &nbsp;<div id="printdiv">
    <center >
    <table cellpadding ="0" cellspacing ="0"  border ="0" style="font-size:13px;" >
    <tr>
    <td colspan ="2"> 
        <asp:Label ID="title" runat="server" 
            Text="Madhya Pradesh State Civil Supplies Corporation Limited" 
            ForeColor ="Maroon" Font-Size ="20px" style="font-weight: 700"></asp:Label>
     </td>
     </tr> 
       
      
                <tr>
                <td> </td>
                <td> </td>
                </tr>
                <tr>
                <td style="height: 19px" class="style2" colspan="2"><center> <strong>स्वीकृति पत्रक डिलीट 
                    करने हेतु आवेदन </strong></center> </td>
                </tr>
                <tr>
                <td style="height: 19px" class="style2"> &nbsp;</td>
                <td style="height: 19px; text-align: right;" class="style2"> <span class="style3">Date</span>-<asp:Label ID="lbl_todaydate" 
                        runat="server" Font-Size="12px"></asp:Label> </td>
                </tr>
        <tr>
        <td> 
            &nbsp;</td>
                <td> 
                    &nbsp;</td>
                </tr>
  
    
    </table>
    </center>
    <center >
    <table  cellspacing="2" style="font-size:13px;">
    <tr> 
    <td style="font-weight: 700" >   
            <asp:Label ID="Label1" runat="server" Text="District:-" 
            style="font-weight: 700">
                <asp:Label ID="lbldistt" runat="server" Text=""></asp:Label></asp:Label> </td>
    <td style="text-align: left">  
            <asp:Label ID="lbl_dist" runat="server" style="font-weight: 700"></asp:Label>
        </td>
    <td> </td>
    <td colspan="2" style="font-weight: 700; text-align: right;">   
            &nbsp;</td>
    <td style="text-align: right">  
            <asp:Label ID="lbldepott" runat="server" Text="Issue Center:-" 
            style="font-weight: 700; text-align: left;">
                <asp:Label ID="lbldepot" runat="server" Text=""></asp:Label></asp:Label> 
        </td>
    
    <td style="text-align: left">  
            <asp:Label ID="lbl_issuecentre" runat="server" 
                style="font-weight: 700; text-align: center"></asp:Label>
        </td>
    
    </tr>
    <tr> 
    <td > &nbsp;</td>
    <td> &nbsp;</td>
    <td> &nbsp;</td>
    <td colspan="2"> &nbsp;</td>
    <td colspan="2"> &nbsp;</td>
    
    </tr>
    <tr> 
    <td > प्रति,</td>
    <td> &nbsp;</td>
    <td> &nbsp;</td>
    <td colspan="2"> &nbsp;</td>
    <td colspan="2"> &nbsp;</td>
    
    </tr>
    <tr> 
    <td > &nbsp;</td>
    <td> &nbsp;</td>
    <td colspan="3"> HO CSMS महोदय </td>
    <td colspan="2"> &nbsp;</td>
    
    </tr>
    <tr> 
    <td > &nbsp;</td>
    <td> &nbsp;</td>
    <td colspan="3"> स्टेट सिविल सप्लाइज कारपोरेशन लिमिटेड</td>
    <td colspan="2"> &nbsp;</td>
    
    </tr>
    <tr> 
    <td > &nbsp;</td>
    <td> &nbsp;</td>
    <td colspan="3"> भोपाल(मध्य प्रदेश)</td>
    <td colspan="2"> &nbsp;</td>
    
    </tr>
    <tr> 
    <td > &nbsp;</td>
    <td> &nbsp;</td>
    <td> &nbsp;</td>
    <td colspan="2"> &nbsp;</td>
    <td colspan="2"> &nbsp;</td>
    
    </tr>
    <tr> 
    <td > &nbsp;</td>
    <td> विषय :- </td>
    <td colspan="5" style="text-align: left"> 
       सीएसएमएस सॉफ्टवेयर में&nbsp; 
        <asp:Label ID="lbl_subject" runat="server" 
            style="text-align: left"></asp:Label>
        &nbsp;|</td>
    
    </tr>
    <tr> 
    <td > &nbsp;</td>
    <td> &nbsp;</td>
    <td colspan="5"> &nbsp;</td>
    
    </tr>
    <tr> 
    <td > &nbsp;</td>
    <td> महोदय जी ,</td>
    <td colspan="5"> &nbsp;</td>
    
    </tr>
    <tr> 
    <td > &nbsp;</td>
    <td> &nbsp;</td>
    <td colspan="5"> निवेदन है कि, सीएसएमएस सॉफ्टवेयर में एंट्री के दौरान ऑपरेटर द्वारा 
        गलती होने के </td>
    
    </tr>
    <tr> 
    <td > &nbsp;</td>
    <td> &nbsp;</td>
    <td colspan="5" style="text-align: left"> कारण,जिस गलती का विवरण उक्त में 
        निम्नानुसार है:</td>
    
    </tr>
    <tr> 
    <td > &nbsp;</td>
    <td> &nbsp;</td>
    <td colspan="5" style="text-align: left"> &nbsp;</td>
    
    </tr>
    <tr> 
    <td > &nbsp;</td>
    <td> &nbsp;</td>
    <td colspan="2" style="border-width: 1px; border-color: #000000; text-align: left"> Crop Year</td>
    <td colspan="3" style="border-width: 1px; border-color: #000000; text-align: left"> 
        <asp:Label ID="lblcrop" runat="server" style="font-weight: 700"></asp:Label> </td>
    
    </tr>
    <tr> 
    <td > &nbsp;</td>
    <td> &nbsp;</td>
    <td colspan="2" style="border-width: 1px; border-color: #000000; text-align: left"> Commodity</td>
    <td colspan="3" style="border-width: 1px; border-color: #000000; text-align: left"> 
        <asp:Label ID="lblcomdty" runat="server" Text="" style="font-weight: 700"> </asp:Label> </td>
    
    </tr>
    <tr> 
    <td > &nbsp;</td>
    <td> &nbsp;</td>
    <td colspan="2" style="border-width: 1px; border-color: #000000; text-align: left"> Acceptance No.</td>
    <td colspan="3" style="border-width: 1px; border-color: #000000; text-align: left"> 
        <asp:Label ID="lbl_acceptance" runat="server" style="font-weight: 700"></asp:Label>
        </td>
    
    </tr>
    <tr> 
    <td > &nbsp;</td>
    <td> &nbsp;</td>
    <td colspan="2" style="border-width: 1px; border-color: #000000; text-align: left"> Sending District</td>
    <td colspan="3" style="border-width: 1px; border-color: #000000; text-align: left"> 
        <asp:Label ID="lblsenddist" runat="server" style="font-weight: 700"></asp:Label> </td>
    
    </tr>
    <tr> 
    <td > &nbsp;</td>
    <td> &nbsp;</td>
    <td colspan="2" style="border-width: 1px; border-color: #000000; text-align: left"> Purchase Centre</td>
    <td colspan="3" style="border-width: 1px; border-color: #000000; text-align: left"> 
        <asp:Label ID="lblpccenter" runat="server" Font-Size="12px" 
            style="font-weight: 700"></asp:Label> </td>
    
    </tr>
    <tr> 
    <td > &nbsp;</td>
    <td> &nbsp;</td>
    <td colspan="5" style="border-width: 1px; border-color: #000000; text-align: left">  
                                <asp:GridView ID="grd_viewDepot" runat="server" AutoGenerateColumns="False" 
                                    BackColor="White" BorderColor="Black" 
            BorderStyle="None" BorderWidth="1px" 
                                    CellPadding="3" GridLines="Vertical" 
                                    Width="500px" Font-Size="Small" 
            style="font-size: small; text-align: center;" >
                                    <Columns>
                                        <asp:BoundField DataField="TC_Number" HeaderText="Challan No" >
                                        <FooterStyle BorderColor="Black" BorderWidth="0px" />
                                        <HeaderStyle BorderColor="Black" BorderWidth="1px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Truck_No" HeaderText="Truck No." >
                                        <FooterStyle BorderColor="Black" BorderWidth="0px" />
                                        <HeaderStyle BorderColor="Black" BorderWidth="1px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Dispatch_Date" HeaderText="Dispach Date" DataFormatString="{0:dd-MM-yyyy}" >
                                        <FooterStyle BorderColor="Black" BorderWidth="0px" />
                                        <HeaderStyle BorderColor="Black" BorderWidth="1px" />
                                        </asp:BoundField>
<asp:BoundField DataField="Acceptance_Date" HeaderText="Acceptance Date" DataFormatString="{0:dd-MM-yyyy}">
                                        <FooterStyle BorderColor="Black" BorderWidth="0px" />
                                        <HeaderStyle BorderColor="Black" BorderWidth="1px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Accept_Qty" HeaderText="Accepted Qty(In Qtl)" >
                                        <FooterStyle BorderColor="Black" BorderWidth="0px" />
                                        <HeaderStyle BorderColor="Black" BorderWidth="1px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IssueID" HeaderText="Issue id" >
                                        <FooterStyle BorderColor="Black" BorderWidth="0px" />
                                        <HeaderStyle BorderColor="Black" BorderWidth="1px" />
                                        </asp:BoundField>
                                    </Columns>
                                  
                                </asp:GridView>
                            </td>
    
    </tr>
    <tr> 
    <td > &nbsp;</td>
    <td colspan="6" style="text-align: right"> 
                                &nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
    
    </tr>
    <tr> 
    <td > &nbsp;</td>
    <td> विवरण :-</td>
    <td colspan="5" style="text-align: left"> 
        <asp:Label ID="lbl_discription" runat="server"></asp:Label>
        </td>
    
    </tr>
    <tr> 
    <td > &nbsp;</td>
    <td> &nbsp;</td>
    <td colspan="5" style="text-align: left"> अतः महोदयजी से निवेदन है की ,दी गयी 
        जानकारी के अनुसार उक्त गलत एंट्री </td>
    
    </tr>
    <tr> 
    <td > &nbsp;</td>
    <td> &nbsp;</td>
    <td colspan="5" style="text-align: left"> डिलीट करें |</td>
    
    </tr>
    <tr> 
    <td > &nbsp;</td>
    <td> &nbsp;</td>
    <td colspan="5" style="text-align: left"> &nbsp;</td>
    
    </tr>
    <tr> 
    <td > &nbsp;</td>
    <td> &nbsp;</td>
    <td colspan="5" style="text-align: left"> &nbsp;</td>
    
    </tr>
    <tr>
    <td class="style4"> </td>
    <td class="style4"> </td>
        <td align="left" colspan="3" class="style1">
            <strong>Data Entry Operator</strong></td>
    <td class="style4" colspan="2" style="text-align: right"> <strong>Seal &amp; Signature</strong></td>
    </tr>
    <tr>
    <td> &nbsp;</td>
    <td> &nbsp;</td>
        <td align="left" colspan="3" style="color: maroon">
            <asp:Label ID="lbl_oprater" runat="server" ForeColor="Black"></asp:Label>
        </td>
    <td colspan="2" style="text-align: right"> (IssueCenter Manager)</td>
    </tr>
    <tr>
    <td> &nbsp;</td>
    <td> &nbsp;</td>
        <td align="left" colspan="3" style="color: maroon">
            &nbsp;</td>
    <td colspan="2"> 
        &nbsp;</td>
    </tr>
    <tr>
    <td> &nbsp;</td>
    <td> &nbsp;</td>
        <td align="left" colspan="3" style="color: maroon">
            &nbsp;</td>
    <td colspan="2"> &nbsp;</td>
    </tr>
 

    </table>
    </center >
    </div>
    </form>
</body>
</html>
