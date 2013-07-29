using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Globalization;

public partial class pregledProjekata : System.Web.UI.Page
{
    protected static List<Projects> search_list = new List<Projects>();
    protected List<Projects> sorted_list = new List<Projects>();
    string searchParamName;
    bool searchBoxUsed = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        string scriptExpirationStart = "$(document).ready(function(){$('#" + TextBoxExpirationDateStart.ClientID + "'" + ").datepicker({ dateFormat: 'dd.mm.yy' });});";
        ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowExpirationDateStartpicker", scriptExpirationStart, true);
        string scriptCreatedAtStart = "$(document).ready(function(){$('#" + TextBoxCreatedAtStart.ClientID + "'" + ").datepicker({ dateFormat: 'dd.mm.yy' });});";
        ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowCreatedAtStartpicker", scriptCreatedAtStart, true);
        string scriptCreatedAtEnd = "$(document).ready(function(){$('#" + TextBoxCreatedAtEnd.ClientID + "'" + ").datepicker({ dateFormat: 'dd.mm.yy' });});";
        ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowCreatedAtEndpicker", scriptCreatedAtEnd, true);
        string scriptExpirationEnd = "$(document).ready(function(){$('#" + TextBoxExpirationDateEnd.ClientID + "'" + ").datepicker({ dateFormat: 'dd.mm.yy' });});";
        ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowExpirationDateEndpicker", scriptExpirationEnd, true);
        if (Request["searchBox"] != null)
        {
            searchParamName = Request["searchBox"].ToString();
            searchBoxUsed = true;
            ButtonSearch_Click(sender, e);
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        this.DropDownListSort.SelectedIndex = 0;
        this.RadioButtonDESC.Checked = true;
    }

    protected void DropDownListSort_SelectedIndexChanged(object sender, EventArgs e)
    {
        search_list = (List<Projects>)Session["list_projects"];
        sorted_list = Sort(search_list);
        showProjects(sorted_list);
    }

    protected void ButtonReset_Click(object sender, EventArgs e)
    {
        this.TextBoxProjectName.Text = "";
        this.TextBoxGoalStart.Text = "";
        this.TextBoxGoalEnd.Text = "";
        this.TextBoxCreatedAtStart.Text = "";
        this.TextBoxCreatedAtEnd.Text = "";
        this.TextBoxExpirationDateStart.Text = "";
        this.TextBoxExpirationDateEnd.Text = "";
        this.LabelSearchResult.Text = "";
        this.DropDownListSort.Enabled = false;
        this.RadioButtonASC.Enabled = false;
        this.RadioButtonDESC.Enabled = false;
        projekti_search.Attributes.Clear();
    }

    /// <summary>
    /// Developer: Ivan
    /// Description: metoda salje parametre za pretrazivanje searchProjects metodi
    /// u klasi Projects te nazad dobiva listu projekata
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonSearch_Click(object sender, EventArgs e)
    {
        DateTime createdAtDateStart, expirationDateStart, createdAtDateEnd, expirationDateEnd;
        DateTime.TryParseExact(TextBoxExpirationDateStart.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out expirationDateStart);
        DateTime.TryParseExact(TextBoxCreatedAtStart.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out createdAtDateStart);
        DateTime.TryParseExact(TextBoxCreatedAtEnd.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out createdAtDateEnd);
        DateTime.TryParseExact(TextBoxExpirationDateEnd.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out expirationDateEnd);
        string createdAtStart, expirationStart, createdAtEnd, expirationEnd;
        createdAtStart = createdAtDateStart.ToShortDateString();
        createdAtEnd = createdAtDateEnd.ToShortDateString();
        expirationStart = expirationDateStart.ToShortDateString();
        expirationEnd = expirationDateEnd.ToShortDateString();

        if (searchBoxUsed == true)
        {
            search_list = Projects.searchProjects(searchParamName, TextBoxGoalStart.Text, TextBoxGoalEnd.Text,
            createdAtStart, createdAtEnd, expirationStart, expirationEnd);
            this.TextBoxProjectName.Text = searchParamName;
        }
        else
            search_list = Projects.searchProjects(TextBoxProjectName.Text, TextBoxGoalStart.Text, TextBoxGoalEnd.Text,
            createdAtStart, createdAtEnd, expirationStart, expirationEnd);

        showProjects(search_list);

        if (search_list.Count > 1)
        {
            DropDownListSort.Enabled = true;
            RadioButtonASC.Enabled = true;
            RadioButtonDESC.Enabled = true;
        }
        else 
        {
            this.DropDownListSort.Enabled = false;
            this.RadioButtonASC.Enabled = false;
            this.RadioButtonDESC.Enabled = false;
        }

        Session["list_projects"] = search_list;
    }
    
