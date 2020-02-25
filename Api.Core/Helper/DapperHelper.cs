using System.Data;
using Microsoft.Data.SqlClient;

namespace Api.Core
{
    public class DapperHelper
    {
        public static IDbConnection DB
        {
            get
            {
                //return new SqlConnection(Appsetting.GetConfig("DBConnectionString"));
                return new SqlConnection(Appsettings.app(new string[] { "DB", "DBConnectionString" }));
            }
        }
    }
}
