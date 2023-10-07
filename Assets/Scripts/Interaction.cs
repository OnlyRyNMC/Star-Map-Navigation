using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interaction : MonoBehaviour {
    private TextMeshProUGUI text;
    [SerializeField] private CanvasGroup TextPanel;

    // Panel that has the text elements on it
    void Start(){
        if (TextPanel != null){
            text = TextPanel.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    // Update is called once per frame
    void Update(){
        // If left click is pressed on a star
        if (Input.GetKeyDown(KeyCode.Mouse0)){ 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                // Reveal the panel and add display the stars name
                StartCoroutine(RevealPanel());
                text.text = "Name : " + hit.transform.name;
            }
        }
    }
    //Class to be put on button so i can reset my scene and show a new set of stars
    public void reset(string sceneManager){
        SceneManager.LoadScene(sceneManager);
    }

    // Using the easings script i have to reveal the panel
    private IEnumerator RevealPanel() {
        float time = 0;
        while (time <= 1) {
            TextPanel.alpha = Easings.Quadratic.In(time);
            time += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
