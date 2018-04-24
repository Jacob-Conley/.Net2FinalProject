/* Check if database already exists and delete it if it does exist*/
IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases WHERE name = 'gameDB') 
BEGIN
	DROP DATABASE gameDB
	print '' print '*** dropping database gameDB'
END
GO

print '' print '*** creating database gameDB'
GO
CREATE DATABASE gameDB
GO

print '' print '*** using database gameDB'
GO
USE [gameDB]
GO

print '' print '*** Creating GameUser Table'
GO
/* ***** Object:  Table [dbo].[GameUser]     ***** */
CREATE TABLE [dbo].[GameUser](
	[GameUserID] 	[int]	IDENTITY  (100000,1) NOT NULL,
	[Username]		[nvarchar](16)				 NOT NULL,
	[Email]			[nvarchar](200)				 NOT NULL, /* Email used to find password if forgotten */
	[PasswordHash]	[nvarchar](100)				 NOT NULL,
	[GameEditor]	[bit]						 NOT NULL DEFAULT 0,
	[Active]		[bit]						 NOT NULL DEFAULT 1,
	CONSTRAINT [pk_GameUserID] PRIMARY KEY([GameUserID] ASC),
	CONSTRAINT [ak_Username] UNIQUE ([Username] ASC),
	CONSTRAINT [ak_Email] UNIQUE ([Email] ASC)
)
GO

print '' print '*** Creating Index for GameUser.Username'
GO
CREATE NONCLUSTERED INDEX [ix_GameUser_Username] ON [dbo].[GameUser]([Username]);
GO

print '' print '*** Creating PlayerCharacter Table'
GO
/* ***** Object:  Table [dbo].[PlayerCharacter]   ***** */
CREATE TABLE [dbo].[PlayerCharacter](
	[PlayerCharacterID]		[int]	IDENTITY  (100000,1)NOT NULL,
	[GameUserID]			[int]						NOT NULL,
	[PlayerName]			[nvarchar](40)				NOT NULL,
	[PlayerRace]			[nvarchar](20)				NOT NULL,
	[PlayerClass]			[nvarchar](20)				NOT NULL,
	[PlayerImage]			[nvarchar](40)				NOT NULL,
	[PlayerSlot]			[int]						NOT NULL,
	[StatID]				[int]						NOT NULL,
	[Active]				[bit]						NOT NULL DEFAULT 1,
	CONSTRAINT [pk_PlayerCharacterID] PRIMARY KEY([PlayerCharacterID] ASC)
)
GO

print '' print '*** Creating Stat Table'
GO
/* ***** Object:  Table [dbo].[Stat]   ***** */
CREATE TABLE [dbo].[Stat](
	[StatID]			[int]	IDENTITY  (100000,1) NOT NULL,
	[PlayerLevel]		[int]						 NOT NULL DEFAULT 1,
	[Strength]			[int]						 NOT NULL DEFAULT 10,
	[Stamina]			[int]						 NOT NULL DEFAULT 10,
	[Dexterity]			[int]						 NOT NULL DEFAULT 10,
	[Intelligence]		[int]						 NOT NULL DEFAULT 10,
	[PointsRemaining]	[int]						 NOT NULL DEFAULT 0,
	[Experience]		[int]						 NOT NULL DEFAULT 0,
	CONSTRAINT [pk_StatID] PRIMARY KEY([StatID] ASC)
)
GO

print '' print '*** Creating Equipment Table'
GO
/* ***** Object:  Table [dbo].[Equipment]   ***** */
CREATE TABLE [dbo].[Equipment](
	[EquipmentID]	[int]	IDENTITY  (100000,1) NOT NULL,
	[Name]			[nvarchar](40)				 NOT NULL,
	[Description]	[nvarchar](500)				 NOT NULL,
	[Defense]		[int]						 NOT NULL DEFAULT 0,
	[Active]		[bit]						 NOT NULL DEFAULT 1
	CONSTRAINT [pk_EquipmentID] PRIMARY KEY([EquipmentID] ASC)
)
GO

