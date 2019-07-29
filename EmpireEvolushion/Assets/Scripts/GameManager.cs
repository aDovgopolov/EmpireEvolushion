using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager :MonoBehaviour
{
	[SerializeField]
	private GameObject _scrollbar;
	public GameObject _prefab;
	[SerializeField]
	private int _unitsOnScene = 0;
	[SerializeField]
	private int _maxUnitsOnScene = 5;
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
	private Text text;
	public int CountUnitsOnScene
	{
		get
		{
			return _unitsOnScene;
		}
		set
		{
			if (value >= 0)
			{
				_unitsOnScene = value;
			}
		}
	}
	[HideInInspector]
	public static GameManager instance = null;
	[SerializeField]
	private int _cooldownTime = 5;// 180 = ? 30 sec
	Camera cam;


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
		//StartCoroutine(LoadSavedScene());
	}

	private void Update()
	{

#if UNITY_ANDROID
		if (Input.GetMouseButton(0))
		{
			var v3 = Input.mousePosition;
			v3.z = 10f;
			v3 = Camera.main.ScreenToWorldPoint(v3);

			RaycastHit2D hit = Physics2D.Raycast(v3, Vector2.zero);
			Debug.DrawLine(v3, new Vector3(1.6f, 1.7f, 0f), Color.red, Time.deltaTime);
			
			if (hit.collider != null)
			{
				CheckHitObject(hit);
				Debug.Log($"hu {hit.collider.name}");
				cam = Camera.main;
				Vector3 cameraRelative = cam.WorldToScreenPoint(hit.transform.position);
				UIManagerMainScene.instance.GainBuildingCreateControl(hit.transform.gameObject);
			}
		}

#elif UNITY_EDITOR
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
						Debug.Log($"{hit.transform.gameObject.name}");
						UIManagerMainScene.instance.GainBuildingCreateControl(hit.transform.gameObject);
					}
			}
		}
#else
    Debug.Log("Any other platform");
		
#endif

		UpdateCoinText();
	}


	private void CheckHitObject(RaycastHit2D hit)
	{
		Building build = hit.collider.gameObject.GetComponent<Building>();
		Debug.Log($"NO Success {build}");
		if (build != null){
			if(build._IsSomethingBuiltProp)
			{
				build.YAYA();
				Debug.Log("Success");
			}
		}
	}

	private void UpdateCoinText()
	{
		Scene scene = SceneManager.GetActiveScene();
		//if (scene.name.Equals("MainScene"))
		//{
		//	Debug.Log("MainScene  / set count");
		//	text.text = "" + _coinCount;
		//}
		//Debug.Log($" UpdateCoinText() {_coinCount}");
		text.text = "" + _coinCount;
	}

	public IEnumerator LoadSavedScene()
	{
		_scrollbar.GetComponent<Scrollbar>().size = 0;

		int i = 0;
		while (true)
		{
			_scrollbar.GetComponent<Scrollbar>().size += 1 / 180f;

			i++;

			if (i == _cooldownTime)
			{
				i = 0;
				if (_unitsOnScene < _maxUnitsOnScene)
				{
					Instantiate(_prefab, new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(2f, -3.5f), 0), Quaternion.identity);
					_unitsOnScene++;
				}

				_scrollbar.GetComponent<Scrollbar>().size = 0;
			}

			yield return new WaitForSeconds(0.1f);
		}
	}

	public void SetCoinCount(int coinCount)
	{
		_coinCount = coinCount;
	}

}
