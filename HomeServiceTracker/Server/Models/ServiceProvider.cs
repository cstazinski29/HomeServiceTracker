﻿namespace HomeServiceTracker.Server.Models
{
    public class ServiceProvider
    {
        public int Id { get; set; }
        public string ServiceProviderId { get; set; }
        public int NumberOfServices { get; set; }
        public float AverageServiceProviderRating { get; set; }
    }
}
