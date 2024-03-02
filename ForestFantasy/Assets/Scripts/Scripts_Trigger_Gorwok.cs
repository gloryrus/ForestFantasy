using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Scripts_Trigger_Gorwok : MonoBehaviour
{
    public static GameObject _gorwok;
    private Image image;

    private void OnTriggerExit2D(Collider2D collision)
    {
        image = this.GetComponent<Image>();
        image.color = Color.white;
        _gorwok = null;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Drop")
        {
            if (Script_Load_Save.info == 7 || Script_Load_Save.info == 10 
                || _gorwok == this.gameObject.transform.parent.gameObject || _gorwok != null 
                || Script_Trigger_Pods._tree != null) { return; }

            if (this.transform.parent.childCount == 1)
            {
                image = this.GetComponent<Image>();
                image.color = Color.gray;
                _gorwok = this.gameObject.transform.parent.gameObject;
            }
        }
    }
}

