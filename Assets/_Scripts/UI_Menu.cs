using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Menu : MonoBehaviour
{

    GameManager gm;
    public GameObject player;

    private void OnEnable()
    {
        gm = GameManager.GetInstance();
    }

    public void StartGame()
    {
        player.transform.position = new Vector3(-93.41f, -1.38f, 0);
        gm.ChangeState(GameManager.GameState.GAME);
    }
}