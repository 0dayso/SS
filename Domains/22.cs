public void Run()
{
	var url = "http://am.22.cn/ykj/?t=0.8370669174473733&ddlSuf=.net&stype=32&rekeyword1=a,e,i,o,u,v&ddlclass=11&registrar=0&chkorder=1&chkday=-1&position=&position1=&position2=&MinPrice=0&MaxPrice=&selMinLen=1&selMaxLen=200&dealtype=2&keytype=0&issch=1&showtype=0";
	Logger.ClearAll();
	Default.Navigate(url);
	Default.Ready("#buynow_list>tr>td>a",10);	//check if load successfully
	var nets = Default.SelectNodes("#buynow_list>tr");
	while(nets.Count()<10) nets = Default.SelectNodes("#buynow_list>tr");
	Logger.Log(nets.Count());
	int i = 0;
	foreach(var net in nets)
	{
		var name = net.SelectSingleNode("td:eq(0)>a:eq(1)");
		//Logger.Log(name.Text()); //Logger.Log(name.Attr("href"));
		var register = net.SelectSingleNode("td:eq(2)");
		var price = net.SelectSingleNode("td:eq(4)");
		
		Logger.Log(name.Text().Trim() + " " +　price.Text().Replace("￥","") + " " + register.Text()  + " " + name.Attr("href"));
	}
}