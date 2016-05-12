public void Run()
{
		
	Logger.ClearAll();
	JQueryBrowser Explain = Browsers.Has("Explain")?Browsers.Get("Explain"):Browsers.Create("Explain","about:blank");
	Explain.Available = true;
	Default.Navigate("http://www.jyeoo.com/physics/ques/search");
	Default.Ready();
	//add your codes here:
	while(true)
	{
		var version = Default.SelectSingleNode("a.sf-with-ul");
		Logger.Log(version.Text());
		var questions = Default.SelectNodes("div.pt1");
		var answers = Default.SelectNodes("div.pt2");
		var explains = Default.SelectNodes("span.fieldtip");
		for(int i = 0; i < questions.Count; i++)
		{
			Logger.Log(questions[i].Text());
			var selections = answers[i].SelectNodes("td.selectoption");
			foreach(var sel in selections)
			{
				Logger.Log(sel.Text());
				var pic = sel.SelectSingleNode("img");
				if(pic.IsEmpty() == false)
				{
					Logger.Log(pic.Attr("src"));
				}
				
			}
			//Logger.Log("Explain: " + explains[i].SelectSingleNode("a").Attr("href"));
			Explain.Navigate(explains[i].SelectSingleNode("a").Attr("href"));
			Explain.Ready();
			Logger.Log(Explain.SelectSingleNode("div.pt6").Text());
			string sPoint = Explain.SelectSingleNode("div.pt3").Text();
			Logger.Log(sPoint);
			Logger.Log("");			
		}
		var next = Default.SelectSingleNode("p.search>a:contains(\"下一页\")");
		if(next.IsEmpty() == false)
		{
			var href = next.Attr("href");
			Logger.Log("Navigating: " + href);
			Default.Navigate(href);
			Default.Ready();
		}
		else
		{
			return;
		}
	}
	
}


