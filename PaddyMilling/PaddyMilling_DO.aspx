<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="PaddyMilling_DO.aspx.cs" Inherits="PaddyMilling_PaddyMilling_DO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" lang="javascript" src="Scripts/jquery-2.1.1.js"></script>

    <%--For Calendar Controls--%>
    <link href="calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="calendar/calendar.js"></script>


    <%--Allow Only Number & One Decimal & 2 digit after decimal using 
        onblur="extractNumber(this,2,true);"
        onkeyup="extractNumber(this,2,true);"
        onkeypress="return blockNonNumbers(this, event, true, true);" --%>

    <%--Allow Only Number & One Decimal & 2 digit after decimal--%>
    <script type="text/javascript" lang="javascript" src="Scripts/Number2D.js"></script>

    <script type="text/javascript" language="javascript">
        function disableRefresh(evt) {

            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if (event.keyCode == 116) {
                evt.keyCode = 0;
                return false
            }
        }

        document.onkeydown = disableRefresh;

    </script>



    <style type="text/css">
        .tblnew {
            width: 800px;
            text-align: left;
            border-style: solid;
            border-width: 2px;
            border-color: black;
        }

            .tblnew th {
                /*font-weight: 700;*/
                border-bottom: 1px solid #ddd;
                text-align: left;
                border-top: 1px solid #ddd;
                border-right: 1px solid #ddd;
                background: #669999;
                color: #FFFFFF;
                /*font-family: georgia;*/
                /*font-size: 17px;*/
                border-style: solid;
                border-color: #66FFFF;
            }

            .tblnew td, .tblnew th {
                padding: 2px 2px;
                border-left: 1px solid #cbe2eb;
                border-right: 1px solid #cbe2eb;
                border-bottom: 1px solid #cbe2eb;
                font-size: 14px;
            }

        .category_table td {
            text-align: left;
        }

        .NSC {
            width: 500px;
        }

        .ButtonClass {
            cursor: pointer;
        }

        .auto-style2 {
            font-weight: bold;
            font-size: large;
        }

        .auto-style3 {
            text-decoration: underline;
            color: #FFFFFF;
            font-size: large;
        }

        .auto-style6 {
            color: #FFFFFF;
        }

        .auto-style7 {
            font-size: large;
        }

        .auto-style8 {
            width: 288px;
        }

        .auto-style9 {
            font-size: xx-small;
        }

        .auto-style10 {
            font-size: small;
        }
    </style>

    <div>

        <table class="tblnew">
            <tr>
                <td colspan="4" style="text-align: center; background-color: #996633;" class="auto-style2"><span class="auto-style3">*Delivery Order</span><span class="auto-style6"> <span class="auto-style7">(PaddyMilling)*</span></span>
                    <input id="hdfArvaChawalRs" type="hidden" runat="server" />
                    <input id="hdfUshnaChawalRs" type="hidden" runat="server" />
                    <input id="hdfTotalLotCount" type="hidden" runat="server" />
                    <input id="hdfMillingType" type="hidden" runat="server" />
                    <input id="hdfLotOnlyNumber" type="hidden" runat="server" />
                    <input id="hdfDistanceDist" type="hidden" runat="server" />
                    <input id="hdfMappedDist" type="hidden" runat="server" />
                    <input id="hdfFCI" type="hidden" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: center">
                    <asp:Label ID="lblDONumber" runat="server" Font-Size="Medium" ForeColor="Red" Visible="False" Style="font-weight: 700"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">District</td>
                <td class="auto-style8" style="text-align: left">
                    <asp:TextBox ID="txtDistManager" runat="server" Width="137px" Style="margin-left: 0px" ReadOnly="True" Enabled="False"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDistManager" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

                </td>
                <td style="text-align: left">Crop Year</td>
                <td style="text-align: left">

                    <asp:TextBox ID="txtYear" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>

                </td>
            </tr>


            <tr>
                <td style="text-align: left">मिल का नाम</td>
                <td class="auto-style8" style="text-align: left">
                    <asp:DropDownList ID="ddlMillName" runat="server" Height="27px" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="ddlMillName_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlMillName" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

                </td>
                <td style="text-align: left">अनुबंध नंबर</td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlAgtmtNumber" runat="server" Height="27px" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlAgtmtNumber_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlAgtmtNumber" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

                </td>
            </tr>


            <tr id="FCI" runat="server" visible="false" style="background-color: #CCFF99; color: #FFFFFF; text-align: center">
                <td colspan="4" style="color: #FFFFFF; background-color: #000000">इस अनुबंध की धान का CMR सिर्फ
                <asp:Label ID="lblDist" runat="server" Text="" Style="color: #FFFF00"></asp:Label>
                    की FCI में ही जमा होगा|</td>
            </tr>

            
            <tr>
                <td style="text-align: left">कामन धान<b> (Qtls)</b></td>
                <td class="auto-style8" style="text-align: left">
                    <asp:TextBox ID="txtTotalCDhan" runat="server" class="NumberDecimal" MaxLength="10" Width="137px" AutoComplete="off" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>

                    &nbsp;<b>(Total)</b></td>
                <td style="text-align: left">ग्रेड-ए धान<b> (Qtls)</b></td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtTotalGDhan" runat="server" class="NumberDecimal" MaxLength="10" Width="137px" AutoComplete="off" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>
                    <b>&nbsp;(Total)</b></td>
            </tr>

            <tr>
                <td style="text-align: left">कामन धान<b> (Qtls)</b></td>
                <td class="auto-style8" style="text-align: left">
                    <asp:TextBox ID="txtRemCommonDhan" runat="server" class="NumberDecimal" MaxLength="10" Width="137px" AutoComplete="off" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>

                    &nbsp;<b>(Rem.)</b></td>
                <td style="text-align: left">ग्रेड-ए धान<b> (Qtls)</b></td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtRemGradeADhan" runat="server" class="NumberDecimal" MaxLength="10" Width="137px" AutoComplete="off" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>
                    <b>&nbsp;(Rem.)</b></td>
            </tr>

            <tr>
                <td style="text-align: left">धान का प्रदाय</td>
                <td class="auto-style8" style="text-align: left">
                    <asp:DropDownList ID="ddlDhanLot" runat="server" Height="26px" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlDhanLot_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;<b>(Rem. Lot)</b></td>
                <td style="text-align: left"> Lots Allowed </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtRemDhanlLot" runat="server" class="NumberDecimal" MaxLength="10" Width="137px" AutoComplete="off" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>
                    &nbsp;<b><%--(Rem. Lot)--%></b>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: center">
                    Ratio Of Fixed Deposit Receipt(FDR)/Bank Security and Cheques
                </td>
            </tr>
             <tr>
                <td style="text-align: left">FDR/Bank Security</td>
                <td class="auto-style8" style="text-align: left">
                     <asp:TextBox ID="txtFDRBS" runat="server" class="NumberDecimal" MaxLength="10" Width="137px" AutoComplete="off" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>
                    &nbsp;<b></b>
                 </td>
                <td style="text-align: left">Cheque</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtCheque" runat="server" class="NumberDecimal" MaxLength="10" Width="137px" AutoComplete="off" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
             <tr>
                <td style="text-align: left">Value of FDR/Bank Security</td>
                <td class="auto-style8" style="text-align: left">
                  <asp:TextBox ID="txtvalueFDR" runat="server" class="NumberDecimal" MaxLength="10" Width="137px" AutoComplete="off" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>
                    &nbsp;<b></b></td>
                <td style="text-align: left">Value of Cheque</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtvalueCheck" runat="server" class="NumberDecimal" MaxLength="10" Width="137px" AutoComplete="off" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>
                    &nbsp;<b></b>
                </td>
            </tr>


            <tr>
                <td style="text-align: left">DO Date</td>
                <td class="auto-style8" style="text-align: left">
                    <asp:TextBox ID="txtFromDate" runat="server" Width="137px" Style="margin-left: 0px" ReadOnly="True"></asp:TextBox>
                    &nbsp;
                    
                    <%--<img src="calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtFromDate', 'restrict=true,instance=single,limit=<%= DateLimit %>'  )" />--%>
                    <img src="calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtFromDate' , 'expiry=true,elapse=-150,restrict=true,close=true')" />

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFromDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                </td>
                <td style="text-align: left">DO Validity Date</td>
                <td style="text-align: left">

                    <asp:TextBox ID="txtToDate" runat="server" Width="137px" ReadOnly="True"></asp:TextBox>
                    &nbsp;
                    <%--<img src="calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtToDate' , 'restrict=true,instance=single,limit=<%= DateLimit %>')" />--%>
                    <img src="calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtToDate' , 'expiry=true,elapse=-150,restrict=false,close=true')" />

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtToDate" ErrorMessage="RequiredFieldValidator" Font-Bold="False" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                </td>
            </tr>
              <tr>
                <td colspan="2" style="text-align: center">
                   <asp:RadioButton ID="rdbGodown" runat="server" Text="Godowns" Checked="false" Enabled="true" AutoPostBack="true" OnCheckedChanged="rdbGodown_CheckedChanged" ></asp:RadioButton>

                </td>
                    <td colspan="2" style="text-align: center">
                   <asp:RadioButton ID="rdbSociety" runat="server" Text="Society" Checked="false" AutoPostBack="true" OnCheckedChanged="rdbSociety_CheckedChanged" ></asp:RadioButton>

                </td>
            </tr>
            <tr >
                <td style="text-align: left"><%--प्रदाय केंद्र का नाम--%> <asp:Label ID="lblsocietydistGodownIC" runat="server"  Visible="false"></asp:Label></td>
                <td class="auto-style8" style="text-align: left">
                    <asp:DropDownList ID="ddlIssueCentre" Visible="false" runat="server" Height="26px" Width="141px" OnSelectedIndexChanged="ddlIssueCentre_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                     <asp:DropDownList ID="ddlsocietydist"  Visible="false"  runat="server" Height="26px" Width="141px" OnSelectedIndexChanged="ddlsocietydist_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td style="text-align: left"><%--गोदाम का नाम--%>  <asp:Label ID="lblsociety_Godownname" runat="server"  Visible="false"> </asp:Label></td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlGodown" Visible="false" runat="server" Height="26px" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">
                    </asp:DropDownList>
                     <asp:DropDownList ID="ddlsociety" Visible="false" runat="server" Height="26px" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlsociety_SelectedIndexChanged" >
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td runat="server" visible="false" id="tdRemgodownqty"   style="text-align: left" class="auto-style9"><span class="auto-style10">Mapping के आधार पर</span>
                    <br />
                    <span class="auto-style10">गोदाम में बची हुई मात्रा</span></td>
                <td runat="server" visible="false" id="tdremqtygo"  class="auto-style8" style="text-align: left">
                    <asp:TextBox ID="txtGodownMapRemQty" runat="server" Width="137px" ReadOnly="True" Style="text-align: right" Enabled="False"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtGodownMapRemQty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                </td>
                <td style="text-align: left">लॉट क्रमांक</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtLotNumber" runat="server" class="alphaNumericWithoutSpace" Width="137px" AutoComplete="off" MaxLength="30" Style="text-align: right" Enabled="False"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtLotNumber" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td style="text-align: left; background-color: #CCFF99; font-size: medium">Rem. Qty <strong>(Qtls)</strong></td>
                <td style="text-align: left; background-color: #CCFF99; font-size: medium">
                    <asp:TextBox ID="txtQuantity" runat="server" Width="137px" ReadOnly="True" Style="text-align: right" Enabled="False"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtQuantity" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                </td>
                <td style="text-align: left; background-color: #CCFF99; font-size: medium">Add Qty<strong> (Qtls)</strong></td>
                <td style="text-align: left; background-color: #CCFF99; font-size: medium">
                    <asp:TextBox ID="txtQty" runat="server" Width="81px" Style="text-align: right" MaxLength="10" AutoComplete="off"
                        onblur="extractNumber(this,2,true);"
                        onkeyup="extractNumber(this,2,true);"
                        onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;" Enabled="False"></asp:TextBox>

                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="NAN" ControlToValidate="txtQty" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                    <asp:Button ID="btnAdd" runat="server" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="22px" Text="Add" Width="50px" CssClass="ButtonClass" BackColor="Silver" ForeColor="Red" Style="margin-left: 1px" OnClick="btnAdd_Click" />

                </td>
            </tr>
        </table>

        <table style="width: 800px; border-style: solid; border-width: 1px;" border="1" runat="server" visible="false" id="tblgodown">
            <tr>
                <td>
                    <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" ShowFooter="True" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                        OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField ReadOnly="true" HeaderText="Lot No.">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ICName" HeaderText="Issue Center">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="GodownName" HeaderText="Godown" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <FooterStyle HorizontalAlign="Right"></FooterStyle>

                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">

                                <FooterStyle HorizontalAlign="Right"></FooterStyle>

                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>

                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Remove">
                                <ItemTemplate>
                                    <asp:LinkButton ID="iddelete" runat="server" CausesValidation="false" CommandName="Delete" OnClientClick="return confirm('Are You Sure You Want To Remove?');" Text="Remove"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    </asp:GridView>
                </td>
            </tr>

        </table>
         <table style="width: 800px; border-style: solid; border-width: 1px;" border="1" runat="server" visible="false" id="tblsociety">
            <tr>
                <td>
                    <asp:GridView ID="GridView2" Width="100%" runat="server" AutoGenerateColumns="False" ShowFooter="True" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                        OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField ReadOnly="true" HeaderText="Lot No.">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SocietyDist" HeaderText="Society District">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="SocietyName" HeaderText="Society " HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <FooterStyle HorizontalAlign="Right"></FooterStyle>

                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">

                                <FooterStyle HorizontalAlign="Right"></FooterStyle>

                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>

                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Remove">
                                <ItemTemplate>
                                    <asp:LinkButton ID="iddelete" runat="server" CausesValidation="false" CommandName="Delete" OnClientClick="return confirm('Are You Sure You Want To Remove?');" Text="Remove"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    </asp:GridView>
                </td>
            </tr>

        </table>

        <%--Allow Only Alphabets using class="alphaNumericWithoutSpace" --%>
        <script type="text/javascript" lang="javascript" src="Scripts/Alphabets.js"></script>



        <table align="center" class="NSC">
            <tr>
                <td>
                    <asp:Button ID="btnNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnNew_Click" />
                </td>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit" Width="115px" CssClass="ButtonClass" OnClick="btnSubmit_Click" OnClientClick="return confirm('Are You Sure To Submit Paddy Milling Delivery Order?');" Enabled="False" />
                </td>
                <td>
                    <asp:Button ID="btnPrint" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Print" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnPrint_Click" Enabled="False" />
                </td>
                <td>
                    <asp:Button ID="btnClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnClose_Click" />
                </td>
            </tr>
        </table>


    </div>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

