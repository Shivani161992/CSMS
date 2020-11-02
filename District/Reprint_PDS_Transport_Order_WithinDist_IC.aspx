<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Reprint_PDS_Transport_Order_WithinDist_IC.aspx.cs" Inherits="District_Reprint_PDS_Transport_Order_WithinDist_IC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <html>
    <head>
        <style>
            .tddl
            {
                width: 400px;
                border-radius: 32px;
                height: 35px;
                text-align: center;
                font-family: 'Lucida Fax';
                text-align: center;
            }
            .tcolumn
        {
            width: 400px;
            text-align: center;
            border-radius: 32px;
            height: 35px;
            font-size: 15px;
            font-family: 'Lucida Fax';
        }

            .tcolumn:focus
            {
                box-shadow: 0 0 25px rgb(107,142,35);
                padding: 3px 0px 3px 3px;
                margin: 5px 1px 3px 0px;
                border: 1px solid rgba(81, 203, 238, 1);
                width:0px;
            }

            .bttn
            {
                width: 400px;
                height: 40px;
                border-radius: 25px;
                color: #fff!important;
                background-color: #D4AC0D!important;
                border-radius: 32px;
            }

            .w3-grey, .w3-hover-grey:hover, .w3-gray, .w3-hover-gray:hover
            {
                color: #1B2631!important;
                background-color: #D4AC0D!important;
                font-family: 'Lucida Fax';
                font-size: 25px;
            }

            .bttn:active
            {
                background-color: yellow;
            }
        </style>
    </head>
    <body>
        <table style="width: 800px; border-color: black; background-color: #AEB6BF;">
            <tr>
                <td colspan="3"></td>
            </tr>

            <tr>
                <td style="width:25px;"></td>
                <td style="text-align: center;">

                    <table style="width: 750px; border-color: black; background-color: #1B2631; border-color:black;" border="1">
                        <tr>
                            <td colspan="4" style="text-align: center; font-size: 25px; color: #D4AC0D; background-color: #1B2631;">
                                <b>Reprint  </b>
                                <br />
                                <b>Transport Order (Between Issue Center) </b>

                            </td>
                           
                        </tr>
                        <tr>
                            <td style="text-align: left; font-size: 18px; color: #AEB6BF;">
                                <b>District</b></td>
                            <td>

<asp:TextBox ID="txtDistrict" runat="server" Width="400px" class="tcolumn" Style="text-align:center" MaxLength="10" AutoComplete="off" Enabled="false" ></asp:TextBox>
                            </td>
                            
                           
                        </tr>
                        <tr>
                            <td style="text-align: left; font-size: 18px; color: #AEB6BF;">
                                <b>Transport Order No.</b></td>
                            <td>

                                <asp:DropDownList ID="ddlTransPortOrder" runat="server" class="tddl" AutoPostBack="True"></asp:DropDownList>

                            </td>
                            
                           
                        </tr>
                         <tr>
                            <td style="text-align:center; font-size: 18px; color: #AEB6BF;" colspan="2">
                                <asp:Button ID="bttRegister" runat="server" Text="Print" CssClass="bttn w3-grey" OnClick="bttprint_Click" /></td>
                            <td>

                              

                            </td>
                            
                           
                        </tr>
                    </table>

                </td>
                <td style="width:25px;"></td>
            </tr>

            <tr>
                <td colspan="3"></td>
            </tr>
        </table>

    </body>
    </html>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

