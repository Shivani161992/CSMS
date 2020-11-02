<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="PM_FDR_Cheque_Update.aspx.cs" Inherits="PaddyMilling_PM_FDR_Cheque_Update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        function onlyNumbers(evt) {
            var AsciiCode = event.keyCode;
            var txt = evt.value;
            var txt2 = String.fromCharCode(AsciiCode);
            var txt3 = txt2 * 1;
            if ((AsciiCode < 46) || (AsciiCode > 57)) {
                alert('Please enter only numbers.');
                event.cancelBubble = true;
                event.returnValue = false;
            }

            var num = evt.value;
            var len = num.length;
            var indx = -1;
            indx = num.indexOf('.');
            if (indx != -1) {
                var dgt = num.substr(indx, len);
                var count = dgt.length;
                //alert (count);

                if (AsciiCode == 46) {
                    if (num.split(".").length > 1) {
                        alert('दशमलव एक ही बार आ सकता है |');
                        return false;
                    }
                }

                if (count > 5) {
                    alert("Only 5 decimal digits allowed");
                    event.cancelBubble = true;
                    event.returnValue = false;
                }



            }

        }


        //    var e = event || evt; // for trans-browser compatibility
        //    var charCode = e.which || e.keyCode;
        //    if (charCode > 31 && (charCode < 46 || charCode > 57))
        //        return false;
        //    return true;
        //}      
        function onlyNumbersbags(evt) {
            var AsciiCode = event.keyCode;
            var txt = evt.value;
            var txt2 = String.fromCharCode(AsciiCode);
            var txt3 = txt2 * 1;
            if ((AsciiCode <= 46) || (AsciiCode > 57)) {
                alert('Please enter only numbers.');
                event.cancelBubble = true;
                event.returnValue = false;
            }
        }

    </script>
    <script type="text/javascript">
        function Check(textBox, maxLength) {
            if (textBox.value.length > maxLength) {
                alert("Max characters allowed are " + maxLength);
                textBox.value = textBox.value.substr(0, maxLength);
            }
        }
    </script>

    <script>
        function allowOnlyNumber(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
   </script>
    <style>
        .tablepage
        {
            width: 100%;
            height: 100%;
            overflow: scroll;
        }

        .headerpage
        {
            background: rgba(88,89,99, 1);
            width: 100%;
            text-align: center;
            color: #fff;
            letter-spacing: 4px;
            border-radius: 8px;
            height: 25px;
            font-size: 20px;
            font-family: sans-serif;
            font-weight: 500;
        }



        .subtable
        {
            width: 100%;
            background: rgba(134, 136, 147, 0.38);
            border-radius: 8px;
            color: rgb(6, 41, 70);
            letter-spacing: 2px;
            border-collapse: collapse;
            border-color: #fff;
            border-width: 0px;
            height: 70%;
            overflow: scroll;
            font-size: 12px;
        }

        .main
        {
            width: 80%;
            text-align: center;
        }

        .columnpage
        {
            text-align: left;
        }

        .txt
        {
            border-radius: 8px;
            letter-spacing: 2px;
            font-family: sans-serif;
            color: black;
            height: 15px;
            width: 250px;
            text-align: center;
        }

            .txt:focus
            {
                border-color: #4ca4d7;
                box-shadow: inset 0 1px 1px rgba(81, 203, 238, 1), 0 0 1px rgba(81, 203, 238, 1);
                border-collapse: collapse;
            }

        .ddl
        {
            border-radius: 8px;
            letter-spacing: 2px;
            font-family: sans-serif;
            color: black;
            height: 20px;
            width: 250px;
            text-align: center;
        }

            .ddl::selection
            {
                background-color: #062946;
                color: #fff;
            }


            .ddl:focus
            {
                border-color: #4ca4d7;
                box-shadow: inset 0 1px 1px rgba(81, 203, 238, 1), 0 0 1px rgba(81, 203, 238, 1);
                border-collapse: collapse;
            }

        .heading
        {
            background: rgba(134, 136, 147, 0.69);
        }

        .blank
        {
            background: rgba(255,255,255,0.65);
        }

        .button
        {
            display: block;
            margin: 0 auto;
            width: 30%;
            height: 25px;
            letter-spacing: 4px;
            font-size: 15px;
            font-family: sans-serif;
            border-bottom: 5px solid black;
            border-top: none;
            border-left: none;
            border-right: none;
            background: linear-gradient(#062946,black);
            color: white;
            border-radius: 8px;
            box-shadow: 0px 2px 10px grey;
            transition: 150ms ease;
        }

        /*.button:active
            {
                border: none;
                border-bottom: 2px solid black;
                box-shadow: 0px 1px 5px grey;
                background: linear-gradient(black,#062946);
                color: #fff;
            }*/

        .sign
        {
            color: #062946;
            font-size: 15px;
            text-decoration: none;
            letter-spacing: 2px;
        }
    </style>
    <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="../PaddyMilling/calendar/calendarLot.js"></script>
    <table style="width: 1000px; font-size: 12px;">
        <tr>
            <td style="text-align: left; width: 500px">
                <a href="../District/PaddyMillingHome.aspx" class="sign">&#9754 Back
                </a>
            </td>
            <td style="text-align: right; width: 500px">
                <a href="PM_FDR_Cheque_Update.aspx" class="sign">&#8635 New
                </a>
            </td>
        </tr>
    </table>
    <center>
        <table class="tablepage">
            <tr>
                <td class="headerpage">Paddy Milling Add Cheque against FDR
                       <input id="hdfStateCode" type="hidden" runat="server" />
                       <input id="hdfMillDist" type="hidden" runat="server" />
                </td>
            </tr>
        </table>
        <table class="subtable" border="1">
            <%-- <tr class="heading">
                <td style="width: 50%; text-align: center; height: 10px;" colspan="2">कम से कम एक लाट की FDR होना अनिवारिया है| </td>

            </tr>--%>
            <tr>
                <td style="width: 50%; text-align: left; padding-left: 40px">Crop Year
                </td>
                <td style="width: 50%; text-align: left; padding-left: 40px">

                    <asp:DropDownList ID="ddlCropYear" CssClass="ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCropYear_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr runat="server" id="tr1" visible="true">
                <td style="height: 10px;" colspan="2" class="blank"></td>
            </tr>
            <tr>
                <td style="width: 50%; text-align: left; padding-left: 40px">Accepting District
                </td>
                <td style="width: 50%; text-align: left; padding-left: 40px">


                    <asp:TextBox ID="txtMillDist" CssClass="txt" runat="server" AutoPostBack="true" AutoComplete="off" Enabled="false"></asp:TextBox>
                </td>
            </tr>
           
            <tr runat="server" id="trdistspace" visible="true">
                <td style="height: 10px;" colspan="2" class="blank"></td>
            </tr>
            <tr runat="server" id="trmillname" visible="true">
                <td style="width: 50%; text-align: left; padding-left: 40px">Miller Name
                </td>
                <td style="width: 50%; text-align: left; padding-left: 40px">

                    <asp:DropDownList ID="ddlMillName" CssClass="ddl" Style="width: 350px;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMillName_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
              <tr>
                <td style="height: 10px;" colspan="2" class="blank"></td>
            </tr>
            <tr class="heading">
                <td style="width: 50%; text-align: center; height: 10px;">
                       <asp:Label ID="lblmillState" runat="server" Style="text-align: center; font-weight: 700; padding-left: 20px; color: Black;" ForeColor="Black" Visible="False"></asp:Label>
      

                </td>
                <td style="width: 50%; text-align: center; height: 10px;">
                       <asp:Label ID="lblmillDist" runat="server" Style="text-align: center; font-weight: 700; padding-left: 20px; color: Black;" ForeColor="Black" Visible="False"></asp:Label>
      
                </td>
            </tr>
            <tr runat="server" id="tr2" visible="true">
                <td style="height: 10px;" colspan="2" class="blank"></td>
            </tr>
            <tr class="heading">
                <td style="width: 25%; padding-left: 40px; height: 10px;">Minimum Fixed Deposit Receipt (FDR)/ Bank Security :-                    
                    <asp:Label ID="lblMinFDR" runat="server" Style="text-align: center; font-weight: 700; padding-left: 20px; color: Black;" ForeColor="Black" Visible="False"></asp:Label>
                </td>
                <td style="width: 25%; padding-left: 40px; height: 10px;">Maximum Cheques :-                    
                    <asp:Label ID="lblCheck" runat="server" Style="text-align: center; padding-left: 20px; font-weight: 700; color: Black;" ForeColor="Blue" Visible="False"></asp:Label>
                </td>

            </tr>
            <tr runat="server" id="tr7" visible="true">
                <td style="height: 10px;" colspan="2" class="blank"></td>
            </tr>
            <tr runat="server" id="tr4" visible="false">
                <td style="width: 100%; text-align: left; text-align: center;" colspan="2">

                    <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" ShowFooter="True" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                        Style="letter-spacing: 2px; font-family: sans-serif;">
                        <AlternatingRowStyle BackColor="White" />

                        <Columns>
                            <%-- <asp:CommandField HeaderText="Select" ShowSelectButton="True" />--%>


                            <%--  <asp:BoundField DataField="FDR_ChequeID" HeaderText="ID">
                                <HeaderStyle Font-Size="12px" Font-Names="Arial" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="FDR_OR_BankSecu" HeaderText="FDR OR Bank Security">
                                <HeaderStyle Font-Size="12px" Font-Names="Arial" />

                            </asp:BoundField>
                            <asp:BoundField DataField="FDR_Number" HeaderText="FDR/Bank Security Number">
                                <HeaderStyle Font-Size="12px" Font-Names="Arial" />
                            </asp:BoundField>

                            <asp:BoundField DataField="FDR_Value" HeaderText="FDR/ Bank Security Value (Rs.)">
                                <HeaderStyle Font-Size="12px" Font-Names="Arial" />

                            </asp:BoundField>

                            <asp:BoundField DataField="Check_Number" HeaderText="Cheque Number">
                                <HeaderStyle Font-Size="12px" Font-Names="Arial" />

                            </asp:BoundField>
                            <asp:BoundField DataField="Cheque_Value" HeaderText="Cheque Value (Rs.)">
                                <HeaderStyle Font-Size="12px" Font-Names="Arial" />

                            </asp:BoundField>




                            <%--<asp:BoundField DataField="Posting_Place" HeaderText="District_Code"  >
                <HeaderStyle Font-Size="12px" Font-Names="Arial" />
            </asp:BoundField>--%>
                        </Columns>
                        <FooterStyle BackColor="#868893" Font-Bold="True" ForeColor="#062946" HorizontalAlign="Right" VerticalAlign="Middle" />
                        <HeaderStyle BackColor="#868893" Font-Bold="True" ForeColor="#062946" Height="18px" />
                        <PagerStyle BackColor="#DCDCDC" ForeColor="#062946" HorizontalAlign="Center" />
                        <RowStyle BackColor="#DCDCDC" ForeColor="#062946" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    </asp:GridView>

                </td>
            </tr>
            <tr runat="server" id="tr3" visible="true">
                <td style="height: 10px;" colspan="2" class="blank"></td>
            </tr>

            <tr>
                <td style="width: 50%; text-align: left; padding-left: 40px">Total FDR/Bank Security Value (Rs.)  :-
                <asp:Label ID="lblTotalFDR" runat="server" Style="text-align: center; font-weight: 700; padding-left: 20px; color: Black;" ForeColor="Black" Visible="False"></asp:Label>
                </td>
                <td style="width: 50%; text-align: left; padding-left: 40px">Total Cheque Value (Rs.) :-
                    <asp:Label ID="lblTotalCheque" runat="server" Style="text-align: center; font-weight: 700; padding-left: 20px; color: Black;" ForeColor="Black" Visible="False"></asp:Label>
                </td>
            </tr>
             <tr runat="server" id="tr5" visible="true">
                <td style="height: 10px;" colspan="2" class="blank"></td>
            </tr>
            <tr style="text-align:center">
                <td  style="width: 50%; text-align: left; padding-left: 40px"> Already Added Cheque Total value (Rs) :-
                                        <asp:Label ID="lblAddChequVal" runat="server" Style="text-align: center; font-weight: 700; padding-left: 20px; color: Black;" ForeColor="Black" Visible="False"></asp:Label>

                </td>
                <td  style="width: 50%; text-align: left; padding-left: 40px"> Balance Cheque to maintain Ratio (Rs.) :-
                                        <asp:Label ID="lblBalancecheque" runat="server" Style="text-align: center; font-weight: 700; padding-left: 20px; color: Black;" ForeColor="Black" Visible="False"></asp:Label>

                </td>
               
            </tr>
             <tr runat="server" id="tr6" visible="false">
                <td style="height: 10px;" colspan="2" class="blank"></td>
            </tr>
             <tr class="heading" style="text-align:center" runat="server" id="tr19" visible="false">
                <td colspan="2" style="width: 25%; padding-left: 40px; height: 10px;">Do You Want to submit Balance Cheque aganist the total FDR?        <asp:CheckBox ID="chkbalcheque" runat="server" AutoPostBack="true" OnCheckedChanged="chkbalcheque_CheckedChanged" Checked="false" />           
                </td>
             

            </tr>
              <tr runat="server" id="tr8" visible="false">
                <td style="height: 10px;" colspan="2" class="blank"></td>
            </tr>
            <tr runat="server" id="tr13" visible="false">
                  <td style="width: 25%; text-align: left; padding-left: 40px;">Cheque Bank
                </td>
                <td style="width: 25%; text-align: left; padding-left: 40px;">
                    <asp:DropDownList ID="ddlcheckbank" CssClass="ddl" Style="width: 300px;" runat="server"></asp:DropDownList>
                </td>
            </tr>
             <tr runat="server" id="tr9" visible="false">
                <td style="height: 10px;" colspan="2" class="blank"></td>
            </tr>
            <tr runat="server" id="tr14" visible="false">
                  <td style="width: 25%; text-align: left; padding-left: 40px;">Cheque IFSC Code
                </td>
                <td style="width: 25%; text-align: left; padding-left: 40px;">
                    <asp:TextBox ID="txtcheckifsc" CssClass="txt" runat="server" AutoPostBack="false" AutoComplete="off" Enabled="false"></asp:TextBox>
                </td>
            </tr>
             <tr runat="server" id="tr10" visible="false">
                <td style="height: 10px;" colspan="2" class="blank"></td>
            </tr>
            <tr runat="server" id="tr15" visible="false">
                  <td style="width: 25%; text-align: left; padding-left: 40px;">Cheque Number
                </td>
                <td style="width: 25%; text-align: left; padding-left: 40px;">
                    <asp:TextBox ID="txtChequeNumber" CssClass="txt" runat="server" AutoPostBack="true" AutoComplete="off" Enabled="true" OnTextChanged="txtChequeNumber_TextChanged" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>
                </td>
            </tr>
             <tr runat="server" id="tr11" visible="false">
                <td style="height: 10px;" colspan="2" class="blank"></td>
            </tr>
            <tr runat="server" id="tr16" visible="false">
                  <td style="width: 25%; text-align: left; padding-left: 40px;">Date of Cheque 
                </td>
                <td style="width: 25%; text-align: left; padding-left: 40px;">
                   
                     <asp:TextBox ID="txtCheckDate" CssClass="txt" runat="server" AutoPostBack="true" AutoComplete="off" Enabled="true"></asp:TextBox>
                    <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtCheckDate' , 'expiry=true,elapse=-150,restrict=true,close=true')" />


                     </td>
            </tr>
             <tr runat="server" id="tr12" visible="false">
                <td style="height: 10px;" colspan="2" class="blank"></td>
            </tr>
             <tr class="heading" id="trmessage" visible="false" runat="server">
                <td colspan="4" style="width: 25%; text-align: center; height: 10px;">                   
                    <asp:Label ID="lblmessage" runat="server" Style="text-align: center; font-weight: 700; padding-left: 20px; color: Black;" ForeColor="Black" Visible="False"></asp:Label>
                </td>
               

            </tr>
             <tr id="trmessageSpace" visible="false" runat="server">
                <td style="height: 10px;" colspan="4" class="blank"></td>
            </tr>
            <tr runat="server" id="tr17" visible="false">
                  <td style="width: 25%; text-align: left; padding-left: 40px;">Value of Cheque (Rs.) 
                </td>
                <td style="width: 25%; text-align: left; padding-left: 40px;">
                   
                       <asp:TextBox ID="txtvalueCheck" CssClass="txt" runat="server" AutoPostBack="true" onkeypress="return onlyNumbersbags(this);" AutoComplete="off" Class="alphaNumericWithoutSpace" Enabled="true" OnTextChanged="txtvalueCheck_TextChanged" ></asp:TextBox>


                     </td>
            </tr>
             <tr>
                <td style="height: 10px;" colspan="4" class="blank"></td>
            </tr>
            <tr runat="server" id="tr18" visible="false">
                <td colspan="4" class="blank">
                    <asp:Button ID="bttsubmit" runat="server" Text="Submit" CssClass="button" Enabled="false" OnClick="bttsubmit_Click"></asp:Button>
                </td>
            </tr>
            <tr>
                <td style="height: 10px;" colspan="4" class="blank"></td>
            </tr>
        </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

