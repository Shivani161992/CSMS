<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Reprint_Ins_CMR_Rej.aspx.cs" Inherits="PaddyMilling_Reprint_Ins_CMR_Rej" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


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
     <center>
         <table style="width: 1100px; font-size: 12px;">
            <tr>
                <td style="text-align: left; width: 200px">
                    <a href="../District/PaddyMillingHome.aspx" class="sign">&#9754 Back
                    </a>
                </td>
                <td style="text-align: center; width: 700px">
                    <h2 style="color: #e67e22; font-size: 20px; font-family: 'Bellefair'; letter-spacing: 4px; text-decoration-style: wavy; text-align:center"> Reprint Inspection Of CMR Rejection Entry</h2>
                     <input id="hdfCropYear" type="hidden" runat="server" />
                </td>

                <td style="text-align: right; width: 200px">
                    <a href="Reprint_Ins_CMR_Rej.aspx" class="sign">&#8635 New
                    </a>
                </td>
            </tr>

        </table>
          <table class="InspecTable">

            <tr>
                <td colspan="3" style="height: 10px;"></td>
            </tr>
               
                   
            <tr>
                <td class="InspColumn" style="padding-left: 20px;">
                    Miller Name

                   <br />
                     <asp:DropDownList ID="ddlMillName" runat="server" CssClass="inspddl" AutoPostBack="True" 
                         OnSelectedIndexChanged="ddlMillName_SelectedIndexChanged"  >

                                       
            
                </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlMillName" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">*</asp:RequiredFieldValidator>

                                  
                </td>
                <td class="InspColumn" style="padding-left: 20px;">Rejection No. <br />
                     <asp:DropDownList ID="ddlMvmtNumber" runat="server" CssClass="inspddl"  AutoPostBack="True"   >
                </asp:DropDownList>
                                   
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlMvmtNumber" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">*</asp:RequiredFieldValidator>

                                  
                </td>
                <td class="InspColumn" style="padding-left: 20px;">
                  
                  
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

