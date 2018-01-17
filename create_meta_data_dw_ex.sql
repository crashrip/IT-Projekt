INSERT INTO DW_DIMENSION
	(DIM_SID, DIM_NAME) values
	(1, 'Doctor'),
	(2, 'Insurant'),
	(3, 'Drug'),
	(4, 'MedService'),
	(5, 'Hospital'),
	(6, 'Time');


INSERT INTO DW_LEVEL
	(LVL_SID, LVL_NAME, DIM_SID, LVL_DATATYPE, LVL_POSITION) values
   (-1, 'DoctorVar',		1,	'VARCHAR(100)',	0),
   (-2, 'InsurantVar',		2,	'VARCHAR(100)',	0),
   (-3, 'DrugVar',			3,	'VARCHAR(100)',	0),
   (-4, 'MedServiceVar',	4,	'VARCHAR(100)',	0),
   (-5, 'HospitalVar',		5,	'VARCHAR(100)',	0),
   (-6, 'TimeVar',			6,	'VARCHAR(100)',	0),
	(1, 'doctor',			1,	'VARCHAR(100)', 1),
	(2, 'docDistrict',		1,	'VARCHAR(100)', 2),
	(3, 'docProvince',		1,	'VARCHAR(100)', 3),
	(4, 'topDoctor',		1,	'VARCHAR(100)', 4),
	(5, 'insurant',			2,	'VARCHAR(100)', 1),
	(6, 'insDistrict',		2,	'VARCHAR(100)', 2),
	(7, 'insProvince',		2,	'VARCHAR(100)', 3),
	(8, 'topInsurant',		2,	'VARCHAR(100)', 4),
	(9, 'drug',				3,	'VARCHAR(100)', 1),
	(10, 'atc5',			3,	'VARCHAR(100)', 2),
	(11, 'atc4',			3,	'VARCHAR(100)', 3),
	(12, 'atc3',			3,	'VARCHAR(100)', 4),
	(13, 'atc2',			3,	'VARCHAR(100)', 5),
	(14, 'atc1',			3,	'VARCHAR(100)', 6),
	(15, 'topDrug',			3,	'VARCHAR(100)', 7),
	(16, 'medServ',			4,	'VARCHAR(100)', 1),
	(17, 'topMedService',	4,	'VARCHAR(100)', 2),
	(18, 'hospital',		5,	'VARCHAR(100)', 1),
	(19, 'hospDistrict',	5,	'VARCHAR(100)', 2),
	(20, 'hospProvince',	5,	'VARCHAR(100)', 3),
	(21, 'topHospital',		5,	'VARCHAR(100)', 4),
	(22, 'date',			6,	'INT', 			1),
	(23, 'month',			6,	'INT', 			2),
	(24, 'quarter',			6,	'INT', 			3),
	(25, 'year',			6,	'INT', 			4),
	(26, 'topTime',			6,	'VARCHAR(100)', 5);


INSERT INTO DW_LEVEL_DIRECT_ROLLUP
	(LVL_SID_SUB, 		LVL_SID_SUPER) values
	(1, 2),
	(2, 3),
	(3, 4),
	(5, 6),
	(6, 7),
	(7, 8),
	(9, 10),
	(10, 11),
	(11, 12),
	(12, 13),
	(13, 14),
	(14, 15),
	(16, 17),
	(18, 19),
	(19, 20),
	(20, 21),
	(22, 23),
	(23, 24),
	(24, 25),
	(25, 26);


INSERT INTO DW_ATTRIBUTE
	(ATTR_SID, ATTR_NAME, LVL_SID, ATTR_DATATYPE) values
	(1, 'docAge',					1,	'INT'),
	(2, 'inhPerSqkmInDocDistr',		2,	'INT'),
	(3, 'insAge',					5,	'INT'),
	(4, 'inhPerSqkmInInsDistr',		6,	'INT'),
	(5, 'inhPerSqkmInHospDistr',	19,	'INT'),
	(6, 'drugPrice',				9,	'NUMERIC(12,2)'),
	(7, 'medServFee',				16,	'NUMERIC(12,2)');

INSERT INTO DW_SIMPLE_BASE_MEASURE
	(SBMSR_SID, SBMSR_NAME) values	
	(1, 'costs'		),
	(2, 'quantity'	),
	(3, 'days'		);

