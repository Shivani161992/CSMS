<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Generate_TransportOrder.aspx.cs" Inherits="District_Generate_TransportOrder" Title="Generate Transport Order " %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <table cellpadding ="0" cellspacing ="0" border ="0" style="width: 607px">
     <tr>
         <td align="center" colspan="6" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; color: white; border-bottom: silver 1px solid;
             position: static; height: 17px; background-color: lightslategray">
             <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Size="16px" Text="Transport Order Against  FCI  Release Order "></asp:Label></td>
     </tr>
     <tr>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; height: 19px; background-color: #cfdcdc">
         </td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; height: 19px; background-color: #cfdcdc; width: 131px;">
         </td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; color: white; border-bottom: silver 1px solid;
             position: static; height: 19px; background-color: lightslategray">
             <asp:Label ID="Label15" runat="server" Text="District Logged in" Width="99px"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; color: white; border-bottom: silver 1px solid;
             position: static; height: 19px; background-color: lightslategray">
             <asp:TextBox ID="txtdistrict" runat="server" Width="110px" BackColor="#E0E0E0" Font-Bold="True" Font-Italic="True" Font-Size="14px" ForeColor="White"></asp:TextBox></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; height: 19px; background-color: #cfdcdc">
         </td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; height: 19px; background-color: #cfdcdc">
         </td>
     </tr>
     <tr>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
             </td>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; width: 131px;" align="left">
             </td>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
             <asp:Label ID="Label16" runat="server" Text="Month " Visible="False"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; width: 139px;">
             <asp:DropDownList ID="ddl_allot_month" runat="server" Width="118px" AutoPostBack="True" OnSelectedIndexChanged="ddl_allot_month_SelectedIndexChanged" Visible="False">
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
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
             <asp:Label ID="Label17" runat="server" Text="Year  " Visible="False"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:DropDownList ID="ddd_allot_year" runat="server" OnSelectedIndexChanged="ddd_allot_year_SelectedIndexChanged" Width="113px" Visible="False">
                                </asp:DropDownList>
         </td>
     </tr>
                    <tr>
                       
                          
                                
                                
                                        <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;" align="left">
                                            RO No.</td>
                                        <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px; width: 131px;" align="left">
                                            <asp:DropDownList ID="ddlrono" runat="server" Width="110px" OnSelectedIndexChanged="ddlrono_SelectedIndexChanged"
                                                AutoPostBack="True" BackColor="#E0E0E0">
                                                
                                            </asp:DropDownList>
                                        </td>
                                        <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;" align="left">
                                            &nbsp;<asp:Label ID="Label13" runat="server" Text="Commodity"></asp:Label></td>
                                        <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; width: 139px; height: 24px;" align="left">
                                            &nbsp;<asp:TextBox ID="txtcommodity" runat="server" Width="105px" ReadOnly="True" BackColor="#E0E0E0"></asp:TextBox></td>
                                        <td  style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;" align="left">
                                            <asp:Label ID="Label12" runat="server" Text="Scheme"></asp:Label></td>
                                        <td  align ="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 24px;" >
             <asp:TextBox ID="txtscheme" runat="server" Width="110px" ReadOnly="True" BackColor="#E0E0E0"></asp:TextBox></td>
                                    </tr>
     <tr>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; height: 18px; background-color: #cfdcdc">
         </td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; height: 18px; background-color: #cfdcdc; width: 131px;">
         </td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; height: 18px; background-color: #cfdcdc">
             <asp:Label ID="Label10" runat="server" Text="RO Qty." Width="56px"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; width: 139px; border-bottom: silver 1px solid;
             position: static; height: 18px; background-color: #cfdcdc">
                                            <asp:TextBox ID="txtroqty" runat="server" Width="110px" ReadOnly="True" BackColor="#E0E0E0" ></asp:TextBox></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; height: 18px; background-color: #cfdcdc">
             <asp:Label ID="Label11" runat="server" Text="Validity Date"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; height: 18px; background-color: #cfdcdc">
                                            <asp:TextBox ID="txtrodate" runat="server" Width="110px" ReadOnly="True" BackColor="#E0E0E0" ></asp:TextBox></td>
     </tr>
     <tr>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcdc">
         </td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcdc; width: 131px;">
         </td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcdc">
             <asp:Label ID="Label9" runat="server" Text="Issued T.O.  Qty." Width="88px"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; width: 139px; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcdc">
             <asp:TextBox ID="txtcumlqty" runat="server" Width="110px" ReadOnly="True" BackColor="#E0E0E0"></asp:TextBox></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcdc">
             <asp:Label ID="Label8" runat="server" Text="Pending Qty for  issue of T.O." Width="85px"></asp:Label></td>
         <td align="left" class="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcdc">
                                            <asp:TextBox ID="txtbalqty" runat="server" ReadOnly="True" Width="110px" BackColor="#E0E0E0"></asp:TextBox></td>
     </tr>
     <tr>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcdc">
         </td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcdc; width: 131px;">
         </td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcdc">
         </td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; width: 139px; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcdc">
         </td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcdc">
         </td>
         <td align="left" class="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcdc">
         </td>
     </tr>
     <tr>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcc8">
             <asp:Label ID="Label18" runat="server" Text="Transport Order  No."></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcc8">
                                            <asp:TextBox ID="txttorderno" runat="server" Width="104px" MaxLength="12"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txttorderno"
                                                ErrorMessage="Transport Order No Required " Font-Bold="True" ValidationGroup="1"
                                                Width="5px">*</asp:RequiredFieldValidator></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcc8">
             <asp:Label ID="Label19" runat="server" Text="Transport Order Date(dd/mm/yyyy)" Width="104px"></asp:Label></td>
         <td align="left" colspan="2" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcc8">
                         <asp:TextBox ID="DaintyDate1" runat="server" Width="110px"></asp:TextBox>
                          <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate1'
	    });
	     </script>

         </td>
         <td align="left" class="tdmarginro" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcc8">
         </td>
     </tr>
     <tr>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcc8">
             <asp:Label ID="Label7" runat="server" Text="Transporter Name/(Lead)" Width="72px"></asp:Label></td>
         <td align="left" style="border-right: silver 1px solid; border-top: silver 1px solid;
             font-size: 10pt; border-left: silver 1px solid; border-bottom: silver 1px solid;
             position: static; background-color: #cfdcc8" colspan="5">
                                                <asp:DropDownList ID="ddltransport" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddltransport_SelectedIndexChanged">
                                                </asp:DropDownList></td>
     </tr>
     <tr>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
         </td>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="right" colspan="2">
             <asp:Label ID="lblsch" runat="server" Visible="False" Width="60px"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; width: 139px;">
             <asp:Label ID="lblcomdty" runat="server" Visible="False" Width="60px"></asp:Label></td>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
             <asp:Label ID="Label6" runat="server" Text="Lifted Qty. from FCI" Width="49px" Visible="False"></asp:Label></td>
         <td align="left" class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
             <asp:TextBox ID="txtliftqty" runat="server" Width="110px" ReadOnly="True" Visible="False"></asp:TextBox></td>
     </tr>
     <tr>
         <td align="left" style="background-color: #cccccc; font-size: 10pt; position: static;">
         </td>
         <td  style="background-color: #cccccc; font-size: 10pt; position: static; width: 131px;">
         </td>
         <td colspan="3" style="background-color: #cccccc; font-size: 10pt; position: static;" align="left">
             <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="15px" Text="Destination    Information"></asp:Label></td>
         <td  style="background-color: #cccccc; font-size: 10pt; position: static;">
         </td>
     </tr>
     <tr>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static;" align="left">
             </td>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 131px;">
             &nbsp;&nbsp;
             </td>
         <td style="background-color: #cfdcc8; font-size: 10pt; position: static;" align="left" colspan="3">
             <strong>
                 <asp:Label ID="lbltoqty" runat="server" Visible="False"></asp:Label></strong>
             &nbsp;&nbsp;
             <asp:DropDownList ID="ddldepottype" runat="server" Width="43px" Visible="False">
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
                 <asp:ListItem Value="02">OWNED</asp:ListItem>
                 <asp:ListItem Value="03">CWC</asp:ListItem>
                 <asp:ListItem Value="04">SWC</asp:ListItem>
                 <asp:ListItem Value="05">PRIVATE</asp:ListItem>
                 <asp:ListItem Value="06">Hired(Private party)</asp:ListItem>
                 <asp:ListItem Value="07">HIRED</asp:ListItem>
             </asp:DropDownList>
             <asp:DropDownList ID="ddllead" runat="server" Width="115px" AutoPostBack="True" OnSelectedIndexChanged="ddllead_SelectedIndexChanged" Visible="False">
             </asp:DropDownList></td>
         <td  style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             
                                            </td>
     </tr>
     <tr>
         <td align="left" colspan="6" style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="#C00000" Visible="False"></asp:Label></td>
     </tr>
     <tr>
         <td align="center" style="border-right: #cccccc 0px solid; border-top: #cccccc 0px solid;
             border-left: #cccccc 0px solid; border-bottom: #cccccc 0px solid; background-color: lightslategray; font-size: 10pt; position: static; color: white;" colspan="6">
             <strong>
             From FCI </strong>
         </td>
     </tr>
     <tr>
         <td align="left" style="border-right: #cccccc 0px solid; border-top: #cccccc 0px solid;
             border-left: #cccccc 0px solid; border-bottom: #cccccc 0px solid; background-color: #cfdcdc; font-size: 10pt; position: static;">
             FCI Region</td>
         <td  style="border-right: #cccccc 0px solid; border-top: #cccccc 0px solid; border-left: #cccccc 0px solid;
             border-bottom: #cccccc 0px solid; background-color: #cfdcdc; font-size: 10pt; position: static; width: 131px;">
             <asp:DropDownList ID="ddlfcidist" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfcidist_SelectedIndexChanged"
                 Width="127px">
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
         <td align="left"  style="border-right: #cccccc 0px solid; border-top: #cccccc 0px solid;
             border-left: #cccccc 0px solid; border-bottom: #cccccc 0px solid; background-color: #cfdcdc; font-size: 10pt; position: static;">
             Dispatch Depot:</td>
         <td align="left" colspan="2" style="border-right: #cccccc 0px solid; border-top: #cccccc 0px solid;
             font-size: 10pt; border-left: #cccccc 0px solid; border-bottom: #cccccc 0px solid;
             position: static; background-color: #cfdcdc">
             <asp:DropDownList ID="ddlfcidepo" runat="server" OnSelectedIndexChanged="ddlfcidepo_SelectedIndexChanged"
                 Width="143px">
                 <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
             </asp:DropDownList></td>
         <td  style="border-right: #cccccc 0px solid; border-top: #cccccc 0px solid; border-left: #cccccc 0px solid;
             border-bottom: #cccccc 0px solid; background-color: #cfdcdc; font-size: 10pt; position: static;">
             </td>
     </tr>
     <tr>
         <td align="center" style="background-color: lightslategray; font-size: 10pt; position: static; color: white;" colspan="6">
             <strong>
             To &nbsp; MPSCSC Issue Center</strong></td>
     </tr>
     <tr>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: #cccccc 1px solid; border-top: #cccccc 1px solid; border-left: #cccccc 1px solid; border-bottom: #cccccc 1px solid;">
             District</td>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: #cccccc 1px solid; border-top: #cccccc 1px solid; border-left: #cccccc 1px solid; border-bottom: #cccccc 1px solid;">
             <asp:DropDownList ID="ddldistrict" runat="server" Width="128px" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged"
                                                AutoPostBack="True">
             </asp:DropDownList></td>
         <td align="left"  style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: #cccccc 1px solid; border-top: #cccccc 1px solid; border-left: #cccccc 1px solid; border-bottom: #cccccc 1px solid;">
             Issue Center</td>
         <td align="left" colspan="2" style="border-right: #cccccc 1px solid; border-top: #cccccc 1px solid;
             font-size: 10pt; border-left: #cccccc 1px solid; border-bottom: #cccccc 1px solid;
             position: static; background-color: #cfdcdc">
             <asp:DropDownList ID="ddlissue" runat="server" Width="145px" >
             </asp:DropDownList></td>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: #cccccc 1px solid; border-top: #cccccc 1px solid; border-left: #cccccc 1px solid; border-bottom: #cccccc 1px solid;">
             </td>
     </tr>
     <tr>
         <td align="left"  style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: #cccccc 1px solid; border-top: #cccccc 1px solid; border-left: #cccccc 1px solid; border-bottom: #cccccc 1px solid;">
             Quantity (Qtl.kgGms)</td>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: #cccccc 1px solid; border-top: #cccccc 1px solid; border-left: #cccccc 1px solid; border-bottom: #cccccc 1px solid;">
             <asp:TextBox ID="txtsendqty" runat="server" Width="119px" MaxLength="13"></asp:TextBox></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: #cccccc 1px solid; border-top: #cccccc 1px solid; border-left: #cccccc 1px solid; border-bottom: #cccccc 1px solid;" colspan="3">
             &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtsendqty"
                 ErrorMessage="Quantity Required" ValidationGroup="1">*</asp:RequiredFieldValidator>
             &nbsp; &nbsp; &nbsp;
             <asp:Button ID="addmore" runat="server" Text="Add More" Width="132px" OnClick="addmore_Click" ValidationGroup="1" /></td>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: #cccccc 1px solid; border-top: #cccccc 1px solid; border-left: #cccccc 1px solid; border-bottom: #cccccc 1px solid;">
         </td>
     </tr>
     <tr>
         <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static;" colspan="6" valign="top">
              <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" 
          AllowPaging="True" PageSize="5" PagerSettings-Visible ="false" CellPadding="4" ForeColor="#333333" GridLines="None" Width="590px"  >
        <HeaderStyle  CssClass="gridheader" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"   />
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
            <asp:BoundField DataField="District" HeaderText="Did" SortExpression="District" >
                <HeaderStyle Font-Size="0px" />
                <ItemStyle Font-Size="0px" />
            </asp:BoundField>
            
            <asp:BoundField DataField="IssueCenter" HeaderText="ICid" ReadOnly="True" SortExpression="IssueCenter" >
                <ItemStyle CssClass="griditem" Font-Size="0px" />
                <HeaderStyle Font-Size="0px" />
            </asp:BoundField>
           
        </Columns>
         <PagerSettings Visible ="False"/>
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="True" />
                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                  <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                  <EditRowStyle BackColor="#999999" />
    </asp:GridView>
         </td>
     </tr>
     <tr>
         <td style="background-color: #cfdcc8; font-size: 10pt; position: static;" align="left">
             &nbsp;
         </td>
         <td style="background-color: #cfdcc8; font-size: 10pt; position: static;" colspan="2" align="right">
             &nbsp;
             <asp:Button ID="btnsave" runat="server" OnClick="btnsave_Click" Text="Submit" Width="125px" ValidationGroup="1" /></td>
         <td style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 139px;" align="left">
             &nbsp; 
             <asp:Button ID="btnclose" runat="server" OnClick="btnclose_Click" Text="Close " Width="107px" /></td>
         <td style="background-color: #cfdcc8; font-size: 10pt; position: static;" align="left">
             &nbsp;&nbsp;
         </td>
         <td style="background-color: #cfdcc8; font-size: 10pt; position: static;">
             &nbsp;&nbsp;
         </td>
     </tr>
     <tr>
         <td style="background-color: #cfdcc8; font-size: 10pt; position: static;" align="left">
         </td>
         <td style="background-color: #cfdcc8; font-size: 10pt; position: static; width: 131px;">
         </td>
         <td colspan="2" style="background-color: #cfdcc8; font-size: 10pt; position: static;" align="left">
             <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                 ValidationGroup="1" />
         </td>
         <td style="background-color: #cfdcc8; font-size: 10pt; position: static;" align="left">
         </td>
         <td style="background-color: #cfdcc8; font-size: 10pt; position: static;">
         </td>
     </tr>
                                    <tr>
                                        <td class="tdmarginddl" style="background-color: #cfdcc8; font-size: 10pt; position: static;" colspan="6" align="left">
                                            &nbsp;
                                            <asp:Label ID="Label2" runat="server" Visible="False"></asp:Label></td>
                                    </tr>
     <tr>
         <td  colspan="6" style="background-color: #cfdcc8; font-size: 10pt; position: static;" align="left">
             <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Red"
                 Visible="False" Width="475px"></asp:Label></td>
     </tr>
                                    <tr>
                                        <td style="background-color: #cfdcc8; font-size: 10pt; position: static;" align="left" colspan="6">
                                            <asp:GridView ID="dgridchallan" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="dgridchallan_SelectedIndexChanged" OnRowDataBound="dgridchallan_RowDataBound"
          AllowPaging="True" PageSize="5" PagerSettings-Visible ="true" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="dgridchallan_PageIndexChanging"  >
        <HeaderStyle  CssClass="gridheader" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"   />
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
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="True" />
                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <EditRowStyle BackColor="#999999" />
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


