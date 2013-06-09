using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.ServiceModel.Syndication;
using System.Xml;

public partial class RSSFeed : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        Response.ContentType = "application/rss+xml";

        SyndicationFeed feed = SyndicationHelper.getRSS();
        Rss20FeedFormatter formatter = new Rss20FeedFormatter(feed);
        XmlWriter writer = XmlWriter.Create(Response.Output, null);

        formatter.WriteTo(writer);
        writer.Flush();
    }
}