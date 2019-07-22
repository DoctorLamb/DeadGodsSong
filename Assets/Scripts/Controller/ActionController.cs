using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    public GameController gm_controller;

    public void Action(int action_id = 0)
    {
        switch (action_id) {
            case 0:
                //Attack
                ATTK();
                break;
            case 1:
                //Heal
                break;
            case 2:
                //Debuff
                break;
            case 3:
                //Buff
                break;
        }
    }

    private void ATTK() {
        Character active = gm_controller.activeCharacter;
        List<int> enemy_Ids = GetCharacters(active.playerID, active.OID);
        List<CharacterController2D> enemys_InRange = GetCharactersInRange(enemy_Ids);
        foreach (CharacterController2D c in enemys_InRange) {
            Debug.Log("Found character: "+c.characterOID);
        }
    }

    private List<int> GetCharacters(int playerId, int characterId, bool different = true) {
        //Get all characters that are either of the same player Id or of a different Id based on the different bool
        //Debug.Log("In GetCharacters");
        List<int> temp = new List<int>();
        foreach (Character c in gm_controller.ReturnCharacters()) {
            if (c.playerID != playerId && different)
            {
                temp.Add(c.OID);
            }
            else if (c.playerID == playerId && !different) {
                temp.Add(c.OID);
            }
        }
        //Debug.Log("Characters: " + temp.Count);
        return temp;
    }

    private List<CharacterController2D> GetCharactersInRange(List<int> allCharacters) {
        //Debug.Log("In GetCharactersInRange");
        //Return all the characters that are in range of the character taking the action
        GameObject active = gm_controller.activeCharacterController.gameObject;
        List<CharacterController2D> temp = new List<CharacterController2D>();
        foreach (CharacterController2D c in gm_controller.CharacterControllers) {
            if (allCharacters.Contains(c.characterOID)) {
                //Raycast between the new controller and the active controller
                if (InSight(active, c.gameObject)) {
                    temp.Add(c);
                }
            }
        }
        //Debug.Log("Characters in range: " + temp.Count);
        return temp;
    }

    private bool InSight(GameObject a, GameObject b) {
        Vector3 fix = a.transform.position + new Vector3(a.transform.right.x, a.transform.localScale.y, 0);
        Debug.DrawLine(fix, b.transform.position, Color.blue, 5f, false);
        RaycastHit2D hit = Physics2D.Raycast(fix, b.transform.position - a.transform.position);
        Debug.DrawLine(fix, hit.point, Color.red, 5f);
        Debug.Log(hit.collider.gameObject.name);
        if (hit.collider.gameObject == b)
        {
            //Debug.Log("Enemy in sight");
            return true;
        }
        else {
            return false;
        }
    }
}
