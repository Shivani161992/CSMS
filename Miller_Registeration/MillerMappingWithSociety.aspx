<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MillerMappingWithSociety.aspx.cs" Inherits="MillerMappingWithSociety" MasterPageFile="~/MasterPage/SCSC_master.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>

    <script type="text/javascript" lang="javascript" src="../js/calendarMvmt.js"></script>

    <%--For Calendar Controls--%>
    <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="../js/calendarMvmt.js"></script>

    <%--Allow Only 2 Digit After Decimal--%>
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/Number2D.js"></script>

    <style type="text/css">
        .hiddencol {
            display: none;
        }
    </style>

    


    <script type="text/javascript">
        $(document).ready(function () {
            $("input").focus(function () {
                $(this).css("background-color", "#cccccc");
            });
            $("input").blur(function () {
                $(this).css("background-color", "#ffffff");
            });
        });
    </script>

    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
            font-size: small;
        }

        .Qtls {
            font-size: small;
            color: #FF0000;
        }

        .hover_row {
            background-color: #A1DCF2;
        }
    </style>

    <script type="text/javascript">


        function CheckCalDate(tx) {
            var AsciiCode = event.keyCode;
            var txt = tx.value;
            var txt2 = String.fromCharCode(AsciiCode);
            var txt3 = txt2 * 1;
            if ((AsciiCode > 0)) {
                alert('Please Click on Calander Controll to Enter Date');
                event.cancelBubble = true;
                event.returnValue = false;
            }
        }
</script> 
   


   


    <table style="width: 800px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
        <tr>
            <td colspan="2" style="font-family: Calibri; font-size: large; color: #000099; background-color: #CCCCFF;">

                <center><strong>Miller Mapping With Society</strong></center>
            </td>
        </tr>




        <tr>
            <td style="width: 271px; height: 1px; text-align: left">Crop Year:</td>
            <td style="width: 324px; height: 1px; text-align: left">
                <asp:DropDownList ID="ddlyear" runat="server" Width="194px" Height="27px" Style="text-align: left" AutoPostBack="true" OnSelectedIndexChanged="ddlyear_SelectedIndexChanged">
                     <asp:ListItem Text="Select" Value="0"></asp:ListItem> 
                     <asp:ListItem Text="2016-2017" Value="2016-2017"></asp:ListItem>
                     <asp:ListItem Text="2017-2018" Value="2017-2018"></asp:ListItem>
                     <asp:ListItem Text="2018-2019" Value="2018-2019"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
         <tr>
            <td style="width: 274px; height: 1px; text-align: left"> Miller District :</td>
            <td style="width: 324px; height: 1px; text-align: left">
                  <asp:DropDownList ID="ddlMillerDistrict" runat="server" Height="27px" Width="387px" Enabled="false">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td style="width: 274px; height: 1px; text-align: left"> Miller Name :</td>
            <td style="width: 324px; height: 1px; text-align: left">
                <asp:DropDownList ID="ddlMiller" runat="server" Height="27px" Width="387px" AutoPostBack="true" OnSelectedIndexChanged="ddlMiller_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>



        <tr>
            <td style="width: 274px; height: 1px; text-align: left" >Society District :</td>
            <td style="width: 324px; height: 1px; text-align: left">
                <asp:DropDownList ID="ddlSocietyDistrict" runat="server" Height="27px" Width="194px" AutoPostBack="true" OnSelectedIndexChanged="ddlSocietyDistrict_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td style="width: 271px; height: 1px; text-align: left">Society:</td>
            <td style="width: 324px; height: 1px; text-align: left">
                <asp:DropDownList ID="ddlSociety" runat="server" Height="27px" Width="194px"  AutoPostBack="true" OnSelectedIndexChanged="ddlSociety_SelectedIndexChanged" >
                </asp:DropDownList>
            </td>
        </tr>        

     
         <tr>
            <td style="width: 271px; height: 1px; text-align: left">From Date :</td>
            <td style="width: 324px; height: 1px; text-align: left">
                <asp:TextBox ID="txtFromDate" runat="server" AutoComplete="off"></asp:TextBox>
                <%--<img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ContentPlaceHolder1_txtFromDate' , 'expiry=true,elapse=-150,restrict=false,close=true')" />--%>
                 <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtFromDate' , 'expiry=true,elapse=-150,restrict=false,close=true')" />
               
            </td>
        </tr>

         <tr>
            <td style="width: 271px; height: 1px; text-align: left">To Date :</td>
            <td style="width: 324px; height: 1px; text-align: left">
                <asp:TextBox ID="txtToDate" runat="server" AutoComplete="off"></asp:TextBox>
               <%--<img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ContentPlaceHolder1_txtToDate' , 'expiry=true,elapse=-150,restrict=false,close=true')" />--%>
             <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtToDate' , 'expiry=true,elapse=-150,restrict=false,close=true')" />
            <%--   ctl00_ContentPlaceHolder1_txtDateofReceipt--%>
                
               
            </td>
        </tr>

        <tr>
            <td style="width: 271px; height: 1px; text-align: justify"></td>
            <td style="width: 324px; height: 1px; text-align: left">
                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="110px" OnClick="btnRefresh_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
                  <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="113px" OnClick="btnSubmit_Click" />
            </td>
        </tr>


    </table>
</asp:Content>
