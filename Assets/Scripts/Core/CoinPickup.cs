using UnityEngine;
using System.Collections;

public class CoinPickup : MonoBehaviour {

    public int coinValue = 1;

	void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            GD.player.IncreaseCoins(coinValue);
            Destroy(gameObject);
        }
    }


}
