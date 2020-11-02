<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="IssueAgainst_DOfor_Millers.aspx.cs" Inherits="IssueCenter_IssueAgainst_OpenSales_DO" Title="DO Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">
        function CheckIsNumeric(e, tx) {
            var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
            if ((AsciiCode < 46 && AsciiCode != 8) || (AsciiCode > 57) || (AsciiCode == 47)) {
                alert('Please enter only numbers.');
                return false;
            }
            var num = tx.value;
            var len = num.length;
            var indx = -1;
            indx = num.indexOf('.');
            if (indx != -1) {
                if ((AsciiCode == 46)) {
                    alert('Point must be apear only one time.');
                    return false;
                }
                var dgt = num.substr(indx, len);
                var count = dgt.length;
                //alert (count);
                if (count > 5 && AsciiCode != 8) {
                    alert("Only 5 decimal digits allowed.");
                    return false;
                }
            }
        }
    </script>

    <style type="text/css">
     .ButtonClass {
            cursor: pointer;
        }
    </style>

    <div>
        <asp:Panel ID="panel_do" runat="server" Width="641px">
            <table>
                <tr>
                    <td align="center"
                        style="background-color: #808000; height: 21px; width: 624px;">
                        <asp:Label ID="Label27" runat="server" Font-Bold="True"
                            Text="Issue Against DO for Miller"
                            Style="font-family: Georgia; color: #333300; text-decoration: underline; font-size: large; background-color: #FFFFFF;"></asp:Label></td>
                </tr>
                <tr>
                    <td style="vertical-align: top; text-align: left; height: 130px; font-family: Georgia; font-size: small; width: 624px;">
                        <fieldset style="height: 100px; width: 600px;">
                            <table style="width: 629px; background-color: lightblue;" border="1"
                                cellpadding="0" cellspacing="0">
                                <tr>
                                    <td colspan="4"
                                        style="font-size: 10pt; position: static; background-color: #ffffcc; text-align: center;"
                                        align="left">
                                        <asp:Label ID="Label1" runat="server" ForeColor="#0000C0" Font-Size="Medium" Font-Italic="True"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc; width: 119px;">Crop Year</td>
                                    <td style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc; width: 198px;">
                                        <asp:TextBox ID="txtYear" runat="server" Enabled="False" ReadOnly="True" Width="112px"></asp:TextBox>
                                    </td>
                                    <td align="left" style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc">
                                        <asp:Label ID="lbltoissue" runat="server" Text="Issued To"></asp:Label>
                                    </td>
                                    <td align="left"
                                        style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc; width: 232px;">
                                        <asp:DropDownList ID="ddlissueto" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlissueto_SelectedIndexChanged" Width="162px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc; width: 119px;">
                                        <asp:Label ID="lbldono" runat="server" ForeColor="Red" Text="Delivery Order No." Width="120px" Style="color: #000000"></asp:Label>
                                    </td>
                                    <td style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc; width: 198px;">
                                        <asp:DropDownList ID="ddl_do_no" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_do_no_SelectedIndexChanged" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc">Lot Number</td>
                                    <td align="left" style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc; width: 232px;">
                                        <asp:TextBox ID="txtLotNumber" runat="server" Enabled="False" ReadOnly="True" Width="112px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc; width: 119px;">
                                        <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="Party/Miller Name"
                                            Width="99px" Style="color: #000000"></asp:Label>
                                    </td>
                                    <td style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc" colspan="3">
                                        <asp:DropDownList ID="ddlpartyname" runat="server" Enabled="False"
                                            OnSelectedIndexChanged="ddlissueto_SelectedIndexChanged" Width="470px">
                                        </asp:DropDownList>
                                    </td>

                                </tr>
                                <tr>
                                    <td style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc; width: 119px;">
                                        <asp:Label ID="lbldodate" runat="server" Text="DO From Date" Width="98px"></asp:Label>
                                    </td>
                                    <td style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc; width: 198px;">
                                        <asp:TextBox ID="tx_do_date" runat="server" BackColor="#E0E0E0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Width="112px"></asp:TextBox>
                                        <span style="color: #000000; font-size: 10px">(dd/mm/yyyy)</span></td>
                                    <td align="left" style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc">
                                        <asp:Label ID="lbldodate0" runat="server" Text="DO Validity Date" Width="103px"></asp:Label>
                                    </td>
                                    <td align="left" style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc; width: 232px;">
                                        <asp:TextBox ID="txtValidDate" runat="server" BackColor="#E0E0E0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Width="112px"></asp:TextBox>
                                        <span style="font-size: 10px; color: #000000">(dd/mm/yyyy)</span></td>
                                </tr>
                                <tr>
                                    <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 18px; width: 119px;" align="left">
                                        <asp:Label ID="lblQuantity" runat="server" Text="DO Quantity" Width="85px"></asp:Label>
                                        <br />
                                        <span style="color: #FF0000">(Common Dhan)</span></td>
                                    <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 18px; width: 198px;">
                                        <asp:TextBox ID="tx_do_qty" runat="server" BackColor="#E0E0E0"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True"
                                            Width="112px"></asp:TextBox>
                                        <asp:Label ID="Label28" runat="server" ForeColor="#CC0000"
                                            Style="text-align: center; font-size: small; color: #000000;" Text="in Qtls."></asp:Label>
                                    </td>
                                    <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 18px;" align="left">
                                        <asp:Label ID="lblQuantity0" runat="server" Text="DO Quantity" Width="85px"></asp:Label>
                                        <br />
                                        <span style="color: #FF0000">(Grade-A Dhan)</span></td>
                                    <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 18px; width: 232px;"
                                        align="left">
                                        <asp:TextBox ID="txt_DOGradeADhan" runat="server" BackColor="#E0E0E0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Width="112px" Style="text-align: right"></asp:TextBox>
                                        <asp:Label ID="Label31" runat="server" ForeColor="#CC0000" Style="text-align: center; font-size: small; color: #000000;" Text="in Qtls."></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="font-size: 10pt; position: static; background-color: #ffffcc; height: 18px; width: 119px;">
                                        <asp:Label ID="lbl_balqty" runat="server" Text="Bal Quantity"></asp:Label>
                                        <br />
                                        <span style="color: #FF0000">(Common Dhan)</span></td>
                                    <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 18px; width: 198px;">
                                        <asp:TextBox ID="tx_balance_qty" runat="server" BackColor="#E0E0E0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Width="112px"></asp:TextBox>
                                        <asp:Label ID="Label30" runat="server" ForeColor="#CC0000" Style="text-align: center; font-size: small; color: #000000;" Text="in Qtls."></asp:Label>
                                    </td>
                                    <td align="left" style="font-size: 10pt; position: static; background-color: #ffffcc; height: 18px;">
                                        <asp:Label ID="lbl_balqty0" runat="server" Text="Bal Quantity"></asp:Label>
                                        <br />
                                        <span style="color: #FF0000">(Grade-A Dhan)</span></td>
                                    <td align="left" style="font-size: 10pt; position: static; background-color: #ffffcc; height: 18px; width: 232px;">
                                        <asp:TextBox ID="tx_balanceGradeADhan" runat="server" BackColor="#E0E0E0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Width="112px" Style="text-align: right"></asp:TextBox>
                                        <asp:Label ID="Label32" runat="server" ForeColor="#CC0000" Style="text-align: center; font-size: small; color: #000000;" Text="in Qtls."></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left"
                                        style="font-size: 10pt; position: static; background-color: #ffffcc; height: 18px; width: 119px;">
                                        <asp:Label ID="lblCommodity" runat="server" Text="Commodity"></asp:Label>
                                    </td>
                                    <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 18px; width: 198px;">
                                        <asp:DropDownList ID="ddl_commodity" runat="server" Enabled="False"
                                            ValidationGroup="1" Width="162px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left"
                                        style="font-size: 10pt; position: static; background-color: #ffffcc; height: 18px;">
                                        <asp:Label ID="lblScheme" runat="server" Text="Scheme" Width="63px"></asp:Label>
                                    </td>
                                    <td align="left"
                                        style="font-size: 10pt; position: static; background-color: #ffffcc; height: 18px; width: 232px;">
                                        <asp:DropDownList ID="ddl_scheme" runat="server" Enabled="False" Width="162px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="4"
                                        style="font-size: 10pt; position: static; background-color: #ffffcc; height: 20px;">
                                        <asp:HiddenField ID="hdfMillingType" runat="server" />
                                        <asp:HiddenField ID="hdfDistName" runat="server" />
                                        <asp:HiddenField ID="hdfServerDate" runat="server" />
                                        <asp:HiddenField ID="hdfICName" runat="server" />
                                        <asp:HiddenField ID="hdfChallanNo" runat="server" />
                                         <asp:HiddenField ID="hdfDONumber" runat="server" />
                                         <asp:HiddenField ID="hdfGodown" runat="server" />
                                        <asp:HiddenField ID="hdfAgrmtID" runat="server" />
                                          <asp:HiddenField ID="hdfArvaChawalRs" runat="server" />
                                        <asp:HiddenField ID="hdfUshnaChawalRs" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </fieldset>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>

                </tr>

                <tr>
                    <td style="vertical-align: top; text-align: left; height: 252px; background-color: #FFFFFF; width: 624px;">
                        <fieldset style="height: 177px; font-family: Georgia; font-size: small;">
                            <legend>Issued Details
                            </legend>
                            <table style="background-color: lightsteelblue; width: 630px;" border="1"
                                cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 20px; width: 123px;" align="left">
                                        <asp:Label ID="lbl_issueqty" runat="server" Text="Issued Quantity"></asp:Label>
                                        <br />
                                        <span style="color: #FF0000">(Common Dhan)</span></td>
                                    <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 20px;"
                                        colspan="2">
                                        <asp:TextBox ID="tx_issued_qty" runat="server" ReadOnly="True" Width="112px"
                                            BackColor="#E0E0E0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                        <asp:Label ID="Label33" runat="server" ForeColor="#CC0000" Style="text-align: center; font-size: small; color: #000000;" Text="in Qtls."></asp:Label>
                                    </td>
                                    <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 20px; width: 113px;"
                                        align="left">
                                        <asp:Label ID="lbl_issueqty0" runat="server" Text="Issued Quantity"></asp:Label>
                                        <br />
                                        <span style="color: #FF0000">(Grade-A Dhan)</span></td>
                                    <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 20px;">
                                        <asp:TextBox ID="txt_IssuedGradeADhan" runat="server" BackColor="#E0E0E0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Style="text-align: right" Width="112px"></asp:TextBox>
                                        <asp:Label ID="Label35" runat="server" ForeColor="#CC0000" Style="text-align: center; font-size: small; color: #000000;" Text="in Qtls."></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="font-size: 10pt; position: static; background-color: #ffffcc; height: 20px; width: 123px;">
                                        <asp:Label ID="lblbalqty" runat="server" Text="Balance Qty."></asp:Label>
                                        <br />
                                        <span style="color: #FF0000">(Common Dhan)</span></td>
                                    <td colspan="2" style="font-size: 10pt; position: static; background-color: #ffffcc; height: 20px;">
                                        <asp:TextBox ID="tx_issue_balqty" runat="server" BackColor="#E0E0E0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Width="112px"></asp:TextBox>
                                        <asp:Label ID="Label34" runat="server" ForeColor="#CC0000" Style="text-align: center; font-size: small; color: #000000;" Text="in Qtls."></asp:Label>
                                    </td>
                                    <td align="left" style="font-size: 10pt; position: static; background-color: #ffffcc; height: 20px; width: 113px;">
                                        <asp:Label ID="lbl_balqty1" runat="server" Text="Bal Quantity"></asp:Label>
                                        <br />
                                        <span style="color: #FF0000">(Grade-A Dhan)</span></td>
                                    <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 20px;">
                                        <asp:TextBox ID="txt_IssuedbalanceGradeADhan" runat="server" BackColor="#E0E0E0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Style="text-align: right" Width="112px"></asp:TextBox>
                                        <asp:Label ID="Label36" runat="server" ForeColor="#CC0000" Style="text-align: center; font-size: small; color: #000000;" Text="in Qtls."></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="background-color: #FFCCFF;">
                                        <asp:Label ID="lbldispsource" runat="server" Text="Stock Issued From"></asp:Label></td>
                                    <td style="background-color: #FFCCFF;" colspan="2">
                                        <asp:DropDownList ID="ddlsarrival" runat="server"
                                            OnSelectedIndexChanged="ddlsarrival_SelectedIndexChanged" Width="162px" Enabled="False">
                                        </asp:DropDownList></td>
                                    <td align="left"
                                        style="background-color: #FFCCFF; width: 113px;">
                                        &nbsp;</td>
                                    <td
                                        style="background-color: #FFCCFF;">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="left" style="background-color: #FFCCFF;">
                                        <asp:Label ID="lblBranchName" runat="server" Text="Branch Name"></asp:Label>
                                    </td>
                                    <td colspan="2" style="background-color: #FFCCFF;">
                                        <asp:DropDownList ID="ddlBranch" runat="server" Width="162px" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" Enabled="False">
                    </asp:DropDownList>
                                    </td>
                                    <td align="left" style="background-color: #FFCCFF; width: 113px;">
                                        <asp:Label ID="lblGodownNo" runat="server" Text="Godown"></asp:Label>
                                    </td>
                                    <td style="background-color: #FFCCFF;">
                                        <asp:DropDownList ID="ddl_godown" runat="server" AutoPostBack="True" Enabled="False" OnSelectedIndexChanged="ddl_godown_SelectedIndexChanged" Width="180px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="background-color: #FFCCFF;">
                                        <asp:Label ID="lblbalcomdty" runat="server" Text="Current Balance of Commodity at IssueCentre"></asp:Label></td>
                                    <td align="left" colspan="3" style="background-color: #FFCCFF;">
                                        <asp:TextBox ID="tx_cur_bal" runat="server" BackColor="#E0E0E0" ReadOnly="True"
                                            Width="227px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                        <asp:Label ID="Label37" runat="server" ForeColor="#CC0000" Style="text-align: center; font-size: small; color: #000000;" Text="in Qtls."></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background-color: #FFCCFF;" align="left">
                                        <asp:Label ID="lblDispatchQty" runat="server" Text="Quantity To Issue"></asp:Label>
                                        <br />
                                        <span style="color: #FF0000">(Common Dhan)</span></td>
                                    <td style="background-color: #FFCCFF;">
                                        <asp:TextBox ID="tx_qty_to_issue" runat="server" Width="112px" MaxLength="13"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" AutoComplete="off"></asp:TextBox>*&nbsp;<asp:Label
                                                ID="Label29" runat="server" ForeColor="#CC0000"
                                                Style="text-align: center; font-size: small; color: #000000;" Text="in Qtls."></asp:Label>
                                    </td>

                                    <td style="background-color: #FFCCFF;"
                                        align="left" colspan="2">
                                        <span style="color: #FF0000">
                                        <asp:Label ID="lblDispatchQty0" runat="server" style="color: #000000" Text="Quantity To Issue"></asp:Label>
                                        <br />
                                        (Grade-A Dhan)</span></td>
                                    <td style="background-color: #FFCCFF;" align="left">
                                        <asp:TextBox ID="tx_qty_to_issueGradeA" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" MaxLength="13" Width="112px" Style="text-align: right" AutoComplete="off" ></asp:TextBox>
                                        <asp:Label ID="Label38" runat="server" ForeColor="#CC0000" Style="text-align: center; font-size: small; color: #000000;" Text="in Qtls."></asp:Label>
                                    </td>

                                </tr>


                                <tr>
                                    <td align="left" style="background-color: #FFCCFF;">
                                        <asp:Label ID="lblBagNumber" runat="server" Text="No of Bags"></asp:Label>
                                    </td>
                                    <td style="background-color: #FFCCFF;">
                                        <asp:TextBox ID="tx_bags" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" class="Number" MaxLength="4" Width="112px" AutoComplete="off"></asp:TextBox>
                                    </td>
                                    <td align="left" colspan="2" style="background-color: #FFCCFF;">&nbsp;</td>
                                    <td align="left" style="background-color: #FFCCFF;">&nbsp;</td>
                                </tr>


                                <tr>
                                    <td align="left" style="background-color: #FFCCFF;">
                                        <asp:Label ID="lblTruckNumber" runat="server" Text="Truck No."></asp:Label>
                                    </td>
                                    <td style="background-color: #FFCCFF;">
                                        <asp:TextBox ID="tx_gatepass" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" MaxLength="70" Width="112px" AutoComplete="off"></asp:TextBox>
                                    </td>
                                    <td align="left" colspan="2" style="background-color: #FFCCFF;">
                                        <asp:Label ID="lblissuedate" runat="server" Text="Issued Date"></asp:Label>
                                    </td>
                                    <td align="left" style="background-color: #FFCCFF;">
                                        <asp:TextBox ID="tx_issued_date" runat="server" BorderColor="Black"
                                            BorderStyle="Solid" BorderWidth="1px" Width="112px"></asp:TextBox>*
                                        <script type="text/javascript">
                                            new tcal({
                                                'formname': '0',
                                                'controlname': 'ctl00_ContentPlaceHolder1_tx_issued_date'
                                            });
                                        </script>
                                    </td>
                                </tr>


                            </table>
                            <table border="1" cellpadding="0" cellspacing="0"
                                style="background-color: lightsteelblue; width: 629px;" >
                          
                                <tr>
                                    <td colspan="4"
                                        style="background-color: #808000; text-align: center;">
                                        <asp:Button ID="btn_new" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Bold="False" Font-Size="Medium" OnClick="btn_new_Click" Style="font-family: Calibri; font-size: large" Text="New" CssClass="ButtonClass" ValidationGroup="1" Width="88px" CausesValidation="False" />
                                        <asp:Button ID="btnsave" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Bold="False" Font-Size="Medium" OnClick="btnsave_Click" Text="Save" ValidationGroup="1" Width="88px" Style="font-family: Calibri; font-size: large" CssClass="ButtonClass" />
                                        <asp:Button ID="btnPrint" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Bold="False" Font-Size="Medium" Style="font-family: Calibri; font-size: large" Text="Print" ValidationGroup="1" Width="88px" CausesValidation="False" CssClass="ButtonClass" OnClick="btnPrint_Click" Enabled="False" />
                                        <asp:Button ID="btnclose" runat="server" BorderColor="Black"
                                            BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium"
                                            OnClick="btnclose_Click" Text="Close" Width="95px"
                                            Style="font-family: Calibri; font-size: large" CausesValidation="False" CssClass="ButtonClass" />
                                    </td>
                                    <td style="background-color: #808000; text-align: center;">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </fieldset>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tx_qty_to_issue"
                            ErrorMessage="Please enter quantity to issue" Height="0px" ValidationGroup="1"
                            Width="1px">*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tx_bags"
                            ErrorMessage="Please enter no of bags" Height="0px" ValidationGroup="1" Width="0px">*</asp:RequiredFieldValidator>
                        &nbsp; &nbsp; &nbsp;&nbsp;
                <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                    Font-Size="Small" ShowMessageBox="True" ShowSummary="False" Style="z-index: 101"
                    ValidationGroup="1" Width="365px" />
                    </td>
                </tr>
            </table>

        </asp:Panel>

    </div>

</asp:Content>

