using DatabasePractice;
using Microsoft.EntityFrameworkCore;

public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=blogging.db");
    }

    public void DisplayBlogPosts(int blogId, string header)
    {
        Console.WriteLine($"=== {header} ===");

        var blog = Blogs
            .Include(b => b.Posts)
            .FirstOrDefault(b => b.BlogId == blogId);

        if (blog != null && blog.Posts.Any())
        {
            foreach (var post in blog.Posts)
            {
                Console.WriteLine($"[Post ID: {post.PostId}] - Title: \"{post.Title}\"");
                Console.WriteLine($"  Content: {post.Content}");
            }
        }
        else
        {
            Console.WriteLine("No posts found.");
        }
    }
}
