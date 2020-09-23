using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour 
{
	[SerializeField] private Wave[] waves; // An array that contains all the waves of the current level
	[SerializeField] private Transform spawnPoint; // Position at which the enemies are spawning
	[SerializeField] private Text waveCountdown; // Reference to the countdown text 

	public static int enemiesAlive { get; set; }

	float timeBetweenWaves = 5f; // Time in seconds that passes before the spawn of the next wave
	float countdown;

	enum State
	{
		Counting,
		Spawning,
		Waiting
	};

	int waveIndex; // index of the current wave in the waves array
	int enemyIndex; // index of the current enemy in the current wave 

	Wave currentWave; 

	State spawnerState; // The current State of the WaveSpawner

	private void Awake()
	{
		// Find the spawnpoint position
		spawnPoint = transform.GetChild(0).transform;

		if (spawnPoint == null)
		{
			Debug.LogError("Error in WaveSpawner script. No spawnpoint found in childern");	
		}

		if (waves.Length == 0)
		{
			Debug.LogError("Error in WaveSpawner script. No waves to spawn found");
		}
	}
	// Initialization
	void Start () 
	{
		waveIndex = 0;
		currentWave = waves[waveIndex];
		countdown = 10f;
		spawnerState = State.Counting;
		enemiesAlive = 0;
	}
	
	void Update () 
	{
		// if spawner currently spawning wait
		if (spawnerState == State.Spawning)
			return;
		// spawner currently waiting - check to spawn next wave or not
		if (spawnerState == State.Waiting)
		{
			if (enemiesAlive <= 0)
				NextWave();
			else
				return;
		}
		// check if time to spawn the next wave
		if (countdown <= 0 && spawnerState == State.Counting)
		{
			StartCoroutine(SpawnWave());
			return;
		}
		// Update countdown timer
		countdown -= Time.deltaTime;
		countdown = Mathf.Clamp(countdown, 0, Mathf.Infinity);
		waveCountdown.text = string.Format("{0:00.00}", countdown).ToString();
	}
	// Function that spawns the enemies on the current wave
	IEnumerator SpawnWave()
	{
		spawnerState = State.Spawning;
		foreach (Enemy enemy in currentWave.enemies)
		{
			SpawnEnemy(enemy);
			yield return new WaitForSeconds(currentWave.rate);
		}
		spawnerState = State.Waiting;
	}
	// Function that instansiates the current enemy on spawnpoint position
	void SpawnEnemy(Enemy enemy)
	{
		Instantiate(enemy, spawnPoint.position, enemy.transform.rotation);
	}
	// Function that points to the next index of the waves array
	void NextWave()
	{
		if (GameManager.instance.gameIsOver) // Disable this object if the game is already over
		{
			this.enabled = false;
			return;
		}
		// Check if it was the last wave 
		++waveIndex;
		if (waveIndex >= waves.Length )
		{
			GameManager.instance.WinLevel();
			this.enabled = false;
		}
		else
		{
			currentWave = waves[waveIndex];
			countdown = timeBetweenWaves;
			spawnerState = State.Counting;
		}
	}
}
