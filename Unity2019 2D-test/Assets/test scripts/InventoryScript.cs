using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public List<ItemObj> myInventory = new List<ItemObj>();
    public int itemCounter = 0;

        //add to the inventory
        public void addToInventory(GameObject obj, int amount)
        {
            bool doesExist = false;
            int position = 0;
            for(int i = 0; i < myInventory.Count; i++) //iterate through items in list to find if item already exists
            {
                if(myInventory[i].item.Equals(obj.name))  
                {
                doesExist = true;
                position = i;
                }                  
            }

            if (doesExist)  //if item exists then add amount to item in list
             {
                myInventory[position].addQuantity(amount);
             }
            else            //otherwise item is new and add to list
            {
                myInventory.Add(new ItemObj(obj, amount));
                itemCounter++;
            } 
        }

        //remove from the inventory
        public void removeFromInventory(string name)
        {
            for (int i = 0; i < myInventory.Count; i++) //iterate through items in list
                {
                    if (myInventory[i].item == name)
                    {
                         myInventory.RemoveAt(i);
                    }
             }
        }
        //check if inventory contains item
        public bool inventoryContains(string name)
        {
            for (int i = 0; i < myInventory.Count; i++) //iterate through items in list
            {
                if (myInventory[i].item == name)
                {
                    return true;
                }
            }
            return false;
        }

    
}

public class ItemObj
{
    public string item;
    public int quantity;

    public ItemObj(GameObject a, int b)
    {
        item = a.name;
        quantity = b;
    }
    public void addQuantity(int value)
    {
        quantity += value;
    }
    

}