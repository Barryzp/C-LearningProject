using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGameServer.Model;
using MyGameServer.Manager;
using NHibernate;
using NHibernate.Criterion;

namespace MyGameServer.Manager
{
    public class UserManager : IUserManager
    {

        public void Add(User user)
        {
            using (ISession session = NHibernateHelper.Session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(user);
                    transaction.Commit();
                }
            }
        }

        public void Delete(User user)
        {
            using (ISession session = NHibernateHelper.Session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(user);
                    transaction.Commit();
                }
            }
        }

        public ICollection<User> GetAllUsers()
        {
            using (ISession session = NHibernateHelper.Session)
            {
                ICriteria criteria = session.CreateCriteria(typeof(User));
                IList<User> users = criteria.List<User>();
                return users;
            }
        }

        public User GetUserByID(int id)
        {
            using (ISession session = NHibernateHelper.Session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    User user = session.Get<User>(id);
                    transaction.Commit();
                    return user;
                }
            }
        }

        public User GetUserByName(string name)
        {
            using (ISession session = NHibernateHelper.Session)
            {
                ICriteria criteria = session.CreateCriteria(typeof(User));
                //这里的Username是User类的字段中的Username
                criteria.Add(Restrictions.Eq("Username", name));
                User user = criteria.UniqueResult<User>();
                return user;
            }
        }

        public void Update(User user)
        {
            using (ISession session = NHibernateHelper.Session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(user);
                    transaction.Commit();
                }
            }
        }

        public bool VerifyUserAccount(string username, string password)
        {
            using (ISession session = NHibernateHelper.Session)
            {
                ICriteria criteria = session.CreateCriteria(typeof(User));
                //这里的Username是User类的字段中的Username
                criteria.Add(Restrictions.Eq("Username", username));
                criteria.Add(Restrictions.Eq("Password", password));
                User user = criteria.UniqueResult<User>();

                if(user==null)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
