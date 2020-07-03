using Applitools;
using Applitools.Selenium;
using Applitools.VisualGrid;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Drawing;
using Configuration = Applitools.Selenium.Configuration;
using ScreenOrientation = Applitools.VisualGrid.ScreenOrientation;

namespace ApplitoolsHackathon
{
	public class ModernTestsV2
	{

		public static void Main(string[] args)
		{
			// Create a new chrome web driver
			IWebDriver webDriver = new ChromeDriver();

			// Create a runner with concurrency of 1
			VisualGridRunner runner = new VisualGridRunner(10);

			// Create Eyes object with the runner, meaning it'll be a Visual Grid eyes.
			Eyes eyes = new Eyes(runner);

			SetUp(eyes);

			webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

			try
			{
				// ⭐️ Note to see visual bugs, run the test using the above URL for the 1st run.
				// but then change the above URL to https://demo.applitools.com/gridHackathonV2.html
				// (for the 2nd run)
				Task1(webDriver, eyes);
				Task2(webDriver, eyes);
				Task3(webDriver, eyes);
			}
			finally
			{
				TearDown(webDriver, runner);
			}

		}

		public static void SetUp(Eyes eyes)
		{

			// Initialize eyes Configuration
			Configuration config = new Configuration();

			// You can get your api key from the Applitools dashboard
			config.SetApiKey("NsL1007le0YYh5ZRgq1aFjc5GoHIM8110YURc7jHobW4gcE110");

			// create a new batch info instance and set it to the configuration
			config.SetBatch(new BatchInfo("UFG Hackathon"));

			// Add browsers with different viewports
			config.AddBrowser(1200, 700, BrowserType.CHROME);
			config.AddBrowser(1200, 700, BrowserType.FIREFOX);
			config.AddBrowser(1200, 700, BrowserType.EDGE_CHROMIUM);
			config.AddBrowser(768, 700, BrowserType.CHROME);
			config.AddBrowser(768, 700, BrowserType.FIREFOX);
			config.AddBrowser(768, 700, BrowserType.EDGE_CHROMIUM);

			// Add mobile emulation devices in Portrait mode
			config.AddDeviceEmulation(DeviceName.iPhone_X, ScreenOrientation.Portrait);

			// Set the configuration object to eyes
			eyes.SetConfiguration(config);

		}

		public static void Task1(IWebDriver webDriver, Eyes eyes)
		{

			try
			{

				// Navigate to the url we want to test
				//webDriver.Url = "https://demo.applitools.com/gridHackathonV1.html";
				webDriver.Url = "https://demo.applitools.com/gridHackathonV2.html";

				// Call Open on eyes to initialize a test session
				eyes.Open(webDriver, "AppilFashion", "Task 1", new Size(800, 600));

				// check the login page with fluent api, see more info here
				// https://applitools.com/docs/topics/sdk/the-eyes-sdk-check-fluent-api.html
				eyes.Check(Target.Window().Fully().WithName("Cross-Device Elements Test"));

				// Call Close on eyes to let the server know it should display the results
				eyes.CloseAsync();

			}
			catch (Exception e)
			{
				eyes.AbortAsync();
			}
		}
		public static void Task2(IWebDriver webDriver, Eyes eyes)
		{

			try
			{

				// Call Open on eyes to initialize a test session
				eyes.Open(webDriver, "AppilFashion", "Task 2", new Size(800, 600));

				webDriver.FindElement(By.Id("ti-filter")).Click();

				webDriver.FindElement(By.XPath("//*[@id='SPAN__checkmark__107']")).Click();
				webDriver.FindElement(By.Id("filterBtn")).Click();

				// Check the product grid only				
				eyes.Check("Product Grid", Target.Region(By.Id("#product_grid")));



				// Call Close on eyes to let the server know it should display the results
				eyes.CloseAsync();

			}
			catch (Exception e)
			{
				eyes.AbortAsync();
			}

		}

		public static void Task3(IWebDriver webDriver, Eyes eyes)
		{

			try
			{

				// Call Open on eyes to initialize a test session
				eyes.Open(webDriver, "AppilFashion", "Task 3", new Size(800, 600));

				webDriver.FindElement(By.Id("FIGURE____214")).Click();


				// Check the product details page
				eyes.Check(Target.Window().Fully().WithName("Product Details test"));



				// Call Close on eyes to let the server know it should display the results
				eyes.CloseAsync();

			}
			catch (Exception e)
			{
				eyes.AbortAsync();
			}

		}

		private static void TearDown(IWebDriver webDriver, VisualGridRunner runner)
		{
			// Close the browser
			webDriver.Quit();

			// we pass false to this method to suppress the exception that is thrown if we
			// find visual differences
			TestResultsSummary allTestResults = runner.GetAllTestResults(false);
			System.Console.WriteLine(allTestResults);
		}

	}
}
