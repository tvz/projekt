using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Page : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {  
        
        //By :Andor
        //Za sada da playa neki video sa VideoID kako je tu podesen
        //Potrebna dorada da vadi iz baze url
        



        string VideoID = "HnRc46xWSfI";
        LabelShowYouTubeVideo.Text = "<object width='425' height='355'><param name='movie' value='http://www.youtube.com/v/" + VideoID + "'></param><param name='wmode' value='transparent'></param><embed src='http://www.youtube.com/v/" + VideoID + "' type='application/x-shockwave-flash' wmode='transparent' width='425' height='355'></embed></object>";
    }
}
