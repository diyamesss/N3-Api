using EntryApi.Database;
using EntryApi.Models;
using EntryApi.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntryApi.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly DbEntryContext _dbEntryContext;
        public UserService(DbEntryContext dbEntryContext)
        {
            _dbEntryContext = dbEntryContext;
        }

        public async Task<User> GetUserById(long userId)
        {
            return await _dbEntryContext.Users.Where(u => u.UserId == userId).SingleOrDefaultAsync();
        }

        public async Task<long> InsertUser(User user)
        {
            _dbEntryContext.Users.Add(user);
            await _dbEntryContext.SaveChangesAsync();
            return user.UserId;
        }

        public async Task<User> UpdateUser(User user)
        {
            _dbEntryContext.Users.Update(user);
            await _dbEntryContext.SaveChangesAsync();
            return user;
        }

        public Task<User> DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
