using System.Data.Common;
using System.Data.SqlClient;

namespace DemoPractice.Repository
{
        public class BaseAsyncRepository
        {
            private string sqlwriterConnectionString;
            private string sqlreaderConnectionString;

            private string databaseType;

            public BaseAsyncRepository(IConfiguration configuration)
            {
                sqlwriterConnectionString = configuration.GetSection("DBInfo:WriterConnectionString").Value;
                sqlreaderConnectionString = configuration.GetSection("DBInfo:ReaderConnectionString").Value;
                databaseType = configuration.GetSection("DbInfo:DbType").Value;
            }

            internal DbConnection sqlwriterConnection
            {
                get
                {
                    // Initiate Appropriate Database engine specific connection

                    // Create IDbConnection as per Database type

                    switch (databaseType)
                    {
                        case "SqlServer":
                            return new System.Data.SqlClient.SqlConnection(sqlwriterConnectionString);
                        default:
                            return new SqlConnection(sqlwriterConnectionString);
                    }
                }
            }

            internal DbConnection sqlreaderConnection
            {
                get
                {
                    switch (databaseType)
                    {
                        case "SqlServer":
                            return new System.Data.SqlClient.SqlConnection(sqlreaderConnectionString);
                        default:
                            return new SqlConnection(sqlreaderConnectionString);
                    }

                }
            }

        }
    }

