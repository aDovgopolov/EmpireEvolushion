using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManagerMainScene :MonoBehaviour
{
	[SerializeField]
	private Button _trophyButton; 
	[SerializeField]
	private Button _missionButton;
	[SerializeField]
	private Button _mapButton;
	[SerializeField]
	private GameObject _achivkaPanel;
	[SerializeField]
	private Text _notEnoughMoneyText;
	[SerializeField]
	private GameObject _questPanel;
	[SerializeField]
	private GameObject _mainScreenPanel;
	[SerializeField]
	public GameObject _buildMenuPanel;
	[SerializeField]
	private Button _btn1Meel;
	[SerializeField]
	private Image _image;
	public Sprite imageBtn1Sprite1;
	public Sprite millImage;
	public Sprite imageBtn1Sprite2;
	private bool _isSelectedBuilding = false;
	private GameObject _selectedBuilding;

	//private int coinScore;

	[SerializeField]
	private GameObject _scrollbar;
	public GameObject _prefab;

	[HideInInspector]
	public static UIManagerMainScene instance = null;

	Canvas canvas;

	public int millPrice = 100;
	private bool _coroutineStarted = false;



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
	[SerializeField]
	private int _unitsOnScene = 0;
	[SerializeField]
	private int _maxUnitsOnScene = 5;
	[SerializeField]
	private int _cooldownTime = 5;



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
		//coinScore = GameManager.instance.MyCoinCount;
		GameObject obj = GameObject.Find("Canvas");
		canvas = obj.GetComponent<Canvas>();
	}

	void Update()
	{
		Scene scene = SceneManager.GetActiveScene();
		if (scene.name.Equals("MainScene"))
		{
			Debug.Log(scene.name);
			//_mapButton.gameObject.SetActive(false);
			//_trophyButton.gameObject.SetActive(true);
			//_missionButton.gameObject.SetActive(true);
		}

		//if (scene.name.Equals("Main") && !_coroutineStarted)
		//{
		//	_coroutineStarted = true;
		//	StartCoroutine(StartSpawnUnits());
		//}

	}

	public void OpenMissionMenu()
	{
		Debug.Log("OpenMissionMenu()");
		_mainScreenPanel.SetActive(false);
		_questPanel.SetActive(true);
	}

	public void OpenTrothyMenu()
	{
		_mainScreenPanel.SetActive(false);
		_achivkaPanel.SetActive(true);
	}

	public void ClosePanel()
	{
		_questPanel.SetActive(false);
		_achivkaPanel.SetActive(false);
		_mainScreenPanel.SetActive(true);
	}

	public void OpenSettingsMenu()
	{
		Debug.Log("OpenSettingsMenu()");
	}

	public void UpdateMeelImage()
	{
		//Debug.Log("UpdateMeelImage()");
		if (!_isSelectedBuilding)
		{
			_image.GetComponent<Image>().sprite = imageBtn1Sprite2;
			_isSelectedBuilding = true;
		}
		else if (GameManager.instance.MyCoinCount < millPrice)
		{
			//Debug.Log("NOT ENOUGH COINS");
			NotEnoughMoneyMessage();
		}
		else
		{
			// build meel
			//Debug.Log("build meel");
			GameManager.instance.MyCoinCount = -millPrice;
			_selectedBuilding.GetComponent<SpriteRenderer>().sprite = millImage;
			_selectedBuilding.GetComponent<Building>().SetIsSomethingBuilt();
			_isSelectedBuilding = false;
			_image.GetComponent<Image>().sprite = imageBtn1Sprite1;

			//hit.collider.gameObject.GetComponent<Building>()._IsSomethingBuiltProp = true;
			_buildMenuPanel.SetActive(false);
		}
	}

	private void NotEnoughMoneyMessage()
	{
		_notEnoughMoneyText.GetComponent<Text>().enabled = true;
		StartCoroutine(TextDisaper());
		_isSelectedBuilding = false;
		_image.GetComponent<Image>().sprite = imageBtn1Sprite1;
		_buildMenuPanel.SetActive(false);
	}

	IEnumerator TextDisaper()
	{
		yield return new WaitForSeconds(2f);
		_notEnoughMoneyText.GetComponent<Text>().enabled = false;
	}

	public void GainBuildingCreateControl(GameObject hit)
	{
		_selectedBuilding = hit;
		_buildMenuPanel.SetActive(true);
		_buildMenuPanel.transform.position = WorldToUISpace(canvas, hit.transform.localPosition);
	}

	public Vector3 WorldToUISpace(Canvas parentCanvas, Vector3 worldPos)
	{
		//Convert the world for screen point so that it can be used with ScreenPointToLocalPointInRectangle function
		Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
		Vector2 movePos;

		//Convert the screenpoint to ui rectangle local point
		RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, screenPos, parentCanvas.worldCamera, out movePos);
		//Convert the local point to world point
		/*
		 * Костыль movePos.y + 20
		 */
		return parentCanvas.transform.TransformPoint(new Vector3(movePos.x, movePos.y + 20));
	}



	public void DisablePanelsBeforeSceneLoad()
	{
		Debug.Log("DisablePanelsBeforeSceneLoad()");
		_mapButton.gameObject.SetActive(true);
		_trophyButton.gameObject.SetActive(false);
		_missionButton.gameObject.SetActive(false);
		_buildMenuPanel.SetActive(false);
		Debug.Break();
	}

	public void EnablePanelsBeforeSceneLoad()
	{
		Debug.Log("EnablePanelsBeforeSceneLoad()");
		_mapButton.gameObject.SetActive(false);
		_trophyButton.gameObject.SetActive(true);
		_missionButton.gameObject.SetActive(true);
		_buildMenuPanel.SetActive(false);
		Debug.Break();
	}

	public void LoadMainMap()
	{
		Debug.Log("LoadMainMap()");
		SceneManager.LoadScene("MainScene");
		EnablePanelsBeforeSceneLoad();
	}



	public IEnumerator StartSpawnUnits()
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



	//public void UpdateScoreCoinText(int score)
	//{
	//	coinScore += score;
	//	//_scoreText.text = "" + coinScore;
	//	GameManager.instance.SetCoinCount(coinScore);
	//}
}
