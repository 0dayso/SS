public void Run()
{
	//add your codes here:
	
	Logger.ClearAll();
	
	string url = "http://s8.taobao.com/search?q=%C5%AE%D7%B0&bcat=16&tab=coefp&style=grid&promoted_service4=4&olu=yes&filterFineness=2&atype=b&pid=mm_12613800_2379417_9178614&&&&&unid=&clk1=3b7d2480ed56931c98b10db5bbf71b17&spm=1.36622.0.107.zvJD6H";
	Default.Navigate(url);
	Default.Ready(160);
	
	var cateBrowser = Browsers.Has("Cate") ? Browsers.Get("Cate") : Browsers.Create("Cate", "about:blank"); // Common.CreateBrowser("Cate");
	var detail = Browsers.Has("Details") ? Browsers.Get("Details") : Browsers.Create("Details", "about:blank"); // Common.CreateBrowser("Cate");
	
	var cates = Default.SelectNodes("#J_RecommendCate>dl>dd<>a");
	bool isCateNext = true;
	bool isUserNext = true;
	int itemCount = 0;
	int cateCount = 0;
	string link;
	
	using(csv = new CSVFile(fileName))
	{	
		csv.Write("Cate","Name","Url","Buyer","Date");
	
			foreach(var c in cates)
			{
				
				itemCount = 0;
				if(cateCount >0) 
				{
					//isCateNext = false;
					//break;
				}
				cateCount ++;
				if(Default.Available == false) break;
				link = c.Attr("href");
				cate = c.Text();
				if(cate != "休闲套装" && cate != "风衣" && cate != "打底裤") continue;
				csv.Write(cate);
				Logger.Log(link);
				Logger.Log(cate);
				
				cateBrowser.Navigate(link+ "&sort=sale-desc");
				cateBrowser.Ready(160);
				isCateNext = true;
				
				while(isCateNext)
				{
					var items = cateBrowser.SelectNodes("form#bid-form>ul>li>h3>a");
					itemCount += items.Count();
								
					foreach(var i in items)
					{
						try
						{
							if(Default.Available == false) break;
							//Logger.ClearAll();
							//cateCount++;
							//if(cateCount >100 ) break;
							Name = i.Text();
							Url = i.Attr("href");
							Logger.Log(Name);
							Logger.Log(Url);
						
							csv.Write("",Name,Url);
							
							
						}
						catch(Exception e)
						{
							Logger.Log(e.Message);
						}
					}				
								
					qc = cateBrowser.SelectSingleNode("a.page-next");
					if(itemCount > 101)
					{
						isCateNext = false;
					}
					else if(qc.IsEmpty() ==  false)
					{
						qc.Click();
						cateBrowser.Ready(60);
						link = cateBrowser.Url.ToString();
						
						Thread.Sleep(2000);
						Logger.Log("next cate page");
					}
					else
					{
						isCateNext = false;
						Logger.Log("no cate next page");
					}
				}
		}
	}
	
	System.Diagnostics.Process.Start(fileName);
}

JQueryContext qc;
List<JQueryContext> qcs;
List<JQueryContext> qc1s;
string cate = string.Empty;
string Name = string.Empty;
string Url = string.Empty;
string buyer = string.Empty;
string date = string.Empty;

CSVFile csv;
string fileName = @"c:\temp\taobao" + DateTime.Now.ToString("yyyyMMddhhmm") + ".csv";