﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveManager
{
    //Database Directory
    public static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";

    //Initialise Directory
    public static void Initialise()
    {
        if(!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    //Save Function
    public static void Save(string data)
    {
        File.WriteAllText(SAVE_FOLDER + "save.json", data);
    }

    //Load Function
    public static string Load()
    {
        if(File.Exists(SAVE_FOLDER + "save.json"))
        {
            string data = File.ReadAllText(SAVE_FOLDER + "save.json");
            return data;
        }
        else
        {
            return null;
        }
    }
}