<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InicioSesion.aspx.cs" Inherits="EstadiaMWE.InicioSesion" %>

<!DOCTYPE html>

<style type="text/css">
    input {
        max-width: 100%;
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
        color: black;
        text-decoration: none;
        font-family: Calibri;
    }


    body {
        text-align: center;
    }

    form {
        background-color: white;
        background-repeat: round;
        background-attachment: fixed;
        text-align: center;
        width: 40%;
        border-radius: 40px 40px 40px 40px;
        display: inline-block;
        box-shadow: 2px 2px 10px 15px rgba(220, 211, 211, 0.10), -2px -2px 30px 15px rgba(0,0,0,0.22);
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
        }

        .greenButton:hover {
            background: linear-gradient(to bottom, #77a809 5%, #89c403 100%);
            background-color: #77a809;
        }

        .greenButton:active {
            position: relative;
            top: 1px;
        }


    .css-input {
        padding: 5px;
        font-size: 12px;
        border-width: 1px;
        border-color: #CCCCCC;
        background-color: #FFFFFF;
        color: #000000;
        border-style: solid;
        border-radius: 12px;
        box-shadow: 0px 0px 5px rgba(66,66,66,.75);
    }

        .css-input:focus {
            outline: none;
        }

    .labelStyle {
        font-family: Calibri;
        font-size: medium;
    }
</style>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript">
        javascript: window.history.forward(1);
    </script>
    <title>Inicio de sesion</title>
    <%--    <link rel="styles" runat="server" media="screen" href="~/css/styles.css" />--%>
</head>
<body>
    <div style="text-align: left">
        <a href="MenuPrincipal.aspx">
            <div class="largeCard">

                <div class="largecard_image">
                    <img src="https://img.icons8.com/color/48/000000/logout-rounded-left--v1.png" />
                </div>
                <div class="largecard_title">
                    <p>Volver al menu</p>
                </div>
            </div>
        </a>
    </div>
    <br />
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <br />
        <br />
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/logo2.png" />
        <br />
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Label runat="server" CssClass="labelStyle" Text="SISTEMA DE ANALISIS Y RE TRABAJO DE UNIDADES" Font-Bold="true" Font-Size="X-Large" />
                <br />
                <br />
                <asp:Label runat="server" CssClass="labelStyle" Text="Usuario" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtUsuario" CssClass="css-input" AutoCompleteType="Disabled" runat="server" MaxLength="30"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="Label2" runat="server" CssClass="labelStyle" Text="Contraseña"></asp:Label>
                &nbsp;
        <asp:TextBox ID="txtPassword" CssClass="css-input" runat="server" AutoCompleteType="Disabled" MaxLength="30" TextMode="Password"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="lblError" CssClass="labelStyle" runat="server" ForeColor="#FF3300" Text="Informacion incorrecta"></asp:Label>
                <br />
                <br />
                <asp:Button ID="btnEntrar" class="greenButton" runat="server" Text="Entrar" OnClick="btnEntrar_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnCancelar" class="redButton" runat="server" Text="Cancelar" />
                <br />
                <br />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnEntrar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>
