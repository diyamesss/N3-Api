using EntryApi.Database;
using EntryApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EntryApi.Services.Interface
{
    public interface IUserService
    {
        public Task<User> GetUserById(long userId);
        public Task<long> InsertUser(User user);
        public Task<User> UpdateUser(User user);
        public Task<User> DeleteUser(int userId);
    }
}
