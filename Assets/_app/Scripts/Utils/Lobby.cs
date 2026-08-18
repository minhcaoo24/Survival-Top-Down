using UnityEngine;
using UnityEngine.SceneManagement;

namespace STD.Utils
{
    public class Lobby : MonoBehaviour
    {
        public void NextScene()
        {
            SceneManager.LoadScene(1);
        }
    }
}