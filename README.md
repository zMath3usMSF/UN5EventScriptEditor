![MainBackground](UN5EventScriptEditor/Resources/BackgroundImage.png)
# About
This tool is designed to be a decompiler and compiler for the custom programming language used in the Event scripts of the RPG in the ADV/EVENT folder, such as Story Mode, Side Quests, NPCs, Shops, and more.
You can obtain it by going to the releases section of this page. If you encounter any bugs, please remember to report them in the issues section.
# You can check the list of available opcodes below:
**SWITCH_NOW_TO_STAGE**: Immediately switches to the requested Stage. Syntax (int StageID)<br>
**SWITCH_AFTER_TO_STAGE**: Switches to the Stage after something, such as after a Cutscene for example. Syntax (int StageID)<br>
**UPDATE_DIALOGUE**: Updates the flag of a dialogue in the MemoryCard to the requested value. Syntax (int MemoryCardOffset, int Value)<br>
**UPDATE_PROGRESS**: Updates the flag of a progress in the MemoryCard to the requested value. Syntax (int MemoryCardOffset, int Value)<br>
**SET_ITEM_TO_PLAYER**: Gives the requested item to the player and the quantity. Syntax (int ItemID, int ItemCount)<br>
**LOCK_PLAYER_ACTIONS**: Locks the player's actions.<br>
**UNLOCK_PLAYER_ACTIONS**: Unlocks the player's actions.<br>
**SET_PLAYER_POSITION**: Repositions the player to the requested position. Syntax (int PositionX, int PositionY, int PositionZ)<br>
**SET_PLAYER_ROTATION**: Repositions the player’s rotation to the requested rotation. Syntax (int RotationX, int RotationY, int RotationZ)<br>
**ROTATE_PLAYER_TO_NPC**: Rotates the player towards the direction of the requested NPC. Syntax (int NPCID)<br>
**UNLOCK_PLAYER_FROM_DIALOGUE**: Unlocks the player to be able to start another conversation.<br>
**SWITCH_PLAYER_TO**: Switches the player's character to the requested character. Syntax (int CharacterID)<br>
**SPAWN_NPC**: Spawns an NPC. Syntax (int NPCID, int StageID, int PositionX, int PositionY, int PositionZ, int RotationX, AnimationID, BalloonID)<br>
**REMOVE_NPC**: Removes an NPC. Syntax (int NPCID, StageID)<br>
**SET_NPC_POSITION_TO**: Repositions the NPC to the requested position. Syntax (int PositionX, int PositionY, int PositionZ)<br>
**SET_NPC_ROTATION_TO**: Repositions the NPC’s rotation to the requested rotation. Syntax (int RotationX, int RotationY, int RotationZ)<br>
**SPAWN_NPC_SMOKE**: Spawns an NPC with a smoke effect. Syntax (int NPCID, int StageID, int PositionX, int PositionY, int PositionZ, int RotationX, AnimationID, BalloonID)<br>
**REMOVE_NPC_SMOKE**: Removes an NPC with a smoke effect. Syntax (int NPCID, StageID)<br>
**ROTATE_NPC_TO_PLAYER**: Rotates the NPC towards the player’s direction. Syntax (int NPCID)<br>
**UNLOCK_NPC_TO_TALK**: Unlocks the requested NPC to be able to talk again. Syntax (int NPCID)<br>
**SET_INITIAL_CAMERA_POSITION_TO**: Sets the initial camera position to the requested position. Syntax (int PositionX, int PositionY, int PositionZ)<br>
**SET_FINAL_CAMERA_POSITION_TO**: Sets the final camera position to the requested position. Syntax (int PositionX, int PositionY, int PositionZ, int AnimationSpeed, int Unk)<br>
**SET_INITIAL_CAMERA_ROTATION_TO**: Sets the initial camera rotation to the requested rotation. Syntax (int RotationX, int RotationY, int RotationZ)<br>
**SET_FINAL_CAMERA_ROTATION_TO**: Sets the final camera rotation to the requested rotation. Syntax (int RotationX, int RotationY, int RotationZ, int AnimationSpeed, int Unk)<br>
**START_CAMERA_ANIMATION**: Starts the camera animation with the previously defined values.<br>
**UNLOCK_CAMERA**: Unlocks camera control.<br>
**SHOW_DIALOGUE**: Displays a dialogue, with the NPC’s name, message, and audio in rpgvoice.afs. Syntax (string NPCName, string NPCMessage, int SubAFSID, int AHXADXID)<br>
**CLOSE_DIALOGUE**: Closes the open dialogue.<br>
**SHOW_ANSWER_BOX**: Displays a box with alternatives. Syntax (string Option1, string Option2, string Option3, string Option4, int PreselectedOption)<br>
**CLOSE_ANSWER_BOX**: Closes the open alternatives box.<br>
**SHOW_OBTAINED_ITEM_MESSAGE**: Displays a message for the item that was obtained. Syntax (int ItemID)<br>
**CLOSE_OBTAINED_ITEM_MESSAGE**: Closes the open message for the obtained item.<br>
**SHOW_ACT_ANIMATION**: Starts an animation for the requested act. Syntax (int ActID)<br>
**START_2D_BATTLE**: Starts the requested 2D battle. Syntax (int BattleID)<br>
**START_3D_BATTLE**: Starts the 3D battle with the previously defined settings.<br>
**ADD_TO_3D_BATTLE**: Adds the requested configuration to the 3D battle.<br>
**START_CUTSCENE**: Starts the requested cutscene. Syntax (int CutsceneID)<br>
**SHOW_SAVE_OPTION**: Displays the save option box.<br>
**SHOW_LETTER**: Displays the requested letter. Syntax (int LetterID)<br>
**INDICATE_OBJECTIVE_ON_STAGE**: Indicates on the map where the objective is located. Syntax (int StageID)<br>
**START_TIME_ATTACK**: Starts a time attack. Syntax (int Minutes, int Seconds, int Milliseconds)<br>
**DISABLE_BANDITS_ON_STAGE**: Disables the bandits on the requested map.<br>
**SHOW_SHOP_MENU**: Displays the Shop Menu.<br>
**SHOW_RECORD_MENU**: Displays the Record Menu.<br>
**SHOW_INFORMATION**: Displays an information message. Syntax (int InformationID)<br>
**SHOW_MISSION_RESULT**: Displays the result of the Side Quest.<br>
**SPAWN_TRAP**: Spawns the requested trap on the map.<br>
**START_RETRY_MENU**: Displays the restart menu.<br>
**SHOW_EDIT_GROUP_MENU**: Displays the edit group menu.<br>
**PLAY_VAG_SOUND**: Plays the requested vag audio in snddata.bin. Syntax (int VagID)<br>
**PLAY_AFS_SOUND**: Plays the requested audio in afs. Syntax (int SubAFSID, int AHXADXID)<br>
**WAIT**: Waits the requested time before executing the next Script. Syntax (int Time)<br>
