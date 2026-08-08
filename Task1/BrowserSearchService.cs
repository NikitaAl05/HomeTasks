using OpenQA.Selenium;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;

namespace Task1;

internal sealed class BrowserSearchService : IDisposable, ITakesScreenshot
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;
    
    public BrowserSearchService()
    {
        // Если вы через windows закомментируйте Safari и расскоментируйте ChromeDriver
        driver = new SafariDriver(); // для macOS (Safari)
        // driver = new ChromeDriver(); для windows 
        
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }
    
    private void WaitForPageToLoad()
    {
        wait.Until(d => ((IJavaScriptExecutor)d)
            .ExecuteScript("return document.readyState")
            .Equals("complete"));
    }
    public void NavigateToSearch(string query)
    {
        string encodedQuery = Uri.EscapeDataString(query);
        driver.Navigate().GoToUrl($"https://yandex.ru/search/?text={encodedQuery}");        
        WaitForPageToLoad();
    }
    
    public void NavigateToImages(string query)
    {
        string encodedQuery = Uri.EscapeDataString(query);
        driver.Navigate().GoToUrl($"https://yandex.ru/images/search?text={encodedQuery}");        
        WaitForPageToLoad();
    }
    
    public Screenshot GetScreenshot()
    {
        return ((ITakesScreenshot)driver).GetScreenshot();
    }
    
    public (Screenshot mainSearch, Screenshot imagesSearch) CaptureAllScreenshots(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return (null, null);
        
        NavigateToSearch(query);
        Screenshot mainScreenshot = GetScreenshot();
        
        NavigateToImages(query);
        Screenshot imagesScreenshot = GetScreenshot();

        return (mainScreenshot, imagesScreenshot);
    }
    
    public void Dispose()
    {
        driver?.Quit();
        driver?.Dispose();
    }
    
}