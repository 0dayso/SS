public void Run()
{
	string folder = @"c:\webcast";
    if(!Directory.Exists(folder))Directory.CreateDirectory(folder);
	Logger.ClearAll();
	//add your codes here:
	Default.Navigate(url);
	Default.Ready();
	List<string> titleList = new List<string>();
	var titles = Default.SelectNodes("a.msft");
	foreach(var title in titles)
	{
		titleList.Add(title.Text());
	}
	var nodes = Default.SelectNodes("a");
	Logger.Log(nodes.Count.ToString());
	int i =0;
	foreach(var node in nodes)
	{
		string text = node.Text();
		if(text == "WMV")
		{
			i++;
			string link = node.Attr("href");
			Logger.Log(link);
			System.Net.WebClient client = new System.Net.WebClient();
			client.DownloadFile(link,folder + titleList[i] + ".wmv");
		}
	}
}

string url = "http://msdnwebcast.net/webcast/5/2012/";