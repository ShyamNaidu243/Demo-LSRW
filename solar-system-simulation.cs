using UnityEngine;

// Script for controlling individual planets
public class Planet : MonoBehaviour
{
    public float orbitSpeed = 1f;
    public float rotationSpeed = 1f;
    public float distanceFromSun = 10f;
    public float orbitTilt = 0f;
    
    private Vector3 currentRotation;
    
    void Start()
    {
        // Random starting position on orbit
        transform.position = Random.onUnitSphere * distanceFromSun;
        currentRotation = new Vector3(orbitTilt, 0, 0);
    }
    
    void Update()
    {
        // Orbit around the sun
        transform.RotateAround(Vector3.zero, Quaternion.Euler(currentRotation) * Vector3.up, orbitSpeed * Time.deltaTime);
        
        // Rotate around own axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}

// Main script for setting up the solar system
public class SolarSystem : MonoBehaviour
{
    public GameObject planetPrefab;
    
    [System.Serializable]
    public class PlanetData
    {
        public string name;
        public float size;
        public float distanceFromSun;
        public float orbitSpeed;
        public float rotationSpeed;
        public float orbitTilt;
        public Color color;
    }
    
    public PlanetData[] planetDataArray = new PlanetData[]
    {
        new PlanetData { name = "Mercury", size = 0.383f, distanceFromSun = 5f, orbitSpeed = 47.87f, rotationSpeed = 10.83f, orbitTilt = 7f, color = new Color(0.7f, 0.7f, 0.7f) },
        new PlanetData { name = "Venus", size = 0.949f, distanceFromSun = 7f, orbitSpeed = 35.02f, rotationSpeed = -6.52f, orbitTilt = 3.4f, color = new Color(0.9f, 0.7f, 0.5f) },
        new PlanetData { name = "Earth", size = 1f, distanceFromSun = 10f, orbitSpeed = 29.78f, rotationSpeed = 15f, orbitTilt = 0f, color = new Color(0.2f, 0.5f, 1f) },
        new PlanetData { name = "Mars", size = 0.532f, distanceFromSun = 15f, orbitSpeed = 24.07f, rotationSpeed = 14.6f, orbitTilt = 1.9f, color = new Color(1f, 0.3f, 0f) },
        new PlanetData { name = "Jupiter", size = 11.21f, distanceFromSun = 25f, orbitSpeed = 13.07f, rotationSpeed = 36.6f, orbitTilt = 1.3f, color = new Color(0.8f, 0.6f, 0.4f) },
        new PlanetData { name = "Saturn", size = 9.45f, distanceFromSun = 35f, orbitSpeed = 9.68f, rotationSpeed = 33.5f, orbitTilt = 2.5f, color = new Color(0.9f, 0.8f, 0.5f) },
        new PlanetData { name = "Uranus", size = 4.01f, distanceFromSun = 45f, orbitSpeed = 6.80f, rotationSpeed = -14.8f, orbitTilt = 0.8f, color = new Color(0.5f, 0.8f, 0.9f) },
        new PlanetData { name = "Neptune", size = 3.88f, distanceFromSun = 55f, orbitSpeed = 5.43f, rotationSpeed = 17.2f, orbitTilt = 1.8f, color = new Color(0.2f, 0.3f, 0.9f) }
    };

    void Start()
    {
        CreateSun();
        CreatePlanets();
        SetupCamera();
    }

    void CreateSun()
    {
        GameObject sun = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sun.name = "Sun";
        sun.transform.localScale = Vector3.one * 3f;
        
        // Add light component to sun
        Light sunLight = sun.AddComponent<Light>();
        sunLight.type = LightType.Point;
        sunLight.intensity = 2f;
        
        // Add material to make sun glow
        Material sunMaterial = new Material(Shader.Find("Standard"));
        sunMaterial.color = Color.yellow;
        sunMaterial.EnableKeyword("_EMISSION");
        sunMaterial.SetColor("_EmissionColor", Color.yellow);
        sun.GetComponent<Renderer>().material = sunMaterial;
    }

    void CreatePlanets()
    {
        foreach (PlanetData planetData in planetDataArray)
        {
            GameObject planet = Instantiate(planetPrefab);
            planet.name = planetData.name;
            planet.transform.localScale = Vector3.one * planetData.size;
            
            Planet planetScript = planet.AddComponent<Planet>();
            planetScript.orbitSpeed = planetData.orbitSpeed;
            planetScript.rotationSpeed = planetData.rotationSpeed;
            planetScript.distanceFromSun = planetData.distanceFromSun;
            planetScript.orbitTilt = planetData.orbitTilt;
            
            // Set planet color
            Material planetMaterial = new Material(Shader.Find("Standard"));
            planetMaterial.color = planetData.color;
            planet.GetComponent<Renderer>().material = planetMaterial;
        }
    }

    void SetupCamera()
    {
        // Position camera to view entire solar system
        Camera.main.transform.position = new Vector3(0, 60f, -10f);
        Camera.main.transform.LookAt(Vector3.zero);
    }
}
