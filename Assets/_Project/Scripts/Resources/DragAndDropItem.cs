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
            if(oldSlot.item.itemType!=ItemType.BuildItem)
            {
                GameObject itemObject = Instantiate(oldSlot.item.itemPrefab, _player.position, Quaternion.identity);
                itemObject.GetComponent<Item>()._amount = oldSlot.amount;
                oldSlot.NullifySlotData();
            }
            else
            {
                var newObjectPosition = Input.mousePosition;
                newObjectPosition.z = -1f;
                PlaceObject(newObjectPosition);
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
        if(newSlot.item)CleanSliderAndPercentageDuringSwap(newSlot);
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
        if(oldSlot.item)CleanSliderAndPercentageDuringSwap(oldSlot);
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
        else if(newSlot.item.itemType is ItemType.Instrument or ItemType.Light)
            newSlot.UpdateShelfPercentageUI(1,0);
    }
    private void SetNewSliderAndPercentageDuringSwap(InventorySlot newSlot)
    {
        if(newSlot.item.itemType==ItemType.Food)newSlot.UpdateShelfSliderUI(newSlot.item.currTimeShelfLife);
        else if(newSlot.item.itemType is ItemType.Instrument or ItemType.Light)
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
            if(objectOnScene) oldSlot.NullifySlotData();
        }
        else
        {
            transform.SetParent(oldSlot.transform);
            transform.position = oldSlot.transform.position;
        }
    }
}
