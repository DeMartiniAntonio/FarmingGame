using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public int currentDay { get; private set; }
    public static GameManager instance;
    [SerializeField] private int money;
    [SerializeField] private int cropInventory;
    [SerializeField] private int potatoInventory;
    public CropData selectedCropToPlant;
    public event UnityAction OnNewDay;
    [SerializeField] private TMP_Text statsText;
    [SerializeField] private TMP_Dropdown dropdown;
    private Dictionary<string, int> allCropsInventory = new Dictionary<string, int>() ;
    [SerializeField] private CropData[] allCrops;

    private void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        instance = this;
    }
    private void Start()
    {      
        dropdown.options.Clear();
        for (int i = 0; i < allCrops.Length; i++) {
            dropdown.options.Add(new TMP_Dropdown.OptionData(allCrops[i].name));

            allCropsInventory.Add(allCrops[i].name, 0);
        }
        allCropsInventory["Wheat"] = 2;
        dropdown.onValueChanged.AddListener(delegate { OnDropdownChange(); });
        UpdateText();
    }

    private void OnDropdownChange() {
        selectedCropToPlant = allCrops[dropdown.value];
        UpdateText();
    }
    
    public void SetNewDay()
    {
        currentDay++;
        OnNewDay?.Invoke();
        UpdateText();
    }
    public void OnPlantCrop(CropData cropData)
    {
        allCropsInventory[cropData.name]--;
        UpdateText();
    }

    public void OnHarvestCrop(CropData cropData)
    {
        money += cropData.sellPrice;
        UpdateText();

    }
    public void PurchaseCrop(CropData cropData)
    {
       //cropInventory++;
        allCropsInventory[cropData.name]++;
        money -= cropData.purchasePrice;
        UpdateText();
    }
    public bool CanPlatnCrop() {

        return allCropsInventory[selectedCropToPlant.name] > 0;
    }

    public void OnBuyCropButton(CropData cropData) {

        if (!allCropsInventory.ContainsKey(cropData.name)) {
            allCropsInventory.Add (cropData.name, 0);
        }
        

        if (money >= cropData.purchasePrice) {
            PurchaseCrop(cropData);
        }
    }

    public void UpdateText()
    {
        statsText.text = $"Day: {currentDay} \nMoney: ${money} \n{selectedCropToPlant.name}: {allCropsInventory[selectedCropToPlant.name]}";
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
