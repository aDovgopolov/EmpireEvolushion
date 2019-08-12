using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Building :MonoBehaviour
{
	public UnityEvent m_MyEvent;

	public bool _IsSomethingBuiltProp { get; set; } = false;

	void Start()
	{
		if (m_MyEvent == null)
			m_MyEvent = new UnityEvent();
	}

	public void SetIsSomethingBuilt()
	{
		_IsSomethingBuiltProp = true;
	}

	public void EnterBuildingScene()
	{
		m_MyEvent.Invoke();
	}

	public void Ping()
	{
		if(_IsSomethingBuiltProp)
		{
			UIManagerMainScene.instance.DisablePanelsBeforeSceneLoad();
			SceneManager.LoadScene("Main");

			if(UnitSpawnManager.instance != null)
				UnitSpawnManager.instance.StartNeededCoroutine();
		}
	}
}
