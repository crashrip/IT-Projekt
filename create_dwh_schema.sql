CREATE TABLE Doctor (
         doctor      			VARCHAR(100),
         docAge      			INT,
         docDistrict        	VARCHAR(100),
         inhPerSqkmInDocDistr   INT,
         docProvince        	VARCHAR(100) );
         
CREATE TABLE Insurant (
         insurant      			VARCHAR(100),
         insAge      			INT,
         insDistrict        	VARCHAR(100),
         inhPerSqkmInInsDistr   INT,
         insProvince        	VARCHAR(100) );     
		 
CREATE TABLE Drug (
         drug      		VARCHAR(100),
         drugPrice		DECIMAL(12,2),
         atc5      		VARCHAR(100),
         atc4      		VARCHAR(100),
         atc3        	VARCHAR(100),
         atc2        	VARCHAR(100),
         atc1        	VARCHAR(100) );

CREATE TABLE MedService (
         medServ     		VARCHAR(100),
         medServFee			DECIMAL(12,2) );		 

CREATE TABLE Hospital (
         hospital      			VARCHAR(100),
         hospDistrict        	VARCHAR(100),
         inhPerSqkmInHospDistr  INT,
         hospProvince        	VARCHAR(100) );   		 
		 
		 
CREATE TABLE Time (
         day      		INT,
         month      	INT,
         quarter       	INT,
         year        	INT );
		 

         
CREATE TABLE DrugPrescription (
         insurant      		VARCHAR(100),
         doctor      		VARCHAR(100),
         drug        		VARCHAR(100),
         date        		INT,
         quantity        	DECIMAL(12,2),
         costs		   		DECIMAL(12,2) );

CREATE TABLE AmbTreatment (
         insurant      		VARCHAR(100),
         doctor      		VARCHAR(100),
         medServ       		VARCHAR(100),
         date        		INT,
         quantity        	DECIMAL(12,2),
         costs		   		DECIMAL(12,2) );   

CREATE TABLE Hospitalization (
         insurant      		VARCHAR(100),
         doctor      		VARCHAR(100),
         hospital      		VARCHAR(100),
         date        		INT,
         days	        	DECIMAL(12,2),
         costs		   		DECIMAL(12,2) );  







		 
