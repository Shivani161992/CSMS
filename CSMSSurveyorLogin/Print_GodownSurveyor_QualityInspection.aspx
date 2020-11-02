<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_GodownSurveyor_QualityInspection.aspx.cs" Inherits="CSMSSurveyorLogin_Print_GodownSurveyor_QualityInspection" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style type="text/css">
        .A4
        {
            width: 210mm; /* You willmay need to reduce this to handle printer margins */
            margin: auto; /* This means it will be centred */
            height: 250mm;
            text-align: justify;
            font-size: medium;
        }

        .LineBreak
        {
            /*page-break-before: always;*/
            height: 0mm;
            overflow: hidden;
        }

        .auto-style1
        {
            width: 100%;
            border-style: solid;
            border-width: 2px;
            border-color: black;
        }

        p
        {
            margin-right: 0in;
            margin-left: 0in;
            font-size: 12.0pt;
            font-family: "Times New Roman","serif";
        }

        @page
        {
            size: auto; /* auto is the current printer page size */
            margin: 25px 25px 0px 30px; /* this affects the margin in the printer settings */
        }

        .auto-style2
        {
            text-decoration: underline;
        }

        .auto-style3
        {
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
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/mpscsc_logo.jpg" Height="120px" Width="120px" />
                    </td>
                    <td style="text-align: center; color: #FF0000;" class="auto-style3">मध्य प्रदेश स्टेट सिविल सप्लाईज कार्पोरेशन लिमिटेड <input type="hidden" id="hdfRejectionNumber" runat="server" /></td>
                    <td rowspan="3" style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0">
                        <asp:Image ID="ImgQRCode" runat="server" Height="125px" Width="125px" />
                    </td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3">Surveyor ....<asp:Label ID="lblSurveyor" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: center" class="auto-style3"><strong>&lt;&lt;<span class="auto-style2"> Godown Surveyor Quality Inspection </span>&gt;&gt;
                        <input id="hdfTruckChallan" type="hidden" runat="server" />

                           <input type="hidden" runat="server" id="hdfGodown" />
                     <input type="hidden" runat="server" id="hdfCommoditiesUparjan" />
                    <input type="hidden" runat="server" id="hdfCommoditiesCSMS" />
                      <input id="hdfOTP" type="hidden" runat="server" />

                      <input id="hdfsocietyDist" type="hidden" runat="server" />
                      <input id="hdfsociety" type="hidden" runat="server" />
                      <input id="hdftransporterid" type="hidden" runat="server" />
                     <input id="hdfbranch" type="hidden" runat="server" />

                    <input id="hdfSurveyorID" type="hidden" runat="server" />
                     <input id="HdfSurveyorName" type="hidden" runat="server" />

                     <input id="hdfStatus" type="hidden" runat="server" />




                                                                       </strong></td>

                </tr>
            </table>
            <table align="center" class="auto-style1">

                <tr>
                    <td style="text-align: center" class="auto-style7" colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: left">Crop Year...<asp:Label ID="lblCropYear" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td style="text-align: right">दिनाँक- ....<asp:Label ID="lblInspectionDate" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>

                <tr>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: right">&nbsp;</td>
                </tr>
                   <tr>
                    <td style="text-align: left">Rejection Number...<asp:Label ID="lblrejectionNum" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td style="text-align: right">Godown...<asp:Label ID="lblGodown" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>
                   <tr>
                    <td style="text-align: left">Truck Challan...<asp:Label ID="lbltruckChallan" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td style="text-align: right">Commodity...<asp:Label ID="lblCommodity" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>
                   <tr>
                    <td style="text-align: left">Society District...<asp:Label ID="lblSocietyDist" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td style="text-align: right">Society...<asp:Label ID="lblsociety" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>
                 <tr>
                    <td style="text-align: left">Date Of Dispatch...<asp:Label ID="lblDateOfDispatch" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td style="text-align: right">Transporter...<asp:Label ID="lbltransporter" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>
                 <tr>
                    <td style="text-align: left">Truck Number...<asp:Label ID="lblTruckNum" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td style="text-align: right">Quantity...<asp:Label ID="lblQuantity" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>
                 <tr>
                    <td style="text-align: left">Bags...<asp:Label ID="lblBags" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td style="text-align: right">Bags Type...<asp:Label ID="lblBagsType" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>
                  <tr>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: right">&nbsp;</td>
                </tr>
                 <tr>
                    <td style="text-align: left">Surveyor Name...<asp:Label ID="lblSurName" runat="server" Font-Bold="True"></asp:Label>....</td>
                    <td style="text-align: right">Mobile Number...<asp:Label ID="lblMobNum" runat="server" Font-Bold="True"></asp:Label>....</td>
                </tr>
                      <tr id="trsarson" runat="server" visible="false">
                <td colspan="3">
                    <center>
                        <table style="width: 100%" class="QIParameters">
                            <tr>
                                <td colspan="4">
                                    <center>

                                        <h2 style="color: Brown; font-size: 16px; font-family: 'Bellefair'; letter-spacing: 2px;">Mustard Seeds (Sarson)</h2>
                                    </center>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%; background-color: #070b30; text-align: left;" class="QIParametersColumn">
                                    <h2 style="color: #FFF; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Special Characteristics</h2>
                                </td>
                               <td style="width: 25%; background-color: #070b30; text-align: center;" class="QIParametersColumn">
                                    <h2 style="color: #FFF; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Maximum Limits Of Tolerance
                                       <br />(% by Weight per qtl.) For FAQ</h2>
                                </td>
                                 <td style="width: 25%; background-color: #070b30; text-align: center;" class="QIParametersColumn">
                                    <h2 style="color: #FFF; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Surveyor present at Societies</h2>
                                </td>
                                <td style="width: 25%; background-color: #070b30; text-align: center;" class="QIParametersColumn">
                                    <h2 style="color: #FFF; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Surveyor present at Godown</h2>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Impurities/Foreign matter including Taramira</h2>

                                </td>
                                <td style="text-align: center;">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblIFM_IncTaramira" runat="server" Text="2" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>

                                 <td style="text-align: center;">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparFM_IncTaramira" runat="server" Text="uparFM_IncTaramira" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>

                                <td style="text-align: center;">
                                     <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGdnFM_IncTaramira" runat="server" Text="GdnFM_IncTaramira" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>
                                </td>
                            </tr>

                            <tr>
                                 <td style="text-align: left; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Admixture with other types including Toria</h2>

                                </td>

                                 <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblAM_OT_Toria" runat="server" Text="10" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>

                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparAM_OT_Toria" runat="server" Text="uparAM_OT_Toria" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                       <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGDNAM_OT_Toria" runat="server" Text="GDNAM_OT_Toria" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>
                                </td>
                            </tr>


                            <tr>
                                 <td style="text-align: left; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Unripe, Shrivelled or Immature</h2>

                                </td>

                                 <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblUR_Shvld_Imm" runat="server" Text="4" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                 <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparUR_Shvld_Imm" runat="server" Text="uparUR_Shvld_Imm" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                  <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGDNUR_Shvld_Imm" runat="server" Text="GDNUR_Shvld_Imm" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>
                                </td>
                            </tr>

                            <tr>
                                <td style="text-align: left; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Damaged & Weevilled</h2>

                                </td>
                                 <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblDamWeevd" runat="server" Text="2" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparDamWeevd" runat="server" Text="uparDamWeevd" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGDNDamWeevd" runat="server" Text="GDNDamWeevd" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>
                                </td>
                            </tr>
                            <tr>
                               <td style="text-align: left; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Small atrophied seeds</h2>

                                </td>
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblSmallAtroSeeds" runat="server" Text="10" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparSmallAtroSeeds" runat="server" Text="uparSmallAtroSeeds" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                     <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGDNSmallAtroSeeds" runat="server" Text="GDNSmallAtroSeeds" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>
                                </td>
                            </tr>

                            <tr>
                                <td style="text-align: left; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Moisture Content</h2>

                                </td>
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblMoisCont" runat="server" Text="8" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparMoisCont" runat="server" Text="uparMoisCont" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                     <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGDNMoisCont" runat="server" Text="GDNMoisCont" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>
                                </td>
                            </tr>
                             <tr>
                                <td colspan="4" style="text-align: center;">
                                    </td></tr>
                        
                        </table>
                    </center>
                </td>
            </tr>

            <tr id="trmasur" runat="server" visible="false">
                <td colspan="3">
                    <center>
                        <table style="width: 100%" class="QIParameters">
                            <tr>
                                <td colspan="4">
                                    <center>


                                        <h2 style="color: #070b30; font-size: 16px; font-family: 'Bellefair'; letter-spacing: 2px;">Red Lentils (Massur)</h2>
                                    </center>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%; background-color: #070b30; text-align: left;" class="QIParametersColumn">
                                    <h2 style="color: #FFF; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Special Characteristics</h2>
                                </td>
                                <td style="width: 25%; background-color: #070b30; text-align: center;" class="QIParametersColumn">
                                    <h2 style="color: #FFF; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Maximum Limits Of Tolerance
                                       <br />
                                        (% by Weight per qtl.) For FAQ</h2>
                                </td>
                                 <td style="width: 25%; background-color: #070b30; text-align: center;" class="QIParametersColumn">
                                    <h2 style="color: #FFF; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Surveyor present at Societies</h2>
                                </td>
                                 <td style="width: 25%; background-color: #070b30; text-align: center;" class="QIParametersColumn">
                                    <h2 style="color: #FFF; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Surveyor present at Godown</h2>
                                </td>
                            </tr>
                            <tr>
                               <td style="text-align: left; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Foreign matter</h2>

                                </td>
                                 <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblmasur_Foreignmatter" runat="server" Text="2.0" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparmasur_Foreignmatter" runat="server" Text="uparmasur_Foreignmatter" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                   <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="GDNmasur_Foreignmatter" runat="server" Text="GDNmasur_Foreignmatter" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>
                                </td>
                            </tr>

                            <tr>
                                 <td style="text-align: left; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Admixture</h2>

                                </td>
                                 <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblmasur_admixture" runat="server" Text="3.0" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparmasur_admixture" runat="server" Text="uparmasur_admixture" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGDNmasur_admixture" runat="server" Text="GDNmasur_admixture" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>
                                </td>
                            </tr>
                            <tr>
                               <td style="text-align: left; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Damaged pulses</h2>

                                </td>
                                 <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblmasur_DamagedPulses" runat="server" Text="3.0" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                 <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparmasur_DamagedPulses" runat="server" Text="uparmasur_DamagedPulses" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                   <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGDNmasur_DamagedPulses" runat="server" Text="GDNmasur_DamagedPulses" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>
                                </td>
                            </tr>
                            <tr>
                               <td style="text-align: left; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Slightly damaged pulses</h2>

                                </td>
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblmasur_sligDamagPulses" runat="server" Text="4.0" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                 <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparmasur_sligDamagPulses" runat="server" Text="uparmasur_sligDamagPulses" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGDNmasur_sligDamagPulses" runat="server" Text="GDNmasur_sligDamagPulses" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Immature shrivelled pulses</h2>

                                </td>
                                 <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblmasur_ImmaShvldPulses" runat="server" Text="3.0" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparmasur_ImmaShvldPulses" runat="server" Text="uparmasur_ImmaShvldPulses" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                  <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="GDNmasur_ImmaShvldPulses" runat="server" Text="GDNmasur_ImmaShvldPulses" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                            </tr>

                            <tr>
                                 <td style="text-align: left; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Weevilled pulses</h2>

                                </td>
                                 <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblmasur_WeevldPulses" runat="server" Text="4.0" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparmasur_WeevldPulses" runat="server" Text="lbluparmasur_WeevldPulses" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="GDNrmasur_WeevldPulses" runat="server" Text="GDNrmasur_WeevldPulses" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>
                                </td>
                            </tr>
                            <tr>
                               <td style="text-align: left; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Moisture Content</h2>

                                </td>
                                 <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblMasur_MoistureContent" runat="server" Text="12.0" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                   <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparMasur_MoistureContent" runat="server" Text="uparMasur_MoistureContent" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="GDNMasur_MoistureContent" runat="server" Text="GDNMasur_MoistureContent" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>
                                </td>
                            </tr>
                             <tr>
                                <td colspan="4" style="text-align: center;">
                                    </td></tr>
                           
                        </table>
                    </center>
                </td>
            </tr>

            <tr id="trgram" runat="server" visible="false">
                <td colspan="3">
                    <center>
                        <table style="width: 100%" class="QIParameters" border="1">
                            <tr>
                                <td colspan="4">
                                    <center>
                                          <h2 style="color: #070b30; font-size: 16px; font-family: 'Bellefair'; letter-spacing: 2px;"> Gram (Channa)</h2>
                                    </center>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%; background-color: #070b30; text-align: left;" class="QIParametersColumn">
                                    <h2 style="color: #FFF; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Special Characteristics</h2>
                                </td>
                                <td style="width: 25%; background-color: #070b30; text-align: center;" class="QIParametersColumn">
                                    <h2 style="color: #FFF; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Maximum permissible Limits Of different refractions 
                                      <br /> (% by Weight per qtl.) For FAQ</h2>
                                </td>
                                 <td style="width: 25%; background-color: #070b30; text-align: center;" class="QIParametersColumn">
                                    <h2 style="color: #FFF; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Surveyor present at Societies</h2>
                                </td>
                                 <td style="width: 25%; background-color: #070b30; text-align: center;" class="QIParametersColumn">
                                    <h2 style="color: #FFF; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Surveyor present at Godown</h2>
                                </td>
                            </tr>
                             <tr>
                                 <td style="text-align: left; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Foreign matter</h2>

                                </td>
                                 <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGramForeign_Matter" runat="server" Text="1.0" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparGramForeign_Matter" runat="server" Text="uparGramForeign_Matter" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                   <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGDNGramForeign_Matter" runat="server" Text="GDNGramForeign_Matter" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>
                                </td>
                            </tr>
                              <tr>
                               <td style="text-align: left; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Other Food Grains</h2>

                                </td>
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGramFoodGrains" runat="server" Text="3.0" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparGramFoodGrains" runat="server" Text="lbluparGramFoodGrains" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                   <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGDNGramFoodGrains" runat="server" Text="GDNGramFoodGrains" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>
                                </td>
                            </tr>
                             <tr>
                                 <td style="text-align: left; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Damaged Grains</h2>

                                </td>
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGram_DamagFoodGrains" runat="server" Text="3.0" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparGram_DamagFoodGrains" runat="server" Text="uparGram_DamagFoodGrains" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                     <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGDNGram_DamagFoodGrains" runat="server" Text="GDNGram_DamagFoodGrains" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                            </tr>
                             <tr>
                                 <td style="text-align: left; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;"> Slightly damaged touched Grains</h2>

                                </td>
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGram_SligDamagTochedGrains" runat="server" Text="4.0" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                   <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparGram_SligDamagTochedGrains" runat="server" Text="uparGram_SligDamagTochedGrains" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGDNGram_SligDamagTochedGrains" runat="server" Text="GDNGram_SligDamagTochedGrains" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>
                                </td>
                            </tr>
                             <tr>
                                <td style="text-align: left; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;"> Immature shrivelled & broken grains</h2>

                                </td>
                                   <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGram_ImmaShrivBroGrains" runat="server" Text="6.0" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                   <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparGram_ImmaShrivBroGrains" runat="server" Text="uparGram_ImmaShrivBroGrains" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGDNGram_ImmaShrivBroGrains" runat="server" Text="GDNGram_ImmaShrivBroGrains" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>
                                </td>
                            </tr>

                             <tr>
                               <td style="text-align: left; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;"> Admixture of other varieties</h2>

                                </td>
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGram_AdmixOtherVarie" runat="server" Text="5.0" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparGram_AdmixOtherVarie" runat="server" Text="uparGram_AdmixOtherVarie" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>

                                <td style="text-align: center;">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGDNGram_AdmixOtherVarie" runat="server" Text="GDNGram_AdmixOtherVarie" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>
                                </td>
                            </tr>
                            <tr>
                               <td style="text-align: left; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;"> Weevilled Grains</h2>

                                </td>
                                 <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGram_WeevldGrains" runat="server" Text="4.0" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparGram_WeevldGrains" runat="server" Text="uparGram_WeevldGrains" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGDNGram_WeevldGrains" runat="server" Text="GDNGram_WeevldGrains" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>
                                </td>
                            </tr>

                              <tr>
                                <td style="text-align: left; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;"> Moisture content</h2>

                                </td>
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGram_MoistureContent" runat="server" Text="14.0" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparGram_MoistureContent" runat="server" Text="uparGram_MoistureContent" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                     <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGDNGram_MoistureContent" runat="server" Text="GDNGram_MoistureContent" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>
                                </td>
                            </tr>
                               
                           
                        </table>
                    </center>
                </td>
            </tr>
        </div>
    </form>
</body>
</html>
