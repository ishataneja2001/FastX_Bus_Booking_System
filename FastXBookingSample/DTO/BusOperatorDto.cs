namespace FastXBookingSample.DTO
{
    public class BusOperatorDto
    {
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string ContactNo { get; set; } = null!;
    }
}
