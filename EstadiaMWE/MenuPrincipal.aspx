<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuPrincipal.aspx.cs" Inherits="EstadiaMWE.MenuPrincipal" %>

<!DOCTYPE html>
<html>
<head>
       <title>Menu principal</title>
    <script type="text/javascript">
        javascript: window.history.forward(1);
    </script>

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
                font-size: 25px;
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
            text-shadow: 0 1px 0 #ccc, 0 2px 0 #c9c9c9, 0 3px 0 #bbb, 0 4px 0 #b9b9b9, 0 5px 0 #aaa, 0 6px 1px rgba(0,0,0,.1), 0 0 5px rgba(0,0,0,.1), 0 1px 3px rgba(0,0,0,.3), 0 3px 5px rgba(0,0,0,.2), 0 5px 10px rgba(0,0,0,.25), 0 10px 10px rgba(0,0,0,.2), 0 20px 20px rgba(0,0,0,.15);
            font-family: Calibri;
        }

        footer {
            text-align: left;
            position: fixed;
            bottom: 0;
            left: 0
        }

        a {
            color: black;
            text-decoration: none;
            font-family: Calibri;
        }
    </style>
</head>
<body>
    <h1 style="text-align: center">Menu principal</h1>

    <div class="cards-list">

        <a href="Alta_Analisis.aspx">
            <div class="card 1">

                <div class="card_image">
                    <img src="https://img.icons8.com/color/512/000000/search-more.png" />
                </div>
                <div class="card_title">
                    <p>Alta para analisis</p>
                </div>
            </div>
        </a>

        <a href="AltaUnidad.aspx">
            <div class="card 2">

                <div class="card_image">
                    <img src="https://img.icons8.com/color/512/000000/computer-support.png" />
                </div>
                <div class="card_title">
                    <p>Alta para retrabajo</p>
                </div>
            </div>
        </a>


    </div>
    <div style="text-align: center">
        <a href="InicioSesion.aspx">
            <div class="card 3">

                <div class="card_image">
                    <img src="https://img.icons8.com/color/512/000000/enter-2.png" />
                </div>
                <div class="card_title">
                    <p>Iniciar sesion</p>
                </div>
            </div>
        </a>
    </div>
</body>
</html>



