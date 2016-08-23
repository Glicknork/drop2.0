using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{

    public void MainMenu()
    {
        Application.LoadLevel(0);
    }
    public void Level1()
    {
        Application.LoadLevel(2);       
    }
    public void Level2()
    {
        Application.LoadLevel(3);
    }
    public void Level3()
    {
        Application.LoadLevel(4);
    }
    public void Level4()
    {
        Application.LoadLevel(5);
    }
    public void Level5()
    {
        Application.LoadLevel(6);
    }
    public void Level6()
    {
        Application.LoadLevel(7);
    }
    public void Level7()
    {
        Application.LoadLevel(8);
    }


    public void BackToMenu()
    {
        Application.LoadLevel(0);
    }
}