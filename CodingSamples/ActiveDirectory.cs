using System;
using System.DirectoryServices;

namespace CodingSamples {
  internal class ActiveDirectory {
    private static void Test() {
      DirectoryEntry directoryEntry; // binding object
      SearchResultCollection searchResultCollection; // results collection
      // Construct binding string
      const string path = "LDAP://localhost:000/OU=TestOU,O=Fabrikam,C=US";
      Console.WriteLine($"Bind to:{path}");
      Console.WriteLine($"Enum: Groups and members.");
      // Get the AD LDS object
      try {
        directoryEntry = new DirectoryEntry(path);
        directoryEntry.RefreshCache();
      }
      catch (Exception ex) {
        Console.WriteLine($"Error: Bind Failed | {ex.Message}");
        throw;
      }
      // Get search object, specify filter and scope
      try {
        var directorySearcher = new DirectorySearcher(directoryEntry) {
          Filter = "(&(objectClass=group))",
          SearchScope = SearchScope.Subtree
        };
        searchResultCollection = directorySearcher.FindAll();
      }
      catch (Exception ex) {
        Console.WriteLine($"Error: Search Failed | {ex.Message}");
        throw;
      }
      // Enumerate groups and members
      try {
        if (searchResultCollection.Count > 0) {
          foreach (SearchResult searchResult in searchResultCollection) {
            var groupEntry = searchResult.GetDirectoryEntry();
            Console.WriteLine($"Group  {groupEntry.Name}");

            foreach (var objMember in groupEntry.Properties["member"]) {
              Console.WriteLine($"Member:  {objMember}");
            }
          }
        }
        else {
          Console.WriteLine("No groups found");
        }
      }
      catch (Exception ex) {
        Console.WriteLine($"Error: Enumerate Failed | {ex.Message}");
        throw;
      }
      finally {
        Console.WriteLine("Success: Enumeration complete");
      }
    }
  }
}
