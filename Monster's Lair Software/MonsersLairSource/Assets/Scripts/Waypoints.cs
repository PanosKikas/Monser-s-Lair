using UnityEngine;

public class Waypoints : MonoBehaviour 
{
	public static Transform[] points;
	// Initialize the points array with the position of the Waypoints
	private void Awake()
	{
		points = new Transform[transform.childCount];
		for (int i = 0; i != transform.childCount; ++i)
		{
			points[i] = transform.GetChild(i);
		}
	}
}
