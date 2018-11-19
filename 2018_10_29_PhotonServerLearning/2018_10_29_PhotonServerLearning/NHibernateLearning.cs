using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGameServer.Model;
using NHibernate;
using NHibernate.Cfg;

namespace MyGameServer
{
    public class NHibernateLearning
    {
        private Configuration configuration;

        //与数据库进行对话
        private ISessionFactory sessionFactory;
        private ISession session;
        private ITransaction transaction;

        public NHibernateLearning()
        {
            configuration = new Configuration();
            sessionFactory = null;
            session = null;
            transaction = null;

            //解析nhibernate.cfg.xml
            configuration.Configure();

            //解析 映射文件 User.hbm.xml，等等
            configuration.AddAssembly("2018_10_29_PhotonServerLearning");
        }

        //object是一个Model
        public void InsertToDB(List<User> objs)
        {
            try
            {
                sessionFactory = configuration.BuildSessionFactory();
                //打开一个跟数据库有关的会话
                session = sessionFactory.OpenSession();

                //创建事务
                transaction = session.BeginTransaction();
                //插入语句
                foreach (var item in objs)
                {
                    session.Save(item);
                }
                transaction.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if(transaction!=null)
                {
                    transaction.Dispose();
                }
                if (session != null)
                {
                    session.Close();
                }
                if (sessionFactory != null)
                {
                    sessionFactory.Close();
                }
            }


        }

    }
}
