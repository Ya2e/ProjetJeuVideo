﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** GUISpellDisplayer class
 * This script should be associated to GUI Slots that represents Spells on the screen.
 * This script is used to display images, CD, descriptions and every informations we have about spells.
 **/
public class GUISpellDisplayer : MonoBehaviour
{
    /** Fields contains a Spell, associated to the Displayer and two images.
	 * The _CDSpellImage is a rotative image filler that is filled by the CD value of the spell.
	 * The _spellAvailableForGUI is a simple image that should be activated when the spell is not activable at any conditions (stuns for example)
	 **/
    private Spell _spell;
    [SerializeField] private Image _spellImgDescription;
    [SerializeField] private Image _CDSpellImage;
    [SerializeField] private Image _spellAvailableForGUI;
    [SerializeField] private Text _timerCDText;
    [SerializeField] private Image _stackImage;
    [SerializeField] private Image _backgroundStack;

    private Text _stackText;
    private Text _spellTextDescription;
    private int _numberOfStacks;

    /** Start private void Method
	 * The start method de-activate the _spellAvailableForGUI component by default.
	 * Basically, the spell Image is only CD dependant.
	 **/
    private void Start()
    {
        _spellAvailableForGUI.enabled = false;
        _spellTextDescription = _spellImgDescription.GetComponentInChildren<Text>();
        _spellImgDescription.enabled = false;
        _spellTextDescription.enabled = false;
        _timerCDText.enabled = false;
        _stackText = _stackImage.GetComponentInChildren<Text>();
        _stackText.enabled = false;
        _stackImage.enabled = false;
        _backgroundStack.enabled = false;
    }

    /** Update private void Method
	 * We ensure that _spellAvailableForGUI is always equal, to the AvailableForGUI() method in the Spell associated.
	 * We also ensure that the fillAmount of _CDSpellImage is always equal to the current CD of the spell associated. 
	 * Please note that the fillAmount must be [0;1] this is why we divide the currentCD by the CoolDownValue.
	 **/
    private void Update()
    {
        _spellAvailableForGUI.enabled = !_spell.AvailableForGUI();
        if (_spell.IsUnderGCD)
        {
            _CDSpellImage.fillAmount = _spell.CurrentCD / _spell.SpellGCD;
        }
        else
            _CDSpellImage.fillAmount = _spell.CurrentCD / _spell.CoolDownValue;

        _timerCDText.text = ((int)_spell.CurrentCD + 1).ToString();
        _timerCDText.enabled = _CDSpellImage.fillAmount != 0 || _spell.CoolDownValue < _spell.SpellGCD;
        if (_spell is StackableSpell)
        {
            StackableSpell spell = (StackableSpell)_spell;
            _stackText.text = spell.CurrentNumberOfStacks.ToString();
            _stackImage.enabled = spell.CurrentNumberOfStacks > 0;
            _stackImage.fillAmount = spell.CurrentStackCD / spell.StackCD;
        }
    }

    /** AttributeSpellToGUI public void Method
	 * @Params : Spell
	 * This public method should only be called by the Character script so far.
	 * In this method, we attribute to the Gui the correct spell.
	 * Then, we try to locate a Sprite associated to the spell in the Image Folder associated to the Character.
	 **/
    public void AttributeSpellToGUI(Spell spell)
    {
        _spell = spell;
        Image spellImage = GetComponent<Image>();
        spellImage.sprite = Resources.Load<Sprite>("Images/Spells/" + _spell.GetComponent<Character>().GetType().ToString() + "/" + _spell.GetType());
        if (spellImage.sprite == null)
        {
            spellImage.sprite = Resources.Load<Sprite>("Images/Spells/DefaultSpell");
        }

        if (spell is StackableSpell)
        {
            _stackText.enabled = true;
            _stackImage.enabled = true;
            _backgroundStack.enabled = true;
        }
    }

    /** MouseEnter, public void Method
	 * This Method is launched with an event trigger when the mouse enters the spell icon on the screen
	 **/
    public void MouseEnter()
    {
        GUIDescriptionDisplayer.DisplayDescriptionOnScreen(_spellTextDescription, _spellImgDescription, _spell.GetDescriptionGUI());
    }

    /** MouseExit, public void Method
	 * This Method is launched with an event trigger when the mouse exits the spell icon on the screen
	 **/
    public void MouseExit()
    {
        GUIDescriptionDisplayer.CancelDescriptionOnScreen(_spellTextDescription, _spellImgDescription);
    }
}