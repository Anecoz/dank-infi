using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {
	public GameObject player;
	public GameObject grassSpritePrefab;
	public GameObject desertSpritePrefab;
	public GameObject waterSpritePrefab;
	public GameObject bushPrefab;

	public float noiseScale = 30f;
	public float lacunarity = 2f;
	public float persistance = 0.5f;

	private int maxX, minX, maxY, minY;
	private int spawnDistance = 20;

	private enum Biome {
		Grass,
		Desert,
		Water
	}

	void Start () {
		SetMinMax ();
		for (int x = minX - 1; x < maxX + 1; x++)
			for (int y = minY - 1; y < maxY + 1; y++) {
				SpawnAt (x, y);
			}
	}

	void Update () {
		Vector3 playerPos = player.transform.position;
		if (maxX < (int)playerPos.x + spawnDistance) {
			SpawnRight ();
		}
		if (minX > (int)playerPos.x - spawnDistance) {
			SpawnLeft ();
		}
		if (maxY < (int)playerPos.y + spawnDistance) {
			SpawnTop ();
		}
		if (minY > (int)playerPos.y - spawnDistance) {
			SpawnBottom ();
		}
		SetMinMax ();
	}

	private void SetMinMax() {
		Vector3 playerPos = player.transform.position;
		maxX = (int)playerPos.x + spawnDistance;
		minX = (int)playerPos.x - spawnDistance;
		maxY = (int)playerPos.y + spawnDistance;
		minY = (int)playerPos.y - spawnDistance;
	}

	private void SpawnRight() {
		Vector3 playerPos = player.transform.position;
		int localminY = (int)playerPos.y - spawnDistance;
		int localmaxY = (int)playerPos.y + spawnDistance;
		for (int y = localminY - 1; y < localmaxY + 1; y++) {
			SpawnAt (maxX, y);
		}
	}

	private void SpawnTop() {
		Vector3 playerPos = player.transform.position;
		int localminX = (int)playerPos.x - spawnDistance;
		int localmaxX = (int)playerPos.x + spawnDistance;
		for (int x = localminX - 1; x < localmaxX + 1; x++) {
			SpawnAt (x, maxY);
		}
	}

	private void SpawnLeft() {
		Vector3 playerPos = player.transform.position;
		int localminY = (int)playerPos.y - spawnDistance;
		int localmaxY = (int)playerPos.y + spawnDistance;
		for (int y = localminY - 1; y < localmaxY + 1; y++) {
			SpawnAt (minX, y);
		}
	}

	private void SpawnBottom() {
		Vector3 playerPos = player.transform.position;
		int localminX = (int)playerPos.x - spawnDistance;
		int localmaxX = (int)playerPos.x + spawnDistance;
		for (int x = localminX - 1; x < localmaxX + 1; x++) {
			SpawnAt (x, minY);
		}
	}

	private void SpawnAt(int x, int y) {
		GameObject sprite = null;
		var biome = GetBiome (x, y);
		Vector3 spawnpos = new Vector3 (x, y, 1);
		switch (biome) {
		case Biome.Desert:
			sprite = (GameObject)Instantiate (desertSpritePrefab);
			break;
		case Biome.Grass:
			if (RollDice(2f)) {
				SpawnBush (x, y);
			}
			sprite = (GameObject)Instantiate (grassSpritePrefab);
			break;
		default:
		case Biome.Water:
			sprite = (GameObject)Instantiate (waterSpritePrefab);
			break;
		}
		sprite.gameObject.GetComponent<Tile> ().SetParameters (player, spawnDistance*2);
		sprite.transform.position = spawnpos;
	}

	private void SpawnBush(int x, int y) {
		GameObject bush = (GameObject)Instantiate (bushPrefab);
		bush.gameObject.GetComponent<Tile> ().SetParameters (player, spawnDistance * 2);
		bush.gameObject.GetComponent<Collectible> ().SetParameters (player, InventoryItem.Type.Wood);
		bush.transform.position = new Vector3 (x, y, 0.5f);
	}

	private Biome GetBiome(int x, int y) {
		float noise = Noise (x, y);
		if (noise < 0.3f)
			return Biome.Water;
		if (noise < 0.4f)
			return Biome.Desert;		
		return Biome.Grass;
	}

	private float Noise(int x, int y) {
		float xf = (float)x/noiseScale;
		float yf = (float)y/noiseScale;
		float height = 0f;
		int octaves = 3;
		float frequency = 0f;
		float amplitude = 0f;
		for (int i = 0; i < octaves; i++) {
			frequency = Mathf.Pow (lacunarity, i);
			amplitude = Mathf.Pow (persistance, i);
			height += Mathf.PerlinNoise (xf * frequency, yf * frequency) * amplitude * 0.5f;
		}
		return height;
	}

	public bool RollDice(float chance) {
		return Random.Range (0f, 100f) <= chance;
	}
}
