/*
Created By OFGONEN
*/
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour {

    #region Variables
    public GameObject player;
    public bool startingGate;
    #endregion

    private void Start()
    {
        // Eger Baslangic Gate ' i isek Gate ' in  basladigi yerde player ' i spawnliyoruz.
        if (startingGate)
        {
            player.transform.position = transform.position;
        }
    }

    #region Methods

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //TODO Buraya bir bolum sonu animasyonu ekleyip , bir sonraki level ' i yukleme olayini o animasyon bitimine koyalim.
            Loader.instance.LoadNextLevel();
        }
    }
    #endregion
}
