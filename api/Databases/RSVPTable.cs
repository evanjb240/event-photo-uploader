using System;
using Azure;
using Azure.Data.Tables;

namespace Events.Databases
{
    public class RSVPEntity : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string Code { get; set; }
        public string Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string RSVPDecision { get; set; }
        public string ArrivalDate { get; set; }
        public string DepartureDate { get; set; }
        public int PlusOne { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
