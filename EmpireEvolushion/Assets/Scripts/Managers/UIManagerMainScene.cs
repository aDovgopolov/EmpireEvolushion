using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManagerMainScene : MonoBehaviour
{
	#region Fields
	[HideInInspector]
	public static UIManagerMainScene instance = null;

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
	[SerializeField]
	private GameObject _scrollbar;

	public Sprite imageBtn1Sprite1;
	public Sprite millImage;
	public Sprite imageBtn1Sprite2;

	private Canvas canvas;
	private GameObject _selectedBuilding;
	public GameObject _prefab;

	private bool _isSelectedBuilding = false;
	public bool IsSelectedBuilding
	{
		get => _isSelectedBuilding;
		//set => _isSelectedBuilding = value;
	}

	private bool _coroutineStarted = false;

	public int millPrice = 100;

	[SerializeField]
	private int _unitsOnScene = 0;
	[SerializeField]
	private int _maxUnitsOnScene = 5;
	[SerializeField]
	private int _cooldownTime = 5;

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
		GameObject obj = GameObject.Find("Canvas");
		canvas = obj.GetComponent<Canvas>();
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
		//Empty logic yet
	}

	public void UpdateMeelImage()
	{
		if (!_isSelectedBuilding)
		{
			_image.GetComponent<Image>().sprite = imageBtn1Sprite2;
			_isSelectedBuilding = true;
		}
		else if (GameManager.instance.MyCoinCount < millPrice)
		{
			//Debug.Log("NOT ENOUGH COINS");
			NotEnoughMoneyMessage();
			GameManager.instance.BuildSelected = false;
		}
		else
		{
			GameManager.instance.BuildSelected = false;
			GameManager.instance.MyCoinCount = -millPrice;
			_selectedBuilding.GetComponent<SpriteRenderer>().sprite = millImage;

			/*что лучше ниже? */
			//_selectedBuilding.GetComponent<Building>().SetIsSomethingBuilt();
			_selectedBuilding.GetComponent<Building>()._IsSomethingBuiltProp = true;

			_isSelectedBuilding = false;
			_image.GetComponent<Image>().sprite = imageBtn1Sprite1;

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
		//_isSelectedBuilding = true;
		_buildMenuPanel.SetActive(true);
		_buildMenuPanel.transform.position = WorldToUISpace(canvas, hit.transform.localPosition);
	}

	public Vector3 WorldToUISpace(Canvas parentCanvas, Vector3 worldPos)
	{
		Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
		Vector2 movePos;

		RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, screenPos, parentCanvas.worldCamera, out movePos);
		/*
		 * Костыль movePos.y + 20
		 */
		return parentCanvas.transform.TransformPoint(new Vector3(movePos.x, movePos.y + 35));
	}



	public void DisablePanelsBeforeSceneLoad()
	{
		Debug.Log("DisablePanelsBeforeSceneLoad()");
		_mapButton.gameObject.SetActive(true);
		_trophyButton.gameObject.SetActive(false);
		_missionButton.gameObject.SetActive(false);
		_buildMenuPanel.SetActive(false);
		//Debug.Break();
	}

	public void EnablePanelsBeforeSceneLoad()
	{
		Debug.Log("EnablePanelsBeforeSceneLoad()");
		_mapButton.gameObject.SetActive(false);
		_trophyButton.gameObject.SetActive(true);
		_missionButton.gameObject.SetActive(true);
		_buildMenuPanel.SetActive(false);
		//Debug.Break();
	}

	public void LoadMainMap()
	{
		Debug.Log("LoadMainMap()");
		SceneManager.LoadScene("MainScene");
		EnablePanelsBeforeSceneLoad();
	}

}
