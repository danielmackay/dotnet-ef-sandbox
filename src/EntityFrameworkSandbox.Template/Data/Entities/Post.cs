using System.Text;

namespace EntityFrameworkSandbox.Template.Data.Entities;

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public int BlogId { get; set; }
    public Blog Blog { get; set; }
    public ICollection<Tag> Tags { get; set; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"PostId:{PostId}");
        sb.AppendLine($"BlogId:{BlogId}");
        sb.AppendLine($"Title:{Title}");
        sb.AppendLine($"Content:{Content}");
        return sb.ToString();
    }
}
