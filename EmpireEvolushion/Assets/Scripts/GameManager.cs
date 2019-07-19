using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	public Image[] progressBar;
	public Sprite loadingIcon;
	public Sprite loadingDoneIcon;
	public GameObject _prefab;

	// Start is called before the first frame update
	void Start()
    {
		StartCoroutine(LoadSavedScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public IEnumerator LoadSavedScene()
	{
		int i = 0;
		
		while (true)
		{
			progressBar[i].sprite = loadingDoneIcon;

			i++;

			if (i == progressBar.Length)
			{
				Instantiate(_prefab, new Vector3(Random.Range(-8f, 8f), Random.Range(-3.5f, 1.5f), 0), Quaternion.identity);
				i = 0;
				for (int j = 0; j < progressBar.Length; j++)
				{
					progressBar[j].sprite = loadingIcon;
				}
			}

			yield return new WaitForSeconds(0.5f);
		}
		yield return null;
	}
}
