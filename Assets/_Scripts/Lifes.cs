using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifes : MonoBehaviour
{
    public GameObject heartOne, heartTwo, heartThree, heartFour, heartFive;
    public GameObject emptyHeartOne, emptyHeartTwo, emptyHeartThree, emptyHeartFour, emptyHeartFive;
    GameManager gm;

    void Start()
    {
        gm = GameManager.GetInstance();
    }

    void FillHearts()
    {
        heartOne.SetActive(true);
        heartTwo.SetActive(true);
        heartThree.SetActive(true);
        heartFour.SetActive(true);
        heartFive.SetActive(true);

        emptyHeartOne.SetActive(false);
        emptyHeartTwo.SetActive(false);
        emptyHeartThree.SetActive(false);
        emptyHeartFour.SetActive(false);
        emptyHeartFive.SetActive(false);
    }

    void Update()
    {
        switch (gm.lifes)
        {
            case 4:
                heartFive.SetActive(false);
                emptyHeartFive.SetActive(true);
                break;
            case 3:
                heartFour.SetActive(false);
                emptyHeartFour.SetActive(true);
                break;
            case 2:
                heartThree.SetActive(false);
                emptyHeartThree.SetActive(true);
                break;
            case 1:
                heartTwo.SetActive(false);
                emptyHeartTwo.SetActive(true);
                break;
            case 0:
                heartOne.SetActive(false);
                emptyHeartOne.SetActive(true);
                break;
        }
        if (gm.gameState == GameManager.GameState.MENU) FillHearts();
    }
}