INSERT INTO DW_DERIVED_BASE_MEASURE
	(DBMSR_SID, DBMSR_NAME, DBMSR_EXPR, SBMSR_SID, ATTR_SID) values	
	(1, 'costs',		'costs',			1,		null	),
	(2, 'quantity',		'quantity',			2, 		null	),
	(3, 'days',			'days',				3,		null	),
	(4, 'price',		'costs/quantity',	null,	null	),
	(5, 'priceInclVAT',	'price*1.2',		null,	null	);


INSERT INTO DW_DERIVED_BASE_MEASURE_USAGE
	(DBMSR_SID, DBMSR_SID_USED) values		
	(4, 1),
	(4, 2),
	(5, 4);

INSERT INTO DW_SIMPLE_AGGREGATE_MEASURE
	(SAMSR_SID, SAMSR_NAME, SAMSR_EXPR, DBMSR_SID, LVL_SID) values		
	(1,	'SumOfCosts',		'SUM(costs)',				1,			null),
	(2,	'SumOfQuantity',	'SUM(quantity)',			2,			null),
	(3, 'NumOfInsurants',	'COUNT(DISTINCT insurant)', null,		5);

INSERT INTO DW_DERIVED_AGGREGATE_MEASURE
	(DAMSR_SID, DAMSR_NAME, DAMSR_EXPR, SAMSR_SID) values
	(1,	'SumOfCosts',			'SumOfCosts',		1),
	(2,	'SumOfQuantity',		'SumOfQuantity',	2),
	(3,	'NumOfInsurants',		'NumOfInsurants',	3),
	(4,	'CostsPerUnit',			'SumOfCosts/SumOfQuantity',		null),
	(5,	'CostsPerInsurant',		'SumOfCosts/NumOfInsurants',	null);

INSERT INTO DW_DERIVED_AGGREGATE_MEASURE_USAGE
	(DAMSR_SID, DAMSR_SID_USED) values
	(4,	1),
	(4, 2),
	(5, 1),
	(5, 3);

INSERT INTO DW_CUBE
	(CUBE_SID, CUBE_NAME) values
	(1, 'DrugPrescription'),
	(2, 'AmbTreatment'),
	(3, 'Hospitalizstion');
	
INSERT INTO DW_CUBE_DIMENSION
	(CUBE_SID, DIM_SID) values
	(1,	1),
	(1, 2),
	(1,	3),
	(1, 6),
	(2,	1),
	(2, 2),
	(2,	4),
	(2, 6),
	(3,	1),
	(3, 2),
	(3,	5),
	(3, 6);	
	
INSERT INTO DW_CUBE_SIMPLE_BASE_MEASURE
	(CUBE_SID, SBMSR_SID) values
	(1,	1),
	(1,	2),
	(2,	1),
	(2,	2),
	(3,	1),
	(3,	3);
	
INSERT INTO DW_CUBE_DERIVED_BASE_MEASURE
	(CUBE_SID, DBMSR_SID) values
	(1,	1),
	(1,	2),
	(1,	4),
	(1,	5),
	(2,	1),
	(2,	2),
	(2,	4),
	(2,	5),
	(3,	1),
	(3,	3);

INSERT INTO DW_CUBE_DERIVED_AGGREGATE_MEASURE
	(CUBE_SID, DAMSR_SID) values
	(1, 1),
	(1, 2),
	(1, 3),
	(1, 4),
	(1, 5),
	(2, 1),
	(2, 2),
	(2, 3),
	(2, 4),
	(2, 5),
	(3, 1),
	(3, 3),
	(3, 5);
	
INSERT INTO DW_DIM_PREDICATE
	(DIM_PRED_SID, DIM_PRED_NAME, LVL_SID, DIM_PRED_EXPR) values
   (-6,		'TimeVar',			-6,		''	  ),
   (-5,		'HospitalVar',		-5,		''	  ),
   (-4,		'MedServiceVar',	-4,		''	  ),
   (-3,		'DrugVar',			-3,		''	  ),
   (-2,		'InsurantVar',		-2,		''	  ),
   (-1,		'DoctorVar',		-1,		''	  ),
	(1,		'trueDoctor',		4,		'TRUE'),
	(2,		'trueInsurant',		8,		'TRUE'),
	(3,		'trueDrug',			15,		'TRUE'),
	(4,		'trueMedService',	17,		'TRUE'),
	(5,		'trueHospital',		21,		'TRUE'),
	(6,		'trueTime',			26,		'TRUE'),
	(7,		'DocInRuralDistrict',	2,	'inhPerSqkmInDocDistr < 400'),
	(8,		'DocInUrbanDistrict',	2,	'inhPerSqkmInDocDistr >= 400'),
	(9,		'OldDoctor',			1,	'docAge > 55'),
	(10,	'OldDocInRuralDistrict',	1,	'OldDoctor AND DocInRuralDistrict'),
	(11,	'OldDocInUrbanDistrict',	1,	'OldDoctor AND DocInUrbanDistrict'),
	(12,	'InsInRuralDistrict',	2,	'inhPerSqkmInInsDistr < 400'),
	(13,	'InsInUrbanDistrict',	2,	'inhPerSqkmInInsDistr >= 400'),
	(14,	'OldInsurant',			1,	'insAge > 55'),
	(15,	'OldInsInRuralDistrict',	1,	'OldInsurant AND InsInRuralDistrict'),
	(16,	'OldInsInUrbanDistrict',	1,	'OldInsurant AND InsInUrbanDistrict');

	
