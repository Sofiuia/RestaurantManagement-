using System;

namespace ProcessApp
{
    public class Process
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int OrderId { get; set; }
        public string Status { get; set; } = "Pending";

        public void Start()
        {
            Status = "Running";
            Console.WriteLine($"Process '{Name}' (Order ID: {OrderId}) is now running.");
        }

        public void Complete()
        {
            Status = "Completed";
            Console.WriteLine($"Process '{Name}' (Order ID: {OrderId}) has been completed.");
        }
    }
}