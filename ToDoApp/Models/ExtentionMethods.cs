using System.Collections.Generic;
using System.Linq;

namespace ToDoApp.Models
{
    public static class ExtentionMethods
    {
        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
        {
            return users.Select(t => t.WithoutPassword());
        }

        public static User WithoutPassword(this User user)
        {
            user.Password = null;

            return user;
        }
    }
}