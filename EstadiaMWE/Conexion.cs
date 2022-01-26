using EstadiaMWE.Control;
using EstadiaMWE.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace EstadiaMWE
{
    public static class Conexion
    {

        private static SqlConnection miconexion = new SqlConnection(@"Data Source=192.168.1.4\SQLEXPRESS;Initial Catalog=ayrDB;User ID=admin;Password=mbettaglo;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        //private static void Abrir()
        //{
        //    miconexion.Open();
        //}

        //private static void Cerrar()
        //{
        //    miconexion.Close();
        //}

        #region Altas

        public static void Alta_Unidad(Modelos.Unidad nuevaUnidad, string estatus, string num_empleado, List<string> num_serie,
            bool esNuevaUnidad, List<Modelos.Defecto> defectos_seleccionados)
        {
            foreach (var numero_serie in num_serie)
            {
                miconexion.Open();
                SqlCommand cmd = miconexion.CreateCommand();
                cmd.CommandText = " INSERT INTO UNIDAD(Work_Order,";

                if (estatus != "Analisis" || esNuevaUnidad)
                {
                    cmd.CommandText += "FK_Area,";
                }

                cmd.CommandText += "FK_PartNumber, Serial_Number, FK_Status";

                if (nuevaUnidad.Falla != null)
                {
                    cmd.CommandText += ", Falla";
                }

                cmd.CommandText += ") values('" + nuevaUnidad.Work_Order + "',";


                if (estatus != "Analisis" || esNuevaUnidad)
                {
                    cmd.CommandText += nuevaUnidad.FK_Area + ",";
                }

                cmd.CommandText += nuevaUnidad.FK_PartNumber + ",'" + numero_serie + "'," + nuevaUnidad.FK_Status;

                if (nuevaUnidad.Falla != null)
                {
                    cmd.CommandText += ",'" + nuevaUnidad.Falla + "'";
                }

                cmd.CommandText += ")";

                cmd.ExecuteNonQuery();

                miconexion.Close();

                string defectos = "";
                string referencias = "";
                string partnumbers = "";
                foreach (Modelos.Defecto defecto in defectos_seleccionados)
                {
                    string[] defecto_indv = defecto.defecto.Split('-');
                    referencias += defecto.Referencia + ",";
                    defectos += defecto_indv[0] + ",";
                    if (defecto.Part_Number != "")
                    {
                        partnumbers += defecto.Part_Number + ",";
                    }
                    else
                    {
                        partnumbers += "N/A,";
                    }
                }

                Alta_Bitacora(new Modelos.Bitacora
                {
                    FK_Unidad = obtenerUltimoID("UNIDAD"),
                    _Status = estatus,
                    _Turno = int.Parse(DateTime.Now.TimeOfDay.ToString("hh")) >= 5 && int.Parse(DateTime.Now.TimeOfDay.ToString("hh")) <= 16 ? "Primero" : "Segundo",
                    _NumEmpleado = num_empleado,
                    Fecha = DateTime.Now,
                    Falla = nuevaUnidad.Falla,
                    Defectos = defectos,
                    Referencias = referencias,
                    Part_Numbers = partnumbers
                });

                if (nuevaUnidad.FK_Status == ConsultaEstatus("RWK").Id_Status)
                {
                    //AGREGAR DEFECTOS DE LA UNIDAD SI EXISTEN
                    int i = 0;
                    foreach (var defecto in defectos_seleccionados)
                    {
                        Alta_Unidad_Defecto(defecto, true, nuevaUnidad);
                        i++;
                    }

                }
            }

        }

        private static void Alta_Bitacora(Modelos.Bitacora bitacora)
        {
            miconexion.Open();
            SqlCommand cmd = miconexion.CreateCommand();

            cmd.CommandText = "INSERT INTO BITACORA(FK_Unidad, _Status, _Turno, _NumEmpleado, _Fecha";

            if (bitacora.Falla != null)
            {
                cmd.CommandText += ", Falla";
            }

            if (bitacora.Defectos != "")
            {
                cmd.CommandText += ", Defectos, Referencias";
            }

            if (bitacora.Part_Numbers != "")
            {
                cmd.CommandText += ", Part_Numbers";
            }

            cmd.CommandText += ") values(" + bitacora.FK_Unidad +
                ",'" + bitacora._Status +
                "','" + bitacora._Turno +
                "','" + bitacora._NumEmpleado +
                "','" + bitacora.Fecha + "'";

            if (bitacora.Falla != null)
            {
                cmd.CommandText += ",'" + bitacora.Falla + "'";
            }
            if (bitacora.Defectos != "")
            {
                cmd.CommandText += ",'" + bitacora.Defectos + "','" + bitacora.Referencias + "'";
            }
            if (bitacora.Part_Numbers != "")
            {
                cmd.CommandText += ",'" + bitacora.Part_Numbers + "'";
            }
            cmd.CommandText += ")";

            cmd.ExecuteNonQuery();
            miconexion.Close();
        }

        public static void Alta_Unidad_Defecto(Modelos.Defecto defecto, bool nuevo, Modelos.Unidad unidad)
        {
            int id_unidad = unidad.Id_Unidad;

            if (nuevo)
            {
                id_unidad = obtenerUltimoID("UNIDAD");
            }

            SqlCommand cmd = miconexion.CreateCommand();
            cmd.CommandText = " INSERT INTO UNIDAD_DEFECTO(FK_Unidad, FK_Defecto, Referencia";

            if (defecto.Part_Number != "")
            {
                cmd.CommandText += ", Part_Number";
            }

            cmd.CommandText += ") values (" + id_unidad + "," + defecto.Id_Defecto + ",'" + defecto.Referencia + "'";

            if (defecto.Part_Number != "")
            {
                cmd.CommandText += ",'" + defecto.Part_Number + "'";
            }

            cmd.CommandText += ")";

            miconexion.Open();
            try
            {
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }

            miconexion.Close();

        }

        public static void Alta_Defecto(Modelos.Defecto defecto)
        {
            miconexion.Open();
            SqlCommand cmd = miconexion.CreateCommand();

            cmd.CommandText = " INSERT INTO CAT_DEFECTO values('" + defecto.Codigo + "'," +
                "'" + defecto.Descripcion + "')";

            cmd.ExecuteNonQuery();

            miconexion.Close();
        }

        public static void Alta_NumParte(Modelos.NumParte numparte)
        {
            miconexion.Open();
            SqlCommand cmd = miconexion.CreateCommand();

            cmd.CommandText = " INSERT INTO CAT_NUMPARTE values('" + numparte.Num_Parte + "'," +
                "" + numparte.Precio_Unitario + "," + numparte.FK_Cliente + ")";

            cmd.ExecuteNonQuery();

            miconexion.Close();
        }

        public static void Alta_Cliente(Modelos.ModeloCliente cliente)
        {
            miconexion.Open();
            SqlCommand cmd = miconexion.CreateCommand();

            cmd.CommandText = " INSERT INTO CAT_CLIENTE values('" + cliente.Cliente + "')";

            cmd.ExecuteNonQuery();

            miconexion.Close();
        }

        public static void Alta_Usuario(Modelos.Usuario usuario)
        {
            miconexion.Open();
            SqlCommand cmd = miconexion.CreateCommand();

            cmd.CommandText = " INSERT INTO USUARIO values('" + usuario.Nombre_Empleado + "'" +
                ",'" + usuario.Username + "'" +
                ",'" + usuario.Password + "'" +
                "," + usuario.FK_AreaInt + "" +
                "," + usuario.FK_TipoUsuario + "" +
                ",'" + usuario.Num_Empleado + "')";

            cmd.ExecuteNonQuery();

            miconexion.Close();
        }

        public static void Alta_Area(Modelos.ModeloArea area)
        {
            miconexion.Open();
            SqlCommand cmd = miconexion.CreateCommand();

            cmd.CommandText = " INSERT INTO CAT_AREA values('" + area.Area + "')";

            cmd.ExecuteNonQuery();

            miconexion.Close();
        }

        #endregion

        #region Modificaciones
        public static void Modificar_Unidad(Modelos.Status estatus, string num_empleado, Modelos.Unidad unidad,
            List<Modelos.Defecto> defectos_seleccionados)
        {
            miconexion.Open();
            SqlCommand cmd = miconexion.CreateCommand();


            cmd.CommandText = " UPDATE UNIDAD SET " +
                "Work_Order = '" + unidad.Work_Order + "',";

            if (unidad.FK_Area > 0)
            {
                cmd.CommandText += "FK_Area = " + unidad.FK_Area + ",";
            }

            cmd.CommandText += "FK_PartNumber = " + unidad.FK_PartNumber + "," +
                "Serial_Number = '" + unidad.Serial_Number + "'," +
                "FK_Status = " + estatus.Id_Status;

            if (unidad.Falla != "" && unidad.Falla != "N/A")
            {
                cmd.CommandText += ",Falla = '" + unidad.Falla + "'";
            }

            cmd.CommandText += " WHERE Id_Unidad = " + unidad.Id_Unidad;

            cmd.ExecuteNonQuery();

            miconexion.Close();

            //if (Globales.unidad_seleccionada.FK_Status != estatus.Id_Status)
            //{

            string defectos = "";
            string referencias = "";
            string partnumbers = "";
            foreach (Modelos.Defecto defecto in defectos_seleccionados)
            {
                string[] defecto_indv = defecto.defecto.Split('-');
                referencias += defecto.Referencia + ",";
                defectos += defecto_indv[0] + ",";
                if (defecto.Part_Number != "")
                {
                    partnumbers += defecto.Part_Number + ",";
                }
                else
                {
                    partnumbers += "N/A,";
                }
            }

            Alta_Bitacora(new Modelos.Bitacora
            {
                FK_Unidad = unidad.Id_Unidad,
                _Status = estatus.Nombre_Status,
                _Turno = int.Parse(DateTime.Now.TimeOfDay.ToString("hh")) >= 5 && int.Parse(DateTime.Now.TimeOfDay.ToString("hh")) <= 16 ? "Primero" : "Segundo",
                _NumEmpleado = num_empleado,
                Fecha = DateTime.Now,
                Falla = unidad.Falla,
                Defectos = defectos,
                Referencias = referencias,
                Part_Numbers = partnumbers
            });

            //}

            filtrarDefectos(unidad, defectos_seleccionados);

        }

        public static void Modificar_Defecto(Modelos.Defecto defecto)
        {
            miconexion.Open();
            SqlCommand cmd = miconexion.CreateCommand();

            cmd.CommandText = " UPDATE CAT_DEFECTO SET Codigo = '" + defecto.Codigo + "'," +
                "Descripcion = '" + defecto.Descripcion + "' WHERE Id_Defecto = " + defecto.Id_Defecto;

            cmd.ExecuteNonQuery();

            miconexion.Close();
        }

        public static void Modificar_NumParte(Modelos.NumParte numparte)
        {
            miconexion.Open();
            SqlCommand cmd = miconexion.CreateCommand();

            cmd.CommandText = " UPDATE CAT_NUMPARTE SET " +
                "Num_Parte = '" + numparte.Num_Parte + "'," +
                "Precio_unitario = " + numparte.Precio_Unitario + "," +
                "FK_Cliente = " + numparte.FK_Cliente + " WHERE Id_NumParte = " + numparte.Id_NumParte;

            cmd.ExecuteNonQuery();

            miconexion.Close();
        }

        public static void Modificar_Cliente(Modelos.ModeloCliente cliente)
        {
            miconexion.Open();
            SqlCommand cmd = miconexion.CreateCommand();

            cmd.CommandText = " UPDATE CAT_CLIENTE SET Cliente='" + cliente.Cliente + "' WHERE Id_Cliente = " + cliente.Id_Cliente;

            cmd.ExecuteNonQuery();

            miconexion.Close();
        }

        public static void Modificar_Usuario(Modelos.Usuario usuario)
        {
            miconexion.Open();
            SqlCommand cmd = miconexion.CreateCommand();

            cmd.CommandText = " UPDATE USUARIO SET Nombre_Empleado = '" + usuario.Nombre_Empleado + "'" +
                ",Username = '" + usuario.Username + "'" +
                ",Password = '" + usuario.Password + "'" +
                ",FK_AreaInt = " + usuario.FK_AreaInt + "" +
                ",FK_TipoUsuario = " + usuario.FK_TipoUsuario + "" +
                ",Num_Empleado = '" + usuario.Num_Empleado + "' WHERE Id_Usuario = " + usuario.Id_Usuario;

            cmd.ExecuteNonQuery();

            miconexion.Close();
        }

        public static void Modificar_Area(Modelos.ModeloArea area)
        {
            miconexion.Open();
            SqlCommand cmd = miconexion.CreateCommand();

            cmd.CommandText = " UPDATE CAT_AREA SET Area = '" + area.Area + "' WHERE Id_Area = " + area.Id_Area;

            cmd.ExecuteNonQuery();

            miconexion.Close();
        }
        #endregion

        #region Validaciones
        public static bool validarUsuario(string usuario, string password)
        {
            miconexion.Open();
            SqlCommand cmd = new SqlCommand("select * from USUARIO where username = '" + usuario + "' and password = '" + password + "'", miconexion);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                miconexion.Close();
                return true;
            }
            else
            {
                miconexion.Close();
                return false;
            }
        }
        #endregion

        #region Consultas
        /// <summary>
        /// Metodo Para saber si ya existe un registro
        /// </summary>
        /// <param name="comando"></param>
        /// <returns></returns>
        public static bool Existe(string comando)
        {
            miconexion.Open();
            using (SqlCommand cmd = new SqlCommand(comando, miconexion))
            {
                //miconexion.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                miconexion.Close();
                if (count == 0)
                    return false;
                else
                    return true;
            }
        }
        public static DataSet ConsultaGeneral(string tabla)
        {
            string query = "";
            switch (tabla)
            {
                case "CAT_DEFECTO":
                    query = "SELECT Id_Defecto, Codigo, Descripcion, Codigo + '-' + Descripcion AS FullDefecto FROM CAT_DEFECTO";
                    break;
                case "CAT_NUMPARTE":
                    query = "select CAT_NUMPARTE.Id_NumParte, CAT_NUMPARTE.Num_Parte, CAT_NUMPARTE.Precio_unitario,CAT_CLIENTE.Cliente from CAT_NUMPARTE INNER JOIN CAT_CLIENTE ON CAT_NUMPARTE.FK_Cliente = CAT_CLIENTE.Id_Cliente";
                    break;
                case "USUARIO":
                    query = "SELECT USUARIO.Id_Usuario, USUARIO.Nombre_Empleado, USUARIO.username, USUARIO.password, CAT_AREAINTERNA.AreaInt,CAT_TIPOUSUARIO.TipoUsuario," +
                        " USUARIO.Num_Empleado FROM USUARIO INNER JOIN CAT_AREAINTERNA ON USUARIO.FK_AreaInt = CAT_AREAINTERNA.Id_AreaInterna " +
                        "INNER JOIN CAT_TIPOUSUARIO ON USUARIO.FK_TipoUsuario = CAT_TIPOUSUARIO.Id_TipoUsuario";
                    break;
                case "CAT_TIPOUSUARIO":
                    query = "select * from CAT_TIPOUSUARIO WHERE TipoUsuario != 'Guest'";
                    break;
                default:
                    query = "SELECT * FROM " + tabla;
                    break;
            }
            miconexion.Open();
            SqlCommand cmd = new SqlCommand(query, miconexion);
            cmd.ExecuteNonQuery();
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adaptador.Fill(ds);
            miconexion.Close();
            return ds;
        }

        public static DataSet ConsultaGeneral(string tabla, string nombre_valor, string valor)
        {
            string query = "";
            switch (tabla)
            {
                case "CAT_DEFECTO":
                    query = "SELECT Id_Defecto, Codigo, Descripcion, Codigo + '-' + Descripcion AS FullDefecto FROM CAT_DEFECTO WHERE " + nombre_valor + " = " + valor;
                    break;
                case "CAT_NUMPARTE":
                    query = "select CAT_NUMPARTE.Id_NumParte, CAT_NUMPARTE.Num_Parte, CAT_NUMPARTE.Precio_unitario,CAT_CLIENTE.Cliente " +
                        "from CAT_NUMPARTE INNER JOIN CAT_CLIENTE ON CAT_NUMPARTE.FK_Cliente = CAT_CLIENTE.Id_Cliente WHERE " + nombre_valor + " = " + valor;
                    break;
                case "USUARIO":
                    query = "SELECT USUARIO.Id_Usuario, USUARIO.Nombre_Empleado, USUARIO.username, USUARIO.password, CAT_AREAINTERNA.AreaInt,CAT_TIPOUSUARIO.TipoUsuario," +
                        " USUARIO.Num_Empleado FROM USUARIO INNER JOIN CAT_AREAINTERNA ON USUARIO.FK_AreaInt = CAT_AREAINTERNA.Id_AreaInterna " +
                        "INNER JOIN CAT_TIPOUSUARIO ON USUARIO.FK_TipoUsuario = CAT_TIPOUSUARIO.Id_TipoUsuario WHERE " + nombre_valor + " = " + valor;
                    break;
                default:
                    query = "SELECT * FROM " + tabla + " WHERE " + nombre_valor + " = " + valor;
                    break;
            }
            miconexion.Open();
            SqlCommand cmd = new SqlCommand(query, miconexion);
            cmd.ExecuteNonQuery();
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adaptador.Fill(ds);
            miconexion.Close();
            return ds;
        }


        public static DataSet consultaReporte()
        {
            string query = "SELECT DISTINCT TOP 100 UNIDAD.Id_Unidad,UNIDAD.Work_Order as 'Work Order',UNIDAD.Serial_Number as 'Serial Number'," +
                           "CAT_NUMPARTE.Num_Parte as 'Part Number',IsNull(CAT_AREA.Area, 'N/A') as Area," +
                           "CAT_STATUS.Nombre_status as 'Estatus actual',  IsNull(UNIDAD.Falla, 'N/A') as Falla," +
                           "(select TOP 1 _NumEmpleado from bitacora where FK_Unidad = UNIDAD.Id_Unidad order by _Fecha desc) as 'Num Empleado'," +
                           "(select TOP 1 _Turno from bitacora where FK_Unidad = UNIDAD.Id_Unidad order by _Fecha desc) as 'Turno', " +
                           "IsNull(CONVERT(VARCHAR,(SELECT min(_Fecha) from BITACORA WHERE BITACORA.FK_Unidad = UNIDAD.Id_Unidad and BITACORA._Status = 'Analisis'),121),'N/A') as Analisis," +
                           "IsNull(CONVERT(VARCHAR, (SELECT min(_Fecha) from BITACORA WHERE BITACORA.FK_Unidad = UNIDAD.Id_Unidad and BITACORA._Status = 'RWK'),121),'N/A') as RWK," +
                           "IsNull(CONVERT(VARCHAR, (SELECT min(_Fecha) from BITACORA WHERE BITACORA.FK_Unidad = UNIDAD.Id_Unidad and BITACORA._Status = 'SCRAP'),121),'N/A') as SCRAP," +
                           "IsNull(CONVERT(VARCHAR, (SELECT min(_Fecha) from BITACORA WHERE BITACORA.FK_Unidad = UNIDAD.Id_Unidad and BITACORA._Status = 'CALIDAD'),121),'N/A') as CALIDAD," +
                           "IsNull(STUFF((" +
                           "SELECT ',' + CAT_DEFECTO.Codigo " +
                           "FROM UNIDAD_DEFECTO " +
                           "INNER JOIN CAT_DEFECTO ON UNIDAD_DEFECTO.FK_Defecto = CAT_DEFECTO.Id_Defecto AND UNIDAD_DEFECTO.FK_Unidad = UNIDAD.Id_Unidad " +
                           "FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, ''), 'N/A') as Defectos, " +
                           "IsNull(STUFF(( " +
                           "SELECT ',' + UNIDAD_DEFECTO.Referencia " +
                           "FROM UNIDAD_DEFECTO " +
                           "where UNIDAD_DEFECTO.FK_Unidad = UNIDAD.Id_Unidad " +
                           "FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, ''), 'N/A') as Referencias, " +
                           "IsNull(STUFF(( SELECT ',' + UNIDAD_DEFECTO.Part_Number FROM UNIDAD_DEFECTO where UNIDAD_DEFECTO.FK_Unidad = UNIDAD.Id_Unidad FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, ''), 'N/A') as Part_Numbers  " +
                           "from UNIDAD " +
                           "LEFT JOIN  CAT_AREA ON UNIDAD.FK_Area = CAT_AREA.Id_Area " +
                           "INNER JOIN BITACORA ON UNIDAD.Id_Unidad = BITACORA.FK_Unidad " +
                           "INNER JOIN CAT_STATUS ON UNIDAD.FK_Status = CAT_STATUS.Id_Status and BITACORA._Status = CAT_STATUS.Nombre_status " +
                           "INNER JOIN CAT_NUMPARTE ON UNIDAD.FK_PartNumber = CAT_NUMPARTE.Id_NumParte";

            miconexion.Open();
            SqlCommand cmd = new SqlCommand(query, miconexion);
            cmd.ExecuteNonQuery();
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adaptador.Fill(ds);
            miconexion.Close();
            return ds;
        }

        public static DataSet consultaReporte(string empleado, string estatus, string turno, string wo, int partnumber, string serialnumber,
            string fecha_entrada, string fecha_salida)
        {
            int i = 0;

            string query = "SELECT DISTINCT TOP 100 UNIDAD.Id_Unidad,UNIDAD.Work_Order as 'Work Order',UNIDAD.Serial_Number as 'Serial Number'," +
                           "CAT_NUMPARTE.Num_Parte as 'Part Number',IsNull(CAT_AREA.Area, 'N/A') as Area," +
                           "CAT_STATUS.Nombre_status as 'Estatus actual',  IsNull(UNIDAD.Falla, 'N/A') as Falla," +
                           "(select TOP 1 _NumEmpleado from bitacora where FK_Unidad = UNIDAD.Id_Unidad order by _Fecha desc) as 'Num Empleado'," +
                           "(select TOP 1 _Turno from bitacora where FK_Unidad = UNIDAD.Id_Unidad order by _Fecha desc) as 'Turno', " +
                "IsNull(CONVERT(VARCHAR,(SELECT min(_Fecha) from BITACORA WHERE BITACORA.FK_Unidad = UNIDAD.Id_Unidad and BITACORA._Status = 'Analisis'),121),'N/A') as Analisis," +
                "IsNull(CONVERT(VARCHAR, (SELECT min(_Fecha) from BITACORA WHERE BITACORA.FK_Unidad = UNIDAD.Id_Unidad and BITACORA._Status = 'RWK'),121),'N/A') as RWK," +
                "IsNull(CONVERT(VARCHAR, (SELECT min(_Fecha) from BITACORA WHERE BITACORA.FK_Unidad = UNIDAD.Id_Unidad and BITACORA._Status = 'SCRAP'),121),'N/A') as SCRAP," +
                "IsNull(CONVERT(VARCHAR, (SELECT min(_Fecha) from BITACORA WHERE BITACORA.FK_Unidad = UNIDAD.Id_Unidad and BITACORA._Status = 'CALIDAD'),121),'N/A') as CALIDAD," +
                "IsNull(STUFF((" +
                "SELECT ',' + CAT_DEFECTO.Codigo " +
                "FROM UNIDAD_DEFECTO " +
                "INNER JOIN CAT_DEFECTO ON UNIDAD_DEFECTO.FK_Defecto = CAT_DEFECTO.Id_Defecto AND UNIDAD_DEFECTO.FK_Unidad = UNIDAD.Id_Unidad " +
                "FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, ''), 'N/A') as Defectos, " +
                "IsNull(STUFF(( " +
                "SELECT ',' + UNIDAD_DEFECTO.Referencia " +
                "FROM UNIDAD_DEFECTO " +
                "where UNIDAD_DEFECTO.FK_Unidad = UNIDAD.Id_Unidad " +
                "FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, ''), 'N/A') as Referencias, " +
                "IsNull(STUFF(( SELECT ',' + UNIDAD_DEFECTO.Part_Number FROM UNIDAD_DEFECTO where UNIDAD_DEFECTO.FK_Unidad = UNIDAD.Id_Unidad FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, ''), 'N/A') as Part_Numbers  " +
                "from UNIDAD " +
                "LEFT JOIN  CAT_AREA ON UNIDAD.FK_Area = CAT_AREA.Id_Area " +
                "INNER JOIN BITACORA ON UNIDAD.Id_Unidad = BITACORA.FK_Unidad " +
                "INNER JOIN CAT_STATUS ON UNIDAD.FK_Status = CAT_STATUS.Id_Status and BITACORA._Status = CAT_STATUS.Nombre_status " +
                "INNER JOIN CAT_NUMPARTE ON UNIDAD.FK_PartNumber = CAT_NUMPARTE.Id_NumParte " +
                "WHERE ";

            if (empleado != "")
            {
                query += "BITACORA._NumEmpleado = '" + empleado + "'";
                i++;
            }
            if (estatus != "Select All")
            {
                if (i > 0)
                {
                    query += " and ";
                }

                query += "CAT_STATUS.Nombre_status = '" + estatus + "'";
                i++;
            }
            if (turno != "Select All")
            {
                if (i > 0)
                {
                    query += " and ";
                }
                i++;
                query += "BITACORA._Turno = '" + turno + "'";
            }
            if (wo != "")
            {
                if (i > 0)
                {
                    query += " and ";
                }
                i++;
                query += "UNIDAD.Work_Order = '" + wo + "'";
            }
            if (partnumber != -1)
            {
                if (i > 0)
                {
                    query += " and ";
                }
                i++;
                query += "UNIDAD.FK_PartNumber = " + partnumber + "";
            }
            if (serialnumber != "")
            {
                if (i > 0)
                {
                    query += " and ";
                }
                i++;
                query += "UNIDAD.Serial_Number = '" + serialnumber + "'";
            }
            if (fecha_entrada != "")
            {
                if (i > 0)
                {
                    query += " and ";
                }
                i++;
                query += "cast((SELECT min(_Fecha) from BITACORA WHERE BITACORA.FK_Unidad = UNIDAD.Id_Unidad) as date) = '" + fecha_entrada + "'";
            }
            if (fecha_salida != "")
            {
                if (i > 0)
                {
                    query += " and ";
                }

                query += "cast((SELECT min(_Fecha) from BITACORA WHERE BITACORA.FK_Unidad = UNIDAD.Id_Unidad and BITACORA._Status = CAT_STATUS.Nombre_status) as date) ='" + fecha_salida + "'";
            }

            miconexion.Open();
            SqlCommand cmd = new SqlCommand(query, miconexion);
            cmd.ExecuteNonQuery();
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adaptador.Fill(ds);
            miconexion.Close();
            return ds;
        }

        public static DataSet consultaBitacora()
        {
            string query = "SELECT TOP 100 UNIDAD.Id_Unidad,UNIDAD.Work_Order as 'Work Order',UNIDAD.Serial_Number as 'Serial Number'," +
                "CAT_NUMPARTE.Num_Parte as 'Part Number',IsNull(CAT_AREA.Area, 'N/A') as Area, " +
                "BITACORA._Status as Estatus," +
                "BITACORA._NumEmpleado as 'Num Empleado', BITACORA._Turno as Turno,BITACORA._Fecha as Fecha, " +
                "IsNull(BITACORA.Falla, 'N/A') as Falla ,IsNull(BITACORA.Defectos, 'N/A') as Defectos,IsNull(BITACORA.Referencias, 'N/A') as Referencias, " +
                "IsNull(BITACORA.Part_Numbers, 'N/A') as 'Part_Numbers_Componente' from BITACORA" +
                " INNER JOIN UNIDAD ON BITACORA.FK_Unidad = UNIDAD.Id_UnidaD " +
                "LEFT JOIN  CAT_AREA ON UNIDAD.FK_Area = CAT_AREA.Id_Area " +
                "INNER JOIN CAT_NUMPARTE ON UNIDAD.FK_PartNumber = CAT_NUMPARTE.Id_NumParte";

            miconexion.Open();
            SqlCommand cmd = new SqlCommand(query, miconexion);
            cmd.ExecuteNonQuery();
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adaptador.Fill(ds);
            miconexion.Close();
            return ds;
        }

        public static DataSet consultaBitacora(string empleado, string estatus, string turno, string wo, int partnumber, string serialnumber, string fecha)
        {
            int i = 0;

            string query = "SELECT TOP 100 UNIDAD.Id_Unidad,UNIDAD.Work_Order as 'Work Order',UNIDAD.Serial_Number as 'Serial Number'," +
                "CAT_NUMPARTE.Num_Parte as 'Part Number',IsNull(CAT_AREA.Area, 'N/A') as Area, " +
                "BITACORA._Status as Estatus," +
                "BITACORA._NumEmpleado as 'Num Empleado', BITACORA._Turno as Turno,BITACORA._Fecha as Fecha, " +
                "IsNull(BITACORA.Falla, 'N/A') as Falla ,IsNull(BITACORA.Defectos, 'N/A') as Defectos,IsNull(BITACORA.Referencias, 'N/A') as Referencias, " +
                " IsNull(BITACORA.Part_Numbers, 'N/A') as 'Part_Numbers_Componente' from BITACORA" +
               " INNER JOIN UNIDAD ON BITACORA.FK_Unidad = UNIDAD.Id_UnidaD " +
               "LEFT JOIN  CAT_AREA ON UNIDAD.FK_Area = CAT_AREA.Id_Area " +
               "INNER JOIN CAT_NUMPARTE ON UNIDAD.FK_PartNumber = CAT_NUMPARTE.Id_NumParte " +
               "WHERE ";

            if (empleado != "")
            {
                query += "BITACORA._NumEmpleado = '" + empleado + "'";
                i++;
            }
            if (estatus != "Select All")
            {
                if (i > 0)
                {
                    query += " and ";
                }

                query += "BITACORA._Status = '" + estatus + "'";
                i++;
            }
            if (turno != "Select All")
            {
                if (i > 0)
                {
                    query += " and ";
                }
                i++;
                query += "BITACORA._Turno = '" + turno + "'";
            }
            if (wo != "")
            {
                if (i > 0)
                {
                    query += " and ";
                }
                i++;
                query += "UNIDAD.Work_Order = '" + wo + "'";
            }
            if (partnumber != -1)
            {
                if (i > 0)
                {
                    query += " and ";
                }
                i++;
                query += "UNIDAD.FK_PartNumber = " + partnumber + "";
            }
            if (serialnumber != "")
            {
                if (i > 0)
                {
                    query += " and ";
                }
                i++;
                query += "UNIDAD.Serial_Number = '" + serialnumber + "'";
            }
            if (fecha != "")
            {
                if (i > 0)
                {
                    query += " and ";
                }
                i++;
                query += " cast (BITACORA._Fecha as date) = '" + fecha + "'";
            }

            query += " order by BITACORA._Fecha asc ";

            miconexion.Open();
            SqlCommand cmd = new SqlCommand(query, miconexion);
            cmd.ExecuteNonQuery();
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adaptador.Fill(ds);
            miconexion.Close();
            return ds;
        }

        public static string consultaFechaAlta(string id_unidad)
        {
            string query = "SELECT MIN(_Fecha) FROM BITACORA where FK_Unidad = " + id_unidad;
            SqlCommand command = new SqlCommand(query, miconexion);
            miconexion.Open();
            var fecha_alta = command.ExecuteScalar();
            miconexion.Close();
            return fecha_alta.ToString();
        }

        public static DataSet ConsultaUnidadEstatus(Modelos.Status estatus)
        {
            string query = "SELECT TOP 100 UNIDAD.Id_Unidad,UNIDAD.Work_Order as 'Work Order',IsNull(CAT_AREA.Area, 'N/A') as Area, CAT_NUMPARTE.Num_Parte as 'Part Number',UNIDAD.Serial_Number as 'Serial Number', CAT_STATUS.Nombre_status as Estatus,  IsNull(UNIDAD.Falla, 'N/A') as Falla from UNIDAD LEFT OUTER JOIN  CAT_AREA ON UNIDAD.FK_Area = CAT_AREA.Id_Area INNER JOIN CAT_STATUS ON UNIDAD.FK_Status = CAT_STATUS.Id_Status INNER JOIN CAT_NUMPARTE ON UNIDAD.FK_PartNumber = CAT_NUMPARTE.Id_NumParte ";

            if (estatus.Nombre_Status != "All")
            {
                query += " where UNIDAD.FK_Status = '" + estatus.Id_Status + "'";
            }
            miconexion.Open();
            SqlCommand cmd = new SqlCommand(query, miconexion);
            cmd.ExecuteNonQuery();
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adaptador.Fill(ds);
            miconexion.Close();
            return ds;
        }

        public static DataSet ConsultaUnidadFiltro(Modelos.Status estatus, string wo, string partnumber)
        {
            int i = 0;
            string query = "SELECT TOP 100 UNIDAD.Id_Unidad,UNIDAD.Work_Order as 'Work Order',IsNull(CAT_AREA.Area, 'N/A') as Area, CAT_NUMPARTE.Num_Parte as 'Part Number',UNIDAD.Serial_Number as 'Serial Number', CAT_STATUS.Nombre_status as Estatus,  IsNull(UNIDAD.Falla, 'N/A') as Falla from UNIDAD LEFT OUTER JOIN  CAT_AREA ON UNIDAD.FK_Area = CAT_AREA.Id_Area INNER JOIN CAT_STATUS ON UNIDAD.FK_Status = CAT_STATUS.Id_Status INNER JOIN CAT_NUMPARTE ON UNIDAD.FK_PartNumber = CAT_NUMPARTE.Id_NumParte ";

            if (estatus.Nombre_Status != "All")
            {
                query += " where UNIDAD.FK_Status = '" + estatus.Id_Status + "'";
                i++;
            }

            if (wo != "")
            {
                if (i > 0)
                {
                    query += " and  Work_order = '" + wo + "'";
                }
                else
                {
                    query += "where Work_order = '" + wo + "'";
                }
                i++;
            }

            if (partnumber != "")
            {
                if (i > 0)
                {
                    query += " and Serial_Number = '" + partnumber + "'";
                }
                else
                {
                    query += "where Serial_Number = '" + partnumber + "'";
                }
            }
            miconexion.Open();
            SqlCommand cmd = new SqlCommand(query, miconexion);
            cmd.ExecuteNonQuery();
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adaptador.Fill(ds);
            miconexion.Close();
            return ds;
        }
        #endregion

        #region ConsultasEspecificas
        static int obtenerUltimoID(string tabla)
        {
            string query = "SELECT IDENT_CURRENT('" + tabla + "')";
            SqlCommand command = new SqlCommand(query, miconexion);
            miconexion.Open();
            var _id = command.ExecuteScalar();
            miconexion.Close();
            return Convert.ToInt32(_id);
        }

        public static Modelos.Unidad obtenerUltimaUnidad()
        {
            miconexion.Open();
            SqlCommand cmd = new SqlCommand("SELECT TOP(1) * FROM UNIDAD ORDER BY 1 DESC", miconexion);
            SqlDataReader reader = cmd.ExecuteReader();

            Modelos.Unidad nueva_unidad = new Modelos.Unidad();
            while (reader.Read())
            {
                nueva_unidad.Id_Unidad = (int)reader["Id_Unidad"];
                nueva_unidad.Work_Order = (string)reader["Work_Order"];
                nueva_unidad.FK_Area = (int)reader["FK_Area"];
                nueva_unidad.FK_PartNumber = (int)reader["FK_PartNumber"];
                nueva_unidad.Serial_Number = (string)reader["Serial_Number"];
                try
                {
                    nueva_unidad.Falla = (string)reader["Falla"];
                }
                catch (Exception)
                {
                }
            }
            miconexion.Close();
            return nueva_unidad;
        }

        public static int obtenerTipoUsuario(string tipoUsuario)
        {
            string query = "SELECT Id_TipoUsuario from CAT_TIPOUSUARIO where TipoUsuario= '" + tipoUsuario + "'";
            SqlCommand command = new SqlCommand(query, miconexion);
            miconexion.Open();
            var id_tipousuario = command.ExecuteScalar();
            miconexion.Close();
            return Convert.ToInt32(id_tipousuario);
        }

        public static Modelos.Usuario obtenerUsuarioGuest()
        {
            miconexion.Open();
            SqlCommand cmd = new SqlCommand("select * from USUARIO where Nombre_Empleado = 'Guest'", miconexion);
            SqlDataReader reader = cmd.ExecuteReader();

            Modelos.Usuario nuevo_usuario = new Modelos.Usuario();
            while (reader.Read())
            {

                nuevo_usuario.Id_Usuario = (int)reader["Id_Usuario"];
                nuevo_usuario.Nombre_Empleado = (string)reader["Nombre_Empleado"];
                nuevo_usuario.FK_TipoUsuario = (int)reader["FK_TipoUsuario"];
                nuevo_usuario.Num_Empleado = (string)reader["Num_Empleado"];
            }
            miconexion.Close();
            return nuevo_usuario;
        }
        public static Modelos.Usuario obtenerUsuario(string usuario, string password)
        {
            miconexion.Open();
            SqlCommand cmd = new SqlCommand("select * from USUARIO where username = '" + usuario + "' and password = '" + password + "'", miconexion);
            SqlDataReader reader = cmd.ExecuteReader();

            Modelos.Usuario nuevo_usuario = new Modelos.Usuario();
            while (reader.Read())
            {
                nuevo_usuario.Id_Usuario = (int)reader["Id_Usuario"];
                nuevo_usuario.Nombre_Empleado = (string)reader["Nombre_Empleado"];
                nuevo_usuario.Username = (string)reader["username"];
                nuevo_usuario.Password = (string)reader["password"];
                nuevo_usuario.FK_AreaInt = (int)reader["FK_AreaInt"];
                nuevo_usuario.FK_TipoUsuario = (int)reader["FK_TipoUsuario"];
                nuevo_usuario.Num_Empleado = (string)reader["Num_Empleado"];
            }
            miconexion.Close();
            return nuevo_usuario;
        }
        public static List<Modelos.Defecto> ConsultaDefectosLista(int id_unidad)
        {
            List<Modelos.Defecto> list_bd = new List<Defecto>();
            miconexion.Open();
            SqlCommand cmd = new SqlCommand("SELECT CAT_DEFECTO.Id_Defecto, CAT_DEFECTO.Codigo, CAT_DEFECTO.Descripcion, CAT_DEFECTO.Codigo + '-' + CAT_DEFECTO.Descripcion AS Defecto," +
                " Referencia, IsNull(Part_Number, 'N/A') as \"Part Number\" FROM UNIDAD_DEFECTO INNER JOIN CAT_DEFECTO ON UNIDAD_DEFECTO.FK_Defecto = CAT_DEFECTO.Id_Defecto where FK_Unidad = " + id_unidad, miconexion);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Modelos.Defecto defecto = new Modelos.Defecto();
                defecto.Id_Defecto = (int)reader["Id_Defecto"];
                defecto.Codigo = (string)reader["Codigo"];
                defecto.Descripcion = (string)reader["Descripcion"];
                defecto.defecto = (string)reader["Defecto"];
                defecto.Referencia = (string)reader["Referencia"];
                defecto.Part_Number = (string)reader["Part Number"];
                list_bd.Add(defecto);
                // Globales.defectos_seleccionados.Add(defecto);
            }
            //Globales.defectos_INICIAL = Globales.defectos_seleccionados;
            miconexion.Close();

            return list_bd;
        }


        public static Modelos.Status ConsultaEstatus(string estatus)
        {
            miconexion.Open();
            //if (estatus == "CALIDAD")
            //{
            //    estatus = "CALIDAD or Nombre_Status = SCRAP";
            //}
            SqlCommand cmd = new SqlCommand("SELECT * from CAT_STATUS where Nombre_Status = '" + estatus + "'", miconexion);
            SqlDataReader reader = cmd.ExecuteReader();

            Modelos.Status status = new Modelos.Status();
            while (reader.Read())
            {
                status.Id_Status = (int)reader["Id_Status"];
                status.Nombre_Status = (string)reader["Nombre_Status"];

            }
            miconexion.Close();
            return status;
        }


        #endregion

        #region Bajas
        public static void eliminarUnidad_Defecto(Modelos.Unidad unidad, Modelos.Defecto defecto)
        {
            miconexion.Open();
            SqlCommand cmd = miconexion.CreateCommand();

            cmd.CommandText = "DELETE FROM UNIDAD_DEFECTO WHERE FK_Defecto = " + defecto.Id_Defecto + " AND FK_UNIDAD = " + unidad.Id_Unidad;

            cmd.ExecuteNonQuery();

            miconexion.Close();
        }
        #endregion

        public static void filtrarDefectos(Modelos.Unidad unidad, List<Modelos.Defecto> defectos_seleccionados)
        {
            List<Modelos.Defecto> list_bd = ConsultaDefectosLista(unidad.Id_Unidad);

            foreach (var item in list_bd)
            {
                if (!defectos_seleccionados.Any(x => x.Id_Defecto == item.Id_Defecto
                     && x.Referencia == item.Referencia))
                {
                    eliminarUnidad_Defecto(unidad, item);
                }
            }

            foreach (var item in defectos_seleccionados)
            {
                if (!list_bd.Any(x => x.Id_Defecto == item.Id_Defecto
                     && x.Referencia == item.Referencia))
                {
                    Alta_Unidad_Defecto(item, false, unidad);
                }
            }
        }

    }
}