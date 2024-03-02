using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class Script_Load_Save : MonoBehaviour
{
    public static byte[] armors = new byte[100] { 1, 2, 1, 1, 0, 3, 3, 4, 3, 0, 4, 5, 4, 3, 5, 4, 3,
        5, 2, 3, 1, 2, 4, 1, 2, 1, 1, 3, 0, 0, 3, 4, 0, 2, 3, 4, 2, 3, 5, 0, 1, 0, 0, 3, 0, 2, 1,
        3, 2, 0, 0, 1, 4, 1, 0, 4, 2, 2, 0, 4, 4, 4, 3, 4, 0, 3, 2, 0, 4, 3, 3, 1, 4, 1, 0, 2, 2,
        0, 0, 4, 5, 1, 2, 1, 3, 4, 0, 0, 0, 3, 0, 0, 4, 5, 1, 5, 5, 5, 5, 5};

    public static byte[] forces = new byte[100] { 1 ,1, 2, 1, 4, 1, 0, 0, 2, 4, 1, 3, 2, 3, 1, 1, 1,
        0, 2, 1, 2, 0, 1, 4, 3, 1, 2, 3, 4, 3, 2, 1, 5, 2, 1, 2, 3, 4, 1, 4, 3, 3, 5, 2, 2, 3, 2,
        2, 1, 4, 4, 3, 0, 3, 4, 0, 2, 3, 4, 1, 2, 0, 2, 3, 5, 3, 1, 5, 0, 3, 2, 2, 1, 1, 3, 1, 4,
        4, 1, 2, 4, 2, 1, 4, 5, 3, 4, 2, 1, 1, 2, 5, 3, 1, 5, 5, 5, 5, 5, 5};

    public static int[] cost_Pot = new int[6] { 5, 50, 100, 200, 500, 500 };


    public static bool[] my_Inventory = Enumerable.Repeat(false, 100).ToArray();
    public static int[] my_Pack = Enumerable.Repeat(-1, 5).ToArray();

    public static byte my_Pots = 1;
    public static int[] my_Trees = new int[8];
    public static int[] my_Pods = new int[6];

    public static byte[] my_LoadTrees = new byte[8];
    public static string[] time_Pods = new string[6];

    public static int sheet = 0;
    public static int krutki = 0;

    public static int info = 0;
    public static int elki = 0;


    private void Start()
    {
        GetLoad();
    }
    public static void GetLoad()
    {
        
        if (Enab.ena) { return; }
        

        for(byte i = 0; i< my_Pack.Length; i++)
            my_Pack[i] = PlayerPrefs.GetInt("my_Pack" + i, -1);

        for(byte i = 0; i< my_Inventory.Length; i++)
            my_Inventory[i] = PlayerPrefs.GetInt("my_Inventory" + i, 0) 
                    == 1?true:false;
       
        my_Pots = Convert.ToByte(PlayerPrefs.GetInt("my_Pots", 1));
        
        for(byte i = 0; i< my_Trees.Length; i++)
            my_Trees[i] = PlayerPrefs.GetInt("my_Trees" + i);
        
        my_Trees[0] = PlayerPrefs.GetInt("my_Trees0", 1);

        for(byte i = 0; i< my_Pods.Length; i++)
            my_Pods[i] = PlayerPrefs.GetInt("my_Pods" + i);

        for(byte i = 0; i< my_LoadTrees.Length; i++)
            my_LoadTrees[i] = Convert.ToByte(PlayerPrefs.GetInt("my_LoadTrees" + i));

        for(byte i = 0; i< time_Pods.Length; i++)
            time_Pods[i] = PlayerPrefs.GetString("time_Pods" + i);

        sheet = PlayerPrefs.GetInt("sheet");

        krutki = PlayerPrefs.GetInt("krutki");

        info = PlayerPrefs.GetInt("info");

        elki = PlayerPrefs.GetInt("elki");

        //Активация скриптов
        Enab.ena = true;
        Script_Garden gar = GameObject.Find("Canvas").GetComponent<Script_Garden>();
        gar.enabled = true;
        gar.Start();


        Script_Info1 start = GameObject.Find("Empty").GetComponent<Script_Info1>();
        start.enabled = true;
        start.Start();
    }


    public static void MySaveInventory()
    {
        for(byte i = 0; i< my_Inventory.Length; i++)
            PlayerPrefs.SetInt("my_Inventory" + i, my_Inventory[i]?1:0);

        PlayerPrefs.Save();    
    }

    public static void MySavePack()
    {
        for(byte i = 0; i< my_Pack.Length; i++)
            PlayerPrefs.SetInt("my_Pack" + i, my_Pack[i]);

        PlayerPrefs.Save(); 
    }

    public static void MySavePots()
    {
        PlayerPrefs.SetInt("my_Pots", Convert.ToInt32(my_Pots));

        PlayerPrefs.Save(); 
    }

    public static void MySaveTrees()
    {
        for(byte i = 0; i< my_Trees.Length; i++)
            PlayerPrefs.SetInt("my_Trees" + i, my_Trees[i]);
            
        PlayerPrefs.Save(); 
    }
    public static void MySavePods()
    {
        for(byte i = 0; i< my_Pods.Length; i++)
            PlayerPrefs.SetInt("my_Pods" + i, my_Pods[i]);

        PlayerPrefs.Save(); 
    }
    public static void MySaveLoadTrees()
    {
        for(byte i = 0; i< my_LoadTrees.Length; i++)
            PlayerPrefs.SetInt("my_LoadTrees" + i, Convert.ToInt32(my_LoadTrees[i]));

        PlayerPrefs.Save(); 
    }
    public static void MySaveTime()
    {
        for(byte i = 0; i< time_Pods.Length; i++)
            PlayerPrefs.SetString("time_Pods" + i, time_Pods[i]);

        PlayerPrefs.Save(); 
    }

    public static void MySaveSheet()
    {
        PlayerPrefs.SetInt("sheet", sheet);
        PlayerPrefs.Save(); 
    }
    public static void MySaveKrutki()
    {
        PlayerPrefs.SetInt("krutki", krutki);
        PlayerPrefs.Save(); 
    }
    public static void MySaveInfo()
    {
        PlayerPrefs.SetInt("info", info);
        PlayerPrefs.Save(); 
    }
    public static void MySaveElki()
    {
        PlayerPrefs.SetInt("elki", elki);
        PlayerPrefs.Save(); 
    }
}
