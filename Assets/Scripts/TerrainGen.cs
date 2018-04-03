using UnityEngine;
using System.Collections;

public class TerrainGen : MonoBehaviour {

    private TerrainData terrainData;

    // Width, height, depth of the terrain
    public Vector3 worldSize;   // Starting out with (200, 50, 200)

    // This is the number of divisons in both the X and Y axis of the terrain
    public int terrainResolution;   // Starting out with a value of 128
    [Range(0,1)]
    public float terrainXValue = .1f;
    [Range(0, 1)]
    public float terrainYValue = .1f;

	void Start ()
    {
        //terrainData = gameObject.GetComponent<TerrainData>();
        // or
        terrainData = gameObject.GetComponent<TerrainCollider>().terrainData;
        terrainData.size = worldSize;
        terrainData.heightmapResolution = terrainResolution;

        SetHeightMap();
	}

    void SetHeightMap()
    {
        // Get an array that is the right size, so that even if I change the value of the
        // terrain resolution this will still work
        float[,] heightMap = terrainData.GetHeights(0,0,terrainResolution,terrainResolution);
        
        // I have to loop through each vertex for the height map and set it to a value
        // between 0.0 and 1.0
        for(int i = 0; i < terrainResolution; i++)  
        {
            for(int j = 0; j < terrainResolution; j++)
            {
                heightMap[i, j] = Mathf.PerlinNoise(terrainXValue * i, terrainYValue * j); 

            }
        }

        terrainData.SetHeights(0, 0, heightMap);
     }  
}
