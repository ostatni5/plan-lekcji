using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public partial class server_Json : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connstr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database.mdf;Integrated Security = True; Connect Timeout = 30";
        SqlConnection conn = new SqlConnection(connstr);
        string nazwaTabela = "godziny";
        string user;
        try
        {

            conn.Open();
            string sql = "SELECT  login ";
            sql += "FROM zalogowany ";
            SqlDataAdapter da3 = new SqlDataAdapter(sql, conn);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);
            string login = dt3.Rows[0][0].ToString();
            sql = "SELECT  login_user,password_user,id ";
            sql += "FROM users ";
            sql += "WHERE login_user = \'" + login + "\'";
            SqlDataAdapter da2 = new SqlDataAdapter(sql, conn);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            user = dt2.Rows[0][2].ToString();
            sql = "SELECT * FROM " + nazwaTabela;
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            /*Response.Write("<br />ilość wierszy w tabeli: " + dt.Rows.Count);
            Response.Write("<br />ilość kolumn w tabeli: " + dt.Columns.Count);
            Response.Write("<br />nazwa 1 kolumny: " + dt.Columns[0].ColumnName);
            Response.Write("<br />wartość w 1 komórce 1 wiersza: " + dt.Rows[0][0]+"< br />");*/
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"" + nazwaTabela + "\":");
            //sb.Append("\""); // cudzysłów w napisie
            //sb.AppendLine(); // nowa linia
            //sb.Append("\t"); // tabulator
            sb.AppendLine();
            sb.Append("\t");
            sb.Append("[");
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                sb.AppendLine();
                //sb.Append("\t\t{\""+ dt.Columns[0].ColumnName+"\":\"" + dt.Rows[i][0] + "\",\"" + dt.Columns[1].ColumnName +"\":\"00\",\"" + dt.Columns[2].ColumnName +"\":\"00\",\"" + dt.Columns[3].ColumnName +"\":\"00\",\"" + dt.Columns[4].ColumnName +"\":\"00\"}"); 
                sb.Append("\t\t{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    sb.Append("\"" + dt.Columns[j].ColumnName + "\":\"" + dt.Rows[i][j] + "\"");
                    if (j < dt.Columns.Count - 1)
                        sb.Append(",");
                }
                sb.Append("\t\t}");
                if (i < dt.Rows.Count - 1)
                    sb.Append(",");
            }
            sb.AppendLine();
            sb.Append("\t");
            sb.Append("]");
            sb.AppendLine();

            //sb.Append("}");

            Response.Write(sb.ToString()); // wypisanie na stronie aspc zawartości StringBuilder-a

            conn.Open();
            sql = "SELECT przedmioty.nazwa_krotka_p, lekcje.numer_sali ";
            sql += "FROM lekcje ";
            sql += "LEFT JOIN przedmioty ON (lekcje.pFK = przedmioty.id) ";
            sql += "LEFT JOIN users ON(lekcje.uFK = users.id) ";
            sql += "WHERE lekcje.uFK = " + user;
            da = new SqlDataAdapter(sql, conn);
            dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            sb = new StringBuilder();

            sb.Append(",\"tydzien\":");
            sb.AppendLine();
            sb.Append("\t");
            sb.Append("[");
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                sb.AppendLine();
                sb.Append("\t\t{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    sb.Append("\"" + dt.Columns[j].ColumnName + "\":\"" + dt.Rows[i][j] + "\"");
                    if (j < dt.Columns.Count - 1)
                        sb.Append(",");
                }
                sb.Append("\t\t}");
                if (i < dt.Rows.Count - 1)
                    sb.Append(",");
            }
            sb.AppendLine();
            sb.Append("\t");
            sb.Append("]");
            sb.AppendLine();

            //sb.Append("}");

            Response.Write(sb.ToString());


            int dzien = 1;
            dzien = Int32.Parse(Request["dzien"]);
            conn.Open();
            sql = "SELECT przedmioty.nazwa_krotka_p, przedmioty.dluga_nazwa_p, lekcje.numer_sali ";
            sql += "FROM lekcje ";
            sql += "LEFT JOIN dni ON (lekcje.dFK = dni.id) ";
            sql += "LEFT JOIN przedmioty ON(lekcje.pFK = przedmioty.id) ";
            sql += "WHERE(lekcje.uFK =" + user + " AND  lekcje.dFK = " + dzien + ") ";
            da = new SqlDataAdapter(sql, conn);
            dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            sb = new StringBuilder();

            sb.Append(",\"dzien\":");
            sb.AppendLine();
            sb.Append("\t");
            sb.Append("[");
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                sb.AppendLine();
                sb.Append("\t\t{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    sb.Append("\"" + dt.Columns[j].ColumnName + "\":\"" + dt.Rows[i][j] + "\"");
                    if (j < dt.Columns.Count - 1)
                        sb.Append(",");
                }
                sb.Append("\t\t}");
                if (i < dt.Rows.Count - 1)
                    sb.Append(",");
            }
            sb.AppendLine();
            sb.Append("\t");
            sb.Append("]");
            sb.AppendLine();

            sb.Append("}");

            Response.Write(sb.ToString());

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        if (Request["action"] == "get_dane")
        {
            
        }
        /*try
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"godziny\":");
            //sb.Append("\""); // cudzysłów w napisie
            //sb.AppendLine(); // nowa linia
            //sb.Append("\t"); // tabulator
            sb.AppendLine();
            sb.Append("\t");
            sb.Append("[");
            for(var i = 1; i <11;i++)
            {
                sb.AppendLine();
                sb.Append("\t\t{\"id\":\"");
                sb.Append(i);
                sb.Append("\",\"odG\":\"00\",\"odM\":\"00\",\"doG\":\"00\",\"doM\":\"00\"}");
                if(i<10)
                    sb.Append(",");
            }
            sb.AppendLine();
            sb.Append("\t");
            sb.Append("]");
            sb.AppendLine();
            
            sb.Append("}");

            Response.Write(sb.ToString()); // wypisanie na stronie aspc zawartości StringBuilder-a
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }*/
    }
}