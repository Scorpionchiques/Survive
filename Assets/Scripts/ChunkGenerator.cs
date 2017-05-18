using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

//in work with it
public enum MapObjects
{
    tree = 5, shrub = 4, deepMarsh = 1, marsh = 2, leftCoastMarsh=-1, rightCoastMarsh=-2,
    upCoastMarsh=-3, botCoastMarsh=-4, leftupCoastMarsh=-5, leftbotCoastMarsh=-6, rightupCoastMarsh=-7, rightbotCoastMarsh=-8, road = 3
};

public class ChunkGenerator : MonoBehaviour {

    objectsGenerator oGenerator;
    float backGroundSize;
    float halfBackGroundSize;
    float[] heightBound;// { get; set; }
    public float[] HeightBound {
        get
        {
            if (heightBound == null)
            {
                heightBound = new float[4];
                for (int i = 0; i < 4; ++i)
                {
                    heightBound[i] = UnityEngine.Random.value;
               }
                Debug.Log("neok");
            }
            return heightBound;
        }
        set
        {
            heightBound = value;
        }
    }
    // Use this for initialization
    void Start () {
        //intitialize      
        backGroundSize = 20.0f;
        halfBackGroundSize = backGroundSize / 2;        
        oGenerator = new objectsGenerator();      
        setObjects();
    }

    private Vector3 displacement()
    {
        return new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f), 0);
    }

    void setObjects()
    {
        MapObjects[][] objmap = oGenerator.GetObjectsMap(HeightBound);
        int objmapsize = objmap.GetLength(0);
        for (int i =0; i<objmapsize; ++i)
        {            
            for (int j=0; j< objmapsize; ++j)
            {
                UnityEngine.Object prefab = null;
                GameObject obj = null;
                string pathPrefab = null;
                Vector3 disp = Vector3.zero;
                Quaternion rot = transform.rotation;
                switch (objmap[i][j])
                {
                    case MapObjects.deepMarsh:
                        pathPrefab = "Assets/Prefabs/water.prefab";
                        break;
                    case MapObjects.leftCoastMarsh:
                    case MapObjects.marsh:
                        pathPrefab = "Assets/Prefabs/marsh.prefab";
                        break;
                    case MapObjects.shrub:
                        pathPrefab = "Assets/Prefabs/shrub.prefab";
                        disp = displacement();
                        break;
                    case MapObjects.tree:
                        pathPrefab = "Assets/Prefabs/tree.prefab";
                        disp = displacement();
                        break;
                    case MapObjects.rightCoastMarsh:
                        pathPrefab = "Assets/Prefabs/marsh.prefab";
                        rot *= Quaternion.Euler(0, 0, 90);
                        break;
                }
                if (pathPrefab != null)
                {
                    Vector3 pos = new Vector3(backGroundSize * ((float)(i) / objmapsize) + transform.position.x - halfBackGroundSize,
                                              backGroundSize * ((float)(j) / objmapsize) + transform.position.y - halfBackGroundSize,
                                                0) + disp;
                    prefab = AssetDatabase.LoadAssetAtPath(pathPrefab, typeof(GameObject));
                    obj = (GameObject)Instantiate(prefab,
                        pos,
                        rot );
                    obj.transform.SetParent(this.transform);
                }
            }   
        }
    }
    // Update is called once per frame
    void Update () {
		
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
        size = 64;
        hb = new heightBorder();
        levels = 5;
        hb.size = levels;
        sizesize = (size * size);
        cols = new float[(int)sizesize];
        GRAIN = 128;
    }

    //get objects map
    public MapObjects[][] GetObjectsMap(float[] HeightBound)
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
            cols[(int)x + (int)y * size] = c;
        }
        else
        {
            float middle = (c1 + c2 + c3 + c4) / 4 + displace(newWidth + newHeight);      //Randomly displace the midpoint!
            float edge1 = (c1 + c2) / 2 + displace(newWidth + newHeight); //Calculate the edges by averaging the two corners of each edge.
            float edge2 = (c2 + c3) / 2 + displace(newWidth + newHeight);
            float edge3 = (c3 + c4) / 2 + displace(newWidth + newHeight);
            float edge4 = (c4 + c1) / 2 + displace(newWidth + newHeight);

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
        return UnityEngine.Random.Range(-0.01f, 0.4f) * max;
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
    private MapObjects[][] heightConversion(float[][] mat)
    {
        int l = size/16;
        int increment=0;
        MapObjects[][] matInt = new MapObjects[size/l][];
        for (int i = 0; i < size-1; i+=l)
        {
            matInt[i/l] = new MapObjects[size/l];
            for (int j = 0; j < size-1; j+=l)
            {               
                float control = (mat[i][j]+mat[i + 1][j] + mat[i + 1][j + 1] + mat[i][j + 1])/l;
                increment = 0;
                while (increment < hb.size)
                {
                    if (control > hb.data[increment++])
                    {
                        matInt[i/l][j/l] = (MapObjects)increment;                         
                    }
                }
                if (i / l > 2 && j / l > 2 && matInt[i / l - 1][j / l - 1] == MapObjects.marsh)
                    matInt[i / l - 1][j / l - 1] = chooseCoastOrientation(
                    matInt[i / l - 1][j / l - 2], matInt[i / l - 1][j / l], matInt[i / l][j / l - 1], matInt[i / l - 2][j / l - 1],
                     matInt[i / l][j / l - 2], matInt[i / l - 2][j / l - 2], matInt[i / l][j / l], matInt[i / l - 2][j / l]
                    );
            }
        }
        mat = null;
        return matInt;
    }

    private MapObjects chooseCoastOrientation(MapObjects l, MapObjects r, MapObjects bot, MapObjects up,
                                              MapObjects lbot, MapObjects lup, MapObjects rbot, MapObjects rup)
    {
        MapObjects ret = MapObjects.marsh;
        if (l != MapObjects.marsh && l != MapObjects.deepMarsh && r == MapObjects.deepMarsh || r == MapObjects.marsh)
        {
            ret = MapObjects.leftCoastMarsh;
        }
        if (l == MapObjects.marsh || l == MapObjects.deepMarsh && r != MapObjects.marsh && r != MapObjects.deepMarsh)
        {
            ret = MapObjects.rightCoastMarsh;
        }
        return ret;
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
}