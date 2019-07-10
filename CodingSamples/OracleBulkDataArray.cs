using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

namespace CodingSamples {
  class OracleBulkDataArray<T> {
    private OracleConnection oracleConnection;
    private OracleConnectionStringBuilder stringBuilder;

    public OracleBulkDataArray(string userName, string password, string server) {
      stringBuilder = new OracleConnectionStringBuilder {
        DataSource = server,
        UserID = userName,
        Password = password
      };
      oracleConnection = new OracleConnection { ConnectionString = stringBuilder.ConnectionString };
    }

    public bool UploadBulkData(List<T> bulkData) {
      try {
        var query = @"insert into table (columns) values (vals)";
        oracleConnection.Open();
        using (var cmd = oracleConnection.CreateCommand()) {
          cmd.CommandText = query;
          cmd.CommandType = System.Data.CommandType.Text;
          cmd.BindByName = true;
          // In order to user ArrayBinding, the ArrayBindCount property
          // of OracleCommand object must be set to the number of records to be inserted
          cmd.ArrayBindCount = bulkData.Count;
          cmd.Parameters.Add("");

          return cmd.ExecuteNonQuery() == bulkData.Count;
        }
      }
      catch (OracleException ex) {

        throw;
      }
      finally {
        oracleConnection.Close();
      }
    }
  }
}
