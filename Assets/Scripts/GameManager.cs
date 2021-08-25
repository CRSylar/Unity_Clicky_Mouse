using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
	public List<GameObject>	targets;
	public TextMeshProUGUI	scoreText;
	public TextMeshProUGUI	livesText;
	public TextMeshProUGUI	titleScreen;
	public TextMeshProUGUI	gameOverText;
	public Button			restartButton;
	public AudioSource		audioSource;
	public GameObject		pauseScreen;
	public bool				isGameActive;
	public bool				isPaused;

	private Slider			volumeSlider;
	private int				score = 0;
	private int				lives = 4;
	private float			spawnRate = 1.0f;

	// Use this for initialization
	void Start()
	{
		volumeSlider = GameObject.Find( "VolumeSlider" ).GetComponent<Slider>();

		audioSource.volume = volumeSlider.value;
		volumeSlider.onValueChanged.AddListener(delegate { ChangeAudioValue(); } );
	}

	// Update is called once per frame
	void Update()
	{
		// Aggiungere tasto per pausa
		if ( isGameActive && Input.GetKeyDown( KeyCode.Space ) )
			TooglePause();
		
	}

	public void UpdateScore(int scoreToAdd)
	{
		score += scoreToAdd;
		scoreText.text = "Score: " + score;
	}

	public void UpdateLives(int livesToAdd)
	{
		lives += livesToAdd;
		livesText.text = "Lives: " + lives;
		if ( lives == 0 )
			GameOver();
	}

	public void GameOver()
	{
		restartButton.gameObject.SetActive( true );
		gameOverText.gameObject.SetActive( true );

		StartCoroutine( StopAmbientMusic() );
		isGameActive = false;
	}

	public void StartGame(int difficulty)
	{
		spawnRate /= difficulty;
		titleScreen.gameObject.SetActive( false );
		isGameActive = true;
		StartCoroutine( SpawnTarget() );
		score = 0;
		lives -= difficulty;
		UpdateScore( 0 );
		UpdateLives( 0 );
	}

	public void RestartGame()
	{
		SceneManager.LoadScene( SceneManager.GetActiveScene().name );
	}

	private void ChangeAudioValue()
	{
		audioSource.volume = volumeSlider.value;
	}


	void TooglePause()
	{
		if(!isPaused)
		{
			isPaused = true;
			pauseScreen.SetActive( true );
			Time.timeScale = 0;
		}
		else
		{
			isPaused = false;
			pauseScreen.SetActive( false );
			Time.timeScale = 1;
		}
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

	IEnumerator StopAmbientMusic()
	{
		yield return new WaitForSeconds( 2 );
		audioSource.gameObject.SetActive( false );
	}
}
