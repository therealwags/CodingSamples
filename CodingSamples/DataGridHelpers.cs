using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CodingSamples {
  class DataGridHelpers {
    public Hashtable ViewState { get; set; }
    public SortDirection GridViewSortDirection {
      get {
        if (ViewState["sortDirection"] == null) {
          ViewState["sortDirection"] = SortDirection.Ascending;
        }
        return (SortDirection)ViewState["sortDirection"];
      }
      set => ViewState["sortDirection"] = value;
    }

    public void Load() {
      /* Sets focus to TextBox and Default Submit button */
      var textBox = new TextBox();
      textBox.Focus();
      var button = new Button();
      //this.Form.DefaultButton = button;
    }

    public void UpdatePanel() {
      // To maually display selected info
      var panel = new UpdatePanel();
      panel.Update();
    }

    public void SortDropDownList(ref DropDownList ddl) {
      var textList = new ArrayList();
      var valueList = new ArrayList();

      foreach (ListItem listItem in ddl.Items) {
        textList.Add(listItem.Text);
      }
      textList.Sort();

      foreach (var obj in textList) {
        //var value = ddl.Items.FindByText(item.ToString()).Value;
        valueList.Add(ddl.Items.FindByText(obj.ToString()).Value);
      }
      ddl.Items.Clear();

      for (var i = 0; i < textList.Count; i++) {
        //var objItem = new ListItem(textList[i].ToString(), valueList[i].ToString());
        ddl.Items.Add(new ListItem(textList[i].ToString(), valueList[i].ToString()));
      }
    }

    protected void grid_sorting(object sender, GridViewSortEventArgs e) {
      var sortExpression = e.SortExpression;
      ViewState["SortExpression"] = e.SortExpression;
      if (GridViewSortDirection == SortDirection.Ascending) {
        GridSort(sortExpression, " ASC");
        GridViewSortDirection = SortDirection.Descending;
      }
      else {
        GridSort(sortExpression, " DESC");
        GridViewSortDirection = SortDirection.Ascending;
      }
    }

    private void GridSort(string sortExpression, string v) => throw new NotImplementedException();
  }
}
