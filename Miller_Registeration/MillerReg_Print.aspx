<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MillerReg_Print.aspx.cs" Inherits="Miller_Registeration_MillerReg_Print" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
            page-break-before: always;
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

        .auto-style4
        {
            font-size: large;
        }

        .auto-style5
        {
            font-size: medium;
        }

        .auto-style6
        {
            font-size: small;
        }

        .auto-style7
        {
            width: 590px;
        }

        .auto-style8
        {
            width: 657px;
        }
    </style>

</head>
<body onload="window.print()">
    <form id="form1" runat="server">
        <br />
        <div class="A4">
            <table align="center" class="auto-style1" style="width: 793px;" border="1" cellspacing="0" cellpadding="0">

                <tr>
                    <td style="vertical-align: middle; text-align: center" cellspacing="0" cellpadding="0" class="auto-style8">
                        <strong><span class="auto-style4">Paddy Mill Registration For Crop Year - </span>
                            <asp:Label ID="lblCropYear" runat="server" Text="" CssClass="auto-style4"></asp:Label>
                        </strong>
                    </td>
                    <td style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0">
                        <asp:Label ID="Label1" runat="server" Height="146px" Width="140px" Text="Please Affix Your Photo In Box" Style="text-align: center; vertical-align: bottom; display: inline-block; border-width: 1px;" BorderStyle="Solid"></asp:Label>
                    </td>
                </tr>

            </table>
            <table align="center" class="auto-style1" border="1" cellspacing="0" cellpadding="0">

                <tr>
                    <td style="text-align: center" class="auto-style7" colspan="7">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: left"><b style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">मिल रजिस्ट्रेशन क्रमांक</b></td>
                    <td style="text-align: left" colspan="2">
                        <asp:Label ID="lblRegNo" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: right" colspan="2">
                        <b style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">रजिस्ट्रेशन का दिनांक</b></td>
                    <td style="text-align: right" colspan="2">
                        <asp:Label ID="lblRegDate" runat="server"></asp:Label>
                    </td>
                </tr>




                <tr>
                    <td style="text-align: left"><b style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">मिल का नाम</b></td>
                    <td style="text-align: left" colspan="6">
                        <asp:Label ID="lblMillName" runat="server"></asp:Label>
                    </td>
                </tr>




                <tr>
                    <td style="text-align: left" rowspan="4"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">मिल का पता</span></td>
                    <td style="text-align: center" colspan="6">मिल का राज्य - 
                        <asp:Label ID="lblState" runat="server"></asp:Label>
                    </td>
                </tr>




                <tr>
                    <td style="text-align: left">जिला</td>
                    <td style="text-align: left" colspan="2">
                        <asp:Label ID="lblDist" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: left" colspan="2">तहसील</td>
                    <td style="text-align: left">
                        <asp:Label ID="lblTehsil" runat="server"></asp:Label>
                    </td>
                </tr>




                <tr>
                    <td style="text-align: left"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">ग्राम / वार्ड</span></td>
                    <td style="text-align: left" colspan="2">
                        <asp:Label ID="lblGram" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: left" colspan="2"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 14px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">पिन कोड</span></td>
                    <td style="text-align: left">
                        <asp:Label ID="lblPin" runat="server"></asp:Label>
                    </td>
                </tr>




                <tr>
                    <td style="text-align: left"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">मोबाइल नंबर</span></td>
                    <td style="text-align: left" colspan="2">
                        <asp:Label ID="lblMob" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: left" colspan="2"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 14px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">फोन न.</span></td>
                    <td style="text-align: left">
                        <asp:Label ID="LblPhone" runat="server"></asp:Label>
                    </td>
                </tr>




                <tr>
                    <td style="text-align: left"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">मिल का स्वामित्व किसका है</span></td>
                    <td style="text-align: center" colspan="6">
                        <asp:Label ID="lblOwnership" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>




                <tr>
                    <td style="text-align: left" rowspan="5">&nbsp;</td>
                    <td style="text-align: left" rowspan="3"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 14px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">(i)यदि स्वामित्व प्रोप्राइटरशिप है तो</span></td>
                    <td style="text-align: left" colspan="2"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 14px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">प्रोपाइटर का नाम</span></td>
                    <td style="text-align: left" colspan="3">
                        <asp:Label ID="lblPropName" runat="server"></asp:Label>
                    </td>
                </tr>




                <tr>
                    <td style="text-align: left" colspan="2"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 14px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">प्रोपाइटर का पता</span></td>
                    <td style="text-align: left" colspan="3">
                        <asp:Label ID="lblPropAdd" runat="server"></asp:Label>
                    </td>
                </tr>




                <tr>
                    <td style="text-align: left" colspan="2"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">प्रोपाइटर का शहर</span></td>
                    <td style="text-align: left" colspan="3">
                        <asp:Label ID="lblPropCity" runat="server"></asp:Label>
                    </td>
                </tr>




                <tr>
                    <td style="text-align: left" rowspan="2"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">(ii)यदि फर्म है तो</span></td>
                    <td style="text-align: left" colspan="2"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 14px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">फर्म का प्रकार</span></td>
                    <td style="text-align: left" colspan="3">
                        <asp:Label ID="lblFirm" runat="server"></asp:Label>
                    </td>
                </tr>




                <tr>
                    <td style="text-align: left" colspan="2"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 14px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">पंजीयन क्रमांक</span></td>
                    <td style="text-align: left" colspan="3">
                        <asp:Label ID="lblFirmPanjiyan" runat="server"></asp:Label>
                    </td>
                </tr>




                <tr>
                    <td style="text-align: left"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">मिल संचालक का नाम</span></td>
                    <td style="text-align: left" colspan="6">
                        <asp:Label ID="lblSanchName" runat="server"></asp:Label>
                    </td>
                </tr>




                <tr>
                    <td style="text-align: left"><b style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 14px; font-style: normal; font-variant: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">मिल संचालक के पिता का नाम</b></td>
                    <td style="text-align: left" colspan="6">
                        <asp:Label ID="lblSnachFName" runat="server"></asp:Label>
                    </td>
                </tr>




                <tr>
                    <td style="text-align: left"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">मिल संचालक का स्थाई पता</span></td>
                    <td style="text-align: left" colspan="6">
                        <asp:Label ID="lblSanchAdd" runat="server"></asp:Label>
                    </td>
                </tr>




                <tr>
                    <td style="text-align: left"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">भागीदार/भागीदारो का नाम</span></td>
                    <td style="text-align: left">
                        <asp:Label ID="lblPart1" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: left" colspan="2">
                        <asp:Label ID="lblPart2" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: left" colspan="2">
                        <asp:Label ID="lblPart3" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="lblPart4" runat="server"></asp:Label>
                    </td>
                </tr>




                <tr>
                    <td style="text-align: left"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">टेलीफ़ोन न.<span class="Apple-converted-space">&nbsp;</span></span></td>
                    <td style="text-align: left">
                        <asp:Label ID="lblPartPhone1" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: left" colspan="2">
                        <asp:Label ID="lblPartPhone2" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: left" colspan="2">
                        <asp:Label ID="lblPartPhone3" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="lblPartPhone4" runat="server"></asp:Label>
                    </td>
                </tr>




                <tr>
                    <td style="text-align: left"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">मिल की रनिंग स्थिति</span></td>
                    <td style="text-align: left">
                        <asp:Label ID="lblRunningStatus" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: left" colspan="4"><b style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 14px; font-style: normal; font-variant: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">मिल स्वयं की है या लीज़ पर है</b></td>
                    <td style="text-align: left">
                        <asp:Label ID="lblLiz" runat="server"></asp:Label>
                    </td>
                </tr>




                <tr>
                    <td style="text-align: left" rowspan="2"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">यदि लीज़ पर ली गयी है तो</span></td>
                    <td style="text-align: left"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 14px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">मिल मालिक का नाम</span></td>
                    <td style="text-align: left" colspan="2">
                        <asp:Label ID="lblLizName" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: left" colspan="2"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 14px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">लीज़ अनुबंध समाप्ति की तिथि</span></td>
                    <td style="text-align: left">
                        <asp:Label ID="lblLizDate" runat="server"></asp:Label>
                    </td>
                </tr>




                <tr>
                    <td style="text-align: left"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 14px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">मिल मालिक का पता</span></td>
                    <td style="text-align: left" colspan="5">
                        <asp:Label ID="lblLizAdd" runat="server"></asp:Label>
                    </td>
                </tr>




                <tr>
                    <td style="text-align: left"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">मिलिंग क्षमता</span><span class="auto-style6" style="color: rgb(0, 0, 255); font-family: 'Times New Roman'; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">(मै०टन/प्रति घंटा)</span></td>
                    <td style="text-align: left"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 14px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">अरवा</span></td>
                    <td style="text-align: center" colspan="2">
                        <asp:Label ID="lblArva" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: left" colspan="2"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 14px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">उसना</span></td>
                    <td style="text-align: center">
                        <asp:Label ID="lblUsna" runat="server"></asp:Label>
                    </td>
                </tr>




                <tr>
                    <td style="text-align: left" colspan="4"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);"><strong>Crop Year</strong></span><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);"><span class="Apple-converted-space">&nbsp;</span><asp:Label ID="Label2" runat="server"></asp:Label>
                        में प्रतिदिन काम की जाने वाली शिफ्ट की संख्या</span></td>
                    <td style="text-align: center" colspan="2">
                        <asp:Label ID="lblShift" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: right">&nbsp;</td>
                </tr>




            </table>


            <table>

                <table align="center" width="100%">

                    <tr class="auto-style4">
                        <td style="text-align: left">
                            <asp:Label ID="lblCurrentDateTime" runat="server" Font-Bold="True"></asp:Label>&nbsp;<asp:Label ID="lblRegisID1" runat="server" Font-Bold="True"></asp:Label></td>
                        <td style="text-align: left">&nbsp;</td>
                        <td style="text-align: right">Page 1/2</td>
                    </tr>

                </table>
        </div>

        <div class="LineBreak"></div>
        <div class="A4">

            <br />
            <table align="center" class="auto-style1" border="1" cellspacing="0" cellpadding="0">

                <tr>
                    <td style="text-align: center" class="auto-style7" colspan="5">&nbsp;</td>
                </tr>


                <tr>
                    <td style="text-align: left">
                        <span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">GST नम्बर </span>

                    </td>
                    <td style="text-align: right">
                        <asp:Label ID="lblTaxNo" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: left"><b style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 14px; font-style: normal; font-variant: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">आयकर पैन नम्बर</b></td>
                    <td style="text-align: right" colspan="2">
                        <asp:Label ID="lblPANNo" runat="server"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td style="text-align: left" colspan="2">
                        <span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">आधार नम्बर </span>

                    </td>
                    <td  style="text-align: left" colspan="3">
                         <asp:Label ID="lbl_aadharno" runat="server"></asp:Label>
                    </td>
                </tr>







                <tr>
                    <td style="text-align: left"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">विद्युत विभाग द्वारा आवंटित सर्विस टैक्स न.</span></td>
                    <td style="text-align: right">
                        <asp:Label ID="lblServiceTax" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: left"><b style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 14px; font-style: normal; font-variant: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">विद्युत विभाग द्वारा की गयी वर्तमान रीडिंग न.</b></td>
                    <td style="text-align: right" colspan="2">
                        <asp:Label ID="lblReadingNo" runat="server"></asp:Label>
                    </td>
                </tr>







                <tr>
                    <td style="text-align: left"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">मंडी प्रसंस्करण कर्ता लायसेंस न.</span></td>
                    <td style="text-align: right">
                        <asp:Label ID="lblMandiNo" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: left"><b style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 14px; font-style: normal; font-variant: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">मंडी व्यापर लायसेंस न.</b></td>
                    <td style="text-align: right" colspan="2">
                        <asp:Label ID="lbllicenceNo" runat="server"></asp:Label>
                    </td>
                </tr>







                <tr>
                    <td style="text-align: left" rowspan="7"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">अप्रैल 17 से सितम्बर 17 तक मिलिंग के दौरान<span class="Apple-converted-space">&nbsp;</span></span><br style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);" />
                        <span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">खपत हुई बिजली बिल की यूनिट तथा<span class="Apple-converted-space">&nbsp;</span></span><br style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);" />
                        <span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">कर्मचारी की संख्या</span></td>
                    <td style="text-align: center"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">Month</span></td>
                    <td style="text-align: center"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">Unit Consumption</span></td>
                    <td style="text-align: center"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">Number of Employed</span></td>
                    <td style="text-align: center"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">Number of Shift</span></td>
                </tr>







                <tr>
                    <td style="text-align: center"><strong style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 14px; font-style: normal; font-variant: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">April</strong></td>
                    <td style="text-align: right">
                        <asp:Label ID="AprUnit" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: right">
                        <asp:Label ID="lblAprEmp" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: right">
                        <asp:Label ID="lblAprShift" runat="server"></asp:Label>
                    </td>
                </tr>







                <tr>
                    <td style="text-align: center"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">May</span></td>
                    <td style="text-align: right">
                        <asp:Label ID="MayUnit" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: right">
                        <asp:Label ID="lblMayEmp" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: right">
                        <asp:Label ID="lblMayShift" runat="server"></asp:Label>
                    </td>
                </tr>







                <tr>
                    <td style="text-align: center"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">June</span></td>
                    <td style="text-align: right">
                        <asp:Label ID="JuneUnit" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: right">
                        <asp:Label ID="lblJuneEmp" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: right">
                        <asp:Label ID="lblJuneShift" runat="server"></asp:Label>
                    </td>
                </tr>







                <tr>
                    <td style="text-align: center"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">July</span></td>
                    <td style="text-align: right">
                        <asp:Label ID="JulyUnit" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: right">
                        <asp:Label ID="lblJulyEmp" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: right">
                        <asp:Label ID="lblJulyShift" runat="server"></asp:Label>
                    </td>
                </tr>







                <tr>
                    <td style="text-align: center"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">August</span></td>
                    <td style="text-align: right">
                        <asp:Label ID="AugUnit" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: right">
                        <asp:Label ID="lblAugEmp" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: right">
                        <asp:Label ID="lblAugShift" runat="server"></asp:Label>
                    </td>
                </tr>







                <tr>
                    <td style="text-align: center"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">September</span></td>
                    <td style="text-align: right">
                        <asp:Label ID="SepUnit" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: right">
                        <asp:Label ID="lblSepEmp" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: right">
                        <asp:Label ID="lblSepShift" runat="server"></asp:Label>
                    </td>
                </tr>




                <tr>
                    <td style="text-align: left" colspan="5">&nbsp;</td>
                </tr>







                <tr>
                    <td style="text-align: left" colspan="3"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);" class="auto-style5">गत वर्ष की गयी कस्टम मिलिंग की मात्रा<span class="Apple-converted-space">&nbsp;</span></span><span style="color: rgb(0, 0, 255); font-family: 'Times New Roman'; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">(in MT)</span></td>
                    <td style="text-align: right">
                        <asp:Label ID="lblCustomMilling" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: left">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: left" colspan="3"><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);" class="auto-style5">आगामी 16/01/2018 से 30/06/2018 तक मिलिंग की मात्रा<span class="Apple-converted-space">&nbsp;</span></span><span style="color: rgb(0, 0, 255); font-family: 'Times New Roman'; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">(in MT)</span></td>
                    <td style="text-align: right">
                        <asp:Label ID="lblupcomingsixmonths" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: left">&nbsp;</td>
                </tr>
                <tr id="trone" visible="false" runat="server">
                     <td style="text-align: left" colspan="2" >
                         <span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);" class="auto-style5">
                           समस्त जिलो में मिलर की  कुल अनुबंध की मात्रा (MT)
