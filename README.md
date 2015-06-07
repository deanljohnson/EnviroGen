EnviroGen is a WIP environment generator that can quickly generate large 2d height maps. Currently, these environments are generated using noise algorithms such as Simplex Noise. The environments can then be modified to generate more continent like shapes than standard Simplex Noise would. You can also simulate erosion processes using multiple configurable erosion algorithms. EnviroGen also has easy to use Colorizer classes that facilitate easy coloring of the produced terrain based on height. River generation is currently in the works.

EnviroGen is built to integrate easily with SFML.net and comes with SFML based classes that allow easy visualization of the environment. The EnviroGen Display project in the source code gives an easy to use configuration screen and automatically displays the environment in a separate window, allowing you to experiment with generation parameters.

To try it out:
1) Download the repository from the link on the left.
2) Open the .sln file in Visual Studio.
3) Set EnviroGen Display as the startup project.
4) Build and run.