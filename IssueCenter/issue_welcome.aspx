<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true"
    CodeFile="issue_welcome.aspx.cs" Inherits="movementchallan" Title="Welcome Issue Center (MPSCSC)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="width: 620px">
        <table style="border: thin groove black; width: 632px; text-align: center;">
            <tr>
                <td style="border: thin groove black; height: 8px; text-align: left;">
                    <span style="font-family: Verdana"><span style="font-size: 11pt">
                    <span style="font-size:medium"><span style="color: #990000; font-weight: bold">द्धारा प्रदाय योजना में परिवहन आदेश बनाने की परिवर्तित प्रक्रिया</span><asp:Image ID="Image3" runat="server" Height="19px" ImageUrl="~/IssueCenter/img/blinking_new.gif" Width="39px" />
                      </span>

                        </span></span>

                </td>
            </tr>
            <tr>
                <td style="border: thin groove black; height: 8px; text-align: left;">
                    <asp:HyperLink ID="HyperLink25" a target="_blank" href="dpy calculation.pdf" 
                        runat="server" style="font-weight: 700">द्वारप्रदाय योजना के अन्तर्गत बनने वाले तौल पत्रक के बिल के आकलन लिए देखे| </asp:HyperLink>
                    <span style="font-family: Verdana"><span style="font-size: 11pt">
                    <span style="font-size:medium"><asp:Image ID="Image4" runat="server" 
                        Height="19px" ImageUrl="~/IssueCenter/img/blinking_new.gif" Width="39px" />
                      </span>

                        </span></span>

                    </td>
            </tr>
            <tr>
                <td style="height: 45px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; text-align: left;">
                    | <span style="color: #000099">द्धारा प्रदाय योजना के अंतर्गत संचालनालय खाद्य द्धारा समग्र पोर्टल पर आवंटन जारी 
            करने में विलंब के कारण मुख्यालय द्धारा यह निर्णय लिया गया है कि प्रति माह विगत 3 
            माह के आवंटन के अधिकतम के आधार पर परिवहन आदेश जारी कर वितरण किया जाये एवं 1 माह 
            पहले तक के वास्तविक आवंटन का समायोजन किया जाये |अत: इस माह से समग्र पोर्टल पर 
            आवंटन जारी होने के पूर्व ही परिवहन आदेश बनाये जा सकते है | इस बाबत सॉफ्टवेयर में 
            परिवर्तन कर दिया गया है | प्रति माह वास्तविक आवंटन से समायोजन सॉफ्टवेयर के 
            माध्यम से स्वत: ही हो जायेगा | विस्तृत विवरण सहित पत्र प्रेषित किया जा रहा है |</span></td>
            </tr>
            <tr>
                 <td style="height: 45px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;" align = "left">
                     <span style="font-size: medium" >
                     <span
                             style="font-size: 11pt"> &nbsp;</span></span><span style="font-size: 11pt"><span style="font-family: Verdana">तकनिकी सहायता के लिए कृपया ई मेल anuragintit@gmail.com एवं priyeshdubey40@gmail.com
                        के साथ mp.iisfm@gmail.com पर समस्या लिख कर भेजे साथ में स्क्रीन शोट अटेच करें |</span></span></td>
            </tr>
            <tr>
                <td style="text-align: center; height: 1px; font-size: small;">
                    <a target="_blank" href="marfed_Instruction.pdf">प्रदाय केन्द्रों पर चावल की प्राप्ति
                        की जानकारी हेतु यहाँ क्लिक करें |     </tr>
            <tr>
                <td>
                    <center>
                        <table width="300">
                            <tr>
                                <td rowspan="2">
                                    <asp:GridView ID="grd_operater" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84"
                                        BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2"
                                        EnableModelValidation="True" Font-Size="Small" Visible="False" Width="300px">
                                        <Columns>
                                            <asp:BoundField DataField="OperatorName" HeaderText="Operator Name" />
                                            <asp:BoundField DataField="Mobile" HeaderText="Mobile No." />
                                        </Columns>
                                        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </center>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:LinkButton ID="btnLink_AddOperator" runat="server" OnClick="btnLink_AddOperator_Click"
                        Visible="False" Width="127px" Font-Bold="True" Font-Size="14px">Add Operator</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td style="text-align: center" align="center">
                    <table width="100%" border="2" style="border: thin solid #000000" visible="false">
                        <tr>
                            <td>
                                <asp:TextBox ID="txt_Date" runat="server" BackColor="#FFFF80" BorderColor="Black"
                                    Visible="false"></asp:TextBox>

                                <script type="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_txt_Date'
	    });
                                </script>

                                <asp:Button ID="btnSelect" runat="server" Text="Check Date for No Transaction" OnClick="btnSelect_Click"
                                    Width="195px" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                                <asp:CheckBox ID="chkRecieptDetails" runat="server" Font-Bold="True" Font-Size="8pt"
                                    Text="<----Ckeck Here If no Reciept Details" Enabled="False" Width="273px" AutoPostBack="True"
                                    Visible="false" /></td>
                        </tr>
                        <tr>
                            <td style="width: 100px; height: 14px;">
                                <asp:CheckBox ID="chkDispatchDetails" runat="server" Font-Bold="True" Font-Size="8pt"
                                    Text="<----Ckeck Here If no Dispatch Details" Enabled="False" Width="279px" AutoPostBack="True"
                                    Visible="false" /></td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                                <asp:CheckBox ID="chkDistributionDetails" runat="server" Font-Bold="True" Font-Size="8pt"
                                    Text="<----Ckeck Here If no Distribution Details" Enabled="False" Width="302px"
                                    AutoPostBack="True" Visible="false" /></td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                                <asp:Button ID="btnFinalize" runat="server" OnClick="btnFinalize_Click" Text="Submit"
                                    Visible="false" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
