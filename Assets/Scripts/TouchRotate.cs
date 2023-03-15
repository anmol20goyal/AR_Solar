using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchRotate : MonoBehaviour
{
	#region GameObjects

	private Camera mainCamera;
	private Rigidbody rb;

	[Header("Planets")] 
	[SerializeField] private GameObject thisPlanet;
	[SerializeField] private Transform[] planetSphere;

	[Header("CanvasObjects")] 
	[SerializeField] private GameObject diameterChangePanel;

	#endregion
	
	#region Variables

	private float rotationSpeed;
	private bool dragging = false;
	private bool zooming = false;
	private float zValue;

	public static float zoomLimitSmall;
	public static float zoomLimitLarge;

	private float zoomModifierSpeed = 0.025f;

	private Vector2 firstTouchPrevPos;
	private Vector2 secondTouchPrevPos;

	private Vector3 planetPos;
	
	private float touchesPrevPosDiff, touchesCurPosDiff, zoomModifier;

	#endregion

	private void Awake()
	{
		mainCamera = Camera.main;

		if (SceneManager.GetActiveScene().buildIndex == 1)
		{
			rotationSpeed = 10000;
		}
		else
		{
			rotationSpeed = 20000;
		}
		
		
		rb = GetComponent<Rigidbody>();

		planetPos = thisPlanet.transform.localPosition;
		zValue = planetPos.z;
	}

	private void Start()
	{
		if (SceneManager.GetActiveScene().buildIndex == 0)
		{
			mainCamera.fieldOfView = 100;
		}
	}

	private void Update()
	{
		if (SceneManager.GetActiveScene().name == "ARscene 1")
		{
			PlanetInfo.objectDiameter =
				(float) Math.Round(Vector3.Distance(planetSphere[0].position,planetSphere[1].position), 4);
			
			diameterChangePanel.SetActive(zooming);
			
			switch (PlanetInfo.selectedPlanet)
			{
				case 3:
				case 4:
				case 6:
					zoomLimitSmall = 0.5f;
					zoomLimitLarge = 1.5f;
					zoomModifierSpeed = 0.0005f;
					break;
				default:
					zoomLimitSmall = 5;
					zoomLimitLarge = 50;
					zoomModifierSpeed = 0.025f;
					break;
			}
		}

		switch (Input.touchCount)
		{
			case 1:
				dragging = true;
				zooming = false;
				break;
			case 2:
				zooming = true;
				dragging = false;
				break;
			default:
				zooming = false;
				dragging = false;
				break;
		}
	}

	private void FixedUpdate()
	{
		if (dragging)
		{
			float x = Input.GetAxis("Mouse X") * rotationSpeed * Time.fixedDeltaTime;
			float y = Input.GetAxis("Mouse Y") * rotationSpeed * Time.fixedDeltaTime;

			rb.AddTorque(Vector3.down * x);
			rb.AddTorque(Vector3.right * y);
		}
		
		if (zooming)
		{
			var scaleNew = thisPlanet.transform.localScale;
			
			Touch firstTouch = Input.GetTouch(0);
			Touch secondTouch = Input.GetTouch(1);

			firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
			secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

			touchesPrevPosDiff = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
			touchesCurPosDiff = (firstTouch.position - secondTouch.position).magnitude;

			zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomModifierSpeed;

			if (SceneManager.GetActiveScene().buildIndex == 0)
			{
				if (touchesPrevPosDiff > touchesCurPosDiff)
				{
					zValue = thisPlanet.transform.localPosition.z;
					zValue += zoomModifier;
					thisPlanet.transform.localPosition = new Vector3(planetPos.x,planetPos.y,zValue);
				}

				if (touchesPrevPosDiff < touchesCurPosDiff)
				{
					zValue = thisPlanet.transform.localPosition.z;
					zValue -= zoomModifier;
					thisPlanet.transform.localPosition = new Vector3(planetPos.x,planetPos.y,zValue);
				}
			}
			else
			{
				if (touchesPrevPosDiff > touchesCurPosDiff)
				{
					if (scaleNew.x > zoomLimitSmall)
					{
						scaleNew.x -= zoomModifier;
						scaleNew.y -= zoomModifier;
						scaleNew.z -= zoomModifier;
					}

					thisPlanet.transform.localScale = scaleNew;
				}

				if (touchesPrevPosDiff < touchesCurPosDiff)
				{
					if (scaleNew.x < zoomLimitLarge)
					{
						scaleNew.x += zoomModifier;
						scaleNew.y += zoomModifier;
						scaleNew.z += zoomModifier;
					}

					thisPlanet.transform.localScale = scaleNew;
				}
			}
		}

		if (SceneManager.GetActiveScene().buildIndex == 0)
		{
			if (PlanetInfo.selectedPlanet != 4 && PlanetInfo.selectedPlanet != 6)
			{
				zValue = Mathf.Clamp(zValue, 15, 60);
				thisPlanet.transform.localPosition = new Vector3(planetPos.x, planetPos.y, zValue);
			}
			else if (PlanetInfo.selectedPlanet == 4)
			{
				zValue = Mathf.Clamp(zValue, -120, -60);
				thisPlanet.transform.localPosition = new Vector3(planetPos.x, planetPos.y, zValue);
			}
			else if (PlanetInfo.selectedPlanet == 6)
			{
				zValue = Mathf.Clamp(zValue, 30, 70);
				thisPlanet.transform.localPosition = new Vector3(planetPos.x, planetPos.y, zValue);
			}
		}
		else
		{
			// var tempScale = BtnScript.scaleValuePlanets[PlanetInfo.selectedPlanet - 1];
			// var tempScale2 = Mathf.Clamp(tempScale, tempScale - 20, tempScale + 100);
			// thisPlanet.transform.localScale = Vector3.one * tempScale2;
		}
	}
}