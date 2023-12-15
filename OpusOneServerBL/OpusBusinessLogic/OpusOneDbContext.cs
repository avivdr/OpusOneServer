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
        return Posts.Include(x => x.Creator).Include(x => x.Work).Include(x => x.Comments).Include(x => x.Composer);
    }

    public Post LoadPost(Post post)
    {
        var creator = Users.FirstOrDefault(x => x.Id == post.Creator.Id);
        if (creator != null)
            post.Creator = creator;

        var composer = Composers.FirstOrDefault(x => x.Id == post.Composer.Id);
        if (composer != null)
            post.Creator = composer;
    }

    public async Task SaveComposer(Composer composer)
    {
        if (composer == null) return;
        if (Composers.Any(x => x.Id == composer.Id)) return;

        Composers.Add(composer);
        await SaveChangesAsync();
    }

    public async Task SaveWork(Work work)
    {
        if (work == null) return;
        if (Works.Any(x => x.Id == work.Id)) return;

        await SaveComposer(work.Composer);

        Works.Add(work);
        await SaveChangesAsync();
    }
}