    /// <summary>
    /// Developer: Ivan
    /// Description: metoda sortira listu projekata po zadanim kriterijima
    /// </summary>
    /// <param name="list">Nesortirana lista projekata</param>
    /// <returns>Sortirana lista projekata</returns>
    private List<Projects> Sort(List<Projects> list)
    {
        if (RadioButtonASC.Checked == true)
        {
            if (DropDownListSort.SelectedIndex == 0)
                sorted_list = search_list.OrderBy(o => o.name).ToList();
            else if (DropDownListSort.SelectedIndex == 1)
                sorted_list = search_list.OrderBy(o => o.goal).ToList();
            else if (DropDownListSort.SelectedIndex == 2)
                sorted_list = search_list.OrderBy(o => o.created_at).ToList();
            else if (DropDownListSort.SelectedIndex == 3)
                sorted_list = search_list.OrderBy(o => o.expiration_date).ToList();
        }
        else if (RadioButtonDESC.Checked == true)
        {
            if (DropDownListSort.SelectedIndex == 0)
                sorted_list = search_list.OrderByDescending(o => o.name).ToList();
            else if (DropDownListSort.SelectedIndex == 1)
                sorted_list = search_list.OrderByDescending(o => o.goal).ToList();
            else if (DropDownListSort.SelectedIndex == 2)
                sorted_list = search_list.OrderByDescending(o => o.created_at).ToList();
            else if (DropDownListSort.SelectedIndex == 3)
                sorted_list = search_list.OrderByDescending(o => o.expiration_date).ToList();
        }
        return sorted_list;
    }

    /// <summary>
    /// Developer: Ivan
    /// Description: metoda prikazuje projekte iz liste
    /// </summary>
    /// <param name="list"></param>
    private void showProjects(List<Projects> list)
    {
        string html = null;
        projekti_search.InnerHtml = "";
        this.LabelSearchResult.Text = "Pronađeno: <b>" + list.Count + "</b>";

        foreach (Projects project in list)
        {
            html = "<a href='projektInfo.aspx/" + project.name + "' title='Saznaj više' class='projectLink'><h2>" + project.name + "</h2></a>"
                    + "<img  src=" + "'" + project.image_path + "'" + " alt=" + "'" + project.name + "'" + "> ";
            html += "<h3><b>AUTOR PROJEKTA:</b> " + project.project_owner_username + "</h3>"
            + "<h3><b>OPIS PROJEKTA:</b> " + project.description + " </h3>"
            + "<h3><b>SAKUPLJENO:</b> " + project.DonationSum() + " Kunića " + "(" + project.DonationsPercent() + "%)" + "</h3>"
            + "<h3><b>DO KRAJA:</b> " + (project.expiration_date - DateTime.Now).Days + "Dana" + "</h3>";

            HtmlGenericControl div = new HtmlGenericControl("div");
            div.Attributes.Add("class", "proj");
            div.InnerHtml = html;
            projekti_search.Controls.Add(div);
            projekti_search.Attributes.Add("style", "height:544px;overflow:auto");
        } 
    }
}
