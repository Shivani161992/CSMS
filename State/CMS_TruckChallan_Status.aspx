<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="CMS_TruckChallan_Status.aspx.cs" Inherits="State_CMS_TruckChallan_Status" %>

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
                outline: none;
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
            height: 25px;
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
            background: linear-gradient(silver, #fff);
        }

        .tblsarson
        {
            border-collapse: collapse;
        }
        .lbl
        {
            color:#e74c3c;
        }
    </style>
    <center>
        <table style="width: 1100px; font-size: 12px;">
            <tr>
                <td style="text-align: left; width: 200px">
                    <a href="../State/ChannaMassorSarson_Home.aspx" class="sign">&#9754 Back
                    </a>
                </td>
                <td style="text-align: center; width: 700px">
                    <h2 style="color: #e74c3c; font-size: 20px; font-family: 'Bellefair'; letter-spacing: 4px; text-align: center;">Truck Challan Status</h2>
                    <input type="hidden" runat="server" id="hdfID" />
                </td>

                <td style="text-align: right; width: 200px">
                    <a href="CMS_TruckChallan_Status.aspx" class="sign">&#8635 New
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

            <tr>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">TruckChallan
                    <br />
                    <asp:TextBox ID="txttruckChallan" CssClass="insptxt" runat="server" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txttruckChallan" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                   
                </td>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">
                                                                                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="bttsubother" Visible="true" Enabled="true" OnClick="btnSearch_Click" />


                </td>
                <td class="InspColumn" style="padding-left: 20px; text-align: left"></td>
            </tr>
            <tr>
                <td colspan="3" style="height: 10px;"></td>
            </tr>

            <tr>
                <td colspan="3" style="background-color: lightgray; height: 5px; border-radius: 32px;">
                    <center>
                        <table style="width: 700px">
                            <tr>
                                <td style="background-color: darkgrey; height: 5px; border-radius: 32px;"></td>
                            </tr>
                        </table>
                    </center>

                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 10px;"></td>
            </tr>

            <tr>
                <td colspan="3" style="height: 10px;">

                    <table style="width: 100%;" class="InspecSubTable">
                        <tr>
                            <td colspan="3" style="text-align: center; width: 100%; font-size: small;">जारी विवरण
                                 <input type="hidden" runat="server" id="hdfCommoditiesCSMS" />


                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: center; width: 100%">
                                <h2 style="color: Black; font-size: 12px; font-family: sans-serif; letter-spacing: 2px; text-align: center;">Truck Challan Number :-....
                                    <asp:Label ID="lblTC" runat="server" Font-Bold="True"></asp:Label> ....  <asp:Label ID="lblCropYear" runat="server" Font-Bold="True"></asp:Label></h2>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: Left; height: 10px"></td>
                        </tr>
                        <tr>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left">District
                    <br />

                                <asp:Label ID="lblDist" CssClass="lbl" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left">Purchase Center
                        <br />
                                <asp:Label ID="lblPurchaseCenter" CssClass="lbl" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left">Mandi
                        <br />
                                <asp:Label ID="lblMandi" CssClass="lbl" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left">Date of Issue
                    <br />

                                <asp:Label ID="lblDateOfIssue" CssClass="lbl" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left">Issue ID 
                        <br />
                                <asp:Label ID="lblIssueID" CssClass="lbl" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left">Commodity
                        <br />
                                <asp:Label ID="lblcomm" CssClass="lbl" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left">FAQ/Non FAQ
                    <br />

                                <asp:Label ID="lblUFNF" CssClass="lbl" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left">Quantity (Qtls)
                        <br />
                                <asp:Label ID="lblDQty" CssClass="lbl" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left">Bags
                        <br />
                                <asp:Label ID="lblDBags" CssClass="lbl" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left">Truck Number
                    <br />

                                <asp:Label ID="lblTN" CssClass="lbl" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left">Toul Patrak Number
                        <br />
                                <asp:Label ID="lblToulPat" CssClass="lbl" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left">Bilti Number
                        <br />
                                <asp:Label ID="lblBilNum" CssClass="lbl" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left">Godown
                    <br />

                                <asp:Label ID="lblGod" CssClass="lbl" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left">Surveyor Name
                        <br />
                                <asp:Label ID="lblUsur" CssClass="lbl" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left">Surveyor Inspection Date
                        <br />
                                <asp:Label ID="lblDUS" CssClass="lbl" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3" style="height: 10px;">&nbsp;</td>
                        </tr>

                        <tr>
                            <td colspan="3" style="background-color: lightgray; height: 5px; border-radius: 32px;">
                                <center>
                                    <table style="width: 700px">
                                        <tr>
                                            <td style="background-color: darkgrey; height: 5px; border-radius: 32px;"></td>
                                        </tr>
                                    </table>
                                </center>

                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px;"></td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px;">
                                <h2 style="color: #e67e22; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px; text-align: center;">Quality Inspection Parameters (Purchase Center)</h2>

                            </td>
                        </tr>
                        <%-- 14.28 7
                        12.5 8--%>
                        <tr runat="server" id="trsarson" visible="false">
                            <td colspan="3" style="height: 10px;">
                                <table border="1" id="tblUsarson" style="width: 100%;" class="tblsarson">
                                    <tr>
                                        <td style="width: 16%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Impurities/Foreign matter including Taramira</td>
                                        <td style="width: 16%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Admixture with other types including Toria</td>
                                        <td style="width: 16%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Unripe, Shrivelled or Immature</td>
                                        <td style="width: 16%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Damaged & Weevilled</td>
                                        <td style="width: 16%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Small atrophied seeds</td>
                                        <td style="width: 16%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Moisture Content</td>

                                    </tr>
                                    <tr>
                                        <td style="width: 16%; text-align: center;">
                                            <asp:Label ID="lbluparFM_IncTaramira" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 16%; text-align: center;">
                                            <asp:Label ID="lbluparAM_OT_Toria" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 16%; text-align: center;">
                                            <asp:Label ID="lbluparUR_Shvld_Imm" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 16%; text-align: center;">
                                            <asp:Label ID="lbluparDamWeevd" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 16%; text-align: center;">
                                            <asp:Label ID="lbluparSmallAtroSeeds" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 16%; text-align: center;">
                                            <asp:Label ID="lbluparMoisCont" runat="server" Font-Bold="True"></asp:Label></td>

                                    </tr>

                                </table>
                            </td>
                        </tr>

                        <tr runat="server" id="trmassor" visible="false">
                            <td colspan="3" style="height: 10px;">
                                <table border="1" id="tblUMassor" style="width: 100%;" class="tblsarson">
                                    <tr>
                                        <td style="width: 14%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Foreign matter</td>
                                        <td style="width: 14%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Admixture</td>
                                        <td style="width: 14%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Damaged pulses</td>
                                        <td style="width: 14%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Slightly damaged pulses</td>
                                        <td style="width: 14%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Immature shrivelled pulses</td>
                                        <td style="width: 14%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Weevilled pulses</td>
                                        <td style="width: 14%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Moisture Content</td>

                                    </tr>
                                    <tr>
                                        <td style="width: 14%; text-align: center;">
                                            <asp:Label ID="lbluparmasur_Foreignmatter" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 14%; text-align: center;">
                                            <asp:Label ID="lbluparmasur_admixture" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 14%; text-align: center;">
                                            <asp:Label ID="lbluparmasur_DamagedPulses" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 14%; text-align: center;">
                                            <asp:Label ID="lbluparmasur_sligDamagPulses" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 14%; text-align: center;">
                                            <asp:Label ID="lbluparmasur_ImmaShvldPulses" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 14%; text-align: center;">
                                            <asp:Label ID="lbluparmasur_WeevldPulses" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 14%; text-align: center;">
                                            <asp:Label ID="lbluparMasur_MoistureContent" runat="server" Font-Bold="True"></asp:Label></td>

                                    </tr>

                                </table>
                            </td>
                        </tr>

                        <tr runat="server" id="trchana" visible="false">
                            <td colspan="3" style="height: 10px;">
                                <table border="1" id="tblChanna" style="width: 100%;" class="tblsarson">
                                    <tr>
                                        <td style="width: 12%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Foreign matter</td>
                                        <td style="width: 12%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Other Food Grains</td>
                                        <td style="width: 12%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Damaged Grains</td>
                                        <td style="width: 12%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Slightly damaged touched Grains</td>
                                        <td style="width: 12%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Immature shrivelled & broken grains</td>
                                        <td style="width: 12%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Admixture of other varieties</td>
                                        <td style="width: 12%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Weevilled Grains</td>
                                        <td style="width: 12%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Moisture content</td>

                                    </tr>
                                    <tr>
                                        <td style="width: 12%; text-align: center;">
                                            <asp:Label ID="lbluparGramForeign_Matter" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 12%; text-align: center;">
                                            <asp:Label ID="lbluparGramFoodGrains" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 12%; text-align: center;">
                                            <asp:Label ID="lbluparGram_DamagFoodGrains" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 12%; text-align: center;">
                                            <asp:Label ID="lbluparGram_SligDamagTochedGrains" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 12%; text-align: center;">
                                            <asp:Label ID="lbluparGram_ImmaShrivBroGrains" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 12%; text-align: center;">
                                            <asp:Label ID="lbluparGram_AdmixOtherVarie" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 12%; text-align: center;">
                                            <asp:Label ID="lbluparGram_WeevldGrains" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 12%; text-align: center;">
                                            <asp:Label ID="lbluparGram_MoistureContent" runat="server" Font-Bold="True"></asp:Label></td>

                                    </tr>

                                </table>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3" style="height: 10px;"></td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: center; width: 100%; font-size: small;">जमा विवरण
                                   

                            </td>
                        </tr>
                        <tr>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left">FAQ/Non FAQ
                    <br />

                                <asp:Label ID="Label28" CssClass="lbl" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left">Surveyor Name
                        <br />
                                <asp:Label ID="Label29" CssClass="lbl" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left"> Surveyor Inspection Date
                        <br />
                                <asp:Label ID="Label30" CssClass="lbl" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left">Date of WLC Inspection
                    <br />

                                <asp:Label ID="Label31" CssClass="lbl" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left">Godown
                        <br />
                                <asp:Label ID="Label32" CssClass="lbl" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left">Receive Date
                        <br />
                                <asp:Label ID="Label33" CssClass="lbl" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left">Quantity (Qtls)
                    <br />

                                <asp:Label ID="Label34" CssClass="lbl" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left">Bags
                        <br />
                                <asp:Label ID="Label35" CssClass="lbl" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left">Receiving Godown
                        <br />
                                <asp:Label ID="Label36" CssClass="lbl" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left">Receiveing Date
                    <br />

                                <asp:Label ID="Label37" CssClass="lbl" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left">Acceptance Note 
                        <br />
                                <asp:Label ID="Label38" CssClass="lbl" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left">Date of Acceptance Note 
                        <br />
                                <asp:Label ID="Label39" CssClass="lbl" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left">Depositor Number
                    <br />

                                <asp:Label ID="Label40" CssClass="lbl" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left">Date of Depositer
                        <br />
                                <asp:Label ID="Label41" CssClass="lbl" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left">WHR Number
                        <br />
                                <asp:Label ID="Label42" CssClass="lbl" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left">Date of WHR
                    <br />

                                <asp:Label ID="Label43" CssClass="lbl" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left"></td>
                            <td class="InspColumn" style="padding-left: 20px; text-align: left"></td>
                        </tr>

                        <tr>
                            <td colspan="3" style="height: 10px;">&nbsp;</td>
                        </tr>

                        <tr>
                            <td colspan="3" style="background-color: lightgray; height: 5px; border-radius: 32px;">
                                <center>
                                    <table style="width: 700px">
                                        <tr>
                                            <td style="background-color: darkgrey; height: 5px; border-radius: 32px;"></td>
                                        </tr>
                                    </table>
                                </center>

                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 10px;"></td>
                        </tr>

                        <tr>
                            <td colspan="3" style="height: 10px;">
                                <h2 style="color: #e67e22; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px; text-align: center;">Quality Inspection Parameters (Storage Center)</h2>

                            </td>
                        </tr>
                        <tr id="trGSarson" runat="server" visible="false">
                            <td colspan="3" style="height: 10px;">
                                <table border="1" id="tblGsarson" style="width: 100%;" class="tblsarson">
                                    <tr>
                                        <td style="width: 16%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Impurities/Foreign matter including Taramira</td>
                                        <td style="width: 16%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Admixture with other types including Toria</td>
                                        <td style="width: 16%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Unripe, Shrivelled or Immature</td>
                                        <td style="width: 16%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Damaged & Weevilled</td>
                                        <td style="width: 16%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Small atrophied seeds</td>
                                        <td style="width: 16%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Moisture Content</td>

                                    </tr>
                                    <tr>
                                        <td style="width: 16%; text-align: center;">
                                            <asp:Label ID="Label22" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 16%; text-align: center;">
                                            <asp:Label ID="Label23" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 16%; text-align: center;">
                                            <asp:Label ID="Label24" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 16%; text-align: center;">
                                            <asp:Label ID="Label25" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 16%; text-align: center;">
                                            <asp:Label ID="Label26" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 16%; text-align: center;">
                                            <asp:Label ID="Label27" runat="server" Font-Bold="True"></asp:Label></td>

                                    </tr>

                                </table>
                            </td>
                        </tr>

                        <tr id="trGmassor" runat="server" visible="false">
                            <td colspan="3" style="height: 10px;">
                                <table border="1" id="tblmassor" style="width: 100%;" class="tblsarson">
                                    <tr>
                                        <td style="width: 14%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Foreign matter</td>
                                        <td style="width: 14%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Admixture</td>
                                        <td style="width: 14%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Damaged pulses</td>
                                        <td style="width: 14%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Slightly damaged pulses</td>
                                        <td style="width: 14%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Immature shrivelled pulses</td>
                                        <td style="width: 14%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Weevilled pulses</td>
                                        <td style="width: 14%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Moisture Content</td>

                                    </tr>
                                    <tr>
                                        <td style="width: 16%; text-align: center;">
                                            <asp:Label ID="Label1" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 16%; text-align: center;">
                                            <asp:Label ID="Label2" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 16%; text-align: center;">
                                            <asp:Label ID="Label3" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 16%; text-align: center;">
                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 16%; text-align: center;">
                                            <asp:Label ID="Label5" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 16%; text-align: center;">
                                            <asp:Label ID="Label6" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 16%; text-align: center;">
                                            <asp:Label ID="Label13" runat="server" Font-Bold="True"></asp:Label></td>

                                    </tr>

                                </table>
                            </td>
                        </tr>

                          <tr id="trGChanna" runat="server" visible="false">
                            <td colspan="3" style="height: 10px;">
                                <table border="1" id="lblGChanna" style="width: 100%;" class="tblsarson">
                                    <tr>
                                        <td style="width: 14%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Foreign matter</td>
                                        <td style="width: 14%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Other Food Grains</td>
                                        <td style="width: 14%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Damaged Grains</td>
                                        <td style="width: 14%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Slightly damaged touched Grains</td>
                                        <td style="width: 14%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Immature shrivelled & broken grains</td>
                                        <td style="width: 14%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Admixture of other varieties</td>
                                        <td style="width: 14%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Weevilled Grains</td>
                                        <td style="width: 14%; text-align: center; background-color: #000333; color: #fff; font-size: 12px; letter-spacing: 2px;">Moisture content</td>

                                    </tr>
                                    <tr>
                                        <td style="width: 16%; text-align: center;">
                                            <asp:Label ID="Label7" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 16%; text-align: center;">
                                            <asp:Label ID="Label8" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 16%; text-align: center;">
                                            <asp:Label ID="Label9" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 16%; text-align: center;">
                                            <asp:Label ID="Label10" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 16%; text-align: center;">
                                            <asp:Label ID="Label11" runat="server" Font-Bold="True"></asp:Label></td>
                                        <td style="width: 16%; text-align: center;">
                                            <asp:Label ID="Label12" runat="server" Font-Bold="True"></asp:Label></td>
                                         <td style="width: 16%; text-align: center;">
                                            <asp:Label ID="Label14" runat="server" Font-Bold="True"></asp:Label></td>
                                         <td style="width: 16%; text-align: center;">
                                            <asp:Label ID="Label15" runat="server" Font-Bold="True"></asp:Label></td>

                                    </tr>

                                </table>
                            </td>
                        </tr>

                    </table>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>

