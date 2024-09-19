using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
public class DragAndDropItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public InventorySlot oldSlot;
    private Camera _camera;
    [SerializeField] private Transform _player;
    [SerializeField] private PlayerController _playerController;
    private Vector3 _boxSize = new Vector3(2, 2, 2);
    [SerializeField] private LayerMask _placementMask;
    private MeshRenderer meshRenderer;
    private DigitSwitching _digitSwitching;
    private void Start()
    {
        oldSlot = transform.GetComponentInParent<InventorySlot>();
        _camera = Camera.main;
        _digitSwitching = oldSlot.GetComponentInParent<DigitSwitching>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Standard"));
        meshRenderer.material.color = Color.red;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!oldSlot&&!oldSlot.isComplete) return;
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.75f);
        GetComponentInChildren<Image>().raycastTarget = false;
        transform.SetParent(transform.parent.parent);
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!oldSlot.isComplete) return;
        GetComponent<RectTransform>().position += new Vector3(eventData.delta.x, eventData.delta.y);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!oldSlot.isComplete) return;
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
        GetComponentInChildren<Image>().raycastTarget = true;
        transform.SetParent(oldSlot.transform);
        transform.position = oldSlot.transform.position;
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            InventorySlot newSlot = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.
                GetComponent<InventorySlot>();
            if (newSlot != null) ExchangeSlotData(newSlot); 
        }
        else
        {
            if(!oldSlot.item.build)
            {
                GameObject itemObject = Instantiate(oldSlot.item.itemPrefab, _player.position, Quaternion.identity);
                itemObject.GetComponent<Item>()._amount = oldSlot.amount;
                oldSlot.NullifySlotData();
            }
            else
            {
                var newObjectPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                newObjectPosition.z = -1f;
                PlaceObject(newObjectPosition);
                CreateCubeMesh();
            }
        }
        if(_digitSwitching)
        {
            InventorySlot currentSlot = _digitSwitching.slotParent.GetChild(_digitSwitching.currentSlotID)
                .GetComponent<InventorySlot>();
            if (currentSlot.item == oldSlot.item) _digitSwitching.TakeItemInHands();
        }
    }
    void ExchangeSlotData(InventorySlot newSlot)
    {
        ItemScriptableObject item = newSlot.item;
        int amount = newSlot.amount;
        bool isComplete = newSlot.isComplete;
        Sprite iconGo = newSlot.iconGO.sprite;
        //CleanSliderAndPercentageDuringSwap(newSlot);
        newSlot.item = oldSlot.item;
        newSlot.amount = oldSlot.amount;
        if (oldSlot.isComplete)
        {
            SetNewSliderAndPercentageDuringSwap(newSlot);
            newSlot.SetIcon(oldSlot.iconGO.sprite);
            newSlot.itemAmount.text = oldSlot.amount.ToString();
        }
        else NullItemData(newSlot);
        newSlot.isComplete = oldSlot.isComplete;
        //CleanSliderAndPercentageDuringSwap(oldSlot);
        oldSlot.item = item;
        oldSlot.amount = amount;
        if (isComplete)
        { 
            SetNewSliderAndPercentageDuringSwap(oldSlot);
            oldSlot.SetIcon(iconGo);
            oldSlot.itemAmount.text = amount.ToString();
        }
        else NullItemData(oldSlot);
        oldSlot.isComplete = isComplete;
        
    }
    // void ExchangeSlotData(InventorySlot newSlot)
    // {
    //     ItemScriptableObject item = newSlot.item;
    //     int amount = newSlot.amount;
    //     bool isComplete = newSlot.isComplete;
    //     Sprite iconGo = newSlot.iconGO.sprite;
    //     if (oldSlot.isComplete&& oldSlot.item) ChangeItemData(newSlot, oldSlot);
    //     else NullItemData(newSlot);
    //     newSlot.item = oldSlot.item;
    //     newSlot.amount = oldSlot.amount;
    //     newSlot.isComplete = oldSlot.isComplete;
    //     if (isComplete && item) {ChangeItemData(oldSlot, newSlot);oldSlot.SetIcon(iconGo);}
    //     else NullItemData(oldSlot);
    //     oldSlot.item = item;
    //     oldSlot.amount = amount;
    //     oldSlot.isComplete = isComplete;
    // }
    //
    // private void ChangeItemData(InventorySlot slot1, InventorySlot slot2)
    // {
    //     slot1.SetIcon(slot2.iconGO.sprite);
    //     slot1.itemAmount.text = slot2.amount.ToString();
    //     if(slot2.item.itemType==ItemType.Food)slot1.UpdateShelfSliderUI(slot2.item.currTimeShelfLife);
    //     else if(slot2.item.itemType is ItemType.Instrument or ItemType.Light && !slot2.item.build)
    //         slot1.UpdateShelfPercentageUI(slot2.item.maxTimeShelfLife, 
    //             slot2.item.currTimeShelfLife);
    //     else
    //     {
    //         slot1.UpdateShelfSliderUI(0);
    //         slot1.UpdateShelfPercentageUI(1, 0);
    //     }
    // }
    private void NullItemData(InventorySlot newSlot)
    {
        newSlot.iconGO.color = new Color(1, 1, 1, 0);
        newSlot.iconGO.sprite = null;
        newSlot.itemAmount.text = "";
        newSlot.UpdateShelfSliderUI(0);
        newSlot.UpdateShelfPercentageUI(1, 0);
    }

    private void CleanSliderAndPercentageDuringSwap(InventorySlot newSlot)
    {
        if(newSlot.item.itemType==ItemType.Food)newSlot.UpdateShelfSliderUI(0);
        else if(newSlot.item.itemType is ItemType.Instrument or ItemType.Light && !newSlot.item.build)
            newSlot.UpdateShelfPercentageUI(1,0);
    }
    private void SetNewSliderAndPercentageDuringSwap(InventorySlot newSlot)
    {
        if(newSlot.item.itemType==ItemType.Food)newSlot.UpdateShelfSliderUI(newSlot.item.currTimeShelfLife);
        else if(newSlot.item.itemType is ItemType.Instrument or ItemType.Light && !newSlot.item.build)
            newSlot.UpdateShelfPercentageUI(newSlot.item.maxTimeShelfLife,
                newSlot.item.currTimeShelfLife);
    }
    private bool CanPlaceObject(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapBox(position, _boxSize / 2, Quaternion.identity,
            _placementMask);
        return colliders.Length == 0;
    }
    private void PlaceObject(Vector3 position)
    {
        if (CanPlaceObject(position))
        {
            var objectOnScene = Instantiate(oldSlot.item.itemPrefab, position, Quaternion.identity);
            if(objectOnScene)
            {
                print("Костёр успешно установлен!");
                oldSlot.NullifySlotData();
            }
        }
        else
        {
            transform.SetParent(oldSlot.transform);
            transform.position = oldSlot.transform.position;
            print("Костёр должен вернуться");
        }
    }
    Mesh CreateCubeMesh()
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = {
            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3( 0.5f, -0.5f, -0.5f),
            new Vector3( 0.5f,  0.5f, -0.5f),
            new Vector3(-0.5f,  0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f,  0.5f),
            new Vector3( 0.5f, -0.5f,  0.5f),
            new Vector3( 0.5f,  0.5f,  0.5f),
            new Vector3(-0.5f,  0.5f,  0.5f)
        };
        int[] triangles = {
            0, 2, 1, 0, 3, 2, 
            4, 5, 6, 4, 6, 7,
            0, 1, 5, 0, 5, 4,
            2, 3, 7, 2, 7, 6,
            0, 4, 7, 0, 7, 3,
            1, 2, 6, 1, 6, 5
        };
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        return mesh;
    }
}
