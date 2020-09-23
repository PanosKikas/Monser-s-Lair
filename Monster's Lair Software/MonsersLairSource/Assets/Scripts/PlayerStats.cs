using UnityEngine;

public class PlayerStats : MonoBehaviour 
{
	public static int Money { get; set; }
	public static int Score { get; set; }
	public static int Lives { get; set; }

	[SerializeField]
	private int startMoney = 300;
	[SerializeField]
	private int startLives = 5;

	private void Start()
	{
		Money = startMoney;
		Lives = startLives;
	}
}
