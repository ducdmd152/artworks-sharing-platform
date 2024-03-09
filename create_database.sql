USE master
GO
IF EXISTS(SELECT * FROM sys.databases WHERE name = 'ArtHub')
	BEGIN
    	DROP DATABASE [ArtHub]
  	END
GO 	
IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'ArtHub')
	BEGIN
    	CREATE DATABASE [ArtHub]
  	END
GO
USE [ArtHub]   
GO 
CREATE TABLE ArtHub.dbo.system_config (
	config_id int IDENTITY(0,1) NOT NULL,
	commision_rate float NOT NULL,
	created_date datetime NOT NULL,
	updated_date datetime NOT NULL,
	CONSTRAINT system_config_pk PRIMARY KEY (config_id)
);

CREATE TABLE ArtHub.dbo.[role] (
	role_id int IDENTITY(0,1) NOT NULL,
	role_name nvarchar(256)  NOT NULL,
	created_date datetime NOT NULL,
	updated_date datetime NOT NULL,
	CONSTRAINT role_pk PRIMARY KEY (role_id)
);

CREATE TABLE ArtHub.dbo.account (
	email varchar(256)  NOT NULL,
	password varchar(100)  NOT NULL,
	first_name nvarchar(256)  NOT NULL,
	last_name nvarchar(100)  NOT NULL,
	gender varchar(100)  NOT NULL,
	status varchar(100)  NOT NULL,
	enabled bit NOT NULL,
	avatar varchar(256)  NULL,
	role_id int NOT NULL,
	created_date datetime NOT NULL,
	updated_date datetime NOT NULL,
	CONSTRAINT account_pk PRIMARY KEY (email)
);

ALTER TABLE ArtHub.dbo.account ADD CONSTRAINT account_role_FK FOREIGN KEY (role_id) REFERENCES ArtHub.dbo.[role](role_id);


CREATE TABLE ArtHub.dbo.artist (
	email varchar(256) NOT NULL,
	artist_name nvarchar(256) NOT NULL,
	bio nvarchar(512) NULL,
	total_subscribe int DEFAULT 0 NOT NULL,
	created_date datetime NOT NULL,
	updated_date datetime NOT NULL,
	CONSTRAINT artist_pk PRIMARY KEY (email),
	CONSTRAINT artist_account_FK FOREIGN KEY (email) REFERENCES ArtHub.dbo.account(email)
);

CREATE TABLE ArtHub.dbo.fee (
	fee_id int IDENTITY(0,1) NOT NULL,
	amount float DEFAULT 0 NOT NULL,
	artist_email varchar(256) NOT NULL,
	created_date datetime NOT NULL,
	updated_date datetime NOT NULL,
	CONSTRAINT fee_artist_FK FOREIGN KEY (artist_email) REFERENCES ArtHub.dbo.artist(email)
);

ALTER TABLE ArtHub.dbo.fee ADD CONSTRAINT fee_pk PRIMARY KEY (fee_id);


CREATE TABLE ArtHub.dbo.subscriber (
	subscriber_id int IDENTITY(0,1) NOT NULL,
	email_user varchar(256) NOT NULL,
	email_artist varchar(256) NOT NULL,
	status varchar(100) NOT NULL,
	expired_date datetime NOT NULL,
	created_date datetime NOT NULL,
	updated_date datetime NOT NULL,
	CONSTRAINT subscriber_account_FK FOREIGN KEY (email_user) REFERENCES ArtHub.dbo.account(email),
	CONSTRAINT subscriber_artist_FK FOREIGN KEY (email_artist) REFERENCES ArtHub.dbo.artist(email)
);

ALTER TABLE ArtHub.dbo.subscriber ADD CONSTRAINT subscriber_pk PRIMARY KEY (subscriber_id);

CREATE TABLE ArtHub.dbo.[transaction] (
	transaction_id int IDENTITY(0,1) NOT NULL,
	amount float DEFAULT 0 NOT NULL,
	status varchar(100) NOT NULL,
	[type] varchar(100) NOT NULL,
	fee_id int NOT NULL,
	subscriber_id int NOT NULL,
	created_date datetime NOT NULL,
	updated_date datetime NOT NULL,
	CONSTRAINT transaction_fee_FK FOREIGN KEY (fee_id) REFERENCES ArtHub.dbo.fee(fee_id),
	CONSTRAINT transaction_subscriber_FK FOREIGN KEY (subscriber_id) REFERENCES ArtHub.dbo.subscriber(subscriber_id)
);

