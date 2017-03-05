using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour {

    int size = 16;
    float[] cols;
    int levels;
    float sizesize;
    int GRAIN = 8; //PLAY WITH THIS PARAMETER    
    heightBorder hb;

    // Use this for initialization
    void Start () {
        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Chunk.prefab", typeof(GameObject));
        GameObject chunk = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        
        sizesize = (size * size);

        int[][] heightMap = heightConversion(createHeightMap(size, size));

        //intitialize
        hb = new heightBorder();

        levels = 5;
        hb.data = new float[levels];

        hb.size = levels;

                            
    }
	
    private struct heightBorder
    {
        public float[] data { get; set; }
        public int size { get; set; }
        public void setData(System.Array d)
        {
            for (int i = 0; i < size; ++i)
                data[i] = i;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
    
     //This is something of a "helper function" to create an initial grid
    //before the recursive function is called. 
    float[][] createHeightMap(int w, int h)
    {
        float c1, c2, c3, c4;

        //Assign the four corners of the intial grid random color values
        //These will end up being the colors of the four corners of the applet.     
        c1 = Random.value;
        c2 = Random.value;
        c3 = Random.value;
        c4 = Random.value;

        divideGrid(0.0f, 0.0f, w, h, c1, c2, c3, c4);

        return vec2mat(cols, size);
    }

    //This is the recursive function that implements the random midpoint
    //displacement algorithm.  It will call itself until the grid pieces
    //become smaller than one pixel.   
    void divideGrid(float x, float y, int w, int h, float c1, float c2, float c3, float c4)
    {
        int newWidth = w >> 1;
        int newHeight = h >> 1;

        if (w == 1 || h == 1)
        {
            //The four corners of the grid piece will be averaged and drawn as a single pixel.
            float c = (c1 + c2 + c3 + c4) / 4;
            cols[(int)x + (int)y * size] = c;//computeColor(c); //TEST, TO USE CHNAGE COLOUR TO FLOAT
        }
        else
        {
            float middle = (c1 + c2 + c3 + c4) / 4 + displace(newWidth + newHeight);      //Randomly displace the midpoint!
            float edge1 = (c1 + c2) / 2; //Calculate the edges by averaging the two corners of each edge.
            float edge2 = (c2 + c3) / 2;
            float edge3 = (c3 + c4) / 2;
            float edge4 = (c4 + c1) / 2;

            //Make sure that the midpoint doesn't accidentally "randomly displaced" past the boundaries!
            if (middle <= 0)
            {
                middle = 0;
            }
            else if (middle > 1.0f)
            {
                middle = 1.0f;
            }

            //Do the operation over again for each of the four new grids.                 
            divideGrid(x, y, newWidth, newHeight, c1, edge1, middle, edge4);
            divideGrid(x + newWidth, y, newWidth, newHeight, edge1, c2, edge2, middle);
            divideGrid(x + newWidth, y + newHeight, newWidth, newHeight, middle, edge2, c3, edge3);
            divideGrid(x, y + newHeight, newWidth, newHeight, edge4, middle, edge3, c4);
        }
    }

    float displace(float num)
    {
        float max = num / sizesize * GRAIN;
        return Random.Range(-0.5f, 0.5f) * max;
    }

    float[][] vec2mat(float[] vec, int n)
    {
        float[][] mat = new float[n][];       
        for (int i = 0; i < n; ++i)
        {
            mat[i] = new float[n];
            for (int j = 0; j < n; ++j)
            {
                mat[i][j] = vec[n * i + j];
            }
        }
        return mat;
    }

    int[][] heightConversion(float[][] mat)
    {
        int[][] matInt = new int[size][];
        for (int i = 0; i < size; ++i)
        {
            matInt[i] = new int[size];
            for (int j = 0; j < size; ++j)
            {
                byte increment = 0;
                while (increment>hb.size)
                {
                    if (mat[i][j] > hb.data[increment++])
                        matInt[i][j] = increment;                   
                }
            }
        }
        return matInt; 
    }
    
}
