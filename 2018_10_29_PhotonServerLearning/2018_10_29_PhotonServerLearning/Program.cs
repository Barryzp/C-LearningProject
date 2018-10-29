using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2018_10_29_PhotonServerLearning
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectToMySQL connect = new ConnectToMySQL();
            connect.OpenDB();

            connect.Query("select * from users;");
            connect.Query("select * from users limit 2;");

            /*var item = connect.QueryToGetSingleValue("select username from users where id=6;");
            Console.WriteLine(item.ToString());*/

            //connect.Insert("insert into users(username,password,registerdate) values('Slash','M123','"+DateTime.Now+"');");
            //connect.Update("update users set username='DICKKKKKKK' where id=6;");
            //connect.Drop("delete from users where id=4;");
            //Console.WriteLine(connect.VerifyUser("barry", "123"));
            //Console.WriteLine(connect.VerifyUser("jkkkkk", "123"));

            connect.CloseDB();
        }
    }
}
