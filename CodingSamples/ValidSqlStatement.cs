namespace CodingSamples {
  class ValidSqlStatement {
    private string CheckElementSingleQuote(string value) {
      var retVal = string.Empty;

      try {
        var elements = value.Split(';');
        foreach (var element in elements) {
          var tempString = element.Trim();
          if (!string.IsNullOrEmpty(tempString)) {
            if (!tempString.StartsWith("'")) { tempString = $"'{tempString}"; }
            if (!tempString.EndsWith("'")) { tempString = $"{tempString}'"; }
          }
          retVal += "," + tempString;
        }
        if (retVal.Length > 1) { retVal = retVal.Substring(1); }
      }
      catch {
        retVal = string.Empty;
      }
      return retVal;
    }
  }
}
