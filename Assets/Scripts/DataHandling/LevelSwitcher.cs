using UnityEngine;

public class LevelSwitcher : MonoBehaviour
{
    [SerializeField] public string newLevelName;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LevelObjectsCreator levelGen = GameObject.FindObjectOfType<LevelObjectsCreator>();

            if (levelGen == null)
            {
                return;
            }

            //levelGen.fileAreaName = newLevelName;

            levelGen.LoadMultiLayeredLevelsFromFiles();
        }
    }
}
