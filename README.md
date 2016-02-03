EnviroGen is a WIP environment generator that can quickly generate large 2d height maps. Currently, these environments are generated using noise algorithms such as Simplex Noise. The height maps can be modified with simple modifiers such as addition, scaling, or normalization, or physical processes such as erosion.

EnviroGen's goal is to produce whole simulated environments: terrain, oceans, weather, tectonics, vegetation, animals, and more. This is a large project and is unlikely to be finished soon. Currently, terrain can be generated and colored based on height. Two erosion processes (thermal and hydraulic) can be ran, but they are ran at generation time, not as an active simulation.

The EnviroGen Display project in the source code gives an easy to use node based editor that allows you to quickly apply modifiers to an environment and see the results.

EnviroGen offers the ability for users to build their own nodes that they can then connect to the EnviroGen Display and edit using it's simple node editor. As an example, there is an included plugin that allows you to export EnviroGen terrain into a simple Minecraft map, using the Substrate library.

EnviroGen is also quite easy to port to other viewing environments. Currently, work is being done to hook EnviroGen up to a Minecraft server and have it update the Minecraft world in sync with the simulation. Integration with the Unity3d and Duality game engines is also planned.
