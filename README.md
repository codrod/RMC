RimWorld Military Colony
========================

RimWorld Military Colony is a mod for the story generator game RimWorld. Its primary purpose is to add new ways to recruit colonists that are more similar to RTS games. For example a communications console is used to request reinforcements, they are paid in silver, and take time to "arrive" in the map. Soldiers can have equipment, training, and ranks/backstories. Currently two factions are implemented, Imperial Spacemarine and Mercenary but medieval and tribal factions are planned as well. Also units can be defined and used to maintain a consistent garrison or population for a colony. This mod is designed to be extendable so custom factions can be made by creating your own XML Defs but this is discouraged currently because the mod is still in the early stages of development.

### Installation instructions

You should be able to just copy the folder "RimWorld Military Colony" to Steam\steamapps\common\RimWorld\Mods without building anything. You can also open the visual studio project RMC and build the mod but you will probably have to change the build output path.

Once installed you will need to build a Military comms console which is basically the same as a regular comms console. Note that you can create a custom start scenario to provide starting troops and to help build a comms console earlier in the game. Eventually scenarios will be included with the mod.

### Custom factions

Factions are created using ArmyDefs you can see an example at Defs/ArmyDefs/ArmyDefs.xml. Basically they are used to define ranks, units, and general background information. RankDefs are used to specify equipment, training, and backstories. Note that RankDefs can be either formal like sergeant or role based like sniper or heavy infantry. UnitDefs are at the very least just a group size, but they can be a formal unit like a squad or platoon. Note that they can also be randomized to simulate irregular armies.

### Development plan

  * Add interface to hire soldiers individually
  * Improve documentation
  * Focus on debugging/refactoring
  * Proper packaged release
  * Add new factions
