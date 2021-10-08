using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace waAgenda
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void bt_logar_Click(object sender, EventArgs e)
        {
            String email = txbEmail.Text;
            String senha = txbSenha.Text;

            System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot");
            System.Configuration.ConnectionStringSettings connString;
            connString = rootWebConfig.ConnectionStrings.ConnectionStrings["ConnectionString"];

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connString.ToString();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select * from usuario where email = @email and senha = @senha";
            cmd.Parameters.AddWithValue("email", email);
            cmd.Parameters.AddWithValue("senha", senha);
            con.Open();
            SqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                HttpCookie login = new HttpCookie("login", txbEmail.Text);
                Response.Cookies.Add(login);
                //direcionar a pagina
                Response.Redirect("~/index.aspx");
            }
            else
            {
                Response.Write("<script> alert('E-mail e/ou senha incorreto/s!') </script>");
                // lMsg.Text = "E-mail e/ou senha incorreto/s!";
            }
            con.Close();
        }
    }
}