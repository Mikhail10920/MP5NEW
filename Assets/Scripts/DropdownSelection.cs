using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownSelection : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject myMesh;
    public GameObject myCylinder;
    public Dropdown dropdown;
    void Start()
    {
        StartCoroutine(MyCorout());
        //myCylinder.SetActive(false);
        //dropdown.onValueChanged.AddListener(delegate {valueChanged(dropdown.value);});
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void valueChanged(int dropdownValue)
    {
        if(dropdownValue == 0)
        {
            myCylinder.SetActive(false);
            myMesh.SetActive(true);
        }
        else
        {
            myCylinder.SetActive(true);
            myMesh.SetActive(false);
        }
    }
    IEnumerator MyCorout()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("moo");
        myCylinder.SetActive(false);
        dropdown.onValueChanged.AddListener(delegate {valueChanged(dropdown.value);});
        
        
       

    }
}
