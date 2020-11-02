<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Movement_Challan_WLC.aspx.cs" Inherits="IssueCenter_Movement_Challan_WLC" Title="Movement Challan WLC" %>
 
<%@ Register Assembly="CustomControlFreak" Namespace="CustomControlFreak" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc1" Namespace="EeekSoft.Web" Assembly="EeekSoft.Web.PopupWin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="hedl" style="color: maroon"> Movement Challan</div>
<div id ="ronewmargin">
<center>
 <table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: green 3px double; border-top: green 3px double; border-left: green 3px double; border-bottom: green 3px double; width: 619px; font-size: 10pt;" id="TABLE1" onclick="return TABLE1_onclick()"  >
     <tr>
         <td class="tdmarginro" style="width: 119px; background-color: #e3dba4; height: 18px;">
         </td>
         <td align="left" style="width: 119px; background-color: #e3dba4; height: 18px;">
         </td>
         <td class="tdmarginro" style="width: 119px; background-color: #e3dba4; height: 18px;">
         </td>
         <td align="left" style="width: 119px; background-color: #e3dba4; height: 18px;">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="width: 119px; background-color: #e3dba4;">
             Type Of Depositor</td>
         <td align="left" style="width: 119px; background-color: #e3dba4;">
             <asp:DropDownList ID="ddldepositortype" runat="server" Width="164px" OnSelectedIndexChanged="ddldepositortype_SelectedIndexChanged" AutoPostBack="True">
             </asp:DropDownList></td>
         <td class="tdmarginro" style="width: 119px; background-color: #e3dba4;">
             <asp:Label ID="fcidist" runat="server" Width="80px" Visible="False"></asp:Label></td>
         <td align="left" style="width: 119px; background-color: #e3dba4;">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="width: 119px; background-color: #e3dba4">
             Name of Depositor</td>
         <td align="left" style="width: 119px; background-color: #e3dba4">
             <asp:DropDownList ID="ddldepositor" runat="server" Width="163px" OnSelectedIndexChanged="ddldepositor_SelectedIndexChanged">
             </asp:DropDownList></td>
         <td class="tdmarginro" style="width: 119px; background-color: #e3dba4">
             Date of Deposit</td>
         <td align="left" style="width: 119px; background-color: #e3dba4"><cc1:DaintyDate ID="DaintyDate2" runat="server" FormatType="DDMMYYYY" />
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="width: 119px; background-color: #e3dba4">
         </td>
         <td align="right" style="width: 119px; background-color: #e3dba4">
             </td>
         <td class="tdmarginro" style="width: 119px; background-color: #e3dba4; font-weight: bold; color: #003333;">
             Sender</td>
         <td align="left" style="width: 119px; background-color: #e3dba4">
         </td>
     </tr>
 <tr>
 <td class ="tdmarginro" style="width: 119px; background-color: #e3dba4;" >Source of Arrival</td>
 <td style="width: 119px; background-color: #e3dba4;" align="left">
    <asp:DropDownList ID="ddlsarrival" runat="server" Width="160px" OnSelectedIndexChanged="ddlsarrival_SelectedIndexChanged" AutoPostBack="True">
           
     </asp:DropDownList>
 </td>
     <td class ="tdmarginro" style="width: 119px; background-color: #e3dba4;">
         Challan No.</td>
  <td align="left" style="width: 119px; background-color: #e3dba4"> &nbsp;<asp:DropDownList ID="ddlchallan" runat="server" Width="154px" AutoPostBack="True" OnSelectedIndexChanged="ddlchallan_SelectedIndexChanged" >
        
         <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
         
     </asp:DropDownList></td>
 </tr>
     <tr>
         <td class="tdmarginro" style="width: 119px; background-color: #e3dba4;">
             Book No.</td>
         <td align="left" style="width: 119px; background-color: #e3dba4;">
             <asp:TextBox ID="TextBox6" runat="server" Width="150px"></asp:TextBox></td>
         <td class="tdmarginro" style="width: 119px; background-color: #e3dba4;">
     <asp:Label ID="lblchallandt" runat="server" Text="Challan Date"></asp:Label></td>
         <td style="width: 119px; background-color: #e3dba4;">
         <asp:TextBox ID="txtchallandt" runat="server"></asp:TextBox></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="width: 119px; background-color: #e3dba4;">
             <asp:Label ID="lbldist" runat="server" Text="District"></asp:Label></td>
         <td align="left" style="width: 119px; background-color: #e3dba4;">
             <asp:DropDownList ID="ddldistrict" runat="server" Width="161px" AutoPostBack="True" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
         <td class="tdmarginddl" style="width: 119px; background-color: #e3dba4;">
             <asp:Label ID="lbldepo" runat="server" Text="Depot"></asp:Label></td>
         <td align="left" style="width: 119px; background-color: #e3dba4">
             <asp:DropDownList ID="ddlissue" runat="server" Width="157px">
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #e3dba4;">
             <asp:Label ID="lblchallanno" runat="server" Text="Enter Challan No." Visible="False" Width="109px"></asp:Label></td>
         <td align="left" style="background-color: #e3dba4;">
     <asp:TextBox ID="txtchallan" runat="server" Width="154px" Visible="False" ></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtchallan"
         ErrorMessage="Challan No Requierd " ValidationGroup="1">*</asp:RequiredFieldValidator></td>
         <td class="tdmarginddl" style="background-color: #e3dba4;">
             <asp:Label ID="lblchallandate" runat="server" Text="EnterChallan Date" Visible="False"></asp:Label></td>
         <td align="left" style="background-color: #e3dba4;">
             <cc1:DaintyDate ID="DaintyDate3" runat="server" FormatType="DDMMYYYY" Visible="False" />
         </td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="width: 119px; background-color: #e3dba4">
         <asp:Label ID="lblcomodtyddl" runat="server" Text="Commodity"></asp:Label></td>
         <td align="left" style="width: 119px; background-color: #e3dba4">
      <asp:DropDownList ID="ddlcomdty" runat="server"  Width="162px"  OnSelectedIndexChanged="ddlcomdty_SelectedIndexChanged">
       <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
      </asp:DropDownList></td>
         <td class="tdmarginddl" style="width: 119px; background-color: #e3dba4">
      <asp:Label ID="lblschemeddl" runat="server" Text="Scheme"></asp:Label></td>
         <td style="width: 119px; background-color: #e3dba4">
     <asp:DropDownList ID="ddlscheme" runat="server"  Width="150px" OnSelectedIndexChanged="ddlscheme_SelectedIndexChanged" AutoPostBack="True">
      <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
     </asp:DropDownList>
     <asp:Label ID="lblsch" runat="server" Visible="False" Width="2px"></asp:Label></td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="width: 119px; background-color: #e3dba4">
         </td>
         <td align="left" style="width: 119px; background-color: #e3dba4">
      <asp:Label ID="lblcomdty" runat="server" Visible="False"></asp:Label></td>
         <td class="tdmarginddl" style="font-weight: bold; width: 119px; color: #003333; background-color: #e3dba4">
         </td>
         <td style="width: 119px; background-color: #e3dba4">
         </td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="width: 119px; background-color: #e3dba4">
             Category</td>
         <td align="left" style="width: 119px; background-color: #e3dba4">
      <asp:DropDownList ID="ddlcategory" runat="server" Width="163px"  OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged">
      <asp:ListItem Value ="0"> --Select--</asp:ListItem>
      </asp:DropDownList></td>
         <td class="tdmarginddl" style="width: 119px; background-color: #e3dba4">
             Crop Year</td>
         <td style="width: 119px; background-color: #e3dba4">
                                            <asp:DropDownList ID="ddlcropyear" runat="server"  Width="159px" >
                                                <asp:ListItem Value="01" Selected="True">Crop year not indicated</asp:ListItem>
                                                <asp:ListItem Value="02">2009-2010</asp:ListItem>
                                                <asp:ListItem Value="03">2008-2009</asp:ListItem>
                                                <asp:ListItem Value="04">2007-2008</asp:ListItem>
                                                <asp:ListItem Value="05">2006-2007</asp:ListItem>
                                                <asp:ListItem Value="06">2005-2006</asp:ListItem>
                                                <asp:ListItem Value="07">2004-2005</asp:ListItem>
                                                <asp:ListItem Value="08">2003-2004</asp:ListItem>
                                                <asp:ListItem Value="09">2002-2003</asp:ListItem>
                                                <asp:ListItem Value="10">2001-2002</asp:ListItem>
                                                <asp:ListItem Value="11">2000-2001</asp:ListItem>
                                            </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="width: 119px; background-color: #e3dba4">
             Transporter</td>
         <td align="left" style="width: 119px; background-color: #e3dba4">
     <asp:DropDownList ID="ddltransport" runat="server" Width="162px">
       <asp:ListItem Value ="0"> --Select--</asp:ListItem>
     </asp:DropDownList></td>
         <td class="tdmarginddl" style="width: 119px; background-color: #e3dba4">
             Vehicle No.</td>
         <td style="width: 119px; background-color: #e3dba4">
             <asp:TextBox ID="txtvehleno" runat="server" Width="150px" ></asp:TextBox></td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="width: 119px; background-color: #e3dba4">
             Name of Driver</td>
         <td align="left" style="width: 119px; background-color: #e3dba4">
             <asp:TextBox ID="TextBox5" runat="server" Width="152px"></asp:TextBox></td>
         <td class="tdmarginddl" style="width: 119px; background-color: #e3dba4">
             Type of Vehicle</td>
         <td style="width: 119px; background-color: #e3dba4">
             <asp:DropDownList ID="ddlvrhicletype" runat="server" Width="162px" OnSelectedIndexChanged="ddlvrhicletype_SelectedIndexChanged">
                 <asp:ListItem Value="0"> --Select--</asp:ListItem>
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="width: 119px; background-color: #e3dba4">
             License No.</td>
         <td align="left" style="width: 119px; background-color: #e3dba4">
             <asp:TextBox ID="txtlicense" runat="server" Width="152px"></asp:TextBox></td>
         <td class="tdmarginddl" style="width: 119px; background-color: #e3dba4">
             Valid Upto</td>
         <td style="width: 119px; background-color: #e3dba4">
             <cc1:DaintyDate ID="DaintyDate1" runat="server" FormatType="DDMMYYYY" />
         </td>
     </tr>
     <tr>
         <td class="tdmarginwlc" style="background-color: #e3dba4; height: 64px;" colspan="2">
             Discription of Available 
             <br />
             Paper of Gate Pass in Truck</td>
         <td class="tdmarginddl" style="background-color: #e3dba4; height: 64px;" colspan="2">
             <asp:TextBox ID="TextBox1" runat="server" Height="54px" TextMode="MultiLine" Width="317px"></asp:TextBox></td>
     </tr>
     <tr>
         <td  class="tdmarginddl" style="width: 119px; background-color: #e3dba4; height: 19px;">
         </td>
         <td align="left" style="width: 119px; background-color: #e3dba4; height: 19px;">
         </td>
         <td  style="width: 119px; background-color: #e3dba4; font-weight: normal; color: #003333;">
             Gunny Bags Details</td>
         <td style="width: 119px; background-color: #e3dba4; height: 19px;">
         </td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="width: 119px; background-color: #e3dba4">
             <asp:Button ID="btnadd" runat="server" Text="Add" Width="57px" OnClick="btnadd_Click" /></td>
         <td align="left" style="width: 119px; background-color: #e3dba4">
         </td>
         <td class="tdmarginddl" style="width: 119px; background-color: #e3dba4">
         </td>
         <td style="width: 119px; background-color: #e3dba4">
         </td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="width: 119px; background-color: #e3dba4">
         </td>
         <td align="left" style="width: 119px; background-color: #e3dba4">
     <asp:Label ID="lbltid" runat="server" Visible="False"></asp:Label></td>
         <td class="tdmarginddl" style="font-weight: bold; width: 119px; color: #003333; background-color: #e3dba4">
             Quantity</td>
         <td style="width: 119px; background-color: #e3dba4">
         </td>
     </tr>
  <tr>
  
  <td class ="tdmarginddl" style="width: 119px; background-color: #e3dba4;" >
      Diaspatch Qty</td>
  <td style="width: 119px; background-color: #e3dba4;" align="left">
      <asp:TextBox ID="txtqty" runat="server" Width="90px" ></asp:TextBox>Qtls/Kgms.</td>
      <td class ="tdmarginddl" style="width: 119px; background-color: #e3dba4"> No. Of Bags</td>
      <td style="width: 119px; background-color: #e3dba4"> 
                                            <asp:TextBox ID="txtnobags" runat="server" Width="148px" ></asp:TextBox></td>
 </tr>
     <tr>
         <td class="tdmarginddl" style="width: 119px; background-color: #e3dba4">
         </td>
         <td align="left" style="width: 119px; background-color: #e3dba4">
         </td>
         <td class="tdmarginddl" style="font-weight: bold; width: 119px; color: #003333; background-color: #e3dba4">
             Quality Result</td>
         <td style="width: 119px; background-color: #e3dba4">
         </td>
     </tr>
 <tr>
     <td class ="tdmarginddl" style="width: 119px; background-color: #e3dba4;">
         Category(Accepted)</td>
  <td style="width: 119px; background-color: #e3dba4;" align="left"> &nbsp;
      <asp:DropDownList ID="ddlacptcat" runat="server" Width="152px"  OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged">
          <asp:ListItem Value="0"> --Select--</asp:ListItem>
      </asp:DropDownList></td>
  <td class ="tdmarginddl" style="width: 119px; background-color: #e3dba4;">
      Moisture Content </td>
 <td align="left" style="width: 119px; background-color: #e3dba4;">
     &nbsp;<asp:TextBox ID="txtmoisture" runat="server" Width="110px"></asp:TextBox>%</td>
  
                                       
  </tr>
  <tr>
 <td style="width: 119px; background-color: #e3dba4;" >  </td>
 <td style="width: 119px; background-color: #e3dba4;" align="left">  </td>
 <td align="left" style="width: 119px; background-color: #e3dba4; color: #006666;" >  
     <strong>Additional Information</strong></td>
 <td style="width: 119px; background-color: #e3dba4"> </td>
 </tr>
 <tr>
  <td class ="tdmarginro" style="width: 119px; background-color: #e3dba4;" >
      Received from district</td>
  <td style="width: 119px; background-color: #e3dba4;" align="left"><asp:DropDownList ID="DropDownList2" runat="server" Width="154px"  OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged">
      <asp:ListItem Value="1">OutSide</asp:ListItem>
      <asp:ListItem Value="2">Within</asp:ListItem>
  </asp:DropDownList></td>
      
      <td style="width: 130px; background-color: #e3dba4">
          Sending Time of good</td>
  <td align="left"  background-color: style="width: 130px; background-color: #e3dba4" #e3dba4" >
      <asp:Panel ID="Panel1" runat="server" Height="30px" Width="150px">
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
                        </asp:DropDownList>:<asp:DropDownList ID="ddlminute" runat="server">
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
      </asp:Panel>
         </td>
      
 </tr>
  <tr>
  <td class ="tdmarginro" style="width: 119px; background-color: #e3dba4;" >
      Sample Slip No.</td>
 <td style="width: 119px; background-color: #e3dba4;" align="left">
     &nbsp;<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
     <td class ="tdmarginro" style="background-color: #e3dba4">WCM No.</td>
  <td align="left" style="width: 119px; background-color: #e3dba4"> &nbsp;<asp:TextBox ID="txtwcmno" runat="server" Width="145px" MaxLength="7" ></asp:TextBox></td>

  </tr>
     <tr>
         <td class="tdmarginro" style="width: 119px; background-color: #e3dba4">
             H.O/D.O's Transport No.</td>
         <td align="left" style="width: 119px; background-color: #e3dba4">
             <asp:TextBox ID="TextBox3" runat="server" MaxLength="7" Width="150px"></asp:TextBox></td>
         <td class="tdmarginro" style="width: 119px; background-color: #e3dba4">
             H.O/D.O's Transport Date</td>
         <td align="left" style="width: 119px; background-color: #e3dba4"><cc1:DaintyDate ID="DaintyDate4" runat="server" FormatType="DDMMYYYY" Visible="False" />
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="width: 119px; background-color: #e3dba4">
             Bilty No.</td>
         <td align="left" style="width: 119px; background-color: #e3dba4">
             <asp:TextBox ID="TextBox4" runat="server" MaxLength="7" Width="148px"></asp:TextBox></td>
         <td class="tdmarginro" style="width: 119px; background-color: #e3dba4">
             Bilty Date</td>
         <td align="left" style="width: 119px; background-color: #e3dba4"><cc1:DaintyDate ID="DaintyDate5" runat="server" FormatType="DDMMYYYY" Visible="False" />
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="width: 119px; background-color: #e3dba4">
         </td>
         <td align="left" style="width: 119px; background-color: #e3dba4">
         </td>
         <td class="tdmarginro" style="width: 119px; background-color: #e3dba4">
             </td>
         <td align="left" style="width: 119px; background-color: #e3dba4">
         </td>
     </tr>
  <tr>
    <td class="tdtime" style="width: 119px; background-color: #e3dba4;">
                                            Gunny Type</td>
  
    <td colspan ="2" align="left" style="width: 119px; background-color: #e3dba4;">
                                            <asp:DropDownList ID="ddlgtype" runat="server" Width="154px" >
                                                <asp:ListItem Value="01" Selected="True">-Select-</asp:ListItem>
                                            </asp:DropDownList></td> 
    <td style="width: 119px; background-color: #e3dba4;"> </td>
  </tr>
     <tr>
         <td class="tdtime" style="width: 119px; background-color: #e3dba4">
         </td>
         <td align="left" colspan="2" style="width: 119px; background-color: #e3dba4">
             <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click" ValidationGroup="1" Width="141px" />
         </td>
         <td style="width: 119px; background-color: #e3dba4">
             <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click1" Width="106px" /></td>
     </tr>
  <tr>
  <td colspan ="4" style="width: 119px; background-color: #e3dba4;">
      &nbsp;</td>
                        
  </tr>
       <tr>
       <td align="left" colspan="4" rowspan="2"> 
           <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
               ValidationGroup="1" ShowSummary="False" />
           <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label></td>
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

