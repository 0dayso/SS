 JQueryBrowser contactBrowser;
        JQueryBrowser companyBrowser;
        JQueryBrowser productBrowser;
        JQueryBrowser productsBrowser;
        JQueryContext m_QC;
        List<JQueryContext> m_QCs;

        string Address = string.Empty;
        string CompanyName = string.Empty;
        string CompanyType = string.Empty;
        string CompanyWebsite = string.Empty;
        string ContactPerson = string.Empty;
        string Description = string.Empty;
        string Email = string.Empty;
        string Products = string.Empty;
        string Zip = string.Empty;
        string Fax = string.Empty;
        string QQ = string.Empty;
        string Phone = string.Empty;
        string MobilePhone = string.Empty;
        string City = string.Empty;
        string Province = string.Empty;
        string RegisterCapital = string.Empty;
        DateTime? FoundTime = null;
        string Brand = string.Empty;
        string CompanySize = string.Empty;
        string Category = string.Empty;
        string Industry = string.Empty;
        string LegalPerson = string.Empty;

        string ProductName = string.Empty;
        string Price = string.Empty;
        string ProdcutDesc = string.Empty;
        string ProductHtml = string.Empty;

        public void Run()
        {
            try
            {
                Default.Available = true;
				Default.Navigate("http://search.china.alibaba.com/company/company_search.htm?keywords=%BA%FE%B1%B1&province=%BA%FE%B1%B1&pageSize=30&filt=y&showStyle=noimg&n=y&beginPage=50");
				
                Default.Ready();


                contactBrowser = Browsers.Has("Contact") ? Browsers.Get("Contact") : Browsers.Create("Contact", "about:blank");
                contactBrowser.Available = true;

                companyBrowser = Browsers.Has("Company") ? Browsers.Get("Company") : Browsers.Create("Company", "about:blank");
                companyBrowser.Available = true;

                productsBrowser = Browsers.Has("Prodcuts") ? Browsers.Get("Prodcuts") : Browsers.Create("Prodcuts", "about:blank");
                productsBrowser.Available = true;

                productBrowser = Browsers.Has("Prodcut") ? Browsers.Get("Prodcut") : Browsers.Create("Prodcut", "about:blank");
                productBrowser.Available = true;

                JQueryContext nextPage = null;

                string contactUrl, companyUrl, productsUrl;

                //if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Ali")) Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "Ali");

                do
                {
                    Logger.ClearAll();
                    var comapnies = Default.SelectNodes("#offers>ul>li");
                    foreach (var company in comapnies)
                    {
                        try
                        {
                            if (!Default.Available) return;
                            IDictionary<string, string> result = new Dictionary<string, string>();
                            var title = company.SelectSingleNode("a.Title");
                            CompanyName = title.Attr("title");
                            string detailUrl = title.Attr("href");
                            if (string.IsNullOrEmpty(CompanyName))
                            {
                                continue;
                            }
                            Logger.Log("公司: " + CompanyName);

                            string companyPath = AppDomain.CurrentDomain.BaseDirectory + "Ali" + "\\" + CompanyName;

                            if (Directory.Exists(companyPath)) continue;

                            #region get contact/desc/products url

                            contactUrl = "";
                            companyUrl = "";
                            productsUrl = "";
                            if (detailUrl.Contains(".cn.alibaba.com"))
                            {
                                //http://hbylgy.cn.alibaba.com/
                                //http://hbylgy.cn.alibaba.com/athena/contact/hbylgy.html           contact
                                //http://hbylgy.cn.alibaba.com/athena/offerlist/hbylgy-sale.html    products
                                //http://hbylgy.cn.alibaba.com/athena/companyprofile/hbylgy.html    compan
                                string minName = Regex.Match(detailUrl, @"//.*.cn.").Value.Replace("//", "").Replace(".cn.", "");
                                contactUrl = detailUrl + "athena/contact/" + minName + ".html";
                                companyUrl = detailUrl + "athena/companyprofile/" + minName + ".html";
                                productsUrl = detailUrl + "athena/offerlist/" + minName + "-sale.html";
                            }
                            else if (detailUrl.Contains("china.alibaba.com"))
                            {
                                //http://china.alibaba.com/company/detail/xiaoton888.html
                                //http://china.alibaba.com/company/detail/contact/xiaoton888.html   contact
                                //http://china.alibaba.com/company/detail/intro/xiaoton888.html     company
                                //http://china.alibaba.com/company/offerlist/xiaoton888.html        product
                                contactUrl = detailUrl.Replace("detail/", "detail/contact/");
                                companyUrl = detailUrl.Replace("detail/", "detail/intro/");
                                productsUrl = detailUrl.Replace("detail/", "offerlist/");
                            }

                            else
                            {
                                Logger.Log(detailUrl);
                                Thread.Sleep(1000);
                                continue;
                            }

                            #endregion

                            #region Get contact

                            contactBrowser.Navigate(contactUrl);
                            contactBrowser.Ready();

                            m_QCs = contactBrowser.SelectNodes("a.membername");
                            if (m_QCs.Count == 0)
                            {
                                m_QC = contactBrowser.SelectSingleNode("a#businessCard-embed-name");
                            }

                            if (m_QCs.Count > 0)
                            {
                                //http://13794444488.cn.alibaba.com/athena/contact/13794444488.html
                                ContactPerson = m_QCs[0].Text();

                                m_QCs = contactBrowser.SelectNodes("div.props-part>dl>dd>div>a");
                                for (int j = 0; j < m_QCs.Count; j++)
                                {
                                    CompanyWebsite += m_QCs[j].Text() + " ; ";
                                }

                                m_QCs = contactBrowser.SelectNodes("div.props-part>dl>dt");
                                m_QC = m_QCs.Where(q => q.Text().Contains("电      话")).FirstOrDefault();
                                if (IsQCNotNull()) Phone = m_QC.Next().Text();
                                m_QC = m_QCs.Where(q => q.Text().Contains("传      真")).FirstOrDefault();
                                if (IsQCNotNull()) Fax = m_QC.Next().Text();
                                m_QC = m_QCs.Where(q => q.Text().Contains("邮      编")).FirstOrDefault();
                                if (IsQCNotNull()) Zip = m_QC.Next().Text();
                                m_QC = m_QCs.Where(q => q.Text().Contains("地      址")).FirstOrDefault();
                                if (IsQCNotNull()) Address = m_QC.Next().Text();
                            }
                            else if (m_QC.Document != null)
                            {
                                //http://china.alibaba.com/company/detail/contact/jilinchangchunfy.html

                                ContactPerson = m_QC.Text();
                                m_QCs = contactBrowser.SelectNodes("div.businessCard-embed-content>ul>li");
                                if (m_QCs.Count == 0) continue;
                                m_QC = m_QCs.Where(q => q.Text().Contains("电话：")).FirstOrDefault();
                                if (IsQCNotNull()) Phone = m_QC.Text().Replace("电话：", "");
                                m_QC = m_QCs.Where(q => q.Text().Contains("传真：")).FirstOrDefault();
                                if (IsQCNotNull()) Fax = m_QC.Text().Replace("传真：", "");
                                m_QC = m_QCs.Where(q => q.Text().Contains("邮编：")).FirstOrDefault();
                                if (IsQCNotNull()) Zip = m_QC.Text().Replace("邮编：", "");
                                m_QC = m_QCs.Where(q => q.Text().Contains("网址：")).FirstOrDefault();
                                if (IsQCNotNull()) CompanyWebsite = m_QC.Text().Replace("网址：", "");
                                m_QC = m_QCs.Where(q => q.Text().Contains("地址：")).FirstOrDefault();
                                if (IsQCNotNull()) Address = m_QC.Text().Replace("地址：", "");
                            }
                            else
                            {
                                Logger.Log("No contact info");
                                Thread.Sleep(1000);
                                continue;
                            }

                            Logger.Log("ContactPerson " + ContactPerson);
                            Logger.Log("CompanyWebsite " + CompanyWebsite);
                            Logger.Log("Zip " + Zip);
                            Logger.Log("Fax " + Fax);
                            Logger.Log("Phone " + Phone);
                            Logger.Log("Address " + Address);

                            MobilePhone = Regex.Match(contactBrowser.DocumentTitle, @"\d{11}").Value;
                            Logger.Log("MobilePhone " + MobilePhone);
                            contactBrowser.Navigate("about:blank");

                            #endregion

                            #region Get Desc

                            companyBrowser.Navigate(companyUrl);
                            companyBrowser.Ready();

                            m_QCs = companyBrowser.SelectNodes("tr>th");
                            if (m_QCs.Count > 0)
                            {
                                //http://china.alibaba.com/company/detail/intro/ilutaigao.html

                                m_QCs = companyBrowser.SelectNodes("tbody>tr");

                                m_QC = m_QCs.Where(m => m.Text().Contains("主营行业")).FirstOrDefault();
                                if (IsQCNotNull())
                                {
                                    if (m_QC.Text().Trim().IndexOf("主营行业") == 0)
                                    {
                                        m_QC = m_QC.Next().SelectSingleNode("td:eq(0)");
                                    }
                                    else
                                    {
                                        m_QC = m_QC.Next().SelectSingleNode("td:eq(1)");
                                    }
                                    if (IsQCNotNull()) Industry = m_QC.Text();
                                    Logger.Log("Industry" + Industry);
                                }
                                m_QC = m_QCs.Where(m => m.Text().Contains("主营产品或服务")).FirstOrDefault();
                                if (IsQCNotNull())
                                {
                                    if (m_QC.Text().Trim().IndexOf("主营产品或服务") == 0)
                                    {
                                        m_QC = m_QC.Next().SelectSingleNode("td:eq(0)");
                                    }
                                    else
                                    {
                                        m_QC = m_QC.Next().SelectSingleNode("td:eq(1)");
                                    }
                                    if (IsQCNotNull()) Products = m_QC.Text();
                                    Logger.Log("Products" + Products);
                                }
                            }
                            else
                            {
                                //http://13794444488.cn.alibaba.com/athena/companyprofile/13794444488.html
                                m_QCs = companyBrowser.SelectNodes("tr>td");

                                m_QC = m_QCs.Where(m => m.Text().Contains("主营行业")).FirstOrDefault();
                                if (IsQCNotNull()) Industry = m_QC.Next().Text().Replace("—", "").Replace("[已认证]", "");
                                Logger.Log("Industry" + Industry);

                                m_QC = m_QCs.Where(m => m.Text().Contains("主营产品或服务")).FirstOrDefault();
                                if (IsQCNotNull()) Products = m_QC.Next().Text().Replace("—", "").Replace("[已认证]", "");
                                Logger.Log("Products" + Products);
                            }

                            m_QC = companyBrowser.SelectSingleNode("div.info-body>p");
                            if (m_QC.Document == null) m_QC = companyBrowser.SelectSingleNode("p.cont-p");
                            if (m_QC.Document != null)
                            {
                                Description = m_QC.Text().Replace("\r\n", "");
                                Logger.Log("Desc" + Description);
                            }

                            #endregion

                            string msg = "企业分类|企业所在地|企业全称|联系人|电话|邮箱|网址|介绍|经营范围|企业LOGO\r\n";
                            msg += Industry + "|" + Address + "|" + CompanyName + "|" + ContactPerson + "|" + Phone + "|" + Email + "|" + CompanyWebsite + "|" + Description + "|" + Products + "|" + "";
                            Export(companyPath + "\\company.txt", msg);

                            #region Get Products

                            productsBrowser.Navigate(productsUrl);
                            productsBrowser.Ready();

                            string productPath = string.Empty;

                            do
                            {
                                m_QCs = productsBrowser.SelectNodes("div.wp-offerlist-windows>ul>li");
                                foreach (var p in m_QCs)
                                {
                                    string pUrl = string.Empty;
                                    try
                                    {
                                        m_QC = p.SelectSingleNode("div.title>a");
                                        pUrl = m_QC.Attr("href");
                                        productBrowser.Navigate(pUrl);
                                        productBrowser.Ready();

                                        ProductName = m_QC.Attr("title");

                                        productPath = companyPath + "\\" + ProductName.Replace("/","").Replace(@"\","");

                                        if (Directory.Exists(productPath)) continue;

                                        m_QCs = productBrowser.SelectNodes("div#mod-detail-crumb-in>ul>li>a");
                                        if (m_QCs.Count > 2)
                                        {
                                            m_QC = m_QCs[2];
                                            Logger.Log("产品分类: " + m_QC.Text());
                                            Category = m_QC.Text();
                                        }

                                        m_QC = productBrowser.SelectSingleNode("span.de-pnum-ep:first");
                                        Price = m_QC.Text();
                                        Logger.Log("Price: " + Price);

                                        m_QC = productBrowser.SelectSingleNode("div#mod-detail-description");
                                        if (IsQCNotNull()) ProductHtml = m_QC.Html();

                                        m_QC = productBrowser.SelectSingleNode("div.tab-content");
                                        string picHtml = m_QC.Html();
                                        var picUrls = Regex.Matches(picHtml, @"src=.*?.jpg");
                                        for (int i = 0; i < picUrls.Count; i++)
                                        {
                                            string picUrl = picUrls[i].Value.Replace("src=\"", "");
                                            string picFileName = productPath + "\\" + i + ".jpg";
                                            Result.Download(picUrl, picFileName);
                                            Logger.Log("Download pic to " + picFileName);
                                        }

                                        msg = "产品分类|产品名称|价格|产品介绍HTML\r\n";
                                        msg += Category + "|" + ProductName + "|" + Price + "|" + ProductHtml;
                                        Export(productPath + "\\product.txt", msg);
                                    }
                                    catch (Exception ex)
                                    {
                                        Logger.Log("Url: " + pUrl);
                                    }
                                }

                                var next = productsBrowser.SelectSingleNode("a.next");
                                if (next.IsEmpty()) break;
                                next.Click();
                                productsBrowser.Reset();
                                productsBrowser.Ready();

                            } while (productsBrowser.Available);

                            #endregion

                        }
                        catch (Exception e)
                        {
                            Logger.Log(e.Message);
                        }
                    }

                    nextPage = Default.SelectSingleNode("a.page-next");

                    for (int i = 0; i < 3; i++)
                    {
                        if (nextPage.IsEmpty())
                        {
                            Default.Refresh();
                            Default.Ready();
                            nextPage = Default.SelectSingleNode("a.page-next");
                            i++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    nextPage.Click();
                    Default.Reset();

                    Default.Ready();
                }
                while (Default.Available);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
            }
        }

        private bool IsQCNotNull()
        {
            if (m_QC == null) return false;
            if (m_QC.Document == null) return false;
            return true;
        }

        private object obj = new object();
        public void Export(string fileName, String msg)
        {
            lock (obj)
            {
                try
                {
                    FileInfo fi = new FileInfo(fileName);
                    if (!fi.Directory.Exists) fi.Directory.Create();

                    using (FileStream filestream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                    {
                        StreamWriter writer = new StreamWriter(filestream);
                        writer.BaseStream.Seek(0, SeekOrigin.End);
                        writer.WriteLine(msg);
                        writer.Flush();
                        writer.Close();
                        filestream.Close();
                    }
                }
                catch { }
            }
        }