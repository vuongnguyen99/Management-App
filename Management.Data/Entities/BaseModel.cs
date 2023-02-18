namespace Management.Data.Entities
{
    public class BaseModel
    {
        public string? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
