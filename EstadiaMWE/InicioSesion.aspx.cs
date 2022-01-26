using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EstadiaMWE
{
    public partial class InicioSesion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Abandon();
                //Session["Nombre_Empleado"] = Globales.usuario_actual;
                lblError.Visible = false;
                txtUsuario.Text = "";
                txtPassword.Text = "";
            }
        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            if (Conexion.validarUsuario(txtUsuario.Text, txtPassword.Text))
            {
                lblError.Visible = false;
                //Globales.usuario_actual = Conexion.obtenerUsuario(txtUsuario.Text, txtPassword.Text);
                //Session["Nombre_Empleado"] = Globales.usuario_actual.FK_TipoUsuario;

                Modelos.Usuario usuario_actual = Conexion.obtenerUsuario(txtUsuario.Text, txtPassword.Text);
                Session["Id_Usuario"] = usuario_actual.Id_Usuario;
                Session["Nombre_Empleado"] = usuario_actual.Nombre_Empleado;
                Session["Num_Empleado"] = usuario_actual.Num_Empleado;
                Session["FK_TipoUsuario"] = usuario_actual.FK_TipoUsuario;

                Response.Redirect("Menu.aspx");
            }
            else
            {
                lblError.Visible = true;
                txtUsuario.Text = "";
                txtPassword.Text = "";
            }

        }
    }
}