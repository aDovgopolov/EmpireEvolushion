using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager :MonoBehaviour
{
	// Properties
	#region
	[HideInInspector]
	public static GameManager instance = null;
	[SerializeField]
	private int _cooldownTime = 5;// 180 = ? 30 sec
	private Camera cam;
	private Text text;

	private int _coinCount = 150;
	public int MyCoinCount
	{
		get
		{
			return _coinCount;
		}
		set
		{
			_coinCount += value;
		}
	}
	#endregion

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		Screen.orientation = ScreenOrientation.Portrait;
		text = GameObject.Find("coin_text").GetComponent<Text>();
	}


	private bool buildSelected = false;
	public bool BuildSelected
	{
		get { return buildSelected; }
		set
		{
			buildSelected = value;
		}
	}

	private void Update()
	{
		//продумать другой вариант 
		UpdateCoinText();

		Scene scene = SceneManager.GetActiveScene();
		if (scene.name.Equals("Main"))
		{
			return;
		}

#if UNITY_EDITOR
		if (Input.GetMouseButton(0))
		{
			var v3 = Input.mousePosition;
			v3.z = 10f;
			v3 = Camera.main.ScreenToWorldPoint(v3);

			RaycastHit2D hit = Physics2D.Raycast(v3, Vector2.zero);
			//Debug.DrawLine(v3, new Vector3(1.6f, 1.7f, 0f), Color.red, Time.deltaTime);

			//Debug.Log(UIManagerMainScene.instance.IsSelectedBuilding);
			if (hit.collider != null && !buildSelected) //&& !UIManagerMainScene.instance.IsSelectedBuilding
			{
				//если нужно пересмтроить / продать дом  этот способ не подходит . Возможно заменить за зажимание  на здании
				if (CheckHitObject(hit))
				{
					return;
				}

				//Debug.Log($"hu {hit.collider.name}");
				cam = Camera.main;
				Vector3 cameraRelative = cam.WorldToScreenPoint(hit.transform.position);
				buildSelected = true;
				UIManagerMainScene.instance.GainBuildingCreateControl(hit.transform.gameObject);
			}else if (hit.collider == null)
			{
				buildSelected = false;
			}
		}

#elif UNITY_ANDROID
		if (Input.GetMouseButton(0) || Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			Debug.Log(touch.deltaTime);
			switch (touch.phase)
			{
				case TouchPhase.Began:
					var v3 = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));

					RaycastHit2D hit = Physics2D.Raycast(v3, Vector2.zero);

					if (hit.collider != null)
					{
						if (CheckHitObject(hit))
						{
							return;
						}
						Debug.Log($"{hit.transform.gameObject.name}");
						UIManagerMainScene.instance.GainBuildingCreateControl(hit.transform.gameObject);
					}
			}
		}
#else
		Debug.Log("Any other platform");
		
#endif


	}


	private bool CheckHitObject(RaycastHit2D hit)
	{
		Building build = hit.collider.gameObject.GetComponent<Building>();
		//Debug.Log($"NO Success {build}");

		if (build != null)
		{
			if (build._IsSomethingBuiltProp)
			{
				build.EnterBuildingScene();
				Debug.Log("Success");
				return true;
			}
		}
		return false;
	}

	private void UpdateCoinText()
	{
		text.text = "" + _coinCount;
	}

	public void SetCoinCount(int coinCount)
	{
		_coinCount += coinCount;
	}

}
