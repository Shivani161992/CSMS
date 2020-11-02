<%@ Page Title="" Language="C#" MasterPageFile="~/CSMSSurveyorLogin/Godown_SurveyorMaster.master" AutoEventWireup="true" CodeFile="GodownSurveyor_QualityInspection.aspx.cs" Inherits="CSMSSurveyorLogin_GodownSurveyor_QualityInspection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>
    <script type="text/javascript">

       
        function TimerFunc() {
            var c = 300;
            var t;
            timedCount();

            function timedCount() {

                var hours = parseInt(c / 3600) % 24;
                var minutes = parseInt(c / 60) % 60;
                var seconds = c % 60;

                //var result = (hours < 10 ? "0" + hours : hours) + ":" + (minutes < 10 ? "0" + minutes : minutes) + ":" + (seconds < 10 ? "0" + seconds : seconds);
                var result = (minutes < 10 ? "0" + minutes : minutes) + ":" + (seconds < 10 ? "0" + seconds : seconds);

                $('#timer').html(result);
                if (c == 0) {
                    //setConfirmUnload(false);
                    //$("#quiz_form").submit();
                    window.location = "GodownSurveyor_QualityInspection.aspx";
                }
                c = c - 1;
                t = setTimeout(function () {
                    timedCount()
                },
                1000);

                var GetLabelValue = document.getElementById('ctl00_ContentPlaceHolder1_lblmsg').innerText;
                if (GetLabelValue == 'Success') {
                    c = -500;
                }
            }
        }

        $(document).ready(function () {
            $('#ctl00_ContentPlaceHolder1_ChkOTP').click(function () {

                var GenerateOTP = "<%=this.GenerateOTP%>";
                var EnteredOTP = document.getElementById('ctl00_ContentPlaceHolder1_txtOTP').value;

                //alert(GenerateOTP);

                if (EnteredOTP == GenerateOTP) {
                    //alert('Success');
                    document.getElementById("ctl00_ContentPlaceHolder1_lblmsg").innerHTML = 'Success';
                    document.getElementById("ctl00_ContentPlaceHolder1_lblmsg").style.color = "#00CC00";
                    document.getElementById("ctl00_ContentPlaceHolder1_txtOTP").disabled = true;
                    document.getElementById("ctl00_ContentPlaceHolder1_btnAccept").disabled = false;
                    document.getElementById("ctl00_ContentPlaceHolder1_btnReject").disabled = false;
                    document.getElementById("ctl00_ContentPlaceHolder1_ChkOTP").disabled = true;

                    $('#timer').hide()
                }
                else {
                    //alert('Failed');
                    document.getElementById("ctl00_ContentPlaceHolder1_lblmsg").innerHTML = 'Failed';
                    document.getElementById("ctl00_ContentPlaceHolder1_lblmsg").style.color = "Red";

                    document.getElementById("ctl00_ContentPlaceHolder1_btnAccept").disabled = true;
                    document.getElementById("ctl00_ContentPlaceHolder1_btnReject").disabled = true;
                }

            });
        });
    </script>

     <script type="text/javascript">


         function TimerFuncResend() {
             var c = 600;
             var t;
             timedCount();

             function timedCount() {

                 var hours = parseInt(c / 3600) % 24;
                 var minutes = parseInt(c / 60) % 60;
                 var seconds = c % 60;

                 //var result = (hours < 10 ? "0" + hours : hours) + ":" + (minutes < 10 ? "0" + minutes : minutes) + ":" + (seconds < 10 ? "0" + seconds : seconds);
                 var result = (minutes < 10 ? "0" + minutes : minutes) + ":" + (seconds < 10 ? "0" + seconds : seconds);

                 $('#timer').html(result);
                 if (c == 0) {
                     //setConfirmUnload(false);
                     //$("#quiz_form").submit();
                     window.location = "GodownSurveyor_QualityInspection.aspx";
                 }
                 c = c - 1;
                 t = setTimeout(function () {
                     timedCount()
                 },
                 1000);

                 var GetLabelValue = document.getElementById('ctl00_ContentPlaceHolder1_lblmsg').innerText;
                 if (GetLabelValue == 'Success') {
                     c = -500;
                 }
             }
         }

         $(document).ready(function () {
             $('#ctl00_ContentPlaceHolder1_ChkOTP').click(function () {

                 var GenerateOTP = "<%=this.GenerateOTP%>";
                var EnteredOTP = document.getElementById('ctl00_ContentPlaceHolder1_txtOTP').value;

                //alert(GenerateOTP);

                if (EnteredOTP == GenerateOTP) {
                    //alert('Success');
                    document.getElementById("ctl00_ContentPlaceHolder1_lblmsg").innerHTML = 'Success';
                    document.getElementById("ctl00_ContentPlaceHolder1_lblmsg").style.color = "#00CC00";
                    document.getElementById("ctl00_ContentPlaceHolder1_txtOTP").disabled = true;
                    document.getElementById("ctl00_ContentPlaceHolder1_btnAccept").disabled = false;
                    document.getElementById("ctl00_ContentPlaceHolder1_btnReject").disabled = false;
                    document.getElementById("ctl00_ContentPlaceHolder1_ChkOTP").disabled = true;

                    $('#timer').hide()
                }
                else {
                    //alert('Failed');
                    document.getElementById("ctl00_ContentPlaceHolder1_lblmsg").innerHTML = 'Failed';
                    document.getElementById("ctl00_ContentPlaceHolder1_lblmsg").style.color = "Red";

                    document.getElementById("ctl00_ContentPlaceHolder1_btnAccept").disabled = true;
                    document.getElementById("ctl00_ContentPlaceHolder1_btnReject").disabled = true;
                }

            });
        });
    </script>
    <style>
        .sign
        {
            color: #062946;
            font-size: 15px;
            text-decoration: none;
            letter-spacing: 2px;
        }

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
            width: 100%;
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
            border-color: brown;
            border: 1px solid brown;
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
            border-color: brown;
            box-shadow: 0 0 10px brown;
            outline: none;
        }

        .inspddl
        {
            width: 310px;
            height: 24px;
            letter-spacing: 2px;
            font-family: sans-serif;
            font-size: 12px;
            border-color: brown;
            border: 1px solid brown;
            border-radius: 8px;
            padding-left: 10px;
            border-bottom-style: groove;
        }

            .inspddl:focus
            {
                box-shadow: 0 0 10px brown;
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
    <center>
        <table style="width: 100%; font-size: 12px;">
            <tr>
                <td colspan="3">

                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 10%">
                    <a href="CSMS_SurveyorLogin_Welcome.aspx" class="sign">&#9754 Back
                    </a>
                </td>
                <td style="text-align: center; width: 80%">
                    <h2 style="color: #e74c3c; font-size: 20px; font-family: 'Bellefair'; letter-spacing: 4px;"> Godown Surveyor Quality Inspection</h2>
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

                    <input id="hdfOTPCount" type="hidden" runat="server" />
                      <input id="hdfOTPFirstOTPTime" type="hidden" runat="server" />






                   
                   

                   
                   
                </td>

                <td style="text-align: right; width: 10%">
                    <a href="GodownSurveyor_QualityInspection.aspx" class="sign">&#8635 New
                    </a>
                </td>
            </tr>
        </table>
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
                    <td class="InspColumn" style="padding-left: 20px; text-align: center">
                   Godown
                    <br />

                  <asp:DropDownList ID="ddlGodown" runat="server" CssClass="inspddl" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" Style="font-size: 12px;"
                        ControlToValidate="ddlGodown" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                    <td class="InspColumn" style="padding-left: 20px; text-align: center">Truck Challan
                    <br />

                    <asp:DropDownList ID="ddlTruckChallan" runat="server" CssClass="inspddl" OnSelectedIndexChanged="ddlTruckChallan_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Style="font-size: 12px;"
                        ControlToValidate="ddlTruckChallan" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px; text-align: center">Commodity
                    <br />

                    <asp:TextBox ID="txtCommodities" CssClass="insptxt" runat="server" ReadOnly="true" ></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtCommodities" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
               
               
            </tr>

               <tr>
                  <td class="InspColumn" style="padding-left: 20px; text-align: center">
Society District
                    <br />

                    <asp:TextBox ID="txtSendDist" CssClass="insptxt" runat="server" ReadOnly="true" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtSendDist" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                   </td>
                  <td class="InspColumn" style="padding-left: 20px; text-align: center">
Society
                    <br />

                    <asp:TextBox ID="txtSociety" CssClass="insptxt" runat="server" ReadOnly="true" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtSociety" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                   </td>
                  <td class="InspColumn" style="padding-left: 20px; text-align: center">
Crop Year
                    <br />

                    <asp:TextBox ID="txtCropYear" CssClass="insptxt" runat="server" ReadOnly="true" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtCropYear" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                   </td>
               </tr>

               <tr>
                   <td class="InspColumn" style="padding-left: 20px; text-align: center">Date of Dispatch
                    <br />

                    <asp:TextBox ID="txtdate" CssClass="insptxt" runat="server" ReadOnly="true" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtdate" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px; text-align: center">Transporter
                    <br />

                    <asp:TextBox ID="txttransporter" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtSociety" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px; text-align: center">

                    Truck number 
                    <br />

                    <asp:TextBox ID="txttrucknumber" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtSendDist" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                
            </tr>
             <tr>
                <td class="InspColumn" style="padding-left: 20px; text-align: center">Quantity
                    <br />

                   <asp:TextBox ID="txtqty" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtqty" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px; text-align: center">

                    Bags 
                    <br />

                  <asp:TextBox ID="txtbags" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtbags" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px; text-align: center">Bags Type
                    <br />

                   <asp:TextBox ID="txtbagstype" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtbagstype" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
            </tr>
               <tr>
                     <td class="InspColumn" style="padding-left: 20px; text-align: center">Date of Inspection
                    <br />

                   <asp:TextBox ID="txtDateOfInsp" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtDateOfInsp" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                      <td class="InspColumn" style="padding-left: 20px; text-align: center">Status
                    <br />

                   <asp:TextBox ID="txtStatus" CssClass="insptxt" runat="server" Style="text-align:center;" ReadOnly="true"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtStatus" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                     <td class="InspColumn" style="padding-left: 20px; text-align: center; width:25%;">
                Mobile Number
                    <br />

                    <asp:TextBox ID="txtMobNum" CssClass="insptxt" runat="server" ReadOnly="true" ></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtMobNum" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                 
                   
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
                                <td colspan="4">
                                    <center>

                                        <h2 style="color: Brown; font-size: 16px; font-family: 'Bellefair'; letter-spacing: 2px;">Mustard Seeds (Sarson)</h2>
                                    </center>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%; background-color: #070b30; text-align: center;" class="QIParametersColumn">
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
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparFM_IncTaramira" runat="server" Text="uparFM_IncTaramira" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
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

                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparAM_OT_Toria" runat="server" Text="uparAM_OT_Toria" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
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
                                 <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparUR_Shvld_Imm" runat="server" Text="uparUR_Shvld_Imm" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
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
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparDamWeevd" runat="server" Text="uparDamWeevd" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
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
                                <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparSmallAtroSeeds" runat="server" Text="uparSmallAtroSeeds" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
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
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparMoisCont" runat="server" Text="uparMoisCont" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtMoisCont" CssClass="insptxtPara" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" Style="font-size: 12px;"
                                        ControlToValidate="txtMoisCont" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
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
                                <td style="width: 25%; background-color: #070b30; text-align: center;" class="QIParametersColumn">
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
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparmasur_Foreignmatter" runat="server" Text="uparmasur_Foreignmatter" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
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
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparmasur_admixture" runat="server" Text="uparmasur_admixture" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
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
                                 <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparmasur_DamagedPulses" runat="server" Text="uparmasur_DamagedPulses" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
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
                                 <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparmasur_sligDamagPulses" runat="server" Text="uparmasur_sligDamagPulses" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
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
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparmasur_ImmaShvldPulses" runat="server" Text="uparmasur_ImmaShvldPulses" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
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
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparmasur_WeevldPulses" runat="server" Text="lbluparmasur_WeevldPulses" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
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
                                   <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparMasur_MoistureContent" runat="server" Text="uparMasur_MoistureContent" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtMasur_MoistureContent" CssClass="insptxtPara" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" Style="font-size: 12px;"
                                        ControlToValidate="txtMasur_MoistureContent" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
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
                        <table style="width: 100%" class="QIParameters">
                            <tr>
                                <td colspan="4">
                                    <center>
                                          <h2 style="color: #070b30; font-size: 16px; font-family: 'Bellefair'; letter-spacing: 2px;"> Gram (Channa)</h2>
                                    </center>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%; background-color: #070b30; text-align: center;" class="QIParametersColumn">
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
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparGramForeign_Matter" runat="server" Text="uparGramForeign_Matter" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
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
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparGramFoodGrains" runat="server" Text="lbluparGramFoodGrains" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
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
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparGram_DamagFoodGrains" runat="server" Text="uparGram_DamagFoodGrains" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
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
                                   <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparGram_SligDamagTochedGrains" runat="server" Text="uparGram_SligDamagTochedGrains" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
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
                                   <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparGram_ImmaShrivBroGrains" runat="server" Text="uparGram_ImmaShrivBroGrains" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
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
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparGram_AdmixOtherVarie" runat="server" Text="uparGram_AdmixOtherVarie" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
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
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparGram_WeevldGrains" runat="server" Text="uparGram_WeevldGrains" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
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
                                  <td style="text-align: center; ">
                                    <h2 style="color: #070b30; font-size: 14px; font-family: 'Bellefair'; letter-spacing: 2px;">

                                        <asp:Label ID="lbluparGram_MoistureContent" runat="server" Text="uparGram_MoistureContent" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 14px;" ForeColor="Blue" Visible="true"></asp:Label>
                                    </h2>

                                </td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtGram_MoistureContent" CssClass="insptxtPara" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" Style="font-size: 12px;"
                                        ControlToValidate="txtGram_MoistureContent" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                               
                           
                        </table>
                    </center>
                </td>
            </tr>

               <tr>
                                <td colspan="3" style="text-align: center;">
                                                                 <asp:Button ID="btnQualityInspecction" runat="server" Text="Quality Inspection" CssClass="bttsub" Visible="true" OnClick="btnQualityInspecction_Click"
                                                                      Enabled="false" />
    </td>
                            </tr>

                <tr>
                                <td colspan="3" style="text-align: center;">
                                                                 
    </td>
                            </tr>
               <tr id="trotp" runat="server" visible="false">
                  
                      
                               <td class="InspColumn" style="padding-left: 20px; text-align: center; ">
                                  <asp:Button ID="btnsendOTP" runat="server" Text="Send OTP" CssClass="bttsubother" Visible="true" Enabled="false" OnClick="btnsendOTP_Click" />
                                   <asp:Button ID="btnROTP" runat="server" Text="Resend OTP" CssClass="bttsubother" Visible="false" Enabled="false" OnClick="btnROTP_Click" />
                               
                               </td>
                              <td class="InspColumn" style="padding-left: 20px; text-align: center; width:25%;">
                                     Enter One Time Password
                    <br />

                    <asp:TextBox ID="txtCheckOTP" CssClass="insptxt" runat="server"  ></asp:TextBox>
                  
                   
                               </td>
                              <td class="InspColumn" style="padding-left: 20px; text-align: center; width:25%;">
                                    <span id='timer' style="color: #FF0000; font-weight: bold"></span>

                                                          <asp:Button ID="btncheckOTP" runat="server" Text="Check OTP" CssClass="bttsubother" Visible="true" Enabled="false" causesvalidation="false" OnClick="btncheckOTP_Click" />
                                     <asp:Label ID="lblmsg" runat="server" Text="" Style="font-weight: 700;"></asp:Label>

                              </td>
                           </tr>
                      
               <tr id="trAcceptReject" runat="server" visible="false">
                                <td style="text-align: right;">
                                                                        <asp:Button ID="btnAccept" runat="server" Text="Accept" CssClass="bttsubother" Visible="true" Enabled="false" OnClick="btnAccept_Click" />

                                </td>
                                 <td style="text-align: center;">

                                </td>
                                
                                 <td style="text-align: left;">
                                                                                                             <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="bttsubother" Visible="true" Enabled="false" OnClick="btnReject_Click" />

                                </td>
                            </tr>
               </table>
        </center>
</asp:Content>

