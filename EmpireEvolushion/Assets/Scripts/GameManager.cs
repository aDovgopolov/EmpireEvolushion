using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private GameObject _scrollbar;
	public GameObject _prefab;
	[SerializeField]
	private int _unitsOnScene = 0;
	[SerializeField]
	private int _maxUnitsOnScene = 5;
	private int _coinCount = 0;
	private Text text;
	public int CountUnitsOnScene
	{
		get { return _unitsOnScene; }
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
	//private bool _isGameOver = false;
	[SerializeField]
	private int _cooldownTime = 5;// 180 = ? 30 sec


	void Awake()
	{
		Debug.Log("void Awake()");
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	void Start()
    {
		Debug.Log("void Start()");
		Screen.orientation = ScreenOrientation.Portrait;
		//StartCoroutine(LoadSavedScene());
    }

	private void Update()
	{
		Debug.Log("void Update()");
		Scene scene = SceneManager.GetActiveScene();
		if (scene.name.Equals("MainScene"))
		{
			Debug.Log("MainScene  / set count");
			text = GameObject.Find("coin_text").GetComponent<Text>();
			text.text = "" + _coinCount;
		}
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
				if(_unitsOnScene < _maxUnitsOnScene)
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
