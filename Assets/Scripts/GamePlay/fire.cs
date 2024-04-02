using UnityEngine;

namespace XX
{
	public class fire : Singleton<fire>
	{
		private MapManager map;

		public GameObject gm;

		private void Start()
		{
			map = GameObject.Find("GameManager(Clone)").GetComponent<MapManager>();
			Object.Destroy(base.gameObject, 0.3f);
		}

		public void OnTriggerEnter2D(Collider2D collision)
		{
			Soldier component = collision.gameObject.GetComponent<Soldier>();
			if (collision.gameObject.tag == "Zombie")
			{
				map.zombieCount--;
				Debug.Log("Key: " + map.zombieCount);
				Object.Destroy(collision.gameObject);
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
					if (collision.gameObject.GetComponent<fire>() != null)
					{
						return;
					}
					if (collision.gameObject.GetComponent<Bomb>() != null)
					{
						collision.gameObject.GetComponent<Bomb>().Explode();
					}
				}
				Object.Destroy(collision.gameObject);
			}
			else
			{
				if (!(collision.gameObject.tag == "Player"))
				{
					return;
				}
				if (component.armor)
				{
					component.armor = false;
					return;
				}
				component.heart--;
				gm = collision.gameObject;
				if (gm != null)
				{
					gm.GetComponent<PolygonCollider2D>().enabled = false;
				}
				Singleton<BoomSpawner>.Instance.numberOfBombs = 1;
				Singleton<BoomSpawner>.Instance.firePower = 2;
				Singleton<Soldier>.Instance.speedValue = 1f;
				Singleton<Move>.Instance.newspeed = 2f;
			}
		}
	}
}
