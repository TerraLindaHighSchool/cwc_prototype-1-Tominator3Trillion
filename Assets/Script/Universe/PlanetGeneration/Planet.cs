using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Range(2, 256)]
    public int resolution = 10;
    public bool autoUpdate = true;
    public enum FaceRenderMask { All, Top, Bottom, Left, Right, Front, Back };
    public FaceRenderMask faceRenderMask;
    

    public LODInfo[] lods;

    public ShapeSettings shapeSettings;
    public ColorSettings colorSettings;

    [HideInInspector]
    public bool shapeSettingsFoldOut;
     [HideInInspector]
    public bool colorSettingsFoldOut;

    ShapeGenerator shapeGenerator = new ShapeGenerator();
    ColorGenerator colorGenerator = new ColorGenerator();

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    [SerializeField, HideInInspector]
    MeshCollider[] meshColliders;
    TerrainFace[] terrainFaces;

    void Initialize()
    {

        shapeGenerator.UpdateSettings(shapeSettings);
        colorGenerator.UpdateSettings(colorSettings);
        
        if(meshFilters == null || meshFilters.Length == 0 || meshColliders == null || meshColliders.Length == 0)
        {
            meshFilters = new MeshFilter[6];
            meshColliders = new MeshCollider[6];
        }
        terrainFaces = new TerrainFace[6];

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++)
        {
            if(meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>();
                meshColliders[i] = meshObj.AddComponent<MeshCollider>();
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }
            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colorSettings.planetMaterial;

            terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
            meshColliders[i].sharedMesh = meshFilters[i].sharedMesh;

            bool renderFace = faceRenderMask == FaceRenderMask.All || (int)faceRenderMask - 1 == i;
            meshFilters[i].gameObject.SetActive(renderFace);
        }
    }

    public void RemsheshTerrainFace(int face, int newRes) {
        terrainFaces[face].SetResolution(newRes);
        terrainFaces[face].ConstructMesh();
    }

    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh(false);
        GenerateColors();
    }

    public void OnShapeSettingsUpdated()
    {
        if(autoUpdate) {
            Initialize();
            GenerateMesh(false);
        }
    }

    public void OnColorSettingsUpdated()
    {
        if(autoUpdate) {
            Initialize();
            GenerateColors();
        }
    }

    void GenerateMesh(bool flat)
    {
        for(int i = 0; i < 6; i++)
        {
            if(meshFilters[i].gameObject.activeSelf)
            {
                if(flat) {
                    terrainFaces[i].ConstructFlatMesh();
                } else {
                    terrainFaces[i].ConstructMesh();
                }
                
            }
        }

        colorGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
    }

    void GenerateColors() {
        colorGenerator.UpdateColors();
        for(int i = 0; i < 6; i++)
        {
            if(meshFilters[i].gameObject.activeSelf)
            {
                terrainFaces[i].UpdateUVs(colorGenerator);
            }
        }

    }

    void FixedUpdate() {
        float radius = shapeSettings.planetRadius * (transform.localScale.x);
        float distFromCam = Vector3.Distance(transform.position, Camera.main.transform.position)-radius;
       // Debug.Log("r" + radius);
        //Debug.Log("d" + distFromCam);
        
        //get correct LOD
        int lodIndex = 0;
        for(int i = 0; i < lods.Length; i++) {
            if(distFromCam > lods[i].visibleDistThreshold) {
                lodIndex = i;
            } else {
                break;
            }
        }
        Debug.Log(lodIndex);
        if(resolution != lods[lodIndex].resolution) {
            if(lods[lodIndex].resolution == -1) {
                resolution = 20;
                
                StartCoroutine(BackgroundUpdate(lodIndex, false));
            } else {
                resolution = lods[lodIndex].resolution;
                StartCoroutine(BackgroundUpdate(lodIndex, false));
            }
            
        }
        
        
    }

    IEnumerator BackgroundUpdate(int lodIndex, bool flat) {
        Initialize();
        GenerateMesh(flat);
        GenerateColors();
        yield return null;
    }

    [System.Serializable]
    public class LODInfo {
        public int resolution;
        public float visibleDistThreshold;

        public LODInfo(int resolution, float visibleDistThreshold) {
            this.resolution = resolution;
            this.visibleDistThreshold = visibleDistThreshold;
        }
    }

    
}
