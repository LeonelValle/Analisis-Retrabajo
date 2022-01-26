<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Alta_Analisis.aspx.cs" Inherits="EstadiaMWE.Alta_Analisis" %>

<!DOCTYPE html>
<style type="text/css">
    .largeCard {
        width: 80px;
        height: 80px;
        border-color: white;
        border-radius: 10px;
        border: 1px solid #ccc;
        cursor: pointer;
        display: inline-block;
    }

        .largeCard .largecard_image {
            width: inherit;
            height: inherit;
            text-align: center;
            border-radius: 40px;
        }

            .largeCard .largecard_image img {
                width: 50px;
                height: 50px;
                border-radius: 40px;
                object-fit: cover;
            }

        .largeCard .largecard_title {
            text-align: center;
            border-radius: 0px 0px 40px 40px;
            font-family: Calibri;
            font-size: 15px;
            margin-top: -50px;
            height: 40px;
        }


    a {
        text-decoration: none;
        font-family: Calibri;
        color: red;
    }


    form {
        background-color: white;
        background-repeat: round;
        background-attachment: fixed;
        text-align: left;
        width: 70%;
        display: inline-block;
    }


    .blueButton {
        box-shadow: inset 0px 1px 0px 0px #bee2f9;
        background-color: #63b8ee;
        border-radius: 6px;
        border: 1px solid #3866a3;
        display: inline-block;
        cursor: pointer;
        color: white;
        font-family: Arial;
        font-size: 15px;
        font-weight: bold;
        padding: 6px 24px;
        text-decoration: none;
        text-shadow: 0px 1px 0px #7cacde;
        margin-left: 0px;
        margin-bottom: 0px;
    }

        .blueButton:disabled {
            background-color: #808080;
            color: black;
        }

        .blueButton:hover {
            background: linear-gradient(to bottom, #468ccf 5%, #63b8ee 100%);
            background-color: #468ccf;
        }

        .blueButton:active {
            position: relative;
            top: 1px;
        }

    .redButton {
        box-shadow: inset 0px 1px 0px 0px #f7c5c0;
        background: linear-gradient(to bottom, #fc8d83 5%, #e4685d 100%);
        background-color: #fc8d83;
        border-radius: 6px;
        border: 1px solid #d83526;
        display: inline-block;
        cursor: pointer;
        color: #ffffff;
        font-family: Arial;
        font-size: 15px;
        font-weight: bold;
        padding: 6px 24px;
        text-decoration: none;
        text-shadow: 0px 1px 0px #b23e35;
    }

        .redButton:disabled {
            background-color: #808080;
            color: black;
        }

        .redButton:hover {
            background: linear-gradient(to bottom, #e4685d 5%, #fc8d83 100%);
            background-color: #e4685d;
        }

        .redButton:active {
            position: relative;
            top: 1px;
        }

    .css-input {
        padding: 5px;
        font-size: medium;
        border-width: 0px;
        border-color: #CCCCCC;
        font-family: Calibri;
        background-color: #FFFFFF;
        background-color: #FFFFFF;
        color: #000000;
        border-style: solid;
        border-radius: 7px;
        box-shadow: 0px 0px 5px rgba(66,66,66,.75);
        text-shadow: -50px 0px 0px rgba(66,66,66,.0);
    }

        .css-input:focus {
            outline: none;
        }


    .labelStyle {
        font-family: Calibri;
        font-size: large;
    }
</style>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alta de unidad para analisis</title>
    <script type="text/javascript">
        javascript: window.history.forward(1);
    </script>
</head>
<body style="text-align: center">
    <div style="text-align: left">

        <a href="MenuAnalisis.aspx" style="color: black;">
            <div class="largeCard">
                <div class="largecard_image">
                    <img src="https://img.icons8.com/color/48/000000/logout-rounded-left--v1.png" />
                </div>
                <div class="largecard_title">
                    <p>Volver</p>
                </div>
            </div>
        </a>
       
        <asp:Label Style="font-family: Calibri; position: absolute; top: 10px; left: 37%;" runat="server" Text="Alta de unidad para analisis" Font-Size="XX-Large" Font-Bold="true"></asp:Label>
        <br />
        <asp:Label Style="font-family: Calibri" runat="server" Text="Usuario actual:" Font-Bold="true" Font-Size="Larger"></asp:Label>
        <asp:Label Style="font-family: Calibri" ID="lblUsuario" runat="server" Text="Label" Font-Size="Larger"></asp:Label>
    </div>
    <br />
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanelGeneral" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Label ID="Label16" CssClass="labelStyle" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
                <asp:Label ID="Label1" CssClass="labelStyle" runat="server" Text="Part Number"></asp:Label>
                &nbsp;&nbsp;
       
                <ajaxToolkit:ComboBox ID="ddlpartnumber" runat="server" AutoPostBack="True" DropDownStyle="DropDownList"
                    AutoCompleteMode="Append">
                </ajaxToolkit:ComboBox>

                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                <br />
                <asp:Label ID="Label14" CssClass="labelStyle" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
                <asp:Label ID="Label2" CssClass="labelStyle" runat="server" Text="Work order"></asp:Label>
                &nbsp; &nbsp;&nbsp;
        <asp:TextBox ID="txtNumOrden" runat="server" CssClass="css-input" AutoCompleteType="Disabled" MaxLength="20"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="Label15" CssClass="labelStyle" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>

                <asp:Label ID="Label" CssClass="labelStyle" runat="server" Text="Falla"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtFalla" Style="resize: none;" CssClass="css-input" runat="server" TextMode="MultiLine" AutoCompleteType="Disabled" Width="220px" Height="33px" MaxLength="100"></asp:TextBox>
                <br />
                <br />
                <fieldset>
                    <legend class="labelStyle">Serial Number individuales:</legend>&nbsp;
               <br />
                    <asp:Label ID="Label3" CssClass="labelStyle" runat="server" Text="Serial number"></asp:Label>
                    &nbsp;&nbsp; &nbsp;&nbsp;
               <asp:TextBox ID="txtNumSerie" runat="server" AutoCompleteType="Disabled" CssClass="css-input" MaxLength="30"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               <asp:Button ID="btnAgregarNumSerie" runat="server" class="blueButton" OnClick="btnAgregarNumSerie_Click" Text="+ Serial num" Width="138px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               <asp:Button ID="btnLimpiargv" runat="server" class="redButton" Text="Limpiar" Width="138px" OnClick="btnLimpiargv_Click" />
                    <br />
                    <br />
                    <legend class="labelStyle">Serial Number(s) ingresado(s):</legend>&nbsp;
                    <div style="display: inline-block; overflow-y: scroll; height: 200px; position: center;">
                        <asp:Label ID="Label12" CssClass="labelStyle" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
                        <asp:Label ID="lblNumSerie" CssClass="labelStyle" runat="server" Text="Numeros de serie: 0"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:GridView ID="gvNumSerie" CssClass="labelStyle" runat="server" AutoGenerateSelectButton="True" OnSelectedIndexChanged="gvNumSerie_SelectedIndexChanged" OnRowDataBound="gvNumSerie_RowDataBound">
                   </asp:GridView>
                    </div>
                    <br />
                    <asp:Label ID="lblerrorSN" CssClass="labelStyle" runat="server" Text="Informacion incorrecta" ForeColor="#FF3300"></asp:Label>
                </fieldset>
                <asp:Label ID="lblerror" CssClass="labelStyle" runat="server" Text="Informacion incorrecta" ForeColor="#FF3300"></asp:Label>
                <br />
                <br />
                <asp:Button ID="btnAlta" runat="server" Text="Dar de alta" class="blueButton" OnClick="btnAlta_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="redButton" OnClick="btnCancelar_Click" />

                &nbsp;&nbsp;&nbsp;&nbsp;
        <br />
                <br />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="gvNumSerie" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="btnAgregarNumSerie" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnAlta" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnAgregarNumSerie" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>
