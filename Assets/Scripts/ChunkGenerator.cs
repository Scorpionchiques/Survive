<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour {

    objectsGenerator oGenerator;

    // Use this for initialization
    void Start () {
        
        //intitialize
        oGenerator = new objectsGenerator();      
        setObjects();
    }
	
    void setObjects()
    {
        int[][] objmap = oGenerator.GetObjectsMap();       
        for (int i =0; i<objmap.GetLength(0); ++i)
        {           
            for (int j=0; j<objmap.GetLength(0);++j)
            {               
                if (objmap[i][j] == 4) //magic 4
                {
                    Object treeprefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/tree.prefab", typeof(GameObject));
                    GameObject tree = (GameObject)Instantiate(treeprefab,new Vector3(10.0f * ((float)i / 16), 10.0f * ((float)j / 16), 0), transform.rotation);                    
                    tree.transform.SetParent(transform);
                                        
                    //tree.transform.localPosition = GetComponent<PositionReferences>().GetNextPosition();
                    //in work with it
                }
            }   
        }

    }
    // Update is called once per frame
    void Update () {
		
	}
}

//in work with it
public class PositionReferences : MonoBehaviour
{
    public Transform[] positions;
    private int index = 0;

    public Vector3 GetNextPosition()
    {
        Vector3 result = positions[index].localPosition;
        index = index + 1;
        return result;
    }
}

//object that generate object map throw using diaond square algorithm
public class objectsGenerator
{
    private int size;//2^n size of square objects mat
    private float[] cols;//vec to fill with height
    private int levels; //count of objects types
    private float sizesize; //size^2
    private int GRAIN; //PLAY WITH THIS PARAMETER    
    private heightBorder hb;

    public objectsGenerator()
    {
        size = 16;
        hb = new heightBorder();
        levels = 5;
        hb.size = levels;
        sizesize = (size * size);
        cols = new float[(int)sizesize];
        GRAIN = 8;
    }

    //get objects map
    public int[][] GetObjectsMap()
    {
        return heightConversion(createHeightMap(size, size));
    }

    //This is something of a "helper function" to create an initial grid and to call diamond square agorithm.
    private float[][] createHeightMap(int w, int h)
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

    //This is the recursive function that implements the diamond square algorithm.
    private void divideGrid(float x, float y, int w, int h, float c1, float c2, float c3, float c4)
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

    //random displace for diamond square algorithm
    private float displace(float num)
    {
        float max = num / sizesize * GRAIN;
        return Random.Range(-0.5f, 0.5f) * max;
    }

