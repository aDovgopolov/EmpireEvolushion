using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovingScript :MonoBehaviour
{
	#region
	private int unitId = 1;
	public int UnitId
	{
		get => unitId;
		private set => unitId = value;
	}

	[SerializeField]
	private float _fireRate = 0.5f;
	private float _canFire = -1f;

	#endregion

	void Update()
	{
		return;
#if UNITY_EDITOR
		//Debug.Log("#if UNITY_EDITOR");
		if (Input.GetMouseButton(0) || Input.touchCount > 0)
		{
			var v3 = Input.mousePosition;
			v3.z = 10f;
			v3 = Camera.main.ScreenToWorldPoint(v3);

			RaycastHit2D hit = Physics2D.Raycast(v3, Vector2.zero);

			if (hit.collider != null)
			{
				Clicker(hit);
				Vector3 newPos = new Vector3(v3.x, v3.y, 0);
				hit.transform.position = newPos;
				CheckCollisions(v3);
			}
		}

#elif UNITY_ANDROID
		
		//Debug.Log("#elif UNITY_ANDROID");
    if (Input.GetMouseButton(0) || Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			var v3 = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));
			RaycastHit2D hit = Physics2D.Raycast(v3, Vector2.zero);
			Debug.Log(touch.deltaTime);

			switch (touch.phase)
			{
				case TouchPhase.Began:
					Debug.Log("TouchPhase.Began");
					break;
		
				case TouchPhase.Moved:
					Debug.DrawLine(v3, new Vector3(1.6f, 1.7f, 0f), Color.red, Time.deltaTime);

					if (hit.collider != null)
					{
						Vector3 newPos = new Vector3(v3.x, v3.y, 0);//transform.position;
						hit.transform.position = newPos;
						CheckCollisions(v3);
					}
					break;

				case TouchPhase.Ended:
					Debug.Log("TouchPhase.Ended");
					Clicker(hit);	
					break;
			}
		}
#else
		Debug.Log("Any other platform");
		
#endif
	}


	private void Clicker(RaycastHit2D hit)
	{
		if(hit.transform == null)
			return;

		if (Time.time > _canFire)
		{
			_canFire = Time.time + _fireRate;
			hit.transform.GetComponent<Unit>().SpawnCoinByTouch();
		}
	}

	private void CheckCollisions(Vector3 v3)
	{
		RaycastHit2D[] hits = Physics2D.RaycastAll(v3, Vector2.zero);

		if (hits.Length == 2)
		{
			if (hits[0].transform.gameObject.name == hits[1].transform.gameObject.name)
			{
				GameObject objectToCreate = hits[0].transform.GetComponent<Unit>().NextUnitLevel;
				Debug.Log(objectToCreate);
				Destroy(hits[0].transform.gameObject);
				Destroy(hits[1].transform.gameObject);

				if (objectToCreate != null)
				{
					Instantiate(objectToCreate, v3, Quaternion.identity);
					UnitSpawnManager.instance.CountUnitsOnScene -= 1;
				}
			}
		}
	}

}

