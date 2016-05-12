public void Run()
{
	string title, pic, link;
	JQueryContext qc = null;
	List<string> keys = new List<string>(){"温度","匀速直线运动","密度","压强","功率","光的反射定律","热量","平均速度","电流","电压"};
	foreach(string key in keys)
	{
		Default.Navigate("http://www.soku.com/t/nisearch/" + GetUtf8(key));
		Default.Ready();
		
		var results = Default.SelectNodes("div#srList>div>div");
		foreach(var result in results)
		{
			if(!Default.Available) return;
			qc = result.SelectSingleNode("div.pic>a");
			if(!qc.IsEmpty())
			{
				title = qc.Attr("title");
				link = qc.Attr("href");
				
				qc = result.SelectSingleNode("div>img");
				pic = qc.Attr("src");
				
				Logger.Log(title + " " + pic);
			}
		}
	}
}

private string GetUtf8(string key)
{
	byte[] buffer = Encoding.GetEncoding("utf-8").GetBytes(key);
    string str = "";

    foreach (byte b in buffer) 
    {
    	str += string.Format("%{0:X}", b);
    }        
   
    return str;
}