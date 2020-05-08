RimWorld Military Colony
========================

RimWorld Military Colony (RMC) is a mod for the story generator game RimWorld. Its primary purpose is to add new ways to recruit colonists that are more similar to RTS games. For example a communications console is used to request reinforcements, they are paid in silver, and take time to "arrive" in the map. Soldiers can have equipment, training, and ranks/backstories. RMC is designed to be extendable so custom armies can be created using XML defs.

### Compatiblity

RMC does not patch or inject any code so it should be compatible with most mods. But if a mod alters the PawnGenerator class it may cause RMC to stop functioning properly.

### Contributing

1. Install Visual Studio 2017+
2. Clone this repository
3. Update the build output path in the RMC project settings. You will probably just have to change the drive letter from D: to C:
4. Rebuild the solution
5. Start RimWorld and verify the mod initializes properly

Now you should be completely setup. All of the code can be managed in visual studio and you just have to rebuild the solution for your changes to take effect. When submitting a pull request please document your request as much as possible so I know what the change is for.

### Custom Armies

First you should create a new FactionDef. Then use an ArmyDef to define an army for that faction. You can see some example ArmyDefs in **/Assets/XML/Defs/ArmyDefs**. The most significant part of an ArmyDef is the rank list which defines the types of soldiers that are in an army.

RankDefs are used to specify equipment, training, and back stories for a solider. But RankDefs act as an extension of PawnKindDefs so you must define a PawnKindDef for every RankDef. RankDefs are composed of other defs such as an EquipmentDef, TrainingDef, and/or BackstoryDef. You can use ISC and RWM defs as references. The ISC defs show how to create an army with standard equipment, training, and consistent back stories. The RWM defs show how to create a more irregular army.

 You can also use custom scenarios to spawn a staring unit. First you need to create a UnitDef and define it as the starting unit for an army in that army's ArmyDef. Then create a custom scenario with the **RMC_IncidentDef_Deploy** IncidentDef. You can see an example of some starting units and scenarios in **/Assets/XML/Defs/UnitDefs** and **/Assets/XML/Scenarios**.
