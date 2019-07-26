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
		//Debug.Log("Start()");
		StartCoroutine(SpawnCoin());
		//_myCoin = GetComponent<CointSpawner>();
		//_myCoin.SetCoinCosts(_coinCount);
		//_myCoin.GetComponent<Coin>().coinCosts = _coinCount;
	}

	IEnumerator SpawnCoin()
	{
		while (true)
		{
			yield return new WaitForSeconds(5f);
			SpawnSingleCoin();
			//GameObject coin = Instantiate(_coin, transform.position + new Vector3(0, 0.85f, 0), Quaternion.identity);
			//Coin coin_value = coin.GetComponent<Coin>();
			//coin_value.coinCosts = _coinCount;
			//coin.transform.parent = transform;
		}
	}

	public void SpawnCoinByTouch()
	{
		//Debug.Log("SpawnCoinByTouch()");
		//GameObject coin = Instantiate(_coin, transform.position + new Vector3(0, 0.85f, 0), Quaternion.identity);
		//Coin coin_value = coin.GetComponent<Coin>();
		//coin_value.coinCosts = _coinCount;
		//coin.transform.parent = transform;
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
