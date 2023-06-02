namespace Bazydanych.Models
{
    public class Orders
    {
        public int LoadingId { get; set; }
        public int UnloadingId { get; set; }
        public int contractorID { get; set; }
        public int TraceId { get; set; }
        public int CarId { get; set; }
        public DateTime? Pickupdate { get; set; }
        public TimeSpan? Time_To_Loading { get; set; }
        public TimeSpan? Time_To_Unloading { get; set; }
    }
}
