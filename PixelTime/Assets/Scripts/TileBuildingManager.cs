using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class TileBuildingManager : MonoBehaviour
{
    [SerializeField]private bool isDemolishMode = false;

    private const string MAP_SAVE_KEY = "SavedTownData";
    [Serializable] public class PlacedTileData { public int x, y, tileIndex; }
    [Serializable] public class TownSaveData { public List<PlacedTileData> tiles = new List<PlacedTileData>(); }

    private TownSaveData currentTownData = new TownSaveData();
    [SerializeField] private Tilemap buildTilemap;
    [SerializeField] private GameObject buildPanel;
    [SerializeField] private Animator playerAnimator;

    [SerializeField] private Tile[] buildingTiles;
    [SerializeField] private bool closePanelOnSelect = false;
    [SerializeField] private AudioClip placeSound;

    private Tile selectedTile;
    private SpriteRenderer previewRenderer;
    private Camera mainCam;

    private InputAction toggleMenuAction;
    private InputAction clickAction;
    private InputAction cancelAction;

    private void Awake()
    {
        mainCam = Camera.main;

        GameObject previewObj = new GameObject("BuildingPreview_Ghost");
        previewRenderer = previewObj.AddComponent<SpriteRenderer>();
        previewRenderer.sortingLayerName = "Foreground";
        previewRenderer.sortingOrder = 999;
        previewObj.SetActive(false);

        toggleMenuAction = new InputAction("Toggle", binding: "<Keyboard>/b");
        clickAction = new InputAction("Click", binding: "<Mouse>/leftButton");
        clickAction.AddBinding("<Touchscreen>/primaryTouch/press"); // Simulator ekranında anında algılar
        cancelAction = new InputAction("Cancel", binding: "<Mouse>/rightButton");
        cancelAction.AddBinding("<Keyboard>/escape");

    }
    private void OnEnable()
    {
        toggleMenuAction.Enable();
        clickAction.Enable();
        cancelAction.Enable();
        toggleMenuAction.performed += _ => ToggleBuildMenu();
        clickAction.performed += _ => TryPlaceBuilding();
        cancelAction.performed += _ => CancelBuild();
    }
    private void OnDisable()
    {
        toggleMenuAction.Disable();
        clickAction.Disable();
        cancelAction.Disable();
    }

    private void Start()
    {
        LoadTown();
        if (buildPanel != null)
        {
            buildPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (selectedTile != null && buildTilemap != null)
        {
            if (IsPointerOverUI())
            {
                previewRenderer.gameObject.SetActive(false);
                return;
            }

            previewRenderer.gameObject.SetActive(true);
            Vector3Int cellPos = GetMouseCellPosition();
            previewRenderer.transform.position = buildTilemap.GetCellCenterWorld(cellPos);

            bool isOccupied = buildTilemap.HasTile(cellPos);
            previewRenderer.color = isOccupied ? new Color(1f, 0.2f, 0.2f, 0.5f) : new Color(0.2f, 1f, 0.2f, 0.5f);


        }
    }

    public void ToggleBuildMenu()
    {
        if (buildPanel == null) return;
        bool isActive = buildPanel.activeSelf;
        buildPanel.SetActive(!isActive);

        if (!isActive)
            CancelBuild();
    }

    public void SelectBuilding(int index)
    {
        isDemolishMode = false;
        if (index >= 0 && index < buildingTiles.Length)
        {
            selectedTile = buildingTiles[index];
            previewRenderer.sprite = selectedTile.sprite;
            previewRenderer.gameObject.SetActive(true);

            if (closePanelOnSelect && buildPanel != null)
            {
                buildPanel.SetActive(false);
            }
        }
    }

    public void ActiveDemolishMode()
    {
        selectedTile = null;
        isDemolishMode = true;
        previewRenderer.gameObject.SetActive(false);
    }
    public void TryPlaceBuilding()
    {
        if (isDemolishMode)
        {
            Vector3Int demolishPos = GetMouseCellPosition();
            if (buildTilemap.HasTile(demolishPos))
            {
                buildTilemap.SetTile(demolishPos, null);
                currentTownData.tiles.RemoveAll(t => t.x == demolishPos.x && t.y == demolishPos.y);
                if (playerAnimator != null) playerAnimator.SetTrigger("AttackHash");
            }
            return;
        }
        if (selectedTile == null || buildTilemap == null) return;

        if (IsPointerOverUI()) return;

        Vector3Int cellPos = GetMouseCellPosition();

        if (buildTilemap.HasTile(cellPos)) return;

        buildTilemap.SetTile(cellPos, selectedTile);

        int index = Array.IndexOf(buildingTiles, selectedTile);
        currentTownData.tiles.Add(new PlacedTileData { x = cellPos.x, y = cellPos.y, tileIndex = index });
        SaveTown();
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("AttackHash");
        }

        if (AudioManager.Instance != null && placeSound != null)
        {
            AudioManager.Instance.PlaySFX(placeSound);
        }
    }

    public void CancelBuild()
    {
        selectedTile = null;
        if(previewRenderer != null)
        {
            previewRenderer.gameObject.SetActive(false);
        }
    }

    public Vector3Int GetMouseCellPosition()
    {
        Vector2 screenPos = Pointer.current != null ? Pointer.current.position.ReadValue() : Vector2.zero;
        Vector3 worldPos = mainCam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Mathf.Abs(mainCam.transform.position.z)));
        return buildTilemap.WorldToCell(worldPos);
    }

    private bool IsPointerOverUI()
    {
        if (EventSystem.current == null || Pointer.current == null)
            return false;

        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = Pointer.current.position.ReadValue()
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;

    }

    private void SaveTown()
    {
        string json = JsonUtility.ToJson(currentTownData);
        PlayerPrefs.SetString(MAP_SAVE_KEY, json);
        PlayerPrefs.Save();
    }

    private void LoadTown()
    {
        if (!PlayerPrefs.HasKey(MAP_SAVE_KEY)) return;
        string json = PlayerPrefs.GetString(MAP_SAVE_KEY);
        currentTownData = JsonUtility.FromJson<TownSaveData>(json);

        foreach(PlacedTileData data in currentTownData.tiles)
        {
            Vector3Int pos = new Vector3Int(data.x, data.y, 0);
            buildTilemap.SetTile(pos, buildingTiles[data.tileIndex]);
        }
    }
}