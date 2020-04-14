using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int currentLevel = 0;

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        currentLevel = data.level;
    }

    #region Player Get Set Functions
    public void ChangeLevel (int newLevel)
    {
        currentLevel = newLevel;
    }
    #endregion
}
