public void Run()
{
	//add your codes here:	
	JQueryBrowser brwDetail = Browsers.Has("detail") ? Browsers.Get("detail") : Browsers.Create("detail","about:blank");
	JQueryBrowser brwBlog = Browsers.Has("Blog") ? Browsers.Get("Blog") : Browsers.Create("Blog","about:blank");

	Logger.ClearAll();
     
	sUrl = "http://www.cnblogs.com/iamzyf/archive/2008/01/02/1023327.html";
	brwBlog.Navigate(sUrl);
	brwBlog.Ready(10);
		
	sTitle = brwBlog.Document.GetElementById("cb_post_title_url").GetAttribute("innertext");
	sHtml = brwBlog.Document.GetElementById("cnblogs_post_body").GetAttribute("OuterHtml");
	Logger.Log(sTitle);
	//Logger.Log(sHtml);
	
	using(CSVFile csv = new CSVFile(@"c:\awen\data\cnblogs.csv"))
	{	
		//WriteCSDN(sTitle, sHtml);
		//WriteBlogBus(sTitle, sHtml);
		//WriteSina(sTitle, sHtml);
		//WriteSohu(sTitle, sHtml);
		//Write163(sTitle, sHtml);
		//return;
		
		HtmlElementCollection urls = brwBlog.ChooseNodes("#cnblogs_post_body>div>table>tbody>tr>td>p:eq(1)>a");
		for(int i = 1; i < urls.Count; i++) //HtmlElement url in urls)
		{
			if(!Default.Available)break;
			sUrl = urls[i].GetAttribute("href");
			sTitle = urls[i].GetAttribute("innertext");
			
			brwDetail.Navigate(sUrl);
			brwDetail.Ready(10);
			
			sTitle = brwDetail.Document.GetElementById("cb_post_title_url").GetAttribute("innertext");
			sHtml = brwDetail.Document.GetElementById("cnblogs_post_body").GetAttribute("OuterHtml");
			
			Logger.Log(sTitle);
			//Logger.Log(sHtml.Replace("\"","\\\""));
			//csv.Write(sTitle, sHtml, sUrl);
			
			//WriteCSDN(sTitle, sHtml);
			//WriteSina(sTitle, sHtml);
			//WriteBlogBus(sTitle, sHtml);
			WriteSohu(sTitle, sHtml);
			//Write163(sTitle, sHtml);
		}
	}
}

	string sUrl, sTitle,sHtml,sTag;
	JQueryContext jc;
	
private void Write163(string sTitle,string sHtml)
{
	Default.Navigate("http://gdtsearch.blog.163.com/blog/getBlog.do?fromString=blogmodule");
	Default.Ready(120);
	
	jc = Default.SelectSingleNode("input.ztag");
	jc.Attr("value",sTitle);
	
	jc = Default.SelectSingleNode("div.zebx.ztag>iframe");	
	jc.SetFrameBody(sHtml.Replace("\n","<br>"));
		
	Default.SelectSingleNode(".act>input.nbtn:eq(0)").Click();
	Default.Ready(200);
	//Thread.Sleep(15000);
}

private void WriteBlogBus(string sTitle,string sHtml)
{
	Default.Navigate("http://blog.home.blogbus.com/posts/form/");
	Default.Ready(120);
	
	jc = Default.SelectSingleNode("input.text");
	jc.Attr("value",sTitle);
	
	jc = Default.SelectSingleNode("#content_ifr");	
	jc.SetFrameBody(sHtml.Replace("\n","<br>"));
		
	Default.SelectSingleNode("#btn_pub").Click();
	Default.Ready(200);
	//Thread.Sleep(15000);
}