print '' print '*** Creating Weapon Table'
GO
/* ***** Object:  Table [dbo].[Weapon]   ***** */
CREATE TABLE [dbo].[Weapon] (
	[WeaponID]		[int]	IDENTITY  (100000,1) NOT NULL,
	[Name]			[nvarchar](40)				 NOT NULL,
	[Description]	[nvarchar](500)				 NOT NULL,
	[Attack]		[int]						 NOT NULL DEFAULT 0,
	[Active]		[bit]						 NOT NULL DEFAULT 1
	CONSTRAINT [pk_WeaponID] PRIMARY KEY([WeaponID] ASC)
)
GO

print '' print '*** Creating Item Table'
GO
/* ***** Object:  Table [dbo].[Item]   ***** */
CREATE TABLE [dbo].[Item](
	[ItemID]		[int]	IDENTITY  (100000,1) NOT NULL,
	[Name]			[nvarchar](40)				 NOT NULL,
	[Description]	[nvarchar](500)				 NOT NULL,
	[AttackBoost]	[float]					 	 NOT NULL DEFAULT 0,
	[DefenseBoost]	[float]					 	 NOT NULL DEFAULT 0,
	[Active]		[bit]						 NOT NULL DEFAULT 1
	CONSTRAINT [pk_ItemID] PRIMARY KEY([ItemID] ASC)
)
GO


print '' print '*** Creating CharacterWeapon Table'
GO
/* ***** Object:  Table [dbo].[CharacterWeapon]   ***** */
CREATE TABLE [dbo].[CharacterWeapon](
	[PlayerCharacterID]	[int]						 NOT NULL,
	[WeaponID]			[int]						 NOT NULL,
	[Quantity]			[int]						 NOT NULL DEFAULT 1,
	CONSTRAINT [pk_PlayerCharacterIDWeaponID] PRIMARY KEY([PlayerCharacterID] ASC, [WeaponID] ASC)
)
GO

print '' print '*** Creating CharacterEquipment Table'
GO
/* ***** Object:  Table [dbo].[CharacterEquipment]   ***** */
CREATE TABLE [dbo].[CharacterEquipment](
	[PlayerCharacterID]	[int]						 NOT NULL,
	[EquipmentID]		[int]						 NOT NULL,
	[Quantity]			[int]						 NOT NULL DEFAULT 1,
	CONSTRAINT [pk_PlayerCharacterIDEquipmentID] PRIMARY KEY([PlayerCharacterID] ASC, [EquipmentID] ASC)
)
GO

print '' print '*** Creating CharacterItem Table'
GO
/* ***** Object:  Table [dbo].[CharacterItem]   ***** */
CREATE TABLE [dbo].[CharacterItem](
	[PlayerCharacterID]	[int]						 NOT NULL,
	[ItemID]			[int]						 NOT NULL,
	[Quantity]			[int]						 NOT NULL DEFAULT 1,
	CONSTRAINT [pk_PlayerCharacterIDItemID] PRIMARY KEY([PlayerCharacterID] ASC, [ItemID] ASC)
)
GO


/* Constraints */
print '' print '*** Creating PlayerCharacter GameUserID foreign key'
GO
ALTER TABLE [dbo].[PlayerCharacter] WITH NOCHECK 
ADD CONSTRAINT [FK_GameUserID] FOREIGN KEY([GameUserID])
REFERENCES [dbo].[GameUser] ([GameUserID])
ON UPDATE CASCADE
GO

print '' print '*** Creating PlayerCharacter StatID foreign key'
GO
ALTER TABLE [dbo].[PlayerCharacter] WITH NOCHECK 
ADD CONSTRAINT [FK_StatID] FOREIGN KEY([StatID])
REFERENCES [dbo].[Stat] ([StatID])
ON UPDATE CASCADE
GO

print '' print '*** Creating CharacterWeapon WeaponID foreign key'
GO
ALTER TABLE [dbo].[CharacterWeapon] WITH NOCHECK 
ADD CONSTRAINT [FK_WeaponID] FOREIGN KEY([WeaponID])
REFERENCES [dbo].[Weapon] ([WeaponID])
ON UPDATE CASCADE
GO

print '' print '*** Creating CharacterWeapon PlayerCharacterID foreign key'
GO
ALTER TABLE [dbo].[CharacterWeapon] WITH NOCHECK 
ADD CONSTRAINT [FK_PlayerCharacterID_Weapon] FOREIGN KEY([PlayerCharacterID])
REFERENCES [dbo].[PlayerCharacter] ([PlayerCharacterID])
ON UPDATE CASCADE
GO

