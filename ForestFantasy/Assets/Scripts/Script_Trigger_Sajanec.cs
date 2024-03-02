using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_Trigger_Sajanec : MonoBehaviour
{
    public static int _gorwok_Id = -1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Drop")
        {
            if (_gorwok_Id != -1) { return; }
            if ((int)Char.GetNumericValue(this.name[0]) == ((int)Char.GetNumericValue(collision.name[0]) + 1))
            {
                Image image = this.GetComponent<Image>();
                image.color = Color.gray;
                _gorwok_Id = int.Parse(this.transform.parent.name.Substring(this.transform.parent.name.Length - 1)) - 1;
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Drop")
        {
            if ((int)Char.GetNumericValue(this.name[0]) == ((int)Char.GetNumericValue(collision.name[0]) + 1))
            {
                Image image = this.GetComponent<Image>();
                image.color = Color.white;
                _gorwok_Id = -1;
            }
        }
    }
}
