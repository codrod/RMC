<h1>RimWorld Military Colony</h1>
RimWorld Military Colony is a mod for the story generator game RimWorld. Its primary purpose is to add new ways to recurit colonists that are more similar to RTS games. For example a communications console is used to request reinforcements, they are paid in silver, and take time to "arrive" in the map. Soldiers can have equiment, training, and ranks/backstories. Currently two factions are implemented, Imperial Spacemarine and Mercenary but medieval and triabl factions are planned as well. Also units can be defined and used to maintain a consistent garrison or population for a colony. This mod is designed to be extendable so custom factions can be made by creating your own XML Defs but this is discouraged currently because the mod is still in the early stages of development.
<h3>Installation instructions</h3>
<p>You should be able to just copy the folder "RimWorld Military Colony" to Steam\steamapps\common\RimWorld\Mods without building anything. You can also open the visual studio project RMC and build the mod but you will probably have to change the build output path.</p>
<p>Once installed you will need to build a Military comms console which is basically the same as a regular comms console. Note that you can create a custom start scenario to provide starting troops and to help build a comms console earlier in the game. Eventually scenarios will be included with the mod.</p>
<h3>Custom factions</h3>
Factions are created using ArmyDefs you can see an example at Defs/ArmyDefs/ArmyDefs.xml. Basically they are used to define ranks, units, and general background information. RankDefs are used to specify equipment, training, and backstories. Note that RankDefs can be either formal like sergeant or role based like sniper or heavy infantry. UnitDefs are at the very least just a group size, but they can be a formal unit like a squad or platoon. Note that they can also be randomized to simulate irregular armies.
<h3>Development Plan</h3>
<ol>
  <li>Add interface to hire soldiers individually</li>
  <li>Improve documentation</li>
  <li>Focus on debugging/refactoring</li>
  <li>Proper packaged release</li>
  <li>Add new factions</li>
</ol>
