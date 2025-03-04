using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class FishingManager : MonoBehaviour
{
    public static FishingManager instance;

    public enum GameState
    {
        DEFAULT,
        FISHING,
        GAME_OVER
    }

    public GameState gameState = GameState.DEFAULT;

    public GameObject fishingUI;
    public Image indicator;
    public Transform pulseMover;
    public Transform pulseTarget;
    public Image fishImage;
    [SerializeField] FishingLevelGenerator levelGenerator;

    public float pulseSpeed = 1.0f;
    public float maxPulseScale = 2f;
    public float minPulseScale = 1f;

    // stats
    public int fishHealth = 3;
    public int lineHealth = 1;
    public int reelAtk = 1;
    public float rodRadius = .3f;
    public bool isFishing = false;
    public Fish hookedFish;

    // End and start
    public UnityEvent onGameEnd;
    public UnityEvent onGameStart;


    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void InitializeGame()
    {
        TimeManager.instance.ResetTimer();
        TimeManager.instance.StartTimer();
        levelGenerator.GenerateLevel();

        reelAtk = InventoryManager.instance.GetLevel("reel");
        lineHealth = InventoryManager.instance.GetLevel("line");
        rodRadius = .3f + InventoryManager.instance.GetLevel("rod") * .15f;

        onGameStart?.Invoke();
    }

    void Start()
    {
        TimeManager.instance.onTimeEnd.AddListener(GameOver);

        InitializeGame();
    }

    private void OnDestroy()
    {
        TimeManager.instance.onTimeEnd.RemoveListener(GameOver);
    }

    public void InitializeFishing(FishingHook hook)
    {
        hookedFish = hook.fish;
        fishImage.sprite = hookedFish.icon;
        fishHealth = hookedFish.health;
        pulseSpeed = hookedFish.speed;



        // set random pulse target
        // scale the pulse target with random scale

        float randomScale = Random.Range(minPulseScale, maxPulseScale);
        pulseTarget.localScale = new Vector3(randomScale, randomScale, randomScale);

        fishingUI.SetActive(true);

        levelGenerator.RemoveHook(hook);

        // on next frame, start fishing
        StartCoroutine(StartFishingNextFrame());
    }

    IEnumerator StartFishingNextFrame()
    {
        yield return new WaitForEndOfFrame();
        StartFishing();
    }

    public void StartFishing()
    {
        gameState = GameState.FISHING;
        isFishing = true;
        fishingUI.SetActive(true);
    }

    public void StopFishing()
    {
        gameState = GameState.DEFAULT;
        isFishing = false;
        fishingUI.SetActive(false);
    }

    void GameOver()
    {
        StopFishing();

        onGameEnd?.Invoke();
    }


    void Update()
    {
        // if we are fishing
        // scale the pulseMover in a sine wave pattern using the speed

        if (isFishing)
        {
            pulseMover.localScale = new Vector3(Mathf.Sin(Time.time * pulseSpeed) + 1, Mathf.Sin(Time.time * pulseSpeed) + 1, Mathf.Sin(Time.time * pulseSpeed) + 1);

            // on click, check if the distance between pulseMover and pulseTarget is less than rodRadius
            // if yes, then we caught the fish
            // else, we missed the fish

            float distance = Vector3.Distance(pulseMover.localScale, pulseTarget.localScale);

            // depending on distance show red, yellow, green color
            if (distance < rodRadius)
            {
                indicator.color = Color.green;
            }
            else if (distance < rodRadius * 2)
            {
                indicator.color = Color.yellow;
            }
            else
            {
                indicator.color = Color.red;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (
                    // check if scales are close enough with radius
                    distance < rodRadius
                )
                {
                    Debug.Log("Strike!");
                    fishHealth--;
                    if (fishHealth <= 0)
                    {
                        Debug.Log("You reeled in the fish!");
                        StopFishing();
                        ScoreManager.instance.AddScore(hookedFish.score);

                        hookedFish = null;

                        if (levelGenerator.hooks.Count == 0)
                        {
                            levelGenerator.LevelPassed();
                            levelGenerator.GenerateLevel();
                            TimeManager.instance.LevelCleared();
                        }
                    }
                }
                else
                {
                    Debug.Log("Missed the strike!");
                    lineHealth--;
                    if (lineHealth <= 0)
                    {
                        Debug.Log("Line is broken!");

                        GameOver();
                    }
                }


            }
        }




    }



}
