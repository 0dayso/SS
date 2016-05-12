
public void Run()
{                
	//add your codes here:
	Logger.ClearAll();	
	
	using(csv = new CSVFile(fileName))
	{	
		csv.Write("Title","Price","Url");
		foreach(string skey in keys)
		{
			key = skey;
			SearchZhubajie();
			SearchCSTO();
			SearchBuywit();
			SearchTaskcn();
			Search680();					
			SearchEPweike();
		}
	}
	
	System.Diagnostics.Process.Start(fileName);
}

string[] keys = {"采集","抓取","搜索"};
JQueryContext nTitle,nInput,nBtn,nPrice,nStatus;		
List<JQueryContext> nLists;
string title = string.Empty;
string price = string.Empty;
string url = string.Empty;	
CSVFile csv;
string key = string.Empty;
int timeout = 60;
string fileName = @"..\caiji" + DateTime.Now.ToString("yyyyMMddhhmm") + ".csv";

private void SearchBuywit()
{		
	try
	{
		//**************Taskcn*******************	
		Logger.Log("");	
		Logger.Log("searching " + key + " 百脑汇(buywit.cn)");
		Default.Navigate("http://www.buywit.cn/index.php/task/?keys=" + GetUtf8(key) + "&ccid=1");
		Default.Ready(timeout);	
		nLists = Default.SelectNodes("div.data>dl");
		foreach(var nList in nLists)
		{
			nTitle = nList.SelectSingleNode("dd:eq(1)>a");
			if(nTitle.IsEmpty())continue;
			nStatus = nList.SelectSingleNode("dd:last");
			if(nStatus.Text().Contains("-")|| string.IsNullOrEmpty(nStatus.Text()) )continue;
			nPrice = nList.SelectSingleNode("dd:eq(3)");
			title = nTitle.Text();
			title = FormatContent(title);
			url = nTitle.Attr("href");
			price = nPrice.Text().Replace(",","");
			//Logger.Log("");						
			Logger.Log(title + "  " + price);
			
			csv.Write(title,price,url);
		}
	}
	catch(Exception e)
	{
		Logger.Log(e.Message);
	}
}

private void SearchTaskcn()
{		
	try
	{
		//**************Taskcn*******************	
		Logger.Log("");	
		Logger.Log("searching  " + key + "  任务中国(taskcn)");
		Default.Navigate("http://weike.taskcn.com/list/index/?state=all&keyword=" + GetUtf8(key) + "&r=");
		Default.Ready(timeout);	
		nLists = Default.SelectNodes("tbody>tr");
		foreach(var nList in nLists)
		{
			nTitle = nList.SelectSingleNode("td:eq(1)>a");
			if(nTitle.IsEmpty())continue;
			nStatus = nList.SelectSingleNode("td:eq(3)");
			if(nStatus.Text().Contains("结束") )continue;
			nPrice = nList.SelectSingleNode("td:eq(4)");
			title = nTitle.Text();
			title = FormatContent(title);
			url = nTitle.Attr("href");
			price = nPrice.Text().Replace(",","");
			//Logger.Log("");						
			Logger.Log(title + "  " + price);
			
			csv.Write(title,price,url);
		}
	}
	catch(Exception e)
	{
		Logger.Log(e.Message);
	}
}

private void Search680()
{		
	try
	{
		//**************680*******************	
		Logger.Log("");	
		Logger.Log("searching  " + key + " 时间财富/威客中国(680)");
		Default.Navigate("http://www.680.com");
		Default.Ready(timeout);
		nInput = Default.SelectSingleNode("#keyword");
		nInput.Attr("value",key);
		nBtn = Default.SelectSingleNode("input.sbtn");
		if(!nBtn.IsEmpty()) nBtn.Click();
		Default.Ready("div.mbr-list",timeout);		
		nLists = Default.SelectNodes("div.mbr-list>ul");
		foreach(var nList in nLists)
		{
			nTitle = nList.SelectSingleNode("li:eq(1)>a");
			if(nTitle.IsEmpty())continue;
			nStatus = nList.SelectSingleNode("li:last");
			if(nStatus.Text().Contains("已到期") || nStatus.Text().Contains("已圆满结束"))continue;
			nPrice = nList.SelectSingleNode("li:eq(2)");
			title = nTitle.Text();
			title = FormatContent(title);
			url = nTitle.Attr("href");
			price = nPrice.Text().Replace(",","");
			//Logger.Log("");						
			Logger.Log(title + "  " + price);
			
			csv.Write(title,price,url);
		}
	}
	catch(Exception e)
	{
		Logger.Log(e.Message);
	}
}

