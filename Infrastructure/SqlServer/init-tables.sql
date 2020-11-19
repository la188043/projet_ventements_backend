USE sql_ventements_project;

DROP TABLE IF EXISTS addressuserv;
DROP TABLE IF EXISTS userv;
DROP TABLE IF EXISTS category;
DROP TABLE IF EXISTS subcategory;
DROP TABLE IF EXISTS item;
DROP TABLE IF EXISTS bag;
DROP TABLE IF EXISTS orderv;
DROP TABLE IF EXISTS review;
DROP TABLE IF EXISTS wishlist;
DROP TABLE IF EXISTS baggedItem;

CREATE TABLE addressuserv
(
	id					INT IDENTITY 	NOT NULL,	
	street			VARCHAR(200) 	NOT NULL,
	homeNumber	INT 					NOT NULL,	
	zip					VARCHAR(10) 	NOT NULL,
	city				VARCHAR(40)		NOT NULL,

	PRIMARY KEY (id)
);

CREATE TABLE userv
(
  id 									INT IDENTITY 				NOT NULL,
  firstname						VARCHAR(100) 				NOT NULL,
	lastname 						VARCHAR(100) 				NOT NULL,
	birthdate						DATETIME		 				NOT NULL,
	email								VARCHAR(150) UNIQUE NOT NULL,
	encryptedPassword		VARCHAR(250) 				NOT NULL,
	sexe 								CHAR(1) 		 				NOT NULL,  
	administrator 			BIT					 				NOT NULL,	
	addressId						INT
	
  PRIMARY KEY (id),
	FOREIGN KEY (addressId) REFERENCES addressuserv (id)
);

CREATE TABLE category
(
  id			INT IDENTITY 	NOT NULL,
  title   VARCHAR(100)  NOT NULL,
	
  PRIMARY KEY (id),
);

CREATE TABLE subcategory
(
	id 					INT IDENTITY 	NOT NULL,
	title 			VARCHAR(100) 	NOT NULL,
	categoryId 	INT 					NOT NULL,

	PRIMARY KEY (id),
	FOREIGN KEY (categoryId) REFERENCES category (id)
)

CREATE TABLE item
(
  id    						INT IDENTITY  NOT NULL,
  label   					VARCHAR(100)  NOT NULL,
	price		 					FLOAT  				NOT NULL,
	quantity					INT 					NOT NULL,
	imageItem		 			VARCHAR(100)  NOT NULL,
	descriptionItem	 	VARCHAR(1000) NOT NULL,
	size							VARCHAR(10)		NOT NULL,
	subcategoryId	 		INT 					NOT NULL,
	
  PRIMARY KEY (id),
	FOREIGN KEY (subcategoryId) REFERENCES subcategory (id)
);

CREATE TABLE bag
(
  id      	INT IDENTITY NOT NULL,
  uservId  	INT          NOT NULL,
	
  PRIMARY KEY (id),
	FOREIGN KEY (uservId) REFERENCES userv(id),
);

CREATE TABLE orderv
(
  id    		 		INT IDENTITY 						NOT NULL,
  datev   	 		DATETIME 		 						NOT NULL,
	isPaid 		 		BIT		   		 	DEFAULT 0 NOT NULL,
	ordervedAt		DATETIME								NOT NULL,
	bagId	 				INT           					NOT NULL,
	itemId		 		INT           					NOT NULL,
	
  PRIMARY KEY (id),
	FOREIGN KEY (bagId) REFERENCES bag(id), 
	FOREIGN KEY (itemId) REFERENCES item (id)
);

CREATE TABLE review
(
  id      					 	INT IDENTITY  NOT NULL,
  stars   					 	INT           NOT NULL,
  likes   					 	INT           NOT NULL,
	title								VARCHAR(30)		NOT NULL,
	descriptionReview	 	VARCHAR(1000) NOT NULL,
	itemId						 	INT  				 	NOT NULL,
	uservId  					 	INT  				 	NOT NULL,
	
  PRIMARY KEY (id),
  FOREIGN KEY (itemId) REFERENCES item (id),
  FOREIGN KEY (uservId) REFERENCES userv (id)
);

CREATE TABLE wishlist
(
  id      	INT IDENTITY 	NOT NULL,
  uservId  	INT           NOT NULL,
	itemId		INT           NOT NULL,
	addedAt 	DATETIME 			NOT NULL,
	
  PRIMARY KEY (id),
	FOREIGN KEY (uservId) REFERENCES userv (id),
	FOREIGN KEY (itemId) REFERENCES item (id)
);

CREATE TABLE baggedItem
(
	id				INT IDENTITY 	NOT NULL,
	addedAt 	DATETIME 			NOT NULL,
	uservId 	INT 					NOT NULL,
	itemId 		INT 					NOT NULL,

	PRIMARY KEY (id),
	FOREIGN KEY (uservId) REFERENCES userv (id),
	FOREIGN KEY (itemId) REFERENCES item (id)
);