using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Web.Security;

public partial class server_Json : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connstr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database.mdf;Integrated Security = True; Connect Timeout = 30";
        SqlConnection conn = new SqlConnection(connstr);       
        
        if (Request["action"] == "scan")
        {            
            try
            {
                string login = Request["login"];
                login = login.Replace("'", ""); // zamiana pojedynczego apostrofu na pusty string
                login = login.Replace("\"", "");
                login = login.Replace(">", "");
                login = login.Replace("<", "");
                login = login.Replace("javascript", "");
                string haslo = Request["haslo"];
                haslo = haslo.Replace("'", ""); // zamiana pojedynczego apostrofu na pusty string
                haslo = haslo.Replace("\"", "");
                haslo = haslo.Replace(">", "");
                haslo = haslo.Replace("<", "");
                haslo = haslo.Replace("javascript", "");
                string zaszyfrowane = FormsAuthentication.HashPasswordForStoringInConfigFile(haslo, "SHA1");
                conn.Open();
                string sql = "SELECT  login_user,password_user,id ";
                sql += "FROM users ";
                sql += "WHERE login_user = \'"+login+"\'" ;
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                sql = "SELECT  login_user ";
                sql += "FROM users ";
                SqlDataAdapter da2 = new SqlDataAdapter(sql, conn);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                conn.Close();
                /*Response.Write("<br />ilość wierszy w tabeli: " + dt.Rows.Count);
            Response.Write("<br />ilość kolumn w tabeli: " + dt.Columns.Count);
            Response.Write("<br />nazwa 1 kolumny: " + dt.Columns[0].ColumnName);
            Response.Write("<br />wartość w 1 komórce 1 wiersza: " + dt.Rows[0][0]+"< br />");*/
                //Response.Write(dt.Rows[0][2]);
                if (dt.Rows.Count == 1)
                {
                    if (dt.Rows[0][1].ToString() == zaszyfrowane)
                    {
                        Response.Write("login");
                        conn.Open();
                        sql = "DELETE FROM zalogowany; ";
                        sql += "INSERT INTO zalogowany VALUES (\'" + login + "\')";                        
                        SqlCommand command = new SqlCommand();
                        command.CommandText = sql;
                        command.Connection = conn;
                        command.ExecuteNonQuery(); // wykonanie
                        conn.Close();
                    }
                    else
                    {
                        Response.Write("error");
                    }
                }
                else
                {                    
                    conn.Open();
                    sql = "INSERT INTO users VALUES (\'"+login+"\',\'"+ zaszyfrowane + "\')";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    SqlCommand command = new SqlCommand();
                    sql = "";
                    for (var i = 0; i < 70; i++)
                    {
                        int dzien = i / 14;
                        int lekcja = (i % 12 + 1);
                        int godzina = i % 14 + 1;
                        int sala = (dt2.Rows.Count + 2) * 111;
                        //lekcja = dzien + 1;
                        sql += "INSERT INTO lekcje VALUES("+sala+"," + (dzien + 1) + "," + godzina + "," + lekcja + ","+ (dt2.Rows.Count+1) + ");";

                    }
                    command.CommandText = sql;
                    command.Connection = conn;
                    command.ExecuteNonQuery();
                    conn.Close();
                    Response.Write("register");

                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        
    }
}