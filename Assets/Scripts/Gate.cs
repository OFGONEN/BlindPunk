/*
Created By OFGONEN
*/
using UnityEngine;

public class Gate : MonoBehaviour {

    #region Variables
    public int level_to_Load;
	#endregion
	

    #region Methods

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Loader.instance.LoadLevel(level_to_Load);
        }
    }
    #endregion
}
