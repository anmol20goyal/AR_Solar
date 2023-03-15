using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdsManager : MonoBehaviour
{
    string GameID = "ca-app-pub-7133464756774809~2455272134";

    #region Variables

    public bool interstitialAdLoaded;

    #endregion
    
    // ad ids
    private const string bannerAdId = "ca-app-pub-7133464756774809/6593827698";
    // private const string bannerAdId = "ca-app-pub-3940256099942544/6300978111";
    
    private const string interstitialAdID = "ca-app-pub-7133464756774809/3596963413";
    // private const string interstitialAdID = "ca-app-pub-3940256099942544/1033173712";
   

    public BannerView bannerAd;
    public InterstitialAd interstitial;

    public static AdsManager instanceAM;

    private void Awake()
    {
        if (instanceAM != null && instanceAM != this)
        {
            Destroy(gameObject);
            return;
        }
        instanceAM = this;
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    private void Start()
    {
        MobileAds.Initialize(initStatus => { });
        if (PlayerPrefs.GetInt("PurchasePlanets",0) == 0)
        {
            Invoke(nameof(ReqBannerAd), 2f);
            Invoke(nameof(RequestInterstitial), 2f);
        }

        interstitialAdLoaded = false;
    }

    #region BannerAds

    public void ReqBannerAd()
    {
        this.bannerAd = new BannerView(bannerAdId, AdSize.SmartBanner, AdPosition.Top);

        // Called when an ad request has successfully loaded.
        this.bannerAd.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.bannerAd.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;

        AdRequest request = new AdRequest.Builder().Build();

        this.bannerAd.LoadAd(request);
    }

    public void DestroyBanner()
    {
        if (bannerAd != null)
        {
            this.bannerAd.Destroy();
        }
    }
    
    #endregion

    #region Interstitial
   
    public void RequestInterstitial()
    {
        this.interstitial = new InterstitialAd(interstitialAdID);

        this.interstitial.OnAdLoaded += this.HandleOnInterstitialAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += this.InterstitialAdFailedToLoad;
        // Called when an ad is clicked.
        this.interstitial.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.interstitial.OnAdClosed += this.HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;

        AdRequest request = new AdRequest.Builder().Build();

        this.interstitial.LoadAd(request);
    }

    public void ShowInterstitialAd()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }

    #endregion

    #region AdDelegates

    private void HandleOnAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("Ad Loaded");
    }

    private void HandleOnInterstitialAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("Interstitial Ad Loaded");
        interstitialAdLoaded = true;
    }
    
    private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("couldnt load ad" + args.Message);
    }
    
    private void InterstitialAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("couldnt load ad" + args.Message);
        interstitialAdLoaded = false;
    }

    private void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    private void HandleOnAdClosed(object sender, EventArgs args)
    {
        Debug.Log("Ad Closed");
        BtnScript.ChangeScene();
    }

    private void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    #endregion
}
