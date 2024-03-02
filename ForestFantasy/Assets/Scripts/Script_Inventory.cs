using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Script_Inventory : MonoBehaviour
{
    [SerializeField] private Transform _contentInventory;
    [SerializeField] private Transform _contentPack;
    [SerializeField] private GameObject _prefab_Pictures;


    private int id_image;
    private void Start()
    {
        Script_Load_Save.GetLoad();

        Load_Inventory();
        Load_Pack();
    }
    private void Load_Inventory()
    {
        foreach (Transform child in _contentInventory) Destroy(child.gameObject);


        for (int i = 0; i < Script_Load_Save.my_Inventory.Length; i++)
        {
            if (Script_Load_Save.my_Inventory[i] == true)
            {

                GameObject icon = Instantiate(_prefab_Pictures, _contentInventory.transform);
                Image image = icon.transform.GetChild(0).GetComponent<Image>();
                image.sprite = Resources.Load<Sprite>("im_" + i);

                GameObject panel = icon.transform.GetChild(1).gameObject;
                

                Button bt = icon.transform.GetChild(2).gameObject.GetComponent<Button>();
                bt.onClick.AddListener(() => this.ClickImage(bt.gameObject) );

                GameObject text = panel.transform.GetChild(0).gameObject;
                TextMeshProUGUI texts = text.GetComponent<TextMeshProUGUI>();
                texts.text = (i + 1).ToString();

                panel.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = 
                    Script_Load_Save.forces[i].ToString();
                panel.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = 
                    Script_Load_Save.armors[i].ToString();

                Image pn = panel.GetComponent<Image>();
                if (i >= 95) { pn.color = new Color32(171, 67, 167, 210); }

                for (int j = 0; j < Script_Load_Save.my_Pack.Length; j++)
                {
                    if (i == Script_Load_Save.my_Pack[j])
                    {
                        image.color = Color.cyan;
                        break;
                    }
                }

            }
        }
}
    private void Add_Pack(int id)
    {

        for (int i = 0; i < Script_Load_Save.my_Pack.Length; i++)
        {
            if (Script_Load_Save.my_Pack[i] == -1)
            {
                Script_Load_Save.my_Pack[i] = id;
                break;
            }
        }

        Script_Load_Save.MySavePack();
        Load_Pack();
        Load_Inventory();
    }

    private void Delete_Pack(int id)
    {
        int a = 0;

        for (int i = 0; i < Script_Load_Save.my_Pack.Length; i++)
        {
            if (Script_Load_Save.my_Pack[i] == id)
            {
                Script_Load_Save.my_Pack[i] = -1;
            }

            if (Script_Load_Save.my_Pack[i] == -1 && i < 4)
            {
                a = Script_Load_Save.my_Pack[i];
                Script_Load_Save.my_Pack[i] = Script_Load_Save.my_Pack[i + 1];
                Script_Load_Save.my_Pack[i + 1] = a;
            }
        }


        Script_Load_Save.MySavePack();

        Load_Pack();
        Load_Inventory();
    }


    private void Load_Pack()
    {
        foreach (Transform child in _contentPack) Destroy(child.gameObject);

        foreach (var slot in Script_Load_Save.my_Pack)
        {
            if (slot == -1) { continue; }

            GameObject floor = Instantiate(_prefab_Pictures, _contentPack.transform);
            GameObject icon = floor.transform.GetChild(0).gameObject;
            Image image = icon.GetComponent<Image>();
            image.sprite = Resources.Load<Sprite>("im_" + slot);

            GameObject panel = floor.transform.GetChild(1).gameObject;
            panel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = 
                (slot + 1).ToString();

            Button bt = floor.transform.GetChild(2).gameObject.GetComponent<Button>();
                bt.onClick.AddListener(() => this.ClickImage(bt.gameObject) );

            panel.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 50);
            panel.transform.GetChild(0).gameObject.SetActive(false);

            panel.transform.GetChild(1).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text =
                Script_Load_Save.forces[slot].ToString();
            panel.transform.GetChild(2).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text =
                Script_Load_Save.armors[slot].ToString();

            panel.transform.GetChild(1).gameObject.transform.localPosition = new Vector2(-70, 0);
            panel.transform.GetChild(2).gameObject.transform.localPosition = new Vector2(35, 0);
            Destroy(panel.transform.GetChild(3).gameObject);
        }
    }
    public void ClickImage(GameObject gm){
        try
        {
            gm = gm.transform.parent.gameObject;

            GameObject text = gm.transform.GetChild(1).GetChild(0).gameObject;
            TextMeshProUGUI texts = text.GetComponent<TextMeshProUGUI>();

            id_image = (int.Parse(texts.text) - 1);

            for (int i = 0; i < Script_Load_Save.my_Pack.Length; i++)
            {
                if (Script_Load_Save.my_Pack[i] == id_image)
                {
                    Delete_Pack(id_image);
                    return;
                }
            }
            Add_Pack(id_image);
        }
        catch {; }
    }
}

