// Abfrage zur Wiederherstellung des Originalzustandes vor Inserts
Delete from AGS_ANALYSIS_GRAPH_SCHEMA where ags_sid > 1;
Delete from ags_analysis_situation_schema where ass_sid > 23;
Delete from ags_non_cmp_ass where ass_sid_nass > 23;
Delete from ags_nass_dim_qual where nass_dq_sid > 16;
Delete from ags_nass_dim_qual_slice_cond where nass_sc_sid > 16;

// Update der bereitgestellten Datenbank
Delete from 
Insert into DW_LEVEL (LVL_SID, LVL_NAME, DIM_SID, LVL_DATATYPE, LVL_POSITION) VALUES(-1, 'DoctorVar',		1,	'VARCHAR(100)',	0);
Insert into DW_LEVEL (LVL_SID, LVL_NAME, DIM_SID, LVL_DATATYPE, LVL_POSITION) VALUES(-2, 'InsurantVar',		2,	'VARCHAR(100)',	0);
Insert into DW_LEVEL (LVL_SID, LVL_NAME, DIM_SID, LVL_DATATYPE, LVL_POSITION) VALUES(-3, 'DrugVar',			3,	'VARCHAR(100)',	0);
Insert into DW_LEVEL (LVL_SID, LVL_NAME, DIM_SID, LVL_DATATYPE, LVL_POSITION) VALUES(-4, 'MedServiceVar',	4,	'VARCHAR(100)',	0);
Insert into DW_LEVEL (LVL_SID, LVL_NAME, DIM_SID, LVL_DATATYPE, LVL_POSITION) VALUES(-5, 'HospitalVar',		5,	'VARCHAR(100)',	0);
Insert into DW_LEVEL (LVL_SID, LVL_NAME, DIM_SID, LVL_DATATYPE, LVL_POSITION) VALUES(-6, 'TimeVar',			6,	'VARCHAR(100)',	0);

Insert into DW_DIM_PREDICATE (Dim_pred_sid, dim_pred_name, lvl_sid, dim_pred_expr) VALUES(-6,		'TimeVar',			-6,		''	  );
Insert into DW_DIM_PREDICATE (Dim_pred_sid, dim_pred_name, lvl_sid, dim_pred_expr) VALUES(-5,		'HospitalVar',		-5,		''	  );
Insert into DW_DIM_PREDICATE (Dim_pred_sid, dim_pred_name, lvl_sid, dim_pred_expr) VALUES(-4,		'MedServiceVar',	-4,		''	  );
Insert into DW_DIM_PREDICATE (Dim_pred_sid, dim_pred_name, lvl_sid, dim_pred_expr) VALUES(-3,		'DrugVar',			-3,		''	  );
Insert into DW_DIM_PREDICATE (Dim_pred_sid, dim_pred_name, lvl_sid, dim_pred_expr) VALUES(-2,		'InsurantVar',		-2,		''	  );
Insert into DW_DIM_PREDICATE (Dim_pred_sid, dim_pred_name, lvl_sid, dim_pred_expr) VALUES(-1,		'DoctorVar',		-1,		''	  );

INSERT INTO DW_BMSR_PREDICATE
	(BMSR_PRED_SID, BMSR_PRED_NAME, CUBE_SID, BMSR_PRED_EXPR) values
	(-3,	'HospitalizationVar',		3,	'True'),
	(-2,	'AmbTreatmentVar',			2,	'True'),
	(-1,	'DrugPrescriptionVar',		1,	'True');

INSERT INTO DW_AMSR_PREDICATE
	(AMSR_PRED_SID, AMSR_PRED_NAME, CUBE_SID, AMSR_PRED_EXPR) values
   (-3,		'HospitalizationVar',		3,	'TRUE'),
   (-2,		'AmbTreatmentVar',			2,	'TRUE'),
   (-1,		'DrugPrescriptionVar',		1,	'TRUE');

Delete from DW_DIM_PREDICATE WHERE DIM_PRED_SID > 0;

INSERT INTO DW_DIM_PREDICATE
	(DIM_PRED_SID, DIM_PRED_NAME, LVL_SID, DIM_PRED_EXPR) values
	(1,		'trueDoctor',		4,		'TRUE'),
	(2,		'trueInsurant',		8,		'TRUE'),
	(3,		'trueDrug',			15,		'TRUE'),
	(4,		'trueMedService',	17,		'TRUE'),
	(5,		'trueHospital',		21,		'TRUE'),
	(6,		'trueTime',			26,		'TRUE'),
	(7,		'DocInRuralDistrict',	2,	'inhPerSqkmInDocDistr < 400'),
	(8,		'DocInUrbanDistrict',	2,	'inhPerSqkmInDocDistr >= 400'),
	(9,		'OldDoctor',			1,	'docAge > 55'),
	(10,	'OldDocInRuralDistrict',	1,	'docAge > 55 AND inhPerSqkmInDocDistr < 400'),
	(11,	'OldDocInUrbanDistrict',	1,	'docAge > 55 AND inhPerSqkmInDocDistr >= 400'),
	(12,	'InsInRuralDistrict',	6,	'inhPerSqkmInInsDistr < 400'),
	(13,	'InsInUrbanDistrict',	6,	'inhPerSqkmInInsDistr >= 400'),
	(14,	'OldInsurant',			5,	'insAge > 65'),
	(15,	'OldInsInRuralDistrict',	5,	'insAge > 65 AND inhPerSqkmInDocDistr < 400'),
	(16,	'OldInsInUrbanDistrict',	5,	'insAge > 65 AND inhPerSqkmInDocDistr >= 400');