using System.Collections;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	public bool IsLocked { get; set; } = false;

	[SerializeField] private CanvasGroup darkOverlay;
	[SerializeField] private CanvasGroup interactCanvas;
	[SerializeField] private float zoom_fov = 30f;
	[SerializeField] private float zoom_speed = 2f;
	[SerializeField] private float maxUpAngle = 45f;
	[SerializeField] private float maxDownAngle = 25f;

	private float default_fov;
	private float speed = 1f;
	private Vector2 rotation;
	private bool cameraShaking = false;
	private bool cameraFading = false;
	private Camera cam;
	private RaycastHit hit;
	private Interactable activeInteractable;

	private void Start()
    {
		cam = GetComponent<Camera>();
		default_fov = cam.fieldOfView;
		speed = PlayerPrefs.GetFloat("MouseSensitivity", 1f);
		interactCanvas.alpha = 0f;
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

	public void WaveCamera(float duration = 1f, float magnitude = 0.1f)
	{
		if (cameraShaking) return;

		StartCoroutine(CameraWave(duration, magnitude));
	}

	public void FadeCamera(float alpha, float duration = 1f)
    {
		if (cameraFading) return;

		StartCoroutine(FadeCameraCoroutine(alpha, duration));
    }

	private IEnumerator CameraShake(float duration, float magnitude)
    {
		cameraShaking = true;

		Vector3 originalPosition = transform.localPosition;
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

	private IEnumerator CameraWave(float duration, float magnitude)
	{
		cameraShaking = true;

		Quaternion originalRotation = transform.localRotation;
		float timeElapsed = 0f;

		while (timeElapsed < duration)
		{
			float x = Mathf.Sin(timeElapsed) * magnitude;
			float y = Mathf.Cos(timeElapsed) * magnitude;
			float z = Mathf.Sin(timeElapsed) * magnitude;

			transform.localRotation = Quaternion.Euler(originalRotation.eulerAngles.x + x, originalRotation.eulerAngles.y + y, originalRotation.eulerAngles.z + z);

			timeElapsed += Time.deltaTime;

			yield return null;
		}

		transform.localRotation = originalRotation;

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

		if (Input.GetMouseButton(1))
        {
			cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoom_fov, Time.deltaTime * zoom_speed);
        }
        else
        {
			cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, default_fov, Time.deltaTime * zoom_speed);
		}
	}

	private void PollInteractionInput()
    {
		if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
		{
			activeInteractable = hit.collider.GetComponent<Interactable>();

			if (activeInteractable != null)
			{
				interactCanvas.alpha = 1f;

				if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
				{
					activeInteractable.Interact(this, hit);
				}
			}
			else
            {
				interactCanvas.alpha = 0f;
			}
		}
	}
}