public void Run()
{
	//add your codes here:
	Logger.ClearAll();
	var brwDetail = Browsers.Has("detail") ? Browsers.Get("detail") : Browsers.Create("detail","about:blank");
	var brwSina = Browsers.Has("sina") ? Browsers.Get("sina") : Browsers.Create("sina","about:blank");
	brwSina.Navigate("blog.sina.com.cn");
	brwSina.Ready(10);
	var user = brwSina.SelectSingleNode("#userNickName");
	if(user.IsEmpty())
	{
		var un = brwSina.SelectSingleNode("#loginName");
		var pw = brwSina.SelectSingleNode("#loginPass");
		var btn = brwSina.SelectSingleNode("#loginButton");
		un.Attr("value","121285904@qq.com");
		pw.Attr("value","fwdd1127,.");
		btn.Click();
		brwSina.Ready(10);
	}
	
	Default.Navigate("http://www.cnblogs.com/iamzyf/archive/2008/01/02/1023327.html");
	Default.Ready(10);
	
	string sUrl, sTitle,sHtml,sTag;
	var urls = Default.SelectNodes("#cnblogs_post_body>div:eq(0)>table>tbody>tr>td>p:eq(1)>a");
	Logger.Log(urls.Count().ToString());
	using(CSVFile csv = new CSVFile(@"c:\cnblogs.csv"))
	{
		foreach(var url in urls)
		{
			sUrl = url.Attr("href");
			Logger.Log(url.Text());
			//Logger.Log(url.Attr("href"));
			
			brwDetail.Navigate(sUrl);
			brwDetail.Ready(10);
			
			// read blog
			var title = brwDetail.SelectSingleNode("#cb_post_title_url");
			sTitle = title.Text();
			Logger.Log(sTitle);
			
			var html = brwDetail.SelectSingleNode("#cnblogs_post_body");
			sHtml = html.Html();
			
			csv.Write(sTitle, sHtml);
		}
	}
}