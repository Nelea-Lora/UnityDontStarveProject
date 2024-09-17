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
    private Vector3 _boxSize = new Vector3(2, 2, 2);
    [SerializeField] private LayerMask _placementMask;
    private MeshRenderer meshRenderer;
    private void Start()
    {
        oldSlot = transform.GetComponentInParent<InventorySlot>();
        _camera = Camera.main;
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
            if (newSlot != null)
            {
                ExchangeSlotData(newSlot);
            }
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
                // var newObjectPosition = eventData.pointerCurrentRaycast.worldPosition;
                var newObjectPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                newObjectPosition.z = -1f;
                // PlaceObject(newObjectPosition);
                PlaceObject(newObjectPosition);
                CreateCubeMesh();
            }
        }
    }
    void ExchangeSlotData(InventorySlot newSlot)
    {
        ItemScriptableObject item = newSlot.item;
        int amount = newSlot.amount;
        bool isComplete = newSlot.isComplete;
        Sprite iconGo = newSlot.iconGO.sprite;
        
        newSlot.item = oldSlot.item;
        newSlot.amount = oldSlot.amount;
        if (oldSlot.isComplete)
        {
            newSlot.SetIcon(oldSlot.iconGO.sprite);
            newSlot.itemAmount.text = oldSlot.amount.ToString();
        }
        else
        {
            newSlot.iconGO.color = new Color(1, 1, 1, 0);
            newSlot.iconGO.sprite = null;
            newSlot.itemAmount.text = "";
        }
        newSlot.isComplete = oldSlot.isComplete;
        oldSlot.item = item;
        oldSlot.amount = amount;
        if (isComplete)
        { 
            oldSlot.SetIcon(iconGo);
            oldSlot.itemAmount.text = amount.ToString();
        }
        else
        {
            oldSlot.iconGO.color = new Color(1, 1, 1, 0);
            oldSlot.iconGO.sprite = null;
            oldSlot.itemAmount.text = "";
        }
        oldSlot.isComplete = isComplete;
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
