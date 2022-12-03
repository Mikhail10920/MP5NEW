using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CylinderController : MonoBehaviour
{

    [SerializeField] Slider resolutionSlider;
    [SerializeField] Slider resolutionSliderUp;

    [SerializeField] Slider DegreeSlider;

    [SerializeField] Slider RadiusSlider;
    [SerializeField] Slider SquareHeightSlider;
    
    [SerializeField] CylinderMesh mesh;
    [SerializeField] GameObject meshObj;

    int resolutionVal = 2;
    int resolutionValUp = 2;

    int degreeVal = 60;

    int radiusVal = 1;

    int squareHeightVal = 1;


    


    // Start is called before the first frame update
    void Start()
    {
        meshObj = GameObject.Find("CylinderMesh");
        //meshObj.transform.GetChild(0).gameObject.SetActive(false);
        StartCoroutine(MyCorout());
        mesh.degrees = DegreeSlider.value;
        mesh.resolution = (int)resolutionSlider.value;
        mesh.upResolution = (int)resolutionSliderUp.value;
        mesh.radius = RadiusSlider.value;
        mesh.squareHeight = SquareHeightSlider.value;
        DegreeSlider.onValueChanged.AddListener(delegate {sliderChange();});
        RadiusSlider.onValueChanged.AddListener(delegate {sliderChange();});
        resolutionSlider.onValueChanged.AddListener(delegate {sliderChange();});
        resolutionSliderUp.onValueChanged.AddListener(delegate {sliderChange();});
        SquareHeightSlider.onValueChanged.AddListener(delegate {sliderChange();});



    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            
            meshObj.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            meshObj.transform.GetChild(0).gameObject.SetActive(false);
            
        }



        //if (Input.GetButtonDown("Fire1"))
        //{
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        if (hit.collider.gameObject.tag == "SphereControllerX" && choosenObjeck != null)
        //        {
        //            choosenObjeck.transform.position += new Vector3((float)(Input.mouseScrollDelta.x), 0, 0);
        //            Debug.Log("Controller");
        //        }
        //    }
        //}
    }
    public void sliderChange()
    {
        mesh.degrees = DegreeSlider.value;
        mesh.resolution = (int)resolutionSlider.value;
        mesh.upResolution = (int)resolutionSliderUp.value;
        mesh.radius = RadiusSlider.value;
        mesh.squareHeight = SquareHeightSlider.value;
        mesh.ChangeResolution();
    }

    //private void OnMouseDown()
    //{
    //    mZCoord = Camera.main.WorldToScreenPoint(choosenObjeck.transform.position).z;

    //    mOffset = choosenObjeck.transform.position - getMouseWorldPos();
    //}

     

    //private void OnMouseDrag()
    //{
    //    choosenObjeck.transform.position = getMouseWorldPos() + mOffset;
    //}

    

    

    

    

    IEnumerator MyCorout()
    {
        yield return new WaitForSeconds(0.5f);
        meshObj.transform.GetChild(0).gameObject.SetActive(false);

    }
}
