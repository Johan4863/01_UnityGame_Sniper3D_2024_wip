using UnityEngine;

public class ToggleNPCNamesVisibility : MonoBehaviour
{
    public GameObject[] npcNames; // Array to hold the NPC name objects

    void Start()
    {
        // Set NPC names invisible by default
        SetNPCNamesVisibility(false);
    }

    void Update()
    {
        // Toggle NPC names visibility when the "V" key is pressed
        if (Input.GetKeyDown(KeyCode.V))
        {
            ToggleNPCNames();
        }
    }

    void ToggleNPCNames()
    {
        // Toggle visibility of NPC names
        bool currentVisibility = npcNames[0].activeSelf; // Assuming all NPC names have the same visibility
        SetNPCNamesVisibility(!currentVisibility);
    }

    void SetNPCNamesVisibility(bool isVisible)
    {
        // Set visibility of NPC names
        foreach (GameObject npcName in npcNames)
        {
            npcName.SetActive(isVisible);
        }
    }
}
