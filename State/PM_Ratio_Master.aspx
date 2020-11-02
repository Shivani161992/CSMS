<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="PM_Ratio_Master.aspx.cs" Inherits="State_PM_Ratio_Master" %>

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

    <style>
        .tablepage
        {
            width: 100%;
            height:100%;
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
            font-family: sans-serif;
            border-collapse: collapse;
            border-color: #fff;
            border-width: 0px;
            height: 70%;
            overflow: scroll;
             font-size:12px;
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

            .button:active
            {
                border: none;
                border-bottom: 2px solid black;
                box-shadow: 0px 1px 5px grey;
                background: linear-gradient(black,#062946);
                color: #fff;
            }

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
    <table style="width: 100%">
        <tr>
            <td style="text-align: left">
                <a href="PaddyMillingHome.aspx" class="sign">&#9754 Back
                </a>
            </td>
            <td style="text-align: right">
                <a href="PM_Ratio_Master.aspx" class="sign">&#8635 New
                </a>
            </td>
        </tr>
    </table>
    <center>
    <table class="tablepage">
        <tr>
            <td class="headerpage">Paddy Milling Ratio Master
            </td>
        </tr>
    </table>
    <table class="subtable" border="1" >
        <tr class="heading">
            <td style="width:50% ; text-align:center;" >
<asp:RadioButton ID="rdbAll" runat="server" Text="All Millers" Checked="true" AutoPostBack="true" OnCheckedChanged="rdbAll_CheckedChanged"></asp:RadioButton>
            </td>
             <td style="width:50%; text-align:center;">
                 <asp:RadioButton ID="rdbparticularMiller" runat="server" Text=" Particular Miller" Checked="false" AutoPostBack="true" OnCheckedChanged="rdbparticularMiller_CheckedChanged"></asp:RadioButton>
            </td>
        </tr>
        <tr  >
            <td style="height:10px;" colspan="2" class="blank">

            </td>
        </tr>
        <tr>
            <td style="width:50% ; text-align:left; padding-left:40px" >
                Crop Year
            </td>
            <td style="width:50% ; text-align:left;padding-left:40px" >

                <asp:DropDownList ID="ddlCropYear" CssClass="ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCropYear_SelectedIndexChanged"></asp:DropDownList>
            </td>
        </tr>
          <tr  >
            <td style="height:10px;" colspan="2" class="blank">

            </td>
        </tr>
          <tr runat="server" id="trdist" visible="false">
            <td style="width:50% ; text-align:left; padding-left:40px" >
               Miller District
            </td>
            <td style="width:50% ; text-align:left; padding-left:40px" >

                <asp:DropDownList ID="ddlmillDist" CssClass="ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlmillDist_SelectedIndexChanged"></asp:DropDownList>
            </td>
        </tr>
          <tr runat="server" id="trdistspace" visible="false"  >
            <td style="height:10px;" colspan="2" class="blank">

            </td>
        </tr>
          <tr runat="server" id="trmillname" visible="false">
            <td style="width:50% ; text-align:left; padding-left:40px" >
                Miller Name
            </td>
            <td style="width:50% ; text-align:left; padding-left:40px" >

                <asp:DropDownList ID="ddlMillName" CssClass="ddl" style="width:350px;" runat="server"></asp:DropDownList>
            </td>
        </tr>
          <tr runat="server" id="trmillnamespace" visible="false" >
            <td style="height:10px;" colspan="2" class="blank">

            </td>
        </tr>
         <tr>
            <td style="width:50% ; text-align:left; padding-left:40px" >
               1 Lot Amount(Rs.)
            </td>
            <td style="width:50% ; text-align:left; padding-left:40px" >

              
                <asp:TextBox ID="txtlot" CssClass="txt" runat="server"  AutoPostBack="true"  MinLength="1" onkeypress="return onlyNumbersbags(this);" AutoComplete="off" Class="alphaNumericWithoutSpace"></asp:TextBox>
            </td>
        </tr>
          <tr  >
            <td style="height:10px;" colspan="2" class="blank">

            </td>
              </tr>
         <tr>
            <td style="width:50% ; text-align:left; padding-left:40px" >
               Total
            </td>
            <td style="width:50% ; text-align:left; padding-left:40px" >

              
                <asp:TextBox ID="txttotal" CssClass="txt" runat="server" OnTextChanged="txttotal_TextChanged" AutoPostBack="true" MaxLength="2" MinLength="1" onkeypress="return onlyNumbersbags(this);" AutoComplete="off" Class="alphaNumericWithoutSpace"></asp:TextBox>
            </td>
        </tr>
          <tr  >
            <td style="height:10px;" colspan="2" class="blank">

            </td>
        </tr>
          <tr>
            <td style="width:50% ; text-align:left; padding-left:40px" >
              Minimum Fixed Deposit Receipt (FDR)
            </td>
            <td style="width:50% ; text-align:left; padding-left:40px" >

              
                <asp:TextBox ID="txtfdr" CssClass="txt" runat="server" OnTextChanged="txtfdr_TextChanged" AutoPostBack="true"  MaxLength="2" MinLength="1" onkeypress="return onlyNumbersbags(this);" AutoComplete="off" Enabled="false" Class="alphaNumericWithoutSpace"></asp:TextBox>
            </td>
        </tr>
          <tr  >
            <td style="height:10px;" colspan="2" class="blank">

            </td>
        </tr>
         <tr>
            <td style="width:50% ; text-align:left; padding-left:40px" >
              Maximum Cheque
            </td>
            <td style="width:50% ; text-align:left; padding-left:40px" >

              
                <asp:TextBox ID="txtcheck" CssClass="txt" runat="server" OnTextChanged="txtcheck_TextChanged" AutoPostBack="true"  MaxLength="2" MinLength="1" onkeypress="return onlyNumbersbags(this);" AutoComplete="off" Enabled="false" Class="alphaNumericWithoutSpace"></asp:TextBox>
            </td>
        </tr>
         <tr  >
            <td style="height:10px;" colspan="2" class="blank">

            </td>
        </tr>
         <tr>
            <td style="width:50% ; text-align:left; padding-left:40px" >
            From Date
            </td>
            <td style="width:50% ; text-align:left; padding-left:40px" >

              
                <asp:TextBox ID="txtFrmdate" CssClass="txt" runat="server"  AutoPostBack="true"    AutoComplete="off" Enabled="false" ></asp:TextBox>
              
                    <%--<img src="calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtToDate' , 'restrict=true,instance=single,limit=<%= DateLimit %>')" />--%>
                    <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtFrmdate' , 'expiry=true,elapse=-150,restrict=true,close=true')" />

            </td>
        </tr>
         <tr  >
            <td style="height:10px;" colspan="2" class="blank">

            </td>
        </tr>
         <tr>
            <td style="width:50% ; text-align:left; padding-left:40px" >
             To Date
            </td>
            <td style="width:50% ; text-align:left; padding-left:40px" >

              
                <asp:TextBox ID="txtTOdate" CssClass="txt" runat="server"  AutoPostBack="true"    AutoComplete="off" Enabled="false" ></asp:TextBox>
                                    <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtTOdate' , 'expiry=true,elapse=-150,restrict=false,close=true')" />

            </td>
        </tr>
         <tr  >
            <td style="height:10px;" colspan="2" class="blank">

            </td>
        </tr>
           <tr  >
            <td  colspan="2" class="blank">
                <asp:Button ID="bttsubmit" runat="server" Text="Submit" CssClass="button" Enabled="false" OnClick="bttsubmit_Click"></asp:Button>
            </td>
        </tr>
           <tr  >
            <td style="height:10px;" colspan="2" class="blank">

            </td>
        </tr>
       
    </table>
        </center>
</asp:Content>

