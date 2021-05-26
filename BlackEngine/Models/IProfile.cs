namespace BlackEngine.Models
{
    public interface IProfile
    {
        int ID { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Alias { get; set; }
        string Location { get; set; }
        int? Age { get; set; }
        string DOB { get; set; }
        string Mobile { get; set; }
        string Twitter { get; set; }
        string Instagram { get; set; }
        string Facebook { get; set; }
        string Snapchat { get; set; }
        string AltSocialMedia { get; set; }

    }
}
