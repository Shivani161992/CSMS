<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="CSC_ChanaMasoor.aspx.cs" Inherits="IssueCenter_CSC_ChanaMasoor" Title=" " MaintainScrollPositionOnPostback = "true" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>

    <script type="text/javascript" lang="javascript" src="../js/calendarMvmt.js"></script>

    <%--For Calendar Controls--%>
    <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="../js/calendarMvmt.js"></script>

    <%--Allow Only 2 Digit After Decimal--%>
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/Number2D.js"></script>

    <style type="text/css">.
        .hiddencol {
            display: none;
        }
    </style>

    <script type="text/javascript">
        $(function() {
            $("[id*=dgridchallan] td").hover(function() {
                $("td", $(this).closest("tr")).addClass("hover_row");
            }, function() {
                $("td", $(this).closest("tr")).removeClass("hover_row");
            });
        });
    </script>

    <script type="text/javascript" language="javascript">
        function validate() {
            if (Page_ClientValidate())
                return confirm('क्या आपने सही जानकारी भरी हैं? यदि हाँ, तो OK पर क्लिक करे अन्यथा Cancel पर क्लिक करे तथा सही जानकारी भरें|');
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function() {
            $("input").focus(function() {
                $(this).css("background-color", "#cccccc");
            });
            $("input").blur(function() {
                $(this).css("background-color", "#ffffff");
            });
        });
    </script>

    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
            font-size: small;
        }

        .Qtls {
            font-size: small;
            color: #FF0000;
        }

        .hover_row {
            background-color: #A1DCF2;
        }
        .style1
        {
            color: #000099;
            text-align: center;
            }
        .style3
        {
            height: 6px;
        }
        .style4
        {
            color: #993300;
        }
        .style5
        {
            height: 36px;
        }
        .style7
        {
        }
        .style8
        {
            color: #000099;
            text-align: center;
            width: 418px;
            height: 18px;
        }
        .style9
        {
            height: 18px;
        }
        .style10
        {
            width: 249px;
            height: 18px;
        }
        .style11
        {
            color: #000099;
            text-align: left;
            }
            
            .style110
        {
            color: Red;
            text-align: center;
            }
        .style111
        {
            color: #000099;
            text-align: center;
            font-weight: bold;
        }
        .style112
        {
            height: 35px;
        }
        .style113
        {
            color: #000099;
            text-align: left;
            width: 418px;
        }
        .style114
        {
            color: Red;
            text-align: center;
            width: 418px;
        }
        .style115
        {
            width: 418px;
        }
        </style>
          
   
    <table align="center" 
        style="border-style: solid; border-width: 1px; text-align: left; font-size: medium; width: 1050px" 
        border="1" cellspacing="0" cellpadding="2">
        <tr>
            <td colspan="6" style="text-align: center; font-size: large; text-decoration: underline; color: Blue">
                <span class="style4"><strong>Dispatch From Purchase Center (Gram,Mustard and 
                Lentil Procurement 2018)</strong></span>
                <input id="hdfCSMS_Comid" type="hidden" runat="server" />
                <input id="hdfgdntype" type="hidden" runat="server" />
                <input id="hdfWeighbridgeID" type="hidden" runat="server" />
            </td>
        </tr>

        <tr>
            <td rowspan="39">&nbsp;</td>
            <td colspan="4" style="text-align: center; background-color:DarkSalmon">
                <asp:Label ID="Label2" runat="server" Style="text-align: center; font-weight: 700;" ForeColor="Blue" Visible="False"></asp:Label>

                        


            </td>
            <td rowspan="39"></td>
        </tr>

        <tr>
            <td class="style113">Issue Center</td>
            <td>
                <asp:TextBox ID="txtissue" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>

            </td>
            <td class="style7">Crop Year</td>
            <td>
                <asp:TextBox ID="txtYear" runat="server" Width="137px" ReadOnly="True" Enabled="False">2018-2019</asp:TextBox>

            </td>
        </tr>




        <tr>
            <td class="style113">Commodity</td>
            <td>
                <asp:DropDownList ID="ddlcomdty" runat="server" Width="141px"  AutoPostBack="true" OnSelectedIndexChanged="ddlcomdty_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
            <td class="style7">Sending District</td>
            <td>
                <asp:DropDownList ID="ddldistpdy" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddldistpdy_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
        </tr>

        <tr>
            <td class="style113">Purchase Center</td>
            <td colspan="3">
                <asp:DropDownList ID="ddluparjan" runat="server" Width="572px" AutoPostBack="True" OnSelectedIndexChanged="ddluparjan_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:Label ID="lbl_KP" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>


        <tr>
            <td colspan="4" style="text-align: center; background-color: Aqua; font-size: small; font-weight: 700;">कृपया सेक्टर की खरीद केंद्र से मेपिंग एवं 
             परिवहनकर्ता मास्टर की एंट्री ,जिला कार्यालय द्वारा अनिवार्य रूप से करा ले , बिना  इसके प्राप्ति नहीं की जा सकेगी|
            </td>
        </tr>


        <tr>
            <td style="font-size: small" class="style113">खरीदी केंद्र का सम्बंधित सेक्टर</td>
            <td colspan="3">
                <asp:DropDownList ID="ddlsector" runat="server" Width="249px" Enabled="false" 
                    Height="44px">
                </asp:DropDownList></td>
        </tr>


        <tr>
            <td colspan="4">
                <center>
                    <asp:Panel ID="pnlgrd" runat="server" ScrollBars="Vertical" Visible="false" Width="1100px" Height="200px">