ALTER TABLE ArtHub.dbo.[transaction] ADD CONSTRAINT transaction_pk PRIMARY KEY (transaction_id);


CREATE TABLE ArtHub.dbo.post (
	post_id int IDENTITY(0,1) NOT NULL,
	title nvarchar(256) NOT NULL,
	description nvarchar(512) NOT NULL,
	status varchar(100) NOT NULL,
	[scope] varchar(100) NOT NULL,
	total_react int NOT NULL,
	total_view int NOT NULL,
	total_bookmark int NOT NULL,
	artist_email varchar(256) NOT NULL,
	created_date datetime NOT NULL,
	updated_date datetime NULL,
	CONSTRAINT post_pk PRIMARY KEY (post_id),
	CONSTRAINT post_artist_FK FOREIGN KEY (artist_email) REFERENCES ArtHub.dbo.artist(email)
);

CREATE TABLE ArtHub.dbo.bookmark (
	bookmark_id int IDENTITY(0,1) NOT NULL,
	delete_flag bit NOT NULL,
	post_id int NOT NULL,
	account_email int NOT NULL,
	created_date datetime NOT NULL,
	updated_date datetime NOT NULL,
	CONSTRAINT bookmark_pk PRIMARY KEY (bookmark_id),
	CONSTRAINT bookmark_post_FK FOREIGN KEY (post_id) REFERENCES ArtHub.dbo.post(post_id)
);

ALTER TABLE ArtHub.dbo.bookmark ALTER COLUMN account_email varchar(256) NOT NULL;
ALTER TABLE ArtHub.dbo.bookmark ADD CONSTRAINT bookmark_account_FK FOREIGN KEY (account_email) REFERENCES ArtHub.dbo.account(email);

CREATE TABLE ArtHub.dbo.[image] (
	image_id int IDENTITY(0,1) NOT NULL,
	[type] varchar(100) NOT NULL,
	image_url varchar(256) NOT NULL,
	delete_flag bit DEFAULT 0 NOT NULL,
	post_id int NOT NULL,
	created_date datetime NOT NULL,
	updated_date datetime NOT NULL,
	CONSTRAINT image_pk PRIMARY KEY (image_id),
	CONSTRAINT image_post_FK FOREIGN KEY (post_id) REFERENCES ArtHub.dbo.post(post_id)
);

CREATE TABLE ArtHub.dbo.reaction (
	reaction_id int IDENTITY(0,1) NOT NULL,
	post_id int NOT NULL,
	account_email varchar(256) NOT NULL,
	created_date datetime NOT NULL,
	updated_date datetime NOT NULL,
	CONSTRAINT reaction_pk PRIMARY KEY (reaction_id),
	CONSTRAINT reaction_post_FK FOREIGN KEY (post_id) REFERENCES ArtHub.dbo.post(post_id),
	CONSTRAINT reaction_account_FK FOREIGN KEY (account_email) REFERENCES ArtHub.dbo.account(email)
);


CREATE TABLE ArtHub.dbo.category (
	category_id int IDENTITY(0,1) NOT NULL,
	category_name varchar(100) NOT NULL,
	description nvarchar(512) NOT NULL,
	created_date datetime NOT NULL,
	updated_date datetime NOT NULL,
	CONSTRAINT category_pk PRIMARY KEY (category_id),
	CONSTRAINT category_unique UNIQUE (category_name)
);

CREATE TABLE ArtHub.dbo.post_category (
	category_id int NOT NULL,
	post_id int NOT NULL,
	created_date datetime NOT NULL,
	updated_date datetime NOT NULL,
	CONSTRAINT post_category_category_FK FOREIGN KEY (category_id) REFERENCES ArtHub.dbo.category(category_id),
	CONSTRAINT post_category_post_FK FOREIGN KEY (post_id) REFERENCES ArtHub.dbo.post(post_id)
);


INSERT INTO ArtHub.dbo.[role] (role_name,created_date,updated_date) VALUES
	 (N'audience','2023-05-05 00:00:00.0','2023-05-05 00:00:00.0');
