using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StarMaker : MonoBehaviour{
    // GameObject of the star
    [SerializeField] public GameObject star;

    // GameObject List of all the stars
    [SerializeField] public static GameObject[] gameStarList;

    // List of all the stars
    private List<OnStar> allStarList;

    // Setting the stars routes (for the scene)
    [SerializeField] public GameObject starRoutings;

    // List of the stars names
    [SerializeField] public string[] nameS;
    
    // Line renderer of the actual routes in the scene
    private LineRenderer starLines;

    // Used for colliders (gizmo testing)
    public LayerMask starlayer;

    
    // Class that sets up the routes
    public void SetStarRoutes(Transform[] locations) {
        // For all the locations (of stars = all the stars)
        for (int x = 0; x < locations.Length; x++) {
            // Adding in a ovelap sphere to see which stars touch each other in a set radius
            Collider[] starsInRadius = Physics.OverlapSphere(locations[x].position, 20.0f, starlayer);
            // For all the stars that collide
            foreach (Collider col in starsInRadius){
                if (col.gameObject.transform.position != locations[x].position) {
                    // Add a route between the stars
                    GameObject actualRoutes = Instantiate(starRoutings, locations[x].position, Quaternion.identity);
                    starLines = actualRoutes.GetComponent<LineRenderer>();
                    starLines.SetPosition(0, locations[x].position);
                    starLines.SetPosition(1, col.gameObject.transform.position);
                    float Distance = Vector3.Distance(locations[x].position, col.gameObject.transform.position);
                    // Adding the routes to the Dictionary of star routes to use the Dijkstra pathfinding
                    col.gameObject.GetComponent<OnStar>().DicStarRoutes.Add(gameStarList[x].GetComponent<OnStar>(), Distance);
                }
                //if (c.gameObject == star) continue;
            }
        }
    }
    
    // Class which creates a new list of stars
    public void NewStar() {
        // Variable to set the positions of the stars in a list
        Transform[] starListTrans;
        
        // Setting variable names for stars and creating lists for them
        gameStarList = new GameObject[10]; 
        starListTrans = new Transform[gameStarList.Length]; 
        allStarList = new List<OnStar>();

        // For all the stars in the list
        for (var i = 0; i < 10; i++) {
            // Spawn them at a random position within a set range
            var starPoint = new Vector3(Random.Range(-21.0f, 21.0f), Random.Range(-21.0f, 21.0f), Random.Range(-21.0f, 21.0f));
            gameStarList[i] = Instantiate(star, starPoint, Quaternion.identity);
            // Add a tag to them so they can be located easily
            gameStarList[i].transform.tag = "Star";
            gameStarList[i].transform.name = i + " " + nameS[Random.Range(0, nameS.Length)];
            // Setting the postion of the star into a transform list
            starListTrans[i] = gameStarList[i].transform;
            // Adding the GameObject Star to the List of all the stars
            allStarList.Add(gameStarList[i].GetComponent<OnStar>());
        }
        DijkstraSimplified.SetGalaxyStarList(allStarList);
    
        SetStarRoutes(starListTrans);
        
    }

    // Start is called before the first frame update
    void Start(){
        // Get line renderer
        starLines = GetComponent<LineRenderer>();
        // Create stars
        NewStar();  
    }



    //void OnDrawGizmos() {
    //    foreach (GameObject star in starList){
    //        // Draw a yellow sphere at the transform's position
    //        Gizmos.color = Color.yellow;
    //        Gizmos.DrawWireSphere(star.transform.position, 20.0f);
    //    }
    //}



    //private Transform[] starRouteTrans;

    //// Update is called once per frame
    //void Update(){
    //   for (int i = 0; i <starRouteTrans.Length; i++) {
    //        //for (int i = 0; i <Random.Range(0, starRouteTrans.Length); i++) {
    //        starLines.SetPosition(i, starRouteTrans[i].position);       
    //   }
    //}

    
}
