namespace SoftDelete.Models.Entities
{
    public class Category : ISoftDelete
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime RegistrationDate { get; set; }
        public DateTime? EditedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
