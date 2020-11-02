<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReprintCMRAccept.aspx.cs" Inherits="IssueCenter_ReprintCMRAccept" %>

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

        .auto-style3 {
            width: 100%;
        }
        .auto-style4 {
            width: 100%;
            font-size: large;
        }
    </style>
</head>
<body onload="window.print()">
    <form id="form1" runat="server">
        <br />
        <div class="A4">

            <table align="center" class="auto-style1">

                <tr>
                    <td rowspan="3" style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/mpscsc_logo.jpg" Height="120px" Width="120px" />
                    </td>
                    <td style="text-align: center" class="auto-style4"><strong style="color: #003399">MP State Civil Supplies Corporation Limited</strong></td>
                    <td rowspan="3" style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0">
                        <asp:Image ID="ImgQRCode" runat="server" Height="125px" Width="125px" />
                    </td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3">District ....<asp:Label ID="lblDistManagerName" runat="server" Font-Bold="True"></asp:Label>....KMS<asp:Label ID="lblYear" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3"><strong>&lt;&lt;
                        <asp:Label ID="lblCMRAcptRjct" runat="server" Text="CMR Acceptance Note" Style="text-decoration: underline"></asp:Label>
                        &nbsp;&gt;&gt;</strong></td>

                </tr>
            </table>

            <table align="center" class="auto-style1">
                <tr>
                    <td>Miller Name - ....<asp:Label ID="lblMillerName" runat="server" Font-Bold="True" Visible="True"></asp:Label>....</td>

                    <td style="text-align: right">Agreement No - ....<asp:Label ID="lblAgrmtNo" runat="server" Font-Bold="True"></asp:Label>....
                    </td>
                </tr>
                <tr>
                    <td>Miller State&nbsp; -&nbsp;....<asp:Label ID="lblMillerState" runat="server" Font-Bold="True" Visible="true"></asp:Label>....
                    </td>

                    <td style="text-align: right">Miller District - ....<asp:Label ID="lblMillerDistrict" runat="server" Font-Bold="True" Visible="true"></asp:Label>....
                    </td>
                </tr>

            </table>
            <table align="center" class="auto-style1">
                <tr>
                    <td>
                        <asp:Label ID="lblAcptRjctNO" runat="server" Text="Acceptance No -"></asp:Label>
                        &nbsp;....<asp:Label ID="lblAcptNo" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="text-align: right">
                        <asp:Label ID="lblAcptRjctDate" runat="server" Text="Acceptance Date -"></asp:Label>
                        &nbsp;....<asp:Label ID="lblAcptDate" runat="server" Font-Bold="True"></asp:Label>....</td>

                </tr>
                <tr>
                    <td>CMR Deposit Order No -
                        &nbsp;....<asp:Label ID="lblCMRDepositNo" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="text-align: right">Type of Bags - ....<asp:Label ID="lblBagType" runat="server" Font-Bold="True"></asp:Label>....</td>

                </tr>
                <tr>
                    <td>Miller Tags - ....<asp:Label ID="lblTag" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="text-align: right">No. of Tag - ....<asp:Label ID="lblTagNo" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td>Toul Parchi No. - ....<asp:Label ID="lblToulNo" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="text-align: right">Godown Name ....<asp:Label ID="lblGodown" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td>Inspected By - ....<asp:Label ID="lblInspector" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="text-align: right">Truck Number ....<asp:Label ID="lblTruckNumber" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>
                 <tr>
<td>Lot Number - ....<asp:Label ID="lblLot" runat="server" Font-Bold="True"></asp:Label>....</td>                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="text-align: right">Quantity (Qtls) ....<asp:Label ID="lblQty" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>


            </table>

            <table align="center" class="auto-style1">

                <tr>
                    <td style="font-size: medium">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" CellPadding="4" EnableModelValidation="True" Width="100%" OnRowDataBound="GridView1_RowDataBound1">
                            <Columns>
                                <asp:BoundField HeaderText="Lot No." DataField="LotNumber">
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>

                                <asp:BoundField HeaderText="Deposit District" DataField="district_name">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>

                                <asp:BoundField HeaderText="Issue Center" DataField="DepotName">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>

                                <asp:BoundField HeaderText="Godown" DataField="Godown_Code">
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Quantity (Qtls)" DataField="CMRRecd">
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>
                                  <asp:BoundField HeaderText="Bags" DataField="Bags">
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>
                                  <asp:BoundField HeaderText="Truck No." DataField="Truck_No">
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="White" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" VerticalAlign="Middle" />
                            <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" />
                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                            <RowStyle BackColor="White" ForeColor="#330099" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                        </asp:GridView>

                    </td>

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
                    <td class="auto-style5" colspan="3"><span class="auto-style7"></span><span class="auto-style6"> <br />
                        </span>
                        <asp:Label ID="lblmsg1" runat="server" Text=""></asp:Label>
                    </td>

                </tr>

                <tr>
                    <td style="font-size: medium" colspan="3">
                       <asp:Label ID="lblmsg2" runat="server" Text=""></asp:Label>
                    </td>

                </tr>

                <tr>
                    <td colspan="3">&nbsp;</td>

                </tr>

                <tr>
                    <td colspan="3">&nbsp;</td>

                </tr>

                <tr>
                    <td style="text-align: left"><b>मिलर्स हस्ताक्षर</b></td>
                    <td style="text-align: right"><b>शाखा प्रबंधक हस्ताक्षर(भण्डारण संस्था)</b></td>

                    <td style="text-align: right"><b>Signature (Issue Center Incharge) MPSCSC</b></td>

                </tr>
            </table>

            <asp:Label ID="lblServerDateTime" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
