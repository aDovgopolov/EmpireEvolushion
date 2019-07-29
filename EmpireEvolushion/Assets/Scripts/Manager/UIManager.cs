using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	[HideInInspector]
	public static UIManager instance = null;

	[SerializeField]
	private GameObject _scrollbar;
	public GameObject _prefab;
	private int coinScore = GameManager.instance.MyCoinCount;
	[SerializeField]
	private int _unitsOnScene = 0;
	[SerializeField]
	private int _maxUnitsOnScene = 5;
	[SerializeField]
	private int _cooldownTime = 5;

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

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
	}

	void Start()
    {
		//_scoreText.text = "" + coinScore;
		StartCoroutine(StartSpawnUnits());
	}

	public void UpdateScoreCoinText(int score)
	{
		coinScore += score;
		//_scoreText.text = "" + coinScore;
		GameManager.instance.SetCoinCount(coinScore);
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
}
