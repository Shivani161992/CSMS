<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PMillingAgreement.aspx.cs" Inherits="PaddyMilling_MillingAgreement" %>
<%@ PreviousPageType VirtualPath="~/PaddyMilling/MillingAgreement.aspx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Paddy Milling Agreement</title>
    
        <style type="text/css">
            .A4 {
                width: 210mm; /* You willmay need to reduce this to handle printer margins */
                margin: auto; /* This means it will be centred */
                height: 250mm;
                text-align: justify;
                font-size: large;
            }

            .LineBreak {
                /*page-break-before: always;*/
                height: 0mm;
                overflow: hidden;
            }

            .auto-style5 {
                width: 100%;
            }

            p {
                margin-right: 0in;
                margin-left: 0in;
                font-size: 12.0pt;
                font-family: "Times New Roman","serif";
            }

            .auto-style6 {
                font-weight: bold;
                font-size: large;
            }

            @page {
                size: auto; /* auto is the current printer page size */
                margin: 30px 25px 0px 25px; /* this affects the margin in the printer settings */
            }
        </style>


</head>
<body onload="window.print()">
    <form id="form1" runat="server">

         <div class="A4">
            
             <table align="center" class="auto-style5">
                 <tr>
                     <td >
                     <pre>




























                     </pre>
                     </td>
                 </tr>
                 <tr>
                     <td style="text-align: center">
                         <p align="center" style="text-align:center">
                             <u><span class="auto-style6" lang="HI" style="font-family: &quot;Mangal&quot;,&quot;serif&quot;; mso-ascii-font-family: &quot;Times New Roman&quot;; mso-hansi-font-family: &quot;Times New Roman&quot;">धान कस्टम मिलिंग के लिए अनुबंध</span><span style="font-size:18.0pt"><o:p></o:p></span></u></p>
                     </td>
                 </tr>

              <tr>
            <td style="font-size: medium">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;आज दिनांक .... 
                <asp:Label ID="lblDate" runat="server" Font-Bold="True"></asp:Label>&nbsp;.... 
को यह अनुबंध पत्र म० प्र० स्टेट सिविल सप्लाइज कारपोरेशन लिमिटेड (कारपोरेशन) की ओर से श्री/श्रीमती .... 
                <asp:Label ID="lblDistManagerName" runat="server" Font-Bold="True"></asp:Label>&nbsp;.... 
जिला प्रबंधक .... 
                <asp:Label ID="lblDistManager" runat="server" Font-Bold="True"></asp:Label>&nbsp;.... 
जिसे आगे चलकर कारपोरेशन कहा गया है तथा मेसर्स.... 
                <asp:Label ID="lblMasersName" runat="server" Font-Bold="True"></asp:Label>&nbsp;.... 
पता .... 
                <asp:Label ID="lblMacersAddDist" runat="server" Font-Bold="True" ></asp:Label>&nbsp;....
तहसील .... 
                <asp:Label ID="lblMacersAddDivision" runat="server" Font-Bold="True"></asp:Label>&nbsp;....
की ओर से श्री .... 
                <asp:Label ID="lblOwnerName" runat="server" Font-Bold="True" ></asp:Label>&nbsp;....
                <asp:Label ID="lblOwner" runat="server" Font-Bold="True"></asp:Label>&nbsp;....
जिसे आगे चलकर मिलर कहा गया है के मध्य धान मिलिंग कार्य के लिए निम्न शर्तो पर निष्पादित किया जाता है :-
            </td>
          </tr>

                  <tr>
            <td style="font-size: medium">
<b>1.&nbsp;</b>&nbsp;
यह की कारपोरेशन राजस्व जिला .... 
                <asp:Label ID="lblCorporationJila" runat="server" Font-Bold="True" ></asp:Label>&nbsp;....
में खरीफ विपणन वर्ष .... 
                <asp:Label ID="lblYear" runat="server" Font-Bold="True"></asp:Label>&nbsp;....
में समर्थन मूल्य के अंतर्गत क्रय की गई धान की अरवा मिलिंग राज्य शासन की उपार्जन निति अंतर्गत कारकर चावल में रूपांतरित कराना चाहता है इसके लिए मिलर मेसर्स .... 
                <asp:Label ID="lblMasersName1" runat="server" Font-Bold="True" ></asp:Label>&nbsp;....
ने स्थान ....
                <asp:Label ID="lblMacersAddDist1" runat="server" Font-Bold="True"></asp:Label>&nbsp;....
तहसील ....
                <asp:Label ID="lblMacersAddDivision1" runat="server" Font-Bold="True"></asp:Label>&nbsp;....
में स्थित अपने स्वामित्व की राइस मिल में खरीफ वर्ष.... 
                <asp:Label ID="lblYear2" runat="server" Font-Bold="True"></asp:Label>&nbsp;....
में उपार्जित एवं मिलिंग हेतु दी जाने वाली धान की अरवा मिलिंग भारत सरकार द्वारा निर्धारित गुणवत्ता मापदंडों तथा खरीफ विपणन वर्ष .... 
                <asp:Label ID="lblYear3" runat="server" Font-Bold="True"></asp:Label>&nbsp;....
के लिये जारी सी0 एम0 आर0 आदेश की शर्तो के अनुसार मिलिंग कर निर्मित चावल का परिदान म0 प्र0 स्टेट सिविल सप्लाइज लिमिटेड / भारतीय खाद्य निगम को करने के लिए सहमति प्रदान की है|
            </td>
          </tr>
                 
             </table>


             </div>

<div class="LineBreak"></div>
<div class="A4">

    <table align="center" class="auto-style5">
        <tr>
            <td style="font-size: medium">
