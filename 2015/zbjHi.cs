public void Run() 
{ 
 //add your codes here:
 Logger.ClearAll();
 Default.Navigate("http://shop.zhubajie.com/2417573/");
 Default.Ready(60);
 var nLinks = Default.SelectNodes("a.fl");
 if(nLinks.Count> 0) nLinks[0].Click();
 Default.Ready(60);
 var nText = Default.SelectSingleNode("div>textarea");
 nText.Attr("value","hi");
 var nBtn = Default.SelectSingleNode("div.ui-webim-button>a");
 if(!nBtn.IsEmpty())nBtn.Click();
 Thread.Sleep(100);
 var nClose = Default.SelectSingleNode("a.ui-webim-icons.ui-webim-closeline");
 if(!nClose.IsEmpty())nClose.Click();
}