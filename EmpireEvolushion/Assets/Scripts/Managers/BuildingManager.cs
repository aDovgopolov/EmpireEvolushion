using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
	#region Fields
	[HideInInspector]
	public static BuildingManager instance = null;

	public int millPrice = 100;

	[SerializeField]
	private List<GameObject> _buildList = new List<GameObject>();
	#endregion

	#region Methods
	private void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void BuildHouse()
	{

	}

	#endregion
}
