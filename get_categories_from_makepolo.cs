public void Run()
{
	Logger.ClearAll();
	//add your codes here:
	var dtl = Browsers.Create("Details", "about:blank");
	var cateList = Default.SelectNodes("td.p5>table>tbody>tr>td.px14>a");
	foreach(var cate in cateList)
	{
		string url = cate.Attr("href");
		dtl.Navigate(url);
		dtl.Ready();
		var sorter_content = dtl.SelectSingleNode("td.sorter_left_content");
		string content = sorter_content.Text();
		var mc = Regex.Matches(content, @"\w+");
		foreach(Match m in mc)
		{
			Logger.Log(m.Value);
		}
	}
}