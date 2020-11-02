<%@ Page Language="C#" MasterPageFile="~/MasterPage/AdminLogin.master" AutoEventWireup="true" CodeFile="Edit_LARO_Page.aspx.cs" Inherits="Admin_Edit_LARO_Page" Title="Edit Lifting Against Release Order " %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="hedl">
        &nbsp;Edit Lifting Against&nbsp; FCI Release Order
    </div>
    <div id="ronewmargin">
       
                        
                <table cellpadding ="0" cellspacing ="0" border ="0" class="laromargin">
                    <tr>
                        <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
                            &nbsp; &nbsp; &nbsp;&nbsp;
                        </td>
                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
                            &nbsp;
                                            <asp:DropDownList ID="ddlrono" runat="server" Width="110px" OnSelectedIndexChanged="ddlrono_SelectedIndexChanged"
                                                AutoPostBack="True" Visible="False">
                                                
                                            </asp:DropDownList></td>
                        <td  colspan="2" align="center">
                            RO Information&nbsp;
                        </td>
                        <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 52px;">
                            &nbsp;&nbsp;
                        </td>
                        <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
                            &nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                       
                          
                                
                                
                                        <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; height: 26px;">
                                            RO No.</td>
                                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; height: 26px;">
                                            &nbsp;<asp:TextBox ID="txtrono" runat="server" Width="105px" ReadOnly="True"></asp:TextBox></td>
                                        <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; height: 26px;">
                                            RO Date</td>
                                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; height: 26px;">
                                            <asp:TextBox ID="txtrodate" runat="server" Width="110px" ReadOnly="True" ></asp:TextBox>
                                        </td>
                                        <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; height: 26px; width: 52px;">
                                            RO Qty</td>
                                        <td align ="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; height: 26px;" >
                                            <asp:TextBox ID="txtroqty" runat="server" Width="110px" ReadOnly="True" ></asp:TextBox>(Qtl.)
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
                                        </td>
                                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
                                        </td>
                                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
                                            </td>
                                            <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid"></td>
                                            <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 52px;"></td>
                                            <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid"></td>
                                    </tr>
                                    <tr>
                                        <td class="tdmarginddl" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
                                            Commodity</td>
                                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
                                            <asp:TextBox ID="txtcomdty" runat="server" Width="110px" ReadOnly="True" ></asp:TextBox>
                                        </td>
                                        <td class="tdmarginddl" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
                                            Scheme</td>
                                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid">
                                            <asp:TextBox ID="txtscheme" runat="server" Width="100px" ReadOnly="True" ></asp:TextBox>&nbsp;</td> 
                                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 52px;">Bal Qty </td>
                                        <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid"> 
                                            <asp:TextBox ID="txtbalqty" runat="server" ReadOnly="True" Width="100px"></asp:TextBox>(Qtl.)</td>
                                    </tr>
                                    <tr>
                                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
                                        </td>
                                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
                                        </td>
                                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
                                        </td>
                                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
                                        </td>
                                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 52px;">
                                        </td>
                                        <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
                                        </td>
                                    </tr>
                                
        <asp:Label ID="Label1" runat="server"></asp:Label></table> 
                                   <table border="0" cellpadding="0" cellspacing="0" width ="700px">
                                       <tr>
                                           <td class="tdmarginro" style="height: 22px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 193px;" align="left">
                                               &nbsp; &nbsp; &nbsp; &nbsp;
                                           </td>
                                           <td style="height: 22px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
                                               &nbsp;&nbsp; &nbsp; &nbsp;
                                           </td>
                                           <td  class="tdmarginlaroT" align="center"  colspan="3" style="color: olive; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
                                               <strong>
                                               Transpotation&nbsp; Information</strong>&nbsp;
                                           </td>
                                           <td class="tdmarginro" style="height: 22px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
                                               &nbsp;&nbsp; &nbsp;&nbsp;
                                           </td>
                                           <td colspan="3" style="height: 22px">
                                           </td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="height: 22px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 193px;">
                                               &nbsp;&nbsp; &nbsp;</td>
                                           <td class="tdmarginro" colspan="2" style="height: 22px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
                                               &nbsp;</td>
                                           <td class="tdmarginro" style="height: 22px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; color: purple;" align="center" colspan="2">
                                               &nbsp;&nbsp; &nbsp;&nbsp;<strong>&nbsp;From &nbsp;FCI</strong> &nbsp;
                                           </td>
                                           <td   colspan="1" style="height: 22px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
                                               </td>
                                           <td colspan="2" style="height: 22px">
                                           </td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               width: 193px; border-bottom: 1px solid; height: 23px">
                                               Depot Type</td>
                                           <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               border-bottom: 1px solid; height: 23px">
                                               <asp:DropDownList ID="ddldepottype" runat="server" Enabled="False">
                                                   <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
                                                   <asp:ListItem Value="02">OWNED</asp:ListItem>
                                                   <asp:ListItem Value="03">CWC</asp:ListItem>
                                                   <asp:ListItem Value="04">SWC</asp:ListItem>
                                                   <asp:ListItem Value="05">PRIVATE</asp:ListItem>
                                                   <asp:ListItem Value="06">Hired(Private party)</asp:ListItem>
                                               </asp:DropDownList></td>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               color: olive; border-bottom: 1px solid; height: 23px">
                                           </td>
                                           <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               width: 395px; border-bottom: 1px solid; height: 23px">
                                           </td>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               width: 78px; border-bottom: 1px solid; height: 23px">
                                           </td>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               border-bottom: 1px solid; height: 23px">
                                              </td>
                                           <td colspan="3" style="height: 23px">
                                           </td>
                                       </tr>
                                    <tr>
                                        <td class="tdmarginro" style="height: 23px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 193px;">
                                            FCI AM Office</td>
                                        <td style="height: 23px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;" align="left">
                                            <asp:DropDownList ID="ddlfcidist" runat="server" Width="134px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" Enabled="False">
                                            <asp:ListItem Value="01" Selected="True">-Select-</asp:ListItem>
                                            </asp:DropDownList>&nbsp;</td>
                                        <td class="tdmarginro" style="height: 23px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; color: olive; border-bottom: 1px solid;">
                                            &nbsp;
                                            </td>
                                        <td style="height: 23px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 395px;" align="left">
                                            &nbsp;Dispatch Depot:</td>
                                        <td class="tdmarginro" style="height: 23px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 78px;">
                                            <asp:DropDownList ID="ddlfcidepo" runat="server" Width="108px" Enabled="False">
                                            <asp:ListItem Value="01" Selected="True">-Select-</asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td class="tdmarginro" style="height: 23px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
                                            </td>
                                        <td colspan="3" style="height: 23px">
                                        </td>
                                    </tr>
                                       <tr>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               border-bottom: 1px solid; height: 22px; width: 193px;">
                                               &nbsp;</td>
                                           <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               border-bottom: 1px solid; height: 22px">
                                               &nbsp;</td>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; color: olive; border-bottom: 1px solid;">
                                               &nbsp;&nbsp;
                                           </td>
                                           <td class="tdmarginlaro"  align="left" colspan="2" style="border-right: 1px solid; border-top: 1px solid;
                                               border-left: 1px solid; border-bottom: 1px solid; height: 22px; color: purple;">
                                               &nbsp;TO MPSCSC (Issue Center)&nbsp;</td>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               border-bottom: 1px solid; height: 22px">
                                               &nbsp;</td>
                                           <td colspan="3" style="height: 22px">
                                           </td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               border-bottom: 1px solid; height: 22px; width: 193px;" align="left">
                                            Destination District</td>
                                           <td align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               border-bottom: 1px solid; height: 22px">
                                               &nbsp;<asp:DropDownList ID="ddldistrict" runat="server" Width="110px" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged" AutoPostBack ="true" Enabled="False">
                                                <asp:ListItem Value="01" Selected="True">-Select-</asp:ListItem>
                                            </asp:DropDownList></td>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; color: olive; border-bottom: 1px solid;">
                                               &nbsp;&nbsp; </td>
                                           <td class="tdmarginro" align="left" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               border-bottom: 1px solid; height: 22px; width: 395px;">
                                               Destination Depot</td>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               border-bottom: 1px solid; height: 22px; width: 78px;">
                                               &nbsp;<asp:DropDownList ID="ddlissue" runat="server" Width="110px" Enabled="False" >
                                                <asp:ListItem Value="01" Selected="True">-Select-</asp:ListItem>
                                            </asp:DropDownList></td>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               border-bottom: 1px solid; height: 22px">
                                               &nbsp;</td>
                                           <td colspan="3" style="height: 22px">
                                           </td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               border-bottom: 1px solid; height: 22px; width: 193px;">
                                            Transporter Name:</td>
                                           <td class="tdmarginro" colspan="2" style="border-right: 1px solid; border-top: 1px solid;
                                               border-left: 1px solid; border-bottom: 1px solid; height: 22px">
                                              <asp:DropDownList ID="txttrans" runat="server" Width="110px"  OnSelectedIndexChanged="ddlgtype_SelectedIndexChanged" Enabled="False">
                                                <asp:ListItem Value="01" Selected="True">-Select-</asp:ListItem>
                                            </asp:DropDownList>&nbsp;</td>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               border-bottom: 1px solid; height: 22px; width: 395px;">
                                            Truck No:</td>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               border-bottom: 1px solid; height: 22px; width: 78px;">
                                            <asp:TextBox ID="txtvehno" runat="server" Width="85px" ></asp:TextBox></td>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               border-bottom: 1px solid; height: 22px">
                                            </td>
                                           <td colspan="3" style="height: 22px">
                                           </td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               width: 193px; border-bottom: 1px solid; height: 22px">
                                            Challan No</td>
                                           <td class="tdmarginro" colspan="2" style="border-right: 1px solid; border-top: 1px solid;
                                               border-left: 1px solid; border-bottom: 1px solid; height: 22px">
                                            <asp:TextBox ID="txtchallan" runat="server" Width="151px" ></asp:TextBox></td>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               width: 395px; border-bottom: 1px solid; height: 22px">
                                               Challan Date
                                           </td>
                                           <td class="tdmarginro" colspan="2" style="border-right: 1px solid; border-top: 1px solid;
                                               border-left: 1px solid; border-bottom: 1px solid; height: 22px">
                                           <asp:TextBox ID="DaintyDate2" runat="server"></asp:TextBox>
                                            <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_txtrodate'
	    });
	     </script>
                                           </td>
                                           <td colspan="3" style="height: 22px">
                                           </td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               border-bottom: 1px solid; height: 22px; width: 193px;">
                                               Dispatch Qty</td>
                                           <td colspan="2" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               border-bottom: 1px solid; height: 22px">
                                               &nbsp;<asp:TextBox ID="txtqtysend" runat="server" Width="142px"></asp:TextBox>Qtls.</td>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               border-bottom: 1px solid; height: 22px; width: 395px;">
                                            No. of Bags</td>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               border-bottom: 1px solid; height: 22px; width: 78px;">
                                            <asp:TextBox ID="txtnobags" runat="server" Width="114px" ></asp:TextBox></td>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               border-bottom: 1px solid; height: 22px">
                                               </td>
                                           <td colspan="3" style="height: 22px">
                                           </td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               border-bottom: 1px solid; height: 22px; width: 193px;">
                                               <asp:Label ID="Label2" runat="server" Text="Crop Year" Visible="False"></asp:Label></td>
                                           <td class="tdmarginro" colspan="2" style="border-right: 1px solid; border-top: 1px solid;
                                               border-left: 1px solid; border-bottom: 1px solid; height: 22px">
                                            <asp:DropDownList ID="ddlcropyear" runat="server" Width="157px"  AutoPostBack ="false" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Visible="False">
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
                                           <td style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               border-bottom: 1px solid; height: 22px; width: 395px;">
                                               <asp:Label ID="Label3" runat="server" Text="Category" Visible="False"></asp:Label></td>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               border-bottom: 1px solid; height: 22px; width: 78px;">
                                               <asp:DropDownList ID="ddlcategory" runat="server" Visible="False" Width="110px">
                                            </asp:DropDownList></td>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               border-bottom: 1px solid; height: 22px">
                                            <asp:TextBox ID="txtgowdn" runat="server" Width="40px" Visible="False" ></asp:TextBox></td>
                                           <td colspan="3" style="height: 22px">
                                           </td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               border-bottom: 1px solid; height: 22px; width: 193px;">
                                               Dispatch Time</td>
                                           <td align="left" colspan="3" style="border-right: 1px solid; border-top: 1px solid;
                                               border-left: 1px solid; border-bottom: 1px solid; height: 22px">
                                               
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
                            <asp:ListItem Value="60">60</asp:ListItem>
                        </asp:DropDownList>

