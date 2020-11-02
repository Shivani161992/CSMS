<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FarmerRegistration.aspx.cs" Inherits="mpproc_Masters_FarmerRegistration" MasterPageFile="~/mpproc/MasterPage/AgencyMaster.master"  Title="Farmer Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>Farmer Registration Form</title>
 <script type="text/javascript" src="../../js/calendar_eu.js"></script>   
  <script type="text/javascript" src="../js/DecimalValid.js" ></script>
<script type="text/javascript" language="javascript">
function CheckIsChar(tx)
{
var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode < 65 && AsciiCode !=32) || (AsciiCode > 122))
{
alert('Please enter only character');
event.cancelBubble = true;
event.returnValue = false;
}
}

function CheckIsNum(e,tx)
    {         
        var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;  
             
        if ((AsciiCode < 46 && AsciiCode != 47) || (AsciiCode > 57 ))
        {
//        alert(AsciiCode);
          alert('Please enter only numbers.');
          
            return false;
        }                
      
        
    }
  </script>
  <script type="text/javascript">
    function CheckIsNumeric(e,tx)
    {         
        var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;                        
        if ((AsciiCode < 46 && AsciiCode != 8) || (AsciiCode > 57 ) || (AsciiCode == 47 ))
        {
            alert('Please enter only numbers.');
            return false;
        }                
        var num=tx.value;        
        var len=num.length;
        var indx=-1;
        indx=num.indexOf('.');
        if (indx != -1)
        {
            if ((AsciiCode == 46 ))
            {
                alert('Point must be apear only one time.');
                return false;
            }
            var dgt=num.substr(indx,len);
            var count= dgt.length;
            //alert (count);
            if (count > 4 && AsciiCode != 8)  
            {
                alert("Only 4 decimal digits allowed.");
                return false;
            }
        }
    }
    </script>
  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="margin-left:0px; margin-top:0px" width="100%">
        <tr>
            <td style="width: 103px; height: 324px;">
             <table width="100%" cellpadding="2" cellspacing="2" style=" border-collapse: collapse; border:solid 1px white; background-color:#cccc66" id="TABLE1">
              <tr align="center" class="HeadingBlue">
                  <td colspan="4" style="height: 15px; border-collapse: collapse; border:solid 1px white; background-color: dimgray; width: 580px;">
                    </td>
              </tr>
                <tr align="center" > 
                  <td colspan="4" class="boldTxt" style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                      border-bottom: white 1px solid; border-collapse: collapse; width: 580px;" align="center">
                      <span style="font-size: 8pt">
                          <asp:Label ID="lbl_titleFar" runat="server" Text="किसान का पंजीयन" Font-Size="Medium" ForeColor="Maroon" Font-Bold="True" Width="259px"></asp:Label></span></td>
                </tr>
            
              <tr align="center" >
                  <td  colspan="4" align="right" style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                      border-bottom: white 1px solid; border-collapse: collapse; height: 14px; width: 580px;">
             
                     </td>
              </tr>
            <tr>
                <td colspan="4" style="border-right: white 1px solid; border-top: white 1px solid;
                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                    width: 580px; height: 195px;">
                    <asp:Panel ID="pnlFCI_OTDepot" runat="server" Width="100%">
                        <table cellpadding="0" cellspacing="0" style="width:100%; border-collapse: collapse; border:solid 1px white;" >
                            <tr>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 103px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
    <asp:Label ID="lbl_District" runat="server" Text="जिला :" Font-Size="Small"></asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 145px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:DropDownList ID="DDL_Dist" runat="server" OnSelectedIndexChanged="DDL_Dist_SelectedIndexChanged" Font-Bold="True" Font-Size="12px">
                                    </asp:DropDownList></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 335px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
    <asp:Label ID="lbl_Tehsil" runat="server" Font-Size="Small">तहसील :</asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px; width: 206px;">
                                    <asp:DropDownList ID="DDL_Tah" runat="server" OnSelectedIndexChanged="DDL_Tah_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" Font-Size="12px">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 103px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:Label ID="lbl_Village" runat="server" Font-Size="Small" Text="निवासी ग्राम का नाम :" Width="154px"></asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 145px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:DropDownList ID="DDL_Village" runat="server" OnSelectedIndexChanged="DDL_Village_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" Font-Size="12px">
                                    </asp:DropDownList></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 335px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:Label ID="Label2" runat="server" Font-Size="Small" Text="ग्राम पंचायत :"
                                        Width="136px"></asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px; width: 206px;">
                                    <asp:TextBox ID="txt_GramPanchayat" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 103px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
    <asp:Label ID="lbl_Halka" runat="server" Font-Size="Small" Width="161px">पटवारी हल्का क्रमांक :</asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 145px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:TextBox ID="txtHalkaNo" runat="server"></asp:TextBox></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 335px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px; width: 206px;">
                                </td>
                            </tr>
                            <tr >
                                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                    border-bottom: white 1px solid; border-collapse: collapse; height: 21px; width: 103px;" align="left">
                                    <asp:Label ID="lbl_FarName" runat="server" Font-Size="Small">किसान का नाम :</asp:Label></td>
                                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                    border-bottom: white 1px solid; border-collapse: collapse; height: 21px; width: 145px;" align="left">

    <asp:TextBox ID="txtFarmerName" runat="server"></asp:TextBox></td>
                                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                    border-bottom: white 1px solid; border-collapse: collapse; height: 21px; width: 335px;" align="left">
                                    <asp:Label ID="lbl_FatName"  Text="किसान के पति/पिता का नाम :" runat="server" Font-Size="Small" Width="123px"></asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px; width: 206px;">
    <asp:TextBox ID="txt_FatherName" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 103px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:Label ID="Label3" runat="server" Font-Size="Small">मोबाइल नं</asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 145px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:TextBox ID="txt_mobileno" runat="server"></asp:TextBox></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 335px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px; width: 206px;">
                                    </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:Label ID="Label4" runat="server" Font-Size="Small" Width="361px">वर्ग(अनु. जाति / अनु. जनजाति / अ.पि.व. / सामान्य (जो लागू हो))</asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 335px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:DropDownList ID="ddl_Category" runat="server" Font-Bold="True">
                                    </asp:DropDownList></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px; width: 206px;">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 103px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                 <asp:Label ID="lbl_RinPustikanumber" runat="server" Text="स्वयं की ऋणपुस्तिका क्रमांक :-" Font-Size="Small" Width="180px"></asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 145px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:TextBox ID="txt_RinPustikanumber" runat="server"></asp:TextBox></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 335px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px; width: 206px;">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:Label ID="Label5" runat="server" Font-Size="Small" Width="389px">किसान का यू.आई.डी एनरोलमेंट क्रं. / आधार नम्बर (यदि हो तो)</asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 335px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:TextBox ID="txt_UIDAadhaar" runat="server"></asp:TextBox></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px; width: 206px;">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:Label ID="Label6" runat="server" Font-Size="Small" Text="किसान के बैंक का नाम (जिसमे खरीदि मात्रा देय जमा होती है) "></asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 335px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:TextBox ID="txt_bankname" runat="server"></asp:TextBox></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px; width: 206px;">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:Label ID="Label7" runat="server" Font-Size="Small" Text="किसान के बैंक खाता क्रमांक (जिसमे खरीदि मात्रा देय जमा होती है) "></asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 335px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:TextBox ID="txt_BnkAccno" runat="server"></asp:TextBox></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px; width: 206px;">
                                </td>
                            </tr>
                            
                            
                      
                            <tr>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 103px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:Label ID="Label8" runat="server" Font-Size="Small" Text="भूमि का प्रकार  "></asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 145px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:DropDownList ID="ddl_LandType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_LandType_SelectedIndexChanged" Font-Bold="True" Font-Size="12px">
                                    </asp:DropDownList></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 335px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
    </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px; width: 206px;">
                                    </td>
                            </tr>
                            <tr>
                                <td align="left" style="border-right: black 2px solid; border-top: black 2px solid;
                                    border-left: black 2px solid; border-bottom: black 2px solid; border-collapse: collapse;
                                    height: 253px" colspan="4" valign="top">
                                   
                                    <asp:Panel ID="Panel_landType" Width="810px" Height="200px" ScrollBars="Horizontal" runat="server">
                                        <div style="text-align: left">
                                            <table border="2" cellpadding="2" cellspacing="2" style="font-size: 10pt; width: 100%;
                                                height: 150px">
                                                <caption>
                                                    <asp:Label ID="Label10" runat="server" Text="भूमि का विवरण "></asp:Label></caption>
                                                <tr>
                                                    <td align="left" rowspan="2" style="width: 100px">
                                                        <asp:Label ID="Label9" runat="server" Text="ग्राम "></asp:Label></td>
                                                    <td align="left" rowspan="2" style="width: 71px">
                                                        <asp:Label ID="Label11" runat="server" Text="फसल का नाम "></asp:Label></td>
                                                    <td align="left" rowspan="2" style="width: 100px">
                                                        <asp:Label ID="lbl_molbhoname" runat="server" Text="मूल भूमि स्वामी का नाम  " Width="147px"></asp:Label></td>
                                                    <td style="width: 44px;" align="left" rowspan="2">
                                                        <asp:Label ID="lbl_moolbhorinp" runat="server" Text="मूल भू-स्वामी की ऋण पुस्तिका क्र." Width="95px"></asp:Label></td>
                                                    <td align="left" rowspan="2" style="width: 38px">
                                                        <asp:Label ID="Label14" runat="server" Text="खसरा क्रमांक " Width="74px"></asp:Label></td>
                                                    <td align="left" rowspan="2">
                                                        <asp:Label ID="txt_rakbano" runat="server" Text="रकबा"></asp:Label></td>
                                                    <td colspan="2" style="height: 12px" align="left">
                                                        <asp:Label ID="Label16" runat="server" Text="रकबा जिसमे उपार्जन हेतु फसल बोई गई है "
                                                            Width="155px"></asp:Label></td>
                                                    <td colspan="2" style="height: 12px" align="left">
                                                        <asp:Label ID="Label17" runat="server" Text="कुल सम्भावित उपज  (मात्रा क्विंटल मे)" Width="116px"></asp:Label></td>
                                                    <td style="width: 100px;" align="left" rowspan="2">
                                                        <asp:Label ID="Label18" runat="server" Text="उपार्जन हेतु दी जाने वाली मात्रा (क्विंटल मे) "
                                                            Width="76px"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 1px; height: 13px;">
                                                        <asp:Label ID="Label19" runat="server" Text="(सिंचित)"></asp:Label></td>
                                                    <td style="width: 33px; height: 13px;">
                                                        <asp:Label ID="Label20" runat="server" Text="(असिंचित)"></asp:Label></td>
                                                    <td style="width: 60px; height: 13px;">
                                                        <asp:Label ID="Label21" runat="server" Text="(सिंचित)"></asp:Label></td>
                                                    <td style="width: 45px; height: 13px;">
                                                        <asp:Label ID="Label22" runat="server" Text="(असिंचित)"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100px; height: 49px;">
                                                        <asp:DropDownList ID="ddl_GridVillage" runat="server" Font-Bold="True" Font-Size="12px">
                                                        </asp:DropDownList></td>
                                                    <td style="width: 71px; height: 49px;"><asp:DropDownList ID="ddl_GridCrop" runat="server" Font-Bold="True" Font-Size="12px">
                                                    </asp:DropDownList></td>
                                                    <td style="width: 100px; height: 49px;">
                                                        <asp:TextBox ID="txt_MoolBhoSwamiName" runat="server"></asp:TextBox></td>
                                                    <td style="width: 44px; height: 49px;">
                                                        <asp:TextBox ID="txt_Grid_MoolRinPustikaNo" runat="server" Width="92px"></asp:TextBox></td>
                                                    <td style="width: 38px; height: 49px;">
                                                        <asp:TextBox ID="txt_Grid_KhasaraNo" runat="server" Width="77px"></asp:TextBox></td>
                                                    <td style="height: 49px;">
                                                        <asp:TextBox ID="txt_Grid_rakaba" runat="server" Width="62px"></asp:TextBox></td>
                                                    <td style="width: 1px; height: 49px;">
                                                        <asp:TextBox ID="txt_Grid_RKBSinchit" runat="server" Width="47px"></asp:TextBox></td>
                                                    <td style="width: 33px; height: 49px;">
                                                        <asp:TextBox ID="txt_Grid_RKBASinchit" runat="server" Width="51px"></asp:TextBox></td>
                                                    <td style="width: 60px; height: 49px;">
                                                        <asp:TextBox ID="txt_Grid_AchinchitQty" runat="server" Width="55px"></asp:TextBox></td>
                                                    <td style="width: 45px; height: 49px;">
                                                        <asp:TextBox ID="txt_Grid_AsinchitQty" runat="server" Width="55px"></asp:TextBox></td>
                                                    <td style="width: 100px; height: 49px;">
                                                        <asp:TextBox ID="txt_Grid_PrcQty" runat="server" Width="75px"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                        </div>
                                    
                                    
                                    
                                    
                                    </asp:Panel>
                                    <asp:Button ID="btn_LandReocord" runat="server" BorderStyle="None" Font-Bold="True" ForeColor="Navy"
                                        Text="भूमि का विवरण जोडें " OnClick="btn_LandReocord_Click" /></td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4" style="border-right: black 2px solid; border-top: black 2px solid;
                                    border-left: black 2px solid; border-bottom: black 2px solid; border-collapse: collapse;
                                    height: 253px" valign="top">
                                  <asp:GridView ID="GridLandRecord" runat="server"
                BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Size="10pt" AllowPaging="True" OnSelectedIndexChanged="GridLandRecord_SelectedIndexChanged" AutoGenerateColumns="False" Font-Names="Calibri" AutoGenerateDeleteButton="True" OnRowDeleting="GridLandRecord_RowDeleting">
                <RowStyle ForeColor="#330099" BackColor="White" />
            <Columns>
                                            <asp:BoundField DataField="VillageName" HeaderText="ग्राम " />
                                            <asp:BoundField DataField="crop_Name" HeaderText="फसल का नाम " />
                                            <asp:BoundField DataField="MoolBhoSwami" HeaderText="मूल भूमि स्वामी का नाम  " />
                                            <asp:BoundField DataField="BhoSwamiRinPustikaNo" HeaderText="मूल भू-स्वामी  की ऋण पुस्तिका क्रमांक " />
                                            <asp:BoundField DataField="khasarano" HeaderText="खसरा नम्बर  " />
                                            <asp:BoundField DataField="rakba" HeaderText="रकबा " />
                                            <asp:BoundField DataField="Rakba_crop_sinchit" HeaderText="रकबा जिसमे उपार्जन हेतु फसल बोई गई है (सिंचित)" />
                                            <asp:BoundField DataField="Rakba_crop_asinchit" HeaderText="रकबा जिसमे उपार्जन हेतु फसल बोई गई है (असिंचित)" />
                                            <asp:BoundField DataField="Rakba_crop_sinchit_qty" HeaderText="कुल सम्भावित(मात्रा क्विंटल मे) (सिंचित)" />
                                            <asp:BoundField DataField="Rakba_crop_asinchit_qty" HeaderText="कुल सम्भावित(मात्रा क्विंटल मे) (असिंचित)" />
                                            <asp:BoundField DataField="Procured_qty" HeaderText="उपार्जन हेतु दी जाने वाली मात्रा (क्विंटल मे) " />
                                            <asp:BoundField DataField="Village_ID" HeaderText="VillageID" >
                                                <HeaderStyle Font-Size="0px" Width="0px" />
                                                <ItemStyle Font-Size="0px" Width="0px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Village_ID" HeaderText="CropID" >
                                                <ControlStyle Font-Size="0px" />
                                                <FooterStyle Font-Size="0px" Width="0px" />
                                                <HeaderStyle Font-Size="0px" Width="0px" />
                                                <ItemStyle Width="0px" Font-Size="0px" />
                                            </asp:BoundField>
                                              <asp:BoundField DataField="LandType" HeaderText="LandType" >
                                                <HeaderStyle Font-Size="0px" Width="0px" />
                                                <ItemStyle Font-Size="0px" Width="0px" />
                                            </asp:BoundField>
                                        </Columns>
                                        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" Font-Size="X-Small" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 103px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 17px">
                                 </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 145px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 17px">
                                    </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 335px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 17px">
    </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 17px; width: 206px;">
                                    </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 7px">
                                    <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Size="12px" Text="किस खरीदी केन्द्र पर उपज दिया जाना प्रस्तावित है "></asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 335px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 7px">
                                </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 206px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 7px">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 103px; border-bottom: white 1px solid; border-collapse: collapse; height: 23px;">
                                    <asp:Label ID="Label23" runat="server" Font-Size="Small">जिला :</asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 145px; border-bottom: white 1px solid; border-collapse: collapse; height: 23px;">
                                    <asp:DropDownList ID="ddl_DistPrc" runat="server" Font-Bold="True" Font-Size="12px">
                                    </asp:DropDownList></td>
                                <td align="left" colspan="2" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse; height: 23px;">
                                    <asp:Label ID="Label24" runat="server" Font-Size="Small">समिति :</asp:Label>
                                    <asp:DropDownList ID="ddl_samity" runat="server" Font-Bold="True" Font-Size="12px">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 103px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 7px">
                                    <asp:Label ID="Label25" runat="server" Font-Size="Small">स्थान  :</asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 145px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 7px">
                                    <asp:TextBox ID="txt_place" runat="server"></asp:TextBox></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 335px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 7px">
                                    </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 206px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 7px">
                                    
                                    </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="3" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 7px">
                                    <asp:Label ID="Label28" runat="server" Font-Size="Small">किस तिथि को उपज दिया जाना सम्भावित है :</asp:Label>
                                    <asp:TextBox ID="txt_datetithi" runat="server"></asp:TextBox>
                                     <script type="text/javascript">new tcal ({'formname': '0','controlname': 'ctl00_ContentPlaceHolder1_txt_datetithi'});
                                     </script>
                                    
                                    </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 206px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 7px">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="3" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 7px">
                                    <asp:Label ID="Label27" runat="server" Font-Size="12px" Text="केवल कार्यालयीन उपयोग के लिये"></asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 206px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 7px">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="3" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 7px">
                                    <asp:Label ID="Label26" runat="server" Font-Bold="False" Font-Size="12px" Text="कलेक्टर द्वारा सम्बन्धित क्षेत्र हेतु निर्धारित प्रति हेक्टर ओसत उत्पादन के  आधार पर आधिकतम खरीदि योग्य उपज की मात्रा ( क्विंटल मे ) "
                                        Width="661px"></asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 206px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 7px">
                                    <asp:TextBox ID="txt_ColMaxQty" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>&nbsp;
                                </td>
                            </tr>
                        <tr>
                                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                    border-bottom: white 1px solid; border-collapse: collapse; height: 10px; width: 103px;" align="left">
                                    
                                  </td>
                            <td align="left" colspan="2" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 10px">
                                &nbsp;<asp:Button ID="btn_Save" runat="server" Text="सुरक्षित करें" OnClick="btn_Save_Click" Width="98px" />&nbsp;
                                <asp:Button
                                    ID="btn_update" runat="server" Text="संशोधित करें " Visible="False" OnClick="btn_update_Click" />
                                <asp:Button ID="btn_can" runat="server" Text="केंसिल करें " OnClick="btn_can_Click" /></td>
                                 <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                    border-bottom: white 1px solid; border-collapse: collapse; height: 10px; width: 206px;" align="left">
    </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;">
                                    &nbsp;</td>
                            </tr>
                       
                            
                        </table>
                    </asp:Panel>
                    &nbsp; &nbsp; &nbsp;&nbsp;
                </td>
            </tr>
   
         
              <tr class="HeadingBlue">
                <td colspan="4" style="border-collapse: collapse; border:solid 1px white;
                    height: 3px; text-align: center; background-color: dimgray; width: 580px;">
                  
                </td>
            </tr>
        </table>
            </td>
        </tr>
        <tr>
            <td style="width: 103px">
      
       
            </td>
        </tr>
    </table>
  
        

</asp:Content>
