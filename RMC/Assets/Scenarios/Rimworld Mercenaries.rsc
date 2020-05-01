<?xml version="1.0" encoding="utf-8"?>
<savedscenario>
	<meta>
		<gameVersion>1.1.2610 rev1053</gameVersion>
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
		<name>Rimworld Mercenaries</name>
		<summary>Play as ruthless rimworld mercenaries and fight for survival, and profit, on this savage planet.</summary>
		<description>Mercenaries are common on the lawless rimworlds and are significantly more ruthless and savage than those found elsewhere. They will fight against and for anyone who has the money to hire them. But they are usually hired by outlanders as protection against pirates. Their skills and equipment varies drastically but most are capable with a gun and modestly well equipped.</description>
		<playerFaction>
			<def>PlayerFaction</def>
			<factionDef>RMC_FactionDef_RWM_Colony</factionDef>
		</playerFaction>
		<parts>
			<li Class="ScenPart_ConfigPage_ConfigureStartingPawns">
				<def>ConfigPage_ConfigureStartingPawns</def>
				<pawnCount>4</pawnCount>
				<pawnChoiceCount>4</pawnChoiceCount>
			</li>
			<li Class="ScenPart_PlayerPawnsArriveMethod">
				<def>PlayerPawnsArriveMethod</def>
			</li>
			<li Class="ScenPart_StartingThing_Defined">
				<def>StartingThing_Defined</def>
				<thingDef>MealSurvivalPack</thingDef>
				<count>225</count>
			</li>
			<li Class="ScenPart_StartingThing_Defined">
				<def>StartingThing_Defined</def>
				<thingDef>MedicineIndustrial</thingDef>
				<count>30</count>
			</li>
			<li Class="ScenPart_StartingThing_Defined">
				<def>StartingThing_Defined</def>
				<thingDef>ComponentIndustrial</thingDef>
				<count>30</count>
			</li>
			<li Class="ScenPart_ScatterThingsNearPlayerStart">
				<def>ScatterThingsNearPlayerStart</def>
				<thingDef>Steel</thingDef>
				<count>500</count>
			</li>
			<li Class="ScenPart_ScatterThingsNearPlayerStart">
				<def>ScatterThingsNearPlayerStart</def>
				<thingDef>WoodLog</thingDef>
				<count>300</count>
			</li>
			<li Class="ScenPart_ScatterThingsAnywhere">
				<def>ScatterThingsAnywhere</def>
				<thingDef>ShipChunk</thingDef>
				<count>10</count>
			</li>
			<li Class="ScenPart_ScatterThingsAnywhere">
				<def>ScatterThingsAnywhere</def>
				<thingDef>Steel</thingDef>
				<count>1000</count>
			</li>
			<li Class="ScenPart_GameStartDialog">
				<def>GameStartDialog</def>
				<text>After an "unfortunate disagreement" with your former captain you decided to start your own mercenary company. You managed to gather a few like minded mercenaries and "commander" a few supplies before leaving.

Now, after days of walking, you have finally found a place to set up your base.
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