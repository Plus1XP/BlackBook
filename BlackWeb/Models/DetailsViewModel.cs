using BlackEngine.DataAccess;
using BlackEngine.Models;

using System.Collections.Generic;

namespace BlackWeb.Models
{
    public class DetailsViewModel
    {
        SqlDataAccess dataAccess;

        public DetailsViewModel(string connectionString)
        {
            dataAccess = new SqlDataAccess(connectionString);
        }

        public List<IProfile> GetProfiles => dataAccess.LoadData();
    }
}
