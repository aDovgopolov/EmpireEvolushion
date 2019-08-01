using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Building :MonoBehaviour
{
	public UnityEvent m_MyEvent;
	private bool _IsSomethingBuilt = false;
	public bool _IsSomethingBuiltProp
	{
		get { return _IsSomethingBuilt; }
		set
		{
			_IsSomethingBuilt = value;
		}
	}

	void Start()
	{
		if (m_MyEvent == null)
			m_MyEvent = new UnityEvent();
	}

	public void SetIsSomethingBuilt()
	{
		_IsSomethingBuilt = true;
	}

	public void EnterBuildingScene()
	{
		m_MyEvent.Invoke();
	}

	public void Ping()
	{
		if(_IsSomethingBuilt)
		{
			// Load scene depends on type of building
			UIManagerMainScene.instance.DisablePanelsBeforeSceneLoad();
			SceneManager.LoadScene("Main");
			//Debug.Log("Ping");
		}
	}
}
