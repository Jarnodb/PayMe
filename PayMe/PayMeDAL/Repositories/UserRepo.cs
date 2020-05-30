using PayMeDAL.Context;
using PayMeDAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayMeDAL.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext _context;

        public UserRepo()
        {
        }

        public UserRepo(AppDbContext context)
        {
            _context = context;
        }

        public User Create(User model)
        {
            throw new NotImplementedException();
        }

        public User Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            var listUsers = _context.Users;
            return listUsers;
        }

        public User GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public User Update(User model)
        {
            throw new NotImplementedException();
        }
    }
}
