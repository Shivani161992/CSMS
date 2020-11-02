<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="CMS_BillGeneration.aspx.cs" Inherits="State_CMS_BillGeneration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="critical_js_files" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    </style>
    <script>
        function allowOnlyNumber(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>


    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />


    <script type="text/javascript">
        $(function () {
            $("[id*=GridView1] td").hover(function () {
                $("td", $(this).closest("tr")).addClass("hover_row");
            }, function () {
                $("td", $(this).closest("tr")).removeClass("hover_row");
            });
        });
    </script>
    <center>
        <table style="width: 1100px; font-size: 12px;">
            <tr>
                <td style="text-align: left; width: 200px">
                    <a href="../State/ChannaMassorSarson_Home.aspx" class="sign">&#9754 Back
                    </a>
                </td>
                <td style="text-align: center; width: 700px">
                    <h2 style="color: #e74c3c; font-size: 20px; font-family: 'Bellefair'; letter-spacing: 4px; text-align: center;">Bill Generation</h2>
                    <input type="hidden" runat="server" id="hdfCommodity" />
                </td>

                <td style="text-align: right; width: 200px">
                    <a href="CMS_BillGeneration.aspx" class="sign">&#8635 New
                    </a>
                </td>
            </tr>

        </table>

    </center>

    <center>
        <table class="InspecTable">

            <tr>
                <td colspan="3" style="height: 10px;"></td>
            </tr>

            <tr runat="server" id="trNumber" visible="false">
                <td colspan="3" style="height: 10px;">
                    <center>
                        <asp:Label ID="Label2" runat="server" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 20px;" ForeColor="Blue" Visible="False"></asp:Label>
                    </center>
                </td>
            </tr>

            <tr>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">Bill Number
                    <br />

                    <asp:DropDownList ID="ddlBillNumber" runat="server" CssClass="inspddl" OnSelectedIndexChanged="ddlBillNumber_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" Style="font-size: 12px;"
                        ControlToValidate="ddlBillNumber" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td style="text-align: right;">
                    <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="bttsubother" Visible="false" Enabled="true" OnClick="btnPrint_Click" />
                </td>

            </tr>
            <tr>
                <td colspan="3" style="height: 10px"></td>
            </tr>
            <tr>
                <td colspan="3">
                    <center>
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
                                <td style="text-align: Left; width: 50%; font-family: sans-serif; font-size: 14px;">Acceptance Note NO:...( As per enclosed here with...<asp:Label ID="Label30" runat="server" Font-Bold="True"></asp:Label>...)
                                   <%-- <asp:Label ID="lblFromAccept" runat="server" Font-Bold="True"></asp:Label>... TO ...<asp:Label ID="lblTOAccep" runat="server" Font-Bold="True"></asp:Label>...--%>
                                </td>

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
                                <td colspan="2" style="text-align: Left;">GST NO:...                                     <asp:Label ID="Label19" runat="server" Font-Bold="True" Text="23AAAAN4629FIZW"></asp:Label>
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
                                                   <asp:Label ID="Label20" runat="server" Font-Bold="True"></asp:Label> </td>
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
                                             <td>&nbsp;</td>
                                           <td style="width: 40%; border-collapse: collapse; padding-left: 20px;">Total:
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
                        </center>
                </td>
            </tr>
            <tr>
                <td style="text-align: center" colspan="3">
                    <asp:Button ID="btAdd" runat="server" Text="Submit" CssClass="bttsubother" Visible="true" Enabled="true" OnClick="btAdd_Click" />
                    <asp:Button ID="btnPosted" runat="server" Text="Post" CssClass="bttsubother" Visible="false" Enabled="false" OnClick="btnPosted_Click" />
                </td>
            </tr>

        </table>
    </center>

</asp:Content>

