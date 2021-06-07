using BlackEngine.DataAccess;
using BlackEngine.Models;

using System.Collections.Generic;

namespace BlackWeb.Models
{
    public class EditViewModel
    {
        SqlDataAccess dataAccess;

        public EditViewModel(string connectionString)
        {
            dataAccess = new SqlDataAccess(connectionString);
        }

        public List<IProfile> GetProfiles => dataAccess.LoadData();

        public void EditProfile(IProfile profile)
        {
            dataAccess.EditData(profile);
        }
    }
}
