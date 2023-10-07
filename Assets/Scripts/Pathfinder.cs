using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.ProBuilder.Shapes;

public class Pathfinder : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI startingStar;
    [SerializeField] private TextMeshProUGUI endingStar;
    public GameObject starterStar;
    public GameObject enderStar;

    [SerializeField] private Material mat;
    private Vector3 scaleChange;

    // List of all the stars that the Dijikstra passes through to get to the end star
    public List<OnStar> starsTravelled;

    // Set scaleChange to increase my stars size 0.3x if called
    private void Start(){
        scaleChange = new Vector3(0.2f, 0.2f, 0.2f);
    }

    private void Update(){
        // If left arrow is pressed on a star, set it as the start star
        if (Input.GetKeyDown(KeyCode.LeftArrow)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)){
                starterStar = hit.transform.gameObject;
                startingStar.text = "Start Star : " + hit.transform.name;
            }
        }
        // If right arrow is pressed on a star, set it as the end star
        if (Input.GetKeyDown(KeyCode.RightArrow)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                enderStar = hit.transform.gameObject;
                endingStar.text = "End Star : " + hit.transform.name;
            }
        }
        // Use escape to close the game
        if (Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
    }
    
    // Use Dijikstra pathfinding to find the optimal path from the start to the end star
    public void Pathfind(){
        if (starterStar != null && enderStar != null){
            starsTravelled = DijkstraSimplified.FindPath(starterStar.GetComponent<OnStar>(), enderStar.GetComponent<OnStar>());
            Debug.Log(starsTravelled.Count);
            // For each star passed through, change its colour and size
            foreach (OnStar star in starsTravelled){
                star.GetComponent<MeshRenderer>().material = mat;
                star.transform.localScale += scaleChange;
            }
        }
    }
}