using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	public List<GameObject> targets;

	private float spawnRate = 1.0f;

	// Use this for initialization
	void Start()
	{
		StartCoroutine( SpawnTarget() );
	}

	// Update is called once per frame
	void Update()
	{

	}

	IEnumerator SpawnTarget()
	{
		while (true)
		{
			yield return new WaitForSeconds( spawnRate );

			int index = Random.Range( 0, targets.Count );
			Instantiate( targets[index] );
		}
	}
}
