public void Run()
{
	//add your codes here:
	 string url = "http://baike.baidu.com/fenlei/%E5%9F%BA%E6%9C%AC%E7%89%A9%E7%90%86%E6%A6%82%E5%BF%B5";
            Default.Navigate(url);
            Default.Ready(60);
            var bDetail = Browsers.Has("Detail") ? Browsers.Get("Detail") : Browsers.Create("Detail","about:blank");

            while (true)
            {
            	if(Default.Available == false) break;
                var titles = Default.SelectNodes("div.grid-list.grid-list-spot>ul>li>div.list>a");
                foreach (var title in titles)
                {
                	if(Default.Available == false) break;
                    string key = title.Text();
                    string link = title.Attr("href");
                    bDetail.Navigate(link);
                    bDetail.Ready(60);
                    var body = bDetail.SelectSingleNode("div#sec-content0");
                    if (!body.IsEmpty())
                    {
                        string sBody = body.Html().Replace("'", "''");

                        string sInsert = string.Format("insert into netSchool.dbo.knowledge (title, body,link) values (N'{0}',N'{1}',N'{2}')", key, sBody, link);
                        //InsertData(sInsert);
                        Logger.Log(key + ": " + link);
                    }
                }

                var next = Default.SelectSingleNode("a#next.next");
                next.Click();
                Default.Ready(60);
            }
}