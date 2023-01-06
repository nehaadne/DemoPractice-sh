namespace DemoPractice.Model
{
    public class ReligionModel
    {
            public long Id { get; set; }
            public string Religion { get; set; }
            public long CreatedBy { get; set; }
            public DateTime CreatedDate { get; set; }
            public long ModifiedBy { get; set; }
            public DateTime ModifiedDate { get; set; }
            public bool IsDeleted { get; set; }
    }
}