</span>
                    </td>
                    <td>

         <asp:Label ID="lbl_total_agreeQty" runat="server"></asp:Label>
                    </td>
                   <td style="text-align: left" >
                         <span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);" class="auto-style5">
                            निरिक्षण में पाई गयी कुल BRL चावल की मात्रा (MT)
</span>
                    </td>
                <td>
                        <asp:Label ID="lbl_InsBRLQty" runat="server"></asp:Label>
                    </td>
                </tr>




                <tr id="trtwo" visible="false" runat="server">
                  <td style="text-align: left" colspan="2">
                         <span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);" class="auto-style5">
                            Upgraded/बदली गयी BRL चावल की मात्रा (MT)
</span>
                    </td>
                    <td>
                        <asp:Label ID="lbl_Changed_BRLQty" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: left" >
                         <span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);" class="auto-style5">
                           Upgradation/बदलने हेतु शेष मात्रा (MT)
</span>
                    </td>
                    <td>
                        <asp:Label ID="lbl_remQty" runat="server"></asp:Label>
                    </td>
                </tr>

                 <tr id="trthree" visible="false" runat="server">
                     <td style="text-align: left" colspan="2">
                         <span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);" class="auto-style5">
                           पुराने वारदाने (Bales)
</span>
                    </td>
                       <td>
                        <asp:Label ID="lblGunny_bags" runat="server"></asp:Label>
                    </td>
                     <td>

                     </td>
                     <td>

                     </td>
                     </tr>
                <tr>
                    <td style="text-align: left" colspan="2"><strong style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">संचालक के द्वारा अधिकृत प्रतिनिधि का नाम</strong></td>
                    <td style="text-align: left" colspan="3">
                        <asp:Label ID="lblAdhikratName" runat="server"></asp:Label>
                    </td>
                </tr>




                <tr>
                    <td style="text-align: left" colspan="2"><strong style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">संचालक के द्वारा अधिकृत प्रतिनिधि का पता</strong></td>
                    <td style="text-align: left" colspan="3">
                        <asp:Label ID="lblAdhikratAdd" runat="server"></asp:Label>
                    </td>
                </tr>



            </table>
            <table align="center" class="auto-style1">
               <%-- <tr>
                    <td style="text-align: center"><strong>1:- वर्ष 2017-18 में माह जून 2018  तक धान की कितनी मात्रा की मिलिंग कर सकेंगे
                        <br />

                        2:-वर्ष 2017-18 में माह जून 18 तक दिए गए टारगेट की मिलिंग न कर पाने की दशा में रू0 5000/- प्रति टन की दर से पेनाल्टी अधिरोपित की जावेगी |
                        <br />

                        3:-मिलर को मिलिंग चार्जेस का 50% भुगतान तत्समय किया का सकेगा | शेष समस्त राशि का भुगतान धान की सम्पूर्ण मात्रा की मिलिंग के पश्चात की जावेगी |
                    </strong>

                    </td>
                </tr>--%>

                <tr>
                    <td style="text-align: left">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: left">प्रमाणित किया जाता है कि मेरे द्वारा भरी गयी उपरोक्त सभी जानकारी सही है|</td>
                </tr>

                <tr>
                    <td style="text-align: left">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: right">मिलर्स हस्ताक्षर</td>
                </tr>

            </table>

            <table>

                <table align="center" width="100%">

                    <tr class="auto-style4">
                        <td style="text-align: left">
                            <asp:Label ID="lblCurrentDateTime2" runat="server" Font-Bold="True"></asp:Label>&nbsp;<asp:Label ID="lblRegisID2" runat="server" Font-Bold="True"></asp:Label></td>
                        <td style="text-align: left">&nbsp;</td>
                        <td style="text-align: right">Page 2/2</td>
                    </tr>

                </table>
        </div>
    </form>
</body>
</html>
