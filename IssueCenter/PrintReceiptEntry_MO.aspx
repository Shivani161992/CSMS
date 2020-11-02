<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintReceiptEntry_MO.aspx.cs" Inherits="IssueCenter_PrintReceiptEntry_MO" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style type="text/css">
        .A4 {
            width: 210mm; /* You willmay need to reduce this to handle printer margins */
            margin: auto; /* This means it will be centred */
            height: 250mm;
            text-align: justify;
            font-size: medium;
        }

        .LineBreak {
            page-break-before: always;
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

        .auto-style3 {
            width: 100%;
        }

        .auto-style4 {
            width: 100%;
            border-style: solid;
            border-width: 1px;
        }
    </style>

</head>
<body onload="window.print()">

    <form id="form1" runat="server">
        <div class="A4">
            <br />
            <table align="center" class="auto-style1">
                <tr>
                    <td rowspan="3" style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/mpscsc_logo.jpg" Height="110" Width="110px" />
                    </td>
                    <td style="text-align: center" class="auto-style3">मध्य प्रदेश स्टेट सिविल सप्लाईज कार्पोरेशन लिमिटेड</td>
                    <td rowspan="3" style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0">
                        <asp:Image ID="ImgQRCode" runat="server" Height="110px" Width="110px" />
                    </td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3">जिला कार्यालय : ....<asp:Label ID="lblDistName" runat="server" Text="" Style="font-weight: 700"></asp:Label>.... प्रदाय केंद्र : ....<asp:Label ID="lblICName" runat="server" Text="" Style="font-weight: 700"></asp:Label>....

                        <input id="hdfFrmDist" type="hidden" runat="server" />
                        <input id="hdfToDist" type="hidden" runat="server" />
                    </td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3"><strong>&lt;&lt; स्कंध प्राप्ति की पावती &gt;&gt;
                    </strong></td>

                </tr>

            </table>

            <table align="center" class="auto-style1">
                <tr>
                    <td style="text-align: left">पावती क्रमांक : ....<asp:Label ID="lblReceiptID" runat="server" Text=""></asp:Label>....</td>
                    <td style="text-align: right">दिनांक : ....<asp:Label ID="lblDate" runat="server" Text=""></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left"></td>
                    <td style="text-align: right"></td>
                </tr>

            </table>
            <table align="center" class="auto-style1">

                <tr>
                    <td style="text-align: left; font-weight: 700;">&nbsp;</td>
                </tr>


                <tr>
                    <td style="text-align: left; font-weight: 700;">स्कंध प्रेषण विवरण :</td>
                </tr>


                <tr>
                    <td style="text-align: left;">

                        <table align="center" class="auto-style4" border="1" cellspacing="0" cellpadding="0" style="text-align: left">
                            <tr>
                                <td>मुख्यालय मूवमेंट आर्डर
                                    <br />
                                    क्रमांक</td>
                                <td>
                                    <asp:Label ID="lblMONo" runat="server" Text=""></asp:Label>
                                </td>
                                <td>जिला कार्यालय परिवहन
                                    <br />
                                    आदेश क्रमांक</td>
                                <td>
                                    <asp:Label ID="lblTranspNo" runat="server" Text=""></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td>डिलेवरी चालान क्रमांक</td>
                                <td>
                                    <asp:Label ID="lblDCNO" runat="server" Text=""></asp:Label>
                                </td>
                                <td>कमोडिटी</td>
                                <td>
                                    <asp:Label ID="lblComdty" runat="server" Text=""></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td>ट्रक नंबर</td>
                                <td>
                                    <asp:Label ID="lblSendingTCNO" runat="server" Text=""></asp:Label>
                                </td>
                                <td>परिवहन का दिनांक</td>
                                <td>
                                    <asp:Label ID="lblTranspDate" runat="server" Text=""></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td>प्रेषणकर्ता जिला</td>
                                <td>
                                    <asp:Label ID="lblSendingDist" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblBookNo" runat="server" Text="प्रेषणकर्ता प्रदाय केंद्र"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblSendingIC" runat="server" Text=""></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td><asp:Label ID="lblPageNo" runat="server" Text="प्रेषणकर्ता गोदाम"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblSendingGodown" runat="server" Text=""></asp:Label>
                                </td>
                                <td>बोरे का प्रकार</td>
                                <td>
                                    <asp:Label ID="lblSendingBagType" runat="server" Text=""></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td>बोरों की संख्या</td>
                                <td>
                                    <asp:Label ID="lblSendingBags" runat="server" Text=""></asp:Label>
                                </td>
                                <td>प्रेषित मात्रा<asp:Label ID="lblQtls" runat="server" style="font-weight: 700" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblSendingQty" runat="server" Text=""></asp:Label>
                                </td>

                            </tr>

                        </table>

                    </td>
                </tr>


            </table>
            <table align="center" class="auto-style1">

                <tr>
                    <td style="text-align: left; font-weight: 700;">&nbsp;</td>
                </tr>


                <tr>
                    <td style="text-align: left; font-weight: 700;">स्कंध प्राप्ति विवरण :</td>
                </tr>


                <tr>
                    <td style="text-align: left;">

                        <table align="center" class="auto-style4" border="1" cellspacing="0" cellpadding="0" style="text-align: left">
                            <tr>
                                <td>प्राप्ति दिनांक</td>
                                <td>
                                    <asp:Label ID="lblRecdDate" runat="server" Text=""></asp:Label>
                                </td>
                                <td>तौल पर्ची क्रमांक</td>
                                <td>
                                    <asp:Label ID="lblToulReceipt" runat="server" Text=""></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td>प्राप्त बोरों की संख्या</td>
                                <td>
                                    <asp:Label ID="lblRecdBags" runat="server" Text=""></asp:Label>
                                </td>
                                <td>प्राप्त मात्रा<asp:Label ID="lblQtls0" runat="server" style="font-weight: 700" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblRecdQty" runat="server" Text=""></asp:Label>
                                </td>

                            </tr>

                            <tr>
                                <td>प्राप्त बोरों की संख्या में कमी</td>
                                <td>
                                    <asp:Label ID="lblRecdVarBags" runat="server" Text=""></asp:Label>
                                </td>
                                <td>प्राप्त मात्रा में कमी<asp:Label ID="lblQtls1" runat="server" style="font-weight: 700" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblRecdVarQty" runat="server" Text=""></asp:Label>
                                </td>

                            </tr>

                            <tr>
                                <td>गोदाम</td>
                                <td>
                                    <asp:Label ID="lblRecdGodam" runat="server" Text=""></asp:Label>
                                </td>
                                <td>ट्रक नंबर</td>
                                <td>
                                    <asp:Label ID="lblRecdTruckNo" runat="server" Text=""></asp:Label>
                                </td>

                            </tr>

                        </table>
                    </td>
                </tr>

                <table class="auto-style1">
                    <tr>
                        <td style="text-align: left">
                            <asp:HiddenField ID="hdfRecChange_Godown" runat="server" />
                        </td>
                        <td style="text-align: right">
                            <asp:HiddenField ID="hdfRecDefault_Godown" runat="server" />
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: left">उपरोक्तानुसार स्कंध जमा किया |</td>
                        <td style="text-align: right">उपरोक्तानुसार स्कंध प्राप्त किया |</td>
                    </tr>

                    <tr>
                        <td style="text-align: left">&nbsp;</td>
                        <td style="text-align: right">&nbsp;</td>
                    </tr>

                    <tr>
                        <td style="text-align: left">&nbsp;</td>
                        <td style="text-align: right">&nbsp;</td>
                    </tr>

                    <tr>
                        <td style="text-align: left">(हस्ताक्षर)</td>
                        <td style="text-align: right">(हस्ताक्षर)</td>
                    </tr>

                    <tr>
                        <td style="text-align: left">(परिवहनकर्ता / प्रतिनिधि)</td>
                        <td style="text-align: right">(प्रदाय केंद्र प्रभारी)</td>
                    </tr>

                    <tr>
                        <td style="text-align: left">नाम :</td>
                        <td style="text-align: left">
                            <span style="margin-left: 300px">नाम : </span>
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: left">मोबाइल क्रमांक :</td>
                        <td style="text-align: left">
                            <span style="margin-left: 300px">मोबाइल क्रमांक : </span>

                        </td>
                    </tr>

                </table>

            </table>
            <table align="center" width="100%">

                <tr>
                    <td style="text-align: left">
                        <asp:Label ID="lblCurrentDateTime" runat="server" Font-Bold="True"></asp:Label></td>

                </tr>

            </table>
        </div>
    </form>
</body>
</html>
