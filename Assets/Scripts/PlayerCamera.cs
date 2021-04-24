using System.Collections;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	[SerializeField] private float maxUpAngle = 45f;
	[SerializeField] private float maxDownAngle = 25f;

	private float speed = 1f;
	private Vector2 rotation;
	private bool cameraShaking = false;

    private void Start()
    {
		speed = PlayerPrefs.GetFloat("MouseSensitivity", 1f);
    }

    private void OnEnable()
    {
		rotation = transform.rotation.eulerAngles;

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

    private void OnDisable()
    {
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

    private void Update()
	{
		PollCameraInput();
		PollInteractionInput();

		if (Input.GetKeyDown(KeyCode.Z))
        {
			ShakeCamera();
        }
	}

	public void ShakeCamera(float duration = 1f, float magnitude = 0.025f)
    {
		if (cameraShaking) return;

		StartCoroutine(CameraShake(duration, magnitude));
	}

	private IEnumerator CameraShake(float duration, float magnitude)
    {
		cameraShaking = true;

		Vector3 originalPosition = transform.position;
		float timeElapsed = 0f;

		while (timeElapsed < duration)
        {
			float x = Random.Range(-1f, 1f) * magnitude;
			float y = Random.Range(-1f, 1f) * magnitude;
			float z = Random.Range(-1f, 1f) * magnitude;

			transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z + z);

			timeElapsed += Time.deltaTime;

			yield return null;
        }

		transform.localPosition = originalPosition;

		cameraShaking = false;
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