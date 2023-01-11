namespace OneKey.Shared.Models
{
    public class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public bool IsDeleted { get; set; }
    }

    public interface IBaseEntity
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public bool IsDeleted { get; set; }
    }
}