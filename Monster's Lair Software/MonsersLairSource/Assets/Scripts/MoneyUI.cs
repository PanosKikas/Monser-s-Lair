using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour 
{
	[SerializeField]
	Text moneyUI;

	void Update () 
	{
		moneyUI.text = PlayerStats.Money + " G";	
	}
}
