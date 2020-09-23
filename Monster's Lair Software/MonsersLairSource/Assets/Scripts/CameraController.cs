using UnityEngine;

public class CameraController : MonoBehaviour 
{
	float panSpeed = 8f; // The speed at which the camera is moving
	float panBorderThickness = 10f; // The thickness of the borders 
	float scrollSpeed = 5f; // The speed at which the camera is zooming in or out
	// Limit movement on the X-Y axis
	float maxMoveX; 
	float maxMoveY;
	float minMoveX;
	float minMoveY;

	[SerializeField]
	float mapX = 100.0f; // horizontal dimension of the map
	[SerializeField]
	float mapY = 100.0f; // vertical dimension of the map

	// Limits for zoom in / out
	float maxScroll = 4.5f; 
	float minScroll = 10.5f;

	float vertExtent;
	float horzExtent;
	
	float scroll; 

	private void Start()
	{	
		vertExtent = Camera.main.orthographicSize; // Get the current vertical extent
		horzExtent = vertExtent * Screen.width / Screen.height; // Get the current horizontal extent
		// Calculate the bounds for the x and y axis depending on the map size and the extent
		minMoveX = horzExtent - mapX / 2.0f; 
		maxMoveX = mapX / 2.0f - horzExtent;
		minMoveY = vertExtent - mapY / 2.0f;
		maxMoveY = mapY / 2.0f - vertExtent;
	}

	void Update () 
	{
		// Move the camera with panSpeed when the player presses one of the wasd keys or the mouse enters the bounds of panBorder
		if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panBorderThickness)
		{
			transform.Translate(Vector2.up * panSpeed * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorderThickness)
		{
			transform.Translate(Vector2.down * panSpeed * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBorderThickness)
		{
			transform.Translate(Vector2.left * panSpeed * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorderThickness)
		{
			transform.Translate(Vector2.right * panSpeed * Time.deltaTime);
		}

		scroll = Input.GetAxis("Mouse ScrollWheel"); // Detect mouse scrolling
		// Constraint Camera to fit the bounds of the map
		float camSize = GetComponent<Camera>().orthographicSize;
		camSize -= scroll * 30 * scrollSpeed * Time.deltaTime;
		camSize = Mathf.Clamp(camSize, maxScroll, minScroll); // Clamp the scrolling of the camera between max and min scroll
		this.GetComponent<Camera>().orthographicSize = camSize;

		vertExtent = Camera.main.orthographicSize;
		horzExtent = vertExtent * Screen.width / Screen.height;
		// Calculate the new borders depending on the zoom of the camera
		minMoveX = horzExtent - mapX / 2.0f; 
		maxMoveX = mapX / 2.0f - horzExtent;
		minMoveY = vertExtent - mapY / 2.0f;
		maxMoveY = mapY / 2.0f - vertExtent;
		// Clamp the position between max and min on X and Y 
		var v3 = transform.position;
		v3.x = Mathf.Clamp(v3.x, minMoveX, maxMoveX);
		v3.y = Mathf.Clamp(v3.y, minMoveY, maxMoveY);
		transform.position = v3; 
	}
}
