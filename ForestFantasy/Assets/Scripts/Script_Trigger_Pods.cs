using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_Trigger_Pods : MonoBehaviour
{
    public static GameObject _tree;
    private Image image;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Drop")
        {
            if ((int.Parse(this.name) - int.Parse(collision.name)) == 1 && (int.Parse(this.name) != 7))
            {
                image = this.GetComponent<Image>();
                image.color = Color.white;
                _tree = null;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Drop")
        {
            if(Scripts_Trigger_Gorwok._gorwok != null || Script_Load_Save.info == 13 
                || _tree != null || _tree == this.gameObject.transform.parent.gameObject) { return; }

            if ((int.Parse(this.name) - int.Parse(collision.name)) == 1 && (int.Parse(this.name) != 7))
            {
                image = this.GetComponent<Image>();
                image.color = Color.gray;
                _tree = this.gameObject.transform.parent.gameObject;
            }

        }
    }
}

