using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovingScript :MonoBehaviour //, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
	//private Vector3 touchPosition;
	//private Rigidbody2D rb;
	//private Vector3 direction;
	//private float _speed = 10f;
	//private BoxCollider2D collider2D;
	////------------------------
	//Vector3 touchPosWorld;
	//TouchPhase touchPhase = TouchPhase.Ended;
	//------------------------

	public GameObject myObject;
	private int unitId = 1;
	public int MyUnitId
	{
		get
		{
			return unitId;
		}
		private set
		{
		}
	}

	[SerializeField]
	private float _fireRate = 0.5f;
	private float _canFire = -1f;


	void Update()
	{
#if UNITY_ANDROID

		if (Input.GetMouseButton(0) || Input.touchCount > 0)
		{
			var v3 = Input.mousePosition;
			v3.z = 10f;
			v3 = Camera.main.ScreenToWorldPoint(v3);

			RaycastHit2D hit = Physics2D.Raycast(v3, Vector2.zero);
			//Debug.DrawLine(v3, new Vector3(1.6f, 1.7f, 0f), Color.red, Time.deltaTime);

			if (hit.collider != null)
			{
				if(Time.time > _canFire)
				{
					_canFire = Time.time + _fireRate;
					CointSpawner cointSpawner = hit.transform.GetComponent<CointSpawner>();
					GameObject coin = hit.transform.GetComponent<CointSpawner>()._coin;
					coin.transform.parent = cointSpawner.transform;
					Instantiate(coin, v3, Quaternion.identity);
				}

				//Debug.Log(hit.transform.gameObject.name);
				Vector3 newPos = new Vector3(v3.x, v3.y, 0);
				hit.transform.position = newPos;
				CheckCollisions(v3);
			}
		}
#elif UNITY_EDITOR
    if (Input.GetMouseButton(0) || Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			Debug.Log(touch.deltaTime);
			switch (touch.phase)
			{
				//When a touch has first been detected, change the message and record the starting position
				case TouchPhase.Began:
					// Record initial touch position.
					Debug.Log("TouchPhase.Began");
					break;

				//Determine if the touch is a moving touch
				case TouchPhase.Moved:
					var v3 = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));
					//var v3 = Input.mousePosition;
					//v3.z = 10f;
					//v3 = Camera.main.ScreenToWorldPoint(v3);

					RaycastHit2D hit = Physics2D.Raycast(v3, Vector2.zero);
					Debug.DrawLine(v3, new Vector3(1.6f, 1.7f, 0f), Color.red, Time.deltaTime);

					if (hit.collider != null)
					{
						//Debug.Log(hit.transform.gameObject.name);
						Vector3 newPos = new Vector3(v3.x, v3.y, 0);//transform.position;

						//newPos.x = v3.x;
						//newPos.y = v3.y;
						//newPos.z = 0;

						//Vector2.Lerp(newPos, hit.transform.position,  moveSpeed);
						hit.transform.position = newPos;
						CheckCollisions(v3);
					}
					break;

				case TouchPhase.Ended:
					// Report that the touch has ended when it ends
					Debug.Log("TouchPhase.Ended");
					break;
			}
		}
#else
    Debug.Log("Any other platform");
		
