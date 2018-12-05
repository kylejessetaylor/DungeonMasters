using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : MonoBehaviour {

    ObjectPool objectPool;
    private GameObject player;
    //public enum FloorHeight { One, Two, Three, Four }
    //public FloorHeight floorHeight = FloorHeight.One;

    [HideInInspector]
    public Transform selectedTile;
    private GameObject buildDisplay;
    public float maxPlaceDistance = 100f;

    public enum BuildPlacement { None, Floor, Wall, FreePlace }
    public BuildPlacement buildPlacement = BuildPlacement.None;
    public enum Tiling { None, Three, Twelve }
    public Tiling tiling = Tiling.Twelve;


    void Awake()
    {
        objectPool = ObjectPool.Instance; 
    }

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        //SelectBlueprint(buildFloorDisplayPrefab);
        SpawnBlueprintDisplays();

    }

    // Update is called once per frame
    void Update () {
		if (buildPlacement == BuildPlacement.Floor)
        {
            FloorBuildDisplay();
        }
	}

    public void SpawnBlueprintDisplays()
    {
        foreach (ObjectPool.Blueprint blueprint in objectPool.blueprints)
        {
            //Instantiate 
            /// Change so that this adds to list of display objects for player's available blueprints
            /// (i.e. doesnt use 'buildDisplay')
            /// buildDisplay should only be assigned to based on the tag of selected in UI element of list of blueprints
            GameObject GO = Instantiate(blueprint.display);
            GO.SetActive(false);

            buildDisplay = GO;

        }
    }

    public void FloorBuildDisplay()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, maxPlaceDistance))
        {
            if (hit.collider.gameObject != player)
            {
                //Position
                float tileSnapping = 0f;
                #region Converting enum to float
                if (tiling == Tiling.Twelve)
                {
                    tileSnapping = 12f;
                }
                else if (tiling == Tiling.Three)
                {
                    tileSnapping = 3f;
                }
                else
                {
                    Debug.LogError("Tiling is set to None, yet user is trying to place a tile");
                }
                #endregion
                Vector3 snappedPosition = new Vector3(Mathf.Round(hit.point.x / tileSnapping) * tileSnapping,
                                                                 (hit.point.y),
                                                      Mathf.Round(hit.point.z / tileSnapping) * tileSnapping);
                buildDisplay.transform.position = snappedPosition;


                buildDisplay.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.collider.tag != buildDisplay.tag)
                    {
                        Build();
                    }
                    else
                    {
                        Debug.LogWarning("You can not build this object here.");
                    }
                }

            }
            //Hide buildDisplay
            else
            {
                buildDisplay.SetActive(false);
            }
        }
        //Hide buildDisplay
        else
        {
            buildDisplay.SetActive(false);
        }
    }

    public void Build()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ObjectPool.Instance.SpawnFromPool(buildDisplay.tag, buildDisplay.transform.position, buildDisplay.transform.rotation);
        }
    }

    public void Remove()
    {
        
    }


}
