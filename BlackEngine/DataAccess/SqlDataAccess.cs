using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BlackEngine.Models;
using System;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;

namespace BlackEngine.DataAccess
{
    public class SqlDataAccess
    {
        private string connectionString;

        public SqlDataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private string sqlFields => "FirstName, LastName, Alias, Location, Age, DOB, Mobile, Twitter, Instagram, Facebook, Snapchat, AltSocialMedia";
        private string sqlSelect => @$"select ID, {sqlFields} from dbo.Profile;";
        private string sqlInsert => $@"insert into dbo.Profile ({sqlFields}) 
                    values (@FirstName, @LastName, @Alias, @Location, @Age, @DOB, @Mobile, @Twitter, @Instagram, @Facebook, @Snapchat, @AltSocialMedia);";
        private string sqlUpdate => @$"UPDATE Profile 
                    SET FirstName = @FirstName, LastName = @LastName, Alias = @Alias, Location = @Location, Age = @Age, DOB = @DOB, Mobile = @Mobile, 
                    Twitter = @Twitter, Instagram = @Instagram, Facebook = @Facebook, Snapchat = @Snapchat, AltSocialMedia = @AltSocialMedia WHERE ID = @ID";
        private string sqlDelete => "DELETE FROM Profile WHERE ID = @ID";
        public List<IProfile> LoadData()
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                List<IProfile> profileList = new List<IProfile>();

                SqlCommand command = new SqlCommand(sqlSelect, cnn);
                cnn.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    IProfile profile = new Profile();
                    profile.ID = reader.GetInt32(0);
                    profile.FirstName = reader.GetString(1);
                    profile.LastName = reader.GetString(2);
                    profile.Alias = reader.GetString(3);
                    profile.Location = reader.GetString(4);
                    profile.Age = reader.GetInt32(5);
                    profile.DOB = reader.GetDateTime(6) == SqlDateTime.MinValue ? string.Empty : reader.GetDateTime(6).ToString("d MMM yyyy");
                    profile.Mobile = Regex.Replace(reader.GetString(7), @"(\d{5})(\d{3})(\d{3})", "$1 $2 $3");
                    profile.Twitter = reader.GetString(8);
                    profile.Instagram = reader.GetString(9);
                    profile.Facebook = reader.GetString(10);
                    profile.Snapchat = reader.GetString(11);
                    profile.AltSocialMedia = reader.GetString(12);
                    profileList.Add(profile);
                }
                return profileList;
            }
        }

        public int SaveData(IProfile profile)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlInsert;
                command.Parameters.AddWithValue("@FirstName", profile.FirstName);
                command.Parameters.AddWithValue("@LastName", profile.LastName ?? string.Empty);
                command.Parameters.AddWithValue("@Alias", profile.Alias ?? string.Empty);
                command.Parameters.AddWithValue("@Location", profile.Location ?? string.Empty);
                command.Parameters.AddWithValue("@Age", profile.Age ?? 0);
                command.Parameters.AddWithValue("@DOB", profile.DOB == null ? SqlDateTime.MinValue : DateTime.ParseExact(profile.DOB, "dd mm yyyy", null));
                command.Parameters.AddWithValue("@Mobile", profile.Mobile ?? string.Empty);
                command.Parameters.AddWithValue("@Twitter", profile.Twitter ?? string.Empty);
                command.Parameters.AddWithValue("@Instagram", profile.Instagram ?? string.Empty);
                command.Parameters.AddWithValue("@Facebook", profile.Facebook ?? string.Empty);
                command.Parameters.AddWithValue("@Snapchat", profile.Snapchat ?? string.Empty);
                command.Parameters.AddWithValue("@AltSocialMedia", profile.AltSocialMedia ?? string.Empty);
                command.Connection = cnn;
                cnn.Open();
                return command.ExecuteNonQuery();
            }
        }

        public int EditData(IProfile profile)
        {
            //IProfile originalProfile = new Profile();
            List<IProfile> list = LoadData();
            IProfile originalProfile = list.Find(p => p.ID == profile.ID);

            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlUpdate;
                command.Parameters.AddWithValue("@FirstName", profile.FirstName ?? originalProfile.FirstName);
                command.Parameters.AddWithValue("@LastName", profile.LastName ?? string.Empty);
                command.Parameters.AddWithValue("@Alias", profile.Alias ?? string.Empty);
                command.Parameters.AddWithValue("@Location", profile.Location ?? string.Empty);
                command.Parameters.AddWithValue("@Age", profile.Age ?? 0);
                command.Parameters.AddWithValue("@DOB", profile.DOB == originalProfile.DOB ? SqlDateTime.Parse(profile.DOB) : SqlDateTime.Parse(profile.DOB));//DateTime.ParseExact(profile.DOB, "dd mm yyyy", null));
                command.Parameters.AddWithValue("@Mobile", profile.Mobile ?? string.Empty);
                command.Parameters.AddWithValue("@Twitter", profile.Twitter ?? string.Empty);
                command.Parameters.AddWithValue("@Instagram", profile.Instagram ?? string.Empty);
                command.Parameters.AddWithValue("@Facebook", profile.Facebook ?? string.Empty);
                command.Parameters.AddWithValue("@Snapchat", profile.Snapchat ?? string.Empty);
                command.Parameters.AddWithValue("@AltSocialMedia", profile.AltSocialMedia ?? string.Empty);
                command.Parameters.AddWithValue("@ID", profile.ID);
                command.Connection = cnn;
                cnn.Open();
                return command.ExecuteNonQuery();
            }
        }

        public int DeleteData(IProfile profile)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlDelete;
                command.Parameters.AddWithValue("@ID", profile.ID);
                command.Connection = cnn;
                cnn.Open();
                return command.ExecuteNonQuery();
            }
        }

        public static string GetConnectionString(string connectionName = "BlackBox")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        public DataTable LoadDataTable(string sql)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, cnn);
                DataTable table = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(table);
                }
                return table;
            }
        }
    }
}
