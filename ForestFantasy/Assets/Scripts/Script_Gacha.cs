using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Script_Gacha : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private Image image;
    [SerializeField] private GameObject btnClose;
    private bool stop;
    private byte random;

    private void Start()
    {
        stop = false;
        btnClose.SetActive(false);
    }
    public void Gift()
    {
        random = (byte)Random.Range(0, 100);


        if (Script_Load_Save.my_Inventory[random] == true)
        {
            Gift();
        }

        btnClose.SetActive(true);
        Script_Load_Save.my_Inventory[random] = true;
        Script_Load_Save.MySaveInventory();


        
    }

    public void Anim()
    {
        image.gameObject.SetActive(true);

        image.sprite = Resources.Load<Sprite>("im_" + random);

        image.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = Script_Load_Save.forces[random].ToString();
        image.transform.GetChild(0).GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = Script_Load_Save.armors[random].ToString();
        animator.SetBool("Anim_Gift", true);
    }
    public void Stop()
    {
        stop = true;
    }

    public void Close()
    {
            if (stop)
        {
            animator.SetBool("Anim_Gift", false);
            stop = false;
            Script_Gachapon.Gift = true;
            btnClose.SetActive(false);
            image.gameObject.SetActive(false);
        }
    }
}
