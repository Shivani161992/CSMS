<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PMilling_DO.aspx.cs" Inherits="PaddyMilling_Print_PMilling_DO" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>&nbsp;</title>

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

        .auto-style2 {
            text-decoration: underline;
        }

        .auto-style3 {
            width: 100%;
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
                        <asp:Image ID="Image1" runat="server" Height="120px" Width="120px" />
                    </td>
                    <td style="text-align: center" class="auto-style3">
                        <asp:Label ID="lblMFD" runat="server" Text="" style="font-size: large"></asp:Label>
                    </td>
                    <td rowspan="3" style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0">
                        <asp:Image ID="ImgQRCode" runat="server" Height="125px" Width="125px" />
                    </td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3">जिला ....<asp:Label ID="lblDistManagerName" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3"><strong>&lt;&lt; <span class="auto-style2">&nbsp;धान डिलेवरी आर्डर</span>&nbsp;&gt;&gt;</strong></td>

                </tr>
            </table>

            <table align="center" class="auto-style1">
                <tr>
                    <td>डी०ओ० क्रमांक- ....<asp:Label ID="lblDoNumber" runat="server" Font-Bold="True"></asp:Label>....</td>

                    <td style="text-align: right">डी०ओ० दिनाँक ....<asp:Label ID="lblDate" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>

                </tr>
                <tr>
                    <td colspan="2">प्रति ,</td>

                </tr>
                <tr>
                    <td colspan="2" style="padding-left:39px;"> श्री..........................</td>

                </tr>
                <tr>
                    <td colspan="2" style="padding-left:39px;">प्रदाय केन्द्र प्रभारी </td>

                </tr>
                <tr>
                    <td colspan="2" style="padding-left:39px;">प्रदाय केन्द्र ....<asp:Label ID="lblIssueCentreName" runat="server" Font-Bold="True"></asp:Label>....</td>

                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>

                </tr>
                <tr>
                    <td colspan="2">विषय :- उपार्जन वर्ष ....<asp:Label ID="lblCropYear" runat="server" Font-Bold="True"></asp:Label>.... की धान मिलिंग हेतु प्रदाय आदेश | </td>

                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>

                </tr>
                <tr>
                    <td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;कृपया ....<asp:Label ID="lblMillName" runat="server" Font-Bold="True"></asp:Label>.... को कार्पोरेशन के साथ मिलिंग हेतु निष्पादित अनुबंध क्रमांक ....<asp:Label ID="lblAgrmtNo" runat="server" Font-Bold="True"></asp:Label>....
                    दिनाँक ....<asp:Label ID="lblDate1" runat="server" Font-Bold="True"></asp:Label>....
                        अनुबंधित धान की मात्रा ....<asp:Label ID="lblTotalCDhan" runat="server" Font-Bold="True"></asp:Label>.... क्विंटल
                        कामन धान तथा ....<asp:Label ID="lblTotalGDhan" runat="server" Font-Bold="True"></asp:Label>.... क्विंटल
                        ग्रेड-ए धान के तहत ....<asp:Label ID="lblAllotedCDhan" runat="server" Font-Bold="True"></asp:Label>.... क्विंटल
                        कामन धान तथा ....<asp:Label ID="lblAllotedGDhan" runat="server" Font-Bold="True"></asp:Label>.... क्विंटल
                    ग्रेड-ए धान ....<asp:Label ID="lblMillingType" runat="server" Font-Bold="True"></asp:Label>.... मिलिंग हेतु दिनाँक ....<asp:Label ID="lblEndDate" runat="server" Font-Bold="True"></asp:Label>.... तक अनिवार्य रूप से जारी करे |
                        
                    </td>

                </tr>
                <tr>
                    <td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; उक्त मात्रा जारी करने के पश्चात ....<asp:Label ID="lblRemCDhan" runat="server" Font-Bold="True"></asp:Label>....
                    क्विंटल कामन धान तथा ....<asp:Label ID="lblRemGDhan" runat="server" Font-Bold="True"></asp:Label>.... क्विंटल ग्रेड-ए धान जारी की जाना शेष रहेगा | प्रदाय धान की राशि मिलर द्वारा अग्रिम रुप से कार्पोरेशन के पास जमा कर दी गयी है |</td>

                </tr>

                 <table align="center" class="auto-style1">

                <tr>
                    <td class="auto-style4"><strong> उक्त मात्रा निचे दिए गए <asp:Label ID="lblGS" runat="server" Font-Bold="True"></asp:Label> क्रमांक से जारी की जाये :- </strong></td>

                </tr>

                <tr id="trgodown" runat="server" visible="false">
                    <td style="font-size: medium">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" CellPadding="4" EnableModelValidation="True" ShowFooter="True" Width="100%" OnRowDataBound="GridView1_RowDataBound1">
                            <Columns>
                                <asp:BoundField HeaderText="Lot No." DataField="LotNo">
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
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
                                <asp:BoundField HeaderText="Quantity (Qtls)" DataField="Alloted_CommonDhan">
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
                     <tr id="trsociety" runat="server" visible="false">
                    <td style="font-size: medium">
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" CellPadding="4" EnableModelValidation="True" ShowFooter="True" Width="100%" OnRowDataBound="GridView2_RowDataBound">
                            <Columns>
                                <asp:BoundField HeaderText="Lot No." DataField="LotNo">
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Society District" DataField="SocietyDist">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                             
                                <asp:BoundField HeaderText="Society" DataField="SocietyID">
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Quantity (Qtls)" DataField="Alloted_CommonDhan">
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
                    <td colspan="2">&nbsp;</td>

                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>

                </tr>
                <tr style="text-align: right">
                    <td style="text-align: left">मिलर्स हस्ताक्षर</td>

                    <td><asp:Label ID="lblDM" runat="server" Text=""></asp:Label>
&nbsp;/ अधिकृत कर्मी</td>

                </tr>
                <tr style="text-align: right">
                    <td colspan="2">&nbsp;</td>

                </tr>
                <tr>
                    <td colspan="2">प्रतिलिपि –  ....<asp:Label ID="lblMillName1" runat="server" Font-Bold="True"></asp:Label>....
                        कृपया निर्धारित दिनाँक ....<asp:Label ID="lblEndDate1" runat="server" Font-Bold="True"></asp:Label>....
                        तक धान का उठाव सुनिश्चित करें | 
                    </td>

                </tr>
                <tr style="text-align: right">
                    <td colspan="2">&nbsp;</td>

                </tr>
                <tr style="text-align: right">
                    <td colspan="2">&nbsp;</td>

                </tr>
                <tr >
                    <td style="text-align: left">गोदाम प्रभारी</td>

                    <td style="text-align: right"><asp:Label ID="lblDM0" runat="server" Text=""></asp:Label>
&nbsp;/ अधिकृत कर्मी</td>

                </tr>

            </table>

            </table>

                        <table align="center" width="100%">

                <tr class="auto-style4">
                    <td style="text-align: left">
                        <asp:Label ID="lblCurrentDateTime" runat="server" Font-Bold="True"></asp:Label></td>
                    <td style="text-align: right"></td>
                </tr>

            </table>
        </div>


    </form>

</body>
</html>
