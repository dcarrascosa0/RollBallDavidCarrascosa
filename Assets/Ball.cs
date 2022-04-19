using UnityEngine;

// Include the namespace required to use Unity UI and Input System
using TMPro;

public class Ball : MonoBehaviour
{

	// Create public variables for player speed, and for the Text UI game objects
	public float speed;
	public TextMeshProUGUI countText;
	public GameObject winTextObject;
	private float minPosX = -10f;
	private float maxPosX = 25f;
	private float minPosZ = -12f;
	private float maxPosZ = 20f;
	public GameObject prize;



	private Rigidbody rb;
	private int count;

	// At the start of the game..
	void Start()
	{
		// Assign the Rigidbody component to our private rb variable
		rb = GetComponent<Rigidbody>();

		// Set the count to zero 
		count = 15;

		SetCountText();

		// Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
		winTextObject.SetActive(false);
		for(int i = 0; i < count; i++)
        {
			GameObject newPrize = Instantiate(prize, new Vector3(Random.Range(minPosX, maxPosX), 3f, Random.Range(minPosZ, maxPosZ)),Quaternion.identity,transform.parent);
			
        }
	}

	void FixedUpdate()
	{
		// Create a Vector3 variable, and assign X and Z to feature the horizontal and vertical float variables above

		float forceHorizontal = Input.GetAxis("Horizontal");
		float forceVertical = Input.GetAxis("Vertical");
		Move(forceHorizontal, forceVertical);
	}

    private void Move(float forceHorizontal, float forceVertical)
    {
		
		Vector3 movment = new Vector3(forceHorizontal, 0f, forceVertical);
		rb.AddForce(movment * speed,ForceMode.Acceleration);

	}

    void OnTriggerEnter(Collider other)
	{
		// ..and if the GameObject you intersect has the tag 'Pick Up' assigned to it..
		if (other.transform.tag=="Prize")
		{
			Destroy(other.transform.gameObject);

			// Add one to the score variable 'count'
			count -=1;

			// Run the 'SetCountText()' function (see below)
			SetCountText();
		}
	}

	

	void SetCountText()
	{
		countText.text = "Remaining " + count.ToString();

		if (count <= 0)
		{
			// Set the text value of your 'winText'
			winTextObject.SetActive(true);
		}
	}
}