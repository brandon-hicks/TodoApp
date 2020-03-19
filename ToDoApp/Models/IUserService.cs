using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Models
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
    }

    public class UserService : IUserService
    {
        private List<User> _users = new List<User>
        {
            new User{UserId = 1, Name = "test", Username = "Test", Password = "test123"}
        };

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await Task.Run(() =>
                _users.SingleOrDefault(b => b.Username == username && b.Password == password));

            if (user == null)
                return null;

            return user.WithoutPassword();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await Task.Run(()=> _users.WithoutPasswords());
        }
    }
}