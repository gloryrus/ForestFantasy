using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Script_Garden : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private GameObject Pods;
    [SerializeField] private Transform _contentTrees;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject _prefab_Trees;


    [SerializeField] private GameObject _prefab_1;
    [SerializeField] private GameObject _prefab_2;
    [SerializeField] private GameObject _prefab_3;
    [SerializeField] private TextMeshProUGUI _count;
    [SerializeField] private GameObject _1;
    [SerializeField] private GameObject _2;
    [SerializeField] private GameObject _3;
    [SerializeField] private GameObject _4;


    private GameObject drag_Tree;
    private RectTransform _rectTransform;

    private GameObject[] my_Gorwok = new GameObject[6];
    private TextMeshProUGUI[] timer = new TextMeshProUGUI[6];

    private int id;
    private TextMeshProUGUI textCount;
    public static bool drag = false;
    private int _touch;

    private byte[] nums = new byte[8] { 0, 2, 2, 2, 2, 2, 2, 0 };
    private int[] time_Trees = new int[8] { 5, 10, 15, 20, 25, 30, 30, 10 };
    private DateTime[] dt = new DateTime[6];

    public static int[] count = new int[8] { 1, 2, 5, 11, 25, 51, 110, 50 };

    private float second = 0f;
    public void Start()
    {
        if(Enab.ena) 
        {
           _1.SetActive(true);
            _2.SetActive(true);
            _3.SetActive(true);
            _4.SetActive(true);
            Reset();
        }
        else{
             _1.SetActive(false);
            _2.SetActive(false);
            _3.SetActive(false);
            _4.SetActive(false);
            this.GetComponent<Script_Garden>().enabled = false;
        }
    }
    void Update()
    {

        second += Time.deltaTime;

        if (second >= 0.2)
        {
            second = 0f;
            //    ////������ � ��������
            for (int i = 0; i < timer.Length; i++)
            {

                if (timer[i] == null) { continue; }

                TimeSpan ad = dt[i] - DateTime.UtcNow;

                timer[i].text = ad.ToString("ss");

                if (ad.TotalSeconds <= 0 && timer[i].name == "2")
                {
                    Load_Pod(i, 3);
                }
                else
                {
                    if (ad.TotalSeconds <= time_Trees[Script_Load_Save.my_Pods[i] - 1]/2 && timer[i].name == "1")
                    {
                        Load_Pod(i, 2);
                    }
                }
            }
        }
    }

    public void Reset()
    {
        UpdateGorwok();
        UpdateCount();
        Load_Pods();
        Load_Trees();
    }

    private void UpdateGorwok()
    {
        for (int i = 0; i < Script_Load_Save.my_Pots; i++)
        {
            my_Gorwok[i] = null;
            GameObject gorwok = Pods.transform.GetChild(i).gameObject;
            gorwok.SetActive(true);
            my_Gorwok[i] = gorwok;
        }
    }

    private void UpdateCount()
    {
        _count.text = "<sprite name=\"Listike\">" + Script_Load_Save.sheet.ToString();
    }

    private void PodgonTime(int i)
    {
        dt[i] = DateTime.Parse(Script_Load_Save.time_Pods[i]);
    }

    public void Load_Trees()
    {
        foreach (Transform child in _contentTrees) Destroy(child.gameObject);

        for (int i = 0; i < Script_Load_Save.my_Trees.Length; i++)
        {

            while (Script_Load_Save.my_LoadTrees[i] >= nums[i] && nums[i] != 0)
            {
                Script_Load_Save.my_LoadTrees[i] -= nums[i];
                Script_Load_Save.my_Trees[i] += 1;
                Script_Load_Save.MySaveTrees();
                Script_Load_Save.MySaveLoadTrees();
            }

            GameObject icon = _prefab_Trees.transform.GetChild(0).gameObject;
            icon.name = i.ToString();
            Image image = icon.GetComponent<Image>();
            image.sprite = Resources.Load<Sprite>("tr_" + (i + 1) + "_1");

            GameObject text = _prefab_Trees.transform.GetChild(1).gameObject;
            TextMeshProUGUI texts = text.GetComponent<TextMeshProUGUI>();
            texts.text = Script_Load_Save.my_Trees[i].ToString();

            GameObject panel = _prefab_Trees.transform.GetChild(2).gameObject;
            Image pi = panel.GetComponent<Image>();

            if (nums[i] == 0)
            {
                pi.fillAmount = 0;
            }
            else
            {
                pi.fillAmount = Script_Load_Save.my_LoadTrees[i] / (float)nums[i];
            }

            _prefab_Trees.name = (i + 1).ToString();
            Instantiate(_prefab_Trees, _contentTrees.transform);
        }
    }
    private void Load_Pods()
    {
        for (int i = 0; i < Script_Load_Save.my_Pods.Length; i++)
        {
            if (Script_Load_Save.my_Pods[i] <= 0) continue;

            Load_Pod(i, 1);
        }

        if (Script_Load_Save.info == 4)
        {
            Script_Load_Save.info = 5;
            Script_Load_Save.MySaveInfo();
        }
    }


    private void Load_Pod(int i, int v)
    {
        if(timer[i] != null)
        {
            Delete_Pod(i);
        }

        GameObject icon = gameObject;
        if (v == 1) {icon = _prefab_1; }
        if (v == 2) {icon = _prefab_2; }
        if (v == 3) { icon = _prefab_3; }

        GameObject pod = Instantiate(icon, my_Gorwok[i].transform);
        pod.name = (Script_Load_Save.my_Pods[i]-1).ToString();
        Image image = pod.GetComponent<Image>();
        image.sprite = Resources.Load<Sprite>("tr_" + Script_Load_Save.my_Pods[i].ToString() + "_" + v);
        timer[i] = pod.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        timer[i].name = v.ToString();
        my_Gorwok[i].transform.Find("Gorwok").transform.SetAsLastSibling();

        if (v == 3)
        {
            if (Script_Load_Save.my_Pods[i] == 1) { pod.transform.localPosition = new Vector2(-20, 145); }
            if (Script_Load_Save.my_Pods[i] == 2) { pod.transform.localPosition = new Vector2(100, 145); }
            if (Script_Load_Save.my_Pods[i] == 8) { pod.transform.localPosition = new Vector2(0, 180); }
            timer[i] = null;
        }

        PodgonTime(i);

    }
    private void Delete_Pod(int i)
    {
        timer[i] = null;

        Destroy(my_Gorwok[i].transform.GetChild(0).gameObject);
    }

    public void OnDrag(PointerEventData eventData)
    {
        try
        {
            if (eventData.pointerId != -1 && Application.isMobilePlatform != true){ return; }
            if (Application.isMobilePlatform && eventData.pointerId != _touch) { return; }
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }
        catch { };
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        try
        {
            if ((eventData.pointerId != -1 && Application.isMobilePlatform != true) || Script_Load_Save.info == 1) { return; }
            if (drag) { return; }
            GameObject gm = eventData.pointerCurrentRaycast.gameObject.transform.parent.gameObject;
            if (gm.layer != 7) { return; }

            GameObject count = gm.transform.GetChild(1).gameObject;
            textCount = count.GetComponent<TextMeshProUGUI>();
            if (textCount.text == "0") { return; }


            drag_Tree = Instantiate(gm, _canvas.transform) as GameObject;
            drag_Tree.transform.SetAsLastSibling();

            GameObject panel = drag_Tree.transform.GetChild(2).gameObject;
            panel.SetActive(false);

            if (drag_Tree.transform.childCount == 4)
                drag_Tree.transform.GetChild(3).gameObject.SetActive(false);

            drag_Tree.transform.GetChild(0).gameObject.tag = "Drop";

            GameObject image = drag_Tree.transform.GetChild(0).gameObject;
            Image color = image.GetComponent<Image>();
            color.color = new Color(1, 1, 1);


            GameObject text = drag_Tree.transform.GetChild(1).gameObject;
            TextMeshProUGUI texts = text.GetComponent<TextMeshProUGUI>();
            texts.text = "";

            drag_Tree.transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
            _rectTransform = (RectTransform)drag_Tree.transform;

            //����� ����
            textCount.text = (int.Parse(textCount.text) - 1).ToString();

            id = (int)Char.GetNumericValue(drag_Tree.name[0]);


            if (Application.isMobilePlatform) { _touch = eventData.pointerId; }

            drag = true;
        }
        catch { };
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        try
        {
            if (eventData.pointerId != -1 && Application.isMobilePlatform != true) { return; }
            if (Application.isMobilePlatform && eventData.pointerId != _touch) { return; }
            if (drag == false) { return; }

            if (Scripts_Trigger_Gorwok._gorwok != null)
            {
                Script_Load_Save.my_Trees[id-1] -= 1;
                Script_Load_Save.MySaveTrees();

                if (Script_Load_Save.info == 13)
                {
                    Script_Load_Save.info = 14;
                    Script_Load_Save.MySaveInfo();
                }


                Landing();


            }
            else
            {
                //�������� �� ������� ������
                if (Script_Trigger_Pods._tree != null)
                {
                    int i = (int)Char.GetNumericValue(Script_Trigger_Pods._tree.name[0]) - 1;

                    Script_Load_Save.my_LoadTrees[i] += 1;
                    Script_Load_Save.MySaveLoadTrees();

                    Script_Load_Save.my_Trees[id - 1] -= 1;
                    Script_Load_Save.MySaveTrees();
                    Load_Trees();


                    if (Script_Load_Save.info == 7)
                    {
                        Script_Load_Save.info = 8;
                        Script_Load_Save.MySaveInfo();

                    }
                    if (Script_Load_Save.info == 10)
                    {
                        Script_Load_Save.info = 11;
                        Script_Load_Save.MySaveInfo();

                    }
                }
                else
                {
                    textCount.text = (int.Parse(textCount.text) + 1).ToString();

                    if (Script_Load_Save.info == 7)
                    {
                        Script_Load_Save.info = 5;
                        Script_Load_Save.MySaveInfo();
                    }
                    if (Script_Load_Save.info == 10)
                    {
                        Script_Load_Save.info = 8;
                        Script_Load_Save.MySaveInfo();
                    }
                    if (Script_Load_Save.info == 13)
                    {
                        Script_Load_Save.info = 11;
                        Script_Load_Save.MySaveInfo();
                    }
                }
            }
            drag = false;
            Destroy(drag_Tree);
        }
        catch { };
    }

    private void Landing()
    {
        int a = int.Parse(Scripts_Trigger_Gorwok._gorwok.name.Substring(Scripts_Trigger_Gorwok._gorwok.name.Length - 1))-1;
        Script_Load_Save.my_Pods[a] = id;
        Script_Load_Save.MySavePods();

        int b = time_Trees[Script_Load_Save.my_Pods[a] - 1];
        DateTime ad = DateTime.UtcNow.AddSeconds(b);

        Script_Load_Save.time_Pods[a] = ad.ToString();
        Script_Load_Save.MySaveTime();

        Load_Pod(a, 1);
    }
}
