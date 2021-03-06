﻿using UnityEngine;

/** DungeonLauncher Class
 * @Inherits MechanismBase
 * This Mecanism allows the player to launch a dunjon.
 * This Script should only be attached to the DungeonLauncher Prefab.
 **/
public class DungeonLauncher : MechanismBase
{
    private MapGenerator generator;     //Used to create the dungeon

    /** Override Start Method
     * Adding the operations to get and build the MapGenerator with a Seed for a random Dungeon Shape
     */
    protected override void Start()
    {
        base.Start();
        generator = GetComponent<MapGenerator>();
        generator.Seed = Random.Range(0, 50);
    }

    /** ActivateInterractable Method
     * This Method overrides the parent one.
     * It detects if the mechanism as not been activated yet.
     * After activation it launches the dunjon and then Destroyes itself to provide multiple launches.
     * Warning ! Only the script will be Destroyed, not the GameObject
     */
    public override void ActivateInterractable()
    {
        if (!isActivated)
        {
            Debug.Log("Launching new Dunjon");

            // Generation du dungeon
            generator.GenerationMap();

            // Placement of the Player
            SpawnPos();

            base.ActivateInterractable();
        }
        Destroy(this);
    }

    /** SpawnPos Method
     * This method will get the transform position of a random room in the MapGenerator
     * The center of this room is the spawn position of the player and will teleport this one inside.
     * Then the room is removed from the RoomsList
     */
    private void SpawnPos()
    {
        // Select a random number inside the rooms
        int rand = Random.Range(0, generator.rooms.Count);

        // Get the corresponding room and remove it from the list
        Transform spawnRoom = generator.rooms[rand].transform;
        generator.rooms.RemoveAt(rand);

        // Get the player in the scene and teleport him inside the room
        GameObject player = GameObject.FindWithTag("Player");
        player.transform.position = spawnRoom.position;
    }
}
