<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_DeliveryChallan_IssuedCentre.aspx.cs" Inherits="IssueCenter_Print_DeliveryChallan_IssuedCentre" %>

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
                    <td style="text-align: center" class="auto-style3">जिला कार्यालय : ....<asp:Label ID="lblDistName" runat="server" Text="" Style="font-weight: 700"></asp:Label>.... प्रदाय केंद्र : ....<asp:Label ID="lblICName" runat="server" Text="" Style="font-weight: 700"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3"><strong>&lt;&lt; डिलेवरी चालान (Issue Center)&gt;&gt;
                    </strong></td>

                </tr>

            </table>

            <table align="center" class="auto-style1">
                <tr>
                    <td style="text-align: left">डिलेवरी चालान (Issue Center) क्रमांक : ....<asp:Label ID="lblDONo" runat="server" Text=""></asp:Label>....</td>
                    <td style="text-align: right">दिनांक : ....<asp:Label ID="lblDate" runat="server" Text=""></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left">
                        <input id="hdfDC_MO" type="hidden" runat="server" />
                        <input id="hdfDefault_Branch" type="hidden" runat="server" />
                        <input id="hdfDefault_Godown" type="hidden" runat="server" />
                        <input id="hdfCreatedDate" type="hidden" runat="server" />
                        <input id="hdfChange_Branch" type="hidden" runat="server" />
                        <input id="hdfChange_Godown" type="hidden" runat="server" />
                        <input id="hdfRack_No" type="hidden" runat="server" />
                    </td>
                    <td style="text-align: right">
                
                    </td>
                </tr>

                <tr>
                    <td style="text-align: left">संदर्भ :-</td>
                    <td style="text-align: right">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: justify" colspan="2">&nbsp;&nbsp;१. MOVEMENT ORDER क्रमांक ....<asp:Label ID="lblMovmtNo" runat="server" Text=""></asp:Label>.... दिनांक ....<asp:Label ID="lblMODate" runat="server"></asp:Label>....</td>
                </tr>

              

                <tr>
                    <td style="text-align: left" colspan="2">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: justify" colspan="2">&nbsp; संदर्भित आदेश&nbsp;के अनुक्रम में निम्नानुसार स्कंध, .... गोदाम ....<asp:Label ID="lblGodown" runat="server" Text=""></asp:Label>.... से जारी किया जा रहा है |</td>
                </tr>



                <tr>
                    <td style="text-align: justify" colspan="2">&nbsp;</td>
                </tr>



            </table>

            <table align="center" class="auto-style1">



                <tr>
                    <td>
                        <table align="center" class="auto-style4" border="1" cellspacing="0" cellpadding="0" style="text-align: center">
                            <tr>
                                <td><b>क्र.</b></td>
                                <td><b>कमोडिटी का नाम</b></td>
                                <td><b>परिवहन आदेश की मात्रा(Qtls)</b></td>
                                <td><b>प्रदायित मात्रा(Qtls)</b></td>
                                <td><b>बोरे की संख्या(नग)</b></td>
                                <td><b>बोरे का प्रकार</b></td>
                                <td><b>ट्रक न.</b></td>
                            </tr>
                            <tr>
                                <td>1.</td>
                                <td>
                                    <asp:Label ID="lblComdty" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblTotalQty" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblIssuedQty" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblBags" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblBagType" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblTruckNo" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>

                </tr>
            </table>

            <table align="center" class="auto-style1">

                <tr>
                    <td style="text-align: justify" colspan="2">&nbsp;</td>

                </tr>

                <tr>
                    <td runat="server" id="RowRoad" style="text-align: justify" colspan="2">&nbsp; उक्त स्कंध जिला कार्यालय ....<asp:Label ID="lblToDistName" runat="server" Text=""></asp:Label>.... के प्रदाय केंद्र ....<asp:Label ID="lblToIC" runat="server" Text=""></asp:Label> पर जमा किया जाना है |</td>

                </tr>

               <%-- <tr>
                    <td style="text-align: justify" colspan="2">&nbsp; प्रमाणित किया जाता है कि उपरोक्त स्कंध कारपोरेशन के जिला कार्यालय ....<asp:Label ID="lblDistName1" runat="server" Text=""></asp:Label>.... का है एवं जिला/रैक पाइंट ....<asp:Label ID="lblTo" runat="server" Text=""></asp:Label>.... में संग्रहण/वितरण हेतु प्रेषित किया जा रहा है |</td>

                </tr>--%>

                <tr>
                    <td style="text-align: center">&nbsp;</td>

                    <td style="text-align: center">&nbsp;</td>

                </tr>
                <tr>
                    <td style="text-align: right" colspan="2">हस्ताक्षर</td>
                </tr>
                <tr>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: right">(प्रदाय केंद्र प्रभारी)</td>
                </tr>
                <tr>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: left;">
                        <span style="margin-left: 500px">नाम : </span></td>
                </tr>
                <tr>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: left">
                        <span style="margin-left: 500px">मोबाइल क्रमांक : </span>
                    </td>
                </tr>

                <tr>
                    <td style="text-align: left; font-weight: 700;" colspan="2">--------------------------------------------------------------------------------------------------------------------------------------------------</td>
                </tr>

                <tr>
                    <td style="text-align: center; font-weight: 700;" colspan="2">&lt;&lt; पावती &gt;&gt;</td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="2">&nbsp; उपरोक्त विवरण अनुसार स्कंध प्राप्त किया |</td>
                </tr>
                <tr>
                    <td style="text-align: right" colspan="2">हस्ताक्षर</td>
                </tr>
                <tr>
                    <td style="text-align: right" colspan="2">(परिवहनकर्ता के प्रतिनिधि)</td>
                </tr>
                <tr>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: left;">
                        <span style="margin-left: 500px">नाम : </span></td>
                </tr>
                <tr>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: left">
                        <span style="margin-left: 500px">मोबाइल क्रमांक : </span>
                    </td>
                </tr>
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
