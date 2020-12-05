using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject player;
    public GameManager gameManager;

    public Text nameText;
    public GameObject spriteImg;
    public Text dialogueText;

    public GameObject display;

    private Queue<string> speakers;
    private Queue<Sprite> sprites;
    private Queue<string> sentences;

    public int lineNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        speakers = new Queue<string>();
        sprites = new Queue<Sprite>();
        sentences = new Queue<string>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            DisplayNextLine();
            lineNumber++;
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        speakers.Clear();
        sprites.Clear();
        sentences.Clear();
        lineNumber = 0;

        foreach (string name in dialogue.characterNames)
        {
            speakers.Enqueue(name);
        }

        foreach (Sprite image in dialogue.sprites)
        {
            sprites.Enqueue(image);
        }

        foreach (string line in dialogue.lines)
        {
            sentences.Enqueue(line);
        }

        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string speaker = speakers.Dequeue();
        Sprite sprite = sprites.Dequeue();
        string line = sentences.Dequeue();
        nameText.text = speaker;
        spriteImg.GetComponent<Image>().sprite = sprite;
        dialogueText.text = line;
        StopAllCoroutines();
        StartCoroutine(typeSentence(line));
    }

    IEnumerator typeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }


    void EndDialogue()
    {
        display.SetActive(false);
        if (player.GetComponent<LookAhead>().levelEnd)
        {
            if (gameManager.levelNum == 0 || gameManager.levelNum == 1 || gameManager.levelNum == 2)
            {
                gameManager.GoToChaseSequence();
            }
            
            if (gameManager.levelNum == 5)
            {
                gameManager.ToSuburbs();
            }

        }
    }
}

