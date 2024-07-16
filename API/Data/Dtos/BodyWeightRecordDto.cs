namespace API.Data.Dtos
{
    public class BodyWeightRecordDto
    {
        public DateTime Date { get; set; }
        public int Weight { get; set; }
        public int? Neck { get; set; }
        public int? Chest { get; set; }
        public int? Arm { get; set; }
        public int? Forearm { get; set; }
        public int? Waist { get; set; }
        public int? Hip { get; set; }
        public int? Thigh { get; set; }
        public int? Calf { get; set; }
    }
}
