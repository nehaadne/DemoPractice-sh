namespace DemoPractice.Model
{
    public class UserRegistrationModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string userName { get; set; }
        public long mobileNo { get; set; }
        public string emailId { get; set; }
        public long districtId { get; set; }
        public long talukaId { get; set; }
        public long vilageId { get; set; }
        public string password { get; set; }
        public int createdBy { get; set; }
        public DateTime createDate { get; set; }
        public int modifiedBy { get; set; }
        public DateTime modifiedDate { get; set; }
        public bool isDeleted { get; set; }
    }
}
 												