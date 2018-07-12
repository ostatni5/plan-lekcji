using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public partial class server_Database : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connstr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database.mdf;Integrated Security = True; Connect Timeout = 30";
        SqlConnection conn = new SqlConnection(connstr);
        
                
        try
        {   
            conn.Open();
            string sql = "SELECT przedmioty.nazwa_krotka_p, lekcje.numer_sali ";
            sql += "FROM lekcje ";
            sql += "LEFT JOIN przedmioty ON (lekcje.pFK = przedmioty.id) ";
            sql += "LEFT JOIN users ON(lekcje.uFK = users.id) ";
            sql += "WHERE lekcje.uFK = 1  ";
            
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();            
            StringBuilder sb = new StringBuilder();                        

            sb.Append("{\"tydzien\":");            
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
        catch(Exception ex)
        {
            Response.Write(ex.Message);
        }
               
    }
}