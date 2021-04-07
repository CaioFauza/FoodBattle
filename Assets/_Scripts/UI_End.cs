using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_End : MonoBehaviour
{
    public Text message;
    GameManager gm;

    private void OnEnable()
    {
        gm = GameManager.GetInstance();

        if (gm.lifes > 0)
        {
            message.text = "YOU WON!!!";
        }
        else
        {   
            message.text = "YOU LOST!!";
        }
    }

    public void BackMenu()
    {
        gm.ChangeState(GameManager.GameState.MENU);
    }
}