    //convert vec to square mat
    private float[][] vec2mat(float[] vec, int n)
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
        vec = null;
        return mat;
    }

    //convert float heoght map to int objects map
    private int[][] heightConversion(float[][] mat)
    {
        int increment=0;
        int[][] matInt = new int[size][];
        for (int i = 0; i < size; ++i)
        {
            matInt[i] = new int[size];
            for (int j = 0; j < size; ++j)
            {               
                increment = 0;
                while (increment < hb.size)
                {
                    if (mat[i][j] > hb.data[increment++])
                    {
                        matInt[i][j] = increment;                              
                    }
                }
            }
        }
        mat = null;
        return matInt;
    }

    //help tool-structure to carry out height to object conversion
    private class heightBorder
    {
        private float[] _data;
        public float[] data
        {
            get
            {
                if (_data == null)
                {
                    _data = new float[size];
                    for (int i = 0; i < size; ++i)
                        _data[i] = (float)(i) / size;                    
                }
                return _data;
            }
            set { }
        }
        public int size { get; set; }
    }
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour {

    objectsGenerator oGenerator;

    public float[] HeightBound;// { get; set; }
    // Use this for initialization
    void Start () {
        if (HeightBound.Length==0)
        {
            HeightBound = new float[4];
            for (int i = 0; i < 4; ++i)
            {
                HeightBound[i] = Random.value;
            }
        }

        //intitialize        
        oGenerator = new objectsGenerator();      
        setObjects();
    }
	
    void setObjects()
    {
        int[][] objmap = oGenerator.GetObjectsMap(HeightBound);
        for (int i =0; i<objmap.GetLength(0); i+=1)
        {           
            for (int j=0; j<objmap.GetLength(0);j+=1)
            {               
                if (objmap[i][j] == 5) //magic 5
                {
                    Object treeprefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/tree.prefab", typeof(GameObject));
                    GameObject tree = (GameObject)Instantiate(treeprefab,
                        new Vector3(10.0f * ((float)(i) / 16) + transform.position.x, 10.0f * ((float)j / 16) + transform.position.y, 0),
                        transform.rotation);                    
                    tree.transform.SetParent(transform);
                                        
                    //tree.transform.localPosition = GetComponent<PositionReferences>().GetNextPosition();
                    //in work with it
                }
                if (objmap[i][j] == 4) //magic 4
                {
                    Object shrubprefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/shrub.prefab", typeof(GameObject));
                    GameObject shrub = (GameObject)Instantiate(shrubprefab,
                        new Vector3(10.0f * ((float)i / 16) + transform.position.x, 10.0f * ((float)j / 16) + transform.position.y, 0),
                        transform.rotation);
                    shrub.transform.SetParent(transform);

                    //tree.transform.localPosition = GetComponent<PositionReferences>().GetNextPosition();
                    //in work with it
                }

                if (objmap[i][j] == 1) //magic 1
                {
                    Object waterprefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/water.prefab", typeof(GameObject));
                    GameObject water = (GameObject)Instantiate(waterprefab,
                        new Vector3(10.0f * ((float)i / 16) + transform.position.x, 10.0f * ((float)j / 16) + transform.position.y, 0),
                        transform.rotation);
                    water.transform.SetParent(transform);

                    //tree.transform.localPosition = GetComponent<PositionReferences>().GetNextPosition();
                    //in work with it
                }
            }   
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}

//in work with it
public class PositionReferences : MonoBehaviour
{
    public Transform[] positions;
    private int index = 0;

    public Vector3 GetNextPosition()
    {
        Vector3 result = positions[index].localPosition;
        index = index + 1;
        return result;
    }
}

//object that generate object map throw using diaond square algorithm
public class objectsGenerator
{
    private int size;//2^n size of square objects mat
    private float[] cols;//vec to fill with height
    private int levels; //count of objects types
    private float sizesize; //size^2
    private int GRAIN; //PLAY WITH THIS PARAMETER    
    private heightBorder hb;

    public objectsGenerator()
    {
        size = 16;
        hb = new heightBorder();
        levels = 5;
        hb.size = levels;
        sizesize = (size * size);
        cols = new float[(int)sizesize];
        GRAIN = 8;
    }

    //get objects map
    public int[][] GetObjectsMap(float[] HeightBound)
    {
        return heightConversion(createHeightMap(size, size, HeightBound));
    }

    //This is something of a "helper function" to create an initial grid and to call diamond square agorithm.
    private float[][] createHeightMap(int w, int h, float[] HeightBound)
    {

        //Assign the four corners of the intial grid random color values
        //These will end up being the colors of the four corners of the applet.     
        
        divideGrid(0.0f, 0.0f, w, h, HeightBound[0], HeightBound[1], HeightBound[2], HeightBound[3]);

        return vec2mat(cols, size);
    }

    //This is the recursive function that implements the diamond square algorithm.
    private void divideGrid(float x, float y, int w, int h, float c1, float c2, float c3, float c4)
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
            if (middle < 0)
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

    //random displace for diamond square algorithm
    private float displace(float num)
    {
        float max = num / sizesize * GRAIN;
        return Random.Range(-0.5f, 0.5f) * max;
    }

    //convert vec to square mat
    private float[][] vec2mat(float[] vec, int n)
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
        vec = null;
        return mat;
    }

    //convert float heoght map to int objects map
    private int[][] heightConversion(float[][] mat)
    {
        int increment=0;
        int[][] matInt = new int[size][];
        for (int i = 0; i < size; ++i)
        {
            matInt[i] = new int[size];
            for (int j = 0; j < size; ++j)
            {               
                increment = 0;
                while (increment < hb.size)
                {
                    if (mat[i][j] > hb.data[increment++])
                    {
                        matInt[i][j] = increment;                              
                    }
                }
            }
        }
        mat = null;
        return matInt;
    }

    //help tool-structure to carry out height to object conversion
    private class heightBorder
    {
        private float[] _data;
        public float[] data
        {
            get
            {
                if (_data == null)
                {
                    _data = new float[size];
                    for (int i = 0; i < size; ++i)
                        _data[i] = (float)(i) / size;                    
                }
                return _data;
            }
            set { }
        }
        public int size { get; set; }
    }
>>>>>>> origin/master
}