<%@ Page Language="C#" MasterPageFile="~/MasterPage/AdminLogin.master" AutoEventWireup="true" CodeFile="Edit_TransportOrder.aspx.cs" Inherits="Admin_Edit_TransportOrder" Title="Edit Transport Order " %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <table cellpadding ="0" cellspacing ="0" border ="0" style="width: 622px">
                    <tr>
                        <td  style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; height: 21px;">
                            &nbsp;&nbsp;
                        </td>
                        <td  style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; color: navy; height: 21px;" align="center" colspan="4">
                            <strong>Edit Transport Order Details</strong>
                        </td>
                        <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; height: 21px;">
                            &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                        </td>
                    </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; width: 90px; background-color: thistle;">
             District Logged in</td>
         <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; width: 90px; background-color: thistle;">
             <asp:TextBox ID="txtdistrict" runat="server" Width="100px"></asp:TextBox></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; width: 79px; background-color: thistle;" align="left">
             &nbsp; &nbsp;&nbsp;Month
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; width: 90px; background-color: thistle;">
             <asp:DropDownList ID="ddl_allot_month" runat="server" Width="121px" AutoPostBack="True" OnSelectedIndexChanged="ddl_allot_month_SelectedIndexChanged">
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
                            </asp:DropDownList>         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; width: 90px; background-color: thistle;">
             &nbsp; &nbsp;Year&nbsp;
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; width: 90px; background-color: thistle;">
             <asp:DropDownList ID="ddd_allot_year" runat="server" OnSelectedIndexChanged="ddd_allot_year_SelectedIndexChanged" Width="116px">
                                </asp:DropDownList>
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
             RO No.</td>
         <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: thistle">
             <asp:TextBox ID="txtrono" runat="server" MaxLength="12" Width="99px" Font-Italic="True" ForeColor="Navy" ReadOnly="True" OnTextChanged="txtrono_TextChanged"></asp:TextBox></td>
         <td align="left" class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; width: 79px; border-bottom: 1px solid; background-color: thistle">
             R.O Validity</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
             <asp:TextBox ID="txtrodate" runat="server" Width="115px"></asp:TextBox></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
             RO Qty.</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
             <asp:TextBox ID="txtroqty" runat="server" Width="110px"></asp:TextBox></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: thistle">
         </td>
         <td align="left" class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; width: 79px; border-bottom: 1px solid; background-color: thistle">
             T.O. No.</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
                                            <asp:TextBox ID="txttono" runat="server" Width="115px" MaxLength="12" Font-Italic="True" ForeColor="Navy"></asp:TextBox></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
             T.O. Qty.</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
             <asp:TextBox ID="txttoqty" runat="server" MaxLength="12" Width="110px" Font-Italic="True" ForeColor="Navy"></asp:TextBox></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
         </td>
         <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: thistle">
             <asp:Label ID="lblcomdty" runat="server" Visible="False" Width="60px"></asp:Label></td>
         <td align="left" class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; width: 79px; border-bottom: 1px solid; background-color: thistle">
             Lifted Qty.</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
             <asp:TextBox ID="txtcumlqty" runat="server" MaxLength="12" Width="116px" Font-Italic="True" ForeColor="Navy"></asp:TextBox></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
             Pending Qty.</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
             <asp:TextBox ID="txtbalqty" runat="server" MaxLength="12" Width="109px" Font-Italic="True" ForeColor="Navy"></asp:TextBox></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
         </td>
         <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: thistle">
             <asp:Label ID="lblsch" runat="server" Visible="False" Width="60px"></asp:Label></td>
         <td align="left" class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; width: 79px; border-bottom: 1px solid; background-color: thistle">
             Commodity</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
             <asp:TextBox ID="txtcommodity" runat="server" Width="116px"></asp:TextBox></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
             Scheme</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
             <asp:TextBox ID="txtscheme" runat="server" Width="109px"></asp:TextBox></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
         </td>
         <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: thistle" align="left">
             <asp:Label ID="lblupdqty" runat="server" Font-Size="11pt" Text="Update Status"></asp:Label></td>
         <td align="left" class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; width: 79px; border-bottom: 1px solid; background-color: thistle">
             <asp:TextBox ID="txtuqty" runat="server" Width="107px"></asp:TextBox></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
             </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
             <asp:Button ID="btnEdit" runat="server" Text="Edit More Details" Width="119px" OnClick="btnEdit_Click" />
             <asp:Button ID="btnhide" runat="server" OnClick="btnhide_Click" Text="Hide Details"
                 Visible="False" Width="118px" /></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: thistle">
         </td>
         <td align="left" class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; width: 79px; border-bottom: 1px solid; background-color: thistle">
             <asp:TextBox ID="txtgliftqty" runat="server"></asp:TextBox></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
             <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Get Lifted Qty"
                 Width="171px" /></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
         </td>
         <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: thistle">
         </td>
         <td align="left" class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; width: 79px; border-bottom: 1px solid; background-color: thistle">
             <asp:TextBox ID="txtrobalance" runat="server"></asp:TextBox></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
             <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Update RO Balance" /></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
             <asp:TextBox ID="TextBox1" runat="server" Width="90px"></asp:TextBox></td>
         <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: thistle">
             <asp:TextBox ID="TextBox2" runat="server" Width="107px"></asp:TextBox></td>
         <td align="left" class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; width: 79px; border-bottom: 1px solid; background-color: thistle">
             <asp:TextBox ID="txtallotlift" runat="server"></asp:TextBox></td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
             <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Get TO Allot Lift" Width="169px" /></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
             Cumulative</td>
         <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: thistle">
             Pending&nbsp; Qty</td>
         <td align="left" class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; width: 79px; border-bottom: 1px solid; background-color: thistle">
             Lifted Qty</td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
             <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Update To Allot Lift"
                 Width="171px" /></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
         </td>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             width: 90px; border-bottom: 1px solid; background-color: thistle">
         </td>
     </tr>
     <tr>
         <td align="left" class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; border-bottom: 1px solid; background-color: thistle; width: 90px;">
                                                Transporter Name</td>
         <td class="tdmarginro" colspan="2" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; border-bottom: 1px solid; background-color: thistle; width: 90px;">
                                                <asp:DropDownList ID="ddltransport" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddltransport_SelectedIndexChanged" Width="196px">
                                                </asp:DropDownList></td>
         <td align="left" class="tdmarginro" colspan="2" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; border-bottom: 1px solid; background-color: thistle; width: 90px;">
             <asp:TextBox ID="txttid" runat="server" MaxLength="12" Width="90px" Visible="False"></asp:TextBox></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: thistle; width: 90px;">
             </td>
     </tr>
     <tr>
         <td align="left" class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: lightblue">
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: lightblue">
         </td>
         <td colspan="3" style="font-weight: bold; font-style: italic; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: lightblue;">
             <asp:Label ID="lbldesting" runat="server" Text="Destination    Information" Visible="False"></asp:Label></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: lightblue">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: lightblue;" align="left">
             </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: lightblue;">
             &nbsp;&nbsp;
             </td>
         <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: lightblue;" align="center" colspan="2">
             <strong>
                 <asp:Label ID="lbltoqty" runat="server" Visible="False"></asp:Label></strong></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: lightblue;">
             &nbsp;&nbsp;
             </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: lightblue;">
             &nbsp;&nbsp;
                                            </td>
     </tr>
     <tr>
         <td align="left"  colspan="6" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; border-bottom: 1px solid; background-color: lightblue">
             <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="#C00000"
                 Visible="False"></asp:Label></td>
     </tr>
     <tr>
         <td align="left" class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; border-bottom: 1px solid; background-color: lightblue">
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: lightblue">
         </td>
         <td align="left" class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid;
             font-weight: bold; border-left: 1px solid; border-bottom: 1px solid; font-style: italic;
             background-color: lightblue">
             <asp:Label ID="lblfromfci" runat="server" Text="From FCI " Visible="False"></asp:Label></td>
         <td align="left" class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; border-bottom: 1px solid; background-color: lightblue">
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: lightblue">
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: lightblue">
         </td>
     </tr>
     <tr>
         <td align="left" class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; border-bottom: 1px solid; background-color: lightblue">
             <asp:Label ID="lbldepotype" runat="server" Text="Depot Type" Visible="False"></asp:Label></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: lightblue">
             <asp:DropDownList ID="ddldepottype" runat="server" Width="109px" Visible="False">
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
                 <asp:ListItem Value="02">OWNED</asp:ListItem>
                 <asp:ListItem Value="03">CWC</asp:ListItem>
                 <asp:ListItem Value="04">SWC</asp:ListItem>
                 <asp:ListItem Value="05">PRIVATE</asp:ListItem>
                 <asp:ListItem Value="06">Hired(Private party)</asp:ListItem>
             </asp:DropDownList></td>
         <td align="left" class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; border-bottom: 1px solid; background-color: lightblue">
             <asp:Label ID="lblfcireg" runat="server" Text="FCI Region" Visible="False"></asp:Label></td>
         <td align="left" class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; border-bottom: 1px solid; background-color: lightblue">
             <asp:DropDownList ID="ddlfcidist" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfcidist_SelectedIndexChanged"
                 Width="110px" Visible="False">
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: lightblue">
             <asp:Label ID="lbldisdepot" runat="server" Text="Dispatch Depot:" Visible="False"
                 Width="84px"></asp:Label></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: lightblue">
             <asp:DropDownList ID="ddlfcidepo" runat="server" OnSelectedIndexChanged="ddlfcidepo_SelectedIndexChanged"
                 Width="106px" Visible="False">
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td align="left" class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; border-bottom: 1px solid; background-color: lightblue">
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: lightblue">
         </td>
         <td align="left"  style="border-right: 1px solid; border-top: 1px solid;
             font-weight: bold; border-left: 1px solid; border-bottom: 1px solid; font-style: italic;
             background-color: lightblue" colspan="2">
             <asp:Label ID="lbltoissue" runat="server" Text="To MPSCSC Issue Center" Visible="False"></asp:Label></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: lightblue">
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: lightblue">
         </td>
     </tr>
     <tr>
         <td align="left" class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; border-bottom: 1px solid; background-color: lightblue">
             <asp:Label ID="lblqty" runat="server" Text="Quantity (Qtl.kgGms)" Visible="False"></asp:Label></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: lightblue">
             <asp:TextBox ID="txtsendqty" runat="server" Width="89px" MaxLength="13" Visible="False"></asp:TextBox></td>
         <td align="left" class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; border-bottom: 1px solid; background-color: lightblue">
             <asp:Label ID="lbldistrict" runat="server" Text="District" Visible="False"></asp:Label></td>
         <td align="left" class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; border-bottom: 1px solid; background-color: lightblue">
             <asp:DropDownList ID="ddldistrict" runat="server" Width="139px" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged"
                                                AutoPostBack="True" Visible="False">
             </asp:DropDownList></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: lightblue">
             <asp:Label ID="lblissue" runat="server" Text="Issue Center" Visible="False"></asp:Label></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: lightblue">
             <asp:DropDownList ID="ddlissue" runat="server" Width="110px" Visible="False" >
             </asp:DropDownList></td>
     </tr>
     <tr>
         <td align="left" class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; border-bottom: 1px solid; background-color: lightblue">
         </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: lightblue">
         </td>
         <td align="left" class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; border-bottom: 1px solid; background-color: lightblue">
         </td>
         <td align="left" class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; border-bottom: 1px solid; background-color: lightblue">
             </td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: lightblue">
             <asp:Button ID="addmore" runat="server" Text="Add More" Width="84px" OnClick="addmore_Click" Visible="False" /></td>
         <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: lightblue">
         </td>
     </tr>
     <tr>
         <td align="left" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; border-bottom: 1px solid; background-color: lightblue;" colspan="6">
              <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" 
          AllowPaging="True" PageSize="5" PagerSettings-Visible ="false" CellPadding="2" ForeColor="Black" GridLines="None" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" Width="590px" Visible="False"  >
        <HeaderStyle  CssClass="gridheader" BackColor="Tan" Font-Bold="True"   />
        <Columns>
            <asp:CommandField ShowSelectButton="True" HeaderText="Action" SelectText="Delete" >
                <ItemStyle CssClass="griditem" />
            </asp:CommandField>
                       
            <asp:BoundField DataField="RO_No" HeaderText="R.O.  No." ReadOnly="True" SortExpression="RO_No" >
                <ItemStyle CssClass="griditem" />
            </asp:BoundField>
            <asp:BoundField DataField="TO_NO" HeaderText="T. O. No." ReadOnly="True" SortExpression="TO_NO" >
                <ItemStyle CssClass="griditem" />
            </asp:BoundField>
            <asp:BoundField DataField="Quantity" HeaderText="T.O. Qty." SortExpression="Quantity" />
            <asp:BoundField DataField="DisName" HeaderText="District" />
            <asp:BoundField DataField="IssueName" HeaderText="Depot" />
            <asp:BoundField DataField="District" HeaderText="Did" SortExpression="District" />
            
            <asp:BoundField DataField="IssueCenter" HeaderText="ICid" ReadOnly="True" SortExpression="IssueCenter" >
                <ItemStyle CssClass="griditem" />
            </asp:BoundField>
           
        </Columns>
         <PagerSettings Visible ="False"/>
                                                <FooterStyle BackColor="Tan" />
                                                <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                                                <AlternatingRowStyle BackColor="PaleGoldenrod" />
    </asp:GridView>
         </td>
     </tr>
     <tr>
         <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: lightblue;">
             &nbsp;
         </td>
         <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: lightblue;" colspan="2" align="right">
             <asp:Button ID="btnupdate" runat="server" OnClick="btnupdate_Click" Text="Update"
                 Width="92px" />&nbsp;
             <asp:Button ID="btnsave" runat="server" OnClick="btnsave_Click" Text="Update" Width="125px" ValidationGroup="1" Visible="False" /></td>
         <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: lightblue;" align="left">
             &nbsp; 
             <asp:Button ID="btnclose" runat="server" OnClick="btnclose_Click" Text="Close " Width="69px" /></td>
         <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: lightblue;">
             &nbsp;&nbsp;
         </td>
         <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: lightblue;">
             &nbsp;&nbsp;
         </td>
     </tr>
     <tr>
         <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: lightblue;">
         </td>
         <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: lightblue;">
         </td>
         <td colspan="2" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: lightblue;">
             <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                 ValidationGroup="1" Width="250px" />
         </td>
         <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: lightblue;">
         </td>
         <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
             border-bottom: 1px solid; background-color: lightblue;">
         </td>
     </tr>
                                    <tr>
                                        <td class="tdmarginddl" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: lightblue;" colspan="6">
                                            &nbsp;
                                            <asp:Label ID="Label2" runat="server" Visible="False"></asp:Label></td>
                                    </tr>
     <tr>
         <td  colspan="6" style="border-right: 1px solid; border-top: 1px solid;
             border-left: 1px solid; border-bottom: 1px solid; background-color: lightblue" align="left">
             <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Red"
                 Visible="False" Width="475px"></asp:Label></td>
     </tr>
                                    <tr>
                                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: lightblue;" align="left" colspan="6">
                                            <asp:GridView ID="dgridchallan" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="dgridchallan_SelectedIndexChanged" OnRowDataBound="dgridchallan_RowDataBound"
          AllowPaging="True" PageSize="5" PagerSettings-Visible ="true" CellPadding="2" ForeColor="Black" GridLines="None" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" OnPageIndexChanging="dgridchallan_PageIndexChanging"  >
        <HeaderStyle  CssClass="gridheader" BackColor="Tan" Font-Bold="True"   />
        <Columns>
            <asp:CommandField ShowSelectButton="True" HeaderText="Action" SelectText="Delete" >
                <ItemStyle CssClass="griditem" />
            </asp:CommandField>
                       
            <asp:BoundField DataField="RO_No" HeaderText="R.O. No." ReadOnly="True" SortExpression="RO_No" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="TO_Number" HeaderText="T.O. No." ReadOnly="True" SortExpression="TO_Number" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="T. O. Date">
     
                <ItemTemplate>
                <asp:Label ID="lblChallan" runat="server" 
                Text='<%# Eval("TO_Date").ToString()%>'>
                </asp:Label>
                </ItemTemplate>
               <HeaderStyle CssClass="gridlarohead" />
                <ItemStyle CssClass="griditemlaro" />
                 </asp:TemplateField>
            <asp:BoundField DataField="Quantity" HeaderText="T.O. Qty." SortExpression="Quantity" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Cumulative_Qty" HeaderText="Lifted Qty." SortExpression="Cumulative_Qty">
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="DepotName" HeaderText="For Depot" SortExpression="DepotName">
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            
            <asp:BoundField DataField="Tname" HeaderText="Transporter Name" ReadOnly="True" SortExpression="Tname" >
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
            <asp:BoundField DataField="Trunsuction_Id" HeaderText="ID" SortExpression="Trunsuction_Id">
                <ItemStyle CssClass="griditemlaro" />
                <HeaderStyle CssClass="gridlarohead" />
            </asp:BoundField>
           
        </Columns>
                                                <FooterStyle BackColor="Tan" />
                                                <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                                                <AlternatingRowStyle BackColor="PaleGoldenrod" />
    </asp:GridView>
    </td>
                                    </tr>
                                
        <asp:Label ID="Label1" runat="server"></asp:Label></table> 
        <div>
        <table border ="0" cellpadding ="0" cellspacing ="0" class="gridfooter">
        <tr>
                        <td>
                            <div >
                                <asp:LinkButton ID="Firstbutton" Text='<img border="0" src="../images/firstpage.gif" align="absmiddle">'
                                    CommandArgument="0" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false"  />&nbsp;
                                <asp:LinkButton ID="Prevbutton" Text='<img border="0" src="../images/prevpage.gif" align="absmiddle">'
                                    CommandArgument="prev" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" />&nbsp;
                                <asp:LinkButton ID="Nextbutton" Text='<img border="0" src="../images/nextpage.gif" align="absmiddle">'
                                    CommandArgument="next" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" />&nbsp;
                                <asp:LinkButton ID="Lastbutton" Text='<img border="0" src="../images/lastpage.gif" align="absmiddle">'
                                    CommandArgument="last" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" />&nbsp;&nbsp;
                             
                            </div>
                        </td>
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


