using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Data
{
    public interface IUserRepository
    {

        // Retrieves active or inactive users based on the isActive parameter.
        IEnumerable<User> FilterByActive();
        IEnumerable<User> FilterByInActive();
        Task<Activity> AddActivity(Activity activity);
    }
}
