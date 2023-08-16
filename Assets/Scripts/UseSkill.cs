using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;


public class UseSkill : MonoBehaviour
{   
    public GameObject[] elements;
    public TMP_Text textSps;
    public TMP_Text textSuccess;
    public float fadeDuration = 2.0f; // The time that text fade away
    public AudioSource audioSource;
    public AudioSource failAudio;

    private List<char> skillSlot = new List<char>();
    private string[] skillName;
    private float startTime;
    private Transform myTransform;
    private int successSkill;
    private Color originalColor;
    private Color targetColor;
    private float elapsedTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        successSkill = 0;
        startTime = Time.time;
        skillName = new string[] {"eee", "qee", "qqe", "qqq", "qqw", "qwe", "qww", "wee", "wwe", "www"};
        myTransform = GetComponent<Transform>();
        originalColor = textSuccess.color;
        targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0.0f); // 目标透明度为0
        audioSource.time = 0.4f;
    }
    // Update is called once per frame
    void Update()
    {
        textSuccess.color = Color.Lerp(originalColor, targetColor, elapsedTime / fadeDuration);

        if (Input.GetKeyDown(KeyCode.Q)) {
            skillSlot.Add('q');
            if (skillSlot.Count <= 3)
            {
                generateElement();
            }
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            skillSlot.Add('w');
            if (skillSlot.Count <= 3)
            {
                generateElement();
            }
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            skillSlot.Add('e');
            if (skillSlot.Count <= 3)
            {
                generateElement();
            }
        }
        if (skillSlot.Count > 3)
        {
            skillSlot.RemoveAt(0);
            generateElement();
        }
        if (skillSlot.Count == 3 && Input.GetKeyDown(KeyCode.R)) {
            bool success = UseSpell();
            if (success)
            {
                successSkill+= 1;
                elapsedTime = 0.0f;
                textSuccess.text = "Correct Spell!";
                UpdateSpm();
                PlaySoundSuccess();
            }
            else
            {
                elapsedTime = 0.0f;
                textSuccess.text = "Wrong Spell!";
                PlaySoundFail();
            }
        }
        if (elapsedTime < 2.0f)
        {
            elapsedTime += Time.deltaTime;
        }

    }
    // If the current element slot matches the skill, use the skill
    bool UseSpell() {
        for (int i = 0; i < skillName.Length; i++)
        {
            GameObject skill = GameObject.Find(skillName[i]);
            if (skill != null)
            {
                char[] skillArray = skill.name.ToCharArray();
                var groupedSkill = skillArray.GroupBy(x => x);
                var groupedElement = skillSlot.GroupBy(x => x);
                Dictionary<char, int> testDict = new Dictionary<char, int>();

                foreach (var group in groupedSkill)
                {
                    char element = group.Key;
                    int count = group.Count();
                    testDict[element] = count;
                }
                foreach (var group in groupedElement)
                {
                    char element = group.Key;
                    int count = group.Count();
                    if (!testDict.ContainsKey(element) || testDict[element]!= count) return false;
                }
                Destroy(skill);
                return true;
            }
        }
        return false;
    }

    void generateElement()
    {
        GameObject[] existElements = GameObject.FindGameObjectsWithTag("Element");
        for (int i = 0; i < existElements.Length; i++)
        {
            Destroy(existElements[i]);
        }
        for (int i = 0; i < skillSlot.Count; i++)
        {
            Vector3 vec = new Vector3(myTransform.position.x - 1 + i*1, myTransform.position.y - 1, myTransform.position.z);
            GameObject elementObject = null;
            switch (skillSlot[i])
            {   
                case 'q':
                    elementObject = elements[0];
                    break;
                case 'w':
                    elementObject = elements[1];
                    break;
                case 'e':
                    elementObject = elements[2];
                    break;
                default:
                    break;
            }

            GameObject temp = Instantiate(elementObject, vec, Quaternion.identity);
        }
    }

    void UpdateSpm()
    {
        float elapsedTime = Time.time - startTime;
        float Sps = successSkill / elapsedTime;
        textSps.text = "Success Skill per second:" + Sps.ToString("F2");
    }

    private void PlaySoundSuccess()
    {
        if (audioSource != null)
        {
            audioSource.Play(); // 播放音效
        }
    }

    private void PlaySoundFail()
    {
        if (failAudio != null)
        {
            failAudio.Play(); // 播放音效
        }
    }
}
