using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Script_Gachapon : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _count;
    [SerializeField] private TextMeshProUGUI _costGift;
    [SerializeField] private TextMeshProUGUI _costPot;
    [SerializeField] private TextMeshProUGUI _costElka;
    [SerializeField] private GameObject _pot;
    [SerializeField] private GameObject _elka;
    [SerializeField] private Animator animator;

   [SerializeField] private Image image;
    private Script_Gacha imageScript;

    public static bool Gift;

    private int costGift = 1;
    private int costPot = 1;
    private int costElka = 0;


    private void Start()
    {
        Pot_Invisible();
        Gachapon_Invisible();
        Elka_Invisible();
        Gift = true;
        _count.text = "<sprite name=\"Listike\">" +Script_Load_Save.sheet.ToString();



        costGift = (int)((Script_Load_Save.krutki + 1) * (Script_Load_Save.krutki / 1.1f));
        _costGift.text = "<sprite name=\"Listike\">" + costGift.ToString();

        costPot = Script_Load_Save.cost_Pot[Script_Load_Save.my_Pots-1];
        _costPot.text = "<sprite name=\"Listike\">" +costPot.ToString();

        costElka = (Script_Load_Save.elki+1)*100;
        _costElka.text = "<sprite name=\"Listike\">" + costElka.ToString();

        imageScript = image.GetComponent<Script_Gacha>();

    }
    void OnMouseDown()
    {
        if (Gift)
        {
            if (Script_Load_Save.sheet < costGift) { StartCoroutine(BanGift()); return; }

            Script_Load_Save.sheet -= costGift;
            Script_Load_Save.MySaveSheet();
            Script_Load_Save.krutki += 1;
            Script_Load_Save.MySaveKrutki();

            costGift = (int)((Script_Load_Save.krutki + 1) * (Script_Load_Save.krutki / 1.1f));
            _costGift.text = "<sprite name=\"Listike\">" +costGift.ToString();

            _count.text = "<sprite name=\"Listike\">" +Script_Load_Save.sheet.ToString();

            Gift = false;
            animator.SetBool("Anim_Gachapon", true);
            imageScript.Gift();
        }
    }

    public void EndAnimation()
    {
        animator.SetBool("Anim_Gachapon", false);
        
        imageScript.Anim();
        Gachapon_Invisible();
    }

    public void Buy_Pot()
    {

        if (Script_Load_Save.sheet < costPot) { StartCoroutine(BanPot()); return; }

        if (Script_Load_Save.my_Pots < 6)
        {
            Script_Load_Save.sheet -= costPot;
            Script_Load_Save.MySaveSheet();
            _count.text = "<sprite name=\"Listike\">" +Script_Load_Save.sheet.ToString();


            Script_Load_Save.my_Pots += 1;
            Script_Load_Save.MySavePots();


            costPot = Script_Load_Save.cost_Pot[Script_Load_Save.my_Pots-1];
            _costPot.text = "<sprite name=\"Listike\">" + costPot.ToString();

            Pot_Invisible();
        }
        else
        {
            Pot_Invisible();
        }
    }

    public void Buy_Elka(){
        if (Script_Load_Save.sheet < costPot) { StartCoroutine(BanElka()); return; }

        if (Script_Load_Save.elki < 6)
        {
            Script_Load_Save.sheet -= costElka;
            Script_Load_Save.MySaveSheet();
            _count.text = "<sprite name=\"Listike\">" +Script_Load_Save.sheet.ToString();


            Script_Load_Save.elki += 1;
            Script_Load_Save.MySaveElki();


            costElka = (Script_Load_Save.elki+1)*100;
            _costElka.text = "<sprite name=\"Listike\">" + costElka.ToString();

            Script_Load_Save.my_Trees[7] +=1;
            Script_Load_Save.MySaveTrees();

            Elka_Invisible();
        }
        else
        {
            Elka_Invisible();
        }
    }

    private void Pot_Invisible()
    {
        if(Script_Load_Save.my_Pots >= 6)
        {
            _pot.SetActive(false);
        }
    }
    #region Покупка ёлки
    
        public void Elka_Invisible()
        {
            if(Script_Load_Save.elki >= 6)
            {
                _elka.SetActive(false);
            }
        }
    #endregion


    
    
    private void Gachapon_Invisible()
    {
        if (Script_Load_Save.krutki >= 100)
        {
            _costGift.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator BanGift()
    {
        _costGift.color = Color.red;
        yield return new WaitForSeconds(3f);
        _costGift.color = Color.white;
    }

    IEnumerator BanPot()
    {
        _costPot.color = Color.red;
        yield return new WaitForSeconds(3f);
        _costPot.color = Color.white;
    }
    IEnumerator BanElka()
    {
        _costElka.color = Color.red;
        yield return new WaitForSeconds(3f);
        _costElka.color = Color.white;
    }
}
