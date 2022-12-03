using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [SerializeField] GameObject AxisSphereContr;
    public GameObject chosenObject = null;
    public GameObject controlerAxis = null;

    [SerializeField] Slider resolutionSlider;
    [SerializeField] Slider resolutionSliderY;
    [SerializeField] MyMesh mesh;
    [SerializeField] GameObject meshObj;

    int resolutionVal = 2;
    int resolutionValY = 2;


    private Vector3 mOffset;

    private float mZCoord;

    Vector3 mousePoint;

    bool ableToChangeAxis = true;


    // Start is called before the first frame update
    void Start()
    {
        meshObj = GameObject.Find("MyMesh");
        //meshObj.transform.GetChild(0).gameObject.SetActive(false);
        AxisSphereContr = GameObject.Find("AxisControler");
        StartCoroutine(MyCorout());


    }

    // Update is called once per frame
    void Update()
    {
        SliderChangeResolution();
        SliderChangeResolutionY();
        Mouse();
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Debug.Log("CONTROL");
            meshObj.transform.GetChild(0).gameObject.SetActive(true);
            AxisSphereContr.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            meshObj.transform.GetChild(0).gameObject.SetActive(false);
            AxisSphereContr.SetActive(false);
            controlerAxis = null;
        }
        if ((!(Input.GetKey(KeyCode.LeftAlt))))
        {
            Axises();
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

    //private void OnMouseDown()
    //{
    //    mZCoord = Camera.main.WorldToScreenPoint(choosenObjeck.transform.position).z;

    //    mOffset = choosenObjeck.transform.position - getMouseWorldPos();
    //}

    private Vector3 getMouseWorldPos()
    {
        mZCoord = Camera.main.WorldToScreenPoint(chosenObject.transform.position).z;

        mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    //private void OnMouseDrag()
    //{
    //    choosenObjeck.transform.position = getMouseWorldPos() + mOffset;
    //}

    void SliderChangeResolution()
    {
        if (resolutionSlider.value != resolutionVal)
        {
            mesh.m = (int)resolutionSlider.value;
            //mesh.n = (int)resolutionSlider.value;

            mesh.ChangeResolution(mesh.m, mesh.n);
            resolutionVal = (int)resolutionSlider.value;
        }
    }

    void SliderChangeResolutionY()
    {
        if (resolutionSliderY.value != resolutionValY)
        {
            //mesh.m = (int)resolutionSlider.value;
            mesh.n = (int)resolutionSliderY.value;

            mesh.ChangeResolution(mesh.m, mesh.n);
            resolutionValY = (int)resolutionSliderY.value;
        }
    }

    private void Mouse()
    {
        AssignAxisObject();

        if (Input.GetMouseButtonDown(0) && (!(Input.GetKey(KeyCode.LeftAlt))))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Sphere")
                {
                    chosenObject = hit.collider.gameObject;

                    AxisSphereContr.transform.position = hit.collider.gameObject.transform.position;
                    
                    
                }
            }
            SphereController( hit, ray);
        }
    }

    void SphereController(RaycastHit hit, Ray ray) 
    {
        if ((!(Input.GetKey(KeyCode.LeftAlt))))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "SphereControllerX" && chosenObject != null)
                {
                    chosenObject.transform.position += new Vector3((float)(Input.mouseScrollDelta.x), 0, 0);// (float)(Input.mouseScrollDelta.x);
                    Debug.Log("Controller");
                }
            }
            else if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "SphereControllerY")
                {
                    //choosenObjeck = hit.collider.gameObject;

                    AxisSphereContr.transform.position = hit.collider.gameObject.transform.position;


                }
            }
            else if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "SphereControllerZ")
                {
                    chosenObject = hit.collider.gameObject;

                    AxisSphereContr.transform.position = hit.collider.gameObject.transform.position;


                }
            }
        }

    }

    public void AssignAxisObject()
    {
        //Debug.Log(GameObject.Find("MyMesh").transform.GetChild(0).name);

        //AxisSphereContr = GameObject.Find("MyMesh").transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
    }

    void Axises()
    {
        

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (Physics.Raycast(ray, out hit) && (ableToChangeAxis))
                {
                    if (hit.collider.gameObject.tag == "SphereControllerX" && chosenObject != null)
                    {
                        controlerAxis = hit.collider.gameObject.gameObject;
                    }
                    else if (hit.collider.gameObject.tag == "SphereControllerY" && chosenObject != null)
                    {
                        controlerAxis = hit.collider.gameObject.gameObject;
                    }
                    else if (hit.collider.gameObject.tag == "SphereControllerZ" && chosenObject != null)
                    {
                        controlerAxis = hit.collider.gameObject.gameObject;
                    }
                    else
                    {
                        controlerAxis = null;
                    }
                    ableToChangeAxis = false;
                }
            }
            //Debug.Log((float)(Input.mouseScrollDelta.x));
            //choosenObjeck.transform.position += new Vector3((float)(Input.GetAxis("Mouse X")), 0, 0);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            ableToChangeAxis = true;

        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if(controlerAxis != null)
            {

            
                if (controlerAxis.tag == "SphereControllerX")
                {
                    chosenObject.transform.position = new Vector3(getMouseWorldPos().x, chosenObject.transform.position.y, chosenObject.transform.position.z);
                    AxisSphereContr.transform.position = chosenObject.transform.position;
                }
                else if (controlerAxis.tag == "SphereControllerY")
                {
                    chosenObject.transform.position = new Vector3(chosenObject.transform.position.x, getMouseWorldPos().y, chosenObject.transform.position.z);
                    AxisSphereContr.transform.position = chosenObject.transform.position;
                }
                else if (controlerAxis.tag == "SphereControllerZ")
                {
                    chosenObject.transform.position = new Vector3(chosenObject.transform.position.x, chosenObject.transform.position.y, getMouseWorldPos().z);
                    AxisSphereContr.transform.position = chosenObject.transform.position;
                }
            }
        }
    }

    IEnumerator MyCorout()
    {
        yield return new WaitForSeconds(0.5f);
        meshObj.transform.GetChild(0).gameObject.SetActive(false);
        AxisSphereContr.SetActive(false);

    }
}
