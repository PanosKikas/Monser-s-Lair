using UnityEngine;
using UnityEngine.UI;

public class HealthIndicator : MonoBehaviour 
{
	[SerializeField]
	RectTransform healthBar;
	[SerializeField]
	Text healthAmount;
	// Updates the healthbar amount and fillbar 
	public void UpdateStatus(int currentAmount, int maxAmount)
	{

		healthAmount.text = currentAmount + "/" + maxAmount + " HP";
		float amount = (float)currentAmount / maxAmount;
		healthBar.localScale = new Vector3(amount, healthBar.localScale.y, healthBar.localScale.z);
	}
}
