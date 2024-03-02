using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Script_Sbor : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerId != -1 && Application.isMobilePlatform != true) return;

        int derevo = (int)Char.GetNumericValue(this.name[0]);

        GameObject poda = this.gameObject.transform.parent.gameObject;

        if (derevo == 7)
        {
            Script_Load_Save.my_Trees[7] += 1;
            Script_Load_Save.MySaveTrees(); 

            Script_Garden garden = poda.transform.parent.parent.GetComponent<Script_Garden>();
            garden.Load_Trees();
        }

        int pod = int.Parse(poda.name.Substring(poda.name.Length - 1))-1;

        Script_Load_Save.my_Pods[pod] = 0;
        Script_Load_Save.MySavePods();
        Destroy(this.gameObject);

        

        Script_Load_Save.sheet = Script_Load_Save.sheet + Script_Garden.count[derevo];

        Script_Load_Save.MySaveSheet();

        TextMeshProUGUI _count = poda.transform.parent.parent.Find("TextSheet").gameObject.GetComponent<TextMeshProUGUI>();
        _count.text = "<sprite name=\"Listike\">" + Script_Load_Save.sheet.ToString();

    }

}
