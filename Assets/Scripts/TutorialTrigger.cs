using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class TutorialTrigger : MonoBehaviour
{
    public Character player;
    public bool EndConv = false;
    public  NPCConversation tutorialCon;
    public SceneLoader sceneLoader;
    public string SceneName = "Level1";

    void OnTriggerEnter(Collider col){
	    player.canMove = false;
        if(player.animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            player.animator.SetTrigger("land");
        player.animator.SetTrigger("move");
        player.animator.SetFloat("speed", 0);
        player.movementSM.ChangeState(player.standing);
        if (tutorialCon) ConversationManager.Instance.StartConversation(tutorialCon);
        else OnConCompleted();

        ConversationManager.OnConversationEnded += OnConCompleted;
    }

    private void OnDestroy()
    {
        ConversationManager.OnConversationEnded -= OnConCompleted;
    }

    void OnConCompleted()
    {
        if (!EndConv)
        {
            player.canMove = true;
        }
        else
        {
            sceneLoader.LoadScene(SceneName);
        }
    }

}
