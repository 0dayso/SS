public void Run()
{
	Logger.ClearAll();
	Default.Navigate("http://www.cnblogs.com/iamzyf/archive/2008/01/02/1023327.html");
	Default.Ready();
	
	var title = Default.SelectSingleNode("#cb_post_title_url");
	string sTitle = title.Text();
	Logger.Log(title.Attr("href"));
	var body = Default.SelectSingleNode("#cnblogs_post_body");
	string sBody = body.Text();
	Logger.Log(sTitle);
	Logger.Log(sBody);
	
	return;
	//add your codes here:
	//JQueryBrowser jb = new JQueryBrowser();
    Default.Navigate("http://write.blog.csdn.net/postedit");
    Default.Ready();
    JQueryContext jc = Default.SelectSingleNode("#xhe0_iframe");
    string fb = jc.GetFrameBody();
    string value = "test content"; // TODO: Initialize to an appropriate value
    jc.SetFrameBody(value);
    fb = jc.GetFrameBody();
    
     title = Default.SelectSingleNode("#txtTitle");
    title.Attr("value","title");
    Logger.Log(title.Text());
    
    var type = Default.SelectSingleNode("#selType");
    type.Attr("value","2");
            
    var btn = Default.SelectSingleNode("#btnPublish");
    btn.Click();        
}