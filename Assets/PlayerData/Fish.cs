using UnityEngine;

[System.Serializable]
public class Fish
{
    public string name;
    public int rarity;
    public int value;
    public GameObject model;

    public Fish(string name, int rarity, int value, GameObject model)
    {
        this.name = name;
        this.rarity = rarity;
        this.value = value;
        this.model = model;
    }
}