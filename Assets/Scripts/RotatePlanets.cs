using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class RotatePlanets : MonoBehaviour
{
	#region Variables

	private int speed;

	#endregion

	private void Awake()
	{
		if (SceneManager.GetActiveScene().buildIndex == 0)
		{
			speed = Random.Range(5, 20);
		}
		else
		{
			speed = Random.Range(15, 30);
		}
	}

	private void Update()
	{
		transform.Rotate(0, speed * Time.deltaTime, 0);
	}
}