using UnityEngine;

public class Fish {
    public Sprite icon;
    public string name;
    public int health;
    public float speed;

    public Fish(string _name, int _health, float _speed, Sprite _icon) {
        name = _name;
        health = _health;
        speed = _speed;
        icon = _icon;
    }
}

