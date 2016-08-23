using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour {
	
	public void StartApplication()
	{
		Application.LoadLevel (2);
		// could have just used number 1 in build settings
	}

    public void LevelSelectMenu()
    {
        Application.LoadLevel(1);
        
    }

    public void QuitGame()
	{
		Application.Quit();
	}
}