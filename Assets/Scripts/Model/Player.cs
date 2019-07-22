using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Player
{
    //One to many model that one player can have many characters, but each character only has one player.
    public string Name;
    public int OID;
    public List<Character> Characters;
}
