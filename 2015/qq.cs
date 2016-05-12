public void Run()
{
	Logger.ClearAll();
	Default.Navigate( "http://www.qq.com");
	Default.Ready(10);
	
	var listBrs = BrowserManager.Has("listBrs") ? BrowserManager.Get("listBrs") : BrowserManager.Create("listBrs", "about:blank");
	var news = Default.SelectNodes("#top_news>ul>li>a");
	int no = 0;
	foreach(var n in news)
	{
		no++;
		if(no > 3) return;
		var newUrl = n.Attr("href");
		Logger.Log(no.ToString() + " " + newUrl);
		Logger.Log(n.Text());
		listBrs.Navigate(newUrl);
		listBrs.Reset();
		listBrs.Ready(30);
		var comment = listBrs.SelectSingleNode("textarea.textarea-fw.textarea-bf");
		if(!comment.IsEmpty())
		{
			comment.Text("点赞！");
			Logger.Log(comment.Text());
			var btn = listBrs.SelectSingleNode("button:contains(\"发布\")");
			btn.Click();
			Logger.Log("done");
			//listBrs.Reset();
			listBrs.Ready(10);
		}
	}
}