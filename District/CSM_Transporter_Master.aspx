<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="CSM_Transporter_Master.aspx.cs" Inherits="District_CSM_Transporter_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

      <style type="text/css">
        .auto-styleNSC
        {
            width: 622px;
        }

        .InspecTable
        {
            width: 1100px;
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
            border-bottom-style: groove;
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
            border-bottom-style: groove;
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

            .inspddl:focus
            {
                border-radius: 8px;
            }

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
            font-family: sans-serif;
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
            font-family: sans-serif;
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
        .hover_row
    {
       /*// background-color: #A1DCF2;*/
        background: linear-gradient(silver, #fff);
    }
         .GridHeader
        {
            text-align:center;
        }
    </style>
    <center>
        <table style="width: 1100px; font-size: 12px;">
            <tr>
                <td style="text-align: left; width: 200px">
                    <a href="../State/ChannaMassorSarson_Home.aspx" class="sign">&#9754 Back
                    </a>
                </td>
                <td style="text-align: center; width: 700px">
                    <h2 style="color: #e74c3c; font-size: 20px; font-family: 'Bellefair'; letter-spacing: 4px; text-align:center;">Transporter Master</h2>
                      <input type="hidden" runat="server" id="hdfID" />
                </td>

                <td style="text-align: right; width: 200px">
                    <a href="CSM_Transporter_Master.aspx" class="sign">&#8635 New
                    </a>
                </td>
            </tr>

        </table>
        </center>
    <center>
         <table class="InspecTable">
          <tr>
                <td colspan="3" style="height: 10px;"></td>
            </tr>

           

            <tr>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">Transporter Name
                    <br />
 <asp:TextBox ID="txtTransporterName" CssClass="insptxt" runat="server" ReadOnly="true" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtTransporterName" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                 <td class="InspColumn" style="padding-left: 20px; text-align: left">
                    Mobile Number 
                    <br />

                    <asp:TextBox ID="txtMobileNumber" CssClass="insptxt" runat="server" ReadOnly="true" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtMobileNumber" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                 <td class="InspColumn" style="padding-left: 20px; text-align: left">
                     Email Id
                    <br />

                    <asp:TextBox ID="txtEmailID" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtEmailID" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                </tr>

            

                   <tr>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">Street Address
                    <br />
 <asp:TextBox ID="txtStreetAdd" CssClass="insptxt" runat="server" ReadOnly="true" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtStreetAdd" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                 <td class="InspColumn" style="padding-left: 20px; text-align: left">
                  City 
                    <br />

                    <asp:TextBox ID="txtTransporterCity" CssClass="insptxt" runat="server" ReadOnly="true" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtTransporterCity" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                 <td class="InspColumn" style="padding-left: 20px; text-align: left">
                  State
                    <br />

                   <asp:DropDownList ID="ddlstate" runat="server" CssClass="inspddl" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" Style="font-size: 12px;"
                        ControlToValidate="ddlstate" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                </tr>
             
             <tr>
                     <td class="InspColumn" style="padding-left: 20px; text-align: left">
                Pin Code
                    <br />

                   <asp:TextBox ID="txtPinCode" CssClass="insptxt" runat="server" ReadOnly="true" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtPinCode" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">Aadhar Number
                    <br />
 <asp:TextBox ID="txtaadhar" CssClass="insptxt" runat="server" ReadOnly="true" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtaadhar" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                 <td class="InspColumn" style="padding-left: 20px; text-align: left">
                  PAN Number
                    <br />

                    <asp:TextBox ID="txtPan" CssClass="insptxt" runat="server" ReadOnly="true" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtPan" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                
                </tr>

                 <tr>
                     <td class="InspColumn" style="padding-left: 20px; text-align: left">
              Bank Name
                    <br />

                   <asp:DropDownList ID="ddlBankName" runat="server" CssClass="inspddl" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Style="font-size: 12px;"
                        ControlToValidate="ddlBankName" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">Branch
                    <br />
 <asp:TextBox ID="txtBranch" CssClass="insptxt" runat="server" ReadOnly="true" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtBranch" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                 <td class="InspColumn" style="padding-left: 20px; text-align: left">
                 Bank City Name
                    <br />

                    <asp:TextBox ID="txtBankCity" CssClass="insptxt" runat="server" ReadOnly="true" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtBankCity" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                
                </tr>

               <tr>
                     <td class="InspColumn" style="padding-left: 20px; text-align: left">
             Bank Account Number
                    <br />

                   <asp:TextBox ID="TextBox3" CssClass="insptxt" runat="server" ReadOnly="true" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtBranch" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">IFSC Code
                    <br />
 <asp:TextBox ID="TextBox1" CssClass="insptxt" runat="server" ReadOnly="true" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtBranch" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                 <td class="InspColumn" style="padding-left: 20px; text-align: left">
                Valid Upto
                    <br />

                    <asp:TextBox ID="TextBox2" CssClass="insptxt" runat="server" ReadOnly="true" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtBankCity" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                
                </tr>
         </table>
    </center>
</asp:Content>

