using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class CutSceneManager : MonoBehaviour
{
    public Character player;
    public  NPCConversation tutorialCon;
    public bool isEnd;
    public SceneLoader sceneLoader;

    // Start is called before the first frame update
    void Awake()
    {
    	player.canMove = false;
	
    }

    void Start(){
	ConversationManager.Instance.StartConversation(tutorialCon);
	ConversationManager.OnConversationEnded += OnConCompleted;
    }
    private void OnDestroy()
    {
        ConversationManager.OnConversationEnded -= OnConCompleted;
    }

    void OnConCompleted()
    {
        if (!isEnd)
        {
            player.canMove = true;
        }
        else
        {
            sceneLoader.LoadScene("Menu");
        }
    }

    public void Skip(){
	sceneLoader.LoadScene("Level1");
    }

}
