string m_UserName = "*****";
string m_Password = "****";
string m_PW = "fwdd1127,.";
string m_Key = "充值";
string m_PhoneNo = "18971302185";
string m_Area = "";
string m_Role = "";
JQueryBrowser brsSub;

public void Run()
{	
	Logger.ClearAll();	
	brsSub = Browsers.Has("brsSub")?Browsers.Get("brsSub"):Browsers.Create("brsSub","about:blank");
	brsSub.Available = true;		
	var btnBuyConfirm1 = Default.SelectSingleNode("HTML:eq(0)>BODY:eq(0)>DIV:eq(1)>DIV:eq(2)>DIV:eq(0)>DIV:eq(1)>DIV:eq(1)>FORM:eq(0)>DIV:eq(3)>DIV:eq(1)>UL:eq(0)>LI:eq(4)>INPUT:eq(0)");//("Input#J_PerformSubmit");			
	if(!IsQCNull(btnBuyConfirm1))
	{	 	
		Logger.Log("buy...");
		FillValue();
 		btnBuyConfirm1.Click();
 		Default.Ready(10);
		Logger.Log("click buy confirm");
		var pw = Default.SelectSingleNode("input#payPassword_input");
		pw.Attr("value",m_PW);
		var pay = Default.SelectSingleNode("input#J_authSubmit");
		if(!IsQCNull(pay))pay.Click();
		return;	
	}
	else
	{
		Logger.Log("new...");
	}
	
	Login(m_UserName, m_Password);	
	brsSub.Navigate("http://s.taobao.com/search?q=" + GetUtf8(m_Key) + "&sort=price-asc");
	brsSub.Ready();
	Logger.Log(brsSub.Url.ToString());
	var items = brsSub.SelectNodes("ul.list-view>li.list-item>h3>a");
	foreach(var item in items)
	{
		string href = item.Attr("href");
		string title = item.Attr("title");
		Logger.Log(title);
		//Default.Navigate(href);
		//test only
		Default.Navigate("http://item.taobao.com/item.htm?spm=a230r.1.10.1.sphHQi&id=21102804716&_u=5nip0ee8b7");
		Default.Ready();
		Logger.Log("navigate to url");
		var btnBuy = Default.SelectSingleNode("b.J_ClickCatcher.J_LinkBuy");
		btnBuy.Click();
		Default.Ready("input#J_PerformSubmit");
		Logger.Log("click buy");
		FillValue();
		var check = Default.SelectSingleNode("input#J_checkCodeInput");		
		if(!IsQCNull(check))
		{
			var btnBuyConfirm = Default.SelectSingleNode("input#J_performSubmit");
			if(!IsQCNull(btnBuyConfirm))
			{
				btnBuyConfirm.Click();
				Logger.Log("click buy confirm");
			}
		}
		break;		
	}	
}

private void FillValue()
{
	var phone = Default.SelectSingleNode("input#J_phone");
	if(!IsQCNull(phone))
	{
		Logger.Log("phone");		
		phone.Attr("value",m_PhoneNo);
		phone.Click();
		var repeatNo = Default.SelectSingleNode("input#J_phoneRepeat");		
		repeatNo.Attr("value",m_PhoneNo);
		repeatNo.Click();
	}
	else
	{
		Logger.Log("non-phone");
		var area = Default.SelectSingleNode("input#J_virtualArea");
		if(IsQCNull(area)) return;
		//area.Click();
		area.Attr("value",m_Area);
		var role = Default.SelectSingleNode("input#J_roleName");
		//role.Click();
		role.Attr("value",m_Role);
	}
	Logger.Log("fill value");
}

private void Login(string userName, string password)
{
	brsSub.Navigate("http://www.taobao.com");
	brsSub.Ready();
	var login = brsSub.SelectSingleNode("p.login-info>a:eq(0)");
	if(!IsQCNull(login))
	{		
		if (login.Text() == "登录")
		{
			Logger.Log("logining ");
			login.Click();
			brsSub.Ready();
			
			var un = brsSub.SelectSingleNode("input#TPL_username_1");
			un.Attr("value",userName);
			var pw = brsSub.SelectSingleNode("input#TPL_password_1");
			pw.Attr("value",password);
			var btnLogin = brsSub.SelectSingleNode("button#J_SubmitStatic");
			btnLogin.Click();
			brsSub.Ready();
		}
	}
}

private string GetUtf8(string key)
{
	byte[] buffer = Encoding.GetEncoding("utf-8").GetBytes(key);
    string str = "";

    foreach (byte b in buffer) 
    {
    	str += string.Format("%{0:X}", b);
    }        
   
    return str;
}

private bool IsQCNull(JQueryContext qc)
{
    if (qc == null) return true;
    if (qc.Document == null) return true;
    return false;
}