public void Run()
{
	//add your codes here:

	
	while(true)
	{
		Default.Navigate("http://www.lufax.com/");
		Default.Ready("#recentLoanRequests",30);
		
		var loan = Default.SelectSingleNode("#recentLoanRequests>table>tbody>tr:first>td:last>a");
		string sLoan = loan.Text();
		if(sLoan != "交易成功" && !string.IsNullOrEmpty(sLoan))
		{
			Logger.Log("Has ...");
		}
		
		Thread.Sleep(1000);
		
		if(!Default.Available) break;
	}
}