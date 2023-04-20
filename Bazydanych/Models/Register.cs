namespace Bazydanych.Models
{
    public class Register
    {
        public string Login { get; set; } = null!;
        public string Pass { get; set; } = null!;
        public string VerPass { get; set; } = null!;

        public int? Phone { get; set; }
        public string? Licence { get; set; }
        public bool is_driver { get; set; }
        public bool is_in_base { get; set; }
        public int? pause_time { get; set; }

        public string? UserRole { get; set; }
    }
}
