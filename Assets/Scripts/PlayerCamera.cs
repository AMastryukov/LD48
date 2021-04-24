using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	[SerializeField] private float maxUpAngle = 45f;
	[SerializeField] private float maxDownAngle = 25f;

	private float speed = 1f;
	private Vector2 rotation;
	private Quaternion startingRotation;

    private void Start()
    {
		rotation = transform.rotation.eulerAngles;
		speed = PlayerPrefs.GetFloat("MouseSensitivity", 1f);
    }

    private void Update()
	{
		PollCameraInput();
		PollInteractionInput();
	}

	private void PollCameraInput()
    {
		rotation.y += Input.GetAxis("Mouse X") * speed;
		rotation.x += -Input.GetAxis("Mouse Y") * speed;

		rotation.x = Mathf.Clamp(rotation.x, -maxUpAngle, maxDownAngle);

		transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);
	}

	private void PollInteractionInput()
    {
		RaycastHit hit;

		if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				if (hit.collider.GetComponent<Interactable>() != null)
                {
					hit.collider.GetComponent<Interactable>().Interact();
                }
			}
		}
    }
}