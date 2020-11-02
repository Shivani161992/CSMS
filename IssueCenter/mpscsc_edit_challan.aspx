<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="mpscsc_edit_challan.aspx.cs" Inherits="mpscsc_edit_challan" Title="Edit Challan Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<%--
        <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
		<meta content="C#" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />--%>

<div id="hedl">
    &nbsp;</div>
<div id ="ronewmargin">
<center>
 <table cellpadding ="0" cellspacing ="0" border ="0" class ="tablelayout">
     <tr>
         <td align="center"  colspan="4" style="background-color: #cccccc; height: 12px;">
    <asp:Label ID="lblreceipt" runat="server" Text="Edit Receipt Details " Width="155px" Font-Bold="True" Font-Size="14px"></asp:Label></td>
     </tr>
     <tr>
         <td class="tdmarginro" colspan="4" style="background-color: lightslategray; color: white;">
             <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: lightslategray; color: white;">
         </td>
         <td align="center" colspan="2" style="background-color: lightslategray; color: white;">
             <asp:Label ID="lbldispdetails" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="13px"
                 ForeColor="White" Text="Sent Details" Width="146px"></asp:Label></td>
         <td style="background-color: lightslategray; color: white;">
         </td>
     </tr>
 <tr>
  <td class ="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
      <asp:Label ID="lblChallanNumber" runat="server" Text="Challan No." Width="90px"></asp:Label></td>
 <td style="background-color: #cfdcc8; font-size: 10pt; position: static;">
     <asp:TextBox ID="txtchallan" runat="server" Width="146px" ></asp:TextBox>
 </td>
 <td class ="tdmarginddl" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
     <asp:Label ID="lblDateOfChallan" runat="server" Text="Challan Date"></asp:Label></td>
  <td style="background-color: #cfdcc8; font-size: 10pt; position: static;">
  <asp:TextBox ID="DaintyDate3" runat="server" Width="119px"></asp:TextBox>
  <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate3'
	    });
	     </script>
  
  </td>
 </tr>
 
 <tr>
 <td class ="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;" >
     <asp:Label ID="lblSorcePfArrival" runat="server" Text="Source of Arrival" Width="119px"></asp:Label></td>
 <td style="background-color: #cfdcc8; font-size: 10pt; position: static;">
    <asp:DropDownList ID="ddlsarrival" runat="server" Width="153px"  AutoPostBack="True" OnSelectedIndexChanged="ddlsarrival_SelectedIndexChanged" >
     
     </asp:DropDownList>
 </td>
     <td class ="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
         <asp:Label ID="lblMillersName" runat="server" Text="Miller Name" Visible="False"></asp:Label>
         &nbsp;
     </td>
  <td style="background-color: #cfdcc8; font-size: 10pt; position: static;"> 
                                            &nbsp;<asp:DropDownList ID="ddlmiller" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlmiller_SelectedIndexChanged"
          Visible="False" Width="153px">
      </asp:DropDownList>
      </td>
 </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblReleaseOrder" runat="server" Text="R.O. No" Visible="False"></asp:Label></td>
         <td style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:TextBox ID="txtrono" runat="server" Visible="False" Width="143px"></asp:TextBox></td>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lbltono" runat="server" Text="T.O No." Visible="False"></asp:Label></td>
         <td style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:TextBox ID="txttono" runat="server" Visible="False" Width="151px"></asp:TextBox></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblparty" runat="server" Text="Party Name" Visible="False"></asp:Label></td>
         <td colspan="2" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:TextBox ID="txtparty" runat="server" OnTextChanged="txtparty_TextChanged" Visible="False"
                 Width="247px"></asp:TextBox>
             &nbsp; &nbsp; &nbsp;&nbsp;
         </td>
         <td style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             &nbsp; &nbsp;
         </td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblRecFromDist" runat="server" Text="Sending District" Visible="False"></asp:Label></td>
         <td style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:DropDownList ID="ddldistrict" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged"
                 Visible="False" Width="153px">
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
         <td class="tdmarginddl" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblNameDepot" runat="server" Text="Sending Depot" Visible="False"></asp:Label></td>
         <td style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:DropDownList ID="ddlissue" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlissue_SelectedIndexChanged"
                 Visible="False" Width="153px">
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblmonth" runat="server" Text="Allotment Month" Width="109px"></asp:Label></td>
         <td style="background-color: #cfdcc8; font-size: 10pt; position: static;">
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
         <td class="tdmarginddl" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblyear" runat="server" Text="Allotment Year"></asp:Label></td>
         <td style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:DropDownList ID="ddlallot_year" runat="server" OnSelectedIndexChanged="ddlallot_year_SelectedIndexChanged"
                 Width="153px">
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblBagNumber" runat="server" Text="No of Bags"></asp:Label></td>
         <td style="background-color: #cfdcc8; font-size: 10pt; position: static;">
                                            <asp:TextBox ID="txtnobags" runat="server" Width="146px" ></asp:TextBox>
             </td>
         <td class="tdmarginddl" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label></td>
         <td style="background-color: #cfdcc8; font-size: 10pt; position: static;">
      <asp:TextBox ID="txtqty" runat="server" Width="146px" ></asp:TextBox>
             </td>
     </tr>
 <tr>
     <td class ="tdmarginddl" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
         <asp:Label ID="lblCommodity" runat="server" Text="Commodity"></asp:Label></td>
  <td style="background-color: #cfdcc8; font-size: 10pt; position: static;"> 
      <asp:DropDownList ID="ddlcomdty" runat="server"  Width="153px" AutoPostBack="True" OnSelectedIndexChanged="ddlcomdty_SelectedIndexChanged"  >
       <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
      </asp:DropDownList>
 </td>
  <td class ="tdmarginddl" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
      <asp:Label ID="lblScheme" runat="server" Text="Scheme"></asp:Label>
      &nbsp;&nbsp;
  </td>
 <td style="background-color: #cfdcc8; font-size: 10pt; position: static;">
     <asp:DropDownList ID="ddlscheme" runat="server"  Width="153px" AutoPostBack="True" OnSelectedIndexChanged="ddlscheme_SelectedIndexChanged" >
      <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
     </asp:DropDownList>
     &nbsp;
 </td>
 <td style="background-color: #cfdcc8; font-size: 10pt; position: static;"></td>
  
                                       
  </tr>
  <tr>
 <td style="background-color: #cfdcc8; font-size: 10pt; position: static;" >  </td>
 <td style="background-color: #cfdcc8; font-size: 10pt; position: static;">  </td>
 <td style="background-color: #cfdcc8; font-size: 10pt; position: static;" >  &nbsp;
 </td>
     <td style="background-color: #cfdcc8; font-size: 10pt; position: static;"> &nbsp;
         &nbsp;&nbsp;
     </td>
 </tr>
 <tr>
  <td class ="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;" >
      <asp:Label ID="Label4" runat="server" Text="Gunny Type" Visible="False"></asp:Label></td>
  <td style="background-color: #cfdcc8; font-size: 10pt; position: static;">
                                            <asp:DropDownList ID="ddlgtype" runat="server" Width="153px" OnSelectedIndexChanged="ddlgtype_SelectedIndexChanged" Visible="False" >
                                                <asp:ListItem Value="01" >-Select-</asp:ListItem>
                                            </asp:DropDownList></td>
      
      <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
          <asp:Label ID="Label5" runat="server" Text="Crop Year" Visible="False"></asp:Label></td>
  <td style="background-color: #cfdcc8; font-size: 10pt; position: static;" >
                                            <asp:DropDownList ID="ddlcropyear" runat="server"  Width="153px" Visible="False" >
                                                <asp:ListItem Value="01" Selected="True">Crop year not indicated</asp:ListItem>
                                                <asp:ListItem Value="02">2000-2001</asp:ListItem>
                                                <asp:ListItem Value="03">2001-2002</asp:ListItem>
                                                <asp:ListItem Value="04">2002-2003</asp:ListItem>
                                                <asp:ListItem Value="05">2003-2004</asp:ListItem>
                                                <asp:ListItem Value="06">20004-2005</asp:ListItem>
                                                <asp:ListItem Value="07">20005-2006</asp:ListItem>
                                                <asp:ListItem Value="08">20006-2007</asp:ListItem>
                                                <asp:ListItem Value="09">20007-2008</asp:ListItem>
                                                <asp:ListItem Value="10">20008-2009</asp:ListItem>
                                                <asp:ListItem Value="11">20009-2010</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
      
 </tr>
 <tr>
 <td style="background-color: #cfdcc8; font-size: 10pt; position: static;"></td>
 <td style="background-color: #cfdcc8; font-size: 10pt; position: static;"></td>
 <td style="background-color: #cfdcc8; font-size: 10pt; position: static;"></td>
 <td style="background-color: #cfdcc8; font-size: 10pt; position: static;"></td>
 <td style="background-color: #cfdcc8; font-size: 10pt; position: static;"></td>
 </tr>
  <tr>
  <td class ="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;" >
      <asp:Label ID="lblTrans" runat="server" Text="Transporter/Lead"></asp:Label></td>
 <td style="background-color: #cfdcc8; font-size: 10pt; position: static;">
     <asp:DropDownList ID="ddltransport" runat="server" Width="153px" Font-Size="11px">
       <asp:ListItem Value ="0"> --Select--</asp:ListItem>
     </asp:DropDownList>
 </td>
     <td class ="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
         <asp:Label ID="lblTruckNumber" runat="server" Text="Vehile No."></asp:Label></td>
  <td style="background-color: #cfdcc8; font-size: 10pt; position: static;"> <asp:TextBox ID="txtvehleno" runat="server" Width="146px" ></asp:TextBox>
 </td>

  </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
                                            </td>
         <td style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             </td>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             </td>
         <td style="background-color: #cfdcc8; font-size: 10pt; position: static;">
      <asp:DropDownList ID="ddlcategory" runat="server" Width="154px" AutoPostBack="True" Visible="False"  >
      <asp:ListItem Value ="0"> --Select--</asp:ListItem>
      </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
         </td>
         <td style="background-color: #cfdcc8; font-size: 10pt; position: static;">
         </td>
         <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
         </td>
         <td style="background-color: #cfdcc8; font-size: 10pt; position: static;">
         </td>
     </tr>
     <tr>
         <td style="background-color: lightslategray; height: 16px;" >
         </td>
         <td colspan="2" align="center" style="background-color: lightslategray; height: 16px;">
             &nbsp;<asp:Label ID="lblRecepDetail" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="13px"
                 ForeColor="White" Text="Receipt Details" Width="146px"></asp:Label></td>
         <td style="background-color: lightslategray; height: 16px;">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblArrivalTime" runat="server" Text="Arrival Time"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
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
         </td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblReceiptDate" runat="server" Text="Arrival Date"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
          <asp:TextBox ID="DaintyDate1" runat="server" Width="119px"></asp:TextBox>
  <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate1'
	    });
	     </script>
         </td>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         </td>
     </tr>
   <tr>
    <td class ="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
        <asp:Label ID="lbltotalReceivedBags" runat="server" Text="No Of Bags"></asp:Label></td>
  <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
      <asp:TextBox ID="txtrecbags" runat="server" Width="146px"></asp:TextBox></td>
    <td class ="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
        <asp:Label ID="lblTotalQuantityReceived" runat="server" Text="Recd QTY"></asp:Label></td>
  <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
      <asp:TextBox ID="txtrqty" runat="server" Width="146px"  AutoPostBack="false"  ></asp:TextBox></td>
      <td style="background-color: #cfdcdc; font-size: 10pt; position: static;"></td>
       </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblGodownNo" runat="server" Text="Godown No."></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:DropDownList ID="ddlgodown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged"
                 Width="153px" BackColor="ActiveCaption">
             </asp:DropDownList></td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblMaxCap" runat="server" Text="MaxCapacity"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:TextBox ID="txtmaxcap" runat="server" Font-Bold="True" Font-Italic="False" ForeColor="#0000C0"
                 ReadOnly="True" Width="144px" BackColor="ActiveCaption"></asp:TextBox></td>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblCurStackCap" runat="server" Text="Current Cap."></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:TextBox ID="txtcurntcap" runat="server" Font-Bold="True" Font-Italic="False"
                 ForeColor="#0000C0" ReadOnly="True" Width="145px" BackColor="ActiveCaption"></asp:TextBox></td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblAvailable" runat="server" Text="Available"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:TextBox ID="txtavalcap" runat="server" Font-Bold="True" Font-Italic="False"
                 ForeColor="#0000C0" ReadOnly="True" Width="143px" BackColor="ActiveCaption"></asp:TextBox></td>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
     <asp:Label ID="lblbalanceqty" runat="server" BackColor="White" Font-Bold="False"
         ForeColor="Navy" Text="Balance Qty Of Stock" Visible="False" Width="144px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
     <asp:TextBox ID="txtbalqty" runat="server" Visible="False" Width="146px" OnTextChanged="txtbalqty_TextChanged" BackColor="ActiveCaption"></asp:TextBox></td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="Label2" runat="server" ForeColor="Navy" Text="Current Bags"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:TextBox ID="txtcurbags" runat="server" Width="143px" BackColor="ActiveCaption"></asp:TextBox></td>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         </td>
     </tr>
       
       <tr>
      <td class ="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;"  >
          <asp:Label ID="Label6" runat="server" Text="WCM No." Visible="False"></asp:Label></td>
      <td colspan ="1" align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
      <asp:TextBox ID="txtwcmno" runat="server" Width="146px" Visible="False" ></asp:TextBox></td>
   <td class ="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
       <asp:Label ID="Label7" runat="server" Text="Moisture %" Visible="False"></asp:Label></td>
  <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
      <asp:TextBox ID="txtmoisture" runat="server" Width="146px" Visible="False" ></asp:TextBox></td>
      
       </tr>
       <tr>
       <td style="background-color: #cfdcdc; font-size: 10pt; position: static;"> 
           <asp:Label ID="lblgid" runat="server" Visible="False"></asp:Label></td>
       <td style="background-color: #cfdcdc; font-size: 10pt; position: static;"> </td>
       <td style="background-color: #cfdcdc; font-size: 10pt; position: static;"> 
           <asp:Label ID="lbl_ssch" runat="server" Width="73px" Visible="False"></asp:Label></td>
       <td style="background-color: #cfdcdc; font-size: 10pt; position: static;"> 
           <asp:Label ID="lbl_scomdty" runat="server" Visible="False" Width="136px"></asp:Label></td>
       <td style="background-color: #cfdcdc; font-size: 10pt; position: static;"> </td>
       </tr>
       <tr>
       <td style="background-color: #cfdcdc; font-size: 10pt; position: static;" > 
           <asp:Label ID="lblGodown" runat="server" Visible="False"></asp:Label></td>
       <td colspan ="3" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
           <asp:Button ID="btnsave" runat="server" Text="Update" OnClick="btnsave_Click" Width="151px" />
              
       <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" Width="154px" /> </td>
       <td style="background-color: #cfdcdc; font-size: 10pt; position: static;"> </td>
       <td style="background-color: #cfdcdc; font-size: 10pt; position: static;"> </td>
       </tr>
       <tr>
       <td style="background-color: #cfdcdc; font-size: 10pt; position: static;"> 
             <asp:Label ID="lblrecbag" runat="server" Visible="False"></asp:Label>
             <asp:Label ID="lblqty" runat="server" Visible="False"></asp:Label></td>
       <td style="background-color: #cfdcdc; font-size: 10pt; position: static;"> 
     <asp:Label ID="lblcomdty" runat="server" Visible="False"></asp:Label>
         <asp:Label ID="lblsc" runat="server" Width="73px" Visible="False"></asp:Label></td>
       <td style="background-color: #cfdcdc; font-size: 10pt; position: static;"> 
             <asp:Label ID="dcode" runat="server" Visible="False"></asp:Label></td>
       <td style="background-color: #cfdcdc; font-size: 10pt; position: static;"> 
             <asp:Label ID="icode" runat="server" Visible="False"></asp:Label>
      <asp:Label ID="lblmcode" runat="server" Visible="False"></asp:Label></td>
       <td style="background-color: #cfdcdc; font-size: 10pt; position: static;"> </td>
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

