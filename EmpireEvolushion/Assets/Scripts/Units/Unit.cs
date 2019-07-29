using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	[SerializeField]
	private int _coinCount;
	private CointSpawner _myCoin;
	[SerializeField]
	private GameObject _coin;

	public int MyCoinCount
	{
		get {
			return _coinCount;
		}
		private set{}
	}
	[SerializeField]
	private GameObject myObject;
	public GameObject MyObject
	{
		get
		{
			return myObject;
		}
		private set
		{
		}
	}

	void Start()
    {
		StartCoroutine(SpawnCoin());
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
		coin_value.coinCosts = _coinCount;
		coin.transform.parent = transform;
	}
}
