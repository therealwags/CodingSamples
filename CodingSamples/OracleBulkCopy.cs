using System;
using System.Data;
using System.Text;
using Oracle.ManagedDataAccess.Client;

namespace CodingSamples {
  class OracleBulkCopy {
    private readonly string _connectionString;

    public OracleBulkCopy(string connectionString) => _connectionString = connectionString;

    public int BulkCopyTimeout { private get; set; }
    public string DestinationTableName { private get; set; }

    public void WriteToServer(DataTable dataTable) {
      var cols = new StringBuilder(101);
      foreach (var col in dataTable.Columns) {
        cols.Append($"{col}");
      }
      var colNames = cols.ToString().TrimEnd(',');

      var template = $"insert into {DestinationTableName} ({colNames}) values ({0})";
      var limit = 0;

      var rows = new StringBuilder(101);
      foreach (DataRow row in dataTable.Rows) {
        if (limit >= 1000) {
          rows.AppendLine("commit;");
          limit = 0;
        }
        var sb = new StringBuilder(101);
        foreach (DataColumn col in dataTable.Columns) {
          var val = row[col];
          var isNumeric = IsNumeric(col.DataType);
          sb.Append(isNumeric ? $"{val}," : $"'{val}'");
        }
        rows.AppendLine(string.Format(template, sb.ToString().Trim(',')));
        limit++;
        rows.AppendLine("commit;");

        var sql = rows.ToString();

        using (var conn = new OracleConnection(_connectionString)) {
          conn.Open();
          using (var cmd = new OracleCommand(sql, conn)) {
            cmd.CommandTimeout = BulkCopyTimeout;
            cmd.ExecuteNonQuery();
          }
        }
      }
    }

    private bool IsNumeric(Type dataType) {
      switch (dataType.ToString()) {
        case "int":
        case "decimal":
          return true;
        default:
          return false;
      }
    }
  }
}