<b>1.1</b>&nbsp;&nbsp;
कारपोरेशन द्वारा मिलर को भारत सरकार से निर्धारित मिलिंग दर के अतिरिक्त राज्य शासन द्वारा अरवा कस्टम मिलिंग हेतु .... 
                <asp:Label ID="lblArva" runat="server" Font-Bold="True"></asp:Label>&nbsp;....
&nbsp;रु० प्रति क्विं० तथा उष्णा कस्टम मिलिंग हेतु प्रथम 3 माह में मिलिंग कर भारतीय खाद्य निगम में जमा करने पर .... 
                <asp:Label ID="lblUshanF3" runat="server" Font-Bold="True"></asp:Label>&nbsp;....
&nbsp;रु० प्रति क्विं० तथा 3 माह उपरांत जमा करने पर राशि .... 
                <asp:Label ID="lblUshnaA3" runat="server" Font-Bold="True"></asp:Label>&nbsp;....
&nbsp;रु० प्रति क्विं० प्रोत्साहन राशि प्रदान की जावेगी| यह दर धान का परिवहन व्यय तथा मिलिंग उपरांत निर्मित सीएमआर चावल का मिल पॉइंट से चावल संग्रहण डिपो तक प्रथम 08 कि0 मी0 तक के परिवहन व्यय सहित है|
            </td>
        </tr>

         <br/>

        <tr>
            <td style="font-size: medium">
<b>1.2</b>&nbsp;&nbsp;
कारपोरेशन द्वारा मिलर को भुगतान योग्य राशि में से नियमानुसार आयकर एवं अन्य प्रचलित करों, कटौतियों आदि कि वसूली उपरांत ही भुगतान किया जावेगा |
            </td>
        </tr>
         <br/>

      <tr>
            <td style="font-size: medium">
<b>2.&nbsp;</b>&nbsp;
मिलर दिनांक .... <asp:Label ID="lblFromDate" runat="server" Font-Bold="True" ></asp:Label>&nbsp;....
से दिनांक .... <asp:Label ID="lblToDate" runat="server" Font-Bold="True"></asp:Label>&nbsp;....
तक की अनुबंधित अवधि में उपरोकतानुसार .... 
                <asp:Label ID="lblCommonDhan" runat="server" Font-Bold="True"></asp:Label>&nbsp;....
मै0 टन कामन धान तथा .... 
                <asp:Label ID="lblGradeADhan" runat="server" Font-Bold="True" ></asp:Label>&nbsp;....
मै0 टन ग्रेड-ए धान अर्थात् कुल .... 
                <asp:Label ID="lblTotalDhan" runat="server" Font-Bold="True"></asp:Label>&nbsp;....
मै0 टन की मिलिंग कर अरवा चावल में रूपांतरण करने के लिये प्रतिबद्ध है यह मात्रा मिलर की मिलिंग क्षमता देखकर निर्धारित की गई है|
                </td>
          </tr>

          <br/>

      <tr>
            <td style="font-size: medium">

<b>2.2</b>&nbsp;&nbsp;
मिलर द्वारा निविदा के साथ जमा की गई अमानत राशि रु० .... 
                <asp:Label ID="lblDepositMoney" runat="server" Font-Bold="True"></asp:Label>&nbsp;....
जमा करायी जायेगी| जो मिलर को निष्पादित अनुबंध के तहत समस्त कार्य संतोषप्रद रूप से पूर्ण हो जाने, हिसाब प्रस्तुत करने, लेखो के मिलान के उपरान्त एवं कारपोरेशन को कोई वसूली बाकी नहीं रहने पर किया जावेगा |

                  </td>
          </tr>

        <br/>

      <tr>
            <td style="font-size: medium">

<b>2.3 </b>&nbsp;&nbsp;
अमानत राशि पर कारपोरेशन द्वारा कोई ब्याज नही दिया जाएगा |
            </td>
          </tr>

          <br/>
      <tr>
            <td style="font-size: medium">

<b>3&nbsp;</b>&nbsp;&nbsp;
मिलर को अनुबंधित मात्रा की मिलिंग, खरीफ विपणन वर्ष 2013-14 में भारत सरकार द्वारा निर्धारित चावलगुणवत्ता मापदंडो (संलग्न परिशिष्ट-अ जो अनुबंध का भाग है ) के अनुसार मिलिंग कर सीएमआर चावल का परिदान म० प्र० स्टेट सिविल सप्लाइज कारपोरेशन के निर्धारित गोदाम में निर्धारित अवधि अंतर्गत देना होगा,जिस हेतु मिलर सहमत है|
            </td>
          </tr>

            <br/>

      <tr>
            <td style="font-size: medium">

<b>3.1</b>&nbsp;&nbsp;
अनुबंध अवधि के प्रत्येक एक तिहाई भाग में समनुपातिक रूप से अनुबंध मात्रा के एक तिहाई भाग की धान की मिलिंग कर मिलर को चावल का परिदान म० प्र० स्टेट सिविल सप्लाइज कारपोरेशन को करना होगा |
                      </td>
          </tr>

              <tr>
            <td style="font-size: medium">

<b>4.</b>&nbsp;
मिलर को अनुबंध में दी गई समय-सीमा में यथा क्षमता अनुसार अनुपातिक धान की शीघ्र मिलिंग कर, निर्मित सी० एम० आर० चावल कारपोरेशन के पक्ष में म. प्र. सिविल सप्लाइज कारपोरेशन के निर्दिष्ट गोदाम में जमा करना होगा |</td>
          </tr>

    </table>
</div>

<%--<div class="LineBreak"></div>
<div class="A4">Put the second copy of the receipt here.</div>--%>

    </form>
      
</body>
</html>
