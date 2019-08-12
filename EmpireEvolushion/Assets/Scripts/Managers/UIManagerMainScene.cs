using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManagerMainScene : MonoBehaviour, IObserver
{
	#region Fields
	[HideInInspector]
	public static UIManagerMainScene instance = null;

	private Canvas canvas;

	[SerializeField]
	private GameObject _questPanel;
	[SerializeField]
	private GameObject _mainScreenPanel;
	[SerializeField]
	public GameObject _buildMenuPanel;
	[SerializeField]
	private GameObject _achivkaPanel;

	[SerializeField]
	private Button _trophyButton; 
	[SerializeField]
	private Button _missionButton;
	[SerializeField]
	private Button _mapButton;
	[SerializeField]
	private Button _btn1Meel;

	[SerializeField]
	private Text _notEnoughMoneyText;
	private Text _CoinText;

	[SerializeField]
	private Image _image;

	[SerializeField]
	private GameObject _scrollbar;

	public Sprite imageBtn1Sprite1;
	public Sprite millImage;
	public Sprite imageBtn1Sprite2;

	private GameObject _selectedBuilding;

	private bool _isSelectedBuilding = false;
	public bool IsSelectedBuilding
	{
		get => _isSelectedBuilding;
		//set => _isSelectedBuilding = value;
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
		_CoinText = GameObject.Find("coin_text").GetComponent<Text>();
	}

	void Start()
	{
		GameManager.instance.Attach(this);
		GameObject obj = GameObject.Find("Canvas");
		canvas = obj.GetComponent<Canvas>();
	}

	public void OpenMissionMenu()
	{
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
		else if (GameManager.instance.MyCoinCount < BuildingManager.instance.millPrice)
		{
			//Debug.Log("NOT ENOUGH COINS");
			NotEnoughMoneyMessage();
			GameManager.instance.BuildSelected = false;
		}
		else
		{
			GameManager.instance.BuildSelected = false;
			GameManager.instance.SetCoinCount(-BuildingManager.instance.millPrice);
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
		Debug.Log($"canvas = { canvas} / hit = {hit} / hit.transform.localPosition = {hit.transform.localPosition} / _buildMenuPanel = {_buildMenuPanel}");
		_buildMenuPanel.transform.position = WorldToUISpace(canvas, hit.transform.localPosition);
		Debug.Log("GainBuildingCreateControl");
	}

	public Vector3 WorldToUISpace(Canvas parentCanvas, Vector3 worldPos)
	{
		Debug.Log(Camera.main);
		Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
		Vector2 movePos;

		RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, screenPos, parentCanvas.worldCamera, out movePos);
		/*
		 * Костыль movePos.y + 35
		 */
		Debug.Log("WorldToUISpace(Canvas parentCanvas, Vector3 worldPos)");
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
		UnitSpawnManager.instance.StopNeededCoroutine();
		SceneManager.LoadScene("MainScene");
		EnablePanelsBeforeSceneLoad();
	}

	#endregion


	#region Observer 

	public void UpdateUICoin(int subject)
	{
		_CoinText.text = "" + subject;

		//Debug.Log("ConcreteObserverA: Reacted to the event." + subject);
		//if ((subject as Subject).State < 3)
	}
	#endregion
}
