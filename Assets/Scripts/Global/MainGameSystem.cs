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
    public int zomibeCount; 
    private List<GameObject> zombies = new List<GameObject>();

 
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

            // Make all zombies aware of the player when in the flag zone
            foreach (GameObject zombie in zombies){
                AI zombieAI = zombie.GetComponent<AI>();
                if (zombieAI != null)
                    zombieAI.setToAware();
            }

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
        for (int i = 0; i < round_count * zomibeCount; i++){
            GameObject zombieObject = EnemiesSystem.createZombie();
            Zombie zombie = zombieObject.GetComponent<Zombie>();
            if (zombie != null){
                zombies.Add(zombieObject);

                // Subscribe to the OnDestroyedZombie event using a regular method
                zombie.OnDestroyedZombie += RemoveZombie;
            }
        }
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

    private void RemoveZombie(Zombie zombie)
    {
        GameObject zombieObject = zombie.gameObject;
        if (zombies.Contains(zombieObject))
            zombies.Remove(zombieObject);
    }

}
