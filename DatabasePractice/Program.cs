using DatabasePractice;

using var db = new BloggingContext();
db.Database.EnsureDeleted();
db.Database.EnsureCreated();

Console.WriteLine("--- 1. CREATE: Creating a Blog with Posts ---");
var newBlog = new Blog
{
    Url = "https://mytechblog.com",
    Posts =
    [
        new Post { Title = "First Post", Content = "Welcome to my blog!" },
        new Post { Title = "Second Post", Content = "Here is some content about stuff." },
        new Post { Title = "Third Post", Content = "This post will be deleted later." }
    ]
};

db.Blogs.Add(newBlog);
db.SaveChanges();
Console.WriteLine($"Created Blog ID {newBlog.BlogId} with {newBlog.Posts.Count} posts.\n");

db.DisplayBlogPosts(newBlog.BlogId, "Posts after creation");

Console.WriteLine("\n--- 2. UPDATE & DELETE ---");
var posts = db.Posts.Where(p => p.BlogId == newBlog.BlogId).ToList();

if (posts.Count >= 2)
{
    var postToEdit = posts[0];
    postToEdit.Title = "Updated First Post Title";
    postToEdit.Content = "This content has been successfully updated.";
    Console.WriteLine($"Updated Post ID {postToEdit.PostId}.");

    var postToDelete = posts.Last();
    db.Posts.Remove(postToDelete);
    Console.WriteLine($"Deleted Post ID {postToDelete.PostId}.");

    db.SaveChanges();
}

Console.WriteLine("\n--- 3. READ: Final list of blog posts ---");
db.DisplayBlogPosts(newBlog.BlogId, "Posts after Update and Delete");