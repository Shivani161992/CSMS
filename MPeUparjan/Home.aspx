<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="MPeUparjan_Home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Online Procurement Monitoring System ::</title>
    <link href="../CSS/ThemesBlue.css" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" href="Images/favicon.ico" />

    <script type="text/javascript">
   $(function(){

   $('.box').on('mouseenter mouseleave', function( e ){
       $(this).siblings().stop().fadeTo(300, e.type=='mouseenter' ? 0.5 : 1 );
   });
                          
});
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <center>
            <div style="width: 1000px">
                <table style="width: 100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center" valign="top" colspan="3">
                            <img src="../Images/MPLogo2013.png" width="1000px" height="110px" alt="" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px;" colspan="3">
                            <img src="../Images/LineSeparator.png" width="1000px" height="1px" alt="" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top" colspan="3">
                            <img src="../Images/euparjanlogo.jpg" width="400px" height="50px" alt="" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 30px;" colspan="3">
                            <img id="Img10" src="../Images/linetop.png" height="30px" width="100%" alt="" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center" valign="middle">
                            <center>
                                <fieldset style="border: 1px solid Navy; width: 900px">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td style="height: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%" align="center" valign="middle">
                                                <div class="box" style="width: 100%; background-image: url(../Images/div_bg.png);
                                                    background-position: top; background-repeat: no-repeat; height: 182px">
                                                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="height: 10px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2">
                                                                <asp:LinkButton ID="lnkPaddy" runat="server" ForeColor="indianred" Font-Bold="true"
                                                                    Font-Size="15pt" OnClick="lnkPaddy_OnClick">Paddy Procurement Monitoring System</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 100px" align="center" valign="middle" rowspan="3">
                                                                <asp:Image ID="Image10" runat="server" ImageUrl="~/Images/rice_new.png" Height="100px"
                                                                    Width="100px" ToolTip="Rice" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 10px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" valign="top">
                                                                <table cellpadding="0" cellspacing="0" style="width: 90%; border-bottom: #003366 thin solid;
                                                                    border-left: #003366 thin solid; border-top: #003366 thin solid; border-right: #003366 thin solid;">
                                                                    <tr>
                                                                        <td align="center" style="height: 40px">
                                                                            <asp:LinkButton ID="lnkDhanKhareedee1" runat="server" Font-Bold="true" Font-Size="15pt"
                                                                                ForeColor="green" OnClick="lnkDhanKhareedee1_Click">धान 2014-15</asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                    <%--  <tr align="center">
                                                                        <td style="height: 10px">
                                                                            <asp:LinkButton ID="lnkDhanKhareedee2" runat="server" Font-Bold="true" Font-Size="15pt"
                                                                                ForeColor="green" OnClick="lnkDhanKhareedeet_Click">धान 2014-15 (ट्रेनिंग  मोड्यूल)</asp:LinkButton>
                                                                        </td>
                                                                    </tr>--%>
                                                                    <tr>
                                                                        <td style="height: 1px; background-color: #003366;">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" style="height: 10px">
                                                                            <asp:LinkButton ID="lnkDhanKhareedee" runat="server" Font-Bold="true" Font-Size="15pt"
                                                                                ForeColor="indianred" OnClick="lnkDhanKhareedee_Click">धान 2013-14</asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" style="height: 40px">
                                                                            <asp:LinkButton ID="lnk_paddy_2012" runat="server" Font-Bold="true" Font-Size="15pt"
                                                                                ForeColor="indianred" OnClick="lnk_paddy_2012_Click">धान 2012-13</asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                            <td style="width: 50%" align="center" valign="middle">
                                                <div class="box" style="width: 100%; background-image: url(../Images/div_bg.png);
                                                    background-position: top; background-repeat: no-repeat; height: 182px">
                                                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="height: 10px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2">
                                                                <asp:LinkButton ID="lnkWheat" runat="server" Font-Size="15pt" ForeColor="indianred"
                                                                    Font-Bold="true" OnClick="lnkWheat_Click">Wheat Procurement Monitoring System</asp:LinkButton></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 100px" align="center" valign="middle" rowspan="3">
                                                                <asp:Image ID="Image9" runat="server" ImageUrl="~/Images/Wheat-icon.png" Height="100px"
                                                                    Width="100px" ToolTip="Wheat" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 10px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" valign="top">
                                                                <table cellpadding="0" cellspacing="0" style="width: 90%; border-bottom: #003366 thin solid;
                                                                    border-left: #003366 thin solid; border-top: #003366 thin solid; border-right: #003366 thin solid;">
                                                                    <tr>
                                                                        <td align="center" style="height: 40px">
                                                                            <asp:LinkButton ID="lnk_Wheat2015" runat="server" Font-Bold="true" Font-Size="15pt"
                                                                                ForeColor="Green" OnClick="lnk_Wheat2015_Click">गेंहूँ 2015-16</asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                    
                                                                    <tr>
                                                                        <td style="height: 10px" align="center">
                                                                            <asp:LinkButton ID="LinkButton2" runat="server" Font-Bold="true" Font-Size="15pt" ForeColor="Crimson" OnClick="LinkButton2_Click">ट्रेनिंग मोड्यूल गेंहूँ 2015-16</asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                    
                                                                    <tr>
                                                                        <td style="height: 1px; background-color: #003366;">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 10px" align="center">
                                                                      
                                                                      
                                                                        
                                                                            <asp:LinkButton ID="lnk_Wheat2014" runat="server" Font-Bold="true" Font-Size="15pt"
                                                                                ForeColor="IndianRed" OnClick="lnk_Wheat2014_Click">गेंहूँ 2014-15</asp:LinkButton>
                                                                      
                                                                      
                                                                        
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" style="height: 40px">
                                                                            <asp:LinkButton ID="lnkGenhuKhareedee" runat="server" Font-Bold="true" Font-Size="15pt"
                                                                                ForeColor="indianred" OnClick="lnkGenhuKhareedee_Click">गेंहूँ 2013-14 </asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%" align="center" valign="middle">
                                                <div class="box" style="width: 100%; background-image: url(../Images/div_bg.png);
                                                    background-position: top; background-repeat: no-repeat; height: 182px">
                                                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="height: 10px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2">
                                                                <asp:LinkButton ID="lnkMaize" runat="server" Font-Bold="true" Font-Size="15pt" ForeColor="indianred"
                                                                    OnClick="lnkMaize_OnClick" Text="Coarse Grain Procurement Monitoring System"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 100px" align="center" valign="middle" rowspan="3">
                                                                <asp:Image ID="Image11" runat="server" ImageUrl="~/Images/Paddy_new.png" Height="100px"
                                                                    Width="100px" ToolTip="Maize" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 10px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" valign="top">
                                                                <table cellpadding="0" cellspacing="0" style="width: 90%; border-bottom: #003366 thin solid;
                                                                    border-left: #003366 thin solid; border-top: #003366 thin solid; border-right: #003366 thin solid;">
                                                                    <tr>
                                                                        <td align="center" style="height: 40px">
                                                                            <asp:LinkButton ID="lnk_Makka_2014" runat="server" Font-Bold="true" Font-Size="15pt"
                                                                                ForeColor="green" OnClick="lnk_Makka_2014_Click">मोटा अनाज  2014-15</asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 10px" align="center">
                                                                            <asp:LinkButton ID="lnk_Makka_2013" runat="server" Font-Bold="true" Font-Size="15pt"
                                                                                ForeColor="indianred" OnClick="lnk_Makka_2013_Click">मोटा अनाज  2013-14</asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 1px; background-color: #003366;">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 10px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" style="height: 40px">
                                                                            <asp:LinkButton ID="lnkMakkaKhareedee" runat="server" Font-Bold="true" Font-Size="15pt"
                                                                                ForeColor="indianred" OnClick="lnkMakkaKhareedee_Click">मोटा अनाज  2012-13</asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                            <td style="width: 50%" align="center" valign="middle">
                                                <div class="box" style="width: 100%; background-image: url(../Images/div_bg.png);
                                                    background-position: top; background-repeat: no-repeat; height: 182px">
                                                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="height: 10px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2">
                                                                <asp:LinkButton ID="Lnk_Wht_2012" runat="server" ForeColor="indianred" Font-Bold="true"
                                                                    Font-Size="15pt" OnClick="Lnk_Wht_2012_Click">Wheat Procurement Monitoring System</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 10px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 100px" align="center" valign="middle" rowspan="3">
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Wheat-icon.png" Height="100px"
                                                                    Width="100px" ToolTip="Wheat" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" valign="top">
                                                                <table cellpadding="0" cellspacing="0" style="width: 90%; border-bottom: #003366 thin solid;
                                                                    border-left: #003366 thin solid; border-top: #003366 thin solid; border-right: #003366 thin solid;">
                                                                    <tr>
                                                                        <td align="center" style="height: 40px">
                                                                            <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="true" Font-Size="15pt"
                                                                                ForeColor="indianred" OnClick="LinkButton1_Click">गेंहूँ 2012-13 </asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 10px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 1px; background-color: #003366;">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 10px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" style="height: 40px">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </center>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 30px;" colspan="3">
                            <img id="Img2" src="../Images/line.png" height="30px" width="100%" alt="" />
                        </td>
                    </tr>

                      <tr >
                          <td style="width: 20%" align="center">
                        <a href="http://www.mp.nic.in/">
                            <img id="Img4" src="../Images/NIC-logo.png" style="width: 200px; height: 50px" title="National Informatics Center Madhya Pradesh"
                                alt="" />
                        </a>
                      </td>
                        <td style="width: 10%" align="center">
                        <a href="http://mpmandiboard.gov.in/">
                            <img id="Img5" src="../Images/newMandilogo.jpg" style="width: 100px; height: 50px" title="MP State Agriculture Marketting Board(Mandi Board)" alt="" />
                        </a>
                      </td>

                       <td style="width: 20%" align="center">
                        <a href="http://india.gov.in/">
                            <img id="Img3" src="../Images/logo_en.png" style="width: 200px; height: 50px" title="NATIONAL PORTAL OF INDIA"
                                alt="" />
                        </a>
                    </td>

                     </tr>
                </table>
            </div>
        </center>
        <div style="width: 100%; background-image: url(../Images/bgfooter.jpg); background-position: top;
            background-repeat: repeat-x">
            <table style="width: 100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="height: 15px;" colspan="5">
                    </td>
                </tr>
                <tr>
                   
                    <td style="width: 1%" align="center">
                        <img id="Img1" src="../Images/Linevertical.png" style="width: 20px; height: 50px"
                            alt="" />
                    </td>
                    <td style="color: Navy; font-size: 13pt; width: 58%" align="center">
                        <b>Designed, Developed and Hosted By: National Informatics Centre,
                            <br />
                            Madhya Pradesh, Ministry of Communications and Information Technology</b>
                    </td>
                    <td style="width: 1%" align="center">
                        <img id="Logo" src="../Images/Linevertical.png" style="width: 20px; height: 50px"
                            alt="" />
                    </td>
                  
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
