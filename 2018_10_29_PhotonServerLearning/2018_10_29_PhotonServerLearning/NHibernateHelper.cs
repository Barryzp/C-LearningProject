using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;


namespace _2018_10_29_PhotonServerLearning
{
    public class NHibernateHelper
    {
        private static ISessionFactory sessionFactory;

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                {
                    Configuration configuration = new Configuration();
                    configuration.Configure();
                    configuration.AddAssembly("2018_10_29_PhotonServerLearning");
                    sessionFactory = configuration.BuildSessionFactory();
                }
                return sessionFactory;
            }
        }

        public static ISession Session
        {
            get
            {
                return SessionFactory.OpenSession();
            }
        }

    }
}
