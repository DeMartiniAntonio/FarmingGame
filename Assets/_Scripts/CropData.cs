using UnityEngine;

[CreateAssetMenu(fileName = "CropData", menuName = "Scriptable Objects/NewCropData")]
public class CropData : ScriptableObject
{
    public int daysToGrow;
    public Sprite[] growProgressSprites;
    public Sprite readyToHarvestSprite;

    public int purchasePrice;
    public int sellPrice;
}
