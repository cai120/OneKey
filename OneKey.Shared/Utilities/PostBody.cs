using OneKey.Shared.Models;

namespace OneKey.Shared.Utilities
{
    public class PostBody
    {
        public string Expression { get; set; }
        public List<string> Includes { get; set; } = new List<string>();
        public Pagination Pagination { get; set; }
    }
}