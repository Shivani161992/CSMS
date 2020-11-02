<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="mpsc_move_challan.aspx.cs" Inherits="mpsc_move_challan" Title="MPSCSC Movement Challan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat ="server">
    <script type="text/javascript">
function CheckCalDate(tx){
var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode > 0))
{
alert('Please Click on Calander Controll to Enter Date');
event.cancelBubble = true;
event.returnValue = false;
}
}
</script>

<div id="hedl"> &nbsp;</div>
<div id ="ronewmargin">
<center>
 <table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: lightslategray 3px double; border-top: lightslategray 3px double; border-left: lightslategray 3px double; border-bottom: lightslategray 3px double; width: 695px; font-size: 10pt;"  class ="tablelayout" id="TABLE1" onclick="return TABLE1_onclick()">
     <tr>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: white">
         </td>
         <td align="center" colspan="2" style="font-size: 10pt; color: white; position: static;
             background-color: #999999">
    <asp:Label ID="lbldepositstock" runat="server" Text="Deposition Of Stock" Width="150px" Font-Bold="True"></asp:Label></td>
         <td align="left" 
             style="font-size: 10pt; position: static; background-color: white" colspan="2">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: lightslategray; height: 19px;">
         </td>
         <td align="center" colspan="2" style="background-color: lightslategray; height: 19px;">
             <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="White"
                 Text="Reciept Details" Width="155px"></asp:Label></td>
         <td align="left" style="background-color: lightslategray; height: 19px;" 
             colspan="2">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #ffffcc; width: 213px;">
         </td>
         <td align="left" style="font-size: 10pt; position: static; background-color: #ffffcc; width: 262px;">
             &nbsp;</td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #ffffcc; width: 97px;">
         </td>
         <td align="left" 
             style="font-size: 10pt; position: static; background-color: #ffffcc; width: 213px;" 
             colspan="2">
         </td>
     </tr>
 <tr>
 <td class ="tdmarginro" style="background-color: #ffff99; font-size: 10pt; position: static;" >
     <asp:Label ID="lblSorcePfArrival" runat="server" Text="Source of Arrival" Width="116px"></asp:Label></td>
     <td class="tdmarginro" colspan="2" style="font-size: 10pt; position: static; background-color: #ffff99">
    <asp:DropDownList ID="ddlsarrival" runat="server" Width="322px" OnSelectedIndexChanged="ddlsarrival_SelectedIndexChanged" AutoPostBack="True" OnDataBound = "ddlsarrival_DataBound">
           
     </asp:DropDownList>
             <asp:Label ID="fcidist" runat="server" Width="80px" Visible="False"></asp:Label></td>
  <td align="left" style="background-color: #ffff99; font-size: 10pt; position: static;"> 
      <asp:Label ID="lbl_pass" runat="server" style="font-weight: 700" 
          Text="Password" Visible="False"></asp:Label>
     </td>
  <td align="left" style="background-color: #ffff99; font-size: 10pt; position: static;"> 
      <asp:TextBox ID="txt_pass" runat="server" AutoPostBack="True" 
          ontextchanged="txt_pass_TextChanged" TextMode="Password" Visible="False"></asp:TextBox>
     </td>
 </tr>
 <tr>
 <td class ="tdmarginro" style="background-color: #ffff99; font-size: 10pt; position: static;" >
      <asp:Label ID="lblChallanNumber" runat="server" Text="Challan No."></asp:Label></td>
     <td class="tdmarginro" colspan="2" style="font-size: 10pt; position: static; background-color: #ffff99">
     <asp:DropDownList ID="ddlchallan" runat="server" Width="156px" AutoPostBack="True" OnSelectedIndexChanged="ddlchallan_SelectedIndexChanged" >
        
         <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
         
     </asp:DropDownList></td>
  <td align="left" 
         style="background-color: #ffff99; font-size: 10pt; position: static;" 
         colspan="2"> 
      &nbsp;</td>
 </tr>
     <tr>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #ffff99; width: 213px;">
             <asp:Label ID="lblRecFromDist" runat="server" Text="Sending District" Visible="False" Width="107px"></asp:Label></td>
         <td align="left" style="font-size: 10pt; width: 213px; position: static; background-color: #ffff99">
             <asp:DropDownList ID="ddldistrict" runat="server" Visible="False" Width="153px" AutoPostBack="True" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList>
             <asp:Label ID="dcode" runat="server" Visible="False"></asp:Label></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #ffff99; width: 97px;">
             <asp:Label ID="lblNameDepot" runat="server" Text="Sending Depot" Visible="False" Width="105px"></asp:Label></td>
         <td style="font-size: 10pt; position: static; background-color: #ffff99; width: 213px;" 
             colspan="2">
             <asp:DropDownList ID="ddlissue" runat="server" Visible="False" Width="153px">
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList>
             <asp:Label ID="icode" runat="server" Visible="False"></asp:Label></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #ffff99; font-size: 10pt; position: static; width: 97px; height: 25px;">
             <asp:Label ID="lblrack" runat="server" Text="Rack No." Visible="False"></asp:Label></td>
         <td align="left" style="background-color: #ffff99; font-size: 10pt; position: static; width: 97px; height: 25px;">
             <asp:DropDownList ID="ddlrackno" runat="server" Visible="False" Width="153px" OnSelectedIndexChanged="ddlrackno_SelectedIndexChanged1">
             </asp:DropDownList></td>
         <td class="tdmarginro" style="background-color: #ffff99; font-size: 10pt; position: static; width: 97px; height: 25px;">
             <asp:Label ID="lblrailhead" runat="server" Text="Rail Head Name" Visible="False" Width="132px"></asp:Label></td>
         <td style="background-color: #ffff99; font-size: 10pt; position: static; width: 97px; height: 25px;" 
             colspan="2">
             <asp:DropDownList ID="ddlreailhead" runat="server" Visible="False" Width="153px">
             </asp:DropDownList></td>
     </tr>
 <tr>
  <td class ="tdmarginro" style="background-color: #ffff99; font-size: 10pt; position: static; width: 97px;">
             <asp:Label ID="lblMillersName" runat="server" Text="Miller Name" Visible="False"></asp:Label></td>
 <td style="background-color: #ffff99; font-size: 10pt; position: static; width: 97px;" align="left">
     &nbsp;<asp:DropDownList ID="ddlmiller" runat="server" OnSelectedIndexChanged="ddlmiller_SelectedIndexChanged"
                 Visible="False" Width="193px" AutoPostBack="True">
             </asp:DropDownList>
