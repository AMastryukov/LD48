using System.Collections;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	public bool IsLocked { get; set; } = false;

	[SerializeField] private CanvasGroup darkOverlay;
	[SerializeField] private float maxUpAngle = 45f;
	[SerializeField] private float maxDownAngle = 25f;

	private float speed = 1f;
	private Vector2 rotation;
	private bool cameraShaking = false;
	private bool cameraFading = false;

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
		if (IsLocked) return;

		PollCameraInput();
		PollInteractionInput();
	}

	public void ShakeCamera(float duration = 1f, float magnitude = 0.025f)
    {
		if (cameraShaking) return;

		StartCoroutine(CameraShake(duration, magnitude));
	}

	public void FadeCamera(float alpha, float duration = 1f)
    {
		if (cameraFading) return;

		StartCoroutine(FadeCameraCoroutine(alpha, duration));
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

	private IEnumerator FadeCameraCoroutine(float alpha, float duration)
    {
		cameraFading = true;

		float originalAlpha = darkOverlay.alpha;
		float timeElapsed = 0f;

		while (timeElapsed < duration)
        {
			darkOverlay.alpha = Mathf.Lerp(originalAlpha, alpha, timeElapsed / duration);

			timeElapsed += Time.deltaTime;

			yield return null;
        }

		darkOverlay.alpha = alpha;

		cameraFading = false;
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
		if (Input.GetKeyDown(KeyCode.E))
		{
			RaycastHit hit;

			if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
			{
				if (hit.collider.GetComponent<Interactable>() != null)
                {
					hit.collider.GetComponent<Interactable>().Interact(this, hit);
                }
			}
		}
	}
}