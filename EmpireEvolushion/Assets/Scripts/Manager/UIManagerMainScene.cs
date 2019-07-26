using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManagerMainScene : MonoBehaviour
{
	[SerializeField]
	private Button _trophyButton;
	[SerializeField]
	private Button _missionButton;
	[SerializeField]
	private GameObject _achivkaPanel;
	[SerializeField]
	private GameObject _questPanel;
	[SerializeField]
	private GameObject _mainScreenPanel;

	// Start is called before the first frame update
	void Start()
    {
		//_achivkaPanel = GameObject.Find("Achivka_Panel");
		//_achivkaPanel.SetActive(false);
		//_questPanel = GameObject.Find("Quest_Panel");
		//_questPanel.SetActive(false);
		//_mainScreenPanel = GameObject.Find("Main_Screen_Panel");
		//Debug.Log($"{_achivkaPanel}  / {_questPanel} / {_mainScreenPanel}");
		
	}

    // Update is called once per frame
    void Update()
    {
        
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
}
