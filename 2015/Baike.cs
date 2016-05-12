public void Run()
{
	//add your codes here:
	Logger.ClearAll();
	List<string> keys =new List<string>(){ "匀速直线运动","力的三要素","二力平衡","牛顿第一定律","密度","压强","阿基米德原理","功率","光的反射定律","热量"};
	
	foreach(string key in keys)
	{
		Default.Navigate("baike.baidu.com");
		Default.Ready(60);
		
		var input = Default.SelectSingleNode("input#word");
		if(input.IsEmpty()) return;
		input.Attr("value",key);
		
		var btn = Default.SelectSingleNode("input.s_btn");
		btn.Click();
		Default.Ready(60);
		
		string link = Default.Url.ToString();
		var body = Default.SelectSingleNode("div#lemmaContent-0");
		if(!body.IsEmpty())
		{
			var sBody = body.Html();//.Replace("编辑本段","");
			Logger.Log(key);
			Logger.Log(sBody);
		}
		Logger.Log("");
	}
}
