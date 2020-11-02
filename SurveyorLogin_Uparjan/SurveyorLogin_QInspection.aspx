<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SurveyorLogin_QInspection.aspx.cs" Inherits="SurveyorLogin_Uparjan_SurveyorLogin_QInspection" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .QIParametersColumn
        {
            border-radius: 8px;
        }

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
                border:none;
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

        .insptxtPara
        {
            width: 150px;
            height: 20px;
            letter-spacing: 2px;
            font-family: sans-serif;
            font-size: 12px;
            border-color: #03a9f4;
            border: 1px solid #03a9f4;
            border-radius: 8px;
            color: black;
            border-bottom-style: groove;
            text-align: center;
        }
         .insptxtPara:focus
            {
                border: none;
                border-bottom-color: #070b30;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
         <center>
        <table style="width:100%;">
            <tr>
                <td  style="width:100%; text-align:right">
                  <asp:LinkButton ID="LinkButton2" runat="server" Font-Bold="False" ForeColor="black" CssClass="sign" PostBackUrl="#" 
                      CausesValidation="False">Log Out</asp:LinkButton>
                </td>
            </tr>
        </table>
    </center>
        <div>
            <center>
        <table style="width: 1100px; font-size: 12px;">
            <tr>
                <td style="text-align: left; width: 200px">
                    <a href="SurveyorLogin_Welcome.aspx" class="sign">&#9754 Back
                    </a>
                </td>
                <td style="text-align: center; width: 700px">
                    <h2 style="color: #e74c3c; font-size: 20px; font-family: 'Bellefair'; letter-spacing: 4px;">Surveyor Quality Inspection</h2>
                    <input type="hidden" runat="server" id="hdfDist" />
                    <input type="hidden" runat="server" id="hdfInspID" />
                   
                </td>

                <td style="text-align: right; width: 200px">
                    <a href="SurveyorLogin_QInspection.aspx" class="sign">&#8635 New
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
                <td class="InspColumn" style="padding-left: 20px; text-align: left">Commodity
                    <br />

                    <asp:DropDownList ID="ddlcommodities" runat="server" CssClass="inspddl" OnSelectedIndexChanged="ddlcommodities_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" Style="font-size: 12px;"
                        ControlToValidate="ddlcommodities" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px; text-align: left"></td>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">Truck Challan
                    <br />

                    <asp:DropDownList ID="ddlTruckChallan" runat="server" CssClass="inspddl" OnSelectedIndexChanged="ddlTruckChallan_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Style="font-size: 12px;"
                        ControlToValidate="ddlTruckChallan" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
            </tr>
               <tr>
                   <td class="InspColumn" style="padding-left: 20px; text-align: left">Date
                    <br />

                    <asp:TextBox ID="txtdate" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtdate" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">Transporter
                    <br />

                    <asp:TextBox ID="txttransporter" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txttransporter" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">

                    Truck number 
                    <br />

                    <asp:TextBox ID="txttrucknumber" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txttrucknumber" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                
            </tr>
             <tr>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">Quantity
                    <br />

                   <asp:TextBox ID="txtqty" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtqty" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">

                    Bags 
                    <br />

                  <asp:TextBox ID="txtbags" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtbags" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">Bags Type
                    <br />

                   <asp:TextBox ID="txtbagstype" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtbagstype" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
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
               <tr id="trsarson" runat="server" visible="false">
                <td colspan="3">
                    <center>
                        <table style="width: 100%" class="QIParameters">
                            <tr>
                                <td colspan="3">
                                    <center>

                                        <h2 style="color: #070b30; font-size: 16px; font-family: 'Bellefair'; letter-spacing: 2px;">Mustard Seeds (Sarson)</h2>
                                    </center>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 33%; background-color: #070b30; text-align: center;" class="QIParametersColumn">
                                    <h2 style="color: #FFF; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Special Characteristics</h2>
                                </td>
                               <td style="width: 33%; background-color: #070b30; text-align: center;" class="QIParametersColumn">
                                    <h2 style="color: #FFF; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Maximum Limits Of Tolerance
                                       <br />(% by Weight per qtl.) For FAQ</h2>
                                </td>
                                <td style="width: 33%; background-color: #070b30; text-align: center;" class="QIParametersColumn">
                                    <h2 style="color: #FFF; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Found Parameters</h2>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; padding-left: 80px">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Impurities/Foreign matter including Taramira</h2>

                                </td>
                                <td style="text-align: center;">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblIFM_IncTaramira" runat="server" Text="2" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>

                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtIFM_IncTaramira" CssClass="insptxtPara" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Style="font-size: 12px;"
                                        ControlToValidate="txtIFM_IncTaramira" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>

                            <tr>
                                <td style="text-align: left; padding-left: 80px">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Admixture with other types including Toria</h2>

                                </td>

                                 <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblAM_OT_Toria" runat="server" Text="10" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtAM_OT_Toria" CssClass="insptxtPara" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Style="font-size: 12px;"
                                        ControlToValidate="txtAM_OT_Toria" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>


                            <tr>
                                <td style="text-align: left; padding-left: 80px">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Unripe, Shrivelled or Immature</h2>

                                </td>

                                 <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblUR_Shvld_Imm" runat="server" Text="4" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtUR_Shvld_Imm" CssClass="insptxtPara" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Style="font-size: 12px;"
                                        ControlToValidate="txtUR_Shvld_Imm" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>

                            <tr>
                                <td style="text-align: left; padding-left: 80px">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Damaged & Weevilled</h2>

                                </td>
                                 <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblDamWeevd" runat="server" Text="2" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtDamWeevd" CssClass="insptxtPara" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Style="font-size: 12px;"
                                        ControlToValidate="txtDamWeevd" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; padding-left: 80px">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Small atrophied seeds</h2>

                                </td>
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblSmallAtroSeeds" runat="server" Text="10" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtSmallAtroSeeds" CssClass="insptxtPara" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" Style="font-size: 12px;"
                                        ControlToValidate="txtSmallAtroSeeds" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>

                            <tr>
                                <td style="text-align: left; padding-left: 80px">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Moisture Content</h2>

                                </td>
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblMoisCont" runat="server" Text="8" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtMoisCont" CssClass="insptxtPara" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" Style="font-size: 12px;"
                                        ControlToValidate="txtMoisCont" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                             <tr>
                                <td colspan="3" style="text-align: center;">
                                    </td></tr>
                            <tr>
                                <td colspan="3" style="text-align: center;">
                                                                 <asp:Button ID="btnsarsonQualityinspection" runat="server" Text="Quality Inspection" CssClass="bttsub" Visible="true" OnClick="btnsarsonQualityinspection_Click" Enabled="false" />
    </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                                                        <asp:Button ID="btnsarsonaccept" runat="server" Text="Accept" CssClass="bttsubother" Visible="true" Enabled="false" OnClick="btnsarsonaccept_Click" />

                                </td>
                                 <td style="text-align: center;">

                                </td>
                                 <td style="text-align: left;">
                                                                                                             <asp:Button ID="btnsarsonreject" runat="server" Text="Reject" CssClass="bttsubother" Visible="true" Enabled="false" OnClick="btnsarsonreject_Click" />

                                </td>
                            </tr>
                        </table>
                    </center>
                </td>
            </tr>

            <tr id="trmasur" runat="server" visible="false">
                <td colspan="3">
                    <center>
                        <table style="width: 100%" class="QIParameters">
                            <tr>
                                <td colspan="3">
                                    <center>


                                        <h2 style="color: #070b30; font-size: 16px; font-family: 'Bellefair'; letter-spacing: 2px;">Red Lentils (Massur)</h2>
                                    </center>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 33%; background-color: #070b30; text-align: center;" class="QIParametersColumn">
                                    <h2 style="color: #FFF; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Special Characteristics</h2>
                                </td>
                                <td style="width: 33%; background-color: #070b30; text-align: center;" class="QIParametersColumn">
                                    <h2 style="color: #FFF; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Maximum Limits Of Tolerance
                                       <br />
                                        (% by Weight per qtl.) For FAQ</h2>
                                </td>
                                 <td style="width: 33%; background-color: #070b30; text-align: center;" class="QIParametersColumn">
                                    <h2 style="color: #FFF; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Found Parameters</h2>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; padding-left: 80px">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Foreign matter</h2>

                                </td>
                                 <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblmasur_Foreignmatter" runat="server" Text="2.0" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtmasur_Foreignmatter" CssClass="insptxtPara" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" Style="font-size: 12px;"
                                        ControlToValidate="txtmasur_Foreignmatter" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>

                            <tr>
                                <td style="text-align: left; padding-left: 80px">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Admixture</h2>

                                </td>
                                 <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblmasur_admixture" runat="server" Text="3.0" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtmasur_admixture" CssClass="insptxtPara" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" Style="font-size: 12px;"
                                        ControlToValidate="txtmasur_admixture" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; padding-left: 80px">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Damaged pulses</h2>

                                </td>
                                 <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblmasur_DamagedPulses" runat="server" Text="3.0" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtmasur_DamagedPulses" CssClass="insptxtPara" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" Style="font-size: 12px;"
                                        ControlToValidate="txtmasur_DamagedPulses" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; padding-left: 80px">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Slightly damaged pulses</h2>

                                </td>
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblmasur_sligDamagPulses" runat="server" Text="4.0" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtmasur_sligDamagPulses" CssClass="insptxtPara" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" Style="font-size: 12px;"
                                        ControlToValidate="txtmasur_sligDamagPulses" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; padding-left: 80px">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Immature shrivelled pulses</h2>

                                </td>
                                 <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblmasur_ImmaShvldPulses" runat="server" Text="3.0" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtmasur_ImmaShvldPulses" CssClass="insptxtPara" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" Style="font-size: 12px;"
                                        ControlToValidate="txtmasur_ImmaShvldPulses" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>

                            <tr>
                                <td style="text-align: left; padding-left: 80px">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Weevilled pulses</h2>

                                </td>
                                 <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblmasur_WeevldPulses" runat="server" Text="4.0" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtmasur_WeevldPulses" CssClass="insptxtPara" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" Style="font-size: 12px;"
                                        ControlToValidate="txtmasur_WeevldPulses" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; padding-left: 80px">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Moisture Content</h2>

                                </td>
                                 <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblMasur_MoistureContent" runat="server" Text="12.0" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtMasur_MoistureContent" CssClass="insptxtPara" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" Style="font-size: 12px;"
                                        ControlToValidate="txtMasur_MoistureContent" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                             <tr>
                                <td colspan="3" style="text-align: center;">
                                    </td></tr>
                            <tr>
                                <td colspan="3" style="text-align: center;">
                                                                 <asp:Button ID="btnmassurQualityInspection" runat="server" Text="Quality Inspection" CssClass="bttsub" Visible="true" OnClick="btnmassurQualityInspection_Click" Enabled="false" />
    </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                                                        <asp:Button ID="btnmassurAccpet" runat="server" Text="Accept" CssClass="bttsubother" Visible="true" Enabled="false" OnClick="btnmassurAccpet_Click" />

                                </td>
                                 <td style="text-align: center;">

                                </td>
                                 <td style="text-align: left;">
                                                                                                             <asp:Button ID="btnmassurReject" runat="server" Text="Reject" CssClass="bttsubother" Visible="true" Enabled="false" OnClick="btnmassurReject_Click" />

                                </td>
                            </tr>
                        </table>
                    </center>
                </td>
            </tr>

            <tr id="trgram" runat="server" visible="false">
                <td colspan="3">
                    <center>
                        <table style="width: 100%" class="QIParameters">
                            <tr>
                                <td colspan="3">
                                    <center>
                                          <h2 style="color: #070b30; font-size: 16px; font-family: 'Bellefair'; letter-spacing: 2px;"> Gram (Channa)</h2>
                                    </center>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 33%; background-color: #070b30; text-align: center;" class="QIParametersColumn">
                                    <h2 style="color: #FFF; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Special Characteristics</h2>
                                </td>
                                <td style="width: 33%; background-color: #070b30; text-align: center;" class="QIParametersColumn">
                                    <h2 style="color: #FFF; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Maximum permissible Limits Of different refractions 
                                      <br /> (% by Weight per qtl.) For FAQ</h2>
                                </td>
                                 <td style="width: 33%; background-color: #070b30; text-align: center;" class="QIParametersColumn">
                                    <h2 style="color: #FFF; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Found Parameters</h2>
                                </td>
                            </tr>
                             <tr>
                                <td style="text-align: left; padding-left: 80px">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Foreign matter</h2>

                                </td>
                                 <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGramForeign_Matter" runat="server" Text="1.0" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtGramForeign_Matter" CssClass="insptxtPara" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" Style="font-size: 12px;"
                                        ControlToValidate="txtGramForeign_Matter" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                              <tr>
                                <td style="text-align: left; padding-left: 80px">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Other Food Grains</h2>

                                </td>
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGramFoodGrains" runat="server" Text="3.0" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtGramFoodGrains" CssClass="insptxtPara" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" Style="font-size: 12px;"
                                        ControlToValidate="txtGramFoodGrains" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                             <tr>
                                <td style="text-align: left; padding-left: 80px">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">Damaged Grains</h2>

                                </td>
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGram_DamagFoodGrains" runat="server" Text="3.0" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtGram_DamagFoodGrains" CssClass="insptxtPara" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" Style="font-size: 12px;"
                                        ControlToValidate="txtGram_DamagFoodGrains" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                             <tr>
                                <td style="text-align: left; padding-left: 80px">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;"> Slightly damaged touched Grains</h2>

                                </td>
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGram_SligDamagTochedGrains" runat="server" Text="4.0" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtGram_SligDamagTochedGrains" CssClass="insptxtPara" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" Style="font-size: 12px;"
                                        ControlToValidate="txtGram_SligDamagTochedGrains" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                             <tr>
                                <td style="text-align: left; padding-left: 80px">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;"> Immature shrivelled & broken grains</h2>

                                </td>
                                   <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGram_ImmaShrivBroGrains" runat="server" Text="6.0" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtGram_ImmaShrivBroGrains" CssClass="insptxtPara" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" Style="font-size: 12px;"
                                        ControlToValidate="txtGram_ImmaShrivBroGrains" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>

                             <tr>
                                <td style="text-align: left; padding-left: 80px">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;"> Admixture of other varieties</h2>

                                </td>
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGram_AdmixOtherVarie" runat="server" Text="5.0" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>

                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtGram_AdmixOtherVarie" CssClass="insptxtPara" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" Style="font-size: 12px;"
                                        ControlToValidate="txtGram_AdmixOtherVarie" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; padding-left: 80px">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;"> Weevilled Grains</h2>

                                </td>
                                 <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGram_WeevldGrains" runat="server" Text="4.0" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtGram_WeevldGrains" CssClass="insptxtPara" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" Style="font-size: 12px;"
                                        ControlToValidate="txtGram_WeevldGrains" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>

                              <tr>
                                <td style="text-align: left; padding-left: 80px">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;"> Moisture content</h2>

                                </td>
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lblGram_MoistureContent" runat="server" Text="14.0" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtGram_MoistureContent" CssClass="insptxtPara" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" Style="font-size: 12px;"
                                        ControlToValidate="txtGram_MoistureContent" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                                <tr>
                                <td colspan="3" style="text-align: center;">
                                                                 <asp:Button ID="btnGramQualityInspection" runat="server" Text="Quality Inspection" CssClass="bttsub" Visible="true" OnClick="btnGramQualityInspection_Click" Enabled="false" />
    </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                                                        <asp:Button ID="btngramAccept" runat="server" Text="Accept" CssClass="bttsubother" Visible="true" Enabled="false" OnClick="btngramAccept_Click" />

                                </td>
                                 <td style="text-align: center;">

                                </td>
                                 <td style="text-align: left;">
                                                                                                             <asp:Button ID="btnGramReject" runat="server" Text="Reject" CssClass="bttsubother" Visible="true" Enabled="false" OnClick="btnGramReject_Click" />

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
