<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="DC_MO_DispatchByRack.aspx.cs" Inherits="IssueCenter_DC_MO_DispatchByRack" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>

    <script type="text/javascript" lang="javascript" src="../js/calendarMvmt.js"></script>

    <%--For Calendar Controls--%>
    <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="../js/calendarMvmt.js"></script>


    <%--Allow Only 2 Digit After Decimal--%>
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/Number2D.js"></script>

    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }
    </style>

    <style type="text/css">
        .hiddencol {
            display: none;
        }
    </style>

    <script type="text/javascript">
        window.onload = function () {
            var div = document.getElementById("dvScroll");
            var div_position = document.getElementById("div_position");
            var position = parseInt('<%=Request.Form["div_position"] %>');
            if (isNaN(position)) {
                position = 0;
            }
            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            };
        };
    </script>

    <div id="dvScroll" style="overflow-y: scroll; height: 500px;">
        <table align="center" style="width: 760px; border-style: solid; border-width: 1px; text-align: left;" border="1" cellspacing="0" cellpadding="6">
            <tr>
                <td colspan="6" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600"><strong>Delivery Challan(PDS Movement) Against Transport Order</strong>
                    <input id="hdfGunnyType" type="hidden" runat="server" />
                    <input id="hdfGridIC_ID" type="hidden" runat="server" />
                </td>
            </tr>

            <tr>
                <td rowspan="22"></td>
                <td colspan="4" style="text-align: center; background-color: #CCCCCC">
                    <asp:Label ID="Label2" runat="server" Style="text-align: center; font-weight: 700;" ForeColor="Blue" Visible="False"></asp:Label>
                </td>
                <td rowspan="22"></td>
            </tr>

            <tr>
                <td>परिवहन आदेश क्रमांक</td>
                <td>
                    <asp:DropDownList ID="ddlTONo" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlTONo_SelectedIndexChanged">
                    </asp:DropDownList>

                </td>
                <td>मुख्यालय एम ओ क्रमांक</td>
                <td>
                    <asp:TextBox ID="txtMONo" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtMONo" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>रैक क्रमांक</td>
                <td colspan="3">
                    <asp:TextBox ID="txtRackNo" runat="server" Width="520px" ReadOnly="True" Enabled="False"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>परिवहनकर्ता का नाम</td>
                <td>
                    <asp:TextBox ID="txtTransporterName" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTransporterName" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                </td>
                <td>परिवहन का अंतिम दिनांक</td>
                <td>
                    <asp:TextBox ID="txtTranspEndDate" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTranspEndDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>कमोडिटी</td>
                <td>
                    <asp:TextBox ID="txtCommodity" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCommodity" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                </td>
                <td>प्रदाय केंद्र</td>
                <td>
                    <asp:TextBox ID="txtIC" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtIC" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>जिला से</td>
                <td>
                    <asp:TextBox ID="txtFrmDist" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                </td>
                <td>जिला तक</td>
                <td>
                    <asp:TextBox ID="txtToDist" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                </td>
            </tr>


            <tr>
                <td>रेल हेड से</td>
                <td>
                    <asp:TextBox ID="txtFrmRailHead" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                </td>
                <td>रेल हेड तक</td>
                <td>
                    <asp:TextBox ID="txtToRailHead" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                </td>
            </tr>


            <tr>
                <td colspan="4">
                    <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" ShowFooter="True" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                        OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>

                            <asp:BoundField ReadOnly="true" HeaderText="क्र." HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right"></asp:BoundField>


                            <asp:BoundField DataField="Branch" HeaderText="ब्रांच" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right"></asp:BoundField>


                            <asp:BoundField DataField="Godown" HeaderText="गोदाम" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right"></asp:BoundField>


                            <asp:BoundField DataField="RequiredQuantity" HeaderText="कुल मात्रा(Qtls)" HeaderStyle-HorizontalAlign="Right" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right"></asp:BoundField>


                            <asp:BoundField DataField="RemQty" HeaderText="बची हुई मात्रा(Qtls)" HeaderStyle-HorizontalAlign="Right" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right"></asp:BoundField>


                            <asp:BoundField DataField="Branch" HeaderText="BranchVal" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol"></asp:BoundField>

                            <asp:BoundField DataField="Godown" HeaderText="GodownVal" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol"></asp:BoundField>

                            <asp:BoundField DataField="STO_No" HeaderText="STO_No" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol" />
                            <asp:BoundField DataField="STrans_ID" HeaderText="STrans_ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol" />
                            <asp:BoundField DataField="Issue_Center" HeaderText="IC_ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol" />

                            <asp:TemplateField HeaderText="चुनें" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                <ItemTemplate>
                                    <asp:LinkButton ID="idSelect" runat="server" CausesValidation="false" CommandName="Select" Text="Select"></asp:LinkButton>
                                </ItemTemplate>
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

            <tr>
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

            <tr>
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

            <tr id="trBG" runat="server" visible="false" style="background-color: #FFCC99">
                <td>ब्रांच</td>
                <td>
                    <asp:DropDownList ID="ddlBranch" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                    </asp:DropDownList>

                </td>
                <td>गोदाम</td>
                <td>
                    <asp:DropDownList ID="ddlGodown" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">
                    </asp:DropDownList>

                </td>
            </tr>
            <tr>
                <td style="text-align:left">
                    Stack Number
                </td>
                <td  style="text-align:left">
                    <asp:DropDownList ID="ddlstackNumber" runat="server" Width="150px">
                    </asp:DropDownList>

                </td>
                  <td>Type of Bags</td>
                <td>
                   <%-- <asp:RadioButton ID="rdbSBT" runat="server" GroupName="BagsGroup" Text="SBT" />
                    <asp:RadioButton ID="rdbHDPE" runat="server" GroupName="BagsGroup" Text="HDPE" Style="margin-left: 5px;" />--%>
                    <asp:TextBox ID="txtGunnyType" runat="server" Width="80px" ReadOnly="True" Enabled="False" Visible="False"></asp:TextBox>

                    <asp:DropDownList ID="ddlbagstype" runat="server" Width="150px" >
                        </asp:DropDownList>
                    </td>
            </tr>

            <tr>
                <td>Date of Issue</td>
                <td>
                    <asp:TextBox ID="txtIssuedDate" runat="server" Width="146px" ReadOnly="True"></asp:TextBox>
                    <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtIssuedDate' , 'expiry=true,elapse=-240,restrict=true,instance=single,close=true')" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtIssuedDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" style="font-size: x-small">**</asp:RequiredFieldValidator>
                </td>
                <td>Stock Issued From</td>
                <td>
                    <asp:DropDownList ID="ddlSarrival" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlSarrival_SelectedIndexChanged">
                    </asp:DropDownList>

                </td>
            </tr>

            <tr>
                <td>Balance Qty In Godown</td>
                <td>
                    <asp:TextBox ID="txtBalQtyInGodown" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtBalQtyInGodown" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

                </td>
                <td>Balance Bags In Godown</td>
                <td>
                    <asp:TextBox ID="txtBalBagInGodown" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtBalBagInGodown" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>Issued Qty
                    <asp:Label ID="lblQtls" runat="server" Style="font-weight: 700" Text=""></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtIssuedQty" runat="server" Width="146px" Style="text-align: right" MaxLength="12" AutoComplete="off" AutoPostBack="true" OnTextChanged="txtIssuedQty_TextChanged"
                        onblur="extractNumber(this,5,true);"
                        onkeyup="extractNumber(this,5,true);"
                        onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="NAN" ControlToValidate="txtIssuedQty" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,5})?))$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtIssuedQty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

                </td>
                <td>Issued No. of Bags</td>
                <td>
                    <asp:TextBox ID="txtIssuedBags" runat="server" Width="146px" AutoComplete="off" AutoPostBack="true" OnTextChanged="txtIssuedBags_TextChanged" class="Number" MaxLength="4" Style="text-align: right" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtIssuedBags" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

                </td>
            </tr>

            <tr runat="server">
              
                <td colspan="2" style="text-align:right">Truck Number</td>
                <td colspan="2" style="text-align:left">
                    <asp:TextBox ID="txtTCNo" runat="server" Width="146px" AutoComplete="off" Class="TruckNumber" MaxLength="14" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtTCNo" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

                </td>
            </tr>


            <tr runat="server">
                <td>Dispatch Time</td>
                <td>

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

                <td>Destination Issue Centre</td>
                <td>
                    <asp:DropDownList ID="ddlSendIC" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlSendIC_SelectedIndexChanged">
                    </asp:DropDownList>

                </td>
            </tr>


            <tr>
                <td>Destination Branch</td>
                <td>

                    <asp:DropDownList ID="ddlSendBranch" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlSendBranch_SelectedIndexChanged">
                    </asp:DropDownList>

                </td>

                <td>Destination Godown</td>
                <td>
                    <asp:DropDownList ID="ddlSendGodown" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlSendGodown_SelectedIndexChanged">
                    </asp:DropDownList>

                </td>
            </tr>


            <tr>
                <td>Balance Qty
                    <asp:Label ID="lblQtls0" runat="server" Style="font-weight: 700" Text=""></asp:Label>
                </td>
                <td>

                    <asp:TextBox ID="txtBalQtyInSendIC" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>

                </td>

                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>


            <tr>
                <td colspan="3">प्रेक्षणकर्ता गोदाम से प्राप्तकर्ता गोदाम या रेल हेड के बीच की अनुमानित दुरी <strong>(Km)</strong></td>
                <td>
                    <asp:TextBox ID="txtDistance" runat="server" Width="146px" AutoComplete="off" class="Number" MaxLength="5" Style="text-align: right" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
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

