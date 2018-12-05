using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTile : MonoBehaviour {
    
    [HideInInspector]
    public Material myMat;
    private Renderer rend;

    private Collider floorCollider;
    private List<GameObject> invisWalls = new List<GameObject>();

    public Material floorMat;
    public Material highlightedMat;
    public Material invisibleMat;

    private void Awake()
    {
        DeleteFloor();
    }

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        myMat = rend.material;

        //Colliders
        floorCollider = GetComponent<Collider>();
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i);
        }
	}

    private void ChangeMaterial(Material mat)
    {
        myMat = mat;
        rend.material = mat;
    }

    public void PlaceFloor()
    {
        //Change Material
        ChangeMaterial(floorMat);
        //Turn on Floor Collider
        floorCollider.enabled = true;
        //Turn off Wall Collider
        for (int i = 0; i < invisWalls.Count; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        //PlaySound

        //PlayParticles
    }

    public void HighlightFloor()
    {
        //Highlights floor
        ChangeMaterial(highlightedMat);

        //Play Sound
    }

    public void DeleteFloor()
    {
        //Turns off floor visual
        ChangeMaterial(invisibleMat);

        //Turn on Floor Collider
        floorCollider.enabled = false;
        //Turn off Wall Collider
        for (int i = 0; i < invisWalls.Count; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        //PlaySound
    }
}