print '' print '*** Creating CharacterEquipment EquipmentID foreign key'
GO
ALTER TABLE [dbo].[CharacterEquipment] WITH NOCHECK 
ADD CONSTRAINT [FK_EquipmentID] FOREIGN KEY([EquipmentID])
REFERENCES [dbo].[Equipment] ([EquipmentID])
ON UPDATE CASCADE
GO

print '' print '*** Creating CharacterEquipment PlayerCharacterID foreign key'
GO
ALTER TABLE [dbo].[CharacterEquipment] WITH NOCHECK 
ADD CONSTRAINT [FK_PlayerCharacterID_Equipment] FOREIGN KEY([PlayerCharacterID])
REFERENCES [dbo].[PlayerCharacter] ([PlayerCharacterID])
ON UPDATE CASCADE
GO

print '' print '*** Creating CharacterItem ItemID foreign key'
GO
ALTER TABLE [dbo].[CharacterItem] WITH NOCHECK 
ADD CONSTRAINT [FK_ItemID] FOREIGN KEY([ItemID])
REFERENCES [dbo].[Item] ([ItemID])
ON UPDATE CASCADE
GO

print '' print '*** Creating CharacterItem PlayerCharacterID foreign key'
GO
ALTER TABLE [dbo].[CharacterItem] WITH NOCHECK 
ADD CONSTRAINT [FK_PlayerCharacterID_Items] FOREIGN KEY([PlayerCharacterID])
REFERENCES [dbo].[PlayerCharacter] ([PlayerCharacterID])
ON UPDATE CASCADE
GO

/* Sample Data*/

print '' print '*** Inserting GameUser Test Records'
GO
INSERT INTO [dbo].[GameUser]
		([Username], [Email], [PasswordHash])
	VALUES
		('PorkChop284', 'MinecraftMan2142@gmail.com', '89d50fa86f0efcd921423ffc328fbac8db0a7f75900613caf541558cd25e0c71'),
		('NumberOne21', 'MLPFan242@yahoo.com', '79e9b5f58b32c72c5c1e6c1d21e2679230467a228c29172cdabfa7fae91a3209' ),
		('WhateverUWant', 'NotYourFriend@hotmail.com', '936a185caaa266bb9cbe981e9e05cb78cd732b0b3280eb944412bb6f8f8f07af'),
		('Ur2Slow31', 'PieForOne@mail.com', '9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e'),
		('Enter_Username', 'Troll@abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijk.com', '9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e')
GO

print '' print '*** Inserting Item Test Records'

GO
INSERT INTO [dbo].[Item]
		([Name], [Description], [AttackBoost], [DefenseBoost])
	VALUES
		('Rat Blood', 'A slightly poisonous tincture that will boost the amount of damage your weapon will do for one battle', 1.05, 0),
		('Boar Hide','A rough hide that can bolster your defenses in a single battle', 0, 1.1),
		('Spider Venom','A strong poison that will enhance the power of your weapon for a single battle', 1.45, 0),
		('Iron Straps','A durable enhancement that will reduce the damage you take by a significant amount', 0, 1.8),
		('Fire Gem','An extremely rare gem that will significantly increase the damage you can do', 2.0, 0),
		('Re:Life', 'An unusual item that for some reason will allow the user to change their class and reset reconfigure their stats',0, 0)
GO


print '' print '*** Inserting Weapon Test Records'

GO
INSERT INTO [dbo].[Weapon]
		([Name], [Description], [Attack])
	VALUES
		('Wooden Sword', 'An extremely weak starter weapon that will never help anyone out in a real fight', 2),
		('Phasesaber', "A fancy weapon that makes pros think you know what you're doing, until they see just how weak the weapon actually is.", 15),
		('Morning Star', "Everyone's favorite medieval weapon, your enemies will totally see it coming, at least until they get knocked out by it.", 65),
		("Misfortune's Talon", "The game is not smart enough to realize just how powerful this weapon should be. It should be able to become a scythe, shotgun or a sword, but you can only use it as a sword", 125),
		('Golden Axe III', "The only weapon that could be more powerful than this would be Mjolnir, though you haven't seen that yet, so this is your best choice. This is a work of fiction. Names were clearly found in other places, and are most certainly not the products of the creator's imagination.", 250)
