using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestMarker : MonoBehaviour
{

    public Sprite icon;
    public Image image;
    public GameObject compassMarker;

    public Vector2 position
    {
        get { return new Vector2(transform.position.x, transform.position.z); }
    }

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<Compass>().AddQuestMarker(this);
    }

}
