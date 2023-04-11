using System.Collections.Generic;
using MyApp.Models;
using MyApp.Services.Domain.Interfaces.Base;

namespace MyApp.Services.Domain.Interfaces
{
    public interface IUserService : IServiceBase<User>
    {
        /// <summary>
        /// Return users by active state
        /// </summary>
        /// <param name="isActive"></param>
        /// <returns></returns>
        IEnumerable<User> FilterByActive();
        IEnumerable<User> FilterByInActive();
        User CreateUser(User user);
    }
}