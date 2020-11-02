<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_PCInsp.aspx.cs" Inherits="PcGdn_Insp_Print_PCInsp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print</title>
    <style type="text/css">
        .style1
        {
            width: 190px;
        }
        .style2
        {
            height: 33px;
            width: 190px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <center>
   
  <table style="width: 1020px" >
        <tr>
            <td colspan="5" style="color: #000099; height: 35px;" align = "center">
                <b>खरीफ विपणन वर्ष 2016-17 में समर्थन मूल्य पर खरीदी हेतु स्थापित खरीदी केन्द्रों 
                का संयुक्त निरीक्षण प्रतिवेदन</b></td>
        </tr>
        <tr>
            <td style="color: #CC3300; height: 35px;">
                <b>क्र</b></td>
            <td style="width: 368px; color: #CC3300; height: 35px;" align = "center">
                <b>बिन्दु</b></td>
            <td colspan="3" style="color: #CC3300; height: 35px;">
                <b>निरीक्षण में पाई गयी स्थिति</b></td>
        </tr>
        <tr>
            <td style="color: #CC6600; height: 31px;">
                <b>1</b></td>
            <td style="width: 368px; color: #CC6600; height: 31px;" align = "center">
                <b>निरीक्षण दल के अधिकारियो के नाम</b></td>
            <td style="width: 201px; color: #CC6600; height: 31px;" align = "center">
                <b>पद</b></td>
            <td colspan="2" style="color: #CC6600; height: 31px;" align = "center">
                <b>मोबाइल नंबर</b></td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="width: 368px">
                <asp:Label ID="txtName_one" runat="server" Height="26px" Width="200px"> </asp:Label>
            </td>
            <td style="width: 201px">
                <asp:Label ID="txtdesig_one" runat="server" Height="26px" Width="200px"></asp:Label>
            </td>
            <td colspan="2" style="text-align: center">
                <asp:Label ID="txtMob_one" runat="server" Height="26px" Width="200px" 
                    MaxLength="11"></asp:Label>
            &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="width: 368px">
                <asp:Label ID="txtName_two" runat="server" Height="25px" Width="200px"></asp:Label>
            </td>
            <td style="width: 201px">
                <asp:Label ID="txtdesig_two" runat="server" Height="26px" Width="200px"></asp:Label>
            </td>
            <td colspan="2" style="text-align: center">
                <asp:Label ID="txtMob_two" runat="server" Height="25px" Width="200px" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="width: 368px">
                <asp:Label ID="txtName_three" runat="server" Height="26px" Width="200px"></asp:Label>
            </td>
            <td style="width: 201px">
                <asp:Label ID="txtdesig_three" runat="server" Height="26px" Width="200px"></asp:Label>
            </td>
            <td colspan="2" style="text-align: center">
                <asp:Label ID="txtMob_three" runat="server" Height="26px" Width="200px" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="width: 368px">
                <asp:Label ID="txtName_four" runat="server" Height="26px" Width="200px"></asp:Label>
            </td>
            <td style="width: 201px">
                <asp:Label ID="txtdesig_four" runat="server" Height="26px" Width="200px"></asp:Label>
            </td>
            <td colspan="2" style="text-align: center">
                <asp:Label ID="txtMob_four" runat="server" Height="25px" Width="200px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="width: 368px">
                <asp:Label ID="txtName_five" runat="server" Height="26px" Width="200px"></asp:Label>
            </td>
            <td style="width: 201px">
                <asp:Label ID="txtdesig_five" runat="server" Height="26px" Width="200px"></asp:Label>
            </td>
            <td colspan="2" style="text-align: center">
                <asp:Label ID="txtMob_five" runat="server" Height="25px" Width="200px" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                2</td>
            <td style="text-align: left; width: 368px">
                निरीक्षण की दिनाँक</td>
            <td colspan="2" style="text-align: left">
                <asp:Label ID="txtInsp_date" runat="server" Height="25px" Width="200px"></asp:Label>
                                
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                3</td>
            <td style="text-align: left; width: 368px">
                जिले का नाम</td>
            <td colspan="2" style="text-align: left">
                <asp:Label ID="ddlDistrict" runat="server" Height="35px" 
                    Width="219px">
                </asp:Label>
                                                    </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                3.1</td>
            <td style="text-align: left; width: 368px">
                तहसील का नाम</td>
            <td colspan="2" style="text-align: left">
                <asp:Label ID="ddlTehsil" runat="server" Height="35px" 
                    Width="219px">
                </asp:Label>
                                                    </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="height: 30px">
                </td>
            <td style="text-align: left; width: 368px; height: 30px;">
                किस फसल के खरीदी केंद्र का निरक्षण किया जाना है </td>
            <td colspan="2" style="text-align: left; height: 30px;">
                <asp:Label ID="ddlCrop" runat="server" >
                   
                </asp:Label>
                                                    </td>
            <td style="height: 30px">
                                                    </td>
        </tr>
        <tr>
            <td style="height: 36px">
                4</td>
            <td style="text-align: left; width: 368px; height: 36px;">
                खरीदी केंद्र का नाम</td>
            <td colspan="2" style="text-align: left; height: 36px;">
                <asp:Label ID="ddlPCName" runat="server" Height="25px" 
                    Width="350px">
                </asp:Label>
                                                    </td>
            <td style="height: 36px">
                </td>
        </tr>
        <tr>
            <td>
                5</td>
            <td style="text-align: left; width: 368px">
                समिति प्रबंधक का नाम</td>
            <td style="width: 201px">
                <asp:Label ID="txtPC_manager" runat="server" Height="25px" Width="200px"></asp:Label>
            </td>
            <td style="text-align: left; " class="style1">
                प्रबंधक का मोबाइल</td>
            <td>
                <asp:Label ID="txtPC_Mobile" runat="server" Height="25px" Width="175px" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                6</td>
            <td colspan="2" style="text-align: left; background-color: #CCFFCC;">
                कलेक्टर द्वारा नामित नोडल अधिकारी का नाम </td>
            <td style="background-color: #CCFFCC;" colspan="2">
                <asp:Label ID="txtNodal_officer" runat="server" Height="25px" Width="200px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="text-align: left; width: 368px; background-color: #CCFFCC;">
                पद</td>
            <td style="width: 201px; background-color: #CCFFCC;">
                <asp:Label ID="txtNodal_desig" runat="server" Height="25px" Width="200px"></asp:Label>
            </td>
            <td style="background-color: #CCFFCC;" class="style1">
                &nbsp;</td>
            <td style="background-color: #CCFFCC">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="text-align: left; width: 368px; background-color: #CCFFCC;">
                मोबाइल</td>
            <td style="width: 201px; background-color: #CCFFCC;">
                <asp:Label ID="txtNodal_Mobile" runat="server" Height="25px" Width="200px" ></asp:Label>
            </td>
            <td style="background-color: #CCFFCC;" class="style1">
                &nbsp;</td>
            <td style="background-color: #CCFFCC">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                7</td>
            <td style="text-align: left; width: 368px">
                पंजीकृत किसानो की संख्या</td>
            <td style="width: 201px">
                <asp:Label ID="txtregister_Farmer" runat="server" Height="26px" Width="200px"></asp:Label>
            </td>
            <td class="style1">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                8</td>
            <td colspan="4" style="text-align: left">
                समर्थन मूल्य योजनान्तर्गत खरीदी प्रारंभ होने की दिनाँक&nbsp;
                <asp:Label ID="txtProc_date" runat="server" Height="25px" Width="180px"></asp:Label>
                                  
            </td>
        </tr>
        <tr>
            <td style="background-color: #BFE8E8">
                9</td>
            <td style="text-align: center; color: #CC3300; background-color: #BFE8E8;" 
                colspan="4">
                <b>खरीदी विवरण</b></td>
        </tr>
        <tr>
            <td style="background-color: #BFE8E8">
                9.1</td>
            <td colspan="2" style="text-align: left; background-color: #BFE8E8;">
                निरीक्षण के दिन की गई खरीदी(में टन )</td>
            <td colspan="2" style="text-align: left; background-color: #BFE8E8;">
                <asp:Label ID="txtInspDate_Kharidi" runat="server" Height="25px" 
                    Width="200px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="background-color: #BFE8E8">
                9.2</td>
            <td style="text-align: left; width: 368px; background-color: #BFE8E8;">
                प्रगतिशील खरीदी (में टन)</td>
            <td style="width: 201px; background-color: #BFE8E8;">
                &nbsp;</td>
            <td colspan="2" style="text-align: left; background-color: #BFE8E8;">
                <asp:Label ID="txt_totalkharidi" runat="server" Height="25px" Width="200px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="background-color: #BFE8E8">
                9.3</td>
            <td style="text-align: left; width: 368px; background-color: #BFE8E8;">
                प्रगतिशील परिवहन (में टन )</td>
            <td style="width: 201px; background-color: #BFE8E8;">
                &nbsp;</td>
            <td colspan="2" style="text-align: left; background-color: #BFE8E8;">
                <asp:Label ID="txt_totalTransportation" runat="server" Height="25px" 
                    Width="200px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 32px; background-color: #BFE8E8;">
                9.4</td>
            <td style="text-align: left; width: 368px; height: 32px; background-color: #BFE8E8;">
                खरीदी केंद्र पर परिवहन हेतु शेष स्कंध</td>
            <td style="width: 201px; height: 32px; background-color: #BFE8E8;">
                </td>
            <td colspan="2" style="text-align: left; height: 32px; background-color: #BFE8E8;">
                <asp:Label ID="txt_remainTransport" runat="server" Height="25px"  Width="200px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                10</td>
            <td colspan="2" style="text-align: left">
                क्या खरीदी केंद्र पर FAQ सेम्पल रखा गया है ?</td>
            <td colspan="2" style="text-align: left">
                <asp:Label ID="ddl_FaqSample" runat="server" Height="30px"  Width="100px">
                   
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 41px">
                11</td>
            <td colspan="4" align ="center" >
                <b>विश्लेषण के लिए अवश्यक निम्न तकनीक उपकरण की उपलब्द्ता होने पर हैं चुने 
                तथा ना होने पर नहीं चुने|</b></td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="width: 368px; background-color: #FFFFCC;">
                परखी</td>
            <td style="width: 201px; background-color: #FFFFCC;">
                <asp:Label ID="ddl_Parkhi" runat="server" Height="30px"  Width="100px">
                   
                </asp:Label>
            </td>
            <td style="background-color: #FFFFCC;" class="style1">
                इलेक्ट्रोनिक बैलेंस</td>
            <td style="background-color: #FFFFCC">
                <asp:Label ID="ddl_electronicBalance" runat="server" Height="30px" Width="100px">

                </asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="width: 368px; background-color: #FFFFCC;">
                प्लास्टिक कपडे की सेम्पल थेलिया</td>
            <td style="width: 201px; background-color: #FFFFCC;">
                <asp:Label ID="ddl_plasticbag" runat="server" Height="30px"  Width="100px">
                   
                </asp:Label>
            </td>
            <td style="background-color: #FFFFCC;" class="style1">
                एजेंसी की पीतल की सील</td>
            <td style="background-color: #FFFFCC">
                <asp:Label ID="ddl_seal" runat="server" Height="30px" Width="100px">
                    
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="width: 368px; background-color: #FFFFCC;">
                नमी मापक यन्त्र</td>
            <td style="width: 201px; background-color: #FFFFCC;">
                <asp:Label ID="ddl_moistMachine" runat="server" Height="30px"  Width="100px">

                </asp:Label>
            </td>
            <td style="background-color: #FFFFCC;" class="style1">
                एनेमल प्लेट</td>
            <td style="background-color: #FFFFCC">
                <asp:Label ID="ddl_enamelplate" runat="server" Height="30px"  Width="100px">
                   
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                12</td>
            <td style="width: 368px">
                क्या खरीदी केंद्र पर छन्ना उपलब्ध है ?</td>
            <td style="width: 201px">
                <asp:Label ID="ddl_filter" runat="server" Height="30px" 
                    Width="100px">
                   
                </asp:Label>
            </td>
            <td class="style1">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                13</td>
            <td colspan="2" style="text-align: left">
                क्या खरीदी केंद्र पर थ्रेसर में उपयोग होने वाला ब्लोअर उपलब्ध है ?</td>
            <td class="style1">
                <asp:Label ID="ddl_blower" runat="server" Height="30px" 
                    Width="100px">
                    
                </asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                14</td>
            <td colspan="2">
                तिरपाल एवं सिलाई मशीनों की पर्याप्त उपलब्धता है अथवा नहीं</td>
            <td class="style1">
                <asp:Label ID="ddl_TirplaSilai" runat="server" Height="30px" 
                    Width="100px">
                   
                </asp:Label>
                                                    </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                15</td>
            <td colspan="2">
                क्या क्रय स्कंध के बोरो की डबल सिलाई की जा रही है?</td>
            <td class="style1">
                <asp:Label ID="ddl_doubleSilai" runat="server" Height="30px"  Width="100px">
                </asp:Label>
                                                    </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                15.1</td>
            <td colspan="2">
                &nbsp;डबल सिलाई न होने से ट्रक गोदाम से रिजेक्ट हुए |</td>
            <td class="style1">
                <asp:Label ID="ddl_reject_doubleSilai" runat="server" Height="30px" Width="100px">

                </asp:Label>
                                                    </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="2">
                                डबल सिलाई न होने से गोदाम से रिजेक्ट हुए ट्रक की संख्या</td>
            <td class="style1">
                <asp:Label ID="txt_truckreject_dueDoubleSilai" runat="server" Height="23px" 
                    Width="175px"></asp:Label>
                                                    </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                16</td>
            <td colspan="2">
                क्या बोरो पर स्टेंसिल एवं टैग लगाये जा रहे है?&nbsp; </td>
            <td class="style1">
                <asp:Label ID="ddl_stencilTag" runat="server" Height="30px"  Width="100px">
                    
                </asp:Label>
                                                    </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                16.1</td>
            <td colspan="2">
                स्टेंसिल एवं टैग ना होने से कितने ट्रक गोदाम से रिजेक्ट हुए |</td>
            <td class="style1">
                <asp:Label ID="txt_truckreject_duestencil" runat="server" Height="26px" 
                    Width="200px"></asp:Label>
                                                    </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                17</td>
            <td style="text-align: left;" colspan="2">
                क्या संस्था द्वारा कृषको के रिजेक्ट स्कंध के सेम्पल सुरक्षित रखे गए है तथा पंजी 
                में विवरण दर्ज किया गया है |</td>
            <td class="style1">
                <asp:Label ID="ddl_rejectSample" runat="server" Height="33px"  Width="100px">

                </asp:Label>
                                                    </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                18</td>
            <td style="text-align: left;" colspan="2">
                क्या खरीदी केंद्र पर वांछित अभिलेखों का संधारण किया जा रहा है ? यदि नहीं तो नीचे 
                विवरण लिखे</td>
            <td class="style1">
                <asp:Label ID="ddl_registerEntry" runat="server" Height="34px" Width="100px">

                </asp:Label>
                                                    </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                19</td>
            <td colspan="2">
                निरीक्षण के दौरान खरीदी केंद्र पर गुणवत्ता में कमी के कारण रिजेक्ट किये गए स्टॉक 
                की मात्रा तथा रिजेक्सन का कारण (एनालिसिस रिपोर्ट संलग्न करें)</td>
            <td class="style1">
                <asp:Label ID="txt_rejectQty" runat="server" Height="30px" Width="200px"></asp:Label>
                                                    </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                20</td>
            <td style="width: 368px; background-color: #CCCCFF;">
                वाररदाना गठानो की संख्या व गुणवत्ता | </td>
            <td style="width: 201px; background-color: #CCCCFF;">
                कुल प्राप्त वारदाना</td>
            <td style="background-color: #CCCCFF;" class="style1">
                <asp:Label ID="txt_totalBardana" runat="server" Height="26px" Width="200px"></asp:Label>
            </td>
            <td style="background-color: #CCCCFF">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="width: 368px; background-color: #CCCCFF;">
                &nbsp;</td>
            <td style="width: 201px; background-color: #CCCCFF;">
                खरीदी में उपयोग</td>
            <td style="background-color: #CCCCFF;" class="style1">
                <asp:Label ID="txt_usedBardana" runat="server" Height="25px" Width="200px"></asp:Label>
            </td>
            <td style="background-color: #CCCCFF">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="width: 368px; background-color: #CCCCFF;">
                &nbsp;</td>
            <td style="width: 201px; background-color: #CCCCFF;">
                शेष वारदाना गठान में </td>
            <td style="background-color: #CCCCFF;" class="style1">
                <asp:Label ID="txt_remainBardana_Gathan" runat="server" Height="26px" 
                    Width="200px"></asp:Label>
            </td>
            <td style="background-color: #CCCCFF">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="width: 368px; background-color: #CCCCFF;">
                &nbsp;</td>
            <td style="width: 201px; background-color: #CCCCFF;">
                शेष वारदाना लूस में </td>
            <td style="background-color: #CCCCFF;" class="style1">
                <asp:Label ID="txt_remain_bardana" runat="server" Height="26px" Width="200px"></asp:Label>
            </td>
            <td style="background-color: #CCCCFF">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                21</td>
            <td style="text-align: left;" colspan="2">
                क्या ख़रीदे स्टॉक का वजन मानक भर्ती का है ? यदि वजन में अंतर है तो कारण सहित 
                विवरण नीचे लिखे </td>
            <td class="style1">
                <asp:Label ID="ddl_ManikBharti" runat="server" Height="30px" Width="100px">

                </asp:Label>
                                                    </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="text-align: left;" colspan="4">
                <asp:Label ID="txt_reason_manakbharti" runat="server" Height="35px" 
                    Width="900px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 33px">
                22</td>
            <td style="height: 33px; text-align: left;" colspan="2">
                कृषको को भुगतान किस दिनाँक तक का किया गया है ?</td>
            <td class="style2">
                <asp:Label ID="txt_paymentDate" runat="server" Height="25px" Width="150px"></asp:Label>
                
                
            </td>
            <td style="height: 33px">
                                                    </td>
        </tr>
        <tr>
            <td>
                22.1</td>
            <td style="text-align: left;" colspan="2">
                क्या 3 दिन से अधिक विलम्ब हो रहा है ?</td>
            <td class="style1">
                <asp:Label ID="ddl_last3days" runat="server" Height="30px"  Width="100px">
                    
                </asp:Label>
                                                    </td>
            <td>
                                                    </td>
        </tr>
        <tr>
            <td>
                23</td>
            <td style="text-align: left;" colspan="2">
                क्या इलेक्ट्रोनिक तौल काँटा तथा समीपस्थ धर्मकांटा का सत्यापन किया गया है ?</td>
            <td class="style1">
                <asp:Label ID="ddl_verify_taulkanta" runat="server" Height="30px"  Width="100px">
                    
                </asp:Label>
                                                    </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                24</td>
            <td style="text-align: left;" colspan="2">
                धान उपार्जन की स्थिति में क्या बोरो को पलटकर धान की भर्ती की जा रही है ?</td>
            <td class="style1">
                <asp:Label ID="ddl_paddyBharti_return" runat="server" Height="30px"  Width="100px">
                   
                </asp:Label>
                                                    </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                25</td>
            <td style="text-align: left;" colspan="2">
                निरीक्षण दिनाँक तक गोदाम से कितने ट्रक एवं मात्रा के अस्वीकृति पत्रक जारी किये 
                गए ?</td>
            <td style="text-align: left; margin-left: 40px;" class="style1">
                ट्रक
                <asp:Label ID="txt_totalreject_truck" runat="server" Height="24px"  Width="140px"></asp:Label>
            </td>
            <td style="text-align: left">
                मात्रा
                <asp:Label ID="txt_totalreject_qty" runat="server" Height="28px" 
                    Width="140px"></asp:Label>
                                                    </td>
        </tr>
        <tr>
            <td>
                26</td>
            <td style="text-align: left;" colspan="2">
                किस दिनाँक तक के स्वीकृति पत्रक प्राप्त किये गए है |</td>
            <td class="style1">
                <asp:Label ID="txt_accept_recddate" runat="server" Height="25px" 
                    Width="120px"></asp:Label>
                   
           </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                27</td>
            <td style="text-align: left;" colspan="2">
                खरीदी केंद्र पर डबल सिलाई किये, स्टेंसिल एवं टैग लगे बोरे की संख्या जो परिवहन के 
                लिए तैयार है |</td>
            <td class="style1">
                <asp:Label ID="txt_totalbags_allparameter" runat="server" Height="25px" 
                    Width="140px"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                28</td>
            <td style="width: 368px; text-align: left;">
                अन्य कोई महत्वपूर्ण बिन्दु</td>
            <td style="text-align: left;" colspan="3">
                <asp:Label ID="txt_otherpoint" runat="server" Height="40px" Width="550px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="text-align: left;" colspan="4">
                <asp:Label ID="Label3" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="width: 368px; text-align: left;">
                &nbsp;</td>
            <td colspan="3">
              
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="width: 368px; text-align: left;">
                &nbsp;</td>
            <td colspan="3" style="text-align: center">
                <asp:HyperLink ID="hlinkpdo" runat="server" Font-Size="11pt" NavigateUrl="#" Visible="False"
                    Width="142px" Height="22px" style="font-weight: 700">Print This</asp:HyperLink>
            </td>
        </tr>
    </table>
    
    </center>
    </div>
    </form>
</body>
</html>