private void SearchEPweike()
{
	try
	{
		Logger.Log("");	
		Logger.Log("searching  " + key + " 一品威客(epweike)");
		Default.Navigate("http://www.epweike.com/index.php?do=task_list&k=" + GetUtf8(key));//%E9%87%87%E9%9B%86");
		Default.Ready(timeout);		
		nLists = Default.SelectNodes("div.list>dl>dd");
		foreach(var nList in nLists)
		{
			nTitle = nList.SelectSingleNode("ul>li>p:eq(1)>a");
			if(nTitle.IsEmpty())continue;
			nStatus = nList.SelectSingleNode("ul>li:eq(2)>p:last");
			if(nStatus.Text().Contains("投稿已截止"))continue;
			nPrice = nList.SelectSingleNode("ul>li>p:eq(0)");
			title = nTitle.Text();
			title = FormatContent(title);
			url = nTitle.Attr("href");
			price = nPrice.Text().Replace(",","");
			//Logger.Log("");						
			Logger.Log(title + "  " + price );
			
			csv.Write(title,price,url);
		}
	}
	catch(Exception e)
	{
		Logger.Log(e.Message);
	}
}

private void SearchZhubajie()
{
	try
	{
		Logger.Log("");	
		Logger.Log("searching  " + key + " 猪八戒(zhubajie)");
		Default.Navigate("http://search.zhubajie.com/?qa=" + GetUtf8(key));//%E9%87%87%E9%9B%86");
		//Default.Navigate("http://search.zhubajie.com/t-p4.html?qa=%E9%87%87%E9%9B%86");
		Default.Ready(timeout);
	//	nInput = Default.SelectSingleNode("input.text");
	//	nInput.Attr("value",key);
	//	nBtn = Default.SelectSingleNode("button.button");
	//	if(!nBtn.IsEmpty()) nBtn.Click();
	//	Default.Ready("table.list-tit",timeout);		
		nLists = Default.SelectNodes("table.list-tit>tbody>tr");
		foreach(var nList in nLists)
		{
			nTitle = nList.SelectSingleNode("td:eq(1)>a");
			if(nTitle.IsEmpty())continue;
			nStatus = nList.SelectSingleNode("td:eq(4)");
			if(nStatus.Text().Contains("已截止"))continue;
			nPrice = nList.SelectSingleNode("td>u");
			title = nTitle.Text();
			title = FormatContent(title);
			url = nTitle.Attr("href");
			price = nPrice.Text().Replace(",","");
			//Logger.Log("");						
			Logger.Log(title + "  " + price);
			
			csv.Write(title,price,url);
		}
	}
	catch(Exception e)
	{
		Logger.Log(e.Message);
	}
}

private void SearchCSTO()
{
	try
	{
		Logger.Log("");
		Logger.Log("searching " + key + "  csto");
		Default.Navigate("http://www.csto.com/home");
		Default.Ready(timeout);
		nInput = Default.SelectSingleNode("input.input_txt");
		nInput.Attr("value",key);
		nBtn = Default.SelectSingleNode("input.input_btn");
		nBtn.Click();
		Default.Ready("div.options",timeout);
				
		nLists = Default.SelectNodes("div.item_list>dl");
		foreach(var nList in nLists)
		{
			nTitle = nList.SelectSingleNode("dt>span.title>a");
			//var nContent = nList.SelectSingleNode("dt>span.content>p");
			if(nTitle.IsEmpty())continue;
			nPrice = nList.SelectSingleNode("dd>span.price");			
			nStatus = nPrice.Next();
			if(nStatus.Text().Contains("竞标结束") || nStatus.Text().Contains("关闭"))continue;
			title = nTitle.Text();
			title = FormatContent(title);
			url = nTitle.Attr("href");
			//string content = nContent.Text();
			//content = FormatContent(content);
			price = nPrice.Text().Replace(",","");
			//Logger.Log("");			
			Logger.Log(title + "  " + price);
			
			csv.Write(title,price,url);
		}
	}
	catch(Exception e)
	{
		Logger.Log(e.Message);
	}
}

private string FormatContent(string content)
{
	if(string.IsNullOrEmpty(content))
	{
		return string.Empty;
	}
	return content.Replace(',', '.').Replace('\r', ' ').Replace('\n', ' ');
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