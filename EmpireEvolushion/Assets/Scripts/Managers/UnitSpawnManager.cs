using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSpawnManager : MonoBehaviour
{
	#region Fields
	[HideInInspector]
	public static UnitSpawnManager instance = null;
	[SerializeField]
	private int _unitsOnScene = 0;
	[SerializeField]
	private int _maxUnitsOnScene = 5;
	[SerializeField]
	private int _cooldownTime = 5;
	public int CountUnitsOnScene
	{
		get => _unitsOnScene;
		set
		{
			if (value >= 0)
			{
				_unitsOnScene = value;
			}
		}
	}

	[SerializeField]
	private GameObject _scrollbar;
	public GameObject _prefab;

	#endregion

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	void Start()
    {
		AttachScrollBar();
		StartCoroutine(StartSpawnUnits());
	}

	private bool IsLooping = true;

	private IEnumerator StartSpawnUnits()
	{

		_scrollbar.GetComponent<Scrollbar>().size = 0;

		int i = 0;

		while (IsLooping)
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

	private void AttachScrollBar()
	{
		Debug.Log(GameObject.Find("Canvas2"));
		_scrollbar = GameObject.Find("Canvas2").transform.GetChild(0).gameObject;

		if (_scrollbar == null)
			Debug.LogError($"_scrollbar has not been init");
	}

	public void StartNeededCoroutine()
	{
		Debug.Log("StartNeededCoroutine()");
		AttachScrollBar();
		IsLooping = true;
		StartCoroutine(StartSpawnUnits());
	}

	public void StopNeededCoroutine()
	{
		Debug.Log("StopNeededCoroutine()");
		IsLooping = false;
		StopCoroutine(StartSpawnUnits());
	}
}
