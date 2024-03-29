﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	#region
	private float _speed = 0.5f;

	private int coinCosts;
	public int CoinCost
	{
		get => coinCosts;
		set => coinCosts = value;
	}
	#endregion

	void Start()
    {
	    // когда появляеться монетка она увеличивает общую сумму 
		GameManager.instance.SetCoinCount(coinCosts);
		Destroy(this.gameObject, 1.5f);
    }
	
    void Update()
    {
		transform.Translate(Vector3.up * _speed * Time.deltaTime);
	}

}
