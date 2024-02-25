USE master
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


INSERT INTO ArtHub.dbo.[role] (role_id,role_name,created_date,updated_date) VALUES
	 (0,N'audience','2023-05-05 00:00:00.0','2023-05-05 00:00:00.0');


INSERT INTO account (email,password,first_name,last_name,gender,status,enabled,avatar,role_id,created_date,updated_date) VALUES
	 (N'thongne@gnail.com',N'12434',N'hehe',N'hehehe',N'nam',N'ok',0,N'avater new',0,'2024-05-05 00:00:00.0','2024-02-25 16:02:24.74');

	