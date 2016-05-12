int m_Timeout = 60;
string m_RootDir = @"C:\Users\GDTAdmin\Desktop\采集宝\makepolo_data\";

public void Run()
{
	Logger.ClearAll();
	//add your codes here:
	List<string> urls = new List<string>(new string[] {

"http://china.makepolo.com/scc_123001000.html",
"http://china.makepolo.com/scc_123002000.html",
"http://china.makepolo.com/scc_123003000.html",
"http://china.makepolo.com/scc_123004000.html",
"http://china.makepolo.com/scc_123005000.html",
"http://china.makepolo.com/scc_123009000.html",
"http://china.makepolo.com/scc_123010000.html",
"http://china.makepolo.com/scc_123011000.html",
"http://china.makepolo.com/scc_123012000.html",
"http://china.makepolo.com/scc_123013000.html",
"http://china.makepolo.com/scc_123014000.html",
"http://china.makepolo.com/scc_123015000.html",
"http://china.makepolo.com/scc_123016000.html",
"http://china.makepolo.com/scc_123017000.html",
"http://china.makepolo.com/scc_123018000.html",
"http://china.makepolo.com/scc_123019000.html",
"http://china.makepolo.com/scc_123020000.html",
"http://china.makepolo.com/scc_123021000.html",
"http://china.makepolo.com/scc_123022000.html",
"http://china.makepolo.com/scc_123023000.html",
"http://china.makepolo.com/scc_123024000.html",
"http://china.makepolo.com/scc_123025000.html",
"http://china.makepolo.com/scc_123026000.html",
"http://china.makepolo.com/scc_123027000.html",
"http://china.makepolo.com/scc_123028000.html",
"http://china.makepolo.com/scc_123030000.html",
"http://china.makepolo.com/scc_123031000.html",
"http://china.makepolo.com/scc_123032000.html",
"http://china.makepolo.com/scc_123033000.html",
"http://china.makepolo.com/scc_123035000.html",
"http://china.makepolo.com/scc_123036000.html",
"http://china.makepolo.com/scc_123037000.html",
"http://china.makepolo.com/scc_127001000.html",
"http://china.makepolo.com/scc_127002000.html",
"http://china.makepolo.com/scc_127003000.html",
"http://china.makepolo.com/scc_127004000.html",
"http://china.makepolo.com/scc_127005000.html",
"http://china.makepolo.com/scc_127006000.html",
"http://china.makepolo.com/scc_127007000.html",
"http://china.makepolo.com/scc_127008000.html",
"http://china.makepolo.com/scc_127009000.html",
"http://china.makepolo.com/scc_127010000.html",
"http://china.makepolo.com/scc_127011000.html",
"http://china.makepolo.com/scc_127012000.html",
"http://china.makepolo.com/scc_127013000.html",
"http://china.makepolo.com/scc_127014000.html",
"http://china.makepolo.com/scc_127015000.html",
"http://china.makepolo.com/scc_127018000.html",
"http://china.makepolo.com/scc_127023000.html",
"http://china.makepolo.com/scc_127024000.html",
"http://china.makepolo.com/scc_127025000.html",
"http://china.makepolo.com/scc_127026000.html",
"http://china.makepolo.com/scc_127027000.html",
"http://china.makepolo.com/scc_127028000.html",
"http://china.makepolo.com/scc_127029000.html",
"http://china.makepolo.com/scc_127030000.html",
"http://china.makepolo.com/scc_127031000.html",
"http://china.makepolo.com/scc_127032000.html",
"http://china.makepolo.com/scc_127033000.html",
"http://china.makepolo.com/scc_127034000.html",
"http://china.makepolo.com/scc_127035000.html",
"http://china.makepolo.com/scc_104001000.html",
"http://china.makepolo.com/scc_104002000.html",
"http://china.makepolo.com/scc_104003000.html",
"http://china.makepolo.com/scc_104004000.html",
"http://china.makepolo.com/scc_104005000.html",
"http://china.makepolo.com/scc_104006000.html",
"http://china.makepolo.com/scc_104007000.html",
"http://china.makepolo.com/scc_104008000.html",
"http://china.makepolo.com/scc_104009000.html",
"http://china.makepolo.com/scc_104010000.html",
"http://china.makepolo.com/scc_104011000.html",
"http://china.makepolo.com/scc_104012000.html",
"http://china.makepolo.com/scc_104013000.html",
"http://china.makepolo.com/scc_104014000.html",
"http://china.makepolo.com/scc_104015000.html",
"http://china.makepolo.com/scc_104016000.html",
"http://china.makepolo.com/scc_104017000.html",
"http://china.makepolo.com/scc_104018000.html",
"http://china.makepolo.com/scc_104019000.html",
"http://china.makepolo.com/scc_104020000.html",
"http://china.makepolo.com/scc_104021000.html",
"http://china.makepolo.com/scc_104022000.html",
"http://china.makepolo.com/scc_104023000.html",
"http://china.makepolo.com/scc_104024000.html",
"http://china.makepolo.com/scc_104025000.html",
"http://china.makepolo.com/scc_104026000.html",
"http://china.makepolo.com/scc_104027000.html",
"http://china.makepolo.com/scc_104028000.html",
"http://china.makepolo.com/scc_104029000.html",
"http://china.makepolo.com/scc_104030000.html",
"http://china.makepolo.com/scc_104031000.html",
"http://china.makepolo.com/scc_104032000.html",
"http://china.makepolo.com/scc_104033000.html",
"http://china.makepolo.com/scc_104035000.html",
"http://china.makepolo.com/scc_104036000.html",
"http://china.makepolo.com/scc_104037000.html",
"http://china.makepolo.com/scc_104038000.html",
"http://china.makepolo.com/scc_104039000.html",
"http://china.makepolo.com/scc_104040000.html",
"http://china.makepolo.com/scc_117001000.html",
"http://china.makepolo.com/scc_117002000.html",
"http://china.makepolo.com/scc_117003000.html",
"http://china.makepolo.com/scc_117004000.html",
"http://china.makepolo.com/scc_117005000.html",
"http://china.makepolo.com/scc_117006000.html",
"http://china.makepolo.com/scc_117007000.html",
"http://china.makepolo.com/scc_117008000.html",
"http://china.makepolo.com/scc_117009000.html",
"http://china.makepolo.com/scc_117010000.html",
"http://china.makepolo.com/scc_117011000.html",
"http://china.makepolo.com/scc_117012000.html"
	});
	urls.Reverse();
	foreach(var url in urls)
	{
		StringHelper.WriteString(AppDomain.CurrentDomain.BaseDirectory + "continue.txt", url);
		Default.Navigate(url);
		Default.Ready(m_Timeout);
		var hubei = Default.SelectSingleNode("A:contains(\"湖北\")");
		hubei.Click();
		Default.Ready(m_Timeout);
		do
		{
			try
			{
				var companyList = Default.SelectNodes("div.company_list>div.product_list>div.product_top>strong>a.plistk");
				foreach(var company in companyList)
				{
					try
					{
						string companyName = company.Text();
						if(this.IsCompanyExists(companyName))
						{
							Logger.Log(string.Format("{0} 公司已经存在, 跳过.", companyName));
							continue;
						}
						string companyUrl = company.Attr("href");
						this.ProcCompany(companyName, companyUrl);
					}
					catch(Exception ex)
					{
						Logger.Log(string.Format("处理公司时出错: {0}", ex.Message));
					}
				}
				var nextPage = Default.SelectSingleNode("A:contains(\"下一页\")");
				if(nextPage.IsEmpty())
				{
					break;
				}
				nextPage.Click();
				Default.Ready(m_Timeout);
			}
			catch(Exception ex1)
			{
				Logger.Log(string.Format("处理公司时出错: {0}", ex1.Message));
			}
		}
		while(Default.Available);
	}
}