INSERT INTO ArtHub.dbo.[role] (role_name,created_date,updated_date) VALUES
	 (N'creator','2023-05-05 00:00:00.0','2023-05-05 00:00:00.0');
INSERT INTO ArtHub.dbo.[role] (role_name,created_date,updated_date) VALUES
	 (N'moderator','2023-05-05 00:00:00.0','2023-05-05 00:00:00.0');
INSERT INTO ArtHub.dbo.[role] (role_name,created_date,updated_date) VALUES
	 (N'admin','2023-05-05 00:00:00.0','2023-05-05 00:00:00.0');
	 
INSERT INTO account (email,password,first_name,last_name,gender,status,enabled,avatar,role_id,created_date,updated_date) VALUES
	 (N'thongne@gmail.com',N'5Hzghx8mdZs7r8BgULR4IQ==',N'hehe',N'hehehe',N'nam',N'ok',1,N'./images/CreatorProfile.jpg',0,'2024-05-05 00:00:00.0','2024-02-25 16:02:24.74');
INSERT INTO account (email,password,first_name,last_name,gender,status,enabled,avatar,role_id,created_date,updated_date) VALUES
	 (N'user1@gmail.com',N'5Hzghx8mdZs7r8BgULR4IQ==',N'hehe',N'hehehe',N'nam',N'ok',1,N'./images/CreatorProfile.jpg',0,'2024-05-05 00:00:00.0','2024-02-25 16:02:24.74');
INSERT INTO account (email,password,first_name,last_name,gender,status,enabled,avatar,role_id,created_date,updated_date) VALUES
	 (N'user2@gmail.com',N'5Hzghx8mdZs7r8BgULR4IQ==',N'hehe',N'hehehe',N'nam',N'ok',1,N'./images/CreatorProfile.jpg',0,'2024-05-05 00:00:00.0','2024-02-25 16:02:24.74');	
INSERT INTO account (email,password,first_name,last_name,gender,status,enabled,avatar,role_id,created_date,updated_date) VALUES
	 (N'user3@gmail.com',N'5Hzghx8mdZs7r8BgULR4IQ==',N'hehe',N'hehehe',N'nam',N'ok',1,N'./images/CreatorProfile.jpg',0,'2024-05-05 00:00:00.0','2024-02-25 16:02:24.74');
INSERT INTO account (email,password,first_name,last_name,gender,status,enabled,avatar,role_id,created_date,updated_date) VALUES
	 (N'user4@gmail.com',N'5Hzghx8mdZs7r8BgULR4IQ==',N'hehe',N'hehehe',N'nam',N'ok',1,N'./images/CreatorProfile.jpg',0,'2024-05-05 00:00:00.0','2024-02-25 16:02:24.74');
INSERT INTO account (email,password,first_name,last_name,gender,status,enabled,avatar,role_id,created_date,updated_date) VALUES
	 (N'user5@gmail.com',N'5Hzghx8mdZs7r8BgULR4IQ==',N'hehe',N'hehehe',N'nam',N'ok',1,N'./images/CreatorProfile.jpg',0,'2024-05-05 00:00:00.0','2024-02-25 16:02:24.74');		
INSERT INTO account (email,password,first_name,last_name,gender,status,enabled,avatar,role_id,created_date,updated_date) VALUES
	 (N'creator@gmail.com',N'5Hzghx8mdZs7r8BgULR4IQ==',N'hehe',N'hehehe',N'nam',N'ok',1,N'./images/CreatorProfile.jpg',1,'2024-05-05 00:00:00.0','2024-02-25 16:02:24.74');
INSERT INTO account (email,password,first_name,last_name,gender,status,enabled,avatar,role_id,created_date,updated_date) VALUES
	 (N'creator2@gmail.com',N'5Hzghx8mdZs7r8BgULR4IQ==',N'hehe',N'hehehe',N'nam',N'ok',1,N'./images/CreatorProfile.jpg',1,'2024-05-05 00:00:00.0','2024-02-25 16:02:24.74');
INSERT INTO account (email,password,first_name,last_name,gender,status,enabled,avatar,role_id,created_date,updated_date) VALUES
	 (N'creator3@gmail.com',N'5Hzghx8mdZs7r8BgULR4IQ==',N'hehe',N'hehehe',N'nam',N'ok',1,N'./images/CreatorProfile.jpg',1,'2024-05-05 00:00:00.0','2024-02-25 16:02:24.74');
