<%@ Page Title="" Language="C#" MasterPageFile="~/Millers_Login/MillerMaster.master" AutoEventWireup="true" CodeFile="MillerLoginPrints.aspx.cs" Inherits="Millers_Login_MillerLoginPrints" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>





<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .ShiprintMainTable
        {
            width: 95%;
        }

        .shipPrintMainColumn
        {
            width: 30%;
        }

        .ShiPrinttable
        {
            width: 100%;
            background-color: rgba(248, 242, 242, 0.76);
            height: 50%;
            border-radius: 8px;
            box-shadow: 10px 10px 5px rgba(0,0,0,0.5);
            overflow: auto;
        }

        .ShiPrintColoumn
        {
            text-align: center;
        }

        .ShiPrintbox
        {
            height: 470px;
            width: 85%;
            padding: 40px;
            background: rgba(0,0,0,.8);
            box-sizing: border-box;
            box-shadow: 10px 10px 5px rgba(0,0,0,.5);
            border-radius: 10px;
            color: #fff;
        }

        .ShiPrintContext
        {
            height: 370px;
            width: 85%;
            padding: 40px;
            background: rgba(0,0,0,.9);
            box-sizing: border-box;
            box-shadow: 10px 10px 5px rgba(0,0,0,.5);
            border-radius: 10px;
            color: #fff;
            text-align: left;
        }

        .shicolumnSubMenu
        {
            border-bottom-width: thin;
            border-bottom-style: dotted;
            border-bottom-color: #3498db;
            text-align: left;
        }



        .ShiPrintRDB
        {
            padding: 16px;
            color: #056069;
        }

            .ShiPrintRDB:checked ~ tableslider
            {
                position: relative;
                animation: animateleft 0.4s;
            }

        @keyframes animateleft
        {
            from
            {
                left: -300px;
                opacity: 0;
            }

            to
            {
                left: 0;
                opacity: 1;
            }
        }
    </style>


    <div>

        <table class="ShiprintMainTable">
            <tr>
                <td class="shipPrintMainColumn">
                    <table class="ShiPrintbox">
                        <tr>
                            <td class="shicolumnSubMenu">


                                <asp:RadioButton ID="rdbOldJute" runat="server" GroupName="Prints" Text="Miller Registration" Style="margin-left: 8px;" CssClass="ShiPrintRDB" />

                            </td>
                        </tr>
                        <tr>
                            <td class="shicolumnSubMenu">
                                <asp:RadioButton ID="RadioButton1" runat="server" GroupName="Prints" Text="Miller Agreement" Style="margin-left: 8px;" CssClass="ShiPrintRDB" />
                            </td>
                        </tr>
                        <tr>
                            <td class="shicolumnSubMenu">
                                <asp:RadioButton ID="RadioButton2" runat="server" GroupName="Prints" Text="Paddy Delivery Order" Style="margin-left: 8px;" CssClass="ShiPrintRDB" />
                            </td>
                        </tr>
                        <tr>
                            <td class="shicolumnSubMenu">
                                <asp:RadioButton ID="RadioButton3" runat="server" GroupName="Prints" Text="Paddy Delivery Challan" Style="margin-left: 8px;" CssClass="ShiPrintRDB" />
                            </td>
                        </tr>
                        <tr>
                            <td class="shicolumnSubMenu">
                                <asp:RadioButton ID="RadioButton4" runat="server" GroupName="Prints" Text="CMR Delivery Order" Style="margin-left: 8px;" CssClass="ShiPrintRDB" />
                            </td>
                        </tr>
                        <tr>
                            <td class="shicolumnSubMenu">
                                <asp:RadioButton ID="RadioButton5" runat="server" GroupName="Prints" Text="CMR Receipt" Style="margin-left: 8px;" CssClass="ShiPrintRDB" />
                            </td>
                        </tr>
                        <tr>
                            <td class="shicolumnSubMenu">
                                <asp:RadioButton ID="RadioButton6" runat="server" GroupName="Prints" Text="Stack Rejection" Style="margin-left: 8px;" CssClass="ShiPrintRDB" />
                            </td>
                        </tr>
                        <tr>
                            <td class="shicolumnSubMenu">
                                <asp:RadioButton ID="RadioButton7" runat="server" GroupName="Prints" Text="Stack Rejection Challan" Style="margin-left: 8px;" CssClass="ShiPrintRDB" />
                            </td>
                        </tr>
                    </table>


                </td>
                <td style="width: 70%">
                    <table class="ShiPrinttable" id="tableslider" runat="server">

                        <tr>
                            <td style="text-align: center; height: 470px;">

                                <center>
                                    <table class="ShiPrintContext"  >
                                        <tr>
                                            <td valign="top" colspan="2" style="width:50%; text-align:Center; height:20px">
                                              <asp:Label ID="lblHeading" runat="server" Visible="true" Text="Heading"></asp:Label>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td valign="top" style="width:50%; padding-left:20px; height:20px">
                                                Miller Name
                                            </td>
                                              <td valign="top" style="width:50%;  height:20px">
                                               <asp:TextBox ID="txtMillerName" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>

                                         <tr>
                                            <td valign="top" style="width:50%;  height:20px">
                                                Miller Agreement
                                            </td>
                                              <td valign="top" style="width:50%;">
                                               <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>

                                         <tr>
                                            <td valign="top" style="width:50%; height:20px">
                                               Paddy DO
                                            </td>
                                              <td valign="top" style="width:50%; height:20px">
                                               <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </center>
                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
        </table>

    </div>




</asp:Content>



