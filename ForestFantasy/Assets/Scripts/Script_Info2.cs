using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_Info2 : MonoBehaviour
{
    [SerializeField] GameObject _blackScreen;
    [SerializeField] Image _game;
    [SerializeField] Image _game1;

    private bool jump = false;
    private Image _color;
    void Start()
    {
        if (Script_Load_Save.info == 14)
        {
            this.GetComponent<Script_Info2>().enabled = false;
        }
        else{
            if (Script_Load_Save.info == 1)
            {
                Script_Load_Save.info = 2;
                Script_Load_Save.MySaveInfo();
                Two();
            }
        }
    }

    void Update()
    {
        if (Script_Load_Save.info == 3)
        {
            Three();
        }
        Podsvet();
    }
    private void Podsvet()
    {
        if (_color == null) { return; }
            if (_color.color.g >= 0.9f) { jump = false; }
            if (_color.color.g <= 0.1f) { jump = true; }

            if (jump == false)
            {
                _color.color = Color.Lerp(_color.color, new Color32(255, 0, 0, 255), Time.deltaTime);
            }
            else
            {
                _color.color = Color.Lerp(_color.color, new Color32(255, 255, 255, 255), Time.deltaTime);
            }
    }
    private void Two()
    {
        _blackScreen.SetActive(true);
        _color = _game;
    }
    private void Three()
    {
        Script_Load_Save.info = 4;
        Script_Load_Save.MySaveInfo();

        _color.color = new Color32(255, 255, 255, 255);
        _color = _game1;
    }

}
