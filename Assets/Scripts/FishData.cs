using UnityEngine;

[CreateAssetMenu(fileName = "NewFish", menuName = "Fish/Create New Fish")]
public class FishData : ScriptableObject
{
    public Sprite icon;
    public int health;

    // Optional: Get the fish's name from the asset filename
    public string FishName => name;
    public float speed = 1.0f;
    public Fish FishDataToFish() {
        return new Fish(name, health, speed, icon);
    }
}