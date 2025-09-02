using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Events;

public class Crop : MonoBehaviour
{
    private CropData currentCrop;
    private int plantedDay;
    private int daysSinceLastWatered;
    [SerializeField] SpriteRenderer spriteRenderer;
    public static event UnityAction<CropData> OnPlantCrop;
    public static event UnityAction<CropData> OnHarvestCrop;
    public void Plant(CropData cropData) {
        currentCrop = cropData;
        plantedDay = GameManager.instance.currentDay;
        daysSinceLastWatered = 1;
        UpdateCropSprite();
        OnPlantCrop?.Invoke(cropData);
    }

    public void NEwDayCheck() {
        daysSinceLastWatered++;
        if (daysSinceLastWatered > 3) {
            Destroy(gameObject);
            
        }
        UpdateCropSprite();

    }

    private void UpdateCropSprite() {
        int cropProgress = Cropprogress();
        if (cropProgress < currentCrop.daysToGrow) {
            spriteRenderer.sprite = currentCrop.growProgressSprites[cropProgress];
        }

        else 
        {
            spriteRenderer.sprite = currentCrop.readyToHarvestSprite;
        }
    }

    public void Water() {
        
        daysSinceLastWatered = 0;
    }

    public void Harvest() {
    
        if (CanHarvest()) {

            OnHarvestCrop?.Invoke(currentCrop);
            Destroy(gameObject);
        }
    }

    private int Cropprogress() {
        return GameManager.instance.currentDay - plantedDay;
    }
    public bool CanHarvest() {
        return Cropprogress() >= currentCrop.daysToGrow;
    }
}
