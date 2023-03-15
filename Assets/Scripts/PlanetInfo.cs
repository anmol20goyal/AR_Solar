using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vuforia;
using Image = UnityEngine.UI.Image;

public class PlanetInfo : MonoBehaviour
{
	#region GameObjects

	[SerializeField] private GameObject purchaserPanel;
	
	[Header("Insights")]
	[SerializeField] private TextMeshProUGUI insightTitle;
	[SerializeField] private Text insightHead;
	[SerializeField] private Text insightRadius;
	[SerializeField] private Text insightOrbital;
	[SerializeField] private Text insightRotational;
	[SerializeField] private Text insightDistance;

	[Header("Navigation")] 
	[SerializeField] private Text navDistance;
	[SerializeField] private Text[] navDiameter;
	[SerializeField] private Text navHeight;
	
	[Header("ColorButtons")]
	[SerializeField] private Color highlightColor;
	[SerializeField] private Color unHighlightColor;

	[Header("Buttons")] 
	[SerializeField] private Image mercuryBtn;
	[SerializeField] private Image venusBtn;
	[SerializeField] private Image earthBtn;
	[SerializeField] private Image marsBtn;
	[SerializeField] private Image jupiterBtn;
	[SerializeField] private Image saturnBtn;
	[SerializeField] private Image uranusBtn;
	[SerializeField] private Image neptuneBtn;

	[Header("Camera")] 
	private Camera mainCamera;
	
	[Header("Planets")] 
	[SerializeField] private GameObject mercury;
	[SerializeField] private GameObject venus;
	[SerializeField] private GameObject earth;
	[SerializeField] private GameObject mars;
	[SerializeField] private GameObject jupiter;
	[SerializeField] private GameObject saturn;
	[SerializeField] private GameObject uranus;
	[SerializeField] private GameObject neptune;
	
	[Header("Stars")]
	[SerializeField] private GameObject[] btnStar;
	
	#endregion

	#region NormalColors

	[Header("NormalColors")]
	[SerializeField] private Color btnColor;
	[SerializeField] private Color imageColor;
	
	#endregion

	#region Varibales

	private bool once;
	private bool PURCHASED;
	public static int selectedPlanet;
	private float distanceFromCam;
	public static float objectDiameter;

	#endregion

	#region InstancePI

	private static PlanetInfo instance;

