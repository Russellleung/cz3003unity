using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class Challengeview : MonoBehaviour {

    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> ChallengeEntryTransformList;

    private void Awake() {
        entryContainer = transform.Find("ChallengeEntryContainer");
        entryTemplate = entryContainer.Find("ChallengeEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("challengesTable2");
        Challenges Challenges = JsonUtility.FromJson<Challenges>(jsonString);

        if (Challenges == null) {
            // There's no stored table, initialize
            Debug.Log("Initializing table with default values...");
             AddChallengeEntry("CMK");
             AddChallengeEntry("CM");
             AddChallengeEntry("C");
             AddChallengeEntry("CMK");
             AddChallengeEntry("CM");
             AddChallengeEntry("C");
            
            // // Reload
             jsonString = PlayerPrefs.GetString("challengesTable2");
             Challenges = JsonUtility.FromJson<Challenges>(jsonString);
        }

        // Sort entry list by Score
        

        ChallengeEntryTransformList = new List<Transform>();
        //ChallengeEntry ChallengeEntry = new ChallengeEntry{name = "boi"};
        //CreateChallengeEntryTransform(ChallengeEntry, entryContainer, ChallengeEntryTransformList);


        foreach (ChallengeEntry ChallengeEntry in Challenges.ChallengeEntryList) {
            CreateChallengeEntryTransform(ChallengeEntry, entryContainer, ChallengeEntryTransformList);
        }
    }

    private void CreateChallengeEntryTransform(ChallengeEntry ChallengeEntry, Transform container, List<Transform> transformList) {
        float templateHeight = 93f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        
        entryTransform.Find("TextChallengeButton").GetComponent<Text>().text = ChallengeEntry.name;

        // Set background visible odds and evens, easier to read
       
        
        // Highlight First
       

        transformList.Add(entryTransform);
    }

    private void AddChallengeEntry(string name) {
        // Create ChallengeEntry
        ChallengeEntry ChallengeEntry = new ChallengeEntry { name = name };
        
        // Load saved Challenges
        string jsonString = PlayerPrefs.GetString("challengesTable2");
        Challenges Challenges = JsonUtility.FromJson<Challenges>(jsonString);

        if (Challenges == null) {
            // There's no stored table, initialize
            Challenges = new Challenges() {
                ChallengeEntryList = new List<ChallengeEntry>()
            };
        }

        // Add new entry to Challenges
        Challenges.ChallengeEntryList.Add(ChallengeEntry);

        // Save updated Challenges
        string json = JsonUtility.ToJson(Challenges);
        PlayerPrefs.SetString("challengesTable2", json);
        PlayerPrefs.Save();
    }

    private class Challenges {
        public List<ChallengeEntry> ChallengeEntryList;
    }

    /*
     * Represents a single High score entry
     * */
    [System.Serializable] 
    private class ChallengeEntry {
        
        public string name;
    }

}


