using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGameServer.Model;
using MyGameServer.Manager;

namespace MyGameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            RestructiveCode();
        }

        static void RestructiveCode()
        {
            User user = new User { Id = 23, Username = "NastyBarry", Password = "Nb456", Registerdate = new DateTime(2011, 10, 12) };
            IUserManager userManager = new UserManager();

            #region 增删改
            //userManager.Add(user);
            //userManager.Delete(user);
            //userManager.Update(user);
            #endregion
            #region   测试得到指定条件的数据
            //User user1 = userManager.GetUserByID(23);
            //User user1 = userManager.GetUserByName("Slash");
            //Console.WriteLine("User's password is: " + user1.Password);
            #endregion
            #region 测试获取所有的信息
            /*           
            ICollection<User> users = userManager.GetAllUsers();
            foreach(var item in users)
            {
                Console.WriteLine(item.Username+" "+item.Password);
            }*/
            #endregion
            #region 验证账号功能实现
            Console.WriteLine(userManager.VerifyUserAccount(user.Username,user.Password));
            #endregion

        }

        static void NHibernateNative()
        {
            NHibernateLearning nHibernateLearning = new NHibernateLearning();
            List<User> users = new List<User>();

            User user1 = new User() { Id = 1233, Username = "NastyBarry1", Password = "Nb123", Registerdate = new DateTime(2011, 10, 12) };
            users.Add(user1);
            User user2 = new User() { Id = 1236, Username = "NastyBarry2", Password = "Nb123", Registerdate = new DateTime(2011, 10, 12) };
            users.Add(user2);
            nHibernateLearning.InsertToDB(users);
        }

        static void NormalSqlProgram()
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
