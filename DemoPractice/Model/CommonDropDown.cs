namespace DemoPractice.Model
{
    public class CommonDropDown
    {

    }
    public class DistrictDropDown
    {
        public long Id { get; set; }
        public string districtName { get; set; }
    }
    public class TalukaDropDown
    {
        public long Id { get; set; }
        public string talukaName { get; set; }
        public long districtId { get; set; }

    }
    public class VillageDropDown
    {
        public long Id { get; set; }
        public string villageName { get; set; }
        public long talukaId { get; set; }
        public long districtId { get; set; }
        
            


    }
}
