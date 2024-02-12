
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class Inventario : MonoBehaviour
{
    private bool inventarioActivo;
    public GameObject inventario;
    private int allSlots;
    private int activoSlot;
    private GameObject[] slot;
    public GameObject slotHolder;
    public GameObject ItemObject;
    public float distanceInFront = 2.0f;
    public KeyCode removeKey = KeyCode.E;
    public GameObject[] paneles;



    void Start()
    {
        allSlots = slotHolder.transform.childCount;
        slot = new GameObject[allSlots];
        for (int i = 0; i < allSlots; i++)
        {
            slot[i] =slotHolder.transform.GetChild(i).gameObject;

            if (slot[i].GetComponent<Slot>().item == null)
            {
                slot[i].GetComponent<Slot>().empty = true;
            }
        }
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) 
        {
            inventarioActivo = !inventarioActivo;
        }
        if (inventarioActivo == true)
        {
            inventario.SetActive(true);
        }
        else
        {
            inventario.SetActive(false);
        }
        eliminar();
      
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item")
        {
            GameObject itemPickedUp = other.gameObject;
            Item item = itemPickedUp.GetComponent<Item>();
            AddItem(itemPickedUp, item.iD, item.type, item.descripcion, item.icon);
        }
    }

    public void AddItem(GameObject itemObject, int itemID, string itemType, string itemDescripcion, Sprite itemIcon)
    {
        for (int i = 0; i < allSlots; i++)
        {
            if (slot[i].GetComponent<Slot>().empty)
            {
                itemObject.GetComponent<Item>().pickedUp = true;
                slot[i].GetComponent<Slot>().item = itemObject;
                slot[i].GetComponent<Slot>().iD = itemID;
                slot[i].GetComponent<Slot>().type = itemType;
                slot[i].GetComponent<Slot>().descripcion = itemDescripcion;
                slot[i].GetComponent<Slot>().icon = itemIcon;

                itemObject.transform.parent = slot[i].transform;
                itemObject.SetActive(false);

                slot[i].GetComponent<Slot>().UpdateSlot();



                slot[i].GetComponent<Slot>().empty = false;

            }
             return;
            
        }
    }
    void eliminar()
    {
        for (int i = 0; i <= paneles.Length; i++)
        {

            if (Input.GetKeyDown(removeKey))
            {
                Item itemComponent = paneles[i].GetComponentInChildren<Item>();
                
                paneles[i].GetComponentInChildren<Image>().enabled=false;


                Destroy(gameObject);
                if (itemComponent != null)
                {
                    itemComponent.transform.parent = null;
                    
                }
                break;
            }
        }
    }


}
