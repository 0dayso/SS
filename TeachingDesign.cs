public void Run()
{
	//add your codes here:
	Default.Available = true;
	JQueryBrowser Design = Browsers.Has("Design")?Browsers.Get("Design"):Browsers.Create("Design","about:blank");
	Design.Available = true;
	Default.Navigate("http://club.jledu.gov.cn/bbs/forumdisplay.php?fid=68");
	Default.Ready();
	
	while(true)
	{				
		var designList = Default.SelectNodes("table.datatable>tbody");
		foreach(var design in designList)
		{
			if(Default.Available == false) break;
			var url = design.SelectSingleNode("tr>th>span>a");
			if(!url.IsEmpty())
			{
				string title = url.Text();
				string link = url.Attr("href");
				Design.Navigate(link);
				Design.Ready();
				
				var body = Design.SelectSingleNode("td.t_msgfont");
				if(!body.IsEmpty())
				{
					var date = body.SelectSingleNode("div.authorinfo>em");
					string sDate = Regex.Match(date,"\d{4}-\d{1,2}-\d{1,2}").Value;
					DateTime dt = Convert.ToDateTime(sDate);
					InsertData(title, body.Text(),link,dt);
				}
			}
		}
		
		var next = Default.SelectSingleNode("a.next");
		if(!next.IsEmpty())
		{
			var nextUrl = next.Attr("href");
			Logger.Log("next url: " + nextUrl);
			Default.Navigate(nextUrl);
			Default.Ready();
			Thread.Sleep(10*1000);
		}
	}
}

string connectionString = "Data Source=.;Initial Catalog=WQXDemo;Pooling=True;user=sa;password=!qaz2wsx3edc";
private void InsertData(string title,string body,string link, DateTime date)
{
	try
	{
		string sInsert = string.Format("insert into wqxdemo.dbo.TeachingDesign (Title,Body,Link,Date) values(N'{0}',N'{1}',N'{2}',N'{3}')", title,body,link,date);
		using(SqlConnection conn = new SqlConnection(connectionString))
		{
			conn.Open();
			using(SqlCommand cmd = new SqlCommand(sInsert,conn))
			{
				cmd.ExecuteNonQuery();
			}
		}
	}
	catch(Exception e)
	{
		Logger.Log(e.Message);
	}
}