private void WriteCSDN(string sTitle,string sHtml)
{
	//JQueryBrowser brwBlog = Browsers.Has("Blog") ? Browsers.Get("Blog") : Browsers.Create("Blog","about:blank");

	Default.Navigate("http://write.blog.csdn.net/postedit");
	Default.Ready(30);
	
	jc = Default.SelectSingleNode("#selType");
	jc.Attr("value","1");
	
	jc = Default.SelectSingleNode("#txtTitle");
	jc.Attr("value",sTitle);
	
	jc = Default.SelectSingleNode("#xhe0_iframe");	
	//jc.GetFrameBody();
	jc.SetFrameBody(sHtml.Replace("\n","<br>"));
		
	Default.SelectSingleNode("#btnPublish").Click();
	Default.Ready(200);
}

private void Write51CTO(string sTitle, string sHtml)
{
	Default.Navigate("http://awenhu.blog.51cto.com/addblog.php");
	Default.Ready();
	
	var title = Default.SelectSingleNode("#atc_title");
	title.Attr("value","title");
	//var html = Default.SelectSingleNode("#edui4_body");
	//html.Click();
	
	var frm = Default.SelectSingleNode("#baidu_editor_0>body");
	frm.Attr("value","tesst");
	
	var tag = Default.SelectSingleNode("#tags");
	tag.Attr("tag");
	var c = Default.SelectSingleNode("HTML:eq(0)>BODY:eq(0)>DIV:eq(10)");
	c.Attr("value","test content");
	var content = Default.SelectSingleNode("#baidu_editor_0");
	content.Attr("value","content");
	var tj = Default.SelectSingleNode("#tjj");
	tj.Click();
}
	
private void WriteSina(string sTitle,string sHtml)
{
	//Default.Navigate("http://control.blog.sina.com.cn/admin/article/article_add.php");
	//Default.Ready(10);
	//jc = brwBlog.SelectSingleNode("#SinaEditor_Iframe>iframe");	
	//jc.SetFrameBody("test");
	//return;
	//	JQueryBrowser brwBlog = Browsers.Has("Blog") ? Browsers.Get("Blog") : Browsers.Create("Blog","about:blank");

       
//	brwBlog.Navigate("blog.sina.com.cn");
//	brwBlog.Ready(10);
//	var user = brwBlog.SelectSingleNode("#userNickName");
//	if(user.IsEmpty())
//	{
//		var un = brwBlog.SelectSingleNode("#loginName");
//		var pw = brwBlog.SelectSingleNode("#loginPass");
//		var btn = brwBlog.SelectSingleNode("#loginButton");
//		un.Attr("value","121285904@qq.com");
//		pw.Attr("value","fsdfsdf,.");
//		btn.Click();
//		brwBlog.Ready(10);
//	}

	Default.Navigate("http://control.blog.sina.com.cn/admin/article/article_add.php?index");
	Default.Ready(120);
	
	jc = Default.SelectSingleNode("#articleTitle");
	jc.Attr("value",sTitle);
	
	jc = Default.SelectSingleNode("#SinaEditor_Iframe>iframe");	
	jc.SetFrameBody(sHtml.Replace("\n","<br>"));
	
	//HtmlElement he = Default.Document.GetElementById("#SinaEditor_Iframe").GetElementsByTagName("body")[0];
    //he.InnerText = sHtml;
	
	var sort = Default.SelectSingleNode("#sort_id_20_113");
	if(!sort.IsEmpty())sort.Click();
	
	Default.SelectSingleNode("#articlePostBtn").Click();
	Default.Ready(200);
	//Thread.Sleep(1000 * 100);
}

private void WriteSohu(string sTitle,string sHtml)
{
	Default.Navigate("http://blog.sohu.com/manage/entry.do?m=add&t=index");
	Default.Ready(120);
	
	jc = Default.SelectSingleNode("#entrytitle");
	jc.Attr("value",sTitle);
	
	jc = Default.SelectSingleNode("#ifrEditorContainer>div>iframe");	
	jc.SetFrameBody(sHtml.Replace("\n","<br>"));
	
	Default.SelectSingleNode("#save").Click();
	Default.Ready(200);
	//Thread.Sleep(1000 * 100);
}