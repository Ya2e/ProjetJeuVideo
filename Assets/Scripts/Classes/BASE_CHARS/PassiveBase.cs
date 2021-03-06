﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

/** PassiveBase, public abstract class
 * This class should be the mother class of all Passives of our game.
 * The main goal of this script is to create a passive from its JSON file.
 **/
public abstract class PassiveBase : MonoBehaviour
{

    /** Fields of PassiveBase
     * The Passive base is constructed with a PassiveData that comes from the JSON File
     * Other fields are public field initialized in the Awake Method with the values of the JSON.
     * We should always work with these public fields and never with the raw data of the JSON.
     **/
    #region Fields  	
    private PassiveData _passiveDefinition { get; set; }
    public string Name;
    public int[] Damages;
    public string[] DamagesType;
    public string[] OtherValues;
    public int NumberOfStacks;
    public string[] Description;
    #endregion

    #region Functional methods
    /** Awake protected void Method
	 * The Awake ensure a good construction of the passive from the JSON by calling the LoadSpellData.
	 * After that, we transmit all values to public fields that will be used by the other scripts.
	 **/
    protected void Awake()
    {
        LoadSpellData("PassiveData.json");
        NumberOfStacks = _passiveDefinition.NumberOfStacks;
        Description = _passiveDefinition.Description;
        Name = _passiveDefinition.Name;
        Damages = _passiveDefinition.Damages;
        DamagesType = _passiveDefinition.DamagesType;
        OtherValues = _passiveDefinition.OtherValues;
        NumberOfStacks = _passiveDefinition.NumberOfStacks;
        Description = _passiveDefinition.Description;
    }

    /** LoadSpellData, protected void
	 * @Params : string
	 * Loads the JSON PassiveData associated to the Passive.
	 **/
    protected void LoadSpellData(string json)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, json);
        if (File.Exists(filePath))
        {
            string jsonFile = File.ReadAllText(filePath);
            PassiveData[] data = JsonHelper.getJsonArray<PassiveData>(jsonFile);
            foreach (PassiveData passive in data)
            {
                if (passive.ScriptName == this.GetType().ToString())
                {
                    _passiveDefinition = passive;
                    break;
                }
            }
        }
        else
        {
            Debug.LogError("Cannot load game data on : " + this.GetType().ToString());
        }
    }

    /**GetDescriptionGUI, public string Method
	 * Return the description of our passive built by the PassiveDescriptionBuilder of the StringHelper static class.
	 * This method allows to get a dynamic and colored description on the screen.
	 **/
    public string GetDescriptionGUI()
    {
        return StringHelper.PassiveDescriptionBuilder(this);
    }

    #endregion

    #region Serializable Classes
    /** PassiveData public Serializable class
	 * This class was created to be at the service of the PassiveBase class
	 * This class contains all elements to construct a Passive from the JSON file.
	 **/
    [System.Serializable]
    public class PassiveData
    {
        public string ScriptName;
        public string Name;
        public int[] Damages;
        public string[] DamagesType;
        public string[] OtherValues;
        public int NumberOfStacks;
        public string[] Description;
    }
    #endregion
}

