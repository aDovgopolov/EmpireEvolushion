using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, TouchTargetedDelegate
{
	#region

	[SerializeField]
	private int _coinCount;
	public int MyCoinCount
	{
		get { return _coinCount; }
		private set { }
	}

	[SerializeField]
	private GameObject _coin;

	[SerializeField]
	private GameObject _nextSpawningObjct;
	public GameObject NextUnitLevel
	{
		get
		{
			return _nextSpawningObjct;
		}
		private set
		{
		}
	}

	#endregion


	void Start()
    {
		StartCoroutine(SpawnCoin());
		GameObject go = GameObject.Find("SharedTouchDispatcher");
		go.GetComponent<TouchDispatcher>().addTargetedDelegate(this, 1, false);
	}

	IEnumerator SpawnCoin()
	{
		while (true)
		{
			yield return new WaitForSeconds(5f);
			SpawnSingleCoin();
		}
	}

	public void SpawnCoinByTouch()
	{
		SpawnSingleCoin();
	}

	private void SpawnSingleCoin()
	{
		GameObject coin = Instantiate(_coin, transform.position + new Vector3(0, 0.85f, 0), Quaternion.identity);
		Coin coin_value = coin.GetComponent<Coin>();
		coin_value.CoinCost = _coinCount;
		coin.transform.parent = transform;
	}

	public bool TouchBegan(Vector2 position, int fingerId)
	{
		Debug.Log($"TouchBegan {position}");
		return true;
	}

	public void TouchMoved(Vector2 position, int fingerId)
	{
		Vector3 vector3 = new Vector3(position.x, position.y, 0); 
		Vector3 vec = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 10f));
		transform.position = vec;
		Debug.Log($"TouchMoved {position} + {vec}");
	}

	public void TouchEnded(Vector2 position, int fingerId)
	{
		Debug.Log($"TouchEnded {position}");
	}

	public void TouchCanceled(Vector2 position, int fingerId)
	{
		Debug.Log($"TouchCanceled {position}");
	}
}