</td>
 <td class ="tdmarginddl" style="background-color: #ffff99; font-size: 10pt; position: static; width: 97px;">
     <asp:Label ID="lblDateOfChallan" runat="server" Text="Challan Date"></asp:Label></td>
  <td align="left" 
         style="background-color: #ffff99; font-size: 10pt; position: static; width: 97px;" 
         colspan="2">
  <asp:TextBox ID="txtchallandt" runat="server" Width="116px" OnTextChanged="txtchallandt_TextChanged"></asp:TextBox>
   <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_txtchallandt'
	    });
	     </script>
 </td>
 </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #99cc99; font-size: 10pt; position: static; width: 97px;">
             &nbsp;</td>
         <td align="left" style="background-color: #99cc99; font-size: 10pt; position: static; width: 97px;">
             &nbsp;</td>
         <td class="tdmarginddl" style="background-color: #99cc99; font-size: 10pt; position: static; width: 97px;">
             <asp:Label ID="lblfdepo" runat="server" Visible="False"></asp:Label></td>
         <td align="left" 
             style="background-color: #99cc99; font-size: 10pt; position: static; width: 97px;" 
             colspan="2">
             <asp:Label ID="lblfdist" runat="server" Visible="False" Width="19px"></asp:Label></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #99cc99; font-size: 10pt; position: static; height: 19px;">
             <asp:Label ID="lblfcdist" runat="server" Text="FCI AM Office" Visible="False" Width="99px"></asp:Label></td>
         <td align="left" style="background-color: #99cc99; font-size: 10pt; position: static; height: 19px;">
             <asp:DropDownList ID="ddlfcidist" runat="server" Visible="False" Width="153px" OnSelectedIndexChanged="ddlfcidist_SelectedIndexChanged" AutoPostBack="True">
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
         <td class="tdmarginddl" style="background-color: #99cc99; font-size: 10pt; position: static; height: 19px;">
             <asp:Label ID="lbldepofci" runat="server" Text=" Dispatch Depot" Visible="False" Width="138px"></asp:Label></td>
         <td align="left" 
             style="background-color: #99cc99; font-size: 10pt; position: static; height: 19px;" 
             colspan="2">
             <asp:DropDownList ID="ddlfcidepo" runat="server" Visible="False" Width="153px" OnSelectedIndexChanged="ddlfcidepo_SelectedIndexChanged" AutoPostBack="True">
              <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #99cc99; font-size: 10pt; position: static; width: 97px;">
             <asp:Label ID="lblReleaseOrder" runat="server" Text="R.O. No" Visible="False"></asp:Label></td>
         <td align="left" style="background-color: #99cc99; font-size: 10pt; position: static; width: 97px;">
             <asp:TextBox ID="txtrono" runat="server" Visible="False" Width="136px"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RFV_RO" runat="server" ControlToValidate="txtrono"
                 Enabled="False" ErrorMessage="RO NO. Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
         <td class="tdmarginddl" style="background-color: #99cc99; font-size: 10pt; position: static; width: 97px;">
             <asp:Label ID="lbltono" runat="server" Text="T.O No." Visible="False"></asp:Label></td>
         <td align="left" 
             style="background-color: #99cc99; font-size: 10pt; position: static; width: 97px;" 
             colspan="2">
             <asp:TextBox ID="txttono" runat="server" Width="151px" Visible="False"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RFV_TO" runat="server" ControlToValidate="txttono"
                 Enabled="False" ErrorMessage="T.O.  NO. Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #99cc99; font-size: 10pt; position: static; height: 23px;">
             <asp:Label ID="lblparty" runat="server" Text="Supplier Name" Width="99px"></asp:Label></td>
         <td class="tdmarginddl" colspan="2" style="background-color: #99cc99; font-size: 10pt; position: static; height: 23px;">
             <asp:DropDownList ID="DropDownList1" runat="server" Width="356px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
             </asp:DropDownList></td>
         <td align="left" 
             style="background-color: #99cc99; font-size: 10pt; position: static; height: 23px;" 
             colspan="2">
             </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="font-size: 10pt; position: static; height: 23px; background-color: #99cc99">
             <asp:Label ID="lblsupplierorderno" runat="server" Text="Supplier Order No" Width="123px"></asp:Label></td>
         <td class="tdmarginddl" colspan="2" style="font-size: 10pt; position: static; height: 23px;
             background-color: #99cc99">
             <asp:DropDownList ID="ddl_order" runat="server" Width="150px">
             </asp:DropDownList>
         </td>
         <td align="left" 
             style="font-size: 10pt; position: static; height: 23px; background-color: #99cc99" 
             colspan="2">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cccccc; font-size: 10pt; position: static; width: 262px;">
             <asp:Label ID="lblmonth" runat="server" Text="Allotment Month" Width="109px"></asp:Label></td>
         <td align="left" style="background-color: #cccccc; font-size: 10pt; position: static; width: 262px;">
             <asp:DropDownList ID="ddlalotmm" runat="server" OnSelectedIndexChanged="ddlalotmm_SelectedIndexChanged"
                 Width="153px">
                 <asp:ListItem Value="1">January</asp:ListItem>
                 <asp:ListItem Value="2">February</asp:ListItem>
                 <asp:ListItem Value="3">March</asp:ListItem>
                 <asp:ListItem Value="4">April</asp:ListItem>
                 <asp:ListItem Value="5">May</asp:ListItem>
                 <asp:ListItem Value="6">June</asp:ListItem>
                 <asp:ListItem Value="7">July</asp:ListItem>
                 <asp:ListItem Value="8">August</asp:ListItem>
                 <asp:ListItem Value="9">September</asp:ListItem>
                 <asp:ListItem Value="10">October</asp:ListItem>
                 <asp:ListItem Value="11">November</asp:ListItem>
                 <asp:ListItem Value="12">December</asp:ListItem>
             </asp:DropDownList></td>
         <td class="tdmarginddl" style="background-color: #cccccc; font-size: 10pt; position: static; width: 262px;">
             <asp:Label ID="lblyear" runat="server" Text="Allotment Year" Width="103px"></asp:Label></td>
         <td align="left" 
             style="background-color: #cccccc; font-size: 10pt; position: static; width: 262px;" 
             colspan="2">
             <asp:DropDownList ID="ddlallot_year" runat="server" OnSelectedIndexChanged="ddlallot_year_SelectedIndexChanged"
                 Width="153px">
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cccccc; font-size: 10pt; position: static; width: 97px;">
             <asp:Label ID="lblchallanno" runat="server" Text="Enter Challan No." Visible="False" Width="125px"></asp:Label></td>
         <td align="left" style="background-color: #cccccc; font-size: 10pt; position: static; width: 97px;">
     <asp:TextBox ID="txtchallan" runat="server" Width="146px" Visible="False" ></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtchallan"
         ErrorMessage="Challan No Requierd " ValidationGroup="1">*</asp:RequiredFieldValidator></td>
         <td class="tdmarginddl" style="background-color: #cccccc; font-size: 10pt; position: static; width: 97px;">
             <asp:Label ID="lblchallandate" runat="server" Text="EnterChallan Date" Visible="False" Width="123px"></asp:Label></td>
         <td align="left" 
             style="background-color: #cccccc; font-size: 10pt; position: static; width: 97px;" 
             colspan="2">
             <asp:TextBox ID="DaintyDate3" runat="server" Width="119px" Visible="False"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate3'
	    });
	     </script>
             </td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         <asp:Label ID="lblCommodity" runat="server" Text="Commodity"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; width: 262px;">
      <asp:DropDownList ID="ddlcomdty" runat="server"  Width="147px"  OnSelectedIndexChanged="ddlcomdty_SelectedIndexChanged" AutoPostBack="True">
       <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
      </asp:DropDownList>
      <asp:Label ID="lblcomdty" runat="server" Visible="False"></asp:Label></td>
         <td class="tdmarginddl" style="background-color: #cfdcdc; font-size: 10pt; position: static; width: 97px;">
      <asp:Label ID="lblScheme" runat="server" Text="Scheme"></asp:Label></td>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static;" 
             colspan="2">
     <asp:DropDownList ID="ddlscheme" runat="server"  Width="176px" OnSelectedIndexChanged="ddlscheme_SelectedIndexChanged" AutoPostBack="True">
     
     </asp:DropDownList>
             <asp:Label ID="lblsch" runat="server" Visible="False" Width="2px"></asp:Label>&nbsp;
         </td>
     </tr>
 <tr>
     <td class ="tdmarginddl" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
          <asp:Label ID="lblBagNumber" runat="server" Text="No. Of Bags"></asp:Label></td>
  <td style="background-color: #cfdcdc; font-size: 10pt; position: static; width: 262px;" align="left"> &nbsp;<asp:TextBox ID="txtnobags" runat="server" Width="146px" ></asp:TextBox>
 </td>
  <td class ="tdmarginddl" style="background-color: #cfdcdc; font-size: 10pt; position: static; width: 97px;">
      <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label></td>
 <td align="left" 
         style="background-color: #cfdcdc; font-size: 10pt; position: static;" 
         colspan="2">
     &nbsp;<asp:TextBox ID="txtqty" runat="server" Width="143px" ></asp:TextBox>
      <asp:Label ID="lblKgs" runat="server" Text="Qtls." Font-Size="8pt" Width="16px"></asp:Label></td>
  
                                       
  </tr>
 <tr>
  <td class ="tdmarginro" style="background-color: #ffccff; font-size: 10pt; position: static; width: 97px;" >
      <asp:Label ID="Label5" runat="server" Text="Category" Visible="False"></asp:Label></td>
  <td style="background-color: #ffccff; font-size: 10pt; position: static; width: 97px;" align="left">
      <asp:DropDownList ID="ddlcategory" runat="server" Width="154px"  OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged" Visible="False">
      <asp:ListItem Value ="0"> --Select--</asp:ListItem>
      </asp:DropDownList></td>
      
      <td class="tdmarginro" style="background-color: #ffccff; font-size: 10pt; position: static; width: 97px;">
          <asp:Label ID="Label6" runat="server" Text="Crop Year" Visible="False"></asp:Label></td>
  <td align="left" 
         style="background-color: #ffccff; font-size: 10pt; position: static; width: 97px;" 
         colspan="2" >
                                            <asp:DropDownList ID="ddlcropyear" runat="server"  Width="157px" Visible="False" >
                                                <asp:ListItem Value="01" Selected="True">-Select-</asp:ListItem>
                                                <asp:ListItem Value="11">2015-2016</asp:ListItem>
                                                <asp:ListItem Value="02">2014-2015</asp:ListItem>
                                                <asp:ListItem Value="03">2013-2014</asp:ListItem>
                                                <asp:ListItem Value="04">2012-2013</asp:ListItem>
                                                <asp:ListItem Value="05">2011-2012</asp:ListItem>
                                                <asp:ListItem Value="06">2010-2011</asp:ListItem>
                                                <asp:ListItem Value="07">2009-2010</asp:ListItem>
                                                <asp:ListItem Value="08">2008-2009</asp:ListItem>
                                                <asp:ListItem Value="09">2007-2008</asp:ListItem>
                                                <asp:ListItem Value="10">2006-2007</asp:ListItem>
                                                
                                            </asp:DropDownList>
                                        </td>
      
 </tr>
  <tr>
  <td class ="tdmarginro" style="background-color: #ffcccc; font-size: 10pt; position: static; width: 97px;" >
      <asp:Label ID="lblTrans" runat="server" Text="Transporter/Lead"></asp:Label></td>
 <td style="background-color: #ffcccc; font-size: 10pt; position: static; width: 97px;" align="left">
     <asp:DropDownList ID="ddltransport" runat="server" Width="153px" AutoPostBack="True" OnSelectedIndexChanged="ddltransport_SelectedIndexChanged" Font-Size="11px">
       <asp:ListItem Value ="0"> --Select--</asp:ListItem>
     </asp:DropDownList>
     <asp:Label ID="lbltid" runat="server" Visible="False"></asp:Label></td>
     <td class ="tdmarginro" style="background-color: #ffcccc; font-size: 10pt; position: static; width: 97px;">
         <asp:Label ID="lblTruckNumber" runat="server" Text="Vehicle No."></asp:Label></td>
  <td align="left" 
          style="background-color: #ffcccc; font-size: 10pt; position: static; width: 97px;" 
          colspan="2"> <asp:TextBox ID="txtvehleno" runat="server" Width="146px" ></asp:TextBox>
 </td>

  </tr>
  <tr>
    <td class="tdtime" style="background-color: #ffcccc; font-size: 10pt; position: static;">
        <asp:Label ID="Label4" runat="server" Text="Gunny Type" Visible="False" Width="88px"></asp:Label></td>
  
    <td colspan ="2" align="left" style="background-color: #ffcccc; font-size: 10pt; position: static;">
                                            <asp:DropDownList ID="ddlgtype" runat="server" Width="157px" Visible="False" OnSelectedIndexChanged="ddlgtype_SelectedIndexChanged1" >
                                                <asp:ListItem Value="01" Selected="True">-Select-</asp:ListItem>
                                            </asp:DropDownList></td> 
    <td style="background-color: #ffcccc; font-size: 10pt; position: static;" 
          colspan="2"> </td>
  </tr>
     <tr>
         <td class="tdmarginro" style="background-color: lightslategray">
         </td>
         <td align="center" colspan="2" style="background-color: lightslategray">
             <asp:Label ID="lblRecepDetail" runat="server" Text="Receipt Details" Width="155px" Font-Bold="True" Font-Italic="True" ForeColor="White"></asp:Label></td>
         <td align="left" style="background-color: lightslategray" colspan="2">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblArrivalTime" runat="server" Text="Arrival Time"></asp:Label></td>
         <td  colspan="2" align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
    <asp:DropDownList ID="ddlhour" runat="server">
                            <asp:ListItem Value="01">01</asp:ListItem>
                            <asp:ListItem Value="02">02</asp:ListItem>
                            <asp:ListItem Value="03">03</asp:ListItem>
                            <asp:ListItem Value="04">04</asp:ListItem>
                            <asp:ListItem Value="05">05</asp:ListItem>
                            <asp:ListItem Value="06">06</asp:ListItem>
                            <asp:ListItem Value="07">07</asp:ListItem>
                            <asp:ListItem Value="08">08</asp:ListItem>
                            <asp:ListItem Value="09">09</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="11">11</asp:ListItem>
                            <asp:ListItem Value="12">12</asp:ListItem>
                        </asp:DropDownList>
                        :

  
 
 
 <asp:DropDownList ID="ddlminute" runat="server">
                            <asp:ListItem Value="00">00</asp:ListItem>
                            <asp:ListItem Value="01">01</asp:ListItem>
                            <asp:ListItem Value="02">02</asp:ListItem>
                            <asp:ListItem Value="03">03</asp:ListItem>
                            <asp:ListItem Value="04">04</asp:ListItem>
                            <asp:ListItem Value="05">05</asp:ListItem>
                            <asp:ListItem Value="06">06</asp:ListItem>
                            <asp:ListItem Value="07">07</asp:ListItem>
                            <asp:ListItem Value="08">08</asp:ListItem>
                            <asp:ListItem Value="09">09</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="11">11</asp:ListItem>
                            <asp:ListItem Value="12">12</asp:ListItem>
                            <asp:ListItem Value="13">13</asp:ListItem>
                            <asp:ListItem Value="14">14</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="16">16</asp:ListItem>
                            <asp:ListItem Value="17">17</asp:ListItem>
                            <asp:ListItem Value="18">18</asp:ListItem>
                            <asp:ListItem Value="19">19</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="21">21</asp:ListItem>
                            <asp:ListItem Value="22">22</asp:ListItem>
                            <asp:ListItem Value="23">23</asp:ListItem>
                            <asp:ListItem Value="24">24</asp:ListItem>
                             <asp:ListItem Value="25">25</asp:ListItem>
                            <asp:ListItem Value="26">26</asp:ListItem>
                            <asp:ListItem Value="27">27</asp:ListItem>
                            <asp:ListItem Value="28">28</asp:ListItem>
                            <asp:ListItem Value="29">29</asp:ListItem>
                            <asp:ListItem Value="30">30</asp:ListItem>
                            <asp:ListItem Value="31">31</asp:ListItem>
                            <asp:ListItem Value="32">32</asp:ListItem>
                            <asp:ListItem Value="33">33</asp:ListItem>
                            <asp:ListItem Value="34">34</asp:ListItem>
                            <asp:ListItem Value="35">35</asp:ListItem>
                            <asp:ListItem Value="36">36</asp:ListItem>
                            <asp:ListItem Value="37">37</asp:ListItem>
                            <asp:ListItem Value="38">38</asp:ListItem>
                            <asp:ListItem Value="39">39</asp:ListItem>
                            <asp:ListItem Value="40">40</asp:ListItem>
                            <asp:ListItem Value="41">41</asp:ListItem>
                            <asp:ListItem Value="42">42</asp:ListItem>
                            <asp:ListItem Value="43">43</asp:ListItem>
                            <asp:ListItem Value="44">44</asp:ListItem>
                            <asp:ListItem Value="45">45</asp:ListItem>
                            <asp:ListItem Value="46">46</asp:ListItem>
                            <asp:ListItem Value="47">47</asp:ListItem>
                            <asp:ListItem Value="48">48</asp:ListItem>
                            <asp:ListItem Value="79">49</asp:ListItem>
                            <asp:ListItem Value="50">50</asp:ListItem>
                            <asp:ListItem Value="51">51</asp:ListItem>
                            <asp:ListItem Value="52">52</asp:ListItem>
                            <asp:ListItem Value="53">53</asp:ListItem>
                            <asp:ListItem Value="54">54</asp:ListItem>
                            <asp:ListItem Value="55">55</asp:ListItem>
                            <asp:ListItem Value="56">56</asp:ListItem>
                            <asp:ListItem Value="57">57</asp:ListItem>
                            <asp:ListItem Value="58">58</asp:ListItem>
                            <asp:ListItem Value="59">59</asp:ListItem>
                          
                        </asp:DropDownList>

