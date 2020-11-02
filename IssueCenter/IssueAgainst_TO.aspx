<%@ Page Title="Issue Against TO" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="IssueAgainst_TO.aspx.cs" Inherits="IssueCenter_IssueAgainst_TO" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
    

<div>
<asp:Panel ID="panel_do" runat ="server" 
        BorderColor="#66CCFF" >
        <table>
        <tr><td align="center" 
                
                
                style="border: medium groove #000000; background-color: #0099CC; width: 553px; font-family: Calibri; font-size: large; font-weight: bold; color: #FFFFFF; height: 25px;">
            <asp:Label ID="Label27" runat="server" Font-Bold="True" 
                Text="ISSUE AGAINST TRANSPORT ORDER"></asp:Label></td></tr>
            <tr>
                <td style="border: thin groove #FFCCCC; vertical-align: top; text-align: left; width: 700px;" 
                    bgcolor="#FFFFCC">
                <fieldset style="width: 704px"  >
                <legend style="border: thin groove #000000; font-family: Calibri; background-color: #FFCCFF; font-weight: 700; color: #3333CC;">Transport Order Details
                </legend>
                    <table style="margin: 2px; border: thin groove #FFCC99; width: 650px; background-color: #99FF99; font-family: calibri; font-size: small;" 
                        border="1" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td colspan="8" >
                                <asp:Label ID="Label1" runat="server" Font-Italic="True" Font-Size="Medium" 
                                    ForeColor="#0000C0" style="color: #003366"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center">
                                <asp:Label ID="lblallotmonth" runat="server" Text="Allot. Month :"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:DropDownList ID="ddlmonth" runat="server" 
                                    onselectedindexchanged="ddlmonth_SelectedIndexChanged" Width="138px" 
                                    AutoPostBack="True">
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
                                </asp:DropDownList>
                            </td>
                            <td colspan="2" style="text-align: center">
                                <asp:Label ID="lblallotyear" runat="server" Text="Allot.Year"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:DropDownList ID="ddlyear" runat="server" AutoPostBack="True" 
                                    OnSelectedIndexChanged="ddlyear_SelectedIndexChanged" Width="133px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbldono" runat="server" ForeColor="Red" style="font-weight: 700; color: #0000FF;" 
                                    Text="Transport Order No"></asp:Label>
                                :</td>
                            <td colspan="2">
                                <asp:DropDownList ID="ddl_TO_no" runat="server" AutoPostBack="True" 
                                    OnSelectedIndexChanged="ddl_do_no_SelectedIndexChanged" Width="182px">
                                </asp:DropDownList>
                            </td>
                            <td align="left" style="text-align: right">
                                <asp:Label ID="lbldodate" runat="server" Text="TO Date :" Width="85px"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="tx_do_date" runat="server" BackColor="#E0E0E0" 
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" 
                                    Width="90px"></asp:TextBox>
                            </td>
                            <td align="left" style="width: 66px" colspan="2">
                                <asp:Label ID="lbldovalidity" runat="server" Text="Validity :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tx_do_validity" runat="server" BackColor="#E0E0E0" 
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" 
                                    style="margin-left: 0px" Width="86px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td  align="left">
                                <asp:Label ID="lbltransporter" runat="server" Text="Transporter :" Width="97px"></asp:Label>
                            </td>
                            <td colspan="7" >
                                <asp:DropDownList ID="ddltransporter" runat="server" Width="270px" onselectedindexchanged="ddltransporter_SelectedIndexChanged" 
                               >
                                </asp:DropDownList>
                                <asp:Label ID="lblQuantity" runat="server" Text="TO  Quantity :" 
                                    Visible="False"></asp:Label>
                                <asp:TextBox ID="tx_do_qty" runat="server" BackColor="#E0E0E0" 
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" 
                                    Visible="False" Width="92px"></asp:TextBox>
                                <asp:Label ID="lbl_balqty" runat="server" Text="Bal Quantity" Visible="False"></asp:Label>
                                <asp:TextBox ID="tx_balance_qty" runat="server" BackColor="#E0E0E0" 
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" 
                                    Visible="False" Width="88px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" height="30px" 
                                >
                                <asp:Label ID="lbl_route" runat="server" Text="Route No. :"></asp:Label>
                            </td>
                            <td colspan="2" >
                                <asp:DropDownList ID="ddl_route" runat="server" 
                                    onselectedindexchanged="ddl_route_SelectedIndexChanged" Width="100px">
                                </asp:DropDownList>
                            </td>
                            <td align="left" colspan="5" 
                                >
                                <asp:DropDownList ID="ddl_allot_month" runat="server" Enabled="False" 
                                    OnSelectedIndexChanged="ddl_allot_month_SelectedIndexChanged" Visible="False">
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
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddd_allot_year" runat="server" Enabled="False" 
                                    Visible="False">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td  
                                align="left" colspan="1">
                                <asp:Label ID="lbl_feed0" runat="server" Text="Feed No. :"></asp:Label>
                            </td>
                            <td align="left" colspan="2" 
                                >
                                <asp:DropDownList ID="ddl_feed" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddl_feed_SelectedIndexChanged" Width="100px">
                                </asp:DropDownList>
                            </td>
                            <td align="left" 
                                >
                                <asp:Label ID="lbl_fps0" runat="server" Text="FPS Name :"></asp:Label>
                            </td>
                            <td align="left" colspan="4" 
                                
                                >
                                <asp:DropDownList ID="ddl_fpsname" runat="server" 
                                    onselectedindexchanged="ddl_fpsname_SelectedIndexChanged" Width="270px" 
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                   
                    <table style="width: 630px;"> 
                        <tr>
                            <td style="border-color: #FFFF99; font-weight: 600; text-align: right; font-family: Calibri; color: #FF99FF; background-color: #FFCCFF;" >
                                <table style="border: thin groove #FFCC99; width: 100%; background-color: #FFCCFF" 
                                    border="1" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 139px; color: #0033CC;  text-align: center;">
                                            <asp:Label ID="lbl_name" runat="server" Text="FPS Name" 
                                                style="font-weight: 700"></asp:Label>
                                            :</td>
                                        <td style="text-align: center;  font-size: medium;">
                                            <asp:Label ID="lbl_fpsname" runat="server" style="color: #000000; " 
                                              ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="color: #0033CC; background-color: #99CCFF">
                                            <asp:Panel ID="Panel3" runat="server" Visible="False">
                                                <table style="width: 100%" border="1" cellpadding="2" cellspacing="2">
                                                    <tr>
                                                        <td style="text-align: left; width: 175px">
                                                            <b><strong>Connected FPS Name:</strong></b></td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lbl_connectedfps" runat="server" 
                                                                style="font-weight: 700; font-size: medium;"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight: 700; text-decoration: underline;">
                                Total Allotment Quantity(In Qtls)</td>
                        </tr>
                      <center>  <tr>
                            <td style="text-align: center">
                                <asp:GridView ID="Grdtotal_fps" runat="server" AutoGenerateColumns="False" 
                                    BackColor="LightGoldenrodYellow" BorderColor="White" BorderWidth="1px" 
                                    CellPadding="2" DataKeyNames="FPSCode" EnableModelValidation="True" 
                                    Font-Bold="False" Font-Size="Small" ForeColor="Black" GridLines="None" 
                                    Width="100%" BorderStyle="Solid" CellSpacing="1">
                                    <AlternatingRowStyle BackColor="PaleGoldenrod" />
                                    <Columns>
                                        <asp:BoundField DataField="FPSCode" HeaderText="FPS Code" Visible="False">
                                        <ItemStyle Font-Bold="False" BackColor="#FFCC66" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="WheatAllot" HeaderText="Wheat" 
                                            SortExpression="wheat">
                                        <ItemStyle Font-Bold="False" BackColor="#FFCC66" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RiceAllot" HeaderText="Rice" 
                                            SortExpression="rice">
                                        <ItemStyle Font-Bold="False" BackColor="#99CCFF" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SugarAllot" HeaderText="Sugar" 
                                            SortExpression="sugar_alloc">
                                        <ItemStyle Font-Bold="False" BackColor="#99FF99" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SaltAllot" HeaderText="Salt" 
                                            SortExpression="salt_alloc">
                                        <ItemStyle Font-Bold="False" BackColor="#CC99FF" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MaizeAllot" HeaderText="Maize" >
                                        <ItemStyle BackColor="#FFCCFF" />
                                        </asp:BoundField>
                                    </Columns>
                                    <FooterStyle BackColor="Tan" />
                                    <HeaderStyle BackColor="Tan" Font-Bold="True" Font-Size="Small" />
                                    <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
                                        HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                </asp:GridView>
                            </td>
                        </tr></center>
                        <tr>
                            <td style="font-weight: 700; text-decoration: underline;">
                                Balance Allotment Quantity(In Qtls))</td>
                            </caption>
                        </tr>
                        <tr>
                            <td>
                                <center>
                                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" 
                                        style="text-align: center" Width="600px">
                                        <asp:GridView ID="Gridissue" runat="server" AutoGenerateColumns="False" 
                                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                                            CellPadding="3" DataKeyNames="FPSCode" EnableModelValidation="True" 
                                            Font-Bold="False" Font-Size="Small" Width="100%">
                                            <RowStyle ForeColor="#000066" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkBoxId" runat="server" AutoPostBack="True" Checked="True" 
                                                            Enabled="False" oncheckedchanged="chkBoxId_CheckedChanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="FPSCode" HeaderText="FPS Code">
                                                <ItemStyle Font-Bold="False" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="fps_Uname" HeaderText="FPS Name" 
                                                    SortExpression="fps_Uname" Visible="False">
                                                <ItemStyle Font-Bold="False" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="WheatAllot" HeaderText="WheatAlloc" 
                                                    SortExpression="wheat"  >
                                                <ItemStyle Font-Bold="False" BackColor="#FFCC66"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="RiceAllot" HeaderText="RiceAlloc" 
                                                    SortExpression="rice" >
                                                <ItemStyle Font-Bold="False" BackColor="#99CCFF" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SugarAllot" HeaderText="SugarAlloc" 
                                                    SortExpression="sugar_alloc">
                                                <ItemStyle Font-Bold="False"  BackColor="#99FF99" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SaltAllot" HeaderText="SaltAlloc" 
                                                    SortExpression="salt_alloc" >
                                                <ItemStyle Font-Bold="False" BackColor="#CC99FF"  />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MaizeAllot" HeaderText="MaizeAlloc" >
                                                <FooterStyle BackColor="#FF9999" />
                                                <ItemStyle BackColor="#FFCCFF" />
                                                </asp:BoundField>
                                            </Columns>
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Size="Small" 
                                                ForeColor="White" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </center>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_cash" runat="server" 
                                    style="color: #FF0000; font-weight: 700; font-size: medium" 
                                    Text="*Please complete cash payment detail for selected fps" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                    <fieldset style="width: 660px">
                    <legend style="border: thin groove #000000; font-weight: 700; color: #FF6600; background-color: #FFCCFF;"
                                            100%" background-color: #CCCCFF; 
                                            font-weight: 700; font-family: Calibri;" >Issued Details </legend>
                    <table border="1" cellpadding="0" cellspacing="0" 
                        
                                            style="border: thin groove #FFCCFF; padding: 1px; margin: 1px; background-color: lightsteelblue; width: 100%; font-family: Calibri;">
                        <tr>
                            <td align="left" 
                                style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px; width: 158px;">
                                <asp:Label ID="lbl_issueqty" runat="server" Text="Issued TO Quantity" 
                                    Visible="False"></asp:Label>
                                <asp:Label ID="lblissuedate" runat="server" style="color: #000000" 
                                    Text="Issued Date"></asp:Label>
                            </td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;  height: 30px;" 
                                colspan="2">
                                <asp:TextBox ID="tx_issued_qty" runat="server" BackColor="#E0E0E0" 
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" 
                                    Visible="False" Width="90px"></asp:TextBox>
                                <asp:TextBox ID="tx_issued_date" runat="server" BorderColor="Black" 
                                    BorderStyle="Solid" BorderWidth="1px" Width="90px"></asp:TextBox>   <script type="text/javascript">



                                                                                                            new tcal({
                                                                                                                'formname': '0',
                                                                                                                'controlname': 'ctl00_ContentPlaceHolder1_tx_issued_date'
                                                                                                            });
	     </script>
                            </td>
                            <td 
                                
                                style="background-color: #CCCCFF; ">
                                <asp:Label ID="lblCommodity" runat="server" style="text-align: right; font-size: small;" 
                                    Text="Commodity" Visible="False"></asp:Label>
                            </td>
                            <td align="left" colspan="7" 
                                
                                style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px; text-align: left;">
                                <asp:DropDownList ID="ddl_commodity" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddl_commodity_SelectedIndexChanged" ValidationGroup="1" 
                                    Visible="False" Width="170px">
                                </asp:DropDownList>
                                <asp:Label ID="lblbalqty" runat="server" Text="Balance TO Qty." Visible="False"></asp:Label>
                                <asp:TextBox ID="tx_issue_balqty" runat="server" BackColor="#E0E0E0" 
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" 
                                    Visible="False" Width="90px"></asp:TextBox>
                                <asp:Label ID="Label29" runat="server" Text="Qtls" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" 
                                style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px;">
                                <asp:Label ID="lbl_Branch" runat="server" Text="Branch"></asp:Label>
                            </td>
                            <td align="left" 
                                
                                style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px;">
                                <asp:DropDownList ID="ddl_branch" runat="server" 
                                    onselectedindexchanged="ddl_branch_SelectedIndexChanged" Width="135px" 
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td align="left" colspan="3" 
                                style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px;">
                                <asp:Label ID="lblGodownNo" runat="server" Text="Godown"></asp:Label>
                            </td>
                            <td colspan="3" 
                                style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px; ">
                                <asp:DropDownList ID="ddl_godown" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddl_godown_SelectedIndexChanged" Width="170px">
                                </asp:DropDownList>
                            </td>
                            <td align="left" colspan="2" 
                                style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px;">
                                <asp:Label ID="lbldispsource" runat="server" Text="Source"></asp:Label>
                            </td>
                            <td align="left" 
                                style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px;">
                                <asp:DropDownList ID="ddlsarrival" runat="server" 
                                    Width="130px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" 
                                
                                style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px;">
                                <asp:Label ID="lblbalcomdty" runat="server" 
                                    Text="Current Balance of Commodity at Godown"></asp:Label>
                            </td>
                            <td align="left" colspan="3" 
                                
                                style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px;">
                                <asp:TextBox ID="tx_cur_bal" runat="server" BackColor="#E0E0E0" 
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" 
                                    Width="90px"></asp:TextBox>
                                Qtls</td>
                            <td align="left" 
                                style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px;">
                                <asp:Label ID="lblNoofBags" runat="server" Text="No of  Bags"></asp:Label>
                            </td>
                            <td align="left" colspan="4" 
                                style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px;">
                                <asp:TextBox ID="tx_cur_bags" runat="server" BackColor="#E0E0E0" 
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" 
                                    Width="90px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" 
                                style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px; width: 158px;">
                                <asp:Label ID="lblDispatchQty" runat="server" Text="Quantity To Issue"></asp:Label>
                            </td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; color: red; width: 130px; height: 30px; margin-left: 80px;" 
                                colspan="2">
                                <asp:TextBox ID="tx_qty_to_issue" runat="server" BorderColor="Black" 
                                    BorderStyle="Solid" BorderWidth="1px" MaxLength="13" Width="90px"></asp:TextBox>
                                *&nbsp;</td>
                            <td align="left" 
                                style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px;">
                                Qtls</td>
                            <td align="left" 
                                
                                style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px; text-align: right; width: 127px;" 
                                colspan="2">
                                <asp:Label ID="lblBagNumber" runat="server" Text="No of Bags"></asp:Label>
                            </td>
                            <td align="left" colspan="5" 
                                
                                style="font-size: 10pt; position: static; background-color: #cfdcc8; color: red; height: 30px;">
                                <asp:TextBox ID="tx_bags" runat="server" BorderColor="Black" 
                                    BorderStyle="Solid" BorderWidth="1px" MaxLength="4" Width="90px">0</asp:TextBox>
                                *</td>
                        </tr>
                        <tr>
                            <td align="left" 
                                style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px; width: 158px;">
                                <asp:Label ID="lblTruckNumber" runat="server" Text="Truck No."></asp:Label>
                            </td>
                            <td colspan="5" 
                                
                                style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px; color: red; margin-left: 80px;">
                                <asp:TextBox ID="tx_gatepass" runat="server" BorderColor="Black" 
                                    BorderStyle="Solid" BorderWidth="1px" MaxLength="70" Width="270px"></asp:TextBox>
                            </td>
                            <td align="left" colspan="3" 
                                
                                style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px; color: red;">
                             
                                <asp:Label ID="Label32" runat="server" Text="Crop Year" style="color: #000000"></asp:Label>
                            </td>
                            <td align="left" colspan="2" 
                                style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px; color: red;">
                                <asp:DropDownList ID="ddlcropyear" runat="server" Width="130px">
                                    <asp:ListItem>2015-2016</asp:ListItem>
                                    <asp:ListItem Value="2014-2015">2014-2015</asp:ListItem>
                                    <asp:ListItem Value="2013-2014">2013-2014</asp:ListItem>
                                    <asp:ListItem Value="2012-2013">2012-2013</asp:ListItem>
                                    <asp:ListItem Value="2011-2012">2011-2012</asp:ListItem>
                                    <asp:ListItem Value="2010-2011">2010-2011</asp:ListItem>
                                    <asp:ListItem Value="2009-2010">2009-2010</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="11" height="50px" 
                                
                                style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px; text-align: center;">
                                <asp:Button ID="btn_add" runat="server" Height="40px" onclick="btn_add_Click" 
                                    style="font-weight: 700; background-color: #FFFFCC" Text="Add Godown" 
                                    CssClass="btn" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="11" 
                                
                                style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 30px;">
                                <asp:Panel ID="Panel2" runat="server" Visible="False">
                                    <center>
                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                            BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" 
                                            CellPadding="3" CellSpacing="1" DataKeyNames="commodityid,Godownid,Source_ID,Branch_id" 
                                            EnableModelValidation="True" Font-Bold="False" Font-Size="Small" 
                                            GridLines="None" Width="500px">
                                            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                                            <Columns>
                                                <asp:BoundField DataField="Godown" HeaderText="Godown Name" 
                                                    SortExpression="commodity">
                                                <ItemStyle Font-Bold="False" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="commodity" HeaderText="Commodity" />
                                                <asp:BoundField DataField="qty_issue" HeaderText="Issued Qunatity" 
                                                    SortExpression="qty">
                                                <ItemStyle Font-Bold="False" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="bags" HeaderText="Bags" SortExpression="scheme_id" />
                                                <asp:BoundField DataField="gate_pass" HeaderText="Truck No.">
                                                <ItemStyle Font-Bold="False" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="crop_year" HeaderText="Crop Year" />
                                                <asp:BoundField DataField="commodityid" HeaderText="commodityid" 
                                                    Visible="False" />
                                                <asp:BoundField DataField="Godownid" HeaderText="Godownid" Visible="False" />
                                                <asp:BoundField DataField="Source_ID" HeaderText="Source_id" Visible="False" />
                                                <asp:BoundField DataField="Branch_id" HeaderText="Branch" Visible="False" />
                                            </Columns>
                                            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" Font-Size="Small" 
                                                ForeColor="#E7E7FF" />
                                        </asp:GridView>
                                    </center>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" 
                                style="font-size: 10pt; position: static; background-color: #cfdcc8; ">
                                <asp:HiddenField ID="hd_check_wheat" runat="server" />
                                <asp:HyperLink ID="hlinkpdo" runat="server" Font-Size="10pt" NavigateUrl="#" 
                                    Visible="False">Print Delivery challan :-</asp:HyperLink>
                                <asp:Label ID="lbl_dcno" runat="server"></asp:Label>
                            </td>
                            <td height="50px" 
                                
                                style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 127px;" 
                                colspan="2">
                                &nbsp;<asp:Button ID="btnsave" runat="server" ForeColor="Black" Height="40px" 
                                    onclick="btnsave_Click" 
                                  style="font-weight: 700; background-color: #FFFFCC" Text="Submit" 
                                    Width="90px" />
                            </td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8;" 
                                colspan="2">
                                <asp:Button ID="btnclose" runat="server" onclick="btnclose_Click" 
                                   style="font-weight: 700; background-color: #FFFFCC" Text="Close" 
                                    Width="90px" Height="40px"  />
                            </td>
                            <td colspan="2" 
                                style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 81px;">
                                &nbsp;</td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 137px;">
                                <asp:Button ID="btn_new" runat="server" BorderColor="Black" BorderStyle="Solid" 
                                    BorderWidth="1px" Font-Bold="False" Font-Size="Medium" OnClick="btn_new_Click" 
                                   style="font-weight: 700; background-color: #FFFFCC" Text="New" 
                                    Width="90px" Height="40px"  />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="11" 
                                style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                <asp:Label ID="lbl_error" runat="server"></asp:Label>
                                <asp:HiddenField ID="hd_check_rice" runat="server" />
                                <asp:HiddenField ID="hd_check_maize" runat="server" />
                                <asp:HiddenField ID="hd_transpoeter" runat="server" />
                                <asp:HiddenField ID="hd_check_sugar" runat="server" />
                                <asp:HiddenField ID="hd_check_salt" runat="server" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
             <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ControlToValidate="tx_qty_to_issue" 
                    ErrorMessage="Please enter quantity to issue" Height="0px" ValidationGroup="1" 
                    Width="1px">*</asp:RequiredFieldValidator>--%>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                    ControlToValidate="tx_bags" ErrorMessage="Please enter no of bags" Height="0px" 
                    ValidationGroup="1" Width="0px">*</asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                    ControlToValidate="tx_gatepass" ErrorMessage="Please enter Truck No." 
                    Height="0px" ValidationGroup="1" Width="0px">*</asp:RequiredFieldValidator>
                &nbsp; &nbsp; &nbsp;&nbsp;<asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                    Font-Size="Small" ShowMessageBox="True" ShowSummary="False" 
                    Style="z-index: 101" ValidationGroup="1" Width="245px" /></td>
                        </tr>
                    </table>
                </fieldset>
                
                </td>
            </tr>
            
        </table>
        </asp:Panel> 
    </div>

</asp:Content>


