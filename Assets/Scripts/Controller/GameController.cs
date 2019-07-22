using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour
{
    public List<Player> Players = new List<Player>();
    private List<Character> characters = new List<Character>();
    Queue<Character> characterQueue = new Queue<Character>();
    public List<CharacterController2D> CharacterControllers = new List<CharacterController2D>();

    int activeCharacterOID {
        get {
            return activeCharacter.OID;
        }
    }
    public Character activeCharacter;
    public CharacterController2D activeCharacterController;

    public PlayerController playerController;
    // -------------------------------------------------------------------------------------------------------

    void Start()
    {
        GetCharacters();
        EnqueueCharacters();
        NextTurn(true);
    }

    private void GetCharacters() {
        characters.Clear();
        foreach (Player p in Players) {
            foreach (Character c in p.Characters) {
                characters.Add(c);
            }
        }
    }

    public Character[] ReturnCharacters() {
        GetCharacters();
        return characters.ToArray();
    }

    private void EnqueueCharacters()
    {
        //Create a temp list, find the character with the highest initiative and add them to the queue.
        //Remove the character after they are added
        //continue till temp list is empty.
        List<Character> characters_temp = characters;
        do
        {
            int max = 0;
            Character pointer = null;
            foreach (Character c in characters)
            {
                if (c.initiative > max)
                {
                    pointer = c;
                }
            }
            characterQueue.Enqueue(pointer);
            characters_temp.Remove(pointer);
        } while (characters_temp.Count > 0);
    }

    public void Next() {
        NextTurn();
    }

    private void NextTurn(bool first = false) {
        if(!first) characterQueue.Enqueue(activeCharacter);
        activeCharacter = characterQueue.Dequeue();
        activeCharacterController = CharacterControllers.FirstOrDefault(c => c.characterOID == activeCharacterOID);
        playerController.SetCharacter(activeCharacterController, activeCharacter);
    }
}
