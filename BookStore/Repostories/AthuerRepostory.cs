using BookStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Repostories
{
    public class AthuerRepostory : IBookStoreRepostory<Auther>
    {
        IList<Auther> authers;
        public AthuerRepostory()
        {
            authers= new List<Auther>()
            {
                new Auther{id=1,FullName="Amir Nady"},
                new Auther{id=2,FullName="Ahmad Hany"},
                new Auther{id=3,FullName="Nasser Sayed"}
            };
        }
        public void Add(Auther entity)
        {
            if (authers.Count == 0)
            {
                entity.id =  1;
                authers.Add(entity);


            }
            else
            {
                entity.id = authers.Max(x => x.id) + 1;
                authers.Add(entity);
            }
         
        }

        public void Delete(int id)
        {
            var auther = Find(id);
            authers.Remove(auther);
        }

        public Auther Find(int id)
        {
          var auther=  authers.SingleOrDefault(a => a.id == id);
            return auther;
        }

        public IList<Auther> GetAll()
        {
            return authers;
        }

        public IList<Auther> Search(string trm)
        {
            throw new System.NotImplementedException();
        }

        public void Update(int id, Auther entity)
        {
            var auther = Find(id);
            if (auther != null)
            {
                auther.FullName = entity.FullName;
            }
        }
    }
}
