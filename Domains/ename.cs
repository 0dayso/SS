public void Run()
{
	var url = "http://auction.ename.com/tao/buynow?domain=2&domainsld=&transtype=1&sort=2&registrar=0&bidpricestart=1&bidpriceend=&skipword1=&skip_type=0&domaingroup=11&domaintld%5B0%5D=7&tld_type=0&finishtime=0&domainlenstart=1&domainlenend=&skipword2=&groupTwo%5B0%5D=5001&name=&exptime=0";
	Logger.ClearAll();
	Default.Navigate(url);
	Default.Ready(10);
	var nets = Default.SelectNodes("div.tao_box>form>table>tbody>tr");
	Logger.Log(nets.Count());
	int i = 0;
	foreach(var net in nets)
	{
		//Logger.Log(i++);
		var name = net.SelectSingleNode("td:eq(1)>a");
		var register = net.SelectSingleNode("td:eq(3)");
		var price = net.SelectSingleNode("td:eq(5)");
		
		Logger.Log(name.Text().Trim() + " " +　price.Text().Replace("元","") + " " + register.Text()  + " " + name.Attr("href"));
	}
}