public void ProcCompany(string companyName, string url)
{
	JQueryBrowser b = Browsers.Has("公司") ? Browsers.Get("公司") : Browsers.Create("公司", "about:blank");
	b.Navigate(url);
	b.Ready(m_Timeout);
	var contactPage = b.SelectSingleNode("a:contains(\"公司信息\")");
	contactPage.Click();
	b.Ready(m_Timeout);
	var companyType = b.SelectSingleNode("#companyParameter").SelectSingleNode("TBODY:eq(0)>TR:eq(7)>TD:eq(1)");
	var address = b.SelectSingleNode("#companyParameter").SelectSingleNode("TBODY:eq(0)>TR:eq(1)>TD:eq(1)");
	var company = b.SelectSingleNode("#companyParameter").SelectSingleNode("TBODY:eq(0)>TR:eq(0)>TD:eq(1)");
	var legalPerson = b.SelectSingleNode("#companyParameter").SelectSingleNode("TBODY:eq(0)>TR:eq(0)>TD:eq(3)");
	var fax = b.SelectSingleNode("#companyParameter").SelectSingleNode("TBODY:eq(0)>TR:eq(10)>TD:eq(3)");
	var website = b.SelectSingleNode("#companyParameter").SelectSingleNode("TBODY:eq(0)>TR:eq(9)>TD:eq(3)");
	var products = b.SelectSingleNode("#companyParameter").SelectSingleNode("TBODY:eq(0)>TR:eq(8)>TD:eq(1)");
	var desc = b.SelectSingleNode("div.cintroduct2");
	var logo = b.SelectSingleNode("img.img160x160");
	this.OutputCompanyData(
		company.Text(),
		companyType.Text(),
		address.Text(),
		fax.Text(), //phone,
		"", //fax,
		"", //mobile
		legalPerson.Text(), //contactPerson
		website.Text(),
		"", //mail
		logo.Attr("src"),
		desc.Text(),
		products.Text(),
		"" //zip
	);
	
	var productPage = b.SelectSingleNode("a:contains(\"产品中心\")");
	productPage.Click();
	b.Ready(m_Timeout);

	List<string> cateUrls = new List<string>();
	List<string> cateNames = new List<string>();
	var cateList = b.SelectNodes("li.l_ss>a");
	foreach(var cate in cateList)
	{
		cateNames.Add(cate.Text());
		cateUrls.Add(cate.Attr("href"));
	}
	
	int count = 0;
	for(int i = 0; i < cateUrls.Count; i++)
	{
		string cateName = cateNames[i];
		string cateUrl = cateUrls[i];
		
		b.Navigate(cateUrl);
		b.Ready(m_Timeout);
		do
		{
			var productList = b.SelectNodes("div.imgtitle1>a");
			foreach(var product in productList)
			{
				try
				{
					count +=1;
					if(count > 100 ) break;
					string productName = product.Text();
					string productUrl = product.Attr("href");
					this.ProcProduct(companyName, cateName, productUrl);
				}
				catch(Exception ex)
				{
					Logger.Log(string.Format("处理产品时出错: {0}", ex.Message));
				}
			}
			var nextPage = b.SelectSingleNode("a:contains(\"下一页\")");
			if(nextPage.IsEmpty())
			{
				break;
			}
			nextPage.Click();
			b.Ready(m_Timeout);
		}
		while(b.Available);
	}
}