<asp:GridView ID="dgridchallan" runat="server" AutoGenerateColumns="False"
                            OnSelectedIndexChanged="dgridchallan_SelectedIndexChanged" PageSize="1" 
                            PagerSettings-Visible="true" ShowFooter="True" CellPadding="1"
                            ForeColor="#333333" GridLines="None" Width="1100px" CssClass="ButtonClass" 
                            onrowdatabound="dgridchallan_RowDataBound">
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" 
                                Height="15px" />
                            <Columns>
                                <asp:CommandField HeaderText="Action" ShowSelectButton="True" 
                                    SelectText="Click to Receive" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle ForeColor="Red" />
                                </asp:CommandField>
                                <asp:BoundField DataField="DateOfIssue" HeaderText="Date Of Dispatch" 
                                    SortExpression="DateOfIssue">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="IssueID" HeaderText="IssueID" 
                                    SortExpression="IssueID">
                                    <ItemStyle HorizontalAlign="Center" ForeColor="Maroon" />
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TruckChalanNo" HeaderText="TC No." ReadOnly="True" 
                                    SortExpression="TruckChalanNo">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TruckNo" HeaderText="Truck No." ReadOnly="True"
                                    SortExpression="TruckNo">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Bags" HeaderText="Bags" ReadOnly="True" 
                                    SortExpression="Bags">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="QtyTransffer" HeaderText="Qty" ReadOnly="True"
                                    SortExpression="QtyTransffer">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Transporter_Name" HeaderText="Transport Name" 
                                    ReadOnly="True" SortExpression="Transporter_Name">
                                     <ItemStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                                    <HeaderStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TransporterId" HeaderText="TransporterId" 
                                    ReadOnly="True" SortExpression="TransporterId">
                                    <ItemStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                                    <HeaderStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="JutBag" HeaderText="New Jute" ReadOnly="True" 
                                    SortExpression="JutBag">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Jut_OldBag" HeaderText="Jute Old" ReadOnly="True" 
                                    SortExpression="Jut_OldBag">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="HDPEBag" HeaderText="PP Bag" ReadOnly="True" 
                                    SortExpression="HDPEBag">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="GodownTypeId" HeaderText="GdnType" ReadOnly="True" 
                                    SortExpression="GodownTypeId">
                                     <ItemStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                                    <HeaderStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Weighbridge_ID" HeaderText="WebridgeID" 
                                    ReadOnly="True" SortExpression="Weighbridge_ID" ItemStyle-CssClass="hiddencol" 
                                    HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol">
                                     <ItemStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                                    <HeaderStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="GodownNumber" HeaderText="Godown Id" ReadOnly="True" 
                                    SortExpression="GodownNumber" ItemStyle-CssClass="hiddencol" 
                                    HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol">
                                      <ItemStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                                    <HeaderStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="BranchID" HeaderText="Branch Id" ReadOnly="True" 
                                    SortExpression="BranchID" ItemStyle-CssClass="hiddencol" 
                                    HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol">
                                     <ItemStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                                    <HeaderStyle HorizontalAlign="Center" Width="0px" Font-Size="0px" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="U_Status" HeaderText="U_Status" ReadOnly="True" 
                                    SortExpression="U_Status" ItemStyle-CssClass="hiddencol" 
                                    HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="G_Status" HeaderText="GS_Status" ReadOnly="True" 
                                    SortExpression="G_Status" ItemStyle-CssClass="hiddencol" 
                                    HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                            </Columns>
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <SelectedRowStyle BackColor="#E2DED6" ForeColor="Black" Font-Bold="True" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingRowStyle BackColor="#cccccc" ForeColor="#284775" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <EditRowStyle BackColor="#999999" />
                        </asp:GridView>

                    </asp:Panel>
                </center>
            </td>
        </tr>


        <tr>
            <td colspan="4" style="text-align: center; background-color:CadetBlue ; font-size: small; font-weight: 700;">Sending Details</td>
        </tr>

        <tr>
            <td class="style113">Issue_ID</td>
            <td>
                <asp:TextBox ID="txtissueId" runat="server" Width="180px" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txtissueId" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
            <td class="style7">Date of Dispatch</td>
            <td>
                <asp:TextBox ID="DaintyDate1P" runat="server" Width="137px" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="DaintyDate1P" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td class="style113">Send Truck Challan No.</td>
            <td>
                <asp:TextBox ID="txtchlnno" runat="server" Width="137px" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="txtchlnno" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
            <td class="style7">Sending Truck No.</td>
            <td>
                <asp:TextBox ID="txttrucknopady" runat="server" Width="137px" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="txttrucknopady" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td class="style113">Transporter Name</td>
            <td colspan="3">
                <asp:DropDownList ID="ddlpdyTransporter" runat="server" Width="572px" Enabled="false">
                </asp:DropDownList></td>
        </tr>

        <tr>
            <td class="style113">NO.of Bags Dispatched</td>
            <td>
                <asp:TextBox ID="txtissubag" runat="server" Width="137px" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="txtissubag" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
            <td class="style7">Net Qty. Dispatched<b><span class="Qtls"> (Qtls)</span></b></td>
            <td>
                <asp:TextBox ID="txtissueqty" runat="server" Width="137px" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="txtissueqty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td colspan="4" style="text-align: center; background-color: Burlywood; font-size: small; font-weight: 700;">नीचे में केवल सम्बंधित ब्रांच एवं गोदाम का नाम 
                ध्यानपूर्वक देख एंट्री करें, गलत गोदाम दिखने की स्थिति में कलेक्टर लोगिन से 
                गोदाम बदलवाये |
            </td>
        </tr>

        <tr>
            <td class="style113"><strong>Branch</strong></td>
            <td>
                <asp:DropDownList ID="ddlbranchwlc" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlbranchwlc_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
            <td class="style7"><strong>Godown</strong></td>
            <td>
                <asp:DropDownList ID="ddlgodown" runat="server" Width="216px" AutoPostBack = "true" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged" Height="26px">
                </asp:DropDownList>

            </td>
        </tr>

        <tr>
            <td class="style113">Select Stake Name</td>
            <td>
                <asp:DropDownList ID="ddl_StakeNumber" runat="server" Width="200px" Height="26px">
                </asp:DropDownList>
            </td>
            <td class="style7">&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>

        <tr>
            <td class="style113"> <asp:Label ID = "Hired" runat = "server" Visible = false Text = "Hired Type"></asp:Label> </td>
            <td>
                <asp:TextBox ID="txthhty" runat="server" Width="137px" ReadOnly="True" 
                    Enabled="False" Style="text-align: right" Visible="False"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" 
                    ControlToValidate="txthhty" ErrorMessage="RequiredFieldValidator" 
                    SetFocusOnError="True" Display="Dynamic" Visible="False">**</asp:RequiredFieldValidator>

            </td>
            <td class="style7"> <asp:Label ID = "MaxCapacity" runat = "server" Visible = false Text = "Max Capacity"></asp:Label> </td>
            <td>
                <asp:TextBox ID="txtmaxcap" runat="server" Width="137px" ReadOnly="True" 
                    Enabled="False" Style="text-align: right" Visible="False"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" 
                    ControlToValidate="txtmaxcap" ErrorMessage="RequiredFieldValidator" 
                    SetFocusOnError="True" Display="Dynamic" Visible="False">**</asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td class="style8"> <asp:Label ID = "CurrentCap" runat = "server" Visible = false Text = "Current Cap."></asp:Label>   </td>
            <td class="style9">
                <asp:TextBox ID="txtcurntcap" runat="server" Width="137px" ReadOnly="True" 
                    Enabled="False" Style="text-align: right" Visible="False"></asp:TextBox>

            </td>
            <td class="style10"> <asp:Label ID = "AvailableCap" runat = "server" Visible = false Text = "Available Cap."></asp:Label>  </td>
            <td class="style9">
                <asp:TextBox ID="txtavalcap" runat="server" Width="137px" ReadOnly="True" 
                    Enabled="False" Style="text-align: right" Visible="False"></asp:TextBox>

            </td>
        </tr>

        <tr>
            <td colspan="4" style="text-align: center; background-color: Bisque; font-size: small; font-weight: 700;">Receipt Details
            </td>
        </tr>

        <tr>
            <td class="style113">Recd. Bags Jute <b><span class="Qtls">(New)</span></b></td>
            <td>
                <asp:TextBox ID="txt_recJutNew" runat="server" Width="137px" AutoComplete="off" class="Number" MaxLength="5" Style="text-align: right" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txt_recJutNew" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
            <td class="style11">Recd. Bags Jute  <b><span class="Qtls">(Old)</span></b></td>
            <td>
                <asp:TextBox ID="txt_recJutOld" runat="server" Width="137px" AutoComplete="off" 
                    class="Number" MaxLength="5" Style="text-align: right" onfocus="this.select();" 
                    onmouseup="return false;" ReadOnly="True"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_recJutOld" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td class="style113">Recd. Bags P.P.</td>
            <td>
                <asp:TextBox ID="txt_recPP" runat="server" Width="137px" AutoComplete="off" class="Number" MaxLength="5" Style="text-align: right" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_recPP" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
            <td class="style7">&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>



        <tr>
            <td class="style113" >Recd Bags (A_twill) केवल सरसों के लिए</td>
            <td class="style7">
                <asp:TextBox ID="txt_recATwill" runat="server" Width="137px" AutoComplete="off" 
                    class="Number" MaxLength="5" Style="text-align: right" onfocus="this.select();" 
                    onmouseup="return false;" Height="24px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" 
                    ControlToValidate="txt_recPP" ErrorMessage="RequiredFieldValidator" 
                    SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                                                </td>
            <td>
                &nbsp;</td>
        </tr>



        <tr>
            <td class="style113">Recd. Truck No.</td>
            <td>
                <asp:TextBox ID="txtRec_TruckNumber" runat="server" AutoComplete="off" Class="TruckNumber" MaxLength="14" onfocus="this.select();" onmouseup="return false;" Width="137px" Style="text-align: right"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="txtRec_TruckNumber" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
            <td class="style7">गोदाम पर प्राप्ति दिनाँक</td>
            <td>
                <asp:TextBox ID="DaintyDate3" runat="server" Width="137px" ReadOnly="True" Style="text-align: right"></asp:TextBox>
                <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_DaintyDate3' , 'expiry=true,elapse=-450,restrict=true,close=true')" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="DaintyDate3" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                </td>
        </tr>



        <tr>
            <td colspan="4" style="text-align: center; background-color: Bisque; font-size: small; font-weight: 700;">CSMS में भरे गए परिवहनकर्ता का नाम जो की बिल के दौरान उपयोग होगा</td>
        </tr>



        <tr>
            <td class="style1" colspan="2">Transporter Name CSMS मास्टर के अनुसार</td>
            <td class="style7" colspan="2">
                <asp:DropDownList ID="ddlcsms_transp" runat="server" Width="328px" 
                    Height="19px" Enabled="False">
                </asp:DropDownList>
            </td>
        </tr>



        <tr>
             <td colspan="4" style="text-align: center; background-color: Bisque; font-size: small; font-weight: 700;">तौल कांटे का नाम नही आने पर जिला कार्यालय से तौल  कांटे का नाम जोड़ें             </td>
        </tr>



        <tr id = "TD_hideDM" runat = "server">
            <td class="style114">धर्मकांटा का नाम</td>
            <td>
                <asp:DropDownList ID="ddl_WBridge" runat="server" Width="220px" Height="28px">
                </asp:DropDownList>

            </td>
            <td class="style110">खाली गाडी का कुल वजन (Qtls में)</td>
            <td>
                <asp:TextBox ID="Weighbridge_empty" runat="server" Width="137px" 
                    Style="text-align: right" MaxLength="13" AutoComplete="off"
                    onblur="extractNumber(this,5,true);"
                    onkeyup="extractNumber(this,5,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" 
                    onfocus="this.select();" onmouseup="return false;"></asp:TextBox>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                    ErrorMessage="NAN" ControlToValidate="Weighbridge_empty" Display="Dynamic" 
                    ValidationExpression="((\d+)((\.\d{1,5})?))$"></asp:RegularExpressionValidator>
                </td>
        </tr>



        <tr id = "TD_hideRSt" runat = "server">
            <td class="style114">R.S.T . No.</td>
            <td>
                <asp:TextBox ID="Weighbridge_TaulParchi" runat="server" MaxLength="14" Class="TruckNumber" AutoComplete="off" Width="137px" Style="text-align: right" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>

            </td>
            <td class="style110">भरी गाडी का कुल वजन (Qtls में) </td>
            <td>
                <asp:TextBox ID="Weighbridge_Qty" runat="server" Width="137px" Style="text-align: right" MaxLength="13" AutoComplete="off"
                    onblur="extractNumber(this,5,true);"
                    onkeyup="extractNumber(this,5,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="NAN" ControlToValidate="Weighbridge_Qty" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,5})?))$"></asp:RegularExpressionValidator>
                </td>
        </tr>



        <tr>
            <td class="style115" ></td>
            <td >

            </td>
            <td >
            </td>
            <td >
             </td>
        </tr>



        <tr>
            <td colspan="4" align = "center" class="style112" style="color: #CC3300" >
                <asp:Button ID="btn_calc" runat="server" 
                    Text="बोरे का वजन, कुल वजन एवं शुद्ध वजन के लिए यहाँ क्लिक करें| " 
                    Width="900px" Height = "33px"
                    
                    style="font-weight: 700; color: #000099; background-color: #FFFFCC; font-size: medium;" 
                    onclick="btn_calc_Click" />
            </td>
        </tr>



        <tr>
            <td class="style115" >&nbsp;</td>
            <td >

                &nbsp;</td>
            <td >
                &nbsp;</td>
            <td >
                &nbsp;</td>
        </tr>



        <tr id = "TD_hideforKP" runat = "server">
            <td >बारदाने का वजन (PP) 
                <asp:Label ID="Label3" 
                    runat="server"></asp:Label>&nbsp;Gram</td>
            <td>
                <asp:Label ID="lbl_ppweight" runat="server">0</asp:Label>
           
            &nbsp;Qtls</td>
            <td>
                                बारदाने का वजन (Jute New ) 
                <asp:Label ID="Label7" 
                    runat="server"></asp:Label>
                
                  &nbsp;Gram</td>
                  
                  <td>
                <asp:Label ID="lbl_JuteNewWeight" runat="server">0</asp:Label>
                
                  &nbsp;Qtls</td>
        </tr>



        <tr id = "TD_hideKP" runat = "server">
            <td class="style113">बारदाने का वजन (Jute Old ) 
                <asp:Label ID="Label5" 
                    runat="server"></asp:Label>&nbsp;Gram</td>
            <td>
                <asp:Label ID="lbl_JuteOldWeight" runat="server">0</asp:Label>
                                                &nbsp; Qtls</td>
            <td class="style11">बारदाने का वजन (A_twill) 
                <asp:Label ID="Label6" 
                    runat="server"></asp:Label>&nbsp;Gram</td>
            <td>
                <asp:Label ID="lbl_ATwillWeight" runat="server">0</asp:Label>
                                                &nbsp;Qtls</td>
        </tr>



        <tr>
            <td colspan="4" style="text-align: center; background-color:Beige; font-size: small; font-weight: 700;">
                &nbsp;प्राप्त मात्रा की जानकारी &nbsp;प्राप्त मात्रा की जानकारी</td>
        </tr>

        <tr style="font-size: small">
            <td class="style113"><b><span class="Qtls"> Gross Weight&nbsp; (Qtls))</span></b></td>
            <td>
                <asp:TextBox ID="txt_GrossWeight" runat="server" Width="137px" 
                    Style="text-align: right" MaxLength="13" AutoComplete="off"
                    onblur="extractNumber(this,5,true);"
                    onkeyup="extractNumber(this,5,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" 
                    onfocus="this.select();" onmouseup="return false;" ReadOnly="True"></asp:TextBox>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="NAN" ControlToValidate="txt_GrossWeight" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,5})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txt_GrossWeight" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
            <td class="style7">&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>

        <tr style="font-size: small">
            <td class="style113">&nbsp;</td>
            <td>
                &nbsp;</td>
            <td class="style7">Net Weight (Qntls)</td>
            <td>
                <asp:TextBox ID="txt_NetWeight" runat="server" Width="137px" 
                    Style="text-align: right" MaxLength="13" AutoComplete="off" Text = "0" Enabled = "false"
                    onblur="extractNumber(this,5,true);"
                    onkeyup="extractNumber(this,5,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" 
                    onfocus="this.select();" onmouseup="return false;"></asp:TextBox>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
                    ErrorMessage="NAN" ControlToValidate="txt_NetWeight" Display="Dynamic" 
                    ValidationExpression="((\d+)((\.\d{1,5})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" 
                    ControlToValidate="txt_NetWeight" ErrorMessage="RequiredFieldValidator" 
                    SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr style="font-size: small">
            <td class="style111" colspan="4">बोरो पर रिमार्क की जानकारी</td>
        </tr>



        <tr style="font-size: small">
            <td class="style113">खराब सिलाई वाले बोरो की संख्या</td>
            <td>
                <asp:TextBox ID="txtbadStiching" runat="server" Width="137px" AutoComplete="off" class="Number" MaxLength="5" Style="text-align: right" onfocus="this.select();" onmouseup="return false;">0</asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="txtbadStiching" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
            <td class="style7">खराब छाप वाले बोरो की संख्या</td>
            <td>
                <asp:TextBox ID="txtBadStelcile" runat="server" Width="137px" AutoComplete="off" class="Number" MaxLength="5" Style="text-align: right" onfocus="this.select();" onmouseup="return false;">0</asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="txtBadStelcile" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>



        <tr style="font-size: small">
            <td class="style113">टेग निर्धारित प्रारूप में एवं पठनीय कितने में नही लगा है </td>
            <td>
                <asp:TextBox ID="txtbad_Tagread" runat="server" Width="137px" 
                    AutoComplete="off" class="Number" MaxLength="5" Style="text-align: right" 
                    onfocus="this.select();" onmouseup="return false;">0</asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" 
                    ControlToValidate="txtbadStiching" ErrorMessage="RequiredFieldValidator" 
                    SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
            <td class="style7">सही कलर कोड का पालन कितने बोरो में नही किया गया</td>
            <td>
                <asp:TextBox ID="txt_colorcode" runat="server" Width="137px" 
                    AutoComplete="off" class="Number" MaxLength="5" Style="text-align: right" 
                    onfocus="this.select();" onmouseup="return false;">0</asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" 
                    ControlToValidate="txtbadStiching" ErrorMessage="RequiredFieldValidator" 
                    SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>



        <tr>
            <td colspan="4" style="text-align: left;" 
                class="style3"> 
                </td>
        </tr>

        <tr>
            <td colspan="6" style="text-align: center; font-size: large;" class="style5">
                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                <asp:Button ID="btnRecptSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" OnClientClick="return validate();" OnClick="btnRecptSubmit_Click" />

                <asp:Button ID="btnPrint" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Print" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" Enabled="False" CausesValidation="False" OnClick="btnPrint_Click1" />

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
            </td>
        </tr>



        <%--Allow Only Alphabets using class="alphaNumericWithSpecial" --%>
        <script lang="javascript" src="../PaddyMilling/Scripts/TruckNo.js" type="text/javascript">      </script>
        <script lang="javascript" src="../PaddyMilling/Scripts/Number.js" type="text/javascript">       </script>
        <script lang="javascript" src="../PaddyMilling/Scripts/Alphabets.js" type="text/javascript">      </script>

        <tr>
            <td colspan="6" style="text-align: center; font-size: large;">
            
                &nbsp;</td>
        </tr>



        </table>

</asp:Content>

