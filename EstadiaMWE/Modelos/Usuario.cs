using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EstadiaMWE.Modelos
{

    public class Usuario
    {
        int id_Usuario;
        string nombre_Empleado;
        string username;
        string password;
        int fK_AreaInt;
        int fK_TipoUsuario;
        string num_Empleado;

        public int Id_Usuario { get => id_Usuario; set => id_Usuario = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public int FK_AreaInt { get => fK_AreaInt; set => fK_AreaInt = value; }
        public int FK_TipoUsuario { get => fK_TipoUsuario; set => fK_TipoUsuario = value; }
        public string Num_Empleado { get => num_Empleado; set => num_Empleado = value; }
        public string Nombre_Empleado { get => nombre_Empleado; set => nombre_Empleado = value; }
    }
}