GO

print '' print '*** Inserting Equipment Test Records'

GO
INSERT INTO [dbo].[Equipment]
		([Name], [Description], [Defense])
	VALUES
		('Cactus Armor', "Ever wanted to be protected by a cactus suit, now you can. NOTE: this armor will not do damage to you or enemies, though it can be rather uncomfortable.", 5),
		('Liquid Armor', "Good protection from impact, but do you really think you will be safe when most of your enemies use tooth and claw?", 25),
		('Chainmail Armor', "Exactly like you're thinking, made of small metal rings that will protect you from harm most of the time.", 65),
		('Pumpkin Pete Hoodie',"50 Boxtops will give you hoodie, It does just as well as a combat skirt, and it shows an image of a cute little bunny.", 80),
		('Combat Skirt', "Totally not a dress, will protect you from most everything your enemies can throw at you.", 80)
GO

/* *** Stored Procedures *** */


print '' print '*** Creating sp_create_gameuser'
GO
CREATE PROCEDURE [dbo].[sp_create_gameuser]
	(
		@Username				[nvarchar](20),
		@PasswordHash			[nvarchar](100),
		@Email					[nvarchar](200)
	)
AS
	BEGIN
		INSERT INTO [dbo].[GameUser]
		([Username], [Email], [PasswordHash])
		VALUES
		(@Username, @Email, @PasswordHash)
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_authenticate_user'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_user]
	(
		@Username		[nvarchar](16),
		@PasswordHash	[nvarchar](100)
	)
AS
	BEGIN
		SELECT COUNT([GameUserID])
		FROM 	[GameUser]
		WHERE 	[Username] = @Username
		AND 	[PasswordHash] = @PasswordHash
		AND		[Active] = 1
	END
GO

print '' print '*** Creating sp_retrieve_gameuser_by_username'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_gameuser_by_username]
	(
		@Username		[nvarchar](16)
	)
AS
	BEGIN
		SELECT 	[GameUserID], [Username], [PasswordHash], [Email], [GameEditor], [Active]
		FROM 	[GameUser]
		WHERE 	[Username] = @Username
	END
GO


print '' print '*** Creating sp_upgrade_account'
GO
CREATE PROCEDURE [dbo].[sp_upgrade_account]
	(
		@GameUserID			int,
		@OldEmail			nvarchar(200),	
		@NewEmail			nvarchar(200),				
		@OldPasswordHash	nvarchar(100),
		@NewPasswordHash	nvarchar(100)
	)
AS
	BEGIN
		UPDATE [GameUser]
			SET [PasswordHash] = @NewPasswordHash,
				[GameEditor] = 1,
				[Email] = @NewEmail
			WHERE [GameUserID] = @GameUserID
			AND [PasswordHash] = @OldPasswordHash
			AND [Email] = @OldEmail
		RETURN @@ROWCOUNT
	END
GO
print '' print '*** Creating sp_update_account'
GO
CREATE PROCEDURE [dbo].[sp_update_account]
	(
		@GameUserID			int,
		@OldEmail			nvarchar(200),	
		@NewEmail			nvarchar(200),
		@OldPasswordHash	nvarchar(100),
		@NewPasswordHash	nvarchar(100)
	)
AS
	BEGIN
		UPDATE [GameUser]
			SET [PasswordHash] = @NewPasswordHash,
				[Email] = @NewEmail
			WHERE [GameUserID] = @GameUserID
			AND [PasswordHash] = @OldPasswordHash
			AND [Email] = @OldEmail
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_deactivate_gameuser_by_gameuserid'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_gameuser_by_gameuserid]
	(
		@GameUserID		[int]
	)
AS
	UPDATE [GameUser]
		SET [Active] = 0
		WHERE [GameUserID] = @GameUserID
		RETURN @@ROWCOUNT
GO


