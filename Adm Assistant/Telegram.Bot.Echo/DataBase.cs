using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Configuration;

namespace Telegram.Bot.Echo
{
    public class DataBase
    {
        public static OleDbConnection conn;
       

        public DataBase()
        {
             conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["QuestionDB"].ToString());
        }

        public string GetAnswer(string question)
        {
            OleDbCommand cmd = new OleDbCommand();
            string str = "";
            cmd.CommandText = "SELECT Resposta FROM Questoes WHERE Questao = @question";
            cmd.Parameters.AddWithValue("@question", question);
            cmd.Connection = conn;
            OleDbDataReader reader = null;
            try
            {
                conn.Open();

                
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            str += reader.GetString(0);
                        }
                    }
                    reader = null;
                cmd.Dispose();                                
                conn.Close();
                
            }
            catch(Exception ex)
            {
                str = ex.Message;
            }
            return str;
        }
        
    }
}