INSERT INTO account (email,password,first_name,last_name,gender,status,enabled,avatar,role_id,created_date,updated_date) VALUES
	 (N'creator4@gmail.com',N'5Hzghx8mdZs7r8BgULR4IQ==',N'hehe',N'hehehe',N'nam',N'ok',1,N'./images/CreatorProfile.jpg',1,'2024-05-05 00:00:00.0','2024-02-25 16:02:24.74');	
INSERT INTO account (email,password,first_name,last_name,gender,status,enabled,avatar,role_id,created_date,updated_date) VALUES
	 (N'creator5@gmail.com',N'5Hzghx8mdZs7r8BgULR4IQ==',N'hehe',N'hehehe',N'nam',N'ok',1,N'./images/CreatorProfile.jpg',1,'2024-05-05 00:00:00.0','2024-02-25 16:02:24.74');	
INSERT INTO account (email,password,first_name,last_name,gender,status,enabled,avatar,role_id,created_date,updated_date) VALUES
	 (N'moderator@gmail.com',N'5Hzghx8mdZs7r8BgULR4IQ==',N'hehe',N'hehehe',N'nam',N'ok',1,N'./images/CreatorProfile.jpg',2,'2024-05-05 00:00:00.0','2024-02-25 16:02:24.74');
INSERT INTO account (email,password,first_name,last_name,gender,status,enabled,avatar,role_id,created_date,updated_date) VALUES
	 (N'admin@gmail.com',N'5Hzghx8mdZs7r8BgULR4IQ==',N'hehe',N'hehehe',N'nam',N'ok',1,N'./images/CreatorProfile.jpg',3,'2024-05-05 00:00:00.0','2024-02-25 16:02:24.74');
	 
	

-- Inserting 5 artists (assuming there are at least 5 accounts with role 'creator')
INSERT INTO ArtHub.dbo.artist (email, artist_name, bio, created_date, updated_date)
VALUES 
('creator@gmail.com', N'Artist One', N'Bio of Artist One.', GETDATE(), GETDATE()),
('creator2@gmail.com', N'Artist Two', N'Bio of Artist Two.', GETDATE(), GETDATE()),
('creator3@gmail.com', N'Artist Three', N'Bio of Artist Three.', GETDATE(), GETDATE()),
('creator4@gmail.com', N'Artist Four', N'Bio of Artist Four.', GETDATE(), GETDATE()),
('creator5@gmail.com', N'Artist Five', N'Bio of Artist Five.', GETDATE(), GETDATE());


-- Inserting 5 fees (assuming there are at least 5 artists)
INSERT INTO ArtHub.dbo.fee (amount, artist_email, created_date, updated_date)
VALUES 
(100, 'creator@gmail.com', GETDATE(), GETDATE()),
(150, 'creator2@gmail.com', GETDATE(), GETDATE()),
(200, 'creator3@gmail.com', GETDATE(), GETDATE()),
(250, 'creator4@gmail.com', GETDATE(), GETDATE()),
(300, 'creator5@gmail.com', GETDATE(), GETDATE());


-- Inserting 5 subscribers (assuming there are at least 5 user accounts and 5 artists)
INSERT INTO ArtHub.dbo.subscriber (email_user, email_artist, status, expired_date, created_date, updated_date)
VALUES 
('user1@gmail.com', 'creator@gmail.com', 'active', DATEADD(year, 1, GETDATE()), GETDATE(), GETDATE()),
('user2@gmail.com', 'creator2@gmail.com', 'active', DATEADD(year, 1, GETDATE()), GETDATE(), GETDATE()),
('user3@gmail.com', 'creator3@gmail.com', 'active', DATEADD(year, 1, GETDATE()), GETDATE(), GETDATE()),
('user4@gmail.com', 'creator4@gmail.com', 'active', DATEADD(year, 1, GETDATE()), GETDATE(), GETDATE()),
('user5@gmail.com', 'creator5@gmail.com', 'active', DATEADD(year, 1, GETDATE()), GETDATE(), GETDATE());


