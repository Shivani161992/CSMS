<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReprintCMRRejection.aspx.cs" Inherits="IssueCenter_ReprintCMRRejection" %>

<%@ PreviousPageType VirtualPath="~/IssueCenter/Receipt_Entry_CMR.aspx" %>


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
            font-size: large;
        }

        .LineBreak {
            /*page-break-before: always;*/
            height: 0mm;
            overflow: hidden;
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
            /*margin: 25px 25px 0px 30px;*/
        }

        .auto-style1 {
            width: 100%;
            border-style: solid;
            border-width: 1px;
            border-color: black;
        }

        .auto-style2 {
            width: 100%;
            border-style: solid;
            border-width: 1px;
            border-color: black;
        }

        .auto-style3 {
            font-weight: bold;
        }

        .auto-style4 {
            height: 24px;
        }

        .auto-style5 {
            font-size: medium;
        }

        .auto-style8 {
            font-weight: normal;
        }
    </style>

</head>
<body onload="window.print()">
    <form id="form1" runat="server">
        <div class="A4">

            <table align="center" style="width: 100%">
                <tr>
                    <td>
                        
                    </td>
                    <td style="text-align: right">
                        <input type="button" value="Print" onclick="window.print() " /></td>
                </tr>
            </table>

            <table align="center" class="auto-style1">
                <tr style="text-align: center">
                    <td><b>MP State Civil Supplies Corporation Limited</b></td>
                </tr>
                <tr style="text-align: center">
                    <td class="auto-style3">&lt;&lt; <span class="auto-style8">CMR Rejection Note</span> &gt;&gt;</td>
                </tr>
            </table>

            <table align="center" class="auto-style1">

                <tr>
                    <td>District - ....<asp:Label ID="lblDistrict" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="text-align: right">Issue Center - ....<asp:Label ID="lblIC" runat="server" Font-Bold="True"></asp:Label>....</td>

                </tr>
            </table>

            <table align="center" class="auto-style1">
                <tr>
                    <td class="auto-style4">Statement of CMR Rejected at Godown : Godown No - ....<asp:Label ID="lblGodownNo" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>
            </table>
            <table align="center" class="auto-style1">
                <tr>
                    <td>Miller Name - ....<asp:Label ID="lblMillerName" runat="server" Font-Bold="True"></asp:Label>.... </td>
                    
                </tr>
                <tr>
                    <td>
                        Miller State&nbsp; -&nbsp;....<asp:Label ID="lblMillerState" runat="server" Font-Bold="True"></asp:Label>....
                    </td>
                    <td style="text-align:right">
                        Miller District - ....<asp:Label ID="lblMillerDistrict" runat="server" Font-Bold="True"></asp:Label>....
                    </td>
                </tr>
            </table>
            <table align="center" class="auto-style1">
                <tr>
                    <td>Rejected Number - ....<asp:Label ID="lblAcptNo" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="text-align: right">Commodity - ....<asp:Label ID="lblComdty" runat="server" Font-Bold="True">RICE</asp:Label>....&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>

                </tr>
                <tr>
                    <td>Rejected Date -&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ....<asp:Label ID="lblAcptDate" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="text-align: right">&nbsp;CropYear - ....<asp:Label ID="lblYear" runat="server" Font-Bold="True"></asp:Label>....</td>

                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="text-align: right">Quantity in Qtls.Kg.gms</td>

                </tr>
                <tr>
                    <td colspan="4">
                        <table align="center" class="auto-style2" cellspacing="0" cellpadding="0" border="1">
                            <tr style="text-align: center; font-weight: 700;">
                                <td>Lot Number</td>
                                <td>Do Number</td>
                                <td>Truck Number</td>
                                <td>Rejected Qty (Common)</td>
                                <td>Rejected Qty (Grade-A)</td>
                            </tr>
                            <tr style="text-align: center">
                                <td>
                                    <asp:Label ID="lblLotNo" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDoNo" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblTCNo" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblRejectCommon" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblRejectGA" runat="server"></asp:Label>
                                </td>
                            </tr>

                        </table>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>

            <table align="center" class="auto-style1">
                <tr>
                    <td>

                        <table align="center" border="1" style="border-spacing: 1px; text-align: center; width: 100%;">
                            <tr>
                                <td class="auto-style5">क्रम सं.</td>
                                <td colspan="2" class="auto-style5">अपवर्तन</td>

                                <td><span class="auto-style5">अधिकतम सीमा (प्रतिशत)<br />
                                </span>
                                    <b><span class="auto-style5">ग्रेड-ए</span></b><br />
                                </td>
                                <td class="auto-style5">गुणवत्ता परिक्षण उपरांत परिणाम<br />
                                    <b>ग्रेड-ए</b></td>
                                <td class="auto-style5">अधिकतम सीमा (प्रतिशत)<br />
                                    <b>कामन</b></td>
                                <td class="auto-style5">गुणवत्ता परिक्षण उपरांत परिणाम<br />
                                    <b>कामन</b></td>
                                <td class="auto-style5">रिमार्क</td>

                            </tr>
                            <tr>
                                <td rowspan="2">1.</td>
                                <td style="text-align: left;"><span class="auto-style5">टोटा</span><br />
                                </td>
                                <td>
                                    <asp:Label ID="LblMType" runat="server" Style="font-size: medium"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblTotaGA" runat="server"></asp:Label>
                                    <br />
                                </td>
                                <td>
                                    <asp:Label ID="LblTotaGAR" runat="server" CssClass="auto-style3"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblTotaS" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblTotaSR" runat="server" CssClass="auto-style3"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblTotaGARmk" runat="server" CssClass="auto-style5"></asp:Label>
                                </td>

                            </tr>

                            <tr>

                                <td style="text-align: left" class="auto-style5">छोटे टोटे</td>
                                <td>
                                    <asp:Label ID="LblMType0" runat="server" CssClass="auto-style5"></asp:Label>
                                </td>
                                <td>&nbsp;<asp:Label ID="LblChoteToteGA" runat="server"></asp:Label>
                                    &nbsp;</td>
                                <td>
                                    <asp:Label ID="LblChoteToteGAR" runat="server" CssClass="auto-style3"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblChoteToteS" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblChoteToteSR" runat="server" CssClass="auto-style3"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblChoteToteGARmk" runat="server" CssClass="auto-style5"></asp:Label>
                                </td>

                            </tr>

                            <tr>
                                <td>2.</td>
                                <td style="text-align: left;" class="auto-style5">विजातीय तत्व</td>
                                <td>
                                    <asp:Label ID="LblMType1" runat="server" CssClass="auto-style5"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblVijatiyeGA" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblVijatiyeGAR" runat="server" CssClass="auto-style3"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblVijatiyeS" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblVijatiyeSR" runat="server" CssClass="auto-style3"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblVijatiyeGARmk" runat="server" CssClass="auto-style5"></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td>3.</td>
                                <td style="text-align: left">

                                    <asp:Label ID="lblDaaneType" runat="server" Style="font-size: medium"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblMType2" runat="server" CssClass="auto-style5"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblDamageDaaneGA" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblDamageDaaneGAR" runat="server" CssClass="auto-style3"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblDamageDaaneS" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblDamageDaaneSR" runat="server" CssClass="auto-style3"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblDamageDaaneGARmk" runat="server" CssClass="auto-style5"></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td>4.</td>
                                <td style="text-align: left;" class="auto-style5">बदरंग दाने</td>
                                <td>
                                    <asp:Label ID="LblMType3" runat="server" CssClass="auto-style5"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblBadrangDaaneGA" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblBadrangDaaneGAR" runat="server" CssClass="auto-style3"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblBadrangDaaneS" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblBadrangDaaneSR" runat="server" CssClass="auto-style3"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblBadrangDaaneGARmk" runat="server" CssClass="auto-style5"></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td>5.</td>
                                <td style="text-align: left;" class="auto-style5">चाकी दाने</td>
                                <td>
                                    <asp:Label ID="LblMType4" runat="server" CssClass="auto-style5"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblChaakiDaaneGA" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblChaakiDaaneGAR" runat="server" CssClass="auto-style3"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblChaakiDaaneS" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblChaakiDaaneSR" runat="server" CssClass="auto-style3"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblChaakiDaaneGARmk" runat="server" CssClass="auto-style5"></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td>6.</td>
                                <td style="text-align: left;" class="auto-style5">लाल दाने</td>
                                <td>
                                    <asp:Label ID="LblMType5" runat="server" CssClass="auto-style5"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblLaalDaaneGA" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblLaalDaaneGAR" runat="server" CssClass="auto-style3"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblLaalDaaneS" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblLaalDaaneSR" runat="server" CssClass="auto-style3"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblLaalDaaneGARmk" runat="server" CssClass="auto-style5"></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td>7.</td>
                                <td style="text-align: left;" class="auto-style5">निम्न श्रेणी का सम्मिश्रण</td>
                                <td>
                                    <asp:Label ID="LblMType6" runat="server" CssClass="auto-style5"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblOtherGA" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblOtherGAR" runat="server" CssClass="auto-style3"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblOtherS" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblOtherSR" runat="server" CssClass="auto-style3"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblOtherGARmk" runat="server" CssClass="auto-style5"></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td>8.</td>
                                <td style="text-align: left;" class="auto-style5">चोकर सहित दाने</td>
                                <td>
                                    <asp:Label ID="LblMType7" runat="server" CssClass="auto-style5"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblChokarDaaneGA" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblChokarDaaneGAR" runat="server" CssClass="auto-style3"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblChokarDaaneS" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblChokarDaaneSR" runat="server" CssClass="auto-style3"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblChokarDaaneGARmk" runat="server" CssClass="auto-style5"></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td>9.</td>
                                <td style="text-align: left;" class="auto-style5">नमी तत्व (R)</td>
                                <td>
                                    <asp:Label ID="LblMType8" runat="server" CssClass="auto-style5"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblNamiGA" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblNamiGAR" runat="server" CssClass="auto-style3"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblNamiS" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblNamiSR" runat="server" CssClass="auto-style3"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblNamiGARmk" runat="server" CssClass="auto-style5"></asp:Label>
                                </td>

                            </tr>


                        </table>

                    </td>
                </tr>

            </table>

            <table align="center" class="auto-style1">

                <tr>
                    <td class="auto-style5">&nbsp;&nbsp;&nbsp;&nbsp; उपरोक्तानुसार प्रेक्षित लॉट के चावल की केन्द्रीय निर्धारित मापदंड के अंतर्गत नहीं पाये जाने के कारण अस्वीकृत किया जाता है|</td>

                </tr>

                <tr>
                    <td style="font-size: small">
                        <asp:HiddenField ID="hdfGodownNo" runat="server" />
                        <asp:HiddenField ID="hdfPreviousPageAcptNumber" runat="server" />
                        <asp:HiddenField ID="hdfMillCode" runat="server" />
                        <asp:HiddenField ID="hdfAgrmtID" runat="server" />
                    </td>

                </tr>

                <tr>
                    <td>&nbsp;</td>

                </tr>

                <tr>
                    <td>&nbsp;</td>

                </tr>

                <tr style="text-align: right">
                    <td><b>Signature (Issue Center Incharge)</b></td>

                </tr>
            </table>

            <asp:Label ID="lblServerDateTime" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
