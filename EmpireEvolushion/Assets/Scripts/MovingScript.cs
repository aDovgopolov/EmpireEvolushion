using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovingScript : MonoBehaviour
{
	private UIManager UIManager;
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

	private void Start()
	{

	}

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
				Clicker(hit, v3);

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


	private void Clicker(RaycastHit2D hit, Vector3 v3)
	{
		if (Time.time > _canFire)
		{
			_canFire = Time.time + _fireRate;
			hit.transform.GetComponent<Unit>().SpawnCoinByTouch();
			//GameObject coin = hit.transform.GetComponent<CointSpawner>()._coin;
			//Instantiate(coin, v3, Quaternion.identity);
			////coin.transform.parent = hit.transform.GetComponent<CointSpawner>().transform;
			//Debug.Log(hit.collider.gameObject.transform);
			//coin.transform.parent = hit.collider.gameObject.transform;
		}
	}

	private void CheckCollisions(Vector3 v3)
	{
		RaycastHit2D[] hits = Physics2D.RaycastAll(v3, Vector2.zero);

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
					//GameManager.instance.CountUnitsOnScene -= 1;
					UIManager.instance.CountUnitsOnScene -= 1;
				}
			}
		}
	}
}

