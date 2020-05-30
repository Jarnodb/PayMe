using PayMeDAL.Models;
using System.Collections.Generic;

namespace PayMeDAL.Repositories
{
    public interface IUserRepo
    {
        User GetById(int Id);

        IEnumerable<ApplicationUser> GetAll();

        User Create(User model);

        User Update(User model);

        User Delete(int id);
    }
}
