using BlackEngine.DataAccess;
using BlackEngine.Models;

using System.Collections.Generic;

namespace BlackWeb.Models
{
    public class DeleteViewModel
    {
        SqlDataAccess dataAccess;

        public DeleteViewModel(string connectionString)
        {
            dataAccess = new SqlDataAccess(connectionString);
        }

        public List<IProfile> GetProfiles => dataAccess.LoadData();

        public void DeleteProfile(IProfile profile)
        {
            dataAccess.DeleteData(profile);
        }
    }
}
