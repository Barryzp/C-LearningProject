using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2018_10_29_PhotonServerLearning.Model
{
    public class User
    {
        //Virtual为NHibernate所要求的
        public virtual int Id { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual DateTime Registerdate { get; set; }
    }
}
