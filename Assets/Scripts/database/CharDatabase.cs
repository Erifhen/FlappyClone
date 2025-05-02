using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharDatabase", menuName = "Gacha/CharDatabase")]
public class CharDatabase : ScriptableObject
{
    public List<CharData> characters;
}