public void ProcProduct(string companyName, string cateName, string url)
{
	JQueryBrowser b = Browsers.Has("产品") ? Browsers.Get("产品") : Browsers.Create("产品", "about:blank");
	b.Navigate(url);
	b.Ready(m_Timeout);
	var productName = b.SelectSingleNode("#textfeld1>h1");
	var content = b.SelectSingleNode("div.mcontenk0");
	var pic = b.SelectSingleNode("div.imgborder270");
	this.OutputProductData(
		companyName,
		productName.Text(),
		cateName,
		"", //price
		content.Html(),
		pic.IsEmpty() ? "" : pic.Css("'background-image'").Replace("url(\"", "").Replace("\")", "").Trim()
	);
}

public void OutputCompanyData(
	string companyName, 
	string companyType,
	string address,
	string phone,
	string fax,
	string mobile,
	string contactPerson,
	string website,
	string mail,
	string logoUrl,
	string desc,
	string products,
	string zip
)
{
	//lock(this)
	//{
		Logger.Log("CompanyName: " + companyName);
		Logger.Log("--- --- --- ---");
        string dir = m_RootDir + companyName;
		Directory.CreateDirectory(dir);
		using(StreamWriter sw = new StreamWriter(dir + @"\company.txt"))
		{
			sw.WriteLine("企业分类|企业所在地|企业全称|联系人|电话|邮箱|网址|介绍|经营范围|企业LOGO");
			sw.WriteLine(string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}",
				companyType,
				address,
				companyName,
				contactPerson,
				(phone + "/" + mobile).Replace("[免费通话未开通-如何开通?]", ""),
				mail,
				website,
				desc,
				products,
				logoUrl));
		}
	//}
}

public void OutputProductData(
	string companyName,
	string productName,
	string productType,
	string price,
	string descHtml,
	params string[] picUrls
)
{
	//lock(this)
	//{
		Logger.Log("	CompanyName: " + companyName);
		Logger.Log("	ProductName: " + productName);
		Logger.Log("	Html: " + descHtml.Length.ToString());
		Logger.Log("--- --- --- ---");
		string dir = m_RootDir + companyName + @"\" + productName;
		Directory.CreateDirectory(dir);
		using(StreamWriter sw = new StreamWriter(dir + @"\product.txt"))
		{
			sw.WriteLine("产品分类|产品名称|价格|产品介绍HTML");
			sw.WriteLine(string.Format("{0}|{1}|{2}|{3}",
				productType,
				productName,
				price.Replace("单价：", "").Replace("[联系方式]", "").Replace("[如何开通品质保障?]", ""),
				descHtml
			));
		}
		int i = 0;
		foreach(var item in picUrls)
		{
			if(!string.IsNullOrEmpty(item) && item != "System.__ComObject")
			{
				string path = dir + @"\" + (i++).ToString() + ".jpg";
				Result.Download(item, path);
			}
		}
	//}
}

public bool IsCompanyExists(string companyName)
{
	string dir = m_RootDir + companyName;
	return Directory.Exists(dir);
}