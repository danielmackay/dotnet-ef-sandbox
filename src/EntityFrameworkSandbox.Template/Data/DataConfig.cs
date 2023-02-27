namespace EntityFrameworkSandbox.Template.Data;

public class DataConfig
{
    public const string Section = "Data";

    public int Blogs { get; set; }
    public int PostsPerBlog { get; set; }
    public bool EnableMigrations { get; set; }
}
