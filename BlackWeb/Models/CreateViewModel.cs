using BlackEngine.DataAccess;
using BlackEngine.Models;

namespace BlackWeb.Models
{
    public class CreateViewModel
    {
        SqlDataAccess dataAccess;

        public CreateViewModel(string connectionString)
        {
            dataAccess = new SqlDataAccess(connectionString);
        }

        public void CreateProfile(IProfile profile)
        {
            dataAccess.SaveData(profile);
        }
    }
}