-- Inserting 5 transactions (assuming there are at least 5 fees and 5 subscribers)
INSERT INTO ArtHub.dbo.[transaction] (amount, status, [type], fee_id, subscriber_id, created_date, updated_date)
VALUES 
(50, 'completed', 'subscription', 0, 0, GETDATE(), GETDATE()),
(60, 'completed', 'donation', 1, 1, GETDATE(), GETDATE()),
(70, 'pending', 'subscription', 2, 2, GETDATE(), GETDATE()),
(80, 'failed', 'donation', 3, 3, GETDATE(), GETDATE()),
(90, 'completed', 'subscription', 4, 4, GETDATE(), GETDATE());


-- Inserting 5 posts (assuming there are at least 5 artists)
INSERT INTO ArtHub.dbo.post (title, description, status, [scope], total_react, total_view, total_bookmark, artist_email, created_date)
VALUES 
(N'Post Title 1', N'Description 1', 'approval', 'public', 0, 0, 0, 'creator@gmail.com', GETDATE()),
(N'Post Title 2', N'Description 2', 'approval', 'public', 0, 0, 0, 'creator2@gmail.com', GETDATE()),
(N'Post Title 3', N'Description 3', 'approval', 'public', 0, 0, 0, 'creator3@gmail.com', GETDATE()),
(N'Post Title 4', N'Description 4', 'approval', 'public', 0, 0, 0, 'creator4@gmail.com', GETDATE()),
(N'Post Title 5', N'Description 5', 'approval', 'public', 0, 0, 0, 'creator5@gmail.com', GETDATE());


-- Inserting 5 bookmarks (assuming there are at least 5 posts and 5 accounts)
INSERT INTO ArtHub.dbo.bookmark (delete_flag, post_id, account_email, created_date, updated_date)
VALUES 
(0, 0, 'user1@gmail.com', GETDATE(), GETDATE()),
(0, 1, 'user2@gmail.com', GETDATE(), GETDATE()),
(0, 2, 'user3@gmail.com', GETDATE(), GETDATE()),
(0, 3, 'user4@gmail.com', GETDATE(), GETDATE()),
(0, 4, 'user5@gmail.com', GETDATE(), GETDATE());


-- Inserting 5 images (assuming there are at least 5 posts)
INSERT INTO ArtHub.dbo.[image] ([type], image_url, post_id, created_date, updated_date)
VALUES 
('jpg', './images/CreatorProfile.jpg', 0, GETDATE(), GETDATE()),
('png', './images/Login.png', 1, GETDATE(), GETDATE()),
('jpg', './images/CreatorProfile.jpg', 2, GETDATE(), GETDATE()),
('png', './images/Login.png', 3, GETDATE(), GETDATE()),
('jpg', './images/CreatorProfile.jpg', 4, GETDATE(), GETDATE());


-- Inserting 5 reactions (assuming there are at least 5 posts and 5 accounts)
INSERT INTO ArtHub.dbo.reaction (post_id, account_email, created_date, updated_date)
VALUES 
(0, 'user1@gmail.com', GETDATE(), GETDATE()),
(1, 'user2@gmail.com', GETDATE(), GETDATE()),
(2, 'user3@gmail.com', GETDATE(), GETDATE()),
(3, 'user4@gmail.com', GETDATE(), GETDATE()),
(4, 'user5@gmail.com', GETDATE(), GETDATE());


-- Inserting 5 categories
INSERT INTO ArtHub.dbo.category (category_name, description, created_date, updated_date)
VALUES 
('Painting', 'Paintings and artworks', GETDATE(), GETDATE()),
('Sculpture', 'Three-dimensional artwork', GETDATE(), GETDATE()),
('Digital', 'Digital art creations', GETDATE(), GETDATE()),
('Photography', 'Photographic works of art', GETDATE(), GETDATE()),
('Mixed Media', 'Artworks featuring mixed media', GETDATE(), GETDATE());


-- Inserting 5 post_category links (assuming there are at least 5 posts and 5 categories)
INSERT INTO ArtHub.dbo.post_category (category_id, post_id, created_date, updated_date)
VALUES 
(0, 0, GETDATE(), GETDATE()),
(1, 1, GETDATE(), GETDATE()),
(2, 2, GETDATE(), GETDATE()),
(3, 3, GETDATE(), GETDATE()),
(4, 4, GETDATE(), GETDATE());	