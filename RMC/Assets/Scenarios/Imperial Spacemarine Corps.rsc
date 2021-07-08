<?xml version="1.0" encoding="utf-8"?>
<savedscenario>
	<meta>
		<gameVersion>1.2.3005 rev1190</gameVersion>
		<modIds>
			<li>brrainz.harmony</li>
			<li>ludeon.rimworld</li>
			<li>ludeon.rimworld.royalty</li>
			<li>rodgers.rmc</li>
		</modIds>
		<modNames>
			<li>Harmony</li>
			<li>Core</li>
			<li>Royalty</li>
			<li>Rimworld Military Colony</li>
		</modNames>
	</meta>
	<scenario>
		<name>Imperial Spacemarine Corps</name>
		<summary>Play as the Imperial Spacemarines on their endless quest to bring order to the rimworlds.</summary>
		<description>The emperor of man in a fit of deranged pride dispatched thousands of imperial fleets to the edges of the galaxy, in order to "bring law and order to the rimworlds." These fleets still scour the farthest reaches of space even though their emperor and his empire is long dead. They call themselves Imperial Spacemarines, the emperor's chosen. But they are colloquially referred to as "the dead legion" by the inhabitants of the rimworlds.</description>
		<playerFaction>
			<def>PlayerFaction</def>
			<factionDef>RMC_FactionDef_ISC_Colony</factionDef>
		</playerFaction>
		<parts>
			<li Class="ScenPart_ConfigPage_ConfigureStartingPawns">
				<def>ConfigPage_ConfigureStartingPawns</def>
				<pawnCount>4</pawnCount>
				<pawnChoiceCount>4</pawnChoiceCount>
			</li>
			<li Class="ScenPart_PlayerPawnsArriveMethod">
				<def>PlayerPawnsArriveMethod</def>
				<method>DropPods</method>
			</li>
			<li Class="ScenPart_StartingThing_Defined">
				<def>StartingThing_Defined</def>
				<thingDef>Silver</thingDef>
				<count>800</count>
			</li>
			<li Class="ScenPart_StartingThing_Defined">
				<def>StartingThing_Defined</def>
				<thingDef>MealSurvivalPack</thingDef>
				<count>360</count>
			</li>
			<li Class="ScenPart_StartingThing_Defined">
				<def>StartingThing_Defined</def>
				<thingDef>MedicineIndustrial</thingDef>
				<count>50</count>
			</li>
			<li Class="ScenPart_StartingThing_Defined">
				<def>StartingThing_Defined</def>
				<thingDef>ComponentIndustrial</thingDef>
				<count>51</count>
			</li>
			<li Class="ScenPart_ScatterThingsNearPlayerStart">
				<def>ScatterThingsNearPlayerStart</def>
				<thingDef>Steel</thingDef>
				<count>750</count>
			</li>
			<li Class="ScenPart_ScatterThingsNearPlayerStart">
				<def>ScatterThingsNearPlayerStart</def>
				<thingDef>WoodLog</thingDef>
				<count>500</count>
			</li>
			<li Class="ScenPart_ScatterThingsAnywhere">
				<def>ScatterThingsAnywhere</def>
				<thingDef>Steel</thingDef>
				<count>500</count>
			</li>
			<li Class="ScenPart_GameStartDialog">
				<def>GameStartDialog</def>
				<text>As you fall from the imperial starship you can still hear the sound of the Lord-Admiral's orders echoing in your head. His voice cuts through the ear splitting sound of your drop-pod entering the atmosphere as if he was in the drop-pod with you.

"It is your duty to bring the emperor's love to this world filled with pirates and savages.

Remember, all who do not bend to the authority of the empire must be destroyed.

You will cleanse this world like the countless worlds that came before it or you will die trying."

When your drop-pod hits the ground. It finally sinks in, there is no going back, you will die on this planet...
</text>
				<textKey>GameStartDialog</textKey>
				<closeSound>GameStartSting</closeSound>
			</li>
			<li Class="ScenPart_CreateIncident">
				<def>CreateIncident</def>
				<incident>RMC_IncidentDef_Deploy</incident>
			</li>
		</parts>
	</scenario>
</savedscenario>