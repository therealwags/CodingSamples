using System;
using System.Collections.Generic;
using System.DirectoryServices;

namespace CodingSamples {
  internal class AdGroupSearch {
    private static void GetGroupMembers(string[] args) {
      using (var searchRoot = new DirectoryEntry("LDAP://DC=x,DC=y")) {
        foreach (var user in GetUsersRecursively(searchRoot, "CN=MainGroup,DC=x,DC=y")) {
          Console.WriteLine($"{user.Properties["sAMAccountName"]}");
        }
      }
    }

    private static IEnumerable<SearchResult> GetUsersRecursively(DirectoryEntry searchRoot, string groupDn) {
      var searchedGroups = new List<string>();
      var searcedUsers = new List<string>();
      return GetUsersRecursively(searchRoot, groupDn, searchedGroups, searcedUsers);
    }

    private static IEnumerable<SearchResult> GetUsersRecursively(DirectoryEntry searchRoot, string groupDn, List<string> searchedGroups, List<string> searchedUsers) {
      foreach (var subGroup in GetMembers(searchRoot, groupDn, "group")) {
        var subGroupName = ((string)subGroup.Properties["sAMAccountName"][0]).ToUpperInvariant();
        if (searchedGroups.Contains(subGroupName)) { continue; }
        searchedGroups.Add(subGroupName);
        var subGroupDn = ((string)subGroup.Properties["distinguishedName"][0]);

        foreach (var user in GetUsersRecursively(searchRoot, subGroupDn, searchedGroups, searchedUsers)) {
          yield return user;
        }
      }

      foreach (var user in GetMembers(searchRoot, groupDn, "user")) {
        var userName = ((string)user.Properties["sAMAccountName"][0]).ToUpperInvariant();
        if (searchedUsers.Contains(userName)) { continue; }
        searchedUsers.Add(userName);
        yield return user;
      }
    }

    private static IEnumerable<SearchResult> GetMembers(DirectoryEntry searchRoot, string groupDn, string objectClass) {
      using (var searcher = new DirectorySearcher(searchRoot)) {
        searcher.Filter = "";
        searcher.PropertiesToLoad.Clear();
        searcher.PropertiesToLoad.AddRange(new[] { "objectGUID", "sAMAccountName", "distinguishedName" });
        searcher.Sort = new SortOption("sAMAccountName", SortDirection.Ascending);
        searcher.PageSize = 1000;
        searcher.SizeLimit = 0;
        foreach (SearchResult result in searcher.FindAll()) {
          yield return result;
        }
      }
    }
  }
}
