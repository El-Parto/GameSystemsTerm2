using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonPrefab;

    [SerializeField]
    private Transform buttonPanel;

    private Dialogue currentDialogue;

    private GameObject dialoguePanel;

    public static DialogueManager theManager;
    [SerializeField] Text responseText;


    private void Awake()
    {
        dialoguePanel = transform.Find("Scroll View").gameObject;
        dialoguePanel.SetActive(false);
        if (theManager == null)
        {
            theManager = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void LoadDialogue(Dialogue dialogue)
    {
        dialoguePanel.SetActive(true);
        CleanUpButtons();
        currentDialogue = dialogue;
        DisplayResponse(dialogue.greeting);
        Button spawnedButton;
        //spawn a button for each dialogue option inside dialogue
        int i = 0;
        foreach (LineOfDialogue item in dialogue.dialogueOptions)
        {
            float? currentApproval = FactionManager.instance.GetFactionsApproval(dialogue.faction);
            if (currentApproval != null && currentApproval > item.minApproval)
            {
                //if not working, this was not in the if statement
                spawnedButton = Instantiate(buttonPrefab, buttonPanel).GetComponent<Button>();
                spawnedButton.GetComponentInChildren<TextMeshProUGUI>().text = item.topic;
                int i2 = i;
                spawnedButton.onClick.AddListener(delegate { ButtonClicked(i2); });
                i++;
            }
        }
        spawnedButton = Instantiate(buttonPrefab, buttonPanel).GetComponent<Button>();
        spawnedButton.GetComponentInChildren<TextMeshProUGUI>().text = dialogue.goodbye.topic;
        spawnedButton.onClick.AddListener(EndConversation);
    }
    
    void EndConversation()
    {
        
        DisplayResponse(currentDialogue.goodbye.response);
        CleanUpButtons();
        dialoguePanel.SetActive(false);

        if(currentDialogue.goodbye.nextDialogue != null)
        {
            //currentDialogue = currentDialogue.goodbye.nextDialogue;

            LoadDialogue(currentDialogue.goodbye.nextDialogue);
        }
    }

    void ButtonClicked(int _dialogueNum)
    {
        DisplayResponse(currentDialogue.dialogueOptions[_dialogueNum].response);
    }

    private void DisplayResponse(string _response)
    {
        responseText.text = _response;
    }

    

    void CleanUpButtons()
    {
        foreach (Transform child in buttonPanel)
        {
            Destroy(child.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
