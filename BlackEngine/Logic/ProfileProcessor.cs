using BlackEngine.DataAccess;

namespace BlackEngine.Logic
{
    public class ProfileProcessor
    {
        SqlDataAccess sqlDataAccess;

        public ProfileProcessor(string connectionString)
        {
            sqlDataAccess = new SqlDataAccess(connectionString);
        }

        //public int CreateProfile(string firstName, string lastName = null, string alias = null, string location = null, int? age = null, string dob = null, 
        //    int? mobile = null, string twitter = null, string instagram = null, string facebook = null, string snapchat = null, string altSocialMedia = null)
        //{
        //    IProfile data = new Profile
        //    {
        //        FirstName = firstName,
        //        LastName = lastName,
        //        Alias = alias,
        //        Location = location,
        //        Age = age,
        //        DOB = dob,
        //        Mobile = mobile,
        //        Twitter = twitter,
        //        Instagram = instagram,
        //        Facebook = facebook,
        //        Snapchat = snapchat,
        //        AltSocialMedia = altSocialMedia
        //    };

        //    string sql = @"insert into dbo.Profile (FirstName, LastName, Alias, Location, Age, DOB, Mobile, Twitter, Instagram, Facebook, Snapchat, AltSocialMedia) 
        //            values (@FirstName, @LastName, @Alias, @Location, @Age, @DOB, @Mobile, @Twitter, @Instagram, @Facebook, @Snapchat, @AltSocialMedia);";

        //    return sqlDataAccess.SaveData(data);
        //}

        ////public List<IProfile> LoadProfiles()
        ////{
        ////    string sql = @"select ID, FirstName, LastName, Alias, Location, Age, DOB, Mobile, Twitter, Instagram, Facebook, Snapchat, AltSocialMedia from dbo.Profile;";

        ////    return sqlDataAccess.LoadData(sql);
        ////}

        //public List<IProfile> GetProfiles(DataTable dataTable)
        //{
        //    IProfile profile;
        //    List<IProfile> list = new List<IProfile>();

        //    foreach (DataRow row in dataTable.Rows)
        //    {
        //        profile = new Profile();
        //        profile.FirstName = Convert.ToString(row["FirstName"]);
        //        profile.LastName = Convert.ToString(row["LastName"]);
        //        profile.Alias = Convert.ToString(row["Alias"]);
        //        profile.Location = Convert.ToString(row["Location"]);
        //        profile.Age = Convert.ToInt32(row["Age"]);
        //        profile.DOB = Convert.ToString(row["DOB"]);
        //        profile.Mobile = Convert.ToInt32(row["Mobile"]);
        //        profile.Twitter = Convert.ToString(row["Twitter"]);
        //        profile.Instagram = Convert.ToString(row["Instagram"]);
        //        profile.Facebook = Convert.ToString(row["Facebook"]);
        //        profile.Snapchat = Convert.ToString(row["Snapchat"]);
        //        profile.AltSocialMedia = Convert.ToString(row["AltSocialMedia"]);

        //        list.Add(profile);
        //    }

        //    return list;
        //}
    }
}