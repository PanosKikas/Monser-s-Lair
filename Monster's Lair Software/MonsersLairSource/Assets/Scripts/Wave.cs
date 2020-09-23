using UnityEngine;

[System.Serializable]
public class Wave
{
	// An array with all the enemies on the current wave
	public Enemy[] enemies;
	// The rate at which the enemies on the current wave are spawned
	public float rate;
}