	public static PlanetInfo InstancePI
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<PlanetInfo>();
			}

			return instance;
		}
	}

	#endregion
	
	private void Start()
	{
		once = true;
		purchaserPanel.SetActive(false);
		selectedPlanet = 0;
		
		mainCamera = Camera.main;

		highlightColor = new Color(1,0.77f,0,1);
		unHighlightColor = new Color(1, 1, 1, 0.78f);

	}

	private void Update()
	{
		PURCHASED = PlayerPrefs.GetInt("PurchasePlanets",0) == 1;
		if (PURCHASED && once)
		{
			once = false;
			PurchaseSuccess();
		}
		
		#region DataAboutPlanets

		if (SceneManager.GetActiveScene().name == "ARscene 1")
		{
			switch (selectedPlanet)
			{
				case 1:
					distanceFromCam = Vector3.Distance(mainCamera.transform.position, mercury.transform.position);
					navDistance.text = "<size=20>Distance to Mercury</size>\n" + Math.Round(distanceFromCam,2) + " m";
					navDiameter[0].text = "<size=20>Diameter Mercury</size>\n" + objectDiameter + " m";
					navDiameter[1].text = "Diameter Mercury";
					navDiameter[2].text = objectDiameter + " m";
					break;
				case 2:
					distanceFromCam = Vector3.Distance(mainCamera.transform.position, venus.transform.position);
					navDistance.text = "<size=20>Distance to Venus</size>\n" + Math.Round(distanceFromCam,2) + " m";
					navDiameter[0].text = "<size=20>Diameter Venus</size>\n" + objectDiameter + " m";
					navDiameter[1].text = "Diameter Venus";
					navDiameter[2].text = objectDiameter + " m";
					break;
				case 3:
					distanceFromCam = Vector3.Distance(mainCamera.transform.position, earth.transform.position);
					navDistance.text = "<size=20>Distance to Earth</size>\n" + Math.Round(distanceFromCam,2) + " m";
					navDiameter[0].text = "<size=20>Diameter Earth</size>\n" + objectDiameter + " m";
					navDiameter[1].text = "Diameter Earth";
					navDiameter[2].text = objectDiameter + " m";
					break;
				case 4:
					distanceFromCam = Vector3.Distance(mainCamera.transform.position, mars.transform.position);
					navDistance.text = "<size=20>Distance to Mars</size>\n" + Math.Round(distanceFromCam,2) + " m";
					navDiameter[0].text = "<size=20>Diameter Mars</size>\n" + objectDiameter + " m";
					navDiameter[1].text = "Diameter Mars";
					navDiameter[2].text = objectDiameter + " m";
					break;
				case 5:
					distanceFromCam = Vector3.Distance(mainCamera.transform.position, jupiter.transform.position);
					navDistance.text = "<size=20>Distance to Jupiter</size>\n" + Math.Round(distanceFromCam,2) + " m";
					navDiameter[0].text = "<size=20>Diameter Jupiter</size>\n" + objectDiameter + " m";
					navDiameter[1].text = "Diameter Jupiter";
					navDiameter[2].text = objectDiameter + " m";
					break;
				case 6:
					distanceFromCam = Vector3.Distance(mainCamera.transform.position, saturn.transform.position);
					navDistance.text = "<size=20>Distance to Saturn</size>\n" + Math.Round(distanceFromCam,2) + " m";
					navDiameter[0].text = "<size=20>Diameter Saturn</size>\n" + objectDiameter + " m";
					navDiameter[1].text = "Diameter Saturn";
					navDiameter[2].text = objectDiameter + " m";
					break;
				case 7:
					distanceFromCam = Vector3.Distance(mainCamera.transform.position, uranus.transform.position);
					navDistance.text = "<size=20>Distance to Uranus</size>\n" + Math.Round(distanceFromCam,2) + " m";
					navDiameter[0].text = "<size=20>Diameter Uranus</size>\n" + objectDiameter + " m";
					navDiameter[1].text = "Diameter Uranus";
					navDiameter[2].text = objectDiameter + " m";
					break;
				case 8:
					distanceFromCam = Vector3.Distance(mainCamera.transform.position, neptune.transform.position);
					navDistance.text = "<size=20>Distance to Neptune</size>\n" + Math.Round(distanceFromCam,2) + " m";
					navDiameter[0].text = "<size=20>Diameter Neptune</size>\n" + objectDiameter + " m";
					navDiameter[1].text = "Diameter Neptune";
					navDiameter[2].text = objectDiameter + " m";
					break;
				default:
					break;
			}
		}

		#endregion
	}

	#region PlanetBtnFunctions

	public void Mercury()
	{
		mercury.SetActive(true);
		venus.SetActive(false);
		earth.SetActive(false);
		mars.SetActive(false);
		jupiter.SetActive(false);
		saturn.SetActive(false);
		uranus.SetActive(false);
		neptune.SetActive(false);
		
		mercuryBtn.color = highlightColor;
		venusBtn.color = unHighlightColor;
		earthBtn.color = unHighlightColor;
		marsBtn.color = unHighlightColor;
		jupiterBtn.color = unHighlightColor;
		saturnBtn.color = unHighlightColor;
		uranusBtn.color = unHighlightColor;
		neptuneBtn.color = unHighlightColor;
		
		selectedPlanet = 1;
		insightTitle.text = "MERCURY";
		insightHead.text = "It is the smallest planet in our solar system and closest to the Sun.";
		insightRadius.text = "<b>Radius :</b> 2439km";
		insightOrbital.text = "<b>Orbit Duration :</b> 88 Days";
		insightDistance.text = "<b>Distance to Sun :</b> 0.4AU";
		insightRotational.text = "<b>Rotation Period :</b> 58 Days";
	}
	
	public void Venus()
	{
		mercury.SetActive(false);
		venus.SetActive(true);
		earth.SetActive(false);
		mars.SetActive(false);
		jupiter.SetActive(false);
		saturn.SetActive(false);
		uranus.SetActive(false);
		neptune.SetActive(false);
		
		mercuryBtn.color = unHighlightColor;
		venusBtn.color = highlightColor  ;
		earthBtn.color = unHighlightColor  ;
		marsBtn.color = unHighlightColor  ;
		jupiterBtn.color = unHighlightColor  ;
		saturnBtn.color = unHighlightColor  ;
		uranusBtn.color = unHighlightColor  ;
		neptuneBtn.color = unHighlightColor  ;
		
		selectedPlanet = 2;
		insightTitle.text = "VENUS";
		insightHead.text = "Venus is the second planet from the Sun.";
		insightRadius.text = "<b>Radius :</b> 6051km";
		insightOrbital.text = "<b>Orbit Duration :</b> 225 Days";
		insightDistance.text = "<b>Distance to Sun :</b> 0.7AU";
		insightRotational.text = "<b>Rotation Period :</b> 116 Days";
	}
	
	public void Earth()
	{
		mercury.SetActive(false);
		venus.SetActive(false);
		earth.SetActive(true);
		mars.SetActive(false);
		jupiter.SetActive(false);
		saturn.SetActive(false);
		uranus.SetActive(false);
		neptune.SetActive(false);
		
		mercuryBtn.color = unHighlightColor;
		venusBtn.color = unHighlightColor  ;
		earthBtn.color = highlightColor  ;
		marsBtn.color = unHighlightColor  ;
		jupiterBtn.color = unHighlightColor  ;
		saturnBtn.color = unHighlightColor  ;
		uranusBtn.color = unHighlightColor  ;
		neptuneBtn.color = unHighlightColor  ;
		
		selectedPlanet = 3;
		insightTitle.text = "EARTH";
		insightHead.text = "Earth is the only astronomical object known to harbor life.";
		insightRadius.text = "<b>Radius :</b> 6357km";
		insightOrbital.text = "<b>Orbit Duration :</b> 365 Days";
		insightDistance.text = "<b>Distance to Sun :</b> 1AU";
		insightRotational.text = "<b>Rotation Period :</b> 24 hrs";
	}
	
	public void Mars()
	{
		if (PURCHASED)
		{
			mercury.SetActive(false);
			venus.SetActive(false);
			earth.SetActive(false);
			mars.SetActive(true);
			jupiter.SetActive(false);
			saturn.SetActive(false);
			uranus.SetActive(false);
			neptune.SetActive(false);
			
			mercuryBtn.color = unHighlightColor;
			venusBtn.color = unHighlightColor  ;
			earthBtn.color = unHighlightColor  ;
			marsBtn.color = highlightColor  ;
			jupiterBtn.color = unHighlightColor  ;
			saturnBtn.color = unHighlightColor  ;
			uranusBtn.color = unHighlightColor  ;
			neptuneBtn.color = unHighlightColor  ;
		
			selectedPlanet = 4;
			insightTitle.text = "MARS";
			insightHead.text = "It is the second smallest planet in the Solar System.";
			insightRadius.text = "<b>Radius :</b> 3389km";
			insightOrbital.text = "<b>Orbit Duration :</b> 687 Days";
			insightDistance.text = "<b>Distance to Sun :</b> 1.5AU";
			insightRotational.text = "<b>Rotation Period :</b> 25 hrs";
		}
		else
		{
			purchaserPanel.SetActive(true);
		}
	}
	
	public void Jupiter()
	{
		if (PURCHASED)
		{
			mercury.SetActive(false);
			venus.SetActive(false);
			earth.SetActive(false);
			mars.SetActive(false);
			jupiter.SetActive(true);
			saturn.SetActive(false);
			uranus.SetActive(false);
			neptune.SetActive(false);

			mercuryBtn.color = unHighlightColor;
			venusBtn.color = unHighlightColor  ;
			earthBtn.color = unHighlightColor  ;
			marsBtn.color = unHighlightColor  ;
			jupiterBtn.color = highlightColor  ;
			saturnBtn.color = unHighlightColor  ;
			uranusBtn.color = unHighlightColor  ;
			neptuneBtn.color = unHighlightColor  ;
		
			selectedPlanet = 5;
			insightTitle.text = "JUPITER";
			insightHead.text = "It is the largest in the Solar System.";
			insightRadius.text = "<b>Radius :</b> 69,911km";
			insightOrbital.text = "<b>Orbit Duration :</b> 12 yrs";
			insightDistance.text = "<b>Distance to Sun :</b> 5.2AU";
			insightRotational.text = "<b>Rotation Period :</b> 10 hrs";
		}
		else
		{
			purchaserPanel.SetActive(true);
		}
	}
	
	public void Saturn()
	{
		if (PURCHASED)
		{
			mercury.SetActive(false);
			venus.SetActive(false);
			earth.SetActive(false);
			mars.SetActive(false);
			jupiter.SetActive(false);
			saturn.SetActive(true);
			uranus.SetActive(false);
			neptune.SetActive(false);
			
			mercuryBtn.color = unHighlightColor;
			venusBtn.color = unHighlightColor  ;
			earthBtn.color = unHighlightColor  ;
			marsBtn.color = unHighlightColor  ;
			jupiterBtn.color = unHighlightColor  ;
			saturnBtn.color = highlightColor  ;
			uranusBtn.color = unHighlightColor  ;
			neptuneBtn.color = unHighlightColor  ;
		
			selectedPlanet = 6;
			insightTitle.text = "SATURN";
			insightHead.text = "It is the second-largest in the Solar System.";
			insightRadius.text = "<b>Radius :</b> 58,232km";
			insightOrbital.text = "<b>Orbit Duration :</b> 29 yrs";
			insightDistance.text = "<b>Distance to Sun :</b> 9.6AU";
			insightRotational.text = "<b>Rotation Period :</b> 11 hrs";
		}
		else
		{
			purchaserPanel.SetActive(true);
		}
	}
	
	public void Uranus()
	{
		if (PURCHASED)
		{
			mercury.SetActive(false);
			venus.SetActive(false);
			earth.SetActive(false);
			mars.SetActive(false);
			jupiter.SetActive(false);
			saturn.SetActive(false);
			uranus.SetActive(true);
			neptune.SetActive(false);
			
			mercuryBtn.color = unHighlightColor;
			venusBtn.color = unHighlightColor  ;
			earthBtn.color = unHighlightColor  ;
			marsBtn.color = unHighlightColor  ;
			jupiterBtn.color = unHighlightColor  ;
			saturnBtn.color = unHighlightColor  ;
			uranusBtn.color = highlightColor  ;
			neptuneBtn.color = unHighlightColor  ;
		
			selectedPlanet = 7;
			insightTitle.text = "URANUS";
			insightHead.text = "It has the third-largest planetary radius and fourth-largest planetary mass in the Solar System.";
			insightRadius.text = "<b>Radius :</b> 25,362km";
			insightOrbital.text = "<b>Orbit Duration :</b> 84 yrs";
			insightDistance.text = "<b>Distance to Sun :</b> 19.2AU";
			insightRotational.text = "<b>Rotation Period :</b> 17 hrs";
		}
		else
		{
			purchaserPanel.SetActive(true);
		}
	}
	
	public void Neptune()
	{
		if (PURCHASED)
		{
			mercury.SetActive(false);
			venus.SetActive(false);
			earth.SetActive(false);
			mars.SetActive(false);
			jupiter.SetActive(false);
			saturn.SetActive(false);
			uranus.SetActive(false);
			neptune.SetActive(true);
			
			mercuryBtn.color = unHighlightColor;
			venusBtn.color = unHighlightColor  ;
			earthBtn.color = unHighlightColor  ;
			marsBtn.color = unHighlightColor  ;
			jupiterBtn.color = unHighlightColor  ;
			saturnBtn.color = unHighlightColor  ;
			uranusBtn.color = unHighlightColor  ;
			neptuneBtn.color = highlightColor  ;
		
			selectedPlanet = 8;
			insightTitle.text = "NEPTUNE";
			insightHead.text = "It is the eighth and farthest-known Solar planet from the Sun.";
			insightRadius.text = "<b>Radius :</b> 24,622km";
			insightOrbital.text = "<b>Orbit Duration :</b> 165 yrs";
			insightDistance.text = "<b>Distance to Sun :</b> 30AU";
			insightRotational.text = "<b>Rotation Period :</b> 16 hrs";
		}
		else
		{
			purchaserPanel.SetActive(true);
		}
	}

	#endregion

	private void PurchaseSuccess()
	{
		// disable banner ads
		AdsManager.instanceAM.DestroyBanner();
		
		purchaserPanel.SetActive(false);
		
		marsBtn.color = btnColor;
		jupiterBtn.color = btnColor;
		saturnBtn.color = btnColor;
		uranusBtn.color = btnColor;
		neptuneBtn.color = btnColor;

		marsBtn.transform.GetChild(0).GetComponent<Image>().color = imageColor;
		jupiterBtn.transform.GetChild(0).GetComponent<Image>().color = imageColor;
		saturnBtn.transform.GetChild(0).GetComponent<Image>().color = imageColor;
		uranusBtn.transform.GetChild(0).GetComponent<Image>().color = imageColor;
		neptuneBtn.transform.GetChild(0).GetComponent<Image>().color = imageColor;

		foreach (var star in btnStar)
		{
			star.SetActive(false);
		}
	}
	
}
