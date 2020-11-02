<%@ Page Language="C#" MasterPageFile="~/WHP14/Master/ProcurementMaster.master" AutoEventWireup="true" CodeFile="frm_AnajPrapti_FromFarmer.aspx.cs" Inherits="WHP14_Procurement_Wheat_frm_AnajPrapti_FromFarmer" Title="" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPH" Runat="Server">

    <script type="text/javascript">
    function ShowProgress() {
        setTimeout(function () {
            var modal = $('<div />');
            modal.addClass("modal");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
        }, 200);
    }
    $('form').live("submit", function () {
        ShowProgress();
    });
    </script>

    <script type="text/javascript">
        function confirmation() {
            if (confirm('क्या आप खरीदी जानकारी सुरक्षित करना चाहते है ?')) {
            return true;
            }else{
            return false;
            }
        }
    </script>

   <table style="width: 100%; color: #000080; border-right: gray 1px solid; border-left: gray 1px solid; border-top-width: 1px; border-top-color: gray;" border="0" cellpadding="0" cellspacing="0">
        <tr style="height: 22px;">
            <td align="center" colspan="8" style="WIDTH: 800px; BORDER-BOTTOM: gray 1px solid; HEIGHT: 35px" valign="middle">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Maroon" Text="किसान से अनाज की प्राप्ति"></asp:Label></td>
        </tr>
    </table>
    <asp:Panel ID="Panel_Prapti_afterpass" runat="server" Visible="False" Width="100%">
        <table style="width: 100%; color: #000080;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="8" style="BORDER-RIGHT: gray 1px solid; BORDER-LEFT: gray 1px solid; BORDER-BOTTOM: gray 1px solid" align="center">
                    <div style="text-align: left">
                        <asp:Panel ID="pnl_CheckPass_Kharidi" runat="server">
                            <table style="width: 100%; background-color: floralwhite;" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td colspan="3" style="font-weight: bold; height: 21px; width: 500px;" align="center">
                                        पासवर्ड प्रविष्ट करे</td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                    </td>
                                    <td style="width: 100px">
                                        <asp:TextBox ID="txtpwd" runat="server" BorderColor="indianred" BorderStyle="Solid" BorderWidth="1px" Font-Bold="true" Font-Size="12pt" Height="25px" TextMode="Password" Width="250px"></asp:TextBox>
                                    </td>
                                    <td style="width: 100px">
                                        <asp:Button ID="txtSubmit" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="White" Height="32px" OnClick="txtSubmit_Click" Text="Submit" ValidationGroup="vgs" Width="80px" CssClass="CSSButton" TabIndex="1" /></td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                    <asp:Label ID="lblDistName" runat="server" Font-Size="10pt"></asp:Label><asp:DropDownList ID="ddl_Tehsil" runat="server" OnSelectedIndexChanged="ddl_Tehsil_SelectedIndexChanged" Width="176px" AutoPostBack="True" Visible="False">
                    </asp:DropDownList><asp:DropDownList ID="ddl_Village" runat="server" OnSelectedIndexChanged="ddl_Village_SelectedIndexChanged" Width="176px" AutoPostBack="True" Visible="False">
                    </asp:DropDownList><asp:DropDownList ID="ddl_Farmer" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_Farmer_SelectedIndexChanged" Visible="False">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td align="left" colspan="8" style="font-size: 14px; width: 800px; color: #ffffff; height: 35px; background-color: goldenrod">
                    &nbsp; किसान कोड से ढूंढे</td>
            </tr>
            <tr>
                <td colspan="8" style="height: 35px; width: 800px; border-right: gray 1px solid; border-left: gray 1px solid;" align="center">
                    <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 100px; font-size: 10pt; height: 35px;" align="right" valign="middle">
                                किसान कोड प्रविष्ट करे :</td>
                            <td style="width: 250px; height: 35px;" valign="middle">
                                <asp:TextBox ID="txtFarmerCode" runat="server" AutoComplete="off" BorderColor="indianred" BorderStyle="Solid" BorderWidth="1px" Font-Bold="true" Font-Size="12pt" Height="18px" Width="190px" MaxLength="12"></asp:TextBox>&nbsp;
                                <asp:Button ID="btnSearchFarmer" runat="server" BackColor="#429DD5" BorderColor="#EE3123" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="White" Height="24px" OnClick="btnSearchFarmer_Click" Text="ढूँढें" ValidationGroup="vgsearch" Width="50px" OnClientClick="showDiv();" /></td>
                            <td style="width: 100px; height: 35px;" valign="middle" align="left">
                                &nbsp;<asp:Label ID="lblfarmerid" runat="server" Visible="False"></asp:Label>
                                </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <%-- <asp:Panel ID="Panel_Kharidi" runat="server" Width="100%" Visible="False">--%>
        <%-- Yaha Se Start--%>
        <table style="width: 100%; border-right: gray 1px solid; border-top: gray 1px solid; border-left: gray 1px solid; border-bottom: gray 1px solid;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="5" style="font-size: 14px; width: 800px; color: #ffffff; height: 35px; background-color: #cc9900;" valign="middle" align="left">
                    &nbsp;खरीदी सम्बंधित जानकारी</td>
            </tr>
            <tr>
                <td style="font-size: 10pt; width: 120px; height: 20px; border-right: gray 1px solid; border-bottom: gray 1px solid;" valign="middle" align="center">
                    किसान का कुल रकबा (हेक्ट. मे)</td>
                <td style="font-size: 10pt; width: 120px; height: 20px; border-right: gray 1px solid; border-bottom: gray 1px solid;" valign="middle" align="center">
                    सिकमी/बटाई का रकबा(हेक्ट.मे)</td>
                <td style="font-size: 10pt; width: 120px; height: 20px; border-top-width: 1px; border-right: gray 1px solid; border-left-width: 1px; border-left-color: gray; border-top-color: gray; border-bottom: gray 1px solid;" valign="middle" align="center" width="100%">
                    स्वयं का रकबा</td>
                <td style="font-size: 10pt; width: 220px; height: 20px; border-right: gray 1px solid; border-bottom: gray 1px solid;" valign="middle" align="center">
                    <strong><span style="color: #008000"></span></strong>&nbsp;<asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Green" Text="अनाज अनुसार रकबा (हेक्ट मे)" Width="100%"></asp:Label></td>
                <td style="font-size: 10pt; width: 220px; color: #008000; border-bottom: gray 1px solid;" valign="middle" align="center">
                    <asp:Label ID="Label3" runat="server" Text="अभी तक बेचा अनाज (हेक्ट मे)" Width="100%"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 120px; height: 30px; border-right: gray 1px solid;" align="center">
                    <asp:TextBox ID="txtKisaanKulRakba" AutoComplete="off" runat="server" BackColor="#FFFFCC" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Width="120px" style="text-align:right;"></asp:TextBox></td>
                <td style="width: 120px; height: 30px; border-right: gray 1px solid;" align="center">
                    <asp:TextBox ID="txtSikamiRakba" AutoComplete="off" runat="server" BackColor="#FFFFCC" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Width="120px" style="text-align:right;"></asp:TextBox></td>
                <td style="width: 120px; height: 30px; border-right: gray 1px solid;" align="center">
                    <asp:TextBox ID="txtOwnRakba" AutoComplete="off" runat="server" BackColor="#FFFFCC" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Width="120px" style="text-align:right;"></asp:TextBox></td>
                <td style="width: 220px; height: 30px; border-right: gray 1px solid;" align="center" valign="middle">
                    <asp:GridView ID="GridView_CropSinchit_Asinchit" runat="server" AutoGenerateColumns="False" Width="100%" Font-Size="9pt">
                        <Columns>
                            <asp:BoundField DataField="CommodityName" HeaderText="अनाज" />
                            <asp:BoundField DataField="Rakba_crop_sinchit" HeaderText="रकबा (सिंचित)" />
                            <asp:BoundField DataField="Rakba_crop_asinchit" HeaderText="रकबा (असिंचित)" />
                            <asp:BoundField DataField="crpcode" HeaderText="फसल कोड">
                                <HeaderStyle Font-Size="0px" ForeColor="#C8D0D7" BackColor="#C8D0D7" />
                                <ItemStyle Font-Size="0px" ForeColor="#DAE5F0" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle BackColor="#FF8000" HorizontalAlign="Center" />
                        <RowStyle HorizontalAlign="Center" />
                    </asp:GridView>
                </td>
                <td style="width: 220px; height: 30px;" align="center" valign="middle">
                    <asp:GridView ID="GridView_AbhiTak_Becha" runat="server" AutoGenerateColumns="False" Height="24px" Width="100%" Font-Size="9pt">
                        <Columns>
                            <asp:BoundField HeaderText="अनाज" DataField="CommodityName" />
                            <asp:BoundField HeaderText="मात्रा" DataField="Matra" />
                            <asp:BoundField HeaderText="अनाज कोड" DataField="CommodityId">
                                <HeaderStyle Font-Size="0px" ForeColor="#C8D0D7" BackColor="#C8D0D7" />
                                <ItemStyle Font-Size="0px" ForeColor="#DAE5F0" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle BackColor="#FF8000" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <table style="width: 100%;">
            <%-- </td>
            </tr>--%>
            <tr>
                <td colspan="9" valign="top">
                    <table style="width: 100%; background-color: #99ccff; height: 40px;" border="0" frame="border" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 104px; height: 35px; border-right: gray 1px solid; border-bottom: gray 1px solid;" valign="middle" align="center">
                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#CC0000" Text="अनाज का प्रकार" Height="16px"></asp:Label>
                            </td>
                            <td style="height: 28px; border-right: gray 1px solid; border-bottom: gray 1px solid;" valign="middle" align="center">
                                <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#CC0000" Text="बोरी की संख्या" Height="8px"></asp:Label>
                            </td>
                            <td style="height: 28px; width: 183px; border-right: gray 1px solid; border-bottom: gray 1px solid;" valign="middle" align="center">
                                <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#CC0000" Text="मात्रा(क्वीन.मे)" Height="16px"></asp:Label>
                            </td>
                            <td style="font-size: 10pt; width: 58px; height: 28px; border-right: gray 1px solid; border-bottom: gray 1px solid;" valign="top">
                            </td>
                            <td style="height: 28px; font-size: 10pt; border-right: gray 1px solid; border-bottom: gray 1px solid;" valign="middle" align="center">
                                समर्थन मूल्य
                            </td>
                            <td style="height: 28px; font-size: 10pt; border-right: gray 1px solid; border-bottom: gray 1px solid;" valign="middle" align="center">
                                राज्य बोनस
                            </td>
                            <td style="height: 28px; font-size: 10pt; border-bottom: gray 1px solid;" valign="middle" align="center">
                                केन्द्रीय बोनस</td>
                        </tr>
                        <tr>
                            <td style="width: 104px; height: 35px; border-right: gray 1px solid; background-color: #ccccff;" align="center">
                                <asp:DropDownList ID="ddl_Comodity" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_Comodity_SelectedIndexChanged" Width="96px" Enabled="False">
                                </asp:DropDownList>
                            </td>
                            <td style="height: 35px; border-right: gray 1px solid; background-color: #ccccff;" align="center">
                                <asp:TextBox ID="txtBoriSankhya" AutoComplete="off" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Width="120px" style="text-align:right;"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtBoriSankhya" Display="Dynamic" ErrorMessage="बोरी की सही स. प्रविष्ट करे" ValidationExpression="^[0-9]+$" ValidationGroup="vgs" Enabled="False">*</asp:RegularExpressionValidator>
                            </td>
                            <td style="height: 35px; width: 183px; border-right: gray 1px solid; background-color: #ccccff;" align="center">
                                <asp:TextBox ID="txtMatra" AutoComplete="off" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Width="120px" style="text-align:right;"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtMatra" Display="Dynamic" ErrorMessage="सही मात्रा प्रविष्ट करे" ValidationExpression="^[0-9.]+$" ValidationGroup="vgs" Enabled="False">*</asp:RegularExpressionValidator>
                            </td>
                            <td style="width: 58px; height: 8px; border-right: gray 1px solid; background-color: #ccccff;">
                                <asp:Button ID="btnAdd" runat="server" Text="जोड़े" OnClick="btnAdd_Click" ValidationGroup="vgs" BackColor="#429DD5" BorderColor="#EE3123" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="White" Height="20px" Width="64px" /></td>
                            <td style="height: 8px; border-right: gray 1px solid; background-color: #ccccff;" align="center">
                                <asp:TextBox ID="txtSamarthan_muly" AutoComplete="off" runat="server" BackColor="#FFFFCC" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Width="112px" style="text-align:right;"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtSamarthan_muly" Display="Dynamic" ErrorMessage="सही समर्थन मूल्य प्रविष्ट करे" ValidationExpression="^[0-9.]+$" ValidationGroup="vgs">*</asp:RegularExpressionValidator>
                            </td>
                            <td style="height: 35px; border-right: gray 1px solid; background-color: #ccccff;" align="center">
                                <asp:TextBox ID="txt_Rajya_Bonus" AutoComplete="off" runat="server" BackColor="#FFFFCC" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Width="104px" style="text-align:right;"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txt_Rajya_Bonus" Display="Dynamic" ErrorMessage="सही राज्य बोनस प्रविष्ट करे" ValidationExpression="^[0-9.]+$" ValidationGroup="vgs">*</asp:RegularExpressionValidator>
                            </td>
                            <td style="height: 35px; border-right-width: 1px; border-right-color: gray; background-color: #ccccff;" align="center">
                                <asp:TextBox ID="txt_kendraBonus" AutoComplete="off" runat="server" BackColor="#FFFFCC" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Width="104px" style="text-align:right;"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txt_kendraBonus" Display="Dynamic" ErrorMessage="सही केन्द्रीय बोनस प्रविष्ट करे" ValidationExpression="^[0-9.]+$" ValidationGroup="vgs">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="9" style="height: 105px">
             
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; border-right: gray 1px solid; border-top: gray 1px solid; border-left: gray 1px solid; border-bottom: gray 1px solid; background-color: #ccccff;">
                        <tr>
                            <td style="width: 100px; height: 35px; border-right: gray 1px solid; border-bottom: gray 1px solid;">
                                &nbsp;<asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#CC0000" Text="प्राप्ति दिनांक " Width="100%"></asp:Label></td>
                            <td style="width: 150px; border-right: gray 1px solid; border-bottom: gray 1px solid;" align="left">
                                &nbsp;<asp:TextBox ID="txtPraptiDate" runat="server" AutoComplete="off" BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" MaxLength="10" Width="120px" Visible="False"></asp:TextBox>
  <%--<script type="text/javascript">
	                new tcal ({
			        	'formname': '0',
			    	'controlname': 'ctl00_MainContentPH_txtPraptiDate'
	                 });
                                </script>--%>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="ddldate" Display="Dynamic" ErrorMessage="दिनांक dd/MM/yyyy फोर्मेट मे ही चुने" OnServerValidate="CustomValidator1_ServerValidate" ValidateEmptyText="True" ValidationGroup="vg">*</asp:CustomValidator>
                                <asp:DropDownList ID="ddldate" runat="server" Width="120px">
                                </asp:DropDownList></td>
                            <td style="width: 100px; border-bottom: gray 1px solid; border-right: gray 1px solid;">
                                &nbsp;<asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#CC0000" Text="तौल पत्रक क्रमांक" Width="100%"></asp:Label></td>
                            <td style="width: 150px; border-right: gray 1px solid; border-bottom: gray 1px solid;" align="center">
                                &nbsp;<asp:TextBox ID="txtTolPatrak" AutoComplete="off" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" MaxLength="9" Width="120px" style=" text-align:right"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtTolPatrak" Display="Dynamic" ErrorMessage="सही तोल पत्रक क्र. प्रविष्ट करे" ValidationExpression="^[a-zA-Z0-9/. ]+$" ValidationGroup="vg">*</asp:RegularExpressionValidator></td>
                            <td style="width: 500px; height: 35px; border-bottom: gray 1px solid;" align="center" valign="middle">
                             <strong><span style="font-size: 10pt; background-color: #87ceeb; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">&nbsp;बेचे जाने वाले अनाज की जानकारी </span></strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 12px; width: 100px; height: 35px; border-right: gray 1px solid;">
                                &nbsp;अनाज की
                                <br />
                                कुल मात्रा</td>
                            <td style="width: 150px; border-right: gray 1px solid;" align="left">
                                &nbsp;<asp:TextBox ID="txtAnajKulMatra" AutoComplete="off" runat="server" BackColor="#FFFFCC" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Width="120px" style="text-align:right"></asp:TextBox></td>
                            <td style="font-size: 12px; width: 100px; border-right: gray 1px solid;">
                                &nbsp;शुद्ध भुगतान
                                <br />
                                योग्य राशि (रु.मे.)</td>
                            <td style="width: 150px; border-right: gray 1px solid;" align="center">
                                &nbsp;<asp:TextBox ID="txtShudhBhugtan_YogyaRashi" AutoComplete="off" runat="server" BackColor="#FFFFCC" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Width="120px" style="text-align:right"></asp:TextBox></td>
                            <td rowspan="2" style="width: 150px; height: 45px" align="center">
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" Font-Size="10pt">
                                    <Columns>
                                        <asp:CommandField HeaderText="Action" SelectText="Delete" ShowSelectButton="True"></asp:CommandField>
                                        <asp:BoundField DataField="AnajPrakar_Name" HeaderText="अनाज का प्रकार" />
                                        <asp:BoundField DataField="BoriSankhya" HeaderText="बोरी की स." />
                                        <asp:BoundField DataField="Matra" HeaderText="मात्रा (क्वी. मे)" />
                                        <asp:BoundField DataField="BhugtanYogy_Rashi" HeaderText="भुगतान योग्य राशि" />
                                        <asp:BoundField DataField="AnajPrakar_Code" HeaderText="अनाज कोड">
                                            <HeaderStyle Font-Size="0px" ForeColor="SkyBlue" />
                                            <ItemStyle Font-Size="0px" ForeColor="#DAE5F0" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle BackColor="SkyBlue" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px; height: 10px; border-right: gray 1px solid;">
                                <asp:Label ID="lblTehsilYeild" runat="server" Font-Size="1px" ForeColor="Transparent"></asp:Label>&nbsp;</td>
                            <td style="width: 100px; border-right: gray 1px solid;">
                                &nbsp;<asp:Label ID="lblfrmsocityid" runat="server" Visible="False"></asp:Label></td>
                            <td style="width: 100px; border-right: gray 1px solid;">
                                &nbsp;</td>
                            <td style="width: 150px; border-right: gray 1px solid;">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" style="font-size: 13px; width: 100%; border-right: gray 1px solid; border-top: gray 1px solid; border-left: gray 1px solid; border-bottom: gray 1px solid;">
            <tr>
                <td style="width: 133px; height: 30px">
                    सोसाईटी का ऋण(रु.मे)-<asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#CC0000" Text="सोसाईटी का नाम" Visible="False" Width="100%"></asp:Label></td>
                <td style="width: 133px">
                    <asp:TextBox ID="txtSocLoan" AutoComplete="off" runat="server" BackColor="#FFFFCC" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" style=" text-align:right" Width="120px"></asp:TextBox>
                    &nbsp; &nbsp;
                </td>
                <td style="width: 133px">
                    जिला केन्द्रिय&nbsp; सहकारी बैंक&nbsp; का ऋण(रु.मे)</td>
                <td style="width: 133px">
                    <asp:TextBox ID="txtDCCBLoan" AutoComplete="off" runat="server" BackColor="#FFFFCC" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" style=" text-align:right" Width="120px"></asp:TextBox></td>
                <td style="width: 133px">
                    सिंचाई विभाग का बकाया ऋण (रु.मे )</td>
                <td style="width: 133px">
                    <asp:TextBox ID="txtIrrigationLoan" AutoComplete="off" runat="server" BackColor="#FFFFCC" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" style=" text-align:right" Width="120px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 133px; height: 30px">
                    <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="9pt" ForeColor="#CC0000" Text="सोसाईटी ऋण के विरूद्ध वसूली राशि " Width="100%"></asp:Label></td>
                <td style="width: 133px">
                    <asp:TextBox ID="txt_Amount_Against_SocLoan" AutoComplete="off" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" OnTextChanged="txt_Amount_Against_SocLoan_TextChanged" style=" text-align:right" Width="120px">0</asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtSocLoan" ControlToValidate="txt_Amount_Against_SocLoan" Display="Dynamic" ErrorMessage="सोसाइटी ऋण के विरूद्ध वसूली गयी राशि , बकाया ऋण से ज्यादा नहीं हो सकती" Operator="LessThanEqual" Type="Integer" ValidationGroup="vg">*</asp:CompareValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_Amount_Against_SocLoan" Display="Dynamic" ErrorMessage="सोसाइटी ऋण की राशि सही प्रविष्ट करे" ValidationExpression="^[0-9]+$" ValidationGroup="vg">*</asp:RegularExpressionValidator></td>
                <td style="width: 133px">
                    <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="9pt" ForeColor="#CC0000" Text="जिला केन्द्रिय सहकारी बैंक  के विरूद्ध वसूली राशि " Height="16px" Width="100%"></asp:Label></td>
                <td style="width: 133px">
                    <asp:TextBox ID="txt_Amt_AgainstDCCBLoan" AutoComplete="off" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" style=" text-align:right" Width="120px">0</asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="txtDCCBLoan" ControlToValidate="txt_Amt_AgainstDCCBLoan" Display="Dynamic" ErrorMessage="जिला केन्द्रीय सहकारी बैंक ऋण के विरूद्ध वसूली गयी राशि , बकाया ऋण से ज्यादा नहीं हो सकती" Operator="LessThanEqual" Type="Integer" ValidationGroup="vg">*</asp:CompareValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txt_Amt_AgainstDCCBLoan" Display="Dynamic" ErrorMessage="जिला केन्द्रीय सहकारी बैंक ऋण की राशि सही प्रविष्ट क" ValidationExpression="^[0-9]+$" ValidationGroup="vg">*</asp:RegularExpressionValidator></td>
                <td style="width: 133px">
                    <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="9pt" ForeColor="#CC0000" Text="सिचाई विभाग ऋण के विरूद्ध राशि(रु.मे )" Width="100%"></asp:Label></td>
                <td style="width: 133px">
                    <asp:TextBox ID="txt_Amount_AgainstIrrigationLoan" AutoComplete="off" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" style=" text-align:right" Width="120px">0</asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToCompare="txtIrrigationLoan" ControlToValidate="txt_Amount_AgainstIrrigationLoan" Display="Dynamic" ErrorMessage="सिंचाई विभाग ऋण के विरूद्ध वसूली गयी राशि , बकाया ऋण से ज्यादा नहीं हो सकती" Operator="LessThanEqual" Type="Integer" ValidationGroup="vg">*</asp:CompareValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txt_Amount_AgainstIrrigationLoan" Display="Dynamic" ErrorMessage="सिंचाई विभाग ऋण की राशि सही प्रविष्ट क" ValidationExpression="^[0-9]+$" ValidationGroup="vg">*</asp:RegularExpressionValidator></td>
            </tr>
            <tr>
                <td style="font-weight: bold; width: 133px; height: 30px">
                    शुद्ध भुगतान(रु.मे)</td>
                <td style="width: 133px">
                    <asp:TextBox ID="txtShudhBhugtan" AutoComplete="off" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Enabled="False" style=" text-align:right" Width="120px"></asp:TextBox></td>
                <td style="width: 133px">
                </td>
                <td style="width: 133px">
                </td>
                <td style="width: 133px">
                </td>
                <td style="width: 133px">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table style="width: 100%; height: 96px;" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="8" align="center">
                &nbsp;<asp:CheckBox ID="chb_Confirm" runat="server" Text="सुरक्षित करने के लिए चेक करे" AutoPostBack="True" Font-Bold="True" Font-Size="13pt" ForeColor="Navy" OnCheckedChanged="chb_Confirm_CheckedChanged" Height="16px" />
                <asp:Panel ID="pnlConfirm_save" runat="server" Width="100%" Enabled="False">
                    <div style="text-align: center" align="center">
                        <table style="width: 100%; background-color: #e5e6e6;">
                            <tr>
                                <td style="width: 100px; height: 26px;">
                                    <asp:Button ID="btnNew" runat="server" Text="नया" OnClick="btnNew_Click" Height="36px" Width="104px" /></td>
                                <td style="width: 100px; height: 26px;">
                                    <asp:Button ID="btnSave" runat="server" Text="सुरक्षित करे " OnClick="btnSave_Click" OnClientClick="return confirmation();" ValidationGroup="vg" Height="36px" Width="200px" /></td>
                                <td style="width: 109px; height: 26px;">
                                    <asp:Button ID="btnClose" runat="server" Text="बंद करे" Height="36px" Width="160px" OnClick="btnClose_Click" /></td>
                                <td style="width: 109px; height: 26px;">
                                    <asp:Button ID="btnPawati" Visible="false" runat="server" Text="पावती प्रिंट करे" Height="36px" Width="160px" OnClick="btnPawati_Click" />
                                    </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
    </table>
    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 125px; height: 28px;">
                &nbsp;<asp:Label ID="lblb" runat="server" Visible="False"></asp:Label></td>
            <td style="height: 28px">
                &nbsp;<asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" Font-Bold="True" Font-Size="8.5pt" ShowMessageBox="True" ValidationGroup="vg" />
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" Font-Bold="True" Font-Size="8.5pt" ShowMessageBox="True" ValidationGroup="vgs" />
                <asp:Label ID="lbl_ERMSG" runat="server"></asp:Label></td>
            <td style="height: 28px">
                &nbsp;</td>
            <td style="height: 28px">
                &nbsp;</td>
            <td style="width: 171px; height: 28px;">
                &nbsp;</td>
            <td style="height: 28px">
                &nbsp;</td>
            <td style="height: 28px">
                &nbsp;</td>
            <td style="height: 28px">
                &nbsp;</td>
        </tr>
    </table>
    <div class="loading" align="center">
    <br />
        Loading. Please wait.<br />
        <br />
        <img src="../Images/loader.gif" alt="" />
    </div>
    <%-- </asp:Panel>--%>
    <%-- Yaha Tak--%>
    
</asp:Content>

