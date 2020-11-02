<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Edit_LARO_Page.aspx.cs" Inherits="District_Edit_LARO_Page" Title="Edit Lifting Against Release Order " %>

 
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="hedl">
        &nbsp;</div>
    <div id="ronewmargin">
       
                        
                <table cellpadding ="0" cellspacing ="0" border ="0" class="laromargin" style="width: 634px">
                    <tr>
                        <td  colspan="6" style="border-right: 1px solid; border-top: 1px solid;
                            border-left: 1px solid; border-bottom: 1px solid; background-color: #cccccc" align="center">
                            Edit Lifting Against&nbsp; FCI Release Order
                        </td>
                    </tr>
                    <tr>
                        <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 90px;">
                            &nbsp; &nbsp; &nbsp;&nbsp;
                        </td>
                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
                            &nbsp;
                                            <asp:DropDownList ID="ddlrono" runat="server" Width="82px" OnSelectedIndexChanged="ddlrono_SelectedIndexChanged"
                                                AutoPostBack="True" Visible="False">
                                                
                                            </asp:DropDownList></td>
                        <td  colspan="2" align="center">
                            RO Information&nbsp;
                        </td>
                        <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 63px;">
                            &nbsp;&nbsp;
                        </td>
                        <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
                            &nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                       
                          
                                
                                
                                        <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            R.O. No.</td>
                                        <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            <asp:TextBox ID="txtrono" runat="server" Width="99px" ReadOnly="True" OnTextChanged="txtrono_TextChanged" BackColor="ActiveCaption"></asp:TextBox></td>
                                        <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; width: 70px; border-bottom: silver 1px solid;">
                                            R.O. Date</td>
                                        <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            <asp:TextBox ID="txtrodate" runat="server" Width="96px" ReadOnly="True" BackColor="ActiveCaption" ></asp:TextBox>
                                        </td>
                                        <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            R.O. Qty.</td>
                                        <td  align ="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" >
                                            <asp:TextBox ID="txtroqty" runat="server" Width="91px" ReadOnly="True" BackColor="ActiveCaption" ></asp:TextBox>
                                            <asp:Label ID="Label5" runat="server" Font-Size="12px" Text="(Qtl.)"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="tdmarginddl" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            Commodity</td>
                                        <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            <asp:TextBox ID="txtcomdty" runat="server" Width="97px" ReadOnly="True" BackColor="ActiveCaption" ></asp:TextBox>
                                        </td>
                                        <td class="tdmarginddl" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; width: 70px; border-bottom: silver 1px solid;">
                                            Scheme</td>
                                        <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            <asp:TextBox ID="txtscheme" runat="server" Width="94px" ReadOnly="True" BackColor="ActiveCaption" ></asp:TextBox>&nbsp;</td> 
                                        <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            Bal. Qty.</td>
                                        <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;"> 
                                            <asp:TextBox ID="txtbalqty" runat="server" ReadOnly="True" Width="94px" BackColor="ActiveCaption"></asp:TextBox>
                                            <asp:Label ID="Label6" runat="server" Font-Size="12px" Text="(Qtl.)"></asp:Label></td>
                                    </tr>
                                
        <asp:Label ID="Label1" runat="server"></asp:Label></table> 
                                   <table border="0" cellpadding="0" cellspacing="0" style="width: 631px">
                                       <tr>
                                           <td  style="height: 22px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #cccccc;" align="center" colspan="4">
                                               &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;
                                               <strong>
                                               Transpotation&nbsp; Information</strong>&nbsp;
                                           </td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Navy"
                                                   Text="From  FCI......" Width="112px"></asp:Label></td>
                                           <td class="tdmarginro" colspan="1" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               &nbsp;</td>
                                           <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="center" colspan="2">
                                               </td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:Label ID="Label4" runat="server" Text="T.O.Number"></asp:Label></td>
                                           <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:TextBox ID="txttonumber" runat="server" ReadOnly="True" Width="127px" BackColor="ActiveCaption"></asp:TextBox></td>
                                           <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                           </td>
                                           <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                           </td>
                                       </tr>
                                    <tr>
                                        <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            FCI AM Office</td>
                                        <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
                                            <asp:DropDownList ID="ddlfcidist" runat="server" Width="134px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" Enabled="False" BackColor="ActiveCaption">
                                            <asp:ListItem Value="01" Selected="True">-Select-</asp:ListItem>
                                            </asp:DropDownList>&nbsp;</td>
                                        <td  class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
                                            Dispatch Depot:</td>
                                        <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            <asp:DropDownList ID="ddlfcidepo" runat="server" Width="165px" Enabled="False" BackColor="ActiveCaption">
                                            <asp:ListItem Value="01" Selected="True">-Select-</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                       <tr>
                                           <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:Label ID="Label12" runat="server" Text="TO MPSCSC....." Width="115px" Font-Bold="True" ForeColor="Navy"></asp:Label></td>
                                           <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               &nbsp;</td>
                                           <td class="tdmarginlaro"  align="left" colspan="2" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               &nbsp;</td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
                                               <asp:Label ID="Label7" runat="server" Text="Destination District" Width="121px"></asp:Label></td>
                                           <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:DropDownList ID="ddldistrict" runat="server" Width="134px" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged" AutoPostBack ="true" Enabled="False" BackColor="ActiveCaption">
                                                <asp:ListItem Value="01" Selected="True">-Select-</asp:ListItem>
                                            </asp:DropDownList></td>
                                           <td class="tdmarginro" align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:Label ID="Label8" runat="server" Text="Destination Depot" Width="117px"></asp:Label></td>
                                           <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               &nbsp;<asp:DropDownList ID="ddlissue" runat="server" Width="165px" Enabled="False" BackColor="ActiveCaption" >
                                                <asp:ListItem Value="01" Selected="True">-Select-</asp:ListItem>
                                            </asp:DropDownList></td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            Challan No</td>
                                           <td class="tdmarginro" colspan="1" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            <asp:TextBox ID="txtchallan" runat="server" Width="127px" BackColor="ActiveCaption" ></asp:TextBox></td>
                                           <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               Challan Date
                                           </td>
                                           <td colspan="1" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                       <asp:TextBox ID="challandate" runat="server" Width="111px" BackColor="ActiveCaption"></asp:TextBox>
                                       <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_challandate'
	    });
	     </script>
                                       
                                       </td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            Truck No:</td>
                                           <td class="tdmarginro" colspan="1" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            <asp:TextBox ID="txtvehno" runat="server" Width="127px" ></asp:TextBox></td>
                                           <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:Label ID="Label9" runat="server" Text="Transporter" Width="98px"></asp:Label></td>
                                           <td class="tdmarginro" colspan="1" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                              <asp:DropDownList ID="txttrans" runat="server"  OnSelectedIndexChanged="ddlgtype_SelectedIndexChanged" Enabled="False" BackColor="ActiveCaption">
                                                <asp:ListItem Value="01" Selected="True">-Select-</asp:ListItem>
                                            </asp:DropDownList></td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               Dispatch Qty</td>
                                           <td colspan="1" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:TextBox ID="txtqtysend" runat="server" Width="127px"></asp:TextBox>
                                               <asp:Label ID="Label10" runat="server" Font-Size="11px" Text="(Qtl.)"></asp:Label></td>
                                           <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            No. of Bags</td>
                                           <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            <asp:TextBox ID="txtnobags" runat="server" Width="151px" ></asp:TextBox></td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:Label ID="Label2" runat="server" Text="Crop Year" Visible="False"></asp:Label></td>
                                           <td class="tdmarginro" colspan="1" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            <asp:DropDownList ID="ddlcropyear" runat="server" Width="134px"  AutoPostBack ="false" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Visible="False">
                                                <asp:ListItem Value="01" Selected="True">-Not Indicated-</asp:ListItem>
                                                <asp:ListItem Value="02">2010-2009</asp:ListItem>
                                                <asp:ListItem Value="03">2009-2008</asp:ListItem>
                                                <asp:ListItem Value="04">2008-2007</asp:ListItem>
                                                <asp:ListItem Value="05">2007-2006</asp:ListItem>
                                                <asp:ListItem Value="06">2006-2005</asp:ListItem>
                                                <asp:ListItem Value="07">2005-2004</asp:ListItem>
                                                <asp:ListItem Value="08">2004-2003</asp:ListItem>
                                                <asp:ListItem Value="09">2003-2002</asp:ListItem>
                                                <asp:ListItem Value="10">2002-2001</asp:ListItem>
                                                <asp:ListItem Value="11">2001-2000</asp:ListItem>
                                            </asp:DropDownList>
                                            </td>
                                           <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:Label ID="Label3" runat="server" Text="Category" Visible="False"></asp:Label></td>
                                           <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:DropDownList ID="ddlcategory" runat="server" Visible="False" Width="134px">
                                            </asp:DropDownList></td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               Dispatch Time</td>
                                           <td align="left"   style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               
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
                            <asp:ListItem Value="60">60</asp:ListItem>
                        </asp:DropDownList>:<asp:DropDownList ID="ddlampm" runat="server" Width="46px">
    <asp:ListItem Value="01">AM</asp:ListItem>
    <asp:ListItem Value="02">PM</asp:ListItem>
    </asp:DropDownList>
                                            </td>
                                           <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
                                               Moisture( %) &nbsp;&nbsp;
                                           </td>
                                           <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            <asp:TextBox ID="txtmoisture" runat="server" Width="150px"></asp:TextBox></td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            <asp:TextBox ID="txtgowdn" runat="server" Width="102px" Visible="False" ></asp:TextBox></td>
                                           <td align="left" colspan="2" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:Label ID="lblfqty" runat="server" Visible="False"></asp:Label></td>
                                           <td align="left" class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:DropDownList ID="ddldepottype" runat="server" Enabled="False" Visible="False" Width="126px">
                                                   <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
                                                   <asp:ListItem Value="02">OWNED</asp:ListItem>
                                                   <asp:ListItem Value="03">CWC</asp:ListItem>
                                                   <asp:ListItem Value="04">SWC</asp:ListItem>
                                                   <asp:ListItem Value="05">PRIVATE</asp:ListItem>
                                                   <asp:ListItem Value="06">Hired(Private party)</asp:ListItem>
                                               </asp:DropDownList></td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            <asp:DropDownList ID="ddlgtype" runat="server" Width="109px"  OnSelectedIndexChanged="ddlgtype_SelectedIndexChanged" Visible="False">
                                                <asp:ListItem Value="01" Selected="True">-Select-</asp:ListItem>
                                            </asp:DropDownList></td>
                                           <td align="right" colspan="2" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               &nbsp;<asp:Button ID="btnsubmit" runat="server" Text="Update Record " OnClick="btnsubmit_Click" Width="110px" />
                                               </td>
                                           <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:Button ID="btnclose" runat="server" OnClick="btnclose_Click" Text="Close" Width="123px" /></td>
                                       </tr>
                                </table>
                      
               
            
     
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
