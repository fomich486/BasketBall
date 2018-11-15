using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLevel : MonoBehaviour {
    [Header("Changeble Elements")]
    [SerializeField]
    Image background;
    [SerializeField]
    SpriteRenderer player;
    [SerializeField]
    Throw ball;
    [SerializeField]
    AfterShot changeStateSprites;
    [SerializeField]
    SpriteRenderer basket;
    [SerializeField]
    SpriteRenderer ring;
    [SerializeField]
    SpriteRenderer ringHolder;
    [Header("Levels")]
    [SerializeField]
    PeriodContainer pContainer;

    private void Start()
    {
        SetNewLevel();
    }

    public void SetNewLevel()
    {
        int n = 0;
        background.sprite = pContainer.levelList[n].Background;
        player.sprite = pContainer.levelList[n].PlayerRadyToShoot;
        ball._ball.GetComponent<SpriteRenderer>().sprite = pContainer.levelList[n].Ball;
        changeStateSprites.readyToShoot = pContainer.levelList[n].PlayerRadyToShoot;
        changeStateSprites.jumping = pContainer.levelList[n].Jumping;
        changeStateSprites.Shooted = pContainer.levelList[n].PlayerWaiting;
        basket.sprite = pContainer.levelList[n].Basket;
        ring.sprite = pContainer.levelList[n].Ring;
        ringHolder.sprite = pContainer.levelList[n].Basketholder;
    }

}
