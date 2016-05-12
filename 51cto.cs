public void Run()
{
	//add your codes here:
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