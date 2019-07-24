using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	private float _speed = 0.5f;
	private UIManager UIManager;
	
	void Start()
    {
		UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
		UIManager.UpdateScoreCoinText(transform.parent.GetComponent<Unit>().MyCoinCount);
		Destroy(this.gameObject, 1.5f);
    }
	
    void Update()
    {
		transform.Translate(Vector3.up * _speed * Time.deltaTime);
	}
}
