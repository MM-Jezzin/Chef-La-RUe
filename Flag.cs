using UnityEngine;

public class Flag1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Go to next level
            SceneController.instance.NextLevel();
        }
    }
}
