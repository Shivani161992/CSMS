<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CMS_BillGeneration_Print.aspx.cs" Inherits="State_CMS_BillGeneration_Print" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <style type="text/css">
        .auto-styleNSC
        {
            width: 622px;
        }

        .InspecTable
        {
            width: 1100px;
        }

        .InspHead
        {
        }

        .InspColumn
        {
            width: 33%;
            color: #10321f;
            letter-spacing: 2px;
            font-family: Almendra;
            font-size: 13px;
            font-weight: bold;
        }

        .InspecSubTable
        {
            width: 1100px;
            border-top: 6px groove #D9D9D9;
            border-bottom: 6px groove #D9D9D9;
            border-left: 6px groove #D9D9D9;
            border-right: 6px groove #D9D9D9;
            border-radius: 8px;
            /*box-shadow: 0 5px 10px rgba(0,0,0,.5);
           
            background-color:rgb(229, 230, 228);*/
        }

        .insptxt
        {
            width: 300px;
            height: 20px;
            letter-spacing: 2px;
            font-family: sans-serif;
            font-size: 12px;
            border-color: #03a9f4;
            border: 1px solid #03a9f4;
            border-radius: 8px;
            color: black;
            padding-left: 10px;
            border-bottom-style: groove;
        }

        .insfixtext
        {
            width: 310px;
            height: 20px;
            letter-spacing: 2px;
            font-family: sans-serif;
            font-size: 12px;
            border-color: #03a9f4;
            border: 1px solid #03a9f4;
            border-radius: 8px;
            color: black;
            padding-left: 10px;
            border-bottom-style: groove;
        }

        .insptxt:focus
        {
            border: none;
        }

        .inspddl
        {
            width: 310px;
            height: 24px;
            letter-spacing: 2px;
            font-family: sans-serif;
            font-size: 12px;
            border-color: #03a9f4;
            border: 1px solid #03a9f4;
            border-radius: 8px;
            padding-left: 10px;
            border-bottom-style: groove;
        }

            .inspddl:focus
            {
                border-radius: 8px;
            }

        .bttsub
        {
            background: transparent;
            border: none;
            outline: none;
            color: #fff;
            background: #3498db;
            padding: 10px 20px;
            cursor: pointer;
            border-radius: 5px;
            letter-spacing: 4px;
            font-family: sans-serif;
            height: 30px;
            width: 400px;
        }

            .bttsub:enabled, button[enabled]
            {
                background: #e74c3c;
            }

        .bttsubother
        {
            background: transparent;
            border: none;
            outline: none;
            color: #fff;
            background: #00AAA0;
            padding: 10px 20px;
            cursor: pointer;
            border-radius: 5px;
            letter-spacing: 4px;
            font-family: sans-serif;
            height: 30px;
            width: 150px;
        }

            .bttsubother:enabled, button[enabled]
            {
                background: #e74c3c;
            }

        .sign
        {
            color: #062946;
            font-size: 15px;
            text-decoration: none;
            letter-spacing: 2px;
        }

        .hover_row
        {
            // background-color: #A1DCF2;
            background: linear-gradient(silver, #fff);
        }
        .A4 {
            width: 210mm; /* You willmay need to reduce this to handle printer margins */
            margin: auto; /* This means it will be centred */
            height: 250mm;
            text-align: justify;
            font-size: large;
        }

    </style>
</head>
<body onload="window.print()">
    <form id="form1" runat="server">
    <div class="A4">
       <table style="width: 100%;" class="InspecSubTable">
                            <tr>
                                <td colspan="2" style="text-align: center; width: 100%">
                                    <input type="hidden" runat="server" id="hdfnacked" />
                                    <input type="hidden" runat="server" id="hdflabour" />
                                    <input type="hidden" runat="server" id="hdfcentral" />
                                    <input type="hidden" runat="server" id="hdfcommtosociety" />
                                    <input type="hidden" runat="server" id="hdfcommtostaagen" />
                                    <input type="hidden" runat="server" id="hdftransporCost" />
                                    <input type="hidden" runat="server" id="hdfDriageCost" />
                                    <input type="hidden" runat="server" id="hdfCostOfGunny" />
                                    <input type="hidden" runat="server" id="hdfcustody" />
                                    <input type="hidden" runat="server" id="hdfCheckFlag" />
                                    <input type="hidden" runat="server" id="hdfPost" />
                                    <input type="hidden" runat="server" id="hdfCmd" />

                                     <input type="hidden" runat="server" id="hdfBillNumber" />

                                    <input type="hidden" runat="server" id="hdfNirakshritShulka" />

                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center; width: 100%">
                                    <h2 style="color: Black; font-size: 12px; font-family: sans-serif; letter-spacing: 4px; text-align: center;">M.P. State Civil Supplies Corporation LTD., Head Office Bhopal</h2>
                                    <input type="hidden" runat="server" id="Hidden2" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: Left; height: 10px"></td>
                            </tr>
                            <tr>
                                <td style="text-align: Left; width: 50%; font-family: sans-serif; font-size: 14px;">GST No:...
                                    <asp:Label ID="lblGSTNo" runat="server" Font-Bold="True" Text="23AACCM5763B1ZU"></asp:Label>
                                </td>
                                <td style="text-align: Left; width: 50%; font-family: sans-serif; font-size: 14px;">PAN No:
                                    <asp:Label ID="lblPanNo" runat="server" Font-Bold="True" Text="AACCM5763B"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: Left; width: 50%; font-family: sans-serif; font-size: 14px;"></td>
                                <td style="text-align: Left; width: 50%; font-family: sans-serif; font-size: 14px;">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: Left; height: 10px"></td>
                            </tr>
                            <tr>
                                <td style="text-align: Left; width: 50%; font-family: sans-serif; font-size: 14px;">Bill No:...
                                    <asp:Label ID="lblBillNo" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td style="text-align: Left; width: 50%; font-family: sans-serif; font-size: 14px;"> Bill Date:
                                    <asp:Label ID="lblDate" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: Left; width: 50%; font-family: sans-serif; font-size: 14px;">Name Of Commodity:...
                                    <asp:Label ID="lblCommodity" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td style="text-align: Left; width: 50%; font-family: sans-serif; font-size: 14px;">HSN Code NO:
                                    <asp:Label ID="lblHSN" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: Left; width: 50%; font-family: sans-serif; font-size: 14px;">Acceptance Note NO:...
<%--                                    <asp:Label ID="lblFromAccept" runat="server" Font-Bold="True"></asp:Label>... TO ...<asp:Label ID="lblTOAccep" runat="server" Font-Bold="True"></asp:Label>...--%>
                                    <span style="color: rgb(0, 0, 0); font-family: sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">( As per enclosed here with...<asp:Label ID="Label30" runat="server" Font-Bold="True"></asp:Label>...)</span></td>

                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: Left;">To,
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: Left; height: 10px"></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: Left;">Branch Manager,
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: Left;">National Agriculture CO-OP. Marketing Federation Of India LTD,
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: Left;">13-14 Jawahar Marg, Old IDA Building,
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: Left;">3RD Floor
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: Left;">Indore- 452 007 [MADHYA PRADESH]
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: Left;">GST NO:...<asp:Label ID="Label19" runat="server" Font-Bold="True" Text="23AAAAN4629FIZW"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: Left; height: 10px"></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: Left;">Goods Packed in:
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: Left; height: 10px"></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: Left; height: 10px">
                                    <table style="width: 100%; border-collapse: collapse" border="1">
                                        <tr>
                                            <td style="width: 10%; border-collapse: collapse; text-align: center; font-weight: bold;">S.No.
                                            </td>
                                            <td style="width: 40%; border-collapse: collapse; text-align: center; font-weight: bold;">Particulars
                                            </td>
                                            <td style="width: 10%; border-collapse: collapse; text-align: center; font-weight: bold;">No. Of Bags
                                            </td>
                                            <td style="width: 10%; border-collapse: collapse; text-align: center; font-weight: bold;">Quantity
                                                <br />
                                                [in Qtls.]
                                            </td>
                                            <td style="width: 10%; border-collapse: collapse; text-align: center; font-weight: bold;">Rate
                                                <br />
                                                [Per Qtls.]
                                            </td>
                                            <td style="width: 20%; border-collapse: collapse; text-align: center; font-weight: bold;">Total Amount
                                                <br />
                                                [in Rs.]
                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="width: 10%; border-collapse: collapse; padding-left: 20px;">1
                                            </td>
                                            <td style="width: 40%; border-collapse: collapse; padding-left: 20px;">Minimum Support Price
                                            </td>
                                            <td style="width: 10%; border-collapse: collapse; text-align: right">                                            
                                                    <asp:Label ID="Label20" runat="server" Font-Bold="True"></asp:Label></td></td>
                                            <td style="width: 10%; border-collapse: collapse; text-align: right">
                                                <asp:Label ID="Label1" runat="server" Font-Bold="True"></asp:Label>

                                            </td>
                                            <td style="width: 10%; border-collapse: collapse; text-align: right">
                                                <asp:Label ID="Label9" runat="server" Font-Bold="True"></asp:Label>
                                            </td>
                                            <td style="width: 10%; border-collapse: collapse; text-align: right">
                                                <asp:Label ID="Label23" runat="server" Font-Bold="True"></asp:Label>

                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="width: 10%; border-collapse: collapse; padding-left: 20px;">2
                                            </td>
                                            <td style="width: 40%; border-collapse: collapse; padding-left: 20px;">Central Bonus
                                            </td>
                                            <td style="width: 10%; border-collapse: collapse">&nbsp;</td>
                                            <td style="width: 10%; border-collapse: collapse">&nbsp;</td>
                                            <td style="width: 10%; border-collapse: collapse; text-align: right">
                                                <asp:Label ID="Label10" runat="server" Font-Bold="True"></asp:Label>

                                            </td>
                                            <td style="width: 10%; border-collapse: collapse; text-align: right">&nbsp;<asp:Label ID="Label24" runat="server" Font-Bold="True"></asp:Label>

                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="width: 10%; border-collapse: collapse; padding-left: 20px;">3
                                            </td>
                                            <td style="width: 40%; border-collapse: collapse; padding-left: 20px;">Nirashrit Shulka
                                                 <br />
                                                [ <asp:Label ID="Label21" runat="server" Font-Bold="True"></asp:Label>%  &nbsp;of MSP]
                                            </td>
                                            <td style="width: 10%; border-collapse: collapse">&nbsp;</td>
                                            <td style="width: 10%; border-collapse: collapse">&nbsp;</td>
                                            <td style="width: 10%; border-collapse: collapse; text-align: right">
                                                <asp:Label ID="Label17" runat="server" Font-Bold="True"></asp:Label>

                                            </td>
                                            <td style="width: 10%; border-collapse: collapse; text-align: right">
                                                <asp:Label ID="Label18" runat="server" Font-Bold="True"></asp:Label>

                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="width: 10%; border-collapse: collapse; padding-left: 20px;">4
                                            </td>
                                            <td style="width: 40%; border-collapse: collapse; padding-left: 20px;">Labour Charges
                                                <br />
                                                [At Procurring Center]
                                            </td>
                                            <td style="width: 10%; border-collapse: collapse">&nbsp;</td>
                                            <td style="width: 10%; border-collapse: collapse">&nbsp;</td>
                                            <td style="width: 10%; border-collapse: collapse; text-align: right">
                                                <asp:Label ID="Label11" runat="server" Font-Bold="True"></asp:Label>

                                            </td>
                                            <td style="width: 10%; border-collapse: collapse; text-align: right">
                                                <asp:Label ID="Label25" runat="server" Font-Bold="True"></asp:Label>

                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="width: 10%; border-collapse: collapse; padding-left: 20px;">5
                                            </td>
                                            <td style="width: 40%; border-collapse: collapse; padding-left: 20px;">Transportation Charges
                                                <br />
                                                [From Procurring Center to Storage Point]
                                            </td>
                                            <td style="width: 10%; border-collapse: collapse">&nbsp;</td>
                                            <td style="width: 10%; border-collapse: collapse">&nbsp;</td>
                                            <td style="width: 10%; border-collapse: collapse; text-align: right">
                                                <asp:Label ID="Label12" runat="server" Font-Bold="True"></asp:Label>

                                            </td>
                                            <td style="width: 10%; border-collapse: collapse; text-align: right">
                                                <asp:Label ID="Label26" runat="server" Font-Bold="True"></asp:Label>

                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="width: 10%; border-collapse: collapse; padding-left: 20px;">6
                                            </td>
                                            <td style="width: 40%; border-collapse: collapse; padding-left: 20px;">Commission to Society
                                                <br />
                                                [ <asp:Label ID="Label22" runat="server" Font-Bold="True"></asp:Label>% &nbsp;of MSP]
                                            </td>
                                            <td style="width: 10%; border-collapse: collapse">&nbsp;</td>
                                            <td style="width: 10%; border-collapse: collapse">&nbsp;</td>
                                            <td style="width: 10%; border-collapse: collapse; text-align: right">
                                                <asp:Label ID="Label13" runat="server" Font-Bold="True"></asp:Label>

                                            </td>
                                            <td style="width: 10%; border-collapse: collapse; text-align: right">
                                                <asp:Label ID="Label27" runat="server" Font-Bold="True"></asp:Label>

                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="width: 10%; border-collapse: collapse; padding-left: 20px;">7
                                            </td>
                                            <td style="width: 40%; border-collapse: collapse; padding-left: 20px;">Commission to State Agency
                                                <br />
                                               [ <asp:Label ID="Label29" runat="server" Font-Bold="True"></asp:Label>% &nbsp;of MSP]
                                            </td>
                                            <td style="width: 10%; border-collapse: collapse;">&nbsp;</td>
                                            <td style="width: 10%; border-collapse: collapse">&nbsp;</td>
                                            <td style="width: 10%; border-collapse: collapse; text-align: right">
                                                <asp:Label ID="Label14" runat="server" Font-Bold="True"></asp:Label>

                                            </td>
                                            <td style="width: 10%; border-collapse: collapse; text-align: right">
                                                <asp:Label ID="Label28" runat="server" Font-Bold="True"></asp:Label>

                                            </td>

                                        </tr>

                                        <tr>
                                            <td style="width: 10%; border-collapse: collapse; padding-left: 20px;">8
                                            </td>
                                            <td style="width: 40%; border-collapse: collapse; padding-left: 20px;">Driage/Shortage @ 0.15% of MSP
                                               
                                            </td>
                                            <td style="width: 10%; border-collapse: collapse;">&nbsp;</td>
                                            <td style="width: 10%; border-collapse: collapse">&nbsp;</td>
                                            <td style="width: 10%; border-collapse: collapse; text-align: right">
                                                <asp:Label ID="Label3" runat="server" Font-Bold="True"></asp:Label>

                                            </td>
                                            <td style="width: 10%; border-collapse: collapse; text-align: right">
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True"></asp:Label>

                                            </td>

                                        </tr>



                                       <%-- <tr>
                                            <td style="width: 10%; border-collapse: collapse; padding-left: 20px;">9
                                            </td>
                                            <td style="width: 40%; border-collapse: collapse; padding-left: 20px;">Other:
                                                <br />
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 10%; border-collapse: collapse">9.1
                                                        </td>
                                                        <td style="width: 90%; border-collapse: collapse; text-align: left;">Cost Of Gunny Bags
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; border-collapse: collapse; text-align: left;">9.2
                                                        </td>
                                                        <td style="width: 90%; border-collapse: collapse; text-align: left;">Custody and Maintt. Charges [Acquisition Level]
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; border-collapse: collapse; text-align: left;">9.3
                                                        </td>
                                                        <td style="width: 90%; border-collapse: collapse; text-align: left;"></td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 10%; border-collapse: collapse">&nbsp;</td>
                                            <td style="width: 10%; border-collapse: collapse">&nbsp;</td>
                                            <td style="width: 10%; border-collapse: collapse; text-align: center">
                                                <br />
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 10%; border-collapse: collapse">9.1 </td>
                                                        <td style="width: 90%; border-collapse: collapse; text-align: center;">
                                                           

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; border-collapse: collapse; text-align: left;">9.2 </td>
                                                        <td style="width: 90%; border-collapse: collapse; text-align: center;">
                                                          </asp:Label>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; border-collapse: collapse; text-align: left;">9.3 </td>
                                                        <td style="width: 90%; border-collapse: collapse; text-align: center;">
                                                            

                                                        </td>
                                                    </tr>
                                                </table>

                                            </td>
                                            <td style="width: 10%; border-collapse: collapse; text-align: center">
                                                <br />
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 10%; border-collapse: collapse">9.1 </td>
                                                        <td style="width: 90%; border-collapse: collapse; text-align: right;">
                                                           

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; border-collapse: collapse; text-align: left;">9.2 </td>
                                                        <td style="width: 90%; border-collapse: collapse; text-align: right;">
                                                            

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; border-collapse: collapse; text-align: left;">9.3 </td>
                                                        <td style="width: 90%; border-collapse: collapse; text-align: right;">
                                                           

                                                        </td>
                                                    </tr>
                                                </table>

                                            </td>

                                        </tr>--%>




                                     
                                        <tr>
                                            <td rowspan="4" style="width: 10%; border-collapse: collapse; padding-left: 20px;">9
                                            </td>
                                            <td style="width: 40%; border-collapse: collapse; padding-left: 20px;">Others:
                                            </td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>

                                        </tr>
                                        <tr>
                                            <td style="width: 40%; border-collapse: collapse; padding-left: 20px;">9.1 Cost Of Gunny Bags</td>
                                            <td></td>
                                            <td></td>
                                           <td style="width: 10%; border-collapse: collapse; text-align: right">  <asp:Label ID="Label43" runat="server" Font-Bold="True"></asp:Label></td>
                                             <td style="width: 10%; border-collapse: collapse; text-align: right">  <asp:Label ID="Label46" runat="server" Font-Bold="True"></asp:Label></td>

                                        </tr>
                                        <tr>
                                            <td style="width: 40%; border-collapse: collapse; padding-left: 20px;">9.2 Custody and Maintt. Charges [Acquisition Level]
                                            </td>
                                            <td></td>
                                            <td></td>
                                             <td style="width: 10%; border-collapse: collapse; text-align: right">   <asp:Label ID="Label44" runat="server" Font-Bold="True"> </asp:Label></td>
                                            <td style="width: 10%; border-collapse: collapse; text-align: right"> <asp:Label ID="Label47" runat="server" Font-Bold="True"></asp:Label> </td>

                                        </tr>
                                        <tr>
                                            <td style="width: 40%; border-collapse: collapse; padding-left: 20px;">9.3 Interest @ 
                                                            <asp:Label ID="Label7" runat="server" Font-Bold="True"></asp:Label>
                                                % for 2 Months</td>
                                            <td></td>
                                            <td></td>
                                            <td style="width: 10%; border-collapse: collapse; text-align: right">  <asp:Label ID="Label45" runat="server" Font-Bold="True"></asp:Label></td>
                                             <td style="width: 10%; border-collapse: collapse; text-align: right">  <asp:Label ID="Label48" runat="server" Font-Bold="True"></asp:Label></td>

                                        </tr>
                                           <tr id="trMustard" runat="server" visible="false">
                                            <td style="width: 10%; border-collapse: collapse; padding-left: 20px;">10
                                            </td>
                                            <td style="width: 40%; border-collapse: collapse; padding-left: 20px;">ADD: SGST+CGST
                                                <asp:Label ID="Label8" runat="server" Font-Bold="True"></asp:Label>+<asp:Label ID="Label15" runat="server" Font-Bold="True"></asp:Label>
                                                =
                                                <asp:Label ID="Label16" runat="server" Font-Bold="True"></asp:Label>
                                                of Sub-Total 
                                               
                                            </td>
                                            <td style="width: 10%; border-collapse: collapse;">&nbsp;</td>
                                            <td style="width: 10%; border-collapse: collapse">&nbsp;</td>
                                            <td style="width: 10%; border-collapse: collapse; text-align: right">
                                                <asp:Label ID="Label5" runat="server" Font-Bold="True"></asp:Label>

                                            </td>
                                            <td style="width: 10%; border-collapse: collapse; text-align: right">
                                                <asp:Label ID="Label6" runat="server" Font-Bold="True"></asp:Label>

                                            </td>

                                        </tr>

                                        <tr>
                                            <td colspan="2" style="font-weight: bold; text-align: center;">Total:
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td style="text-align: center">&nbsp;</td>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label41" runat="server" Font-Bold="True"></asp:Label>

                                            </td>

                                        </tr>
                                    </table>












                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: left; height: 10px;"></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: left; padding-left: 40px;">[In Words:...
                                                                                    <asp:Label ID="Label42" runat="server" Font-Bold="True"></asp:Label>

                                    ...]
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: right; padding-right: 40px;">Digitally Signatories
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: left; height: 10px;"></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: left; height: 10px;"></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: left; height: 10px;"></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: left; height: 10px;"></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: left; height: 10px;"></td>
                            </tr>
                            <tr>
                                <td style="text-align: center" colspan="2">
                                    <h2 style="color: Black; font-size: 12px; font-family: sans-serif; letter-spacing: 2px; text-align: center;">Note:- {This bill is submitted on the basis of Acceptance Note issued by Godown and Quality inspected by surveyors}</h2>

                                </td>
                            </tr>
                          
                        </table>
    </div>
    </form>
</body>
</html>
