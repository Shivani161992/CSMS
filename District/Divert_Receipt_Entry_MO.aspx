<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Divert_Receipt_Entry_MO.aspx.cs" Inherits="IssueCenter_Receipt_Entry_MO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>
    <script type="text/javascript" lang="javascript" src="../js/calendarMvmt.js"></script>
    <%--For Calendar Controls--%>
    <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" /><script type="text/javascript" lang="" src="../js/calendarMvmt.js"></script><%--Allow Only 2 Digit After Decimal--%><script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/Number2D.js"></script><script type="text/javascript">
                                                                                                                                                                                                                                                                                                                                                           window.onload = function () {
                                                                                                                                                                                                                                                                                                                                                           };
    </script><style type="text/css">
                 .ButtonClass {
                     cursor: pointer;
                 }
             </style><style type="text/css">
                         .hiddencol {
                             display: none;
                         }
                     </style><div id="dvScroll" style="overflow-y: scroll; height: 500px;">
                         <table align="center" style="width: 760px; border-style: solid; border-width: 1px; text-align: left; font-size: medium" border="1" cellspacing="0" cellpadding="2">
                             <tr>
                                 <td colspan="6" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600"><strong>Divert Receipt Entry</strong>
                                     <input id="hdfSMO" type="hidden" runat="server" />
                                     <input id="hdfSTO_No" type="hidden" runat="server" />
                                     <input id="hdfDC_No" type="hidden" runat="server" />
                                     <input id="hdfTransporter_ID" type="hidden" runat="server" />
                                     <input id="hdfFrmDist" type="hidden" runat="server" />
                                     <input id="hdfCommodity" type="hidden" runat="server" />
                                     <input id="hdfTranspDate" type="hidden" runat="server" />
                                     <input id="hdfModeofDispatch" type="hidden" runat="server" />
                                     <input id="hdfToDist" type="hidden" runat="server" />
                                     <input id="hdfTOEndDate" type="hidden" runat="server" />
                                     <input id="hdfRecIC" type="hidden" runat="server" />
                                     <input id="hdfRecBranch" type="hidden" runat="server" />
                                     <input id="hdfRecGodown" type="hidden" runat="server" />
                                        <input id="hdfBagsType" type="hidden" runat="server" />


                                 </td>
                             </tr>

                             <tr>
                                 <td rowspan="37"></td>
                                 <td colspan="4" style="text-align: center; background-color: #CCCCCC">
                                     <asp:Label ID="Label2" runat="server" Style="text-align: center; font-weight: 700;" ForeColor="Blue" Visible="False"></asp:Label>
                                 </td>
                                 <td rowspan="37"></td>
                             </tr>

                             <tr>
                                 <td>Source of Arrival</td>
                                 <td>
                                     <asp:DropDownList ID="ddlSArrival" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlSArrival_SelectedIndexChanged">
                                         <asp:ListItem>--Select--</asp:ListItem>
                                         <asp:ListItem Value="02">Transfer By Road</asp:ListItem>
                                     </asp:DropDownList>

                                 </td>
                                 <td>
                                     <asp:Label ID="LblChallanNo" runat="server" Text="Challan Number"></asp:Label>
                                 </td>
                                 <td>
                                     <asp:DropDownList ID="ddlChallanNo" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlChallanNo_SelectedIndexChanged">
                                     </asp:DropDownList>

                                 </td>
                             </tr>



                             <tr runat="server" id="trBookNo" visible="false">
                                 <td>Book Number</td>
                                 <td>

                                     <asp:DropDownList ID="ddlBookNo" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlBookNo_SelectedIndexChanged">
                                     </asp:DropDownList>


                                 </td>
                                 <td>Page Number</td>
                                 <td>
                                     <asp:DropDownList ID="ddlPageNo" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlPageNo_SelectedIndexChanged">
                                     </asp:DropDownList>


                                 </td>
                             </tr>




                             <tr>
                                 <td>परिवहन आदेश क्रमांक</td>
                                 <td>

                                     <asp:TextBox ID="txtTONo" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>

                                 </td>

                                 <td>मुख्यालय एम ओ क्रमांक</td>
                                 <td>
                                     <asp:TextBox ID="txtMONo" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtMONo" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                                 </td>
                             </tr>




                             <tr>
                                 <td>परिवहनकर्ता का नाम</td>
                                 <td>

                                     <asp:TextBox ID="txtTransporterName" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                                 </td>

                                 <td><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: medium; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">परिवहन का दिनांक</span></td>
                                 <td>
                                     <asp:TextBox ID="txtTranspDate" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                                 </td>
                             </tr>




                             <tr>
                                 <td><span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: medium; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">प्रेषणकर्ता जिला</span></td>
                                 <td>

                                     <asp:TextBox ID="txtFrmDist" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                                 </td>

                                 <td>कमोडिटी</td>
                                 <td>

                                     <asp:TextBox ID="txtCommodity" runat="server" Width="146px" ReadOnly="True" Enabled="False" Style="margin-bottom: 0px"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtCommodity" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                                 </td>
                             </tr>




                             <tr>
                                 <td>भेजी<span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: medium; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);"><span class="Apple-converted-space">&nbsp;गयी मात्रा&nbsp;<asp:Label ID="lblQtls0" runat="server" style="font-weight: 700" Text=""></asp:Label>
                                     </span></span></td>
                                 <td>

                                     <asp:TextBox ID="txtSendQty" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtSendQty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                                 </td>

                                 <td>भेजे गये बोरों की संख्या</td>
                                 <td>

                                     <asp:TextBox ID="txtSendBags" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtSendBags" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                                 </td>
                             </tr>




                             <tr>
                                 <td>बोरों का प्रकार</td>
                                 <td>

                                     <asp:TextBox ID="txtBagsType" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtBagsType" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                                 </td>

                                 <td>ट्रक नंबर</td>
                                 <td>

                                     <asp:TextBox ID="txtTruckNo" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                                 </td>
                             </tr>




                             <tr>
                                 <td>प्राप्ति संग्रहण केंद्र</td>
                                 <td>

                                     <asp:TextBox ID="txtSendIC" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                                 </td>

                                 <td>प्राप्ति ब्रांच</td>
                                 <td>

                                     <asp:TextBox ID="txtSendBranch" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                                 </td>
                             </tr>




                             <tr>
                                 <td>प्राप्ति गोदाम</td>
                                 <td>

                                     <asp:TextBox ID="txtSendGodown" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                                 </td>

                                 <td>उपार्जन वर्ष</td>
                                 <td>

                                     <asp:TextBox ID="txtCropYear" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                                 </td>
                             </tr>



                             <tr>
                                 <td colspan="4">
                                     <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" ShowFooter="True" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                                         OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Style="font-size: medium">
                                         <AlternatingRowStyle BackColor="White" />
                                         <Columns>

                                             <asp:BoundField ReadOnly="true" HeaderText="क्र." HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right">

                                                 <FooterStyle HorizontalAlign="Right"></FooterStyle>

                                                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                                                 <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                             </asp:BoundField>


                                             <asp:BoundField DataField="Branch" HeaderText="ब्रांच" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right">


                                                 <FooterStyle HorizontalAlign="Right"></FooterStyle>

                                                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                                                 <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                             </asp:BoundField>


                                             <asp:BoundField DataField="Godown" HeaderText="गोदाम" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right">


                                                 <FooterStyle HorizontalAlign="Right"></FooterStyle>

                                                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                                                 <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                             </asp:BoundField>


                                             <asp:BoundField DataField="RequiredQuantity" HeaderText="कुल मात्रा(Qtls)" HeaderStyle-HorizontalAlign="Right" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right">


                                                 <FooterStyle HorizontalAlign="Right"></FooterStyle>

                                                 <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle"></HeaderStyle>

                                                 <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
                                             </asp:BoundField>


                                             <asp:BoundField DataField="RemQty" HeaderText="बची हुई मात्रा(Qtls)" HeaderStyle-HorizontalAlign="Right" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right">


                                                 <FooterStyle HorizontalAlign="Right"></FooterStyle>

                                                 <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle"></HeaderStyle>

                                                 <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
                                             </asp:BoundField>


                                             <asp:BoundField DataField="Branch" HeaderText="BranchVal" HeaderStyle-HorizontalAlign="Right" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol">


                                                 <FooterStyle HorizontalAlign="Right"></FooterStyle>

                                                 <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle"></HeaderStyle>

                                                 <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
                                             </asp:BoundField>


                                             <asp:BoundField DataField="Godown" HeaderText="GodownVal" HeaderStyle-HorizontalAlign="Right" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol">

                                                 <FooterStyle HorizontalAlign="Right"></FooterStyle>

                                                 <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle"></HeaderStyle>

                                                 <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
                                             </asp:BoundField>



                                             <asp:BoundField DataField="SMO" HeaderText="SMO" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol" />
                                             <asp:BoundField DataField="RMO" HeaderText="RMO" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol" />



                                             <asp:TemplateField HeaderText="चुनें">
                                                 <ItemTemplate>
                                                     <asp:LinkButton ID="idSelect" runat="server" CausesValidation="false" CommandName="Select" Text="Select"></asp:LinkButton>
                                                 </ItemTemplate>
                                                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                 <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                             </asp:TemplateField>
                                         </Columns>
                                         <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                         <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                         <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                         <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                         <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                     </asp:GridView>
                                 </td>
                             </tr>


                             <tr id="trGridBG" runat="server">
                                 <td>ब्रांच</td>
                                 <td>
                                     <asp:TextBox ID="txtBranch" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtBranch" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                                 </td>
                                 <td>गोदाम</td>
                                 <td>
                                     <asp:TextBox ID="txtGodown" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtGodown" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                                 </td>
                             </tr>

                             <tr id="trGridQty" runat="server">
                                 <td>कुल मात्रा</td>
                                 <td>
                                     <asp:TextBox ID="txtQtyTotal" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtQtyTotal" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                                 </td>
                                 <td>बची हुई मात्रा</td>
                                 <td>
                                     <asp:TextBox ID="txtQtyRemTotal" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtQtyRemTotal" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                                 </td>
                             </tr>

                             <tr id="trMsg" visible="false" runat="server">
                                 <td colspan="4" style="text-align: center; font-weight: 700">क्या आप ब्रांच तथा गोदाम बदलना चाहते है?
                <asp:CheckBox ID="chkChange" runat="server" AutoPostBack="True" OnCheckedChanged="chkChange_CheckedChanged" />
                                 </td>
                             </tr>

                             <tr id="trBG1" runat="server" visible="false" style="background-color: #FFCC99">
                             <td>District</td>
                                 <td>
                                 <asp:DropDownList ID="ddldistrict" runat="server" Width="150px" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged" AutoPostBack="True" >
                </asp:DropDownList>
                                     
                                     </td>
                                 <td>Issue Centre</td>
                                 <td>
                <asp:DropDownList ID="ddlIssueCentre" runat="server" Width="150px" OnSelectedIndexChanged="ddlIssueCentre_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
                                 </td>
                                 
                             </tr>

                             <tr id="trBG" runat="server" visible="true" style="background-color: #FFCC99">
                                 <td>Branch</td>
                                 <td>
                                     <asp:DropDownList ID="ddlBranch" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                     </asp:DropDownList>

                                 </td>
                                 <td>Godown</td>
                                 <td>
                                     <asp:DropDownList ID="ddlGodown" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">
                                     </asp:DropDownList>

                                 </td>
                             </tr>
                             <tr runat="server" visible="false">
                                 <td style="border-top-color: #CCCCCC; border-top-width: 8px">Arrival Time</td>
                                 <td style="border-top-color: #CCCCCC; border-top-width: 8px">

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

                                 <td style="border-top-color: #CCCCCC; border-top-width: 8px">Arrival Date</td>
                                 <td style="border-top-color: #CCCCCC; border-top-width: 8px">
                                     <asp:TextBox ID="txtIssuedDate" runat="server" Width="128px" ReadOnly="True"></asp:TextBox>
                                     <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtIssuedDate' , 'expiry=true,elapse=-450,restrict=true,close=true')" />

                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtIssuedDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                                 </td>
                             </tr>

                             <tr runat="server" visible="false">
                                 <td>Recd. Qty <asp:Label ID="lblQtls" runat="server" style="font-weight: 700" Text=""></asp:Label>
                                 </td>
                                 <td>
                                     <asp:TextBox ID="txtRecdQty" runat="server" Width="146px" Style="text-align: right" MaxLength="12" AutoComplete="off"
                                         onblur="extractNumber(this,5,true);"
                                         onkeyup="extractNumber(this,5,true);"
                                         onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="NAN" ControlToValidate="txtRecdQty" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,5})?))$"></asp:RegularExpressionValidator>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtRecdQty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

                                 </td>
                                 <td>Recd. No. of Bags</td>
                                 <td>
                                     <asp:TextBox ID="txtRecdBags" runat="server" Width="146px" AutoComplete="off" class="Number" MaxLength="4" Style="text-align: right" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>

                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtRecdBags" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

                                 </td>
                             </tr>

                             <tr runat="server" visible="false">
                                 <td>ट्रक नंबर</td>
                                 <td>
                                     <asp:TextBox ID="txtTCNo" runat="server" Width="146px" AutoComplete="off" Class="TruckNumber" MaxLength="14" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>

                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtTCNo" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

                                 </td>
                                 <td>तौल पर्ची क्रमांक</td>
                                 <td>
                                     <asp:TextBox ID="txtToulReceiptNo" runat="server" Width="146px" AutoComplete="off" Class="TruckNumber" MaxLength="14" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>

                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtToulReceiptNo" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

                                 </td>
                             </tr>
                                <tr id="TrNMN" visible="false" runat="server">
                                <td>
                                DivertedMovementOrderNo
                                </td>
                                <td>
                                <asp:Label ID="lblMovmentNumber" runat="server"></asp:Label>
                                </td>
                                </tr>

                             <tr>
                                 <td colspan="5" style="text-align: center; font-size: large;">
                                     <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                                     <asp:Button ID="btnRecptSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" OnClick="btnRecptSubmit_Click" />

                                     <asp:Button ID="btnPrint" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Print" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" Enabled="False" CausesValidation="False" OnClick="btnPrint_Click" />

                                     <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
                                 </td>
                             </tr>

                             <%--Allow Only Alphabets using class="alphaNumericWithSpecial" --%>
                             <script lang="javascript" src="../PaddyMilling/Scripts/TruckNo.js" type="text/javascript">      </script>

                             <script lang="javascript" src="../PaddyMilling/Scripts/Number.js" type="text/javascript">       </script>

                         </table>
                     </div>

    <input type="hidden" id="div_position" name="div_position" />

</asp:Content>

