using BlackEngine.DataAccess;
using BlackEngine.Models;

using System.Collections.Generic;

namespace BlackWeb.Models
{
    public class SearchViewModel
    {
        SqlDataAccess dataAccess;

        public SearchViewModel(string connectionString)
        {
            dataAccess = new SqlDataAccess(connectionString);
        }

        public List<IProfile> GetProfiles => dataAccess.LoadData();
    }
}
