using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public int currentDay { get; private set; }
    public static GameManager instance;
    [SerializeField] private int money;
    [SerializeField] private int cropInventory;
    public CropData selectedCropToPlant;
    public event UnityAction OnNewDay;

    private void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        instance = this;
    }

    private void SetNewDay()
    {
        
    }
    private void OnPlantCrop(CropData cropData)
    {
        cropInventory--;
    }
    private void OnHarvestCrop(CropData cropData)
    {
        money += cropData.sellPrice;
    }
    private void PurchaseCrop(CropData cropData)
    {

    }
    public bool CanPlatnCrop() {

        return cropInventory > 0;
    }

    public void OnBuyCropButton(CropData cropData) {

    }

    private void UpdateText()
    {
        
    }

    private void OnEnable()
    {
        Crop.OnPlantCrop += OnPlantCrop;
        Crop.OnHarvestCrop += OnHarvestCrop;
    }

    private void OnDisable()
    {
        Crop.OnPlantCrop -= OnPlantCrop;
        Crop.OnHarvestCrop -= OnHarvestCrop;
    }
}
