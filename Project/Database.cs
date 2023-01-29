using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public static class Database
    {
        public const string connetionString = @"Server=localhost;Database=SongsDB;Trusted_Connection=True";

        public static string formatStr(string str)
        {
            string res = "";
            foreach (char c in str)
            {
                if(c == '\'')
                {
                    res += "''";
                }
                else
                {
                    res += c;
                }
            }
            return res;
        }
        public static void RunOneWayQuery(string query)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(Database.connetionString);
            cnn.Open();

            SqlCommand command;
            command = new SqlCommand(query, cnn);
            command.ExecuteNonQuery();
            cnn.Close();
        }

        public static void InsertSong(string id, string title, string author, string lyrics, string cover)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(Database.connetionString);
            cnn.Open();

            SqlCommand command;
            command = new SqlCommand("InsertSong", cnn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            
            command.Parameters.Add("@SID", SqlDbType.VarChar).Value = id;
            command.Parameters.Add("@STitle", SqlDbType.VarChar).Value = title;
            command.Parameters.Add("@SAuthor", SqlDbType.VarChar).Value = author;
            command.Parameters.Add("@SLyrics", SqlDbType.VarChar).Value = lyrics;
            command.Parameters.Add("@SCover", SqlDbType.VarChar).Value = cover;

            command.ExecuteNonQuery();
            cnn.Close();
        }

        public static void RunOneWayProcedure(string procedure, string strParam = null)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(Database.connetionString);
            cnn.Open();

            SqlCommand command;
            command = new SqlCommand(procedure, cnn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            if(strParam != null)command.Parameters.Add("@Param1", SqlDbType.VarChar).Value = strParam;

            command.ExecuteNonQuery ();
            cnn.Close();
        }

        public static ObservableCollection<T> RunProcedure<T>(string procedure)
        {
            ObservableCollection<T> collection = null;
            SqlConnection cnn;
            cnn = new SqlConnection(Database.connetionString);
            cnn.Open();

            SqlCommand command;
            command = new SqlCommand(procedure, cnn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            collection = ConvertDataTable<T>(dataTable);

            cnn.Close();

            return collection;
        }

        private static ObservableCollection<T> ConvertDataTable<T>(DataTable dt)
        {
            ObservableCollection<T> data = new ObservableCollection<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            } 
            return obj;
        }
    }
}
