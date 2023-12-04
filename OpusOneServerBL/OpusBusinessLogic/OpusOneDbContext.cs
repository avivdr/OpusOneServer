using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpusOneServerBL.Models;

public partial class OpusOneDbContext
{
    public IEnumerable<User> GetUserWithData()
    {
        return this.Users.Include(x => x.Posts).Include(x => x.Comments).Include(x => x.ForumComments).Include(x => x.Forums).Include(x => x.WorksUsers);
    }
}
