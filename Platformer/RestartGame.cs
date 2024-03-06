namespace Platformer;

public class RestartGame
{
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour {

    public void restartGame() 
    {
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }
//
}

}