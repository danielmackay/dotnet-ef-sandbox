using System.Text;

namespace EntityFrameworkSandbox.Template.Data.Entities;

public class Tag
{
    public int TagId { get; set; }

    public string Name { get; set; }

    public ICollection<Post> Posts { get; set; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"TagId:{TagId}");
        sb.AppendLine($"Name:{Name}");
        return sb.ToString();
    }
}