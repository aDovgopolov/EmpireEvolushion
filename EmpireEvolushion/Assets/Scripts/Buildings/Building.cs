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
	 // Start is called before the first frame update
	void Start()
	{
		if (m_MyEvent == null)
			m_MyEvent = new UnityEvent();
	}

	// Update is called once per frame
	void Update()
	{
		//if (Input.anyKeyDown && m_MyEvent != null)
		//{
		//	m_MyEvent.Invoke();
		//}
	}


	public void SetIsSomethingBuilt()
	{
		_IsSomethingBuilt = true;
	}

	public void YAYA()
	{
		m_MyEvent.Invoke();
	}

	public void Ping()
	{
		if(_IsSomethingBuilt)
		{
			// Load scene depends on type of building
			SceneManager.LoadScene("Main");

			UIManagerMainScene.instance.DisablePanelsBeforeSceneLoad();
			Debug.Log("Ping");
		}
	}
}
