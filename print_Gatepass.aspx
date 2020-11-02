<%@ Page Language="C#" AutoEventWireup="true" CodeFile="print_Gatepass.aspx.cs" Inherits="print_Gatepass" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Print Gate Pass</title>
    <link rel="Stylesheet" href="../MyCss/Comon.css" type="text/css" />
    <link rel="stylesheet" href ="../MyCss/menu.css" type ="text/css" />
    <link rel="stylesheet" type="text/css" href="sdmenu/sdmenu.css" />
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
    <a style="cursor:pointer"  onclick ="javascript:CallPrint('printdiv')"> <img src="Images/gg1.gif"  alt ="print" /><strong >&nbsp;</strong></a>
    <div id="printdiv">
    <center >
    <table cellpadding ="0" cellspacing ="0"  border ="0">
    <tr>
    <td colspan ="2"> 
        <asp:Label ID="title" runat="server" Text=" म. प्र. स्टेट सिविल सप्लाईज कर्पोरेशन लि." ForeColor ="maroon" Font-Size ="20px"></asp:Label>
     </td>
     </tr> 
       
      
                <tr>
                <td> </td>
                <td> </td>
                </tr>
                <tr>
                <td> 
                    <span lang="HI" style="font-size: 12pt; font-family: Mangal; Verdana: 'Times New Roman';
                        mso-fareast-font-family: 'Times New Roman'; mso-hansi-font-family: 'Times New Roman';
                        mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: HI">जिला--&nbsp;<asp:Label ID="lbldepott" runat="server" Text="Depot:">
                <asp:Label ID="lbldepot" runat="server" Text=""></asp:Label></asp:Label>
                        --</span></td>
                <td align="left"> 
                    <span lang="HI" style="font-size: 12pt; font-family: Mangal; Verdana: 'Times New Roman';
                        mso-fareast-font-family: 'Times New Roman'; mso-hansi-font-family: 'Times New Roman';
                        mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: HI">म.
                        प्र.</span></td>
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
    <td style="height: 21px; width: 219px;"> </td>
    <td style="width: 163px; height: 21px"> </td>
    <td style="height: 21px"> </td>
    <td style="height: 21px"> </td>
    
    </tr>
    <tr>
    <td style="width: 219px"> </td>
    <td class = "rgatepass" style="color :Maroon ; width: 163px;"> 
        <span lang="HI" style="font-size: 12pt; color: #000000; font-family: Mangal; Verdana: 'Times New Roman';
            mso-fareast-font-family: 'Times New Roman'; mso-hansi-font-family: 'Times New Roman';
            mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: HI">ट्रक
            चालान </span>
    </td>
    <td> </td>
    <td> </td>
    </tr>
        <tr>
            <td style="width: 219px">
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
    <td style="width: 219px"> </td>
    <td style="width: 163px"> </td>
    <td align ="left"></td>
    <td>
        &nbsp;</td>
    </tr>
    <tr> 
    <td align ="left" style="font-size: 10pt; width: 219px;">
        <p class="MsoNormal" style="margin: 0in 0in 0pt">
            <span lang="HI" style="font-family: Mangal; mso-hansi-font-family: 'Times New Roman';
                mso-bidi-language: HI">बुक नम्बर </span><span style="mso-bidi-language: HI; mso-bidi-font-family: Mangal">
                    <?xml namespace="" ns="urn:schemas-microsoft-com:office:office" prefix="o" ?><o:p></o:p></span></p>
    </td>
    <td align ="left" style="font-size: 10pt; width: 163px" > 
        <strong>:</strong>H/O/A/02-03</td>
    <td style="font-size: 10pt"></td>
    <td style="font-size: 10pt"> </td>
    </tr>
        <tr>
            <td align="left" style="width: 219px">
                <p class="MsoNormal" style="margin: 0in 0in 0pt">
                    <span lang="HI" style="font-family: Mangal; mso-hansi-font-family: 'Times New Roman';
                        mso-bidi-language: HI">रिलीज आर्डर नं.<?xml namespace="" ns="urn:schemas-microsoft-com:office:office"
                            prefix="o" ?><o:p></o:p></span></p>
            </td>
            <td align="left" style="width: 163px">
                <strong>:</strong><asp:Label ID="lblrono" runat="server"></asp:Label></td>
            <td>
                RO.Quantity</td>
            <td>
                <strong>:</strong><asp:Label ID="lblroqty" runat="server"></asp:Label>
                (Qtls)</td>
        </tr>
     <tr> 
    <td align ="left" style="font-size: 10pt; width: 219px;">
        <p class="MsoNormal" style="margin: 0in 0in 0pt">
            <span lang="HI" style="font-family: Mangal; mso-hansi-font-family: 'Times New Roman';
                mso-bidi-language: HI">खरीदी केन्द्र का नाम</span><span style="mso-bidi-language: HI;
                    mso-bidi-font-family: Mangal"><o:p></o:p></span></p>
    </td>
    <td align ="left" style="width: 163px"> 
        <strong>:</strong><asp:Label ID="lblpname" runat="server"></asp:Label></td>
    <td align ="left"> </td>
    <td align ="left">
        &nbsp;</td>
    </tr>
    <tr> 
    <td align ="left" style="font-size: 10pt; width: 219px;"> 
        <p class="MsoNormal" style="margin: 0in 0in 0pt">
            <span lang="HI" style="font-family: Mangal; mso-hansi-font-family: 'Times New Roman';
                mso-bidi-language: HI">संग्रहण केन्द्र का नाम जहाँ माल गया है </span><span style="mso-bidi-language: HI;
                    mso-bidi-font-family: Mangal">
                    <o:p></o:p>
                </span>
        </p>
    </td>
    <td align ="left" style="width: 163px"> 
        <strong>:</strong><asp:Label ID="lblgname" runat="server"></asp:Label></td>
    <td align ="left"> </td>
    <td align ="left">
        &nbsp;</td>
    </tr>
    <tr> 
    <td align ="left" style="font-size: 10pt; width: 219px;"> 
        <p class="MsoNormal" style="margin: 0in 0in 0pt">
            <span lang="HI" style="font-family: Mangal; mso-hansi-font-family: 'Times New Roman';
                mso-bidi-language: HI">माल भेजने की तारीख </span><span style="mso-bidi-language: HI;
                    mso-bidi-font-family: Mangal">
                    <o:p></o:p>
                </span>
        </p>
    </td>
    <td align ="left" style="font-size: 12pt; width: 163px"> 
        <strong>:</strong><asp:Label ID="lblddate" runat="server"></asp:Label></td>
    <td align ="left" style="font-size: 12pt"> </td>
    <td align ="left" style="font-size: 12pt">
        &nbsp;</td>
    </tr>
        <tr style="font-size: 12pt">
            <td align="left" style="font-size: 10pt; width: 219px;">
                <span style="font-family: Mangal">ट्रक का नम्बर जिसमे माल भेजा गया</span></td>
            <td align="left" style="width: 163px">
                <strong>:</strong><asp:Label ID="lbltruckno" runat="server"></asp:Label></td>
            <td align="left">
            </td>
            <td align="left">
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; width: 219px;">
                <p class="MsoNormal" style="margin: 0in 0in 0pt">
                    <span lang="HI" style="font-family: Mangal; mso-hansi-font-family: 'Times New Roman';
                        mso-bidi-language: HI">ट्रांसपोर्टर का नाम
                        <o:p></o:p>
                    </span>
                </p>
            </td>
            <td align="left" style="width: 163px">
                <strong>:</strong><asp:Label ID="lbltransname" runat="server"></asp:Label></td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    <tr style="font-size: 12pt"> 
    <td align ="left" style="font-size: 10pt; width: 219px;"> 
        <span style="font-family: Mangal">माल का विवरण खाद्यान का नाम</span></td>
    <td align ="left" style="width: 163px; font-size: 12pt;"> 
        <strong>:</strong><asp:Label ID="lblcomdtyn" runat="server"></asp:Label></td>
    <td align ="left" style="font-size: 12pt"> </td>
    <td align ="left" style="font-size: 12pt"> </td>
    </tr>
        <tr style="font-size: 12pt">
            <td align="left" style="font-size: 10pt; width: 219px">
                <span style="font-family: Mangal">ग्रेड</span></td>
            <td align="left" style="width: 163px">
                <strong>:</strong><asp:Label ID="lblsch" runat="server"></asp:Label></td>
            <td align="left">
            </td>
            <td align="left">
            </td>
        </tr>
        <tr style="font-size: 12pt">
            <td align="left" style="font-size: 10pt; width: 219px">
                <span style="font-family: Mangal">बोरो की संख्या </span>
            </td>
            <td align="left" style="font-size: 12pt; width: 163px">
                <strong>:</strong><asp:Label ID="lblbagno" runat="server"></asp:Label></td>
            <td align="left" style="font-size: 12pt">
            </td>
            <td align="left" style="font-size: 12pt">
            </td>
        </tr>
        <tr style="font-size: 12pt">
            <td align="left" style="font-size: 10pt; width: 219px">
                <span style="font-family: Mangal">शुद्दृ
                    वजन</span></td>
            <td align="left" style="width: 163px">
                <strong>:</strong><asp:Label ID="lblweight" runat="server"></asp:Label>
                (Qtls.)</td>
            <td align="left">
            </td>
            <td align="left">
            </td>
        </tr>
    <tr> 
    <td style="width: 219px"></td>
    <td style="width: 163px"> </td>
    <td></td>
    <td></td>
    </tr>
    <tr> 
    <td style="width: 219px"></td>
    <td style="width: 163px"> </td>
    <td></td>
    <td> </td>
    </tr>
    <tr> 
    <td style="width: 219px"></td>
    <td style="width: 163px"> </td>
    <td></td>
    <td> </td>
    </tr>
    <tr> 
    <td style="width: 219px"></td>
    <td style="width: 163px"> </td>
    <td></td>
    <td> </td>
    </tr>
    <tr> 
    <td style="width: 219px"></td>
    <td style="width: 163px"> </td>
    <td></td>
    <td> </td>
    </tr>
    <tr> 
    <td style="width: 219px">
        हस्ताक्षर
    </td>
    <td style="width: 163px"> </td>
    <td> </td>
    <td> हस्ताक्षर</td>
    </tr>
    <tr> 
    <td style="width: 219px"> ट्रक ड्राइवर / परिवाहन ठेकेदार </td>
    <td style="width: 163px"> </td>
    <td align="left" colspan="2"> 
        <p class="MsoNormal" style="margin: 0in 0in 0pt">
            <span lang="HI" style="font-family: Mangal; mso-hansi-font-family: 'Times New Roman';
                mso-bidi-language: HI">माल भिजवाने वाले अधिकारी के</span><span style="mso-bidi-language: HI;
                    mso-bidi-font-family: Mangal"><o:p></o:p></span></p>
    </td>
    </tr>
    </table>
    </center >
    </div>
    </form>
</body>
</html>
