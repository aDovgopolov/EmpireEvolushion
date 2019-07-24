using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CointSpawner : MonoBehaviour
{
	[SerializeField]
	public GameObject _coin;

    void Start()
    {
		StartCoroutine(SpawnCoin());
    }

	IEnumerator SpawnCoin()
	{
		while (true)
		{
			yield return new WaitForSeconds(5f);
			//Debug.Log("SpawnCoin()");
			GameObject coin = Instantiate(_coin, transform.position + new Vector3(0, 0.85f, 0), Quaternion.identity);
			coin.transform.parent = transform;
		}
	}
}
