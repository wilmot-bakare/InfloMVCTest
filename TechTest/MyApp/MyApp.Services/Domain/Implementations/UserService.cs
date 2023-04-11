using System;
using System.Collections.Generic;
using System.Linq;
using MyApp.Data;
using MyApp.Models;
using MyApp.Services.Domain.Implementations.Base;
using MyApp.Services.Domain.Interfaces;

namespace MyApp.Services.Domain.Implementations
{
    public class UserService : ServiceBase<User>, IUserService
    {

        protected IUserRepository _userRepository;
        protected IDataAccess DataAccess;
        public UserService(IDataAccess dataAccess, IUserRepository userRepository) : base(dataAccess) {
            this.DataAccess = dataAccess;
            _userRepository = userRepository;

        }

        /// <summary>
        /// Return users by active state
        /// </summary>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public IEnumerable<User> FilterByActive()
        {
            return _userRepository.FilterByActive();
        }

        public IEnumerable<User> FilterByInActive()
        {
            return _userRepository.FilterByInActive();
        }

        public User CreateUser(User user)
        {
            return DataAccess.Create(user);
        }



    }
}
