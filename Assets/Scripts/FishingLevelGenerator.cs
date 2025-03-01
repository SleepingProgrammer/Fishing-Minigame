using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingLevelGenerator : MonoBehaviour
{
    public List<FishData> fishData;
    // first level will have 3 fish
    public int level = 0;
    public int minFishCount = 3;
    public int maxFishCount = 5;

    // Fish data will start varying every 3 levels
    // increase fish rarity
    // every level, increase fish count by 1
    // then on 3rd level, reset fish count to minimum
    // fish added will be of previous level rarity
    public int fishRarity = 0;
    public int fishRarityIncrease = 3;

    public FishingHook hookPrefab;
    [SerializeField] BoxCollider2D fishingArea;
    public float radius = 1.0f;

    public List<FishingHook> hooks = new List<FishingHook>();

    public void RemoveHook(FishingHook hook)
    {
        hooks.Remove(hook);
    }

    public void GenerateLevel()
    {
        int fishCount = level % fishRarityIncrease == 0 ? minFishCount : minFishCount + (level % fishRarityIncrease);

        Debug.Log("Fish count: " + fishCount);

        List<Vector2> points = new List<Vector2>();
        for (int i = 0; i < fishCount; i++)
        {
            FishData f = fishData[fishRarity];
            hookPrefab.fish = f.FishDataToFish(); 
            FishingHook hook = Instantiate(hookPrefab, fishingArea.transform);
            hook.transform.position = GetRandomPointInBoxCollider(fishingArea);

            while (hasCollided(hook.transform.position, radius, points))
            {
                hook.transform.position = GetRandomPointInBoxCollider(fishingArea);
            }

            hook.fish = f.FishDataToFish();
            hook.transform.localScale = Vector3.one;

            hooks.Add(hook);

            points.Add(hook.transform.position);

            Debug.Log("Fish #" + i + " ==> " + f.FishName + " added at " + hook.transform.position);
        }
    }

    public void LevelPassed()
    {
        level++;
        if (level % fishRarityIncrease == 0)
        {
            fishRarity++;
        }
    }

    public bool hasCollided(Vector2 point, float radius, List<Vector2> points)
    {
        foreach (Vector2 p in points)
        {
            if (Vector2.Distance(point, p) < radius)
            {
                return true;
            }
        }
        return false;
    }

    public Vector2 GetRandomPointInBoxCollider(BoxCollider2D boxCollider)
    {
        Bounds bounds = boxCollider.bounds;
        float x = Mathf.Floor(Random.Range(bounds.min.x, bounds.max.x));
        float y = Mathf.Floor(Random.Range(bounds.min.y, bounds.max.y));
        return new Vector2(x, y);
    }
}