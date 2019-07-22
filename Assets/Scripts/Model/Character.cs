using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Character
{
    //One to many model that one player can have many characters, but each character only has one player.
    public string name;
    public int playerID;
    public int OID;
    public int speed;
    public int health;
    public int damage;
    public int initiative;

    /// <summary>
    /// All options constructor for creating a new Character data model
    /// </summary>
    /// <param name="_playerID"> The OID of the player that the character is attached to</param>
    /// <param name="_charID"> The OID of the character</param>
    public Character(int _playerID, int _charID)
    {
        playerID = _playerID;
        OID = _charID;
        speed = 3;
        health = 3;
        damage = 1;
        initiative = 5;
    }

    /// <summary>
    /// All options constructor for creating a new Character data model
    /// </summary>
    /// <param name="_playerID"> The OID of the player that the character is attached to</param>
    /// <param name="_charID"> The OID of the character</param>
    /// <param name="_spd"> The desired speed value for the character, default to 3</param>
    /// <param name="_hp"> The desired health value for the character, defualt to 3</param>
    /// <param name="_dmg"> The desired damage that the character will deal, default to 1</param>
    /// <param name="_init"> The desired initiative that the character will have, default to 5</param>
    public Character(int _playerID, int _charID, int _spd = 3, int _hp = 3, int _dmg = 1, int _init = 5) {
        playerID = _playerID;
        OID = _charID;
        speed = _spd;
        health = _hp;
        damage = _dmg;
        initiative = _init;
    }

    public void modifyHealth(int _val) {
        health += _val;
    }

    public void modifySpeed(int _val){
        speed += _val;
    }

    public void modifyDamage(int _val)
    {
        damage += _val;
    }

    public void modifyInitiative(int _val)
    {
        initiative += _val;
    }
}
