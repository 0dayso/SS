public void Run()
{
	Logger.ClearAll();
	Default.Navigate( "http://www.sohu.com");
	Default.Ready(10);
	
	var listBrs = BrowserManager.Has("listBrs") ? BrowserManager.Get("listBrs") : BrowserManager.Create("listBrs", "about:blank");
	var news = Default.SelectNodes("#top_news>ul>li>a");
	int no = 0;
	List<string> urlList = new List<string>();
	foreach(var n in news)
	{
		urlList.Add(n.Attr("href"));
	}
	
	foreach(string newUrl in urlList)
	{
		no++;
		//if(no > 2) return;
		Logger.Log(no.ToString() + " " + newUrl);
		Default.Navigate(newUrl);
		Default.Reset();
		Default.Ready("button:contains(\"发布\")",10);
		Default.Ready("textarea.textarea-fw.textarea-bf",10);
		Default.Ready(10);
		var comment = Default.SelectSingleNode("textarea.textarea-fw.textarea-bf");
		if(!comment.IsEmpty())
		{
			comment.Text("点赞!!!");
			Logger.Log(comment.Text());
			var btn = Default.SelectSingleNode("button:contains(\"发布\")");
			btn.Click();
			Logger.Log("done");
			listBrs.Reset();
			Default.Ready(10);
		}
		else
		{
			Logger.Log("not found");
		}
	}
}