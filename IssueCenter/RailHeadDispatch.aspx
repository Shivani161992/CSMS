<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="RailHeadDispatch.aspx.cs" Inherits="RailHeadDispatch" Title="Truck Challan (Dispatch  To Rail Head)" %>
 
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="hedl"> &nbsp;</div>
<div id ="ronewmargin">
<center>

<table cellpadding ="0" cellspacing ="0" class ="tablelayout" style="width: 587px">

<tr>
 <td style="width: 830px">
 <table border ="0" cellpadding ="0" cellspacing ="0">
     <tr>
         <td align="center"  colspan="4" style="border-right: lightblue 1px solid;
             border-top: lightblue 1px solid; border-left: lightblue 1px solid; border-bottom: lightblue 1px solid;
             background-color: lightslategray">
    <asp:Label ID="lbltransrailhead" runat="server" Text="Detail Of Truck Challan (Dispatch To Rail Head)" Font-Bold="True" ForeColor="White" Width="357px" Font-Size="13px"></asp:Label></td>
     </tr>
     <tr>
         <td  colspan="4" style="border-right: lightblue 1px solid; border-top: lightblue 1px solid;
             border-left: lightblue 1px solid; border-bottom: lightblue 1px solid; background-color: #cccccc" align="center">
    <asp:Label ID="lbltransferdepot" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#004000"
        Text="To be entered By Sending Issue Center" Width="300px"></asp:Label></td>
     </tr>
 <tr>
 <td class ="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;"></td>
 <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
     <asp:TextBox ID="txtrono" runat="server" Width ="148px" Visible="False"></asp:TextBox>
 </td>
     <td class ="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
         </td>
  <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;"> <asp:TextBox ID="txtroqty" runat="server"  Width ="130px" Visible="False"></asp:TextBox></td>
 
 </tr>
     <tr>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
             <asp:Label ID="lblDistrictName" runat="server" Text="Sending District"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
             <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Navy"
                 Width="157px"></asp:Label></td>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
             <asp:Label ID="lblNameDepot" runat="server" Text="Sending Depot"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
             <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Navy"
                 Width="128px"></asp:Label></td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
             <asp:Label ID="Label5" runat="server" Text="Stock Issued From" Width="136px"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
             <asp:DropDownList ID="ddlsarrival" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlsarrival_SelectedIndexChanged"
                 Width="153px">
             </asp:DropDownList></td>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
             &nbsp;
         </td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
             &nbsp;
         </td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
             <asp:Label ID="lbltono" runat="server" Text="Transport Order No." Width="138px" Visible="False"></asp:Label>&nbsp;</td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
             <asp:DropDownList ID="ddltono" runat="server" OnSelectedIndexChanged="ddltono_SelectedIndexChanged"
                 Width="153px" AutoPostBack="True" Visible="False">
             </asp:DropDownList>&nbsp;
         </td>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
             &nbsp;&nbsp;&nbsp;&nbsp;
         </td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
             &nbsp;&nbsp;&nbsp;&nbsp;
         </td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
             <asp:Label ID="lbldisdate" runat="server" Text="Dispatch Date"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
      
    <asp:TextBox ID="DaintyDate1" runat="server" Width="119px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate1'
	    });
	     </script>
         </td>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
             <asp:Label ID="lblGodownNo" runat="server" Text="Dispatch Godown" Width="122px"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
     <asp:DropDownList ID="ddlgodown" runat="server" Width="153px">
     <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
     </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
             <asp:Label ID="lblCommodity" runat="server" Text="Commodity"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
      <asp:DropDownList ID="ddlcomdty" runat="server"  Width ="153px"  AutoPostBack ="false" >
       <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
      </asp:DropDownList></td>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
             <asp:Label ID="lblScheme" runat="server" Text="Scheme"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
     <asp:DropDownList ID="ddlscheme" runat="server"  Width ="153px" AutoPostBack="True" OnSelectedIndexChanged="ddlscheme_SelectedIndexChanged">
      <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
     </asp:DropDownList></td>
         
     </tr>
     <tr>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
         </td>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
             <asp:Label ID="lblbqty" runat="server" Text="Balance Quantity " Visible="False"></asp:Label></td>
         <td class="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
             <asp:TextBox ID="txtbqty" runat="server" Visible="False" Width="90px"></asp:TextBox></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
         </td>
     </tr>
  <tr>
  <td class ="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
      <asp:Label ID="lblBagNumber" runat="server" Text="NO.Of Bags"></asp:Label></td>
 <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;" align="left">
     <asp:TextBox ID="txtbagno" runat="server" Width="146px"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtbagno"
         ErrorMessage="No. of Bags Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
     <td class ="tdmarginddl" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
         <asp:Label ID="lblQuantity" runat="server" Text="Dispatch Qty."></asp:Label></td>
  <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;"> <asp:TextBox ID="txtquant" runat="server" Width="146px"></asp:TextBox>Qtls.<asp:RequiredFieldValidator
      ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtquant" ErrorMessage="Quantity Required"
      ValidationGroup="1">*</asp:RequiredFieldValidator></td>
 
  
  </tr>
  <tr>
  <td class ="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
      <asp:Label ID="lblChallanNumber" runat="server" Text="Truck Challan No." Width="139px"></asp:Label></td>
 <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
     <asp:TextBox ID="txttrukcno" runat="server"  Width ="146px"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txttrukcno"
         ErrorMessage="Challan Number Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
     <td class ="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
         <asp:Label ID="lblTruckNumber" runat="server" Text="Truck No."></asp:Label></td>
  <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;"> <asp:TextBox ID="txttruckno" runat="server" Width="146px"></asp:TextBox></td>
 
  </tr>
     <tr>
         <td class="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
             <asp:Label ID="lblTrans" runat="server" Text="Transporter Name " Width="130px"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
     <asp:DropDownList ID="ddltransporter" runat="server" Width="161px">
     </asp:DropDownList></td>
         <td class="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
         </td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;" align="left">
             <asp:Label ID="lblrecddist" runat="server" Text="Receiving District" Width="130px"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
             <asp:DropDownList ID="ddldistrict" runat="server" Width="161px" AutoPostBack="True" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
             </asp:DropDownList></td>
         <td class="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
             <asp:Label ID="lblrailhead" runat="server" Text="Rail Head Name" Width="104px"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
             <asp:DropDownList ID="ddlsenrailhead" runat="server" Width="153px" AutoPostBack="True" OnSelectedIndexChanged="ddlissuecenter_SelectedIndexChanged">
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
             <asp:Label ID="lblrackno" runat="server" Text="Rack Number"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
             <asp:DropDownList ID="ddlrackno" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlrackno_SelectedIndexChanged"
                 Width="161px">
             </asp:DropDownList></td>
         <td class="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
         </td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
         </td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
             <asp:Label ID="lbldist" runat="server" Visible="False"></asp:Label></td>
         <td class="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
             <asp:Label ID="lbldepo" runat="server" Visible="False"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
             <asp:Label ID="lbldistime" runat="server" Text="Time Of Dispatch" Width="128px"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
             
                                               
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
    </asp:DropDownList></td>
         <td class="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
         </td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
         </td>
     </tr>
  <tr>
  <td class ="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;" >
      <asp:Label ID="lblRemark" runat="server" Text="Remark" Width="109px"></asp:Label></td>
      <td align="left" colspan="3" rowspan="2" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
     <asp:TextBox ID="txtremark" runat="server"  Width ="314px" TextMode ="MultiLine" ></asp:TextBox>
      </td>
  </tr>
  <tr>
  <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;"> </td>
  
  </tr>
     <tr>
         <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
         </td>
         <td align="left" colspan="3" rowspan="1" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
             <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                 ValidationGroup="1" Width="342px" />
         </td>
     </tr>
  <tr>
   
 <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;"></td>
      <td align="left" colspan="3" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; font-size: 10pt; position: static; background-color: #cfdcc8;">
      <asp:Button ID="btnsave" runat="server" Text="Submit" OnClick="btnsave_Click" ValidationGroup="1" Width="75px"/>&nbsp;
          <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" Width="83px" />
          <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="#" Visible="False">Print Challan</asp:HyperLink></td>
     
      

  
  </tr>
     <tr>
         <td colspan="4" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
             <asp:Label ID="Label1" runat="server" Text="Label" Width="404px" Visible="False"></asp:Label></td>
     </tr>
 </table>
 
 </td>
</tr>
</table>

</center>
</div>
<script type="text/javascript">
function CheckIsNumeric(tx)
{
var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode < 46) || (AsciiCode > 57))
{
alert('Please enter only numbers.');
event.cancelBubble = true;
event.returnValue = false;
}

var num=tx.value;
var len=num.length;
var indx=-1;
indx=num.indexOf('.');
if (indx != -1)
{
var coda = event.keyCode;
 if(coda == 46)
 {
    alert('Decimal cannot come twice');
    event.cancelBubble = true;
    event.returnValue = false;
 }
var dgt=num.substr(indx,len);
var count= dgt.length;
//alert (count);
if (count > 5)  
{
 alert("Only 5 decimal digits allowed");
 event.cancelBubble = true;
 event.returnValue = false;
}
}

}



    </script>
</asp:Content>

