using UnityEngine;

internal class RotatableObject : MonoBehaviour
{
	public float angleX;

	public float angleY;

	public float angleZ;

	private Transform thistransform;

	private void Awake()
	{
		thistransform = GetComponent<Transform>();
	}

	private void Update()
	{
		if ((angleX > 0f) | (angleY > 0f) | (angleZ > 0f))
		{
			thistransform.Rotate(Vector3.up, angleY * Time.deltaTime);
			thistransform.Rotate(Vector3.forward, angleX * Time.deltaTime);
			thistransform.Rotate(Vector3.right, angleZ * Time.deltaTime);
		}
	}
}