:
 
    <asp:DropDownList ID="ddlampm" runat="server">
    <asp:ListItem Value="01">AM</asp:ListItem>
    <asp:ListItem Value="02">PM</asp:ListItem>
    </asp:DropDownList>
                                            </td>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               border-bottom: 1px solid; height: 22px; width: 78px;" align="left">
                                               Moisture( %) &nbsp;&nbsp;
                                           </td>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               border-bottom: 1px solid; height: 22px">
                                            <asp:TextBox ID="txtmoisture" runat="server" Width="80px"></asp:TextBox></td>
                                           <td colspan="3" style="height: 22px">
                                           </td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               width: 193px; border-bottom: 1px solid; height: 22px">
                                           </td>
                                           <td align="left" colspan="3" style="border-right: 1px solid; border-top: 1px solid;
                                               border-left: 1px solid; border-bottom: 1px solid; height: 22px">
                                               <asp:Label ID="lblfqty" runat="server" Visible="False"></asp:Label></td>
                                           <td align="left" class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid;
                                               border-left: 1px solid; width: 78px; border-bottom: 1px solid; height: 22px">
                                           </td>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               border-bottom: 1px solid; height: 22px">
                                           </td>
                                           <td colspan="3" style="height: 22px">
                                           </td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               width: 193px; border-bottom: 1px solid; height: 22px">
                                           </td>
                                           <td align="left" colspan="3" style="border-right: 1px solid; border-top: 1px solid;
                                               border-left: 1px solid; border-bottom: 1px solid; height: 22px">
                                            <asp:DropDownList ID="ddlgtype" runat="server" Width="110px"  OnSelectedIndexChanged="ddlgtype_SelectedIndexChanged" Visible="False">
                                                <asp:ListItem Value="01" Selected="True">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Button ID="btnsubmit" runat="server" Text="Update Record " OnClick="btnsubmit_Click" Width="110px" />
                                               <asp:Button ID="btnclose" runat="server" OnClick="btnclose_Click" Text="Close" Width="87px" /></td>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               width: 78px; border-bottom: 1px solid; height: 22px">
                                            </td>
                                           <td class="tdmarginro" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
                                               border-bottom: 1px solid; height: 22px">
                                           </td>
                                           <td colspan="3" style="height: 22px">
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
