using Microsoft.EntityFrameworkCore;
using ModelsLibrary;
using Server.Context;

namespace Server.DbContextsShop;
public class DbUser
{
    private BookShopDbContext _dbContext;
    public DbUser(DbContextOptions options)
    {
        _dbContext = new BookShopDbContext((DbContextOptions<BookShopDbContext>)options);
    }

    ~DbUser()=>
        _dbContext.Dispose();

    public List<User> GetAllUsers()=>
        _dbContext.Users.ToList();
    
    public List<User> GetIsActiveUsers()=>
         _dbContext.Users
        .Where(u => u.IsActive == true)
        .ToList();

    public List<User> GetFiveUsers()=> 
        _dbContext.Users
        .ToList();

    public void AddNewUser(User user)
    {
        var isChek = _dbContext.Users.FirstOrDefault(u => u.Id == user.Id);
        if (isChek != null)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
    }
    public void RemoveUser(User user)
    {
        var item = _dbContext.Users.Find(user.Id);
        if (item != null)
        {
            item.IsActive = false;
            //_dbContext.Users.Remove(item);
            _dbContext.SaveChanges();
        }
    }
    public void EditUsers(int id, User user)
    {
        var us = _dbContext.Users.Find(id);
        if (us != null)
        {
            us = user;
            _dbContext.SaveChanges();
        }
    }
}
