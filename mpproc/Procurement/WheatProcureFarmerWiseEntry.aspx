<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WheatProcureFarmerWiseEntry.aspx.cs"
    Inherits="Procurement_WheatProcureFarmerWiseEntry" MasterPageFile="~/mpproc/MasterPage/AgencyMaster.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="../../js/calendar_eu.js"></script>

   
    <script type="text/javascript" language="javascript">
function CheckIsNumericInt(tx)
{
var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode < 46) || (AsciiCode > 57) ||(AsciiCode == 46 ) )
{
alert('Please enter only Numeric');
event.cancelBubble = true;
event.returnValue = false;
}


}

function CheckIsNumeric(e,tx)
    {         
        var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;  
        
       // alert(AsciiCode);                      
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
            if (count > 2 && AsciiCode != 8)  
            {
                alert("Only 2 decimal digits allowed.");
                return false;
            }
        }
    }

function chkFields() 
{

  
    var pDate=document.getElementById('ctl00_ContentPlaceHolder1_DaintyDate1');
    var fname=document.getElementById('ctl00_ContentPlaceHolder1_DDL_Farmer');
    var control1 = document.getElementById('ctl00_ContentPlaceHolder1_txt_paidAmount');
    var control2 = document.getElementById('ctl00_ContentPlaceHolder1_DaintyDate2');
    var control3 = document.getElementById('ctl00_ContentPlaceHolder1_txt_chno');
    var chkval = document.getElementById('ctl00_ContentPlaceHolder1_RD_Btn_Yes');
    
    
   if(pDate.value=='')
   {
       alert('Enter Procurement Date');
       event.returnValue = false;
  
   }
  
   if(fname.value=='')
   {
     alert('Please Select Farmer Name:');
       event.returnValue = false;
   
   }

   if(chkval.value=='RD_Btn_Yes')
   {
  
 
     if (control1.value <= 0.0) 
    {
      alert('Payment should not be 0');
       event.returnValue = false;
    } 
    
    if(control1.value=='' || control2.value=='' || control3.value=='')
    {
    
       alert('Paid Amount ,Cheque Date and cheque No is Required... ');
    event.returnValue = false;
}
   }
    
 

}

