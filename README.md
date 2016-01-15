EnviroGen is a WIP environment generator that can quickly generate large 2d height maps. Currently, these environments are generated using noise algorithms such as Simplex Noise. The environments can then be modified to generate more continent like shapes than standard Simplex Noise would. You can also simulate erosion processes using multiple configurable erosion algorithms. EnviroGen also has easy to use Colorizer classes that facilitate easy coloring of the produced terrain based on height.

The EnviroGen Display project in the source code gives an easy to use configuration screen and automatically displays the environment, allowing you to experiment with generation parameters.

EnviroGen's goal is to be completely procedural and infinite in the way it generates it's terrain, not based on predetermined placement of features such as continents.

Current focus of development is to provide more versatility within the configuration display and to implement export options.

To try it out:
1) Download the repository from the link on the left.
2) Open the .sln file in Visual Studio.
3) Set EnviroGen Display as the startup project.
4) Build and run.
