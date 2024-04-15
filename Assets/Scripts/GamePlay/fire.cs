using UnityEngine;

namespace GamePlay
{
    public class fire : Singleton<fire>
    {
        public GameObject gm;
      

        private void Start()
        {
            Destroy(gameObject, 0.3f);
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            var component = collision.gameObject.GetComponent<Soldierbm>();
            if (collision.gameObject.tag == "Zombie")
            {
                collision.gameObject.GetComponent<Zombie>().DieZombie();
                //_mapManagerbm.zombieCount--;
                //Debug.Log("Key: " + _mapManagerbm.zombieCount);
                //Destroy(collision.gameObject);
            }
            else if (collision.gameObject.tag != "Zombie" && collision.gameObject.tag != "Player")
            {
                if (collision.gameObject.GetComponent<Box>() != null)
                {
                    GetComponent<CircleCollider2D>().enabled = false;
                    collision.gameObject.GetComponent<Box>().SpawnPowerUp();
                }
                else
                {
                    if (collision.gameObject.GetComponent<fire>() != null) return;
                    if (collision.gameObject.GetComponent<Bombbm>() != null)
                        collision.gameObject.GetComponent<Bombbm>().Explode();
                }

                Destroy(collision.gameObject);
            }
            else
            {
                if (!(collision.gameObject.tag == "Player")) return;
                if (component.armor)
                {
                    component.armor = false;
                    return;
                }

                component.heart--;
                gm = collision.gameObject;
                if (gm != null) gm.GetComponent<PolygonCollider2D>().enabled = false;
                Singleton<BoomSpawnerbm>.Instance.numberOfBombs = 1;
                Singleton<BoomSpawnerbm>.Instance.firePower = 2;
                Singleton<Soldierbm>.Instance.speedValue = 1f;
                Singleton<Movebm>.Instance.newspeed = 2f;
            }
        }
    }
}