using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour, IDataPersistence
{
    public int releasedLevelStatic = 1;
    public int releasedLevel;
    public string nextLevel;


    public void ButtonNextLevel()
    {
		DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadScene(nextLevel);
        if(releasedLevelStatic <= releasedLevel)
        {
            releasedLevelStatic = releasedLevel;
        }
    }
    public void LevelFinhished()
    {
        if (releasedLevelStatic <= releasedLevel)
        {
            releasedLevelStatic = releasedLevel;
        }
        SceneManager.LoadScene("World_1");
		DataPersistenceManager.instance.SaveGame();
    }

    public void ButtonMenu()
    {
        SceneManager.LoadScene("World_1");
    }

    public void LoadData(GameData data)
    {
        this.releasedLevelStatic = data.releasedLevelStatic;
    }

    public void SaveData(GameData data)
    {
        data.releasedLevelStatic = this.releasedLevelStatic;
    }

}
