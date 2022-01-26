﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuMantenimiento.aspx.cs" Inherits="EstadiaMWE.MenuMantenimiento" %>

<!DOCTYPE html>


<style type="text/css">
    .cards-list {
        display: -webkit-box;
        display: -moz-box;
        display: -ms-flexbox;
        display: -webkit-flex;
        display: flex;
        align-items: center;
        justify-content: center;
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

    .card {
        margin: 30px auto;
        width: 200px;
        height: 200px;
        margin: 15px;
        border-radius: 10px;
        border: 1px solid #ccc;
        box-shadow: 5px 5px 10px 7px rgba(0,0,0,0.25), -5px -5px 10px 7px rgba(0,0,0,0.22);
        cursor: pointer;
        transition: 0.4s;
        display: inline-block;
    }

        .card .card_image {
            width: inherit;
            height: inherit;
            text-align: center;
            border-radius: 40px;
        }

            .card .card_image img {
                width: 150px;
                height: 150px;
                border-radius: 40px;
                object-fit: cover;
            }

        .card .card_title {
            text-align: center;
            border-radius: 0px 0px 40px 40px;
            font-family: Calibri;
            font-size: 24px;
            margin-top: -80px;
            height: 40px;
        }

        .card:hover {
            transform: scale(0.9, 0.9);
            box-shadow: 5px 5px 30px 15px rgba(0,0,0,0.25), -5px -5px 30px 15px rgba(0,0,0,0.22);
        }

    .title-white {
        color: white;
    }

    .title-black {
        color: black;
    }

    @media all and (max-width: 500px) {
        .card-list {
            /* On small screens, we are no longer using row direction but column */
            flex-direction: column;
        }
    }

    h1 {
        font-family: Calibri;
        text-shadow: 0 1px 0 #ccc, 0 2px 0 #c9c9c9, 0 3px 0 #bbb, 0 4px 0 #b9b9b9, 0 5px 0 #aaa, 0 6px 1px rgba(0,0,0,.1), 0 0 5px rgba(0,0,0,.1), 0 1px 3px rgba(0,0,0,.3), 0 3px 5px rgba(0,0,0,.2), 0 5px 10px rgba(0,0,0,.25), 0 10px 10px rgba(0,0,0,.2), 0 20px 20px rgba(0,0,0,.15);
    }

    footer {
        text-align: left;
        position: fixed;
        bottom: 0;
        left: 0
    }
</style>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
       <title>Menu de mantenimiento</title>
    <script type="text/javascript">
        javascript: window.history.forward(1);
    </script>
</head>

<body>
    <a href="Menu.aspx">
        <div class="largeCard">
            <div class="largecard_image">
                <img src="https://img.icons8.com/color/48/000000/logout-rounded-left--v1.png" />
            </div>
            <div class="largecard_title">
                <p>Volver</p>
            </div>
        </div>
    </a>
    <asp:Label Style="font-family: Calibri; position: absolute; top: 10px; left: 42%;" runat="server" Text="Menu Mantenimiento" Font-Size="XX-Large" Font-Bold="true"></asp:Label>
    <br />
    <asp:Label Style="font-family: Calibri" runat="server" Text="Usuario actual:" Font-Bold="true" Font-Size="Larger"></asp:Label>
    <asp:Label Style="font-family: Calibri" ID="lblUsuario" runat="server" Text="Label" Font-Size="Larger"></asp:Label>

    <div class="cards-list">

        <a href="Mantenimiento/Man_Defecto.aspx">
            <div class="card 1">

                <div class="card_image">
                    <img src="https://img.icons8.com/color/512/000000/high-priority.png" />
                </div>
                <div class="card_title">
                    <p>Defectos</p>
                </div>
            </div>
        </a>

        <a href="Mantenimiento/Man_Usuario.aspx">
            <div class="card 2">

                <div class="card_image">
                    <img src="https://img.icons8.com/color/512/000000/team-skin-type-7.png" />
                </div>
                <div class="card_title">
                    <p>Usuarios</p>
                </div>
            </div>
        </a>

        <a href="Mantenimiento/Man_Cliente.aspx">
            <div class="card 3">

                <div class="card_image">
                    <img src="https://img.icons8.com/color/512/000000/staff-skin-type-7.png" />
                </div>
                <div class="card_title">
                    <p>Clientes</p>
                </div>
            </div>
        </a>

    </div>
    <div class="cards-list" runat="server">

        <a href="Mantenimiento/Man_Area.aspx">
            <div class="card 1">

                <div class="card_image">
                    <img src="https://img.icons8.com/color/512/000000/location-off.png" />
                </div>
                <div class="card_title">
                    <p>Areas</p>
                </div>
            </div>
        </a>

        <a href="Mantenimiento/Man_NumParte.aspx">
            <div class="card 2">

                <div class="card_image">
                    <img src="https://img.icons8.com/color/512/000000/raspberry-pi-zero.png" />
                </div>
                <div class="card_title">
                    <p>Part Numbers</p>
                </div>
            </div>
        </a>
    </div>
</body>
</html>
