namespace BlackEngine.Models
{
    public class Profile : IProfile
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Alias { get; set; }
        public string Location { get; set; }
        public int? Age { get; set; }
        public string DOB { get; set; }
        public string Mobile { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string Snapchat { get; set; }
        public string AltSocialMedia { get; set; }
    }
}
