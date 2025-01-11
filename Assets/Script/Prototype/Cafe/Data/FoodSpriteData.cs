using UnityEngine;

[CreateAssetMenu(fileName = "FoodSpriteData", menuName = "Scriptable Objects/FoodSpriteData")]
public class FoodSpriteData : ScriptableObject
{
    [System.Serializable]
    public class FoodSprite
    {
        [Header("FoodSprite")]
        [SerializeField] public Sprite _foodSprite;
        [Header("FoodName")]
        [SerializeField] public string _foodName;
    }

    [Header("FoodSprite")]
    [SerializeField] private FoodSprite[] _foodSprites;

    public FoodSprite[] FoodSpriteArray => _foodSprites;
}
