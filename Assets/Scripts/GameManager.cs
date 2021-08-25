using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
	public List<GameObject> targets;
	public TextMeshProUGUI scoreText;
	public TextMeshProUGUI titleScreen;
	public TextMeshProUGUI gameOverText;
	public Button restartButton;
	public bool isGameActive;

	private int score;
	private float spawnRate = 1.0f;

	// Use this for initialization
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void UpdateScore(int scoreToAdd)
	{
		score += scoreToAdd;
		scoreText.text = "Score: " + score;
	}

	public void GameOver()
	{
		restartButton.gameObject.SetActive( true );
		gameOverText.gameObject.SetActive( true );
		isGameActive = false;
	}

	public void StartGame(int difficulty)
	{
		spawnRate /= difficulty;
		titleScreen.gameObject.SetActive( false );
		isGameActive = true;
		StartCoroutine( SpawnTarget() );
		score = 0;
	}

	public void RestartGame()
	{
		SceneManager.LoadScene( SceneManager.GetActiveScene().name );
	}

	IEnumerator SpawnTarget()
	{
		while (isGameActive)
		{
			yield return new WaitForSeconds( spawnRate );

			int index = Random.Range( 0, targets.Count );
			Instantiate( targets[index] );
		}
	}
}
