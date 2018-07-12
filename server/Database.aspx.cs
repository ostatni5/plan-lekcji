using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Security;
using System.Data;
using System.Text;

public partial class server_Database : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connstr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database.mdf;Integrated Security = True; Connect Timeout = 30";
        SqlConnection conn = new SqlConnection(connstr);
        string nazwaTabela = "godziny";
        string haslo = "admin";
        string zaszyfrowane = FormsAuthentication.HashPasswordForStoringInConfigFile(haslo, "SHA1");
        string sql;
        /*conn.Open();
      string sql = "SELECT  login ";
       sql += "FROM zalogowany ";
       SqlDataAdapter da3 = new SqlDataAdapter(sql, conn);
       DataTable dt3 = new DataTable();
       da3.Fill(dt3);
        conn.Close();
        string login = dt3.Rows[0][0].ToString();*/
        //if (login=="admin")
        //{
            switch (Request["action"])
            {
                case "add_tabela":
                    {
                        try
                        {
                            conn.Open();
                            //bez autoinkrementacji - ta wersja jest na dziś
                            sql = "CREATE TABLE godziny (id INTEGER IDENTITY(1,1) PRIMARY KEY, odG VARCHAR(10), odM VARCHAR(20), doG VARCHAR(20), doM VARCHAR(20))";
                            //z autoinkrementacją co 1
                            //string sql = "CREATE TABLE nazwaTabeli (id INTEGER IDENTITY(1,1), poleA VARCHAR(10), poleB VARCHAR(20))";
                            SqlCommand command = new SqlCommand();
                            command.CommandText = sql;
                            command.Connection = conn;
                            command.ExecuteNonQuery(); // wykonanie
                            sql = "CREATE TABLE lekcje (id INTEGER IDENTITY(1,1) PRIMARY KEY, numer_sali VARCHAR(20), dFK INTEGER, gFK INTEGER, pFK INTEGER, uFK INTEGER)";
                            command = new SqlCommand();
                            command.CommandText = sql;
                            command.Connection = conn;
                            command.ExecuteNonQuery();
                            sql = "CREATE TABLE przedmioty (id INTEGER IDENTITY(1,1) PRIMARY KEY, nazwa_krotka_p VARCHAR(20), dluga_nazwa_p VARCHAR(50))";
                            command = new SqlCommand();
                            command.CommandText = sql;
                            command.Connection = conn;
                            command.ExecuteNonQuery();
                            sql = "CREATE TABLE dni (id INTEGER IDENTITY(1,1) PRIMARY KEY, nazwa_krotka_d VARCHAR(10), dluga_nazwa_d VARCHAR(30))";
                            command = new SqlCommand();
                            command.CommandText = sql;
                            command.Connection = conn;
                            command.ExecuteNonQuery();
                            sql = "CREATE TABLE users (id INTEGER IDENTITY(1,1) PRIMARY KEY, login_user VARCHAR(20), password_user VARCHAR(200))";
                            command = new SqlCommand();
                            command.CommandText = sql;
                            command.Connection = conn;
                            command.ExecuteNonQuery();
                            sql = "CREATE TABLE zalogowany (id INTEGER IDENTITY(1,1) PRIMARY KEY, login VARCHAR(20))";
                            command = new SqlCommand();
                            command.CommandText = sql;
                            command.Connection = conn;
                            command.ExecuteNonQuery();
                            conn.Close();

                            Response.Write("add_tabela");
                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.Message);
                        }
                    }
                    break;
                case "del_tabela":
                    {
                        try
                        {
                            conn.Open();
                            sql = "DROP TABLE " + nazwaTabela + ";";
                            sql += "DROP TABLE users;";
                            sql += "DROP TABLE dni;";
                            sql += "DROP TABLE przedmioty;";
                            sql += "DROP TABLE lekcje;";
                            //sql += "DROP TABLE zalogowany;";
                            SqlCommand command = new SqlCommand();
                            command.CommandText = sql;
                            command.Connection = conn;
                            command.ExecuteNonQuery(); // wykonanie
                            conn.Close();
                            Response.Write("del_tabela");
                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.Message);
                        }
                    }
                    break;
                case "add_dane":
                    {
                        try
                        {
                            conn.Open();                            
                            SqlCommand command = new SqlCommand();
                            for (var i = 1; i <= 14; i++)
                            {
                                sql = "INSERT INTO " + nazwaTabela + " (odG , odM, doG , doM ) VALUES('00','00','00','00')";
                                command.CommandText = sql;
                                command.Connection = conn;
                                command.ExecuteNonQuery(); // wykonanie
                            }

                            sql = "INSERT INTO przedmioty ( nazwa_krotka_p , dluga_nazwa_p) VALUES('PL','Polski');";
                            sql += "INSERT INTO przedmioty ( nazwa_krotka_p , dluga_nazwa_p) VALUES('MAT','Matematyka');";
                            sql += "INSERT INTO przedmioty ( nazwa_krotka_p , dluga_nazwa_p) VALUES('ANG','Angielski');";
                            sql += "INSERT INTO przedmioty ( nazwa_krotka_p , dluga_nazwa_p) VALUES('NIEM','Niemiecki');";
                            sql += "INSERT INTO przedmioty ( nazwa_krotka_p , dluga_nazwa_p) VALUES('WF','Wychowanie fizyczne');";
                            sql += "INSERT INTO przedmioty ( nazwa_krotka_p , dluga_nazwa_p) VALUES('GW','Godzina wychowawcza');";
                            sql += "INSERT INTO przedmioty ( nazwa_krotka_p , dluga_nazwa_p) VALUES('SIECI.K','Sieci komputerowe');";
                            sql += "INSERT INTO przedmioty ( nazwa_krotka_p , dluga_nazwa_p) VALUES('APLK','Aplikacje klienckie');";
                            sql += "INSERT INTO przedmioty ( nazwa_krotka_p , dluga_nazwa_p) VALUES('REL','Religia');";
                            sql += "INSERT INTO przedmioty ( nazwa_krotka_p , dluga_nazwa_p) VALUES('HIS','Historia');";
                            sql += "INSERT INTO przedmioty ( nazwa_krotka_p , dluga_nazwa_p) VALUES('SIECI.S','Seciowe systemy');";
                            sql += "INSERT INTO przedmioty ( nazwa_krotka_p , dluga_nazwa_p) VALUES('INF','Informatyka');";

                            //sql += "INSERT INTO dni ( nazwa_krotka_d , dluga_nazwa_d) VALUES('NI','Niedziela');";
                            sql += "INSERT INTO dni ( nazwa_krotka_d , dluga_nazwa_d) VALUES('PN','Poniedziałek');";
                            sql += "INSERT INTO dni ( nazwa_krotka_d , dluga_nazwa_d) VALUES('WT','Wtorek');";
                            sql += "INSERT INTO dni ( nazwa_krotka_d , dluga_nazwa_d) VALUES('ŚR','Środa');";
                            sql += "INSERT INTO dni ( nazwa_krotka_d , dluga_nazwa_d) VALUES('CZ','Czwartek');";
                            sql += "INSERT INTO dni ( nazwa_krotka_d , dluga_nazwa_d) VALUES('PT','Piątek');";
                            //sql += "INSERT INTO dni ( nazwa_krotka_d , dluga_nazwa_d) VALUES('SB','Sobota');";

                            sql += "INSERT INTO users ( login_user , password_user) VALUES('admin','" + zaszyfrowane + "');";
                            for (var i = 0; i < 70; i++)
                            {
                                int dzien = i / 14;
                                int lekcja = (i % 12 + 1);
                                int godzina = i % 14 + 1;
                                //lekcja = dzien + 1;
                                sql += "INSERT INTO lekcje VALUES(222," + (dzien + 1) + "," + godzina + "," + lekcja + ",1);";

                            }
                            command.CommandText = sql;
                            command.Connection = conn;
                            command.ExecuteNonQuery();
                            conn.Close();
                            Response.Write("add_dane");
                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.Message);
                        }
                    }
                    break;
                case "del_dane":
                    {
                        try
                        {
                            conn.Open();
                            sql = "DELETE FROM " + nazwaTabela + ";";
                            sql += "DELETE FROM users;";
                            sql += "DELETE FROM dni;";
                            sql += "DELETE FROM przedmioty;";
                            sql += "DELETE FROM lekcje;";
                            SqlCommand command = new SqlCommand();
                            command.CommandText = sql;
                            command.Connection = conn;
                            command.ExecuteNonQuery(); // wykonanie
                            conn.Close();
                            Response.Write("del_dane");
                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.Message);
                        }
                    }
                    break;
                case "send_dane":
                    {
                        try
                        {
                            conn.Open();
                            sql = "UPDATE " + nazwaTabela + " SET " + Request["typ"] + "G = " + Request["godzina"] + ", " + Request["typ"] + "M  = " + Request["minuta"] + " WHERE id = " + Request["id"] + "";
                            SqlCommand command = new SqlCommand();
                            command.CommandText = sql;
                            command.Connection = conn;
                            command.ExecuteNonQuery();
                            conn.Close();
                            Response.Write("Zaktualizowano Dane");
                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.Message);
                        }
                    }
                    break;
            }
       // }
        //else {
        //    Response.Write("Brak uprawnień");
        //}
    }
}