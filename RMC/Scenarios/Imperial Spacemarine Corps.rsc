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
		<name>Imperial Spacemarine Corps</name>
		<summary>Imperial spacemarine corps</summary>
		<description>Imperial spacemarine corps</description>
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