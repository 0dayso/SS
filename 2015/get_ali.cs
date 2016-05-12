public void Run()
{
	Logger.ClearAll();
	
	string[] files = Directory.GetFiles(@"..\companies");
	
//	Debugger.Break();
	foreach(string file in files)
	{
		if(!Default.Available) break;
		   if(file.Contains("all.csv") || file.Contains("Desc.csv")) continue;
			
			string contactFile = file.Replace(".csv", "Desc.csv");
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
				    Default.Ready(10);
				   // url = Default.Url.ToString();
				
			
					//do
					{
						try
						{						      
                            #region get contact
                            
                            m_QCs = Default.SelectNodes("li>a");
                            if(m_QCs.Count == 0) continue;
							 m_QC = m_QCs.Where(m => m.Text().Contains("联系方式")).FirstOrDefault();
							  if(m_QC == null) continue;
							 url = m_QC.Attr("href");	
							 if(string.IsNullOrEmpty(url))continue;							 
							 Default.Navigate(url);
							 Default.Ready(10);
                            
                             ContactPerson = string.Empty;
	                         CompanyWebsite = string.Empty;
	                         Phone = string.Empty;
	                         Fax = string.Empty;
	                         Zip = string.Empty;
	                         Address = string.Empty;
	                         MobilePhone = string.Empty;
	                         Index = string.Empty;
							
							string body = Default.SelectSingleNode("body").Text().Replace("传      真","传真").Replace("联 系  人","联系人").Replace("电      话","电话").Replace("地      址","地址").Replace("邮      编","邮编").Replace("：",":");
							body = body.Replace("女士",":女士").Replace("移动电话",":移动电话").Replace("先生",":先生").Replace("公司主页",":公司主页").Replace("传真",":传真").Replace("联系人",":联系人").Replace("电话",":电话").Replace("地址",":地址").Replace("邮编",":邮编");																				
							ContactPerson = Regex.Match(body,"联系人:.*?:").Value.Replace("联系人","").Replace(":","").Trim();
                            Phone = Regex.Match(body, "电话: .*?:").Value.Replace("电话","").Replace(":","").Trim();
                            MobilePhone = Regex.Match(Default.DocumentTitle, @"\d{11}").Value;
                            Fax = Regex.Match(body, "传真:.*?:").Value.Replace("传真","").Replace(":","").Trim();
                            Address = Regex.Match(body,"地址:.*?:").Value.Replace("地址: ","").Replace(":","").Trim();
                            //if(Address == string.Empty) Address = Regex.Match(body,"地址: .*?公司").Value.Replace("地址: ","").Replace("公司","");
							Zip = Regex.Match(body, @"邮编: \d+").Value.Replace("邮编","").Replace(":","").Trim();
							CompanyWebsite = Regex.Match(body, "公司主页: .*? ").Value.Replace("公司主页","").Replace(":","").Trim();
							Index = Regex.Match(body, @"诚信通指数: \d").Value.Replace("诚信通指数: ","").Replace(":","").Trim();							
							
							Logger.Log("");
							Logger.Log("--------Url " + url + " ---------");
							Logger.Log("ContactPerson " + ContactPerson);
							Logger.Log("Phone " + Phone);
                            Logger.Log("MobilePhone " + MobilePhone);
                            Logger.Log("Fax " + Fax);
                            Logger.Log("Address " + Address);
                            Logger.Log("Zip " + Zip);
                            Logger.Log("CompanyWebsite " + CompanyWebsite);
                            Logger.Log("Index " + Index);
                            
                            #endregion 
                           
                                                        
		           			#region Get desc
							
							try
							{
			                    m_QCs = Default.SelectNodes("li>a");
			                    if(m_QCs.Count == 0) continue;
							 m_QC = m_QCs.Where(m => m.Text().Contains("公司介绍")).FirstOrDefault();
							 if(m_QC == null) continue;
							 url = m_QC.Attr("href");
							 if(string.IsNullOrEmpty(url))continue;							 
							 Default.Navigate(url);
							 
			                    Logger.Log("Navigating " + url);
			                    
			                     m_QC = null;
                   				 m_QCs = null;
								 FoundTime = string.Empty;
								 RegisterCapital = string.Empty;
								 Brand = string.Empty;
								 CompanySize = string.Empty;
								 LegalPerson = string.Empty;
								 CompanyType = string.Empty;
								 Pattern = string.Empty;
								 Industry = string.Empty;
								 Products = string.Empty;
								 Description = string.Empty;
								
			                    Default.Ready(10);//"div.info-body>p;p.cont-p");
			
			                     body = Default.SelectSingleNode("body").Text();
 body = body.Replace("：",":").Replace("\r\n",":").Replace("法定代表人","法人代表");
 body = body.Replace("主营产品或服务",":主营产品或服务").Replace("主营行业",":主营行业").Replace("注册资本",":注册资本").Replace("经营模式",":经营模式").Replace("公司成立时间",":公司成立时间").Replace("公司注册地",":公司注册地").Replace("企业类型",":企业类型").Replace("法人代表",":法人代表").Replace("主要销售区域",":主要销售区域").Replace("员工人数",":员工人数").Replace("研发部门人数",":研发部门人数").Replace("质量控制",":质量控制").Replace("是否提供加工/定制",":是否提供加工/定制").Replace("公司主页",":公司主页").Replace("厂房面积",":厂房面积").Replace("详细信息",":详细信息");
 
 
								FoundTime = Regex.Match(body, @"公司成立时间:.*?:").Value.Replace("公司成立时间:","").Replace(":","").Trim();
								Logger.Log(FoundTime); 
								
								RegisterCapital = Regex.Match(body, "注册资本:.*?:").Value.Replace("注册资本:","").Replace(":","").Trim();
								Logger.Log("RegisterCapital " + RegisterCapital);
								
								Brand = Regex.Match(body, "品牌名称:.*?:").Value.Replace("品牌名称:","").Replace(":","").Trim();
								Logger.Log("Brand " + Brand);
								
								CompanySize = Regex.Match(body, "员工人数:.*?:").Value.Replace("员工人数:","").Replace(":","").Trim();
								Logger.Log("CompanySize " + CompanySize);
								
								LegalPerson = Regex.Match(body, "法人代表:.*?:").Value.Replace("法人代表:","").Replace(":","").Trim();
								Logger.Log("LegalPerson " + LegalPerson);
								
								CompanyType = Regex.Match(body, "企业类型:.*?:").Value.Replace("企业类型:","").Replace(":","").Trim();
								Logger.Log("CompanyType " + CompanyType);
								
								Pattern = Regex.Match(body, "经营模式:.*?:").Value.Replace("经营模式:","").Replace(":","").Trim();
								Logger.Log("Pattern " + Pattern);
								
								Industry = Regex.Match(body, "主营行业:.*?:").Value.Replace("主营行业:","").Replace(":","").Trim();
								Logger.Log("Industry " + Industry);
								
								Products = Regex.Match(body, "主营产品或服务:.*?:").Value.Replace("主营产品或服务:","").Replace(":","").Trim();
								Logger.Log("Products " + Products);
			                }
			                catch(Exception e)
			                {
			                	Logger.Log("desc " + ExceptionHelper.ToString(e));
			                }    
		
		                    #endregion
                            
                           csvContact.Write(values[0], values[1], values[2], ContactPerson, Phone,MobilePhone, Fax,Address, Zip,CompanyWebsite, Index,FoundTime,RegisterCapital, Brand, CompanySize,LegalPerson, CompanyType, Pattern,Industry, Products );
						}
						catch(Exception e)
		                {
		                	Logger.Log("contact desc " + ExceptionHelper.ToString(e));
		                }  
					}
					//while(Default.Available);
				}
				catch(Exception e)
	            {
	            	Logger.Log("read csv desc" + ExceptionHelper.ToString(e));
	            }  
			}
				
		}
			csvContact.Close();	
	}
	
}

JQueryContext m_QC = null;
List<JQueryContext> m_QCs = null;
//contact
string ContactPerson = string.Empty;
string CompanyWebsite = string.Empty;
string Phone = string.Empty;
string Fax = string.Empty;
string Zip = string.Empty;
string Address = string.Empty;
string MobilePhone = string.Empty;
string Index = string.Empty;
//desc
string FoundTime = string.Empty;
string RegisterCapital = string.Empty;
string Brand = string.Empty;
string CompanySize = string.Empty;
string LegalPerson = string.Empty;
string CompanyType = string.Empty;
string Pattern = string.Empty;
string Industry = string.Empty;
string Products = string.Empty;
string Description = string.Empty;

private bool IsQCNotNull()
{
	try
	{
		if(m_QCs.Count == 0) return false;
	    if (m_QC == null) return false;
	    if (m_QC.Document == null) return false;
	    return true;
	}
	 catch (Exception e)
    {
        Logger.Log("IsQCNotNull " + ExceptionHelper.ToString(e));
        return false;
    }
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