function CheckIsChar(e,tx)
    {         
        var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;  
//     alert(AsciiCode);        
       if ((AsciiCode < 65 && AsciiCode !=32 && AsciiCode!=44 && AsciiCode!=46 && AsciiCode!=13) || (AsciiCode > 122))
        {
            alert('Please enter only character');
            return false;
        }                
 }





    </script>


    
   
    <table width="800" cellpadding="0" cellspacing="0" style="margin-left: 0px; border-collapse: collapse;
        border: solid 1px white; background-color: #cccc66" id="TABLE1" onclick="return TABLE1_onclick()">
        <tr align="center" class="HeadingBlue">
            <td colspan="4" style="height: 15px; border-collapse: collapse; border: solid 1px white;
                background-color: dimgray; width: 772px;">
                <asp:Label ID="lbl_tit_wheat" runat="server" Font-Bold="True" ForeColor="White" Text="Wheat Procurement Farmer Wise Entry"></asp:Label></td>
        </tr>
        <tr align="center">
            <td colspan="4" align="right" style="border-right: white 1px solid; border-top: white 1px solid;
                border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                height: 14px; width: 772px;">
                <asp:Label ID="lbl_Mas_Q_IS" runat="server" Font-Size="Small" Text="(Qty. is in Qtl.Kgs, Amount in Rs.)"
                    Font-Bold="True" Font-Italic="True" ForeColor="Firebrick"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="border-right: white 1px solid; border-top: white 1px solid;
                border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                height: 137px; width: 772px;">
                <asp:Panel ID="pnlFCI_OTDepot" runat="server" Width="100%">
                    <table cellpadding="0" cellspacing="0" style="width: 100%; border-collapse: collapse;
                        border: solid 1px white;">
                        <tr id="trFCIa_dist_depo">
                            <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                border-bottom: white 1px solid; border-collapse: collapse; height: 21px;"
                                align="left">
                                <asp:Label ID="lbl_Dist" runat="server" Font-Size="Small">District:</asp:Label>
                                <asp:Label ID="lbl_DistRes" runat="server" Font-Size="Small" ForeColor="Blue"></asp:Label></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 295px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                                <asp:Label ID="lbl_Agency" runat="server" Font-Size="Small">Agency:</asp:Label>
                                <asp:Label ID="lbl_AgencyRes" runat="server" Font-Size="Small" ForeColor="Blue"></asp:Label></td>
                            <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                border-bottom: white 1px solid; border-collapse: collapse; height: 21px; width: 181px;"
                                align="left">
                                &nbsp;<asp:Label ID="lbl_MarkSeas" Text="Marketing Season:" runat="server" Font-Size="Small"
                                    Width="113px"></asp:Label></td>
                            <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                border-bottom: white 1px solid; border-collapse: collapse; height: 21px; width: 272px;"
                                align="left">
                                &nbsp;<asp:Label ID="lbl_MarSeasRes" runat="server" Font-Size="Small" ForeColor="Blue"></asp:Label></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 272px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                                <asp:Label ID="lbl_CropYear" runat="server" Font-Size="Small" Text="Crop Year:"></asp:Label></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px; width: 162px;">
                                &nbsp;<asp:Label ID="lbl_CropYearRes" runat="server" Font-Size="Small" ForeColor="Blue"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                                <asp:Label ID="lbl_Procondate" runat="server" Font-Size="Small"> Procurement on Date:(DD/MM/YYYY):</asp:Label></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 295px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                                <asp:TextBox ID="DaintyDate1" runat="server" Width="103px"></asp:TextBox>

                                <script type="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate1'
	    });
                                </script>

                            </td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 181px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                                &nbsp;<asp:Label ID="lbl_pc" runat="server" Font-Size="Small">Purchase Center:</asp:Label></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 272px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                                &nbsp;
                                <asp:DropDownList ID="DDL_PC" runat="server" Enabled="False" Width="136px" OnSelectedIndexChanged="DDL_PC_SelectedIndexChanged">
                                </asp:DropDownList></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 272px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                                <asp:Label ID="lbl_Commodity" runat="server" Font-Size="Small">Commodity:</asp:Label></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px; width: 162px;">
                                &nbsp;<asp:DropDownList ID="DDL_Commodity" runat="server">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                            </td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 295px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                            </td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 181px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                                <asp:Button ID="btn_fecth" runat="server" Text="Fetch" Width="75px" OnClick="btn_fecth_Click" /></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 272px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                            </td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 272px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                            </td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px; width: 162px;">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                                <asp:Label ID="lbl_FarSelection" runat="server" Font-Size="14px" Font-Bold="True"
                                    ForeColor="Maroon" Width="133px">Farmer Selection:</asp:Label>
                                <asp:Label ID="lblfdetail" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Maroon"
                                    Visible="False" Width="133px">Farmer Details:</asp:Label></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 181px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                            </td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 181px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                            </td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 181px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                            </td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 181px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                            </td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px; width: 181px;">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                                <asp:Label ID="lbl_Dist2" runat="server" Font-Size="Small">District:</asp:Label></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 295px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                                <asp:DropDownList ID="DDL_Dist" runat="server" OnSelectedIndexChanged="DDL_Dist_SelectedIndexChanged"
                                    AutoPostBack="True" Width="130px">
                                </asp:DropDownList></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 181px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                                <asp:Label ID="lbl_Tahsil" runat="server" Font-Size="Small">Tahsil:</asp:Label>
                                <asp:Label ID="lblfrname" runat="server" Font-Size="Small" Visible="False">Farmer Name:</asp:Label></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 272px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                                &nbsp;<asp:DropDownList ID="DDL_Tah" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_Tah_SelectedIndexChanged"
                                    Width="142px">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtFarmer" runat="server" Visible="False" Width="137px"></asp:TextBox></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 272px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                            </td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px; width: 162px;">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                                <asp:Label ID="lbl_village" runat="server" Font-Size="Small">Village:</asp:Label></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 295px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                                <asp:DropDownList ID="DDL_Village" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_Village_SelectedIndexChanged"
                                    Width="130px">
                                </asp:DropDownList></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 181px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                                <asp:Label ID="lbl_Farmer" runat="server" Font-Size="Small">Farmer Name:</asp:Label></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 272px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                                <asp:DropDownList ID="DDL_Farmer" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_Farmer_SelectedIndexChanged"
                                    Width="146px">
                                </asp:DropDownList></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 272px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                            </td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 162px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px; color: #0033ff;">
                                <asp:Label ID="lbl_RationcardNo" runat="server" Font-Size="Small">Ration Card No:</asp:Label>
                                <%-- <asp:Label ID="lbl_Village" runat="server" Font-Size="Small" Text="Village"></asp:Label>--%>
                            </td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 295px; color: #0033ff; border-bottom: white 1px solid;
                                border-collapse: collapse; height: 21px">
                                <asp:Label ID="lbl_rcNoRes" runat="server" Font-Size="Small" Font-Bold="True" ForeColor="Purple"></asp:Label></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 160px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px; color: #0033ff;">
                                <asp:Label ID="lbl_RCT" runat="server" Font-Size="Small">Ration Card Type:</asp:Label>
                                <%-- <asp:DropDownList ID="DDL_Village" runat="server" OnSelectedIndexChanged="DDL_Village_SelectedIndexChanged">
                                    </asp:DropDownList>--%>
                            </td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 272px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px; color: #0033ff;">
                                &nbsp;<asp:Label ID="lbl_RcType" runat="server" Font-Size="Small" Width="63px" Font-Bold="True"
                                    ForeColor="Purple"></asp:Label></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 272px; color: #0033ff; border-bottom: white 1px solid;
                                border-collapse: collapse; height: 21px">
                                <asp:Label ID="lbl_KhasaraNo" runat="server" Font-Size="Small" Width="80px">Khasara No:</asp:Label></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px; width: 160px; color: #0033ff;">
                                &nbsp;<asp:Label ID="lbl_KhasaraRes" runat="server" Font-Size="Small" Font-Bold="True"
                                    ForeColor="Purple"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; color: #0033ff; border-bottom: white 1px solid;
                                border-collapse: collapse; height: 21px">
                                <asp:Label ID="lbl_HN" runat="server" Font-Size="Small">Halka No:</asp:Label></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 295px; color: #0033ff; border-bottom: white 1px solid;
                                border-collapse: collapse; height: 21px">
                                <asp:Label ID="lbl_khRes" runat="server" Font-Size="Small" Font-Bold="True" ForeColor="Purple"></asp:Label></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 160px; color: #0033ff; border-bottom: white 1px solid;
                                border-collapse: collapse; height: 21px">
                                <asp:Label ID="lbl_B1" runat="server" Font-Size="Small">B1:</asp:Label></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 272px; color: #0033ff; border-bottom: white 1px solid;
                                border-collapse: collapse; height: 21px">
                                <asp:Label ID="lbl_B1Res" runat="server" Font-Size="Small" Font-Bold="True" ForeColor="Purple"></asp:Label></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 272px; color: #0033ff; border-bottom: white 1px solid;
                                border-collapse: collapse; height: 21px">
                            </td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 160px; color: #0033ff; border-bottom: white 1px solid;
                                border-collapse: collapse; height: 21px">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                                <asp:Label ID="lbl_QuanProc" runat="server" Font-Size="Small">Quantity Procured(Qtls)</asp:Label></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 295px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                                <asp:TextBox ID="txtQuan" runat="server" OnTextChanged="txtQuan_TextChanged" AutoPostBack="True"  style="text-align:right"
                                    Width="109px" CssClass="txtAlign"></asp:TextBox>
                                <asp:Label ID="Label8" runat="server" ForeColor="Red" Text="*"></asp:Label></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 181px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                                <asp:Label ID="lbl_PayAm2Far" runat="server" Font-Size="Small">Payable Amount to Farmer (msp+bonus)*Qty:</asp:Label></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 272px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                                <asp:TextBox ID="txtAmount" runat="server" Enabled="False" Width="115px" style="text-align:right"></asp:TextBox></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 272px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                            </td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px; width: 162px;">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                                <asp:Label ID="lbl_rakbaNo" runat="server" Font-Size="Small" ForeColor="Black">Rakba No:</asp:Label></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 295px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                                <asp:TextBox ID="txt_rkbNo" runat="server" Width="115px"></asp:TextBox></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 181px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                                <asp:Label ID="lbl_Remark" runat="server" Font-Size="Small" ForeColor="Black">Remark:</asp:Label></td>
                            <td align="left" colspan="2" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                                <asp:TextBox ID="txt_Remark" runat="server" TextMode="MultiLine" Font-Size="11pt" Width="257px"></asp:TextBox></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 162px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                                <asp:Label ID="lbl_PayDeatils" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Maroon">Payment Details : </asp:Label></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 181px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                            </td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 181px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                            </td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 181px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                            </td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 181px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                            </td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px; width: 181px;">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 15px">
                                <asp:Label ID="lbl_Chek" runat="server" Font-Size="Small" Width="145px">Do you want to enter payment for transaction? :</asp:Label></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 295px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 15px">
                                <asp:RadioButton ID="RD_Btn_Yes" runat="server" Font-Bold="True" Font-Size="X-Small"
                                    GroupName="PayTrans" Text="Yes" OnCheckedChanged="RD_Btn_Yes_CheckedChanged"
                                    AutoPostBack="True" /><asp:RadioButton ID="RD_Btn_No" runat="server" Font-Bold="True"
                                        Font-Size="X-Small" GroupName="PayTrans" Text="No" OnCheckedChanged="RD_Btn_No_CheckedChanged"
                                        AutoPostBack="True" Checked="True" /></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 181px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 15px">
                                <asp:Label ID="lbl_paidAm" runat="server" Font-Size="Small" Visible="False" Width="85px">Paid Amount</asp:Label></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 272px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 15px">
                                <asp:TextBox ID="txt_paidAmount" runat="server" Visible="False" Width="115px" style="text-align:right"></asp:TextBox></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 272px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 15px">
                                </td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 15px; width: 162px;">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                                <asp:Label ID="lbl_Cheque" runat="server" Font-Size="Small" Width="87px" Visible="False">Cheque Date.</asp:Label></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 295px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                                <asp:TextBox ID="DaintyDate2" runat="server" Visible="False" Width="115px"></asp:TextBox>

                                <script type="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate2'
	    });


                                </script>

                            </td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 181px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                                <asp:Label ID="lbl_chNo" runat="server" Font-Size="Small" Visible="False" Width="87px">Cheque No.</asp:Label></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 272px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                                <asp:TextBox ID="txt_chno" runat="server" Visible="False" Width="119px"></asp:TextBox></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 272px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px">
                            </td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 21px; width: 162px;">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 11px">
                                <asp:Label ID="lblfid" runat="server" Visible="False"></asp:Label></td>
                            <td align="right" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 295px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 11px">
                                <asp:Button ID="btn_Reset" runat="server" Text="Reset" OnClick="btn_Reset_Click"
                                    Width="103px" /></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 181px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 11px">
                                <asp:Button ID="btn_AddNew" runat="server" Text="AddNew" OnClick="btn_AddNew_Click"
                                    Width="108px" />
                                <asp:Button ID="btn_update" runat="server" OnClick="btn_update_Click" Text="Update"
                                    Visible="False" /></td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 272px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 11px">
                                &nbsp;</td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; width: 272px; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 11px">
                            </td>
                            <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 11px; width: 162px;">
                                &nbsp;
                                </td>
                        </tr>
                    </table>
                </asp:Panel>
                &nbsp;
                
            </td>
        </tr>
        <tr class="HeadingBlue">
            <td colspan="4" style="border-collapse: collapse; border: solid 1px white; height: 15px;
                text-align: center; background-color: dimgray; width: 772px;">
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td style="height: 181px">
                <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CC9966"
                    Width="800px" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Size="10pt"
                    AutoGenerateSelectButton="True" AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging"
                    OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDeleting="GridView1_RowDeleting"
                    OnRowDataBound="GridView1_RowDataBound" Font-Names="Calibri">
                    <RowStyle BackColor="White" ForeColor="#330099" />
                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                    <Columns>
                        <asp:BoundField DataField="FarmerName" HeaderText="Farmer Name" />
                        <asp:BoundField DataField="FarmerId" HeaderText="FarmerId" />
                        <asp:BoundField HeaderText="Commodity" DataField="CommodityName" />
                        <asp:BoundField HeaderText="Qty Procured" DataField="QtyProcured" />
                        <asp:BoundField HeaderText="Amount Payable" DataField="AmtPayable" />
                        <asp:BoundField HeaderText="Amount Paid" DataField="AmtPaid" />
                        <asp:BoundField HeaderText="Cheque No" DataField="ChequeNo" />
                        <asp:TemplateField HeaderText="Cheque Date">
                            <ItemTemplate>
                                <asp:Label ID="lblChallan" runat="server" Text='<%# Eval("Chequedate").ToString()%>'>
                                </asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridlarohead" />
                            <ItemStyle CssClass="griditemlaro" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText=" Status " DataField="status" />
                        <asp:BoundField DataField="RakbaNo" HeaderText="Rakba" SortExpression="RakbaNo" />
                        <asp:BoundField DataField="Remark" HeaderText="Remark" />
                        <asp:BoundField DataField="ProcAgentFarmerID" HeaderText="AllC_id">
                            <HeaderStyle Font-Size="0px" />
                            <ItemStyle Font-Size="0px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
