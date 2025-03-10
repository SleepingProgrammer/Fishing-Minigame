using UnityEngine;
 

[CreateAssetMenu(fileName = "NewFish", menuName = "Fish/Create New Fish")]

public class FishData : ScriptableObject
{
    public Sprite icon;
    public int health;
    public int score; 
    public float speed;

    public Fish FishDataToFish()
    {
        Fish fish = new Fish();
        fish.icon = icon;
        fish.health = health;
        fish.score = score;
        fish.speed = speed;
        return fish;
    }

}