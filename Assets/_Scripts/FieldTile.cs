using UnityEngine;

public class FieldTile : MonoBehaviour
{
    private Crop currentCrop;
    [SerializeField] private Crop cropPrefab;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool isTilled;

    [Header("Sprites")]
    [SerializeField] private Sprite grassSprite;
    [SerializeField] private Sprite tilledSprite;
    [SerializeField] private Sprite wateredTilledSprite;

    private void Start()
    {
        spriteRenderer.sprite = grassSprite;
    }

    public void Interact() {
        if (!isTilled)
        {
            Till();
        }
        else if (!HasCrop() && GameManager.instance.CanPlatnCrop())
        {
            PlantNewCrop(GameManager.instance.selectedCropToPlant);
        }
        else if (HasCrop() && currentCrop.CanHarvest())
        {
            currentCrop.Harvest();
        }
        else {
            Water();
        }

    }
    
    private void PlantNewCrop(CropData cropData) {
        if (!isTilled) {
            return;
        }
        currentCrop = Instantiate(cropPrefab, transform);
        currentCrop.Plant(cropData);

        GameManager.instance.OnNewDay += OnNewDay;
    }
    
    private void Till() {
        isTilled = true;
        spriteRenderer.sprite = tilledSprite;
    }

    private void Water() {
        spriteRenderer.sprite = wateredTilledSprite;
        if (HasCrop()) {
            currentCrop.Water();
        }
    }
    private void OnNewDay() {
        if (currentCrop == null) {
            isTilled = false;
            spriteRenderer.sprite = grassSprite;
            GameManager.instance.OnNewDay -= OnNewDay;
        }
        else if(currentCrop != null)
        {
            spriteRenderer.sprite = tilledSprite;
            currentCrop.NEwDayCheck();
        }
    }

    private bool HasCrop() {
        return currentCrop != null;
    }
}
