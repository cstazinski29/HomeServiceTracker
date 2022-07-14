namespace HomeServiceTracker.Server.Models
{
    public class ServiceProviderInfo
    {
        public int Id { get; set; }
        public string ServiceProviderName { get; set; }
        public int NumberOfServices { get; set; }
        public float AverageServiceProviderRating { get; set; }
    }
}
