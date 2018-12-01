/*
Created By OFGONEN
*/
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour {

    #region Variables
    public static Counter instance;
    public Text _text_counter;

    public float _time_round; // Time for round

    public float _time_star_3; // Time pass for getting 3 stars
    public float _time_star_2; // Time pass for getting 2 stars
    public float _time_star_1; // Time pass for getting 1 stars
    public float _time_star_0; // Time pass for getting 0 stars

    public float _counter; // Counter for time

    private bool _can_counter; // Can counter or not. Cant count if game pauses or ends.

    #endregion

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start () 
	{
        _counter = _time_round;
        _text_counter.text = "" + (int)_counter;
        
    }
	
	void Update () 
	{
        // Counts every frame , convert float to integer and updates UI for counter.
        if (_can_counter)
        {
            _counter -= Time.deltaTime;
            _text_counter.text = "" + (int)_counter;
        }

        // If player consumes all the time , game ends.
        if (_counter <= 0.05f)
        {
            _can_counter = false;
            //Gamelogic EndGame;
        }
	}

    #region Methods

    // Pauses the counter.
    public void TooglePause(int pause)
    {
        if(pause == 1)
        {
            _can_counter = true;
        }
        else
        {
            _can_counter = false;
        }

    }
    #endregion
}
