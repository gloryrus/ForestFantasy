using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class Script_Info1 : MonoBehaviour
{
    [SerializeField] GameObject _blackScreen;
    [SerializeField] Image _game;
    [SerializeField] GameObject treees;
    [SerializeField] GameObject pods;
    [SerializeField] GameObject strelka1;
    [SerializeField] GameObject strelka2;

    private bool jump = false;
    public static bool ai = false;
    private Image _color;


    public void Start()
    {
        if(Enab.ena==false || Script_Load_Save.info == 14) {
            this.GetComponent<Script_Info1>().enabled = false;
        }
        else{
            if (Script_Load_Save.info < 4)
            {
                Script_Load_Save.info = 1;
                Script_Load_Save.MySaveInfo();
                One();
            }
            else{
                    if (Script_Load_Save.info == 10 || Script_Load_Save.info == 13 || 
                                Script_Load_Save.info == 7)
                    {
                        Script_Load_Save.info -= 1;
                    }
                    else{
                        if(Script_Load_Save.info == 9 || Script_Load_Save.info == 12 || 
                                Script_Load_Save.info == 6)
                        {
                            Script_Load_Save.info -= 1;
                        }
                    }
            }
        }   
    }
    private void Update()
    {
        if (Script_Load_Save.info == 14)
        {
            Cl();
            Strelka2Off();
            _blackScreen.SetActive(false);
            enabled = false;
        }
        else{
            if (Script_Load_Save.info == 5)
            {
                Cl();
                Three();
                Strelka1On();
            }
            else{
                if (Script_Garden.drag && Script_Load_Save.info == 6)
                {
                    Cl();
                    Four();
                    Strelka1On();
                }
                else{
                    if (Script_Load_Save.info == 8)
                    {
                        Cl();
                        Three();
                        Strelka1On();
                    }
                    else{
                        if (Script_Garden.drag && Script_Load_Save.info == 9)
                        {
                            Cl();
                            Four();
                            Strelka1On();
                        }
                        else{
                            if (Script_Load_Save.info == 11)
                            {
                                Cl();
                                Four();
                                Strelka1Off();
                                Strelka2On();
                            }
                            else{
                                if (Script_Garden.drag && Script_Load_Save.info == 12)
                                {
                                    Cl();
                                    Five();
                                    Strelka2On();
                                }
                            }
                        }
                    }
                }
            }
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
    private void One()
    {
        _game.transform.SetAsLastSibling();
        treees.transform.SetSiblingIndex(4);
        _blackScreen.SetActive(true);
        _color = _game;
    }
    private void Three()
    {
        Script_Load_Save.info += 1;
        Script_Load_Save.MySaveInfo();

        _game.transform.SetSiblingIndex(3);
       treees.transform.SetSiblingIndex(7);
        _blackScreen.SetActive(true);


        _color = treees.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>();

    }
    private void Four()
    {
        Script_Load_Save.info += 1;
        Script_Load_Save.MySaveInfo();

        _game.transform.SetSiblingIndex(3);
        pods.transform.SetSiblingIndex(5);
        treees.transform.SetSiblingIndex(7);
        _blackScreen.SetActive(true);

        _color = treees.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Image>();
    }
    private void Five()
    {
        Script_Load_Save.info += 1;
        Script_Load_Save.MySaveInfo();

        _game.transform.SetSiblingIndex(3);
        _blackScreen.SetActive(true);


        _color = pods.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>();
        pods.transform.SetSiblingIndex(7);
    }
    private void Strelka1On()
    {
        strelka1.SetActive(true);
    }
    private void Strelka1Off()
    {
        strelka1.SetActive(false);
    }
    private void Strelka2On()
    {
        strelka2.SetActive(true);
    }
    private void Strelka2Off()
    {
        strelka2.SetActive(false);
    }
    private void Cl()
    {
        if(_color != null)
        {
            _color.color = new Color(1,1,1);
            _color = null;
        }
    }
}
