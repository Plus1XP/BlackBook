using BlackEngine.DataAccess;
using BlackEngine.Models;

using System.Collections.Generic;

namespace BlackWeb.Models
{
    public class IndexViewModel
    //public static class IndexViewModel
    {
        SqlDataAccess dataAccess;

        public IndexViewModel(string connectionString)
        {
            dataAccess = new SqlDataAccess(connectionString);
        }

        public static int ID { get; set; }
        public static string FirstName { get; set; }
        public static string LastName { get; set; }
        public static string Alias { get; set; }
        public static string Location { get; set; }
        public static int? Age { get; set; }
        public static string DOB { get; set; }
        public static string Mobile { get; set; }
        public static string Twitter { get; set; }
        public static string Instagram { get; set; }
        public static string Facebook { get; set; }
        public static string Snapchat { get; set; }
        public static string AltSocialMedia { get; set; }

        public List<IProfile> GetProfiles => dataAccess.LoadData();

        public void CreateProfile(IProfile profile)
        {
            dataAccess.SaveData(profile);
        }
    }
}
