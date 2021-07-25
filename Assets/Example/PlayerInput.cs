using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	public float MoveSpeed = 6f;
	public float MouseSens = 3f;

	public Camera cam;
	public float camDistance = 3f;

	float camRotX = -40f;
	float camRotY = 0;

	Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		// get normalized input
		Vector2 input = Vector2.zero;
		if (Input.GetKey("w"))
			input.y = 1;
		if (Input.GetKey("s"))
			input.y = -1;
		if (Input.GetKey("a"))
			input.x = -1;
		if (Input.GetKey("d"))
			input.x = 1;
		input = Vector2.ClampMagnitude(input, 1.0f);

		// base movement on camera direction
		// project camera vectors onto horizontal plane
		Vector3 forward = cam.transform.forward;
		Vector3 right = cam.transform.right;
		forward.y = 0;
		right.y = 0;
		forward.Normalize();
		right.Normalize();

		Vector3 move = forward * input.y + right * input.x;
		transform.position += move * MoveSpeed * Time.deltaTime;


		// camera rotation
		camRotY += Input.GetAxis("Mouse X") * MouseSens;
		camRotX += Input.GetAxis("Mouse Y") * MouseSens * -1;

		// limit up/down rotation
		if (camRotX > 0)
			camRotX = 0;
		else if (camRotX < -89)
			camRotX = -89;

		cam.transform.position = transform.position + Quaternion.Euler(camRotX, camRotY, 0) * (camDistance * -Vector3.back);
		cam.transform.LookAt(transform.position, Vector3.up);


		// rotate player to face camera forwards
		transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
	}
}
