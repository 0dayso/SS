public void Run()
{

 	Logger.ClearAll();
	
	string[] files = Directory.GetFiles(@"..\companies");
	
	foreach(string file in files)
	{
	   if(file.Contains("all.csv") || file.Contains("Contact.csv")) continue;
		
		string contactFile = file.Replace(".csv", "Contact.csv");
		if(File.Exists(contactFile))continue;
		
		CSVFile csvContact = new CSVFile(contactFile );	
		using(CSVFile csvCate = new CSVFile(file))
		{
			while(Default.Available)
			{
				try
				{
			
			
					string[] values = csvCate.Read();
					if(values == null)
					{
						 break;
					}
					
				    string url = values[2];
				    Default.Navigate(url);
				    Default.Ready(20);
				    url = Default.Url.ToString();
				
			
					//do
					{
						try
						{
							#region get contact url
							
							string contactUrl = string.Empty;
							if (url.Contains(".cn.alibaba.com"))
                            {
                                //http://hbylgy.cn.alibaba.com/
                                //http://hbylgy.cn.alibaba.com/athena/contact/hbylgy.html           contact
                                //http://hbylgy.cn.alibaba.com/athena/offerlist/hbylgy-sale.html    products
                                //http://hbylgy.cn.alibaba.com/athena/companyprofile/hbylgy.html    compan
                                string minName = Regex.Match(url, @"//.*.cn.").Value.Replace("//", "").Replace(".cn.", "");
                                contactUrl = url + "athena/contact/" + minName + ".html";
                                //companyUrl = detailUrl + "athena/companyprofile/" + minName + ".html";
                                //productsUrl = detailUrl + "athena/offerlist/" + minName + "-sale.html";
                            }
                            else if (url.Contains("china.alibaba.com"))
                            {
                                //http://china.alibaba.com/company/detail/xiaoton888.html
                                //http://china.alibaba.com/company/detail/contact/xiaoton888.html   contact
                                //http://china.alibaba.com/company/detail/intro/xiaoton888.html     company
                                //http://china.alibaba.com/company/offerlist/xiaoton888.html        product
                                contactUrl = url.Replace("detail/", "detail/contact/");
                                //companyUrl = detailUrl.Replace("detail/", "detail/intro/");
                                //productsUrl = detailUrl.Replace("detail/", "offerlist/");
                            }

                            else
                            {
                                Logger.Log(url);
                                Thread.Sleep(1000);
                                continue;
                            }
                            
                            #endregion
                            
                            Default.Navigate(contactUrl);
                            Default.Ready(20);
                            
                             ContactPerson = string.Empty;
	                         CompanyWebsite = string.Empty;
	                         Phone = string.Empty;
	                         Fax = string.Empty;
	                         Zip = string.Empty;
	                         Address = string.Empty;
	                         MobilePhone = string.Empty;
							
							string body = Default.SelectSingleNode("body").Text().Replace("传      真","传真").Replace("联 系  人","联系人").Replace("电      话","电话").Replace("地      址","地址").Replace("邮      编","邮编").Replace("：",":");
														
							ContactPerson = Regex.Match(body,"联系人: .*? ").Value.Replace("联系人: ","");
                            Phone = Regex.Match(body, "电话: .*移动").Value.Replace("电话: ","").Replace("移动","");
                            MobilePhone = Regex.Match(Default.DocumentTitle, @"\d{11}").Value;
                            Fax = Regex.Match(body, "传真: .*地址").Value.Replace("传真: ","").Replace("地址","");
                            Address = Regex.Match(body,"地址: .*?邮编").Value.Replace("地址: ","").Replace("邮编","");
                            if(Address == string.Empty) Address = Regex.Match(body,"地址: .*?公司").Value.Replace("地址: ","").Replace("公司","");
							Zip = Regex.Match(body, @"邮编: \d+").Value.Replace("邮编: ","");
							CompanyWebsite = Regex.Match(body, "公司主页: .*? ").Value.Replace("公司主页: ","");
							
							Logger.Log("--------Url " + contactUrl + " ---------");
							Logger.Log("ContactPerson " + ContactPerson);
							Logger.Log("Phone " + Phone);
                            Logger.Log("MobilePhone " + MobilePhone);
                            Logger.Log("Fax " + Fax);
                            Logger.Log("Address " + Address);
                            Logger.Log("Zip " + Zip);
                            Logger.Log("CompanyWebsite " + CompanyWebsite);
                            
                            if(ContactPerson != string.Empty && Address != string.Empty)
                            csvContact.Write(values[0], values[1], values[2], ContactPerson, Phone,MobilePhone, Fax,Address, Zip,CompanyWebsite);
						}
						catch
						{
						}
					}
					//while(Default.Available);
				}
				catch
				{}
			}
				
		}
			csvContact.Close();	
	}
	
}

JQueryContext m_QC;
List<JQueryContext> m_QCs;
string ContactPerson = string.Empty;
string CompanyWebsite = string.Empty;
string Phone = string.Empty;
string Fax = string.Empty;
string Zip = string.Empty;
string Address = string.Empty;
string MobilePhone = string.Empty;

 private bool IsQCNotNull()
        {
            if (m_QC == null) return false;
            if (m_QC.Document == null) return false;
            return true;
        }

public string FormatPath(string path)
{
	return path.Replace("\\", ".").Replace("|", ".").Replace(">", ".").Replace("<", ".").Replace("\"", ".").Replace("?", ".").Replace("*", ".").Replace(":", ".").Replace("/", ".");
}

class CSVFile : IDisposable
{
	public string FileName {get;set;}
	private StreamWriter sw = null;
	private StreamReader sr = null;
	private FileStream fs = null;
	public CSVFile(string filename)
	{
		this.FileName = filename;
		fs = new FileStream(filename, FileMode.OpenOrCreate);
		sw = new StreamWriter(fs, Encoding.UTF8);
		sr = new StreamReader(fs, Encoding.UTF8);
	}
	
	public void Close()
	{
		sw.Flush();
		fs.Close();
	}
	
	public string[] Read()
	{
		string text = sr.ReadLine();
		if(string.IsNullOrEmpty(text))
		{
			return null;
		}
		return text.Split(',');
	}
	
	public void Write(params string[] values)
	{
		StringBuilder sb = new StringBuilder();
		foreach(var v in values)
		{
			sb.Append(v + ",");
		}
		sw.WriteLine(sb.ToString().Substring(0, sb.Length - 1));
	}
	
	public void Dispose()
	{
		this.Close();
	}
}