:
 
    <asp:DropDownList ID="ddlampm" runat="server">
    <asp:ListItem Value="01">AM</asp:ListItem>
    <asp:ListItem Value="02">PM</asp:ListItem>
    </asp:DropDownList>
             <asp:Label ID="lblReceiptDate" runat="server" Text="Arrival Date"></asp:Label></td>
         <td align="left" 
             style="background-color: #cfdcc8; font-size: 10pt; position: static;" 
             colspan="2">
             <asp:TextBox ID="DaintyDate1" runat="server" Width="118px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate1'
	    });
	     </script>
             
             
             </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; height: 43px;">
             <asp:Label ID="lbltotalReceivedBags" runat="server" Text="Recd. Bags"></asp:Label></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static; height: 43px; width: 262px;">
      <asp:TextBox ID="txtrecbags" runat="server" Width="137px" MaxLength="5"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtrecbags"
          ErrorMessage="No of Bags Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; height: 43px; width: 97px;">
             <asp:Label ID="lblTotalQuantityReceived" runat="server" Text="Recd. Qty."></asp:Label></td>
         <td align="left" 
             style="background-color: #cfdcc8; font-size: 10pt; position: static; height: 43px;" 
             colspan="2">
      <asp:TextBox ID="txtrqty" runat="server" Width="118px" OnTextChanged="txtrqty_TextChanged" MaxLength="13"></asp:TextBox>Qtls<asp:RequiredFieldValidator
          ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtrqty" ErrorMessage="Recd Qty.  Required"
          ValidationGroup="1">*</asp:RequiredFieldValidator></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; height: 27px;">
             <asp:Label ID="lblGodownNo0" runat="server" Text="Branch Name"></asp:Label></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static; height: 27px; width: 262px;">
                            <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" Width="173px">
                            </asp:DropDownList>
                        </td>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; height: 27px; width: 97px;">
             <asp:Label ID="lblGodownNo" runat="server" Text="Godown No."></asp:Label></td>
         <td align="left" 
             style="background-color: #cfdcc8; font-size: 10pt; position: static; height: 27px;" 
             colspan="2">
             <asp:DropDownList ID="ddlgodown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged"
                 Width="143px">
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; height: 27px;">
             <asp:Label ID="lblMaxCap" runat="server" Text="MaxCapacity"></asp:Label></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static; height: 27px; width: 262px;">
             <asp:TextBox ID="txtmaxcap" runat="server" Font-Bold="True" Font-Italic="False" ForeColor="#0000C0"
                 ReadOnly="True" Width="118px"></asp:TextBox></td>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; height: 27px; width: 97px;">
             &nbsp;</td>
         <td align="left" 
             style="background-color: #cfdcc8; font-size: 10pt; position: static; height: 27px;" 
             colspan="2">
             &nbsp;</td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblCurStackCap" runat="server" Text="Current Cap."></asp:Label></td>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 262px;">
             <asp:TextBox ID="txtcurntcap" runat="server" Font-Bold="True" Font-Italic="False"
                 ForeColor="#0000C0" ReadOnly="True" Width="133px"></asp:TextBox></td>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 97px;">
             <asp:Label ID="lblAvailable" runat="server" Text="Available"></asp:Label></td>
         <td align="left" 
             style="background-color: #cfdcc8; font-size: 10pt; position: static;" 
             colspan="2">
             <asp:TextBox ID="txtavalcap" runat="server" Font-Bold="True" Font-Italic="False"
                 ForeColor="#0000C0" ReadOnly="True" Width="118px"></asp:TextBox></td>
     </tr>
   <tr>
    <td class ="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
        </td>
  <td style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 262px;" align="left">
      &nbsp;
             <asp:Label ID="lblbalanceqty" runat="server" Text="Balance Qty Of Stock" Width="161px" Visible="False" BackColor="#FFC0C0" Font-Bold="False" ForeColor="Navy" Font-Italic="True"></asp:Label></td>
    <td class ="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 97px;">
             <asp:TextBox ID="txtbalqty" runat="server" Width="128px" Visible="False"></asp:TextBox></td>
  <td align="left" 
           style="background-color: #cfdcc8; font-size: 10pt; position: static;" 
           colspan="2">
      </td>
       </tr>
       
       <tr>
      <td class ="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;"  >
          <asp:Label ID="Label2" runat="server" Text="WCM No." Visible="False"></asp:Label></td>
      <td colspan ="1" style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 262px;" align="left">
      <asp:TextBox ID="txtwcmno" runat="server" Width="146px" MaxLength="7" Visible="False" ></asp:TextBox></td>
   <td class ="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 97px;">
       <asp:Label ID="Label3" runat="server" Text="Moisture %" Visible="False" Width="121px"></asp:Label></td>
  <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;" 
               colspan="2">
      <asp:TextBox ID="txtmoisture" runat="server" Width="118px" Visible="False" ></asp:TextBox></td>
      
       </tr>
       <tr>
       <td style="background-color: #cfdcc8; font-size: 10pt; position: static;"> </td>
       <td style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 262px;"> </td>
       <td style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 97px;"> </td>
       <td style="background-color: #cfdcc8; font-size: 10pt; position: static;" 
               colspan="2"> </td>
       </tr>
       <tr>
       <td style="background-color: #cfdcc8; font-size: 10pt; position: static;" > 
           <asp:Button ID="btnnew" runat="server" OnClick="btnnew_Click" Text="New" Width="114px" Font-Bold="True" Font-Italic="True" ForeColor="#004040" /></td>
       <td colspan ="4" align="left" 
               style="background-color: #cfdcc8; font-size: 10pt; position: static;"><asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click" ValidationGroup="1" Width="141px" Font-Bold="True" Font-Italic="True" ForeColor="#004040" /><asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click1" Width="110px" Font-Bold="True" Font-Italic="True" ForeColor="#004040" /> 
           <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="#" Visible="False" Width="211px">Print Receipt Acknowledgement</asp:HyperLink></td>
       </tr>
       <tr>
       <td align="left" colspan="5" rowspan="2" 
               style="background-color: #cfdcc8; font-size: 10pt; position: static;"> 
           <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
               ValidationGroup="1" ShowSummary="False" Width="194px" />
           <asp:Label ID="Label1" runat="server" Text="Label" Visible="False" Width="76px"></asp:Label></td>
       </tr>
 </table>
</center>
</div>
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
            if (count > 5 && AsciiCode != 8)  
            {
                alert("Only 5 decimal digits allowed.");
                return false;
            }
        }
    }
    </script>
</asp:Content>

