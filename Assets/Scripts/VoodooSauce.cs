/* Introduction
 *
 * VoodooSauce is a Unity SDK that we implement into all of our games here at Voodoo.  This SDK is responsible for
 * providing Ads, Analytics, IAP, GDPR, etc. functionality in an easy to use package for internal and external studios
 * to integrate into their games. The SDK is used around the world by more than 200+ games, thus reliability and ease of use is
 * incredibly important for us. 
 *
 * For this exercise, we would like you to create a basic VoodooSauce that integrates the fake "TopAds" and "TopAnalytics"
 * SDKs.
 *
 * At the end we ask that you answer some quick questions at the bottom of this file. 
 * 
 */

/* Instructions 
 *
 * Please fill out the method implementations below 
 * Feel free to create additional classes to help with your implementation 
 * Please do not spend more than 2.5 hours on the code implementation portion of this exercise
 * Please do not modify the code in the 3rdParty folder
 * Make sure to read this entire file before starting to code.  We include important instructions on how to use the TopAds and TopAnalytics SDKs
 * 
 */

// Bonus Question : Show an android Toast when you launch the app.

using _3rdParty;
using System;


public static class VoodooSauce{

	// Before calling methods in TopAds and TopAnalytics you must call their init methods 
	// TopAds requires the TopAds prefab to be created in the scene
	// You also need to collect user GDPR consent and pass that boolean value to TopAds and TopAnalytics 
	// You can collect this consent by displaying a popup to the user at the start of the game and then storing that value for future use

	private static int secondsBetweenAds = 0;
	private static int gamesBetweenAds = 0;

	private static DateTime lastTimeAdShown = DateTime.MinValue;
	private static int gamesShown = 0;

    public static void StartGame()
	{
		gamesShown++;
		TopAnalytics.TrackEvent("gameStarted");
		// Track in TopAnalytics that a game has started 
	}

	public static void EndGame()
	{
		TopAnalytics.TrackEvent("gameEnded");
		// Track in TopAnalytics that a game has ended 
	}
	
	public static void ShowAd(string adId)
	{
		// TopAds methods must be called with a unique "string" ad unit id 
		// For your test app that id is "f4280fh0318rf0h2" 
		// However, when releasing the SDK to other studios, their ad unit id will be different 
		// Please find a flexible way to allow studios to provide their ad unit id to your VoodooSauce SDK

		// Before an ad is available to display, you must call TopAds.RequestAd 
		// You must call RequestAd each time before an ad is ready to display
		TopAds.OnAdLoadedEvent += delegate ()
		{
			System.Diagnostics.Debug.WriteLine("The ad is loaded");

			TopAds.ShowAd(adId);
		};

		TopAds.OnAdShownEvent += delegate ()
		{
			System.Diagnostics.Debug.WriteLine("The ad has been shown");

			lastTimeAdShown = DateTime.Now;
			gamesShown = 0;
		};

		TopAds.OnAdFailedEvent += delegate ()
		{
			System.Diagnostics.Debug.WriteLine("The ad failed to load or show, please refer to the SDK documentation for further details");
		};

		DateTime lastTime = lastTimeAdShown.AddSeconds(Convert.ToDouble(secondsBetweenAds));
		if (DateTime.Now.CompareTo(lastTime) >= 0
            && gamesShown >= gamesBetweenAds)
        {
			TopAds.RequestAd(adId);

		}

		// RequestAd will make a "fake" request for an ad that will take 0 to 10 seconds to complete
		// Afterwards, either the OnAdLoadedEvent or OnAdFailedEvent will be invoked 
		// Please implement an autorequest system that ensures an ad is always ready to be displayed
		// Keep in mind that RequestAd can fail multiple times in a row 

		// If an ad is loaded correctly, clicking on the "Show Ad" button within Unity-VoodooSauceTestApp 
		// should display a fake ad popup that you can close. 


		// Track in TopAnalytics when an ad is displayed.  Hint: TopAds.OnAdShownEvent 
	}

	public static void SetAdDisplayConditions(int secondsBetweenAds, int gamesBetweenAds)
	{
		VoodooSauce.secondsBetweenAds = secondsBetweenAds;
		VoodooSauce.gamesBetweenAds = gamesBetweenAds;
		VoodooSauce.gamesShown = gamesBetweenAds;
		// Sometimes studios call "ShowAd" too often and bombard players with ads 
		// Add a system that prevents the "ShowAd" method from showing an available ad 
		// Unless EITHER condition provided is true: 
		// 1) secondsBetweenAds: only show an ad if the previous ad was shown more than "secondBetweenAds" ago 
		// 2) gamesBetweenAds: only show an ad if "gamesBetweenAds" amount of games was played since the previous ad 
	}

    public static void SetGDPRConsent(bool gdprConsent)
    {
		TopAnalytics.InitWithConsent(gdprConsent);
        
		TopAds.InitializeSDK();
        if(gdprConsent)
        {
			TopAds.GrantConsent();
		} else
        {
			TopAds.RevokeConsent();
        }
	}


	// === Please answer these quick questions within the code file ===

	// In the VoodooSauce we integrate many 3rd party SDKs of varying reliability that display Ads, Analytics, etc.
	// What processes would you suggest to ensure that the VoodooSauce SDK is minimally affected by crashes 
	// in another SDK?

	// I would suggest doing a huge number of unit tests, use an agile methodology to shorten the releases, and fix bug faster.
	// I would also suggest isolating every SDK added using interfaces to decouple the code architecture to be able to tests the code easier and faster.
	// Then I would suggest that the sdk catch every crashes that might occur during these 3rd party sdk usage.



	// What are some pitfalls/shortcomings in your above implementation?

	// I hadn't the time to unit test my code so I'm afraid it would not be robust. I also didn't take the time to make the SDK clearer
	// and it could make the users use bad implementations.



	// How would you improve your implementation if you had more than 2 hours?

	// I would have added unit tests, I would have also caught each error that might occur in the third party sdk
	// to process them and deliver better errors understanding to the user.



	// What do you enjoy the most about being a developer?

	// I think architecture is the most entertaining task in my job. Ease the life
	// of the other developers in my team is the best reward I can get.



	// What do you enjoy the least about being a developer?

	// The worst thing that my job requires me to do is to sometimes work with pressure to fulfill an agenda. The problem is that
	// when you shorten the time, you also lessen quality. And this could lead to bad implementations, bugs, and bad mood in the team.



	// Why do you want to work on the VoodooSauce SDK vs. creating games in Unity?

	// I've already created some games in Unity, and that was fun. But I've never tried writing something that other developers could use.
	// I think this kind of experience could bring me a deeper knowledge in my job and I would love that.
}