#endif
	}


	private void CheckCollisions(Vector3 v3)
	{
		RaycastHit2D[] hits = Physics2D.RaycastAll(v3, Vector2.zero);
		//Debug.Log(hits.Length);

		if (hits.Length == 2)
		{
			if (hits[0].transform.gameObject.name == hits[1].transform.gameObject.name)
			{
				GameObject objectToCreate = hits[0].transform.GetComponent<Unit>().MyObject;
				Debug.Log(objectToCreate);
				Destroy(hits[0].transform.gameObject);
				Destroy(hits[1].transform.gameObject);

				if(objectToCreate != null){
					Instantiate(objectToCreate, v3, Quaternion.identity);
					GameManager.instance.CountUnitsOnScene -= 1;
				}
			}
			//Debug.Log("Physics.RaycastAll = " + hits[i].transform.gameObject.name);
		}
	}


	public void old4()
	{
		//RaycastHit2D ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Input.GetTouch(0).position
		//RaycastHit raycastHit;
		//Debug.Log(Input.touchCount + $" = {ray}");

		//if (Physics2D.Raycast(ray, out raycastHit))
		//{
		//	Debug.Log("In the Physics.Raycast(ray, out raycastHit)");
		//	var rig = raycastHit.collider.GetComponent<Rigidbody>();
		//	Instantiate(particle, transform.position, transform.rotation);
		//}


		//if (Input.GetTouch(0).phase == TouchPhase.Began)
		//{
		//	// Construct a ray from the current touch coordinates

		//}
		//for (int i = 0; i < Input.touchCount; ++i)
		//{
		//}

	}

	public void old3()
	{
		if (Input.GetMouseButton(0) || Input.touchCount > 0) // && Input.GetTouch(0).phase == touchPhase
		{


			//Debug.Log(Input.GetTouch(0).position );
			//Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			//RaycastHit raycastHit;
			//if (Physics.Raycast(raycast, out raycastHit))
			//{
			//	Debug.Log($"Something Hit {raycastHit.collider.name}");
			//	if (raycastHit.collider.name == "Soccer")
			//	{
			//		Debug.Log("Soccer Ball clicked");
			//	}

			//	//OR with Tag

			//	if (raycastHit.collider.CompareTag("SoccerTag"))
			//	{
			//		Debug.Log("Soccer Ball clicked");
			//	}
			//}

			//Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			//Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
			//Debug.Log($" {worldPos} - {touchPos}  /  " + Physics2D.OverlapPoint(touchPos));
			//if (collider2D ==)
			//{
			//	//Do stuff with it here like check gameObject tags and such.
			//}





			//if (Input.GetTouch(0).phase == TouchPhase.Began)
			//{
			//	RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero);
			//	Debug.Log(hitInfo.transform.gameObject.name);
			//	// RaycastHit2D can be either true or null, but has an implicit conversion to bool, so we can use it like this
			//	if (hitInfo)
			//	{
			//		Debug.Log(hitInfo.transform.gameObject.name);
			//		// Here you can check hitInfo to see which collider has been hit, and act appropriately.
			//	}
			//}






			//Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			//Vector2 touchPos = new Vector2(wp.x, wp.y);
			//Debug.Log($"{wp } = {touchPos} ");
			//if (collider2D == Physics2D.OverlapPoint(touchPos))
			//{
			//	//your code
			//	Debug.Log("Touched ");
			//}

			//Touch touch = Input.GetTouch(0); 
			//Debug.Log($"{touch }");
			//Vector2 position = touch.position; 
			//Debug.Log($"{position }");
			//--------------------------------------------------------------------------------------------
			//Vector3 position = Input.mousePosition;
			//touchPosWorld = Camera.main.ScreenToWorldPoint(position);
			//Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);

			//Debug.Log($"{position } = {touchPosWorld}  = {touchPosWorld2D}");
			////We now raycast with this information. If we have hit something we can process it.
			//RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);

			//if (hitInformation.collider != null)
			//{
			//	//We should have hit something with a 2D Physics collider!
			//	GameObject touchedObject = hitInformation.transform.gameObject;
			//	//touchedObject should be the object someone touched.
			//	Debug.Log("Touched " + touchedObject.transform.name);
			//}
		}
	}
	public void old2()
	{
		//if (Input.touchCount > 0)
		//{
		//	Touch touch = Input.GetTouch(0);
		//	touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
		//	touchPosition.z = -10f;

		//	direction = (touchPosition - transform.position);
		//	rb.velocity = new Vector2(direction.x, direction.y) * _speed;

		//	if (touch.phase == TouchPhase.Ended)
		//	{
		//		rb.velocity = Vector2.zero;
		//	}
		//}
	}

	//void Update()
	//{
	//	if (Input.GetMouseButton(0) || Input.touchCount > 0)
	//	{
	//		Touch touch = Input.GetTouch(0);

	//		switch (touch.phase)
	//		{
	//			//When a touch has first been detected, change the message and record the starting position
	//			case TouchPhase.Began:
	//				// Record initial touch position.
	//				Debug.Log("TouchPhase.Began");
	//				break;

	//			//Determine if the touch is a moving touch
	//			case TouchPhase.Moved:
	//				var v3 = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));
	//				//var v3 = Input.mousePosition;
	//				//v3.z = 10f;
	//				//v3 = Camera.main.ScreenToWorldPoint(v3);

	//				RaycastHit2D hit = Physics2D.Raycast(v3, Vector2.zero);
	//				Debug.DrawLine(v3, new Vector3(1.6f, 1.7f, 0f), Color.red, Time.deltaTime);

	//				if (hit.collider != null)
	//				{
	//					Debug.Log(hit.transform.gameObject.name);
	//					Vector3 newPos = new Vector3(v3.x, v3.y, 0);//transform.position;

	//					//newPos.x = v3.x;
	//					//newPos.y = v3.y;
	//					//newPos.z = 0;

	//					//Vector2.Lerp(newPos, hit.transform.position,  moveSpeed);
	//					hit.transform.position = newPos;
	//					CheckCollisions(v3);
	//				}
	//				break;

	//			case TouchPhase.Ended:
	//				// Report that the touch has ended when it ends
	//				Debug.Log("TouchPhase.Ended");
	//				break;
	//		}
	//	}
	//}

	public void old()
	{
		// if left-mouse-button is being held OR there is at least one touch
		if (Input.GetMouseButton(0) || Input.touchCount > 0)
		{
			// get mouse position in screen space
			// (if touch, gets average of all touches)
			Vector3 screenPos = Input.mousePosition;
			// set a distance from the camera
			screenPos.z = 10.0f;
			// convert mouse position to world space
			Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

			// get current position of this GameObject
			Vector3 newPos = transform.position;
			// set x position to mouse world-space x position
			newPos.x = worldPos.x;
			newPos.y = worldPos.y;
			// apply new position
			transform.position = newPos;
		}
	}
}

