using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Syndication;
using System.Data;

/// <summary>
/// Summary description for SyndicationHelper
/// </summary>
public class SyndicationHelper
{
	public SyndicationHelper()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /*developer: Ivan
     * description: metoda kreira rss sadrzaj
     */
    public static SyndicationFeed getRSS()
    {
        Uri url = new Uri("http://localhost:54125/zicalica/index.aspx");
        SyndicationFeed feed = new SyndicationFeed();
        feed.Title = TextSyndicationContent.CreateHtmlContent("ŽICALICA");
        feed.Description = TextSyndicationContent.CreateHtmlContent("Novi projekti");
        feed.ImageUrl = new Uri("http://localhost:54125/zicalica/img/logo.png");
        feed.Links.Add(SyndicationLink.CreateAlternateLink(url));

        List<SyndicationItem> items = new List<SyndicationItem>();
        DataTable table = Projects.fetchInfo();

        foreach (DataRow row in table.Rows)
        {
            string title = row["name"].ToString();
            string description = row["description"].ToString();
            string date = row["created_at"].ToString();
            DateTime dt = Convert.ToDateTime(date);
            dt.Subtract(TimeSpan.FromHours(2));

            SyndicationItem sItem = new SyndicationItem();
            sItem.Title = SyndicationContent.CreateHtmlContent(title);
            sItem.Content = SyndicationContent.CreateHtmlContent(description);
            sItem.PublishDate = DateTimeOffset.Parse(dt.ToString("r"));

            items.Add(sItem);
        }

        feed.Items = items;
        return feed;
    }
}