INSERT INTO DW_DIM_PREDICATE_SUBSUMPTION
	(DIM_PRED_SID_SUBSUMER, DIM_PRED_SID_SUBSUMEE) values
	(7,		1),
	(8,		1),
	(9,		1),
	(10,	1),
	(11,	1),
	(12,	2),
	(13,	2),
	(14,	2),
	(15,	2),
	(16,	2),
	(7,		10),
	(8,		11),
	(9,		10),
	(9,		11),
	(12,	15),
	(13,	16),
	(14,	15),
	(14,	16);

	
INSERT INTO DW_BMSR_PREDICATE
	(BMSR_PRED_SID, BMSR_PRED_NAME, CUBE_SID, BMSR_PRED_EXPR) values
	(-3,	'HospitalizationVar',		3,	'True'),
	(-2,	'AmbTreatmentVar',			2,	'True'),
	(-1,	'DrugPrescriptionVar',		1,	'True'),
	(1,		'trueDrugPrescription',		1,	'TRUE'),
	(2,		'trueAmbTreatment',			2,	'TRUE'),
	(3,		'trueHospitalization',		3,	'TRUE'),
	(4,		'HighDrugPrescrPrice',		1,	'price > 50'),
	(5,		'VeryHighDrugPrescrPrice',		1,	'price > 500');

	
INSERT INTO DW_BMSR_PREDICATE_SUBSUMPTION
	(BMSR_PRED_SID_SUBSUMER, BMSR_PRED_SID_SUBSUMEE) values
	(4,		1),
	(5,		1),
	(5,		4);

	
INSERT INTO DW_AMSR_PREDICATE
	(AMSR_PRED_SID, AMSR_PRED_NAME, CUBE_SID, AMSR_PRED_EXPR) values
   (-3,		'HospitalizationVar',		3,	'TRUE'),
   (-2,		'AmbTreatmentVar',			2,	'TRUE'),
   (-1,		'DrugPrescriptionVar',		1,	'TRUE'),
	(1,		'trueDrugPrescription',		1,	'TRUE'),
	(2,		'trueAmbTreatment',			2,	'TRUE'),
	(3,		'trueHospitalization',		3,	'TRUE'),
	(4,		'HighDrugPrescrCostsPerIns',	1,	'CostsPerInsurant > 1000'),
	(5,		'VeryHighDrugPrescrCostsPerIns',	1,	'CostsPerInsurant > 10000');
	
	
INSERT INTO DW_AMSR_PREDICATE_SUBSUMPTION
	(AMSR_PRED_SID_SUBSUMER, AMSR_PRED_SID_SUBSUMEE) values
	(4,		1),
	(5,		1),
	(5,		4);

	
INSERT INTO DW_SCORE
	(SCORE_SID, SCORE_NAME, SCORE_EXPR) values
	(1,		'RatioOfSumOfCosts',		'CoI.SumOfCosts / CoC.SumOfCosts'),
	(2,		'PercDiffOfSumOfCosts',		'(CoI.SumOfCosts - CoC.SumOfCosts) / CoC.SumOfCosts * 100');
	
	
INSERT INTO DW_SCORE_PREDICATE
	(SCORE_PRED_SID, SCORE_PRED_NAME, SCORE_PRED_EXPR) values
	(1,		'true',										'TRUE'),
	(2,		'IncreasedCostsPerInsurant',				'RatioOfSumOfCosts > 1'),
	(3,		'DramaticallyIncreasedCostsPerInsurant',	'RatioOfSumOfCosts > 1.5');


INSERT INTO DW_SCORE_PREDICATE_SUBSUMPTION
	(SCORE_PRED_SID_SUBSUMER, SCORE_PRED_SID_SUBSUMEE) values
	(2,	1),
	(3,	1),
	(3,	2);


	
	