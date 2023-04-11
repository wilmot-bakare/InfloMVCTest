using MyApp.Data;
using MyApp.Services.Domain.Implementations;
using MyApp.Services.Domain.Interfaces;
using MyApp.Services.Factories.Interfaces;

namespace MyApp.Services.Factories.Implementations
{
    public class ServiceFactory : IServiceFactory
    {
        private IDataAccess _dataAccess;
        //   protected IDataAccess DataAccess => _dataAccess ?? (_dataAccess = new DataAccess(new DataContext()));

        protected IDataAccess DataAccess => _dataAccess ?? (_dataAccess = new DataAccess(new MyAppDbContext()));
        private IUserService _userService;
        protected IUserRepository _userRepository;

        protected IUserRepository userRepository => _userRepository ?? (_userRepository = new UserRepository(DataAccess));
        public IUserService UserService => _userService ?? (_userService = new UserService(DataAccess, userRepository));
    }
}
