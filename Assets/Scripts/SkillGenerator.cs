using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillGenerator : MonoBehaviour
{
    public GameObject[] skillArray;
    string[] skillName;
    Transform myTransform;
    // Start is called before the first frame update
    void Start()
    {
        skillName = new string[skillArray.Length];
        for (int i = 0; i < skillArray.Length; i++)
        {
            skillName[i] = skillArray[i].name;
        }
        myTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!SkillExist())
        {   
            Create();
        }
    }

    bool SkillExist()
    {
        for (int i = 0; i < skillName.Length; i++)
        {
            if (GameObject.Find(skillName[i]) != null)
            {
                return true;
            }
        }
        return false;
    }

    void Create()
    {
        int randomNumber = Random.Range(0, skillArray.Length);
        GameObject skillObject = Instantiate(skillArray[randomNumber], myTransform.position, Quaternion.identity);
        skillObject.name = skillName[randomNumber];
    }
}
