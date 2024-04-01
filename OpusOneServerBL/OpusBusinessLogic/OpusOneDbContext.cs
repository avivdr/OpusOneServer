using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpusOneServerBL.Models;

public partial class OpusOneDbContext
{
    public IEnumerable<User> GetUsersWithData()
    {
        return Users.Include(x => x.Posts).Include(x => x.Comments).Include(x => x.ForumComments).Include(x => x.Forums).Include(x => x.WorksUsers);
    }

    public IEnumerable<Post> GetPostsWithData()
    {
        return Posts.Include(x => x.Creator).Include(x => x.Work).Include(x => x.Comments).ThenInclude(x=> x.Creator).Include(x => x.Composer);
    }

    public void AttachPostData(Post p)
    {
        if (p == null)
            return;

        if (p.Work != null && Works.Any(x => x.Id == p.Work.Id))
            Works.Attach(p.Work);        

        if (p.Work != null && p.Work.Composer != null && Composers.Any(x => x.Id == p.Work.Composer.Id))
            Composers.Attach(p.Work.Composer);

        if (p.Composer != null && Composers.Any(x => x.Id == p.Composer.Id))
            Composers.Attach(p.Composer);

        Users.Attach(p.Creator);
    }
}
