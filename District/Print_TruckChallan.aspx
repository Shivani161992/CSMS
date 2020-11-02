<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_TruckChallan.aspx.cs" Inherits="Print_TruckChallan"  Title =" Print Truck Challan"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Print Truck Challan</title>
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
    <a style="cursor:pointer"  onclick ="javascript:CallPrint('printdiv')"> <img   alt ="print" /><strong >&nbsp;</strong></a>
    <div id="printdiv">
    <center >
    <table cellpadding ="0" cellspacing ="0"  border ="0">
    <tr>
    <td colspan ="2"> 
        <asp:Label ID="titlempscsc" runat="server" Text=" म. प्र. स्टेट सिविल सप्लाईज कर्पोरेशन लि." ForeColor ="Maroon" Font-Size ="20px"></asp:Label>
     </td>
     </tr> 
       
      
                <tr>
                <td> </td>
                <td> </td>
                </tr>
                <tr>
                <td style="height: 29px"> --जिला--:<asp:Label ID="lbldepott" runat="server" Text="Depot:">
                <asp:Label ID="lbldepot" runat="server" Text=""></asp:Label></asp:Label>
                        --</td>
                <td align="left" style="height: 29px"> 
                    म.प्र.</td>
                </tr>
        <tr>
        <td style="height: 19px"> </td>
                <td style="height: 19px"> </td>
                </tr>
  
    
    </table>
    </center>
    <center >
    <table >
    <tr> 
    <td style="height: 21px; width: 228px;"> </td>
    <td style="width: 163px; height: 21px"> </td>
    <td style="height: 21px"> </td>
    <td style="height: 21px"> </td>
    
    </tr>
    <tr>
    <td style="width: 228px"> </td>
    <td class = "rgatepass" style="color :Maroon ; width: 163px;"> 
        ट्रक चालान 
    </td>
    <td> </td>
    <td> </td>
    </tr>
        <tr>
            <td style="width: 228px">
            </td>
            <td class="rgatepass" style="width: 163px; color: maroon">
                A</td>
            <td>
                No.</td>
            <td>
                <strong>: </strong>
                <asp:Label ID="lblchalanno" runat="server"></asp:Label></td>
        </tr>
    <tr> 
    <td style="width: 228px"> </td>
    <td style="width: 163px"> </td>
    <td align ="left"></td>
    <td>
        &nbsp;</td>
    </tr>
    <tr> 
    <td align ="left" style="font-size: 10pt; width: 228px;">
       बुक नम्बर 
    </td>
    <td align ="left" style="font-size: 10pt; width: 163px" > 
        H/O/A/02-03</td>
    <td style="font-size: 10pt"></td>
    <td style="font-size: 10pt"> </td>
    </tr>
        <tr style="font-size: 12pt">
            <td align="left" style="font-size: 10pt; width: 228px">
                <span style="font-family: Mangal">खरीदी <span style="mso-tab-count: 1"></span>केन्द्र
                    का नाम</span></td>
            <td align="left" style="width: 163px">
       :<asp:Label ID="lblpname" runat="server" Width="97px"></asp:Label></td>
            <td align="left">
            </td>
            <td align="left">
            </td>
        </tr>
    <tr style="font-size: 12pt"> 
    <td align ="left" style="font-size: 10pt; width: 228px;"> 
         संग्रहण केन्द्र का नाम जहाँ स्कन्ध गया है 
       
    </td>
    <td align ="left" style="width: 163px"> 
        :<asp:Label ID="lblgname" runat="server"></asp:Label></td>
    <td align ="left"> </td>
    <td align ="left">
        &nbsp;</td>
    </tr>
    <tr> 
    <td align ="left" style="font-size: 10pt; width: 228px;"> स्कन्ध&nbsp; भेजने की तारीख 
    </td>
    <td align ="left" style="font-size: 12pt; width: 163px"> 
        :<asp:Label ID="lblddate" runat="server"></asp:Label></td>
    <td align ="left" style="font-size: 12pt"> </td>
    <td align ="left" style="font-size: 12pt">
        &nbsp;</td>
    </tr>
        <tr>
            <td align="left" style="font-size: 10pt; width: 228px">
                स्कन्ध&nbsp; भेजने का <span lang="HI" style="font-family: Mangal; mso-hansi-font-family: 'Times New Roman';
                    mso-bidi-language: HI"><span style="font-size: 10pt">समय<?xml namespace="" ns="urn:schemas-microsoft-com:office:office"
                        prefix="o" ?><o:p></o:p></span></span></td>
            <td align="left" style="font-size: 12pt; width: 163px">
                :<asp:Label ID="lbltime" runat="server" Width="93px"></asp:Label></td>
            <td align="left" style="font-size: 12pt">
            </td>
            <td align="left" style="font-size: 12pt">
            </td>
        </tr>
        <tr style="font-size: 12pt">
            <td align="left" style="font-size: 10pt; width: 228px;">
               ट्रक का नम्बर जिसमे स्कन्ध&nbsp; भेजा गया</td>
            <td align="left" style="width: 163px">
                <strong>:</strong><asp:Label ID="lbltruckno" runat="server"></asp:Label></td>
            <td align="left">
            </td>
            <td align="left">
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; width: 228px;">
                ट्रांसपोर्टर का नाम
                                   </td>
            <td align="left" style="width: 163px">
            :<asp:Label ID="lbltransname" runat="server"></asp:Label></td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    <tr style="font-size: 12pt"> 
    <td align ="left" style="font-size: 10pt; width: 228px;"> स्कन्ध&nbsp; का नाम</td>
    <td align ="left" style="width: 163px; font-size: 12pt;"> 
        :<asp:Label ID="lblcomdtyn" runat="server"></asp:Label></td>
    <td align ="left" style="font-size: 12pt"> </td>
    <td align ="left" style="font-size: 12pt"> </td>
    </tr>
        <tr style="font-size: 12pt">
            <td align="left" style="font-size: 10pt; width: 228px">
               ग्रेड</td>
            <td align="left" style="width: 163px">
                :<asp:Label ID="lblsch" runat="server"></asp:Label></td>
            <td align="left">
            </td>
            <td align="left">
            </td>
        </tr>
        <tr style="font-size: 12pt">
            <td align="left" style="font-size: 10pt; width: 228px">
                बोरो की संख्या 
            </td>
            <td align="left" style="font-size: 12pt; width: 163px">
                :<asp:Label ID="lblbagno" runat="server"></asp:Label></td>
            <td align="left" style="font-size: 12pt">
            </td>
            <td align="left" style="font-size: 12pt">
            </td>
        </tr>
        <tr style="font-size: 12pt">
            <td align="left" style="font-size: 10pt; width: 228px">
                शुद्दृ
                    वजन</td>
            <td align="left" style="width: 163px">
                :<asp:Label ID="lblweight" runat="server"></asp:Label>
                (Qtls.)</td>
            <td align="left">
            </td>
            <td align="left">
            </td>
        </tr>
    <tr> 
    <td style="width: 228px"></td>
    <td style="width: 163px"> </td>
    <td></td>
    <td></td>
    </tr>
    <tr> 
    <td style="width: 228px"></td>
    <td style="width: 163px"> </td>
    <td></td>
    <td> </td>
    </tr>
    <tr> 
    <td style="width: 228px"></td>
    <td style="width: 163px"> </td>
    <td></td>
    <td> </td>
    </tr>
    <tr> 
    <td style="width: 228px; font-size: 12px;">
        हस्ताक्षर
    </td>
    <td style="width: 163px"> </td>
    <td> </td>
    <td style="font-size: 12px"> हस्ताक्षर</td>
    </tr>
    <tr> 
    <td style="width: 228px; font-size: 12px;"> ट्रक ड्राइवर / परिवाहन ठेकेदार </td>
    <td style="width: 163px"> </td>
    <td align="left" colspan="2" style="font-size: 12px"> 
        माल भिजवाने वाले अधिकारी के
    </td>
    </tr>
    </table>
    </center >
    </div>
    </form>
</body>
</html>
