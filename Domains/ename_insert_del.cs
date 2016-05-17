public void Run()
{
	var url = "http://auction.ename.com/tao/buynow?domain=2&domainsld=&transtype=1&sort=2&registrar=0&bidpricestart=1&bidpriceend=&skipword1=&skip_type=0&domaingroup=11&domaintld%5B0%5D=7&tld_type=0&finishtime=0&domainlenstart=1&domainlenend=&skipword2=&groupTwo%5B0%5D=5001&name=&exptime=0";
	Logger.ClearAll();
	Default.Navigate(url);
	Default.Ready("div.tao_box>form>table>tbody>tr>td>a",10);
	var nets = Default.SelectNodes("div.tao_box>form>table>tbody>tr");
	Logger.Log(nets.Count());
	int i = 0;
	List<Domain> domains = new List<Domain>();
	foreach(var net in nets)
	{
		Domain domain = new Domain();
		var name = net.SelectSingleNode("td:eq(1)>a");
		if(name.IsEmpty())continue;

		domain.Name = name.Text().Trim();
		domain.Desc = net.SelectSingleNode("td:eq(2)").Text().Trim();
		domain.Register = net.SelectSingleNode("td:eq(3)").Text().Trim();
		domain.Price = Int32.Parse( net.SelectSingleNode("td:eq(5)").Text().Replace("元","").Trim());
		domain.LeftTime = net.SelectSingleNode("td:eq(6)").Text().Trim();
		domain.Url = name.Attr("href").Trim();
		domain.RegisterUrl = url;
		domain.Type = "net4s";
		
		domains.Add(domain);
		
		Logger.Log(domain.Name + " " +　domain.Price + " " + domain.Register  + " " + domain.Url);
		Logger.Log(domain.Desc + " " + domain.LeftTime);
	}
	
	Delete("易名中国","net4s");
	InsertData(domains);
}


string connectionString = "Data Source=22.22.11;Initial Catalog=sss;Pooling=True;user=sa;password=xxxx";
private void InsertData(List<Domain> domains)
{
	try
	{
		using(SqlConnection conn = new SqlConnection(connectionString))
		{
			conn.Open();
			foreach(var domain in domains)
			{
				string sInsert = string.Format(@"insert into domains.dbo.domain 
				([name],[description],[url],[price],[register],[type],[register_url],[left_time]) values
				(N'{0}',N'{1}',N'{2}',{3},N'{4}',N'{5}',N'{6}',N'{7}')", 
				domain.Name, domain.Desc, domain.Url, domain.Price, domain.Register, 
				domain.Type, domain.RegisterUrl, domain.LeftTime);
				Logger.Log(sInsert);
			
				using(SqlCommand cmd = new SqlCommand(sInsert,conn))
				{
					cmd.ExecuteNonQuery();
				}
			}
		}
	}
	catch(Exception e)
	{
		Logger.Log(e.Message);
	}
}

private void Delete(string register, string type)
{
	try
	{
		using(SqlConnection conn = new SqlConnection(connectionString))
		{
			conn.Open();
			string del = string.Format( "delete from domains.dbo.domain where register =N'{0}' and type = N'{1}'", register, type);
			Logger.Log(del);
			using(SqlCommand cmd = new SqlCommand(del,conn))
			{
				cmd.ExecuteNonQuery();
			}
		}
	}
	catch(Exception e)
	{
		Logger.Log(e.Message);
	}
}

public class Domain {
	public string Name {get;set;}
	public string Url {get;set;}
	public string Desc {get;set;}
	public string Type {get;set;}
	public int Price {get;set;}
	public string Register {get;set;}
	public string LeftTime {get;set;}
	public string RegisterUrl {get;set;}
}