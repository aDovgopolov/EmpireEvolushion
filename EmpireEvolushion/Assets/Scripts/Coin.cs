using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	private float _speed = 0.5f;
	private UIManager UIManager;
	public int coinCosts;

	public int MyProperty
	{
		set { coinCosts = value; }
		get
		{
			return coinCosts;
		}
	}

	void Start()
    {
		UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
		//UIManager.UpdateScoreCoinText(transform.parent.GetComponent<Unit>().MyCoinCount);
		//StartCoroutine(PayCoin());
		UIManager.UpdateScoreCoinText(coinCosts);
		//StartCoroutine(PayCoin());
		Destroy(this.gameObject, 1.5f);
    }
	
    void Update()
    {
		transform.Translate(Vector3.up * _speed * Time.deltaTime);
	}

	public void SetCoinCosts(int value)
	{
		Debug.Log($"Coin SetCoinCosts(); {value}");
		GetComponent<Coin>().coinCosts = value;
		coinCosts = value;
	}

	IEnumerator PayCoin()
	{
		yield return new WaitForSeconds(0.1f);
		Debug.Log(coinCosts + " / ");
		UIManager.UpdateScoreCoinText(coinCosts);
	}
}
