<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Man_NumParte.aspx.cs" Inherits="EstadiaMWE.Mantenimiento.Man_NumParte" %>

<!DOCTYPE html>


<style type="text/css">
    body {
        text-align: center;
    }

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
        color: Highlight;
    }

    .grid {
        display: inline-block;
    }

    form {
        background-color: white;
        background-repeat: round;
        background-attachment: fixed;
        text-align: left;
        width: 70%;
        display: inline-block;
    }


    .greenButton {
        box-shadow: inset 0px 1px 0px 0px #a4e271;
        background: linear-gradient(to bottom, #89c403 5%, #77a809 100%);
        background-color: #89c403;
        border-radius: 6px;
        border: 1px solid #74b807;
        display: inline-block;
        cursor: pointer;
        color: #ffffff;
        font-family: Arial;
        font-size: 15px;
        font-weight: bold;
        padding: 6px 24px;
        text-decoration: none;
        text-shadow: 0px 1px 0px #528009;
    }

        .greenButton:disabled {
            background-color: #808080;
            color: black;
            box-shadow: inset 0px 1px 0px 0px #808080;
            background: linear-gradient(to bottom, #808080 5%, #808080 100%);
        }

        .greenButton:hover {
            background: linear-gradient(to bottom, #77a809 5%, #89c403 100%);
            background-color: #77a809;
        }

        .greenButton:active {
            position: relative;
            top: 1px;
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
            box-shadow: inset 0px 1px 0px 0px #808080;
            color: black;
        }

        .blueButton:hover {
            background: linear-gradient(to bottom, #468ccf 5%, #63b8ee 100%);
            background-color: #353232;
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

    .ajax__combobox_itemlist {
        position: fixed !important;
    }
</style>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mantenimiento de Part Numbers</title>
    <script type="text/javascript">
        javascript: window.history.forward(1);
    </script>
</head>
<body style="text-align: center">
    <div style="text-align: left">

        <a href="../MenuMantenimiento.aspx" style="color: black;">
            <div class="largeCard">
                <div class="largecard_image">
                    <img src="https://img.icons8.com/color/48/000000/logout-rounded-left--v1.png" />
                </div>
                <div class="largecard_title">
                    <p>Volver</p>
                </div>
            </div>
        </a>
        <asp:Label Style="font-family: Calibri; position: absolute; top: 10px; left: 44%;" runat="server" Text="Part Numbers" Font-Size="XX-Large" Font-Bold="true"></asp:Label>
        <br />
        <asp:Label Style="font-family: Calibri" runat="server" Text="Usuario actual:" Font-Bold="true" Font-Size="Larger"></asp:Label>
        <asp:Label Style="font-family: Calibri" ID="lblUsuario" runat="server" Text="Label" Font-Size="Larger"></asp:Label>
    </div>
    <br />
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <%--<asp:UpdatePanel ID="UpdatePanelGeneral" runat="server" UpdateMode="Conditional">--%>
        <%--<ContentTemplate>
                <fieldset>--%>
        <legend class="labelStyle">Alta y modificaciones</legend>&nbsp;
                <br />
        <asp:Label ID="Label2" CssClass="labelStyle" runat="server" Text="Part Number"></asp:Label>
        &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtPartNumber" runat="server" CssClass="css-input"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label5" runat="server" CssClass="labelStyle" Text="Precio unitario"></asp:Label>
        &nbsp; &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="css-input"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label6" runat="server" CssClass="labelStyle" Text="Cliente"></asp:Label>
        &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   <ajaxToolkit:ComboBox ID="ddlCliente" runat="server" AutoPostBack="True" DropDownStyle="DropDownList"
                       AutoCompleteMode="Append">
                   </ajaxToolkit:ComboBox>
        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender1" runat="server" IsSorted="true" PromptCssClass="css-input" PromptPosition="Top" PromptText="click para buscar" QueryPattern="StartsWith" TargetControlID="ddlCliente">
        </ajaxToolkit:ListSearchExtender>
        <br />
        <asp:Label ID="lblerror" CssClass="labelStyle" runat="server" Text="Informacion incorrecta" ForeColor="#FF3300"></asp:Label>
        <br />
        <asp:Button ID="btnAlta" runat="server" CssClass="blueButton" Height="36px" OnClick="btnAlta_Click" Text="Dar de alta" Width="165px" />
        &nbsp;
                     <asp:Button ID="btnModificar" runat="server" CssClass="greenButton" Height="36px" OnClick="btnModificar_Click" Text="Modificar" Width="165px" />
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancelar" runat="server" CssClass="redButton" Height="36px" OnClick="btnCancelar_Click" Text="Cancelar" Width="165px" />
        <br />
        <br />
        <%--</fieldset>
                <fieldset>--%>
        <legend class="labelStyle">Busqueda</legend>
        &nbsp;<asp:Label ID="Label4" runat="server" CssClass="labelStyle" Text="Part Number"></asp:Label>
        &nbsp;
                        <asp:TextBox ID="txtBuscarPartNumber" runat="server" AutoCompleteType="Disabled" CssClass="css-input" MaxLength="30"></asp:TextBox>
        &nbsp;
                    <asp:Button ID="btnBuscar" runat="server" CssClass="greenButton" Height="36px" Text="Buscar" Width="116px" OnClick="btnBuscar_Click" />
        &nbsp;&nbsp;<asp:Button ID="btnLimpiar" runat="server" CssClass="blueButton" Height="36px" Text="Limpiar" Width="124px" OnClick="btnLimpiar_Click" />
        <br />
        <%--</fieldset>--%>
                &nbsp;<asp:GridView ID="gvConsulta" runat="server" Style="font-family: Calibri" AutoGenerateSelectButton="True" OnSelectedIndexChanged="gvConsulta_SelectedIndexChanged" OnRowCreated="gvConsulta_RowCreated">
                    <SelectedRowStyle BackColor="#FFCCFF" />
                </asp:GridView>
        <br />
        <%--            </ContentTemplate>--%>
        <%--<Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnAlta" EventName="Click" />
            </Triggers>--%>
        <%--</asp:UpdatePanel>--%>
    </form>
</body>
</html>