print '' print '*** Creating sp_create_character_by_gameuserid'
GO
CREATE PROCEDURE [dbo].[sp_create_character_by_gameuserid]
	(
		@GameUserID					[int],
		@PlayerName					[nvarchar](40),
		@PlayerRace					[nvarchar](20),
		@PlayerClass				[nvarchar](20),
		@PlayerImage				[nvarchar](40),
		@PlayerSlot					[int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[PlayerCharacter]
		([GameUserID], [PlayerName], [PlayerRace], [PlayerClass], [PlayerImage], [PlayerSlot], [StatID])
		VALUES
		(@GameUserID, @PlayerName, @PlayerRace, @PlayerClass, @PlayerImage, @PlayerSlot, IDENT_CURRENT('Stat'))
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_retrieve_characters_by_gameuserid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_characters_by_gameuserid]
	(
		@GameUserID		[int]
	)
AS
	BEGIN
		SELECT 	[PlayerCharacterID], [PlayerName], [PlayerRace],
				[PlayerClass], [PlayerImage], [PlayerSlot], [StatID]
		FROM 	[PlayerCharacter]
		WHERE 	[GameUserID] = @GameUserID
		AND 	[Active] = 1
	END
GO


print '' print '*** Creating sp_update_character_by_playercharacterid'
GO
CREATE PROCEDURE [dbo].[sp_update_character_by_playercharacterid]
	(
		@PlayerCharacterID			[int],
		@OldClass					[nvarchar](20),
		@NewClass					[nvarchar](20)
	)
AS
	BEGIN
		UPDATE [dbo].[PlayerCharacter]
			SET 	[PlayerClass] = @NewClass
			WHERE 	[PlayerCharacterID] = @PlayerCharacterID
			AND		[PlayerClass] = @OldClass
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_character_by_playercharacterid'
GO
CREATE PROCEDURE [dbo].[sp_delete_character_by_playercharacterid]
	(
		@PlayerCharacterID			int
	)
AS
	BEGIN
		UPDATE [dbo].[PlayerCharacter]
			SET 	[Active] = 0
			WHERE 	[PlayerCharacterID] = @PlayerCharacterID
			AND		[Active] = 1
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_create_stat'
GO
CREATE PROCEDURE [dbo].[sp_create_stat]
	(
		@Strength					[int],
		@Stamina					[int],
		@Dexterity					[int],
		@Intelligence				[int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[Stat]
		([Strength], [Stamina], [Dexterity], [Intelligence])
		VALUES
		(@Strength, @Stamina, @Dexterity, @Intelligence)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_select_stat_by_statid'
GO	
CREATE PROCEDURE [dbo].[sp_select_stat_by_statid]
	(
		@StatID	[int]
	)
AS
	BEGIN
		SELECT	[PlayerLevel],[Strength],[Stamina],[Dexterity],
				[Intelligence], [Experience], [PointsRemaining]
		FROM	[Stat]
		WHERE	[StatID] = @StatID
	END
GO

print '' print '*** Creating sp_reset_stats'
GO
CREATE PROCEDURE [dbo].[sp_reset_stats]
	(
		@StatID						[int],
		@OldStrength				[int],
		@OldStamina					[int],
		@OldDexterity				[int],
		@OldIntelligence			[int]
	)
AS
	BEGIN
		UPDATE [dbo].[Stat]
			SET 	[Strength] = default,
					[Stamina] = default,
					[Dexterity] = default,
					[Intelligence] = default,
					[PointsRemaining] = 5
			WHERE 	[StatID] = @StatID
			AND 	[Strength] = @OldStrength
			AND		[Stamina] = @OldStamina
			AND		[Dexterity] = @OldDexterity
			AND		[Intelligence] = @OldIntelligence
			AND		[PointsRemaining] = 0
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_update_stats_by_statid'
GO
CREATE PROCEDURE [dbo].[sp_update_stats_by_statid]
	(
		@StatID						[int],
		@OldStrength				[int],
		@NewStrength				[int],
		@OldStamina					[int],
		@NewStamina					[int],
		@OldDexterity				[int],
		@NewDexterity				[int],
		@OldIntelligence			[int],
		@NewIntelligence			[int],
		@PointsRemaining			[int]
	)
AS
	BEGIN
		UPDATE [dbo].[Stat]
			SET 	[Strength] = @NewStrength,
					[Stamina] = @NewStamina,
					[Dexterity] = @NewDexterity,
					[Intelligence] = @NewIntelligence,
					[PointsRemaining] = @PointsRemaining
			WHERE 	[StatID] = @StatID
			AND 	[Strength] = @OldStrength
			AND		[Stamina] = @OldStamina
			AND		[Dexterity] = @OldDexterity
			AND		[Intelligence] = @OldIntelligence
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_create_default_character_weapon_inventory'
GO
CREATE PROCEDURE [dbo].[sp_create_default_weapon_inventory]
	(
		@PlayerCharacterID					[int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[CharacterWeapon]
		([PlayerCharacterID], [WeaponID], [Quantity])
		VALUES
		(@PlayerCharacterID, "100000", 1)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_add_weapon_to_character_weapon_inventory'
GO
CREATE PROCEDURE [dbo].[sp_add_weapon_to_weapon_character_inventory]
	(
		@PlayerCharacterID				[int],
		@WeaponID						[int],
		@Quantity						[int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[CharacterWeapon]
		([PlayerCharacterID], [WeaponID], [Quantity])
		VALUES
		(@PlayerCharacterID, @WeaponID, @Quantity)
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_retrieve_weapons_by_playercharacterid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_weapons_by_playercharacterid]
	(
		@PlayerCharacterID			[int]
	)
AS
	BEGIN
		SELECT 	CharacterWeapon.WeaponID, [Name], 
				[Description], [Attack], [Quantity]
		FROM 	[CharacterWeapon], [Weapon]
		WHERE 	CharacterWeapon.WeaponID = Weapon.WeaponID
		AND 	[PlayerCharacterID] = @PlayerCharacterID
		AND		[Quantity] > 0
	END
GO


print '' print '*** Creating sp_update_weapon_quantity_by_playercharacterid'
GO
CREATE PROCEDURE [dbo].[sp_update_weapon_quantity_by_playercharacterid]
	(
		@PlayerCharacterID				[int],
		@WeaponID						[int],
		@OldQuantity					[int],
		@NewQuantity					[int]
	)
AS
	BEGIN
		UPDATE [CharacterWeapon]
			SET [Quantity] = @NewQuantity
			WHERE [PlayerCharacterID] = @PlayerCharacterID
			AND [WeaponID] = @WeaponID
			AND [Quantity] = @OldQuantity
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_weapon_from_character_weapon_inventory'
GO
CREATE PROCEDURE [dbo].[sp_delete_weapon_from_character_weapon_inventory]
	(
		@PlayerCharacterID				[int],
		@WeaponID						[int]
	)
AS
	BEGIN
		DELETE FROM [CharacterWeapon]
			WHERE [PlayerCharacterID] = @PlayerCharacterID
			AND [WeaponID] = @WeaponID
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_create_default_character_equipment_inventory'
GO
CREATE PROCEDURE [dbo].[sp_create_default_character_equipment_inventory]
	(
		@PlayerCharacterID					[int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[CharacterEquipment]
		([PlayerCharacterID], [EquipmentID], [Quantity])
		VALUES
		(@PlayerCharacterID, "100000", 1)
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_add_equipment_to_character_equipment_inventory'
GO
CREATE PROCEDURE [dbo].[sp_add_equipment_to_character_equipment_inventory]
	(
		@PlayerCharacterID				[int],
		@EquipmentID					[int],
		@Quantity						[int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[CharacterEquipment]
		([PlayerCharacterID], [EquipmentID], [Quantity])
		VALUES
		(@PlayerCharacterID, @EquipmentID, @Quantity)
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_retrieve_equipment_by_playercharacterid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_equipment_by_playercharacterid]
	(
		@PlayerCharacterID			[int]
	)
AS
	BEGIN
		SELECT 	CharacterEquipment.EquipmentID, [Name], 
				[Description], [Defense], [Quantity]
		FROM 	[CharacterEquipment], [Equipment]
		WHERE	CharacterEquipment.EquipmentID = Equipment.EquipmentID
		AND 	[PlayerCharacterID] = @PlayerCharacterID
		AND		[Quantity] > 0
	END
GO

print '' print '*** Creating sp_update_equipment_by_playercharacterid'
GO
CREATE PROCEDURE [dbo].[sp_update_equipment_by_playercharacterid]
	(
		@PlayerCharacterID				[int],
		@EquipmentID					[int],
		@OldQuantity					[int],
		@NewQuantity					[int]
	)
AS
	BEGIN
		UPDATE [CharacterEquipment]
			SET [Quantity] = @NewQuantity
			WHERE [PlayerCharacterID] = @PlayerCharacterID
			AND [EquipmentID] = @EquipmentID
			AND [Quantity] = @OldQuantity
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_equipment_from_character_equipment_inventory'
GO
CREATE PROCEDURE [dbo].[sp_delete_equipment_from_character_equipment_inventory]
	(
		@PlayerCharacterID				[int],
		@EquipmentID					[int]
	)
AS
	BEGIN
		DELETE FROM [CharacterEquipment]
			WHERE [PlayerCharacterID] = @PlayerCharacterID
			AND [EquipmentID] = @EquipmentID
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_create_default_item_inventory'
GO
CREATE PROCEDURE [dbo].[sp_create_default_item_inventory]
	(
		@PlayerCharacterID					[int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[CharacterItem]
		([PlayerCharacterID], [ItemID], [Quantity])
		VALUES
		(@PlayerCharacterID, "100005", 1)
		RETURN @@ROWCOUNT
	END
GO



print '' print '*** Creating sp_add_item_to_character_item_inventory'
GO
CREATE PROCEDURE [dbo].[sp_add_item_to_character_item_inventory]
	(
		@PlayerCharacterID			[int],
		@ItemID						[int],
		@Quantity					[int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[CharacterItem]
		([PlayerCharacterID], [ItemID], [Quantity])
		VALUES
		(@PlayerCharacterID, @ItemID, @Quantity)
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_retrieve_items_by_playercharacterid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_items_by_playercharacterid]
	(
		@PlayerCharacterID			[int]
	)
AS
	BEGIN
		SELECT 	CharacterItem.ItemID, [Name], [Description],
				[AttackBoost], [DefenseBoost], [Quantity]
		FROM 	[CharacterItem], [Item]
		WHERE 	CharacterItem.ItemID = Item.ItemID
		AND 	[PlayerCharacterID] = @PlayerCharacterID
		AND		[Quantity] > 0
	END
GO

print '' print '*** Creating sp_update_item_quantity'
GO
CREATE PROCEDURE [dbo].[sp_update_item_quantity]
	(
		@PlayerCharacterID				[int],
		@ItemID							[int],
		@OldQuantity					[int],
		@NewQuantity					[int]
	)
AS
	BEGIN
		UPDATE [CharacterItem]
			SET [Quantity] = @NewQuantity
			WHERE [PlayerCharacterID] = @PlayerCharacterID
			AND [ItemID] = @ItemID
			AND [Quantity] = @OldQuantity
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_item_from_character_item_inventory'
GO
CREATE PROCEDURE [dbo].[sp_delete_item_from_character_item_inventory]
	(
		@PlayerCharacterID				[int],
		@ItemID							[int]
	)
AS
	BEGIN
		DELETE FROM [CharacterItem]
			WHERE [PlayerCharacterID] = @PlayerCharacterID
			AND [ItemID] = @ItemID
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_create_equipment'
GO
CREATE PROCEDURE [dbo].[sp_create_equipment]
	(
		@Name					[nvarchar](40),
		@Description			[nvarchar](500),
		@Defense				[int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[Equipment]
		([Name], [Description], [Defense])
		VALUES
		(@Name, @Description, @Defense)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_retrieve_active_equipment_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_active_equipment_list]
AS
	BEGIN
		SELECT 	[EquipmentID], [Name], [Description], [Defense]
		FROM 	[Equipment]
		WHERE 	[Active] = 1
	END
GO

print '' print '*** Creating sp_update_equipment_by_equipmentid'
GO
CREATE PROCEDURE [dbo].[sp_update_equipment_by_equipmentid]
	(
		@EquipmentID					[int],
		@OldName						[nvarchar](40),
		@NewName						[nvarchar](40),
		@OldDescription					[nvarchar](500),
		@NewDescription					[nvarchar](500),
		@OldDefense						[int],
		@NewDefense						[int]
	)
AS
	BEGIN
		UPDATE [Equipment]
			SET [Name] = @NewName,
				[Description] = @NewDescription,
				[Defense] = @NewDefense
			WHERE [EquipmentID] = @EquipmentID
			AND [Name] = @OldName
			AND [Description] = @OldDescription
			AND [Defense] = @OldDefense
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_equipment_by_equipmentid'
GO
CREATE PROCEDURE [dbo].[sp_delete_equipment_by_equipmentid]
	(
		@EquipmentID					[int]
	)
AS
	BEGIN
		UPDATE [Equipment]
			SET [Active] = 0
			WHERE [EquipmentID] = @EquipmentID
			AND [Active] = 1
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_create_weapon'
GO
CREATE PROCEDURE [dbo].[sp_create_weapon]
	(
		@Name					[nvarchar](40),
		@Description			[nvarchar](500),
		@Attack					[int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[Weapon]
		([Name], [Description], [Attack])
		VALUES
		(@Name, @Description, @Attack)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_retrieve_active_weapon_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_active_weapon_list]
AS
	BEGIN
		SELECT 	[WeaponID], [Name], [Description], [Attack]
		FROM 	[Weapon]
		WHERE 	[Active] = 1
	END
GO

print '' print '*** Creating sp_update_weapon_by_weaponid'
GO
CREATE PROCEDURE [dbo].[sp_update_weapon_by_weaponid]
	(
		@WeaponID						[int],
		@OldName						[nvarchar](40),
		@NewName						[nvarchar](40),
		@OldDescription					[nvarchar](500),
		@NewDescription					[nvarchar](500),
		@OldAttack						[int],
		@NewAttack						[int]
	)
AS
	BEGIN
		UPDATE [Weapon]
			SET [Name] = @NewName,
				[Description] = @NewDescription,
				[Attack] = @NewAttack
			WHERE [WeaponID] = @WeaponID
			AND [Name] = @OldName
			AND [Description] = @OldDescription
			AND [Attack] = @OldAttack
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_weapon_by_weaponid'
GO
CREATE PROCEDURE [dbo].[sp_delete_weapon_by_weaponid]
	(
		@WeaponID					[int]
	)
AS
	BEGIN
		UPDATE [Weapon]
			SET [Active] = 0
			WHERE [WeaponID] = @WeaponID
			AND [Active] = 1
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_item'
GO
CREATE PROCEDURE [dbo].[sp_create_item]
	(
		@Name					[nvarchar](40),
		@Description			[nvarchar](500),
		@AttackBoost			[float],
		@DefenseBoost			[float]
	)
AS
	BEGIN
		INSERT INTO [dbo].[Item]
		([Name], [Description], [AttackBoost], [DefenseBoost])
		VALUES
		(@Name, @Description, @AttackBoost, @DefenseBoost)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_retrieve_active_item_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_active_item_list]
AS
	BEGIN
		SELECT 	[ItemID], [Name], [Description], [AttackBoost], [DefenseBoost]
		FROM 	[Item]
		WHERE 	[Active] = 1
	END
GO

print '' print '*** Creating sp_update_item_by_itemid'
GO
CREATE PROCEDURE [dbo].[sp_update_item_by_itemid]
	(
		@ItemID							[int],
		@OldName						[nvarchar](40),
		@NewName						[nvarchar](40),
		@OldDescription					[nvarchar](500),
		@NewDescription					[nvarchar](500),
		@OldAttackBoost					[float],
		@NewAttackBoost					[float],
		@OldDefenseBoost				[float],
		@NewDefenseBoost				[float]
	)
AS
	BEGIN
		UPDATE [Item]
			SET [Name] = @NewName,
				[Description] = @NewDescription,
				[AttackBoost] = @NewAttackBoost,
				[DefenseBoost] = @NewDefenseBoost
			WHERE [ItemID] = @ItemID
			AND [Name] = @OldName
			AND [Description] = @OldDescription
			AND [AttackBoost] = @OldAttackBoost
			AND [DefenseBoost] = @OldDefenseBoost
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_item_by_itemid'
GO
CREATE PROCEDURE [dbo].[sp_delete_item_by_itemid]
	(
		@ItemID					[int]
	)
AS
	BEGIN
		UPDATE [Item]
			SET [Active] = 0
			WHERE [ItemID] = @ItemID
			AND [Active] = 1
		RETURN @@ROWCOUNT
	END
GO
