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
		<summary>Rimworld Mercenaries</summary>
		<description>Rimworld Mercenaries</description>
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
				<thingDef>Silver</thingDef>
				<count>500</count>
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
				<text />
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