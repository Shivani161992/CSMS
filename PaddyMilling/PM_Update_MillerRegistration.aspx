﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/DistLoginOperations_Master.master" AutoEventWireup="true" CodeFile="PM_Update_MillerRegistration.aspx.cs" Inherits="PaddyMilling_PM_Update_MillerRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .subtablesub
        {
            width: 960px;
            text-align: center;
            background-color: rgba(22, 66, 48, 0.67);
            border-radius: 8px;
            color: #fff;
            box-shadow: 10px 10px 5px rgba(0,0,0,0.5);
            letter-spacing: 2px;
            font-family: Georgia;
            
        }


        .table
        {
            width: 960px;
            text-align: center;
            background-color: rgba(0, 0, 0, 0.83);
            border-radius: 8px;
            color: #fff;
            font-size: 20px;
            letter-spacing: 2px;
            font-family: Georgia;
           
        }

            .table:hover
            {
                background-color: rgba(0, 0, 0, 0.83);
            }

        .colomn
        {
            width: 240px;
            text-align: left;
        }


        .ddl
        {
            border-radius: 8px;
            height: 20px;
            width: 240px;
            letter-spacing: 2px;
            font-family: Georgia;
            text-align: center;
            font-weight: bolder;
        }

        .txt
        {
            border-radius: 8px;
            height: 20px;
            width: 240px;
            letter-spacing: 2px;
            font-family: Georgia;
            text-align: center;
            font-weight: bolder;
        }

        .button
        {
            border-radius: 8px;
            background-color: rgba(187, 7, 106, 0.81);
            width: 240px;
            color: #fff;
            letter-spacing: 2px;
            font-family: Georgia;
        }

            .button:hover
            {
                background-color: rgba(10, 10, 10, 0.81);
            }
    </style>
    <center>    <table style="width: 1000px; text-align: center;">
        <tr>
            <td style="width: 20px"></td>
            <td>
                <table style="width: 960px; text-align: center;">
                    <tr>
                        <td>
                            <table style="width: 960px; text-align: center;">
                                <tr>
                                    <td class="table">UPDATE MILLER REGISTRATION
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 15px; background-color: #fff;"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <table class="subtablesub" border="1">
                                            <tr>
                                                <td colspan="2" style="height: 10px; text-align: left; font-size: 20px; color: #fff;"><a href="../District/PaddyMillingHome.aspx" style="color: #fff; font-size: 25px; text-decoration: none;">&#9754 

                                                                                                                                     </a></td>

                                                <td colspan="2" style="height: 10px; text-align: right; font-size: 20px; color: #fff;"><a href="PM_Update_MillerRegistration.aspx" style="color: #fff; font-size: 25px; text-decoration: none;">&#8635 
                                                     

                                                                                                                                       </a></td>

                                            </tr>
                                            <tr>
                                                <td class="colomn">Crop Year
                                                </td>
                                                <td class="colomn">
                                                    <asp:DropDownList ID="ddlCropyear" CssClass="ddl" runat="server" Height="25px" Width="240px" AutoPostBack="True" OnSelectedIndexChanged="ddlCropyear_SelectedIndexChanged">
                                                    </asp:DropDownList>

                                                </td>
                                                <td class="colomn">District
                                                </td>
                                                <td class="colomn">
                                                    <asp:TextBox ID="txtdist" runat="server" CssClass="txt" Width="240px" Style="margin-left: 0px" ReadOnly="True" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="height: 10px;"></td>
                                            </tr>
                                            <tr>
                                                <td class="colomn">Miller Name
                                                </td>
                                                <td class="colomn" colspan="3">
                                                    <asp:DropDownList ID="ddlMillName" runat="server" CssClass="ddl" Height="25px" Width="480px" AutoPostBack="True" OnSelectedIndexChanged="ddlMillName_SelectedIndexChanged">
                                                    </asp:DropDownList>

                                                </td>

                                            </tr>
                                            <tr>
                                                <td colspan="4" style="height: 10px;"></td>
                                            </tr>
                                            <tr>
                                                <td class="colomn">Milling Capacity Arwa</td>
                                                <td class="colomn">
                                                    <asp:TextBox ID="txtarwa" runat="server" CssClass="txt" Width="240px" Style="margin-left: 0px" Enabled="false"></asp:TextBox></td>
                                                <td class="colomn">Milling Capacity Ushna</td>
                                                <td class="colomn">
                                                    <asp:TextBox ID="txtushna" runat="server" CssClass="txt" Width="240px" Style="margin-left: 0px" Enabled="false"></asp:TextBox></td>

                                            </tr>

                                            <tr>
                                                <td colspan="4" style="height: 10px;"></td>
                                            </tr>
                                            <tr>
                                                <td class="colomn" style="text-align: center" colspan="4">Paddy Quantity 
                                                   <asp:TextBox ID="txtqty" runat="server" CssClass="txt" Width="240px" Style="margin-left: 0px" Enabled="false"></asp:TextBox>
                                                    (M.T) for Milling Process between 16/01/2018 and  30/06/2018  </td>


                                            </tr>
                                            
                                              <tr>
                                                <td class="colomn">कुल भण्डारण शम्ता (M.T)</td>
                                                <td class="colomn">
                                                    <asp:TextBox ID="txtTotalVacantCap" runat="server" CssClass="txt" Width="240px" Style="margin-left: 0px" Enabled="false"></asp:TextBox></td>
                                                <td class="colomn"> 15/1/2018 को मिलर्स रिक्त भण्डारण शम्ता (M.T)</td>
                                                <td class="colomn">
                                                    <asp:TextBox ID="txtVacantCap" runat="server" CssClass="txt" Width="240px" Style="margin-left: 0px" Enabled="false"></asp:TextBox></td>

                                            </tr>
                                            <tr>
                                                <td colspan="4" style="height: 10px;"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <asp:Button ID="bttnupdate" runat="server" Height="30px" Text="UPDATE" CssClass="button" Style="margin-left: 10px" OnClientClick="return validate();" Enabled="False" OnClick="bttnupdate_Click" />

                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="height: 10px;"></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

            </td>
            <td style="width: 20px"></td>
        </tr>
    </table>
        </center>

</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>--%>

