using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class MainGameSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject flagObject;
    public  Terrain terrain;
    public Transform playerTransform;
    public GameObject walkerZombie;
    public TextMeshProUGUI Timer_monitor;
    public TextMeshProUGUI Round_count_monitor;
    public static Action newRoundEvent; 
    private bool InFlagZone() => Vector2.Distance(new Vector2(FlagSystem.getFlagPosition().x, FlagSystem.getFlagPosition().z), new Vector2(playerTransform.position.x, playerTransform.position.z)) < HotZoneRing;
    private float timeInHotZone = 0;
    public GameOverScreen GameOverScreen;
 
    [SerializeField]
    private float HotZoneTime = 120f;
    [SerializeField]
    private float HotZoneRing = 50f;

    private static int round_count = 0;

    void Start()
    {
        //pass all the object
        FlagSystem.flagObject = flagObject;
        FlagSystem.terrain = terrain;
        EnemiesSystem.walkerZombie = walkerZombie;
        round_count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.isPaused && InFlagZone())
        {
            timeInHotZone -= Time.deltaTime;
        }
        if(timeInHotZone <= 0){
            newRound(++round_count);
        }
        Timer_monitor.text = FormatTime(timeInHotZone);
    }

    private void newRound(int n_round)
    {
        // init time
        timeInHotZone = HotZoneTime;
        Round_count_monitor.text = "Round: "+ round_count;

        FlagSystem.newRandomFlagLocated();

        // create zombies
        EnemiesSystem.createZombies(round_count * 3);

        newRoundEvent?.Invoke();
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time % 1f) * 1000f);

        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }

    public void gameOver()
    {
        GameOverScreen.Setup(round_count);
        Debug.Log("---- Game Over ----");
    }

}
