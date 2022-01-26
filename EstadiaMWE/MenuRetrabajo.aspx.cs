using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EstadiaMWE
{
    public partial class MenuRetrabajo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Id_Usuario"] == null)
            {
                Response.Redirect("MenuPrincipal.aspx");
            }
            else
            {
                lblUsuario.Text = Session["Nombre_Empleado"].ToString();
            }
        }

        protected void btnRetrabajo_Click(object sender, EventArgs e)
        {
            Response.Redirect("Retrabajo.aspx");
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Mod_Retrabajo.aspx");
        }

        protected void btnAlta_Click(object sender, EventArgs e)
        {
            Response.Redirect("AltaUnidad.aspx");
        }
    }
}