using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	[SerializeField]
	private int _coinCount = 0;
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
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
