using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2018_10_29_PhotonServerLearning
{
    class ConnectToMySQL
    {
        private string connectStr;
        private MySqlConnection connection;
        public ConnectToMySQL()
        {
            connectStr = "server=localhost;port=3306;database=mygamedb;user=root;password=Zp123456";
            connection = new MySqlConnection(connectStr);
        }

        public void OpenDB()
        {
            //settle exception,cause we need know the reason why it crashed.
            try
            {
                connection.Open();
                Console.WriteLine("Connect successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Connect fail.\n"+e.ToString());
                connection.Close();
            }
        }

        public void CloseDB()
        {
            connection.Close();
        }

        public void Query(string sql) //how to query data from database;
        {
            MySqlCommand sqlCommand = new MySqlCommand(sql, connection);
            MySqlDataReader reader = sqlCommand.ExecuteReader();
            
            while(reader.Read())//the return value means whether current page exists.(true/false)
            {
                Console.WriteLine(reader[0].ToString() + reader[1] + reader[2]);

                //same function.
                /*Console.WriteLine(reader.GetInt32(0)+" "+reader.GetString(1)+" "+reader.GetString(2));
                Console.WriteLine(reader.GetInt32("id") + " " + reader.GetString("username") + " " + reader.GetString("password"));*/
            }
            reader.Close();
        }

        public object QueryToGetSingleValue(string sql)
        {
            MySqlCommand sqlCommand = new MySqlCommand(sql, connection);
            return sqlCommand.ExecuteScalar();
        }

        public void Insert(string sql)
        {
            MySqlCommand sqlCommand = new MySqlCommand(sql, connection);
            try
            {
                int result = sqlCommand.ExecuteNonQuery();
                Console.WriteLine("insert success.");
            }
            catch (Exception e)
            {
                Console.WriteLine("insert fail.\n" + e.ToString());
                connection.Close();
            }
        }

        public void  Update(string sql)
        {
            MySqlCommand sqlCommand = new MySqlCommand(sql, connection);
            try
            {
                int result = sqlCommand.ExecuteNonQuery();
                Console.WriteLine("insert success.");
            }
            catch (Exception e)
            {
                Console.WriteLine("insert fail.\n" + e.ToString());
                connection.Close();
            }
        }

        public void Drop(string sql)
        {
            MySqlCommand sqlCommand = new MySqlCommand(sql, connection);
            try
            {
                int result = sqlCommand.ExecuteNonQuery();
                Console.WriteLine("insert success.");
            }
            catch (Exception e)
            {
                Console.WriteLine("insert fail.\n" + e.ToString());
                connection.Close();
            }
        }

        public bool VerifyUser(string username,string password)
        {
            //method 1;
            //string sql = "select * from users where username='"+username+"' and password='"+password+"';";
            string sql = "select * from users where username=@param1 and password=@param2;";
            MySqlCommand sqlCommand = new MySqlCommand(sql, connection);
            sqlCommand.Parameters.AddWithValue("param1", username);
            sqlCommand.Parameters.AddWithValue("param2", password);
            MySqlDataReader reader = sqlCommand.ExecuteReader();
            
            if (reader.Read())
            {
                //reader用完了记得关闭，不能够重复一直创建。
                reader.Close();
                return true;
            }

            reader.Close();
            return false;
        }
    }
}
