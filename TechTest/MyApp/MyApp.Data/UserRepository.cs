using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Data
{
    public class UserRepository : IUserRepository
    {

        private readonly IDataAccess dataAccess;

        public UserRepository(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public IEnumerable<User> FilterByActive()
        {
            return this.dataAccess.GetAll<User>().Where(u => u.IsActive == true);
        }

        public IEnumerable<User> FilterByInActive()
        {
            return this.dataAccess.GetAll<User>().Where(u => u.IsActive == false);
        }

        public async Task<Activity> AddActivity(Activity activity)
        {
           var res =  await dataAccess.Create(activity);
            return res;

        }

       
    }
}
