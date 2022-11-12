using System.Text;

namespace EntityFrameworkSandbox.Template.Data.Entities;

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }

    public List<Post> Posts { get; set; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"BlogId:{BlogId}");
        sb.AppendLine($"Url:{Url}");
        return sb.ToString();
    }
}
