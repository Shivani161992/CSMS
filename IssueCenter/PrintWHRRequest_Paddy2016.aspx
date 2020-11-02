<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintWHRRequest_Paddy2016.aspx.cs" Inherits="IssueCenter_PrintWHRRequest_Paddy2016" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <style type="text/css">
        .A4 {
            width: 240mm; /* You willmay need to reduce this to handle printer margins */
            margin: auto; /* This means it will be centred */
            height: 250mm;
            text-align: justify;
            font-size: medium;
        }

        .LineBreak {
            /*page-break-before: always;*/
            height: 0mm;
            overflow: hidden;
        }

        .auto-style1 {
            width: 100%;
            border-style: solid;
            border-width: 2px;
            border-color: black;
        }

        p {
            margin-right: 0in;
            margin-left: 0in;
            font-size: 12.0pt;
            font-family: "Times New Roman","serif";
        }

        @page {
            size: auto; /* auto is the current printer page size */
            margin: 25px 25px 0px 30px; /* this affects the margin in the printer settings */
        }

        .auto-style2 {
            text-decoration: underline;
        }

        .auto-style3 {
            width: 100%;
        }

        .auto-style4 {
            font-size: small;
        }
        </style>

</head>
<body onload="window.print()">
    <form id="form1" runat="server">
        <br />
        <br />
        <div class="A4">
            <table align="center" class="auto-style1">

                <tr>
                    <td rowspan="3" style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0">
                        <asp:Image ID="Image1" runat="server" Height="120px" Width="120px" />
                    </td>
                    <td style="text-align: center" class="auto-style3">
                        <asp:Label ID="lblMFD" runat="server" Text="मध्य प्रदेश वेयर हाउसिंग एंड लोजिस्टिक कार्पोरेशन" Style="font-size: medium; font-weight: 700;"></asp:Label>
                    </td>
                    <td rowspan="3" style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0">
                        <asp:Image ID="ImgQRCode" runat="server" Height="125px" Width="125px" />
                    </td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3">District ....<asp:Label ID="lbldist" runat="server" Font-Bold="True" Style="font-size: medium"></asp:Label>....<asp:Label ID="lblCropYear" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3"><strong>&lt;&lt;<span class="auto-style2">&nbsp; माल जमा (डिपोजिट) करने का प्रार्थना पत्र </span>&gt;&gt;</strong></td>

                </tr>
            </table>
            <table align="center" class="auto-style1">

                <tr>
                    <td style="text-align: center" class="auto-style7" colspan="2">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: left">1) डिपोजिट फॉर्म क्रमांक- ....<asp:Label ID="lbl_whrrequest" runat="server" Font-Bold="True" Style="font-size: medium"></asp:Label>....</td>
                    <td style="text-align: right">जमा दिनांक- ....<asp:Label ID="lbldepositdate" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left">2) प्रदाय केंद्र- ....<asp:Label ID="lblissCen" runat="server" Font-Bold="True" Style="font-size: medium"></asp:Label>....</td>
                    <td style="text-align: right">गोदाम- ....<asp:Label ID="lblggdn" runat="server" Font-Bold="True"></asp:Label>....</td>

                </tr>





            </table>

            <table align="center" class="auto-style1">

                <tr>
                    <td style="text-align: left">प्रति,</td>
                </tr>

                <tr>
                    <td style="text-align: left;padding-left:25px;">शाखा प्रबंधक,</td>
                </tr>

                <tr>
                    <td style="text-align: left;padding-left:25px;">वेयर हाउसिंग कार्पोरेशन,</td>
                </tr>

                <tr>
                    <td style="text-align: left;padding-left:25px;">शाखा</td>
                </tr>

                <tr>
                    <td style="text-align: left;padding-left:25px;">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: left;">महोदय,</td>
                </tr>

                <tr>
                    <td style="text-align: left;padding-left:25px;">कृपया नीचे लिखे अनुसार स्कंध को अपने वेयर हाउस के गोदाम
                        <asp:Label ID="lblgodown" runat="server"></asp:Label>&nbsp;
                            में रखने के लिए स्वीकार करेंगे |</td>
                </tr>

                <tr>
                    <td style="text-align: left;padding-left:25px;">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: left">
                        <table class="style13" style="border: 2px solid black; padding: inherit; margin: auto;">
                            <tr">
                                <td  style="border: 1px solid black;  text-align: center; ">कमोडिटी</td>
                                <td style="border: 1px solid black;  text-align: center;">जमा किये गए कुल ट्रकों की संख्या</td>
                                <td style="border: 1px solid black;  text-align: center;">कुल जमा
                                        मात्रा <strong>(Qtls)</strong>                                   
                                 </td>
                                <td style="border: 1px solid black;  text-align: center;">कुल प्राप्त बोरो की संख्या</td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid black; border-top:0px;  text-align: center;">
                                    <asp:Label ID="lblcommodity" runat="server"></asp:Label>
                                </td>
                                <td style="border: 1px solid black; border-top:0px;  text-align: right;">
                                    <asp:Label ID="lbltruckcount" runat="server"></asp:Label>
                                </td>
                                <td style="border: 1px solid black; border-top:0px;   text-align: right;">
                                    <asp:Label ID="lblweight" runat="server"></asp:Label>
                                </td>
                                <td style="border: 1px solid black; border-top:0px;  text-align: right;">
                                    <asp:Label ID="lblbagscount" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>

                    </td>
                </tr>

                <tr>
                    <td style="text-align: left">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: left">मैं यह प्रमाणित करता हू की उपरोक्त बतलाया गया स्कंध 
                        <asp:Label ID="lblMFD1" runat="server" Text=""></asp:Label> जिला कार्यालय
                            <asp:Label ID="lbldistrict" runat="server"></asp:Label> का है और उस पर किसी दूसरे व्यक्ति या संस्था का अधिकार नहीं है|</td>
                </tr>

                <tr>
                    <td style="text-align: left">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: left">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: right">हस्ताक्षर</td>
                </tr>

                <tr>
                    <td style="text-align: right">जमाकर्ता प्रदाय केंद्र प्रभारी</td>
                </tr>

                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="lblscsc" runat="server" Text="मध्य प्रदेश स्टेट सिविल सप्लाईज कार्पोरेशन लिमिटेड" Visible="false"></asp:Label>
                        <asp:Label ID="lblmarkfed" runat="server" Text="मध्य प्रदेश राज्य सहकारी विपणन संघ लिमिटेड" Visible="false"></asp:Label>

                    </td>
                </tr>

                <tr>
                    <td style="text-align: right">
                         प्रदाय केंद्र
                            :
                            <asp:Label ID="lblissuecenter" runat="server" Font-Bold="True"></asp:Label></td>
                </tr>

                <tr>
                    <td style="text-align: right; padding-right:300px;">
                        नाम :</td>
                </tr>
                <tr>
                    <td style="text-align: right; padding-right:300px;">
                        मोबाइल क्रमांक :</td>
                </tr>

            </table>

            <table align="center" class="auto-style1">

                <tr>
                    <td style="text-align: left">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: center">पावती</td>
                </tr>

                <tr>
                    <td style="text-align: left">डिपोजिट फॉर्म क्रमांक
                        <asp:Label ID="lblwhrReq" runat="server" Font-Bold="True"></asp:Label>&nbsp;में उल्लेखित अनुसार स्कंध दिनांक&nbsp;<asp:Label ID="lblpavtidate" runat="server" Font-Bold="True"></asp:Label>&nbsp;को 
                            प्राप्तकर गोदाम क्रमांक  <asp:Label ID="lblpavtigdn" 
                                runat="server"></asp:Label>
                            &nbsp;में जमा किया एवं WHR क्रमांक ____________________ जारी किया |</td>
                </tr>

                <tr>
                    <td style="text-align: right">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: right">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: right">शाखा प्रबंधक</td>
                </tr>

            </table>

            <table>

                <table align="center" width="100%">

                    <tr class="auto-style4">
                        <td style="text-align: left">
                            <asp:Label ID="lblCurrentDateTime" runat="server" Font-Bold="True"></asp:Label></td>
                    </tr>

                </table>
        </div>
    </form>
</body>
</html>
