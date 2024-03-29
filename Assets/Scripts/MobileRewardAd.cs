using System;
 
using UnityEngine;

public class MobileRewardAd : MonoBehaviour
{
	public static MobileRewardAd instance;

	private string IdRewardedVideoAndroid = "ca-app-pub-1049448472911337/5266390194";

	//public RewardBasedVideoAd rewardBasedVideoAd;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	private void Start()
	{
		 
	}

	public void RequestRewardedVideo()
	{
		//string idRewardedVideoAndroid = IdRewardedVideoAndroid;
		//AdRequest request = new AdRequest.Builder().Build();
		//rewardBasedVideoAd.LoadAd(request, idRewardedVideoAndroid);
		//rewardBasedVideoAd.OnAdLoaded += OnAdLoaded;
		//rewardBasedVideoAd.OnAdFailedToLoad += OnAdFailedToLoad;
		//rewardBasedVideoAd.OnAdClosed += OnAdClose;
		//rewardBasedVideoAd.OnAdRewarded += OnAdRewarded;
	}

	private void OnAdLoaded(object sender, EventArgs args)
	{
	}

	private void OnAdFailedToLoad(object sender, EventArgs args)
	{
	}

	private void OnAdClose(object sender, EventArgs args)
	{
		//rewardBasedVideoAd.OnAdLoaded -= OnAdLoaded;
		//rewardBasedVideoAd.OnAdFailedToLoad -= OnAdFailedToLoad;
		//rewardBasedVideoAd.OnAdClosed -= OnAdClose;
		//rewardBasedVideoAd.OnAdRewarded -= OnAdRewarded;
		//RequestRewardedVideo();
	}

	private void OnAdRewarded(object sender, EventArgs args)
	{
	}
}
