using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BtnScript : MonoBehaviour
{
	#region GameObjects

	[Header("GameObjects")]
	// [SerializeField] private GameObject information;
	[SerializeField] private GameObject manualPanel;

	[SerializeField] private GameObject navigationPanel;
	[SerializeField] private GameObject instruction;
	[SerializeField] private GameObject insightPanel;
	[SerializeField] private GameObject notSupportedPanel;

	#endregion

	#region Variables

	// private bool once;

	#endregion

	private void Awake()
	{
		// once = true;

		if (SceneManager.GetActiveScene().buildIndex == 1)
		{
			instruction.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "WARNING";
			instruction.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
				"1. Play the AR game along the supervision of parents/guardians.\n2. Be aware of the physical hazards in the real life surroundings.";

			instruction.SetActive(true);
			Invoke(nameof(ChangeInstructions), 10f);
		}
		else
		{
			instruction.SetActive(true);
			Invoke(nameof(SwitchOffInstruct), 5f);
		}
	}

	private void Start()
	{
		/*if (PlayerPrefs.GetInt("PurchasePlanets",0) == 0)
		{
			AdsManager.instanceAM.RequestInterstitial();
		}*/
	}

	public void ShowInterstitialAds()
	{
		if (PlayerPrefs.GetInt("PurchasePlanets",0) == 0 && AdsManager.instanceAM.interstitialAdLoaded)
		{
			AdsManager.instanceAM.ShowInterstitialAd();
		}
		else
		{
			ChangeScene();
		}
	}
	
	private void ChangeInstructions()
	{
		instruction.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "INSTRUCTIONS";
		instruction.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
			"Please hold your device and point it at a free space. Select the celestial bodies you would like to observe. Use getures to magnify and integrate the celestial bodies in your surroundings.";
		
		Invoke(nameof(SwitchOffInstruct), 5f);
	}

	private void Update()
	{
		if (PlanetInfo.selectedPlanet != 0)
		{
			instruction.SetActive(false);
			// once = false;
		}

		if (SceneManager.GetActiveScene().buildIndex == 1)
		{
			if (notSupportedPanel.activeInHierarchy)
			{
				instruction.SetActive(false);
			}
		}
	}

	public void PlanetBtnPressed()
	{
		insightPanel.SetActive(false);
		manualPanel.SetActive(false);
		if (SceneManager.GetActiveScene().buildIndex == 1)
		{
			navigationPanel.SetActive(false);
		}
	}

	private void SwitchOffInstruct()
	{
		// once = true;
		instruction.SetActive(false);
	}

	public void InsightBtn()
	{
		if (PlanetInfo.selectedPlanet == 0)
		{
			instruction.SetActive(true);
			insightPanel.SetActive(false);
		}
		else
		{
			insightPanel.SetActive(!insightPanel.activeInHierarchy);
		}

		manualPanel.SetActive(false);
		navigationPanel.SetActive(false);
	}

	public void ManualBtn()
	{
		if (SceneManager.GetActiveScene().buildIndex == 1)
		{
			navigationPanel.SetActive(false);
		}

		insightPanel.SetActive(false);
		manualPanel.SetActive(!manualPanel.activeInHierarchy);
	}

	public void NavigationBtn()
	{
		// Debug.Log("ananya: " + PlanetInfo.selectedPlanet);
		if (PlanetInfo.selectedPlanet == 0)
		{
			instruction.SetActive(true);
			navigationPanel.SetActive(false);
		}
		else
		{
			navigationPanel.SetActive(!navigationPanel.activeInHierarchy);
		}

		manualPanel.SetActive(false);
		insightPanel.SetActive(false);
	}

	public static void ChangeScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex == 1 ? "planets 1" : "ARscene 1");
	}
}