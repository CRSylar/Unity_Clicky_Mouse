using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
	private float minSpeed = 12;
	private float maxSpeed = 16;
	private float torque = 10;
	private float xRange = 4;
	private float ySpawnPos = -2;
	private Rigidbody targetRb;
	private GameManager gameManager;

	public int pointValue;
	public ParticleSystem[] explosions;
	// Start is called before the first frame update
	void Start()
	{
		gameManager = GameObject.Find( "GameManager" ).GetComponent<GameManager>();
		targetRb = GetComponent<Rigidbody>();

		targetRb.AddForce( RandomForce(), ForceMode.Impulse );
		targetRb.AddTorque( RandomTorque(),
				RandomTorque(),
					RandomTorque(),
						ForceMode.Impulse );

		transform.position = RandomSpawnPos();
	}

	// Update is called once per frame
	void Update()
	{
	}

	private void OnMouseDown()
	{
		if ( gameManager.isGameActive )
		{
			Destroy( gameObject );

			var explosionIdx = Random.Range( 0, explosions.Length );
			Instantiate( explosions[explosionIdx],
					transform.position,
						explosions[explosionIdx].transform.rotation );

			if (gameObject.CompareTag("Bad"))
			{
				gameManager.GameOver();
			}

			gameManager.UpdateScore( pointValue );
		}
	}

	private void OnTriggerEnter( Collider other )
	{
		Destroy( gameObject );
	}

	Vector3 RandomForce()
	{
		return  Vector3.up * Random.Range( minSpeed, maxSpeed );
	}

	float RandomTorque()
	{
		return Random.Range( -torque, torque );
	}

	Vector3 RandomSpawnPos()
	{
		return new Vector3( Random.Range( -xRange, xRange ), ySpawnPos );
	}
}
