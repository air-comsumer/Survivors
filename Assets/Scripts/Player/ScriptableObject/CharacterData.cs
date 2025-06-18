using UnityEngine;
[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData", order = 1)]
public class CharacterData : ScriptableObject
{
    [SerializeField] Sprite sprite;
    [SerializeField] RuntimeAnimatorController controller;
    [SerializeField] CharacterType characterType;
    [SerializeField] int healthPosint;
    [SerializeField] int attackPower;
    [SerializeField] int defencePower;
    [SerializeField] int speed;

    public int GetHealthPoint()
    {
        return healthPosint;
    }
    public int GetAttackPower()
    {
        return attackPower;
    }
    public int GetDefencePower()
    {
        return defencePower;
    }
    public int GetSpeed()
    {
        return speed;
    }
    public CharacterType GetCharacterType()
    {
        return characterType;
    }
    public RuntimeAnimatorController GetController()
    {
        return controller;
    }
    public Sprite GetSprite()
    {
        return sprite;
    }
}
