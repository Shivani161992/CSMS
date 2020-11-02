<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Gunny_Bags_GodownToSocietyMappingUpdate_Collector.aspx.cs" Inherits="GunnyBags_Gunny_Bags_GodownToSocietyMappingUpdate_Collector" %>

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
   
    <html>
    <head>
        <style>
            .column {
                height: 15px;
                color: #6B8E23;
                font-family: 'Lucida Fax';
                text-align: center;
                font-size: 35px;
            }

            .tcolumn {
                text-align: center;
                border-radius: 32px;
                font-size: 15px;
                font-family: 'Lucida Fax';
            }

                .tcolumn:focus {
                    box-shadow: 0 0 25px rgb(107,142,35);
                    padding: 3px 0px 3px 3px;
                    margin: 5px 1px 3px 0px;
                    border: 1px solid rgba(81, 203, 238, 1);
                }

            .tddl {
                width: 300px;
                border-radius: 32px;
                height: 25px;
                text-align: center;
                font-family: 'Lucida Fax';
            }

            .bttn {
                width: 400px;
                height: 30px;
                border-radius: 25px;
                color: #fff !important;
                background-color: #000 !important;
                border-radius: 32px;
            }

            .w3-grey, .w3-hover-grey:hover, .w3-gray, .w3-hover-gray:hover {
                color: white !important;
                background-color: #004E4E !important;
                font-family: 'Lucida Fax';
                font-size: 15px;
            }

            .bttn:active {
                background-color: yellow;
            }

            .txt {
            }

                .txt:focus {
                }

            .table {
                background-color: #1B2631;
                width: 500px;
                border-radius: 32px;
                margin-right: 35px;
                text-align: center;
            }

            .bttnew {
                background-color: #004E4E;
                color: white !important;
            }

            .auto-style1 {
                width: 800px;
            }
        </style>
    </head>
    <body>
        <form>
            <table runat="server" style="width: 800px; background-color: #004E4E; color: #FFFAF0;" border="1">
                <tr>
                    <td colspan="2" style="width: 800px; text-align: center; font-size: large; font-family: 'Lucida Fax';">
                        <strong>Gunny Bags Godown To Society Mapping </strong>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 800px; height: 15px; text-align: center; background-color: #FFFAFA"></td>
                </tr>
                <tr id="trCropYear" runat="server" visible="true">
                    <td style="width: 400px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                        <strong>Crop Year </strong>
                    </td>
                    <td style="width: 400px; text-align: left; background-color: #FFFAF0">

                        <asp:DropDownList ID="ddlCropYear" AutoPostBack="true" Width="200px" CssClass="tddl" runat="server">
                        </asp:DropDownList>


                    </td>
                </tr>

                 <tr id="tr1" runat="server" visible="true">
                    <td style="width: 400px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                        <strong>Marketing Season </strong>
                    </td>
                    <td style="width: 400px; text-align: left; background-color: #FFFAF0">

                        <asp:DropDownList ID="ddlMSeason"  Width="200px" CssClass="tddl" runat="server">
                            <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Rabi" Value="R"></asp:ListItem>
                            <asp:ListItem Text="Kharif" Value="K"></asp:ListItem>
                        </asp:DropDownList>


                    </td>
                </tr>

                  <tr>
                    <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                        <strong>District Of Godown </strong>
                    </td>
                    <td style="width: 250px; height: 15px; text-align: left; background-color: #FFFAFA">
                        <asp:DropDownList ID="ddlDistrictGodown" CssClass="tddl" runat="server" Width="200px" Enabled="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;" class="auto-style2">
                        <strong>Godown </strong>
                    </td>
                    <td colspan="3" style="width: 250px; height: 15px; text-align: left; background-color: #FFFAFA">
                        <asp:DropDownList ID="ddlGodown" AutoPostBack="true" CssClass="tddl" runat="server" Width="435px" Height="26px"
                            OnSelectedIndexChanged="ddlGodown_selected">
                        </asp:DropDownList>
                    </td>

                </tr>


                <tr runat="server" id="trGrid">
                    <td colspan="2" style="text-align: center; background-color: #FFFAFA" class="auto-style1">
                        <asp:Panel ID="Panel1" runat="server" Style="overflow-y: scroll" Visible="false" Height="234px">
                            <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="SocietyDistrict" HeaderText="District" />
                                    <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="Society_Name" HeaderText="Soceity" />
                                    <asp:BoundField ItemStyle-HorizontalAlign="Right" DataField="distance" HeaderText="Distance" />
                                    <asp:BoundField ItemStyle-HorizontalAlign="Right" DataField="Quantity" HeaderText="Quantity" />
                                    <asp:BoundField ItemStyle-HorizontalAlign="Right" DataField="Priority" HeaderText="Priority" />
                                </Columns>
                                <FooterStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#004E4E" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>

               <tr>
                    <td style="width: 250px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                        <strong>District of Society </strong>
                    </td>
                    <td style="width: 250px; height: 15px; text-align: left; background-color: #FFFAFA">
                        <asp:DropDownList ID="ddlDistrictSociety" AutoPostBack="true" CssClass="tddl" runat="server" Width="200px" OnSelectedIndexChanged="ddlDistrictSociety_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;" class="auto-style2">
                        <strong>Society </strong>
                    </td>
                    <td colspan="3" style="width: 250px; height: 15px; text-align: left; background-color: #FFFAFA">
                        <asp:DropDownList ID="ddlSociety" AutoPostBack="true" CssClass="tddl" runat="server" Width="435px" Height="26px"
                            OnSelectedIndexChanged="ddlSociety_selected">
                        </asp:DropDownList>
                    </td>

                </tr>

               

                <tr>
                    <td style="width: 400px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                        <strong>Distance </strong>
                    </td>

                    <td style="background-color: #FFFAF0; text-align: left;">

                          <asp:TextBox ID="txtDistance" Width="200px" CssClass="tcolumn" runat="server" Enabled="false"></asp:TextBox>

                    </td>
                </tr>

             
                
                <tr>
                    <td style="width: 200px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                        <strong>Quantity </strong></td>
                    <td style="width: 200px; text-align: left; background-color: #FFFAF0">

                        <asp:TextBox ID="txtQuantity" Width="200px" CssClass="tcolumn" runat="server"></asp:TextBox>


                    </td>
                </tr>

                 <tr>
                    <td style="text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;" class="auto-style2">
                        <strong>From Date </strong>
                    </td>
                    <td colspan="3" style="width: 250px; height: 15px; text-align: left; background-color: #FFFAFA">
                         <asp:TextBox ID="txtFromDate" runat="server" AutoComplete="off"></asp:TextBox>
                <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ContentPlaceHolder1_txtFromDate' , 'expiry=true,elapse=-150,restrict=false,close=true')" />
                 <%--<img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtFromDate' , 'expiry=true,elapse=-150,restrict=false,close=true')" />--%>
               
                    </td>

                </tr>

                <tr>
                    <td style="text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;" class="auto-style2">
                        <strong>To Date </strong>
                    </td>
                    <td colspan="3" style="width: 250px; height: 15px; text-align: left; background-color: #FFFAFA">
                       <asp:TextBox ID="txtToDate" runat="server" AutoComplete="off"></asp:TextBox>
               <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ContentPlaceHolder1_txtToDate' , 'expiry=true,elapse=-150,restrict=false,close=true')" />
             <%--<img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtToDate' , 'expiry=true,elapse=-150,restrict=false,close=true')" />--%>
            <%--   ctl00_ContentPlaceHolder1_txtDateofReceipt--%>
                    </td>

                </tr>
              
                <tr id="tr2" runat="server" visible="true">
                    <td style="width: 400px; text-align: left; background-color: #FFFAF0; font-size: medium; font-family: 'Lucida Fax'; color: #004E4E;">
                        <strong>Priority </strong>
                    </td>
                    <td style="width: 400px; text-align: left; background-color: #FFFAF0">

                        <asp:DropDownList ID="ddlPriority"  Width="200px" CssClass="tddl" runat="server" Enabled="false">
                        </asp:DropDownList>


                    </td>
                </tr>
                <tr>
                     <td  style="width: 800px; height: 15px; text-align: center; background-color: #FFFAFA">
                         <asp:Button  CssClass="bttn w3-grey" ID="btnRefresh" runat="server" Text="New" Width="110px" OnClick="btnRefresh_Click" />
                    </td>
                    <td  style="width: 800px; height: 15px; text-align: center; background-color: #FFFAFA">
                        <asp:Button ID="bttSubmit" runat="server" Text="Submit" CssClass="bttn w3-grey" OnClick="bttSubmit_Click" />
                    </td>
                </tr>

                <tr>
                    <td colspan="2" style="width: 800px; height: 15px; text-align: left; background-color: #FFFAFA">&nbsp;</td>
                </tr>

                <tr>
                    <td colspan="2" style="width: 800px; height: 15px; text-align: center; font-size: large; font-family: 'Lucida Fax';"></td>
                </tr>



            </table>
        </form>
    </body>
    </html>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

