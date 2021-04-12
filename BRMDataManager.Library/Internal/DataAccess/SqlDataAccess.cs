using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
namespace BRMDataManager.Library.Internal.DataAccess {
    public class SqlDataAccess {
        internal string GetConnectionString(string name) {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public List<T> LoadData<T, TU>(string storedProcedure, TU parameters, string connectionName) {
            string connectionString = GetConnectionString(connectionName);
            using (IDbConnection connection = new SqlConnection(connectionString)) {
                List<T> rows = connection.Query<T>(storedProcedure, parameters, 
                    commandType: CommandType.StoredProcedure).ToList();
                return rows;
            }
        }

        public void SaveData<T>(string storedProcedure, T parameters, string connectionName) {
            string connectionString = GetConnectionString(connectionName);
            using (IDbConnection connection = new SqlConnection(connectionString)) {
                connection.Execute(storedProcedure, parameters, 
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
