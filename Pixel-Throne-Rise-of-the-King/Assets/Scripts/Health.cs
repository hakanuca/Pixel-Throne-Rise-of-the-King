using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    [SerializeField]
    private Image[] hearts;
    
    public Sprite fullHeart;
    public Sprite emptyHeart;
}
