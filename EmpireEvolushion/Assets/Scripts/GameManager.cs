using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private GameObject _scrollbar;
	public Image[] progressBar;
	public Sprite loadingIcon;
	public Sprite loadingDoneIcon;
	public GameObject _prefab;
	[SerializeField]
	private int _unitsOnScene = 0;
	private int _maxUnitsOnScene = 2;
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

	private bool _isGameOver = false;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		//DontDestroyOnLoad(gameObject);
	}

	void Start()
    {
		Screen.orientation = ScreenOrientation.Portrait;
		StartCoroutine(LoadSavedScene());
    }

	public IEnumerator LoadSavedScene()
	{
		_scrollbar.GetComponent<Scrollbar>().size = 0;

		int i = 0;
		//Instantiate(_prefab, new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(2f, -3.5f), 0), Quaternion.identity);
		while (true)
		{
			_scrollbar.GetComponent<Scrollbar>().size += 1 / 180f; 
			//progressBar[i].gameObject.SetActive(true);

			i++;

			if (i == 5) // 180 = ? 30 sec
			{
				i = 0;
				if(_unitsOnScene < _maxUnitsOnScene)
				{
					Instantiate(_prefab, new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(2f, -3.5f), 0), Quaternion.identity);
					_unitsOnScene++;
				}

				_scrollbar.GetComponent<Scrollbar>().size = 0;
			}

			//progressBar[i].sprite = loadingDoneIcon;

			//i++;

			//if (i == progressBar.Length)
			//{
			//	Instantiate(_prefab, new Vector3(Random.Range(-8f, 8f), Random.Range(-3.5f, 1.5f), 0), Quaternion.identity);
			//	i = 0;
			//	for (int j = 0; j < progressBar.Length; j++)
			//	{
			//		progressBar[j].sprite = loadingIcon;
			//	}
			//}

			yield return new WaitForSeconds(0.1f);
		}
	}
}
