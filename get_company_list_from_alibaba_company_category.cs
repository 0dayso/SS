public void Run()
{
	//add your codes here:
	using(CSVFile csvCate = new CSVFile(@"..\cc.txt"))
	{
		while(true)
		{
			try
			{
				string[] values = csvCate.Read();
				if(values == null)
				{
					break;
				}
				Default.Navigate(values[1]);
				Default.Ready();
				using(CSVFile csv = new CSVFile(@"..\companies\" + FormatPath(values[0]) + ".csv"))
				{
					do
					{
						try
						{
							var list = Default.SelectNodes("div.category-body>ul>li>a");
							foreach(var item in list)
							{
								try
								{
									csv.Write(values[0], item.Text(), item.Attr("href"));
									Logger.Log(item.Text());
								}
								catch(Exception ex1)
								{
									Logger.Log(ex1.Message);
								}
							}
							var nextPage = Default.SelectSingleNode("a.next");
							if(nextPage.IsEmpty())
							{
								break;
							}
							nextPage.Click();
							Default.Ready();
						}
						catch(Exception ex)
						{
							Logger.Log(ex.Message);
						}
					}
					while(true);
				}
			}
			catch(Exception ex2)
			{
				Logger.Log(ex2.Message);
			}
		}
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