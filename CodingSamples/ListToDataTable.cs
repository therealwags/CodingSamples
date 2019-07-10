using System;
using System.Collections.Generic;
using System.Data;

namespace CodingSamples {
  internal class ListToDataTableClass<T> {
    private DataTable ListToDataTable(List<T> list) {
      var dt = new DataTable();

      try {
        foreach (var info in typeof(T).GetProperties()) {
          dt.Columns.Add(new DataColumn(info.Name, info.PropertyType));
        }

        foreach (var t in list) {
          var row = dt.NewRow();

          foreach (var info in typeof(T).GetProperties()) {
            row[info.Name] = info.GetValue(t, null);
          }
          dt.Rows.Add(row);
        }
      }
      catch (Exception ex) {
        Console.WriteLine($"{ex.InnerException.Message}");
      }
      return dt;
    }
  }
}
