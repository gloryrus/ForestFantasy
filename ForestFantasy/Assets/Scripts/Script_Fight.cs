using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Script_Fight : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _hpBoss;
    [SerializeField] private Image _hpBossImage;
    [SerializeField] private GameObject _boss;

    [SerializeField] private TextMeshProUGUI _hpPlayer;
    [SerializeField] private TextMeshProUGUI _forcePlayer;
    [SerializeField] private TextMeshProUGUI _armorPlayer;
    [SerializeField] private TextMeshProUGUI _count;
    [SerializeField] private TextMeshProUGUI _count_2;
    [SerializeField] private TextMeshProUGUI _count_3;

    [SerializeField] private GameObject _backGroung;
    [SerializeField] private GameObject _exit;
    [SerializeField] private GameObject _gift;
    [SerializeField] private GameObject _gift_V2;
    [SerializeField] private GameObject _reklama;
    [SerializeField] private GameObject _exitclick;


    private float[] hp_Boss_ = new float[3] {50f, 500f, 1000f};
    private float hp_Boss = 0f;

    private float[] hit_Boss_ = new float[3] { 5f, 50f, 75f };
    private float hit_Boss = 0f;

    private byte boss = 0;
    private bool spawn = false;
    private bool aaa = true;

    private float hp_Player = 100f;
    private float hit_Player = 1f;

    private bool jump = false;
    private bool fall = false;

    private int force = 0;
    private int armor = 0;
    private int count = 0;
    private int sheet = 0;
    private Transform tr;

    void Start()
    {
        tr = _boss.transform;

        Respawn();

        foreach (var slot in Script_Load_Save.my_Pack)
        {
            if (slot == -1) { continue; }
            force += Script_Load_Save.forces[slot];
            armor += Script_Load_Save.armors[slot];
        }
        _forcePlayer.text = force.ToString();
        _armorPlayer.text = armor.ToString();


        for (int i = 0; i < force; i++)
        {
            hit_Player *= 1.18f;
        }
    }


    void Update()
    {
        if (_hpBossImage.fillAmount < hp_Boss / hp_Boss_[boss - 1] && spawn == true)
        {
            _hpBossImage.fillAmount += Time.deltaTime / 2;
            _hpBoss.text = Math.Round(_hpBossImage.fillAmount * hp_Boss) + "/" + hp_Boss_[boss - 1];
            tr.position = Vector2.Lerp(tr.position, new Vector2(0, -3) ,  Time.deltaTime);
            return;
        }
        else
        {
            if (aaa)
            {
                jump = false;
                fall = false;
                spawn = false;
                aaa = false;
                tr.localPosition = new Vector2(0, -150);
            }
        }



        if (jump)
        {
            tr.position = Vector2.Lerp(tr.position, new Vector2(0, 2f), 5 * Time.deltaTime);
        }
        else if (fall)
        {
            tr.position = Vector2.Lerp(tr.position, new Vector2(0, -3f), 5 * Time.deltaTime);
        }

        if (tr.localPosition.y >= 50)
        {
            jump = false;
            fall = true;
        }
        else if (tr.localPosition.y <= -150 && fall == true)
        {
            jump = false;
            fall = false;
        }
    }

    private void Respawn()
    {
        boss += 1;
        Image im = _boss.GetComponent<Image>();
        im.sprite = Resources.Load<Sprite>("m" + boss);

        hit_Boss = hit_Boss_[boss - 1];
        for (int i = 0; i < armor; i++)
        {
            hit_Boss *= 0.85f;
        }
        tr.localPosition = new Vector2(0, 979);
        spawn = true;
        aaa = true;
        hp_Boss = hp_Boss_[boss - 1];
    }

    public void Hit()
    {
        if (spawn == true || _backGroung.active == true) return;

        tr.localPosition = new Vector2(0, -150);
        jump = true;
        HitBoss();
        HitPlayer();

        if (hp_Player < 1)
        {
            _hpPlayer.text = 0.ToString();
            Losing();
        }
        else
        {
            if (hp_Boss < 1)
            {
                _hpBoss.text = 0.ToString();
                _hpBossImage.fillAmount = 0;
                Victory();
            }
        }
    }
    private void HitBoss()
    {
        hp_Player -= hit_Boss;
        _hpPlayer.text = (int)Math.Round(hp_Player) + "/100";
    }
    private void HitPlayer()
    {
        StartCoroutine(Damage());
        hp_Boss -= hit_Player;
        _hpBoss.text = (int)Math.Round(hp_Boss) + "/" + hp_Boss_[boss - 1];
        _hpBossImage.fillAmount = hp_Boss / hp_Boss_[boss-1];
    }
    private void Victory()
    {
        if (boss <3)
        {
            Respawn();
            return;
        }

        _backGroung.SetActive(true);
        _gift.SetActive(true);

        StartCoroutine(Exits());
        count = 30;
        _count.text = count.ToString();

        Script_Load_Save.my_Trees[0] += count;
        Script_Load_Save.MySaveTrees();

        sheet = 50;
        Script_Load_Save.sheet += sheet;
        Script_Load_Save.MySaveSheet();

    }
    private void Losing()
    {
        Gift();
        _backGroung.SetActive(true);

        if (sheet == 0)
        {
            _gift.SetActive(true);
            _count.text = count.ToString();
        }
        else
        {
            _gift_V2.SetActive(true);
            _count_2.text= count.ToString();
            _count_3.text= sheet.ToString();
        }

        StartCoroutine(Exits());

        Script_Load_Save.sheet += sheet;
        Script_Load_Save.MySaveSheet();

        Script_Load_Save.my_Trees[0] += count;
        Script_Load_Save.MySaveTrees();

        if (Script_Load_Save.info == 2)
        {
            Script_Load_Save.info = 3;
            Script_Load_Save.MySaveInfo();
        }
        
    }

    private void Gift()
    {
        if(boss == 1)
        {
            if (hp_Boss <= 0) { count = 3; return; }
            if (hp_Boss <= 20) { count = 2; return; }
            if (hp_Boss <= 49) { count = 1; return; }
        }
        if (boss == 2)
        {
            if (hp_Boss <= 0) { count = 10; sheet = 20; return; }
            if (hp_Boss <= 100) { count = 9; sheet = 15; return; }
            if (hp_Boss <= 200) { count = 8; sheet = 15; return; }
            if (hp_Boss <= 300) { count = 7; sheet = 10; return; }
            if (hp_Boss <= 400) { count = 6; sheet = 10; return; }
            if (hp_Boss <= 500) { count = 5; sheet = 5; return; }
        }
        if (boss == 3)
        {
            if (hp_Boss <= 0) { count = 30; sheet = 50; return; }
            if (hp_Boss <= 200) { count = 18; sheet = 40; return; }
            if (hp_Boss <= 400) { count = 16; sheet = 40; return; }
            if (hp_Boss <= 600) { count = 14; sheet = 30; return; }
            if (hp_Boss <= 800) { count = 12; sheet = 30; return; }
            if (hp_Boss <= 1000) { count = 11; sheet = 20; return; }
        }
    }

    public void Close()
    {
        Gift();

        Script_Load_Save.sheet += sheet;
        Script_Load_Save.MySaveSheet();

        Script_Load_Save.my_Trees[0] += count;
        Script_Load_Save.MySaveTrees();
    }


    public void Exit()
    {
        _backGroung.SetActive(true);
        _exit.SetActive(true);
        _exitclick.SetActive(false);
    }

    public void No()
    {
        _backGroung.SetActive(false);
        _exit.SetActive(false);
    }


    IEnumerator Damage()
    {
        Image im = _boss.GetComponent<Image>();
        im.color = new Color32(255,148,148,255);
        yield return new WaitForSeconds(0.3f);
        im.color = Color.white;
    }

    IEnumerator Exits()
    {
        yield return new WaitForSeconds(1f);
        _exitclick.SetActive(true);
    }
}
