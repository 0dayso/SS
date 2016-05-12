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
	
	using(csv = new CSVFile(fileName))
	{	
		csv.Write("Cate","Name","Url","Buyer","Date");
	
			foreach(var c in cates)
			{
				itemCount = 0;
				if(cateCount >0) 
				{
					//isCateNext = false;
				//	break;
				}
				cateCount ++;
				if(Default.Available == false) break;
				var link = c.Attr("href");
				cate = c.Text();
				csv.Write(cate);
				Logger.Log(link);
				Logger.Log(cate);
				
				cateBrowser.Navigate(link+ "&sort=sale-desc");
				cateBrowser.Ready(160);
								
				var items = cateBrowser.SelectNodes("form#bid-form>ul>li>h3>a");
				itemCount += items.Count();
				while(isCateNext)
				{				
					foreach(var i in items)
					{
						try
						{
							if(Default.Available == false) break;
							Logger.ClearAll();
							cateCount++;
							if(cateCount >100 ) break;
							Name = i.Text();
							Url = i.Attr("href");
							Logger.Log(Name);
							Logger.Log(Url);
							
							detail.Navigate(i.Attr("href"));
							detail.Ready(160);
							Url = detail.Url.ToString();
							csv.Write("",Name,Url);
							
							while(isUserNext)
							{
								qc = detail.SelectSingleNode("a:contains(\"评价详情\")");
								if(!qc.IsEmpty())
								{
									qc.Click();
									detail.Ready(160);
									
									qcs = detail.SelectNodes("li.tb-r-review");
									foreach(var line in qcs)
									{
										if(Default.Available == false) break;
										buyer = line.SelectSingleNode("div.tb-r-buyer>div>a>span").Text();					
										date = line.SelectSingleNode("span.tb-r-date").Text();
										Logger.Log(buyer  + date );
										csv.Write("","","",buyer,date);
									}
								}
								else
								{
									Logger.Log("Tmall");
									qc = detail.SelectSingleNode("a:contains(\"累计评价\")");
									qc.Click();
									detail.Ready(160);
							
									qcs = Default.SelectNodes("td.buyer>p");
									qc1s = Default.SelectNodes("td.time");
									
									for(int j=0;j< qcs.Count;j++)
									{
										buyer = qcs[j].Text();
										date = qc1s[j].Text();
										Logger.Log(buyer + " " + date);
										csv.Write("","","",buyer,date);
									}
								}
								
								qc = detail.SelectSingleNode("a.page-next");
								if(!qc.IsEmpty())
								{
									qc.Click();
									detail.Ready(60);
									Logger.Log("next user page");
								}
								else
								{
									isUserNext = false;
									Logger.Log("no user next page");
								}
							}
						}
						catch(Exception e)
						{
							Logger.Log(e.Message);
						}
					}				
								
					qc = cateBrowser.SelectSingleNode("a.page-next:contains(\"下一页\")");
					if(itemCount > 100)
					{
						isCateNext = false;
					}
					else if(qc.IsEmpty() ==  false)
					{
						qc.Click();
						cateBrowser.Ready(60);
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