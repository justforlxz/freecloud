using UnityEngine;
using System.Collections;

public class mouselook1 : MonoBehaviour {

	public float moveSpeed = 2f;
	public float rotateSpeed = 2f;

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -85F;
	public float maximumY = 10F;

	public float rotationY = 0F;
	public float theDistance = -3.34f;
	public float MaxDistance = -10f;
	public GameObject target;



	public float minFov = 40f;
	public float maxFov = 90f;
	public float sensitivity = 15f;

	void Update ()
	{

		target = GameObject.Find("unitychan");
		if(theDistance>0)
			theDistance = 0;
		if(theDistance < MaxDistance)
			theDistance = MaxDistance;
		//if(Input.GetMouseButton(1))
		//{

		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		if (h != 0 || v != 0)
		{
			Vector3 targetDirection = new Vector3(h, 0, v);
			float y = Camera.main.transform.rotation.eulerAngles.y;
			targetDirection = Quaternion.Euler(0, y, 0) * targetDirection;

			transform.Translate(targetDirection * Time.deltaTime * moveSpeed,Space.World);
		}
		float fov = Camera.main.fieldOfView;
		fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
		fov = Mathf.Clamp(fov, minFov, maxFov);
		Camera.main.fieldOfView = fov;
//		if (Input.GetKey(KeyCode.J))
//		{
//			transform.Rotate(-Vector3.up * Time.deltaTime * rotateSpeed);
//
//		}
			transform.position = target.transform.position;
			if (axes == RotationAxes.MouseXAndY)
			{
				float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
				rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
				if (rotationY>=-4) {
					rotationY = -4;
				}else if (rotationY>=80) {
					rotationY = 80;
				}
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
				transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
				target.transform.localEulerAngles=new Vector3(0, rotationX, 0);
			}
			else if (axes == RotationAxes.MouseX)
			{
				transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
			}
			else
			{
				rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
				transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
			}
			SetDistance();
		    
		//}
		//else
		//{
		//	transform.position = target.transform.position;
		//	SetDistance();
	//	}
	}

	void Start ()
	{
		if (GetComponent<Rigidbody>())
		{
			GetComponent<Rigidbody>().freezeRotation = true;
			transform.position = target.transform.position;
		}
	}

	//设置相机与人物之间的距离
	void SetDistance()
	{
		Cursor.visible = false;
		transform.Translate(Vector3.forward * theDistance);
	}
}
