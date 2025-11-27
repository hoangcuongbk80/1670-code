namespace FPTJobMatch.Models
{
    public class JobApplication
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string ApplicantName { get; set; }
        public string Email { get; set; }
        public string Resume { get; set; }
    }
}
