﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Reprint_PMDO.aspx.cs" Inherits="PaddyMilling_Reprint_PMDO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>

    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }

        .auto-style3 {
            text-decoration: underline;
        }
    </style>


       <style type="text/css">
        .auto-styleNSC
        {
            width: 622px;
        }

        .InspecTable
        {
            width: 1100px;
            border-top: 6px groove #D9D9D9;
            border-bottom: 6px groove #D9D9D9;
            border-left: 6px groove #D9D9D9;
            border-right: 6px groove #D9D9D9;
            border-radius: 8px;
            /*box-shadow: 0 5px 10px rgba(0,0,0,.5);
           
            background-color:rgb(229, 230, 228);*/
        }

        .InspHead
        {
        }

        .InspColumn
        {
            width: 33%;
            color: #10321f;
            letter-spacing: 2px;
            font-family: Almendra;
            font-size: 13px;
            font-weight: bold;
        }

        .insptxt
        {
            width: 300px;
            height: 20px;
            letter-spacing: 2px;
            font-family: sans-serif;
            font-size: 12px;
            border-color: #03a9f4;
            border: 1px solid #03a9f4;
            border-radius: 8px;
            color: black;
            padding-left: 10px;
        }

        .insfixtext
        {
            width: 310px;
            height: 20px;
            letter-spacing: 2px;
            font-family: sans-serif;
            font-size: 12px;
            border-color: #03a9f4;
            border: 1px solid #03a9f4;
            border-radius: 8px;
            color: black;
            padding-left: 10px;
        }

        .insptxt:focus
        {
            border: none;
        }

        .inspddl
        {
            width: 310px;
            height: 24px;
            letter-spacing: 2px;
            font-family: sans-serif;
            font-size: 12px;
            border-color: #03a9f4;
            border: 1px solid #03a9f4;
            border-radius: 8px;
            padding-left: 10px;
            border-bottom-style: groove;
        }

            .inspddl::selection
            {
                border-radius: 8px;
            }

        /*.inspddl:focus
            {
                border-radius: 8px;
            }*/

        .bttsub
        {
            background: transparent;
            border: none;
            outline: none;
            color: #fff;
            background: #3498db;
            padding: 10px 20px;
            cursor: pointer;
            border-radius: 5px;
            letter-spacing: 4px;
            font-family: Almendra;
            height: 30px;
            width: 400px;
        }

            .bttsub:enabled, button[enabled]
            {
                background: #e74c3c;
            }

        .bttsubother
        {
            background: transparent;
            border: none;
            outline: none;
            color: #fff;
            background: #00AAA0;
            padding: 10px 20px;
            cursor: pointer;
            border-radius: 5px;
            letter-spacing: 4px;
            font-family:Almendra;
            height: 30px;
            width: 150px;
        }

            .bttsubother:enabled, button[enabled]
            {
                background: #e74c3c;
            }

        .sign
        {
            color: #062946;
            font-size: 15px;
            text-decoration: none;
            letter-spacing: 2px;
        }
    </style>

 <%--   <table align="center" style="width: 700px; border-style: solid; border-width: 1px; text-align: left" border="1" cellspacing="0" cellpadding="6">
        <tr>
            <td colspan="4" style="text-align: center; font-size: large; color: #CC6600; font-weight: bold"><span class="auto-style3">Reprint Paddy Milling Delivery Order</span> (DO)</td>
            <input id="hdfCropYear" type="hidden" runat="server" />
        </tr>
        <tr>
           
            <td >Mill Name</td>
           
            <td >
                <asp:DropDownList ID="ddlMillName" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlMillName_SelectedIndexChanged"  >
                </asp:DropDownList>
            </td>
            <td >Agreement Number</td>
            <td>
                <asp:DropDownList ID="ddlAgrmtNo" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlAgrmtNo_SelectedIndexChanged"  >
                </asp:DropDownList>
            </td>
          
        </tr>

        <tr>
            <td colspan="3" ><strong Style="margin-left: 70px" >DO Number</strong>
               
            </td>
            <td>

               
            </td>
        </tr>

    </table>--%>
    <center>
         <table style="width: 1100px; font-size: 12px;">
            <tr>
                <td style="text-align: left; width: 200px">
                    <a href="../District/PaddyMillingHome.aspx" class="sign">&#9754 Back
                    </a>
                </td>
                <td style="text-align: center; width: 700px">
                    <h2 style="color: #e67e22; font-size: 20px; font-family: 'Bellefair'; letter-spacing: 4px; text-decoration-style: wavy; text-align:center"> Reprint Paddy Milling Delivery Order</h2>
                     <input id="hdfCropYear" type="hidden" runat="server" />
                </td>

                <td style="text-align: right; width: 200px">
                    <a href="Reprint_PMDO.aspx" class="sign">&#8635 New
                    </a>
                </td>
            </tr>

        </table>
                 <table class="InspecTable">

            <tr>
                <td colspan="3" style="height: 10px;"></td>
            </tr>
                 <%--<tr>
                <td colspan="3" style="height: 10px;">   <h2 style="color: #e67e22; font-size: 20px; font-family: 'Bellefair'; letter-spacing: 2px; text-align:center; text-decoration-style: wavy;"> Miller State</h2></td>
            </tr>--%>

            <tr>
                <td class="InspColumn" style="padding-left: 20px;">
                    Miller Name

                   <br />
                     <asp:DropDownList ID="ddlMillName" runat="server" CssClass="inspddl" AutoPostBack="True" 
                         OnSelectedIndexChanged="ddlMillName_SelectedIndexChanged"  >
                </asp:DropDownList>
                   
                </td>
                <td class="InspColumn" style="padding-left: 20px;">Agreement Number <br />
                     <asp:DropDownList ID="ddlAgrmtNo" runat="server" CssClass="inspddl"  AutoPostBack="True" OnSelectedIndexChanged="ddlAgrmtNo_SelectedIndexChanged"  >
                </asp:DropDownList>
                </td>
                <td class="InspColumn" style="padding-left: 20px;">
                   DO Number
                    <br/>
                     <asp:DropDownList ID="ddlMvmtNumber" runat="server" CssClass="inspddl" Style="margin-left: 10px" >
                </asp:DropDownList>
                  
                  
                </td>
            </tr>
            <tr>
                <td class="InspColumn" style="padding-left: 20px;">
                </td>
                <td class="InspColumn" style="padding-left: 20px;">
                </td>

                <td class="InspColumn" style="padding-left: 20px;">
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 10px;"></td>
            </tr>
            <tr>
                <td colspan="3" style="height: 10px;">
                    <center>
                      <%--  <asp:Button ID="bttaccept" runat="server" Text="Print" CssClass="bttsubother" Visible="true" 
                            Enabled="false" OnClick="bttaccept_Click" />--%>
                   
                        <asp:Button ID="btnPrint" runat="server" CssClass="bttsubother"  Enabled="false"
                            Text="Print"  OnClick="btnPrint_Click" />
                        


                       

                         </center>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 10px;"></td>
            </tr>

        </table>
    </center>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

