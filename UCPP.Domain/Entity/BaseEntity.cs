namespace UCP.Domain.Entity
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy{ get; set; }
        public DateTime Datejoined { get; set; }
        public string? CreateBy { get; set; }
    }

}
