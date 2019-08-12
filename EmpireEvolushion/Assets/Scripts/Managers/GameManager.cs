using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISubject
{
	#region Fields
	[HideInInspector]
	public static GameManager instance = null;

	private Camera cam;

	private int _coinCount = 150;
	public int MyCoinCount
	{
		get => _coinCount;
		set => _coinCount += value;
	}

	private bool buildSelected = false;
	public bool BuildSelected
	{
		get => buildSelected;
		set => buildSelected = value;
	}
	#endregion

	#region Methods

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
		//SpecialBusinessLogic();
	}

	private void Update()
	{
		Scene scene = SceneManager.GetActiveScene();
		if (scene.name.Equals("Main"))
		{
			return;
		}

#if UNITY_EDITOR
		Debug.Log("UNITY_EDITOR");
		if (Input.GetMouseButton(0))
		{
			var v3 = Input.mousePosition;
			v3.z = 10f;
			v3 = Camera.main.ScreenToWorldPoint(v3);

			RaycastHit2D hit = Physics2D.Raycast(v3, Vector2.zero);

			if (hit.collider != null && !BuildSelected) //&& !UIManagerMainScene.instance.IsSelectedBuilding
			{
				//если нужно пересмтроить / продать дом  этот способ не подходит . Возможно заменить за зажимание  на здании
				if (CheckHitObject(hit))
				{
					return;
				}

				//cam = Camera.main;
				//Vector3 cameraRelative = Camera.main.WorldToScreenPoint(hit.transform.position);
				buildSelected = true;
				UIManagerMainScene.instance.GainBuildingCreateControl(hit.transform.gameObject);
			}else if (hit.collider == null)
			{
				buildSelected = false;
			}
		}

#elif UNITY_ANDROID
		//Debug.Log("UNITY_ANDROID");
		if (Input.touchCount > 0)//if (Input.GetMouseButton(0) || Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);

			switch (touch.phase)
			{
				case TouchPhase.Began:
					var v3 = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));

					RaycastHit2D hit = Physics2D.Raycast(v3, Vector2.zero);

					if (hit.collider != null&& !BuildSelected)
					{
						if (CheckHitObject(hit))
						{
							return;
						}

						Debug.Log($"{hit.transform.gameObject.name}");
		
						buildSelected = true;
						UIManagerMainScene.instance.GainBuildingCreateControl(hit.transform.gameObject);
					}else if (hit.collider == null)
					{
						buildSelected = false;
					}
				break;
			}
		}
#else
		Debug.Log("Any other platform");
		
#endif


	}

	private bool CheckHitObject(RaycastHit2D hit)
	{
		Building build = hit.collider.gameObject.GetComponent<Building>();

		if (build != null)
		{
			if (build._IsSomethingBuiltProp)
			{
				build.EnterBuildingScene();
				return true;
			}
		}
		return false;
	}

	public void SetCoinCount(int coinCount)
	{
		_coinCount += coinCount;
		SpecialBusinessLogic();
	}

	#endregion

	#region Observer data

	private List<IObserver> _observers = new List<IObserver>();

	public void Attach(IObserver observer)
	{
		Debug.Log("Subject: Attached an observer.");
		this._observers.Add(observer);
		//не по канону
		observer.UpdateUICoin(MyCoinCount);
	}

	public void Detach(IObserver observer)
	{
		this._observers.Remove(observer);
		Debug.Log("Subject: Detached an observer.");
	}

	public void Notify()
	{
		Debug.Log("Subject: Notifying observers...");

		foreach (var observer in _observers)
		{
			observer.UpdateUICoin(MyCoinCount);
		}
	}

	public void SpecialBusinessLogic()
	{
		this.Notify();
	}
	#endregion
}
