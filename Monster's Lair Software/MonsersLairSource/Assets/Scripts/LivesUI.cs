using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour 
{
	[SerializeField]
	Text livesCount;
	
	void Update()
	{
		if (PlayerStats.Lives < 0)
		{
			return;
		}
		livesCount.text = PlayerStats.Lives.ToString();
	}
}
