using UnityEngine;

public class LedParticlesController : MonoBehaviour
{
    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles;

    public int gridWidth = 10; // Width of the grid
    public int gridHeight = 10; // Height of the grid
    public float spacing = 1.0f; // Distance between particles

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        if (ps == null)
        {
            Debug.LogError("No ParticleSystem found on " + gameObject.name);
            enabled = false;
            return;
        }

        // Set the maximum number of particles to match grid size
        var main = ps.main;
        main.maxParticles = gridWidth * gridHeight;

        particles = new ParticleSystem.Particle[main.maxParticles];

        ArrangeParticlesInGrid();
        ps.SetParticles(particles, particles.Length);
        ps.Play();
    }

    void ArrangeParticlesInGrid()
    {
        int index = 0;
        // Arrange particles in a 2D grid (rows and columns)
        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                if (index >= particles.Length) return;

                // Create a grid by adjusting the position of each particle
                Vector3 position = new Vector3(x * spacing, 0, z * spacing);
                particles[index].position = position;
                particles[index].startColor = Color.black; // Initial color (off)
                particles[index].startSize = 0.5f; // Adjust size to look like LEDs

                index++;
            }
        }
    }
}