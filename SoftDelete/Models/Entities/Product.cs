namespace SoftDelete.Models.Entities
{
    public class Product : ISoftDelete
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? EditedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
