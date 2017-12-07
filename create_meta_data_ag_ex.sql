-- Analysis Graph Schemas -------------------------------------------------------------------------------------------------

INSERT INTO AGS_ANALYSIS_GRAPH_SCHEMA
	(AGS_SID, AGS_NAME, AGS_DESCRIPTION) values
	(1,		'AG1',		'Analysis Graph Demo');

INSERT INTO AGS_ANALYSIS_SITUATION_SCHEMA
	(ASS_SID, ASS_NAME, ASS_DESCRIPTION, AGS_SID, ASS_POS_X, ASS_POS_Y) values
	(1,		'AS1',	'Show drug prescription costs of year ?y per insurants‘ province',		1,	0, 0),
	(2,		'AS2',	'Show ratio of drug prescription costs of year ?y and ?prY listed per insurants‘ province',		1,	100, 	0),
	(3,		'AS3',	'Show ratio of drug prescription costs of year ?y and ?prY listed per insurants‘ province having significant cost increase',		1,	100, 	100),
	(4,		'AS4',	'Show ratio of drug prescription costs of year ?y and ?prY of insurants‘ province ?prov',		1,	100, 	200),
	(5,		'AS5',	'Show ratio of drug prescription costs of year ?y and ?prY of rural districts of insurants‘ province ?prov',		1,	0, 		300),
	(6,		'AS6',	'Show ratio of drug prescription costs of year ?y and ?prY of rural districts of insurants‘ province ?prov listed per districts',		1,	0, 		400),
	(7,		'AS7',	'Show ratio of drug prescription costs of year ?y and ?prY of urban districts of insurants‘ province ?prov',		1,	100, 	300),
	(8,		'AS8',	'Show ratio of drug prescription costs of year ?y and ?prY of urban districts of insurants‘ province ?prov listed per districts',		1,	100, 	400),
	(9,		'AS9',	'Show ambulant treatment costs of year ?y per insurants‘ province',		1,	0, 		200),
	(10,	'AS2_COI',	'Context of Interst of AS2',		1,		100,	0),
	(11,	'AS2_COC',	'Context of Comparison of AS2',		1,		100,	0),
	(12,	'AS3_COI',	'Context of Interst of AS3',		1,		100,	100),
	(13,	'AS3_COC',	'Context of Comparison of AS3',		1,		100,	100),
	(14,	'AS4_COI',	'Context of Interst of AS4',		1,		100,	200),
	(15,	'AS4_COC',	'Context of Comparison of AS4',		1,		100,	200),
	(16,	'AS5_COI',	'Context of Interst of AS5',		1,		0,		300),
	(17,	'AS5_COC',	'Context of Comparison of AS5',		1,		0,		300),	
	(18,	'AS6_COI',	'Context of Interst of AS6',		1,		0,		400),
	(19,	'AS6_COC',	'Context of Comparison of AS6',		1,		0,		400),	
	(20,	'AS7_COI',	'Context of Interst of AS7',		1,		100,	300),
	(21,	'AS7_COC',	'Context of Comparison of AS7',		1,		100,	300),	
	(22,	'AS8_COI',	'Context of Interst of AS8',		1,		100,	400),
	(23,	'AS8_COC',	'Context of Comparison of AS8',		1,		100,	400);
	
	
INSERT INTO AGS_NON_CMP_ASS
	(ASS_SID_NASS, ASS_USED_IN_CASS, CUBE_SID) values	
	(1,		0,	1),
	(9,		0,	2),
	(10,	1,	1),
	(11,	1,	1),
	(12,	1,	1),
	(13,	1,	1),
	(14,	1,	1),
	(15,	1,	1),
	(16,	1,	1),
	(17,	1,	1),
	(18,	1,	1),
	(19,	1,	1),
	(20,	1,	1),
	(21,	1,	1),
	(22,	1,	1),
	(23,	1,	1);


INSERT INTO AGS_CMP_ASS
	(ASS_SID_CASS, ASS_SID_NASS_COI, ASS_SID_NASS_COC) values	
	(2, 10, 11),	
	(3, 12, 13),	
	(4, 14, 15),	
	(5, 16, 17),	
	(6, 18, 19),	
	(7, 20, 21),	
	(8, 22, 23);
	

-- AS1 -----------------------------------------------------------------------------------------------
INSERT INTO AGS_NASS_DIM_QUAL
	(NASS_DQ_SID, ASS_SID_NASS, DIM_SID, LVL_SID_DICELVL, NASS_DQ_DICE_NODE, LVL_SID_GRANLVL) values
	(1,		1,		1,		4,		'allDoctor',	4),
	(2,		1,		2,		8,		'allInsurant',	7),
	(3,		1,		3,		15,		'allDrug',		15),
	(4,		1,		6,		25,		null,			26);

INSERT INTO AGS_NASS_DIM_QUAL_SLICE_COND
	(NASS_SC_SID, NASS_DQ_SID, DIM_PRED_SID) values
	(1,		1,		1),
	(2,		2,		2),
	(3,		3,		3),
	(4,		4,		6);
	
INSERT INTO AGS_NASS_BMSR_FILTER
	(NASS_BMSR_FLT_SID, ASS_SID_NASS, BMSR_PRED_SID) values
	(1,		1,		1);
	
INSERT INTO AGS_NASS_AGGREGATE_MEASSURE
	(NASS_AMSR_SID, ASS_SID_NASS, DAMSR_SID) values
	(1,		1,		1);
	
INSERT INTO AGS_NASS_AMSR_FILTER
	(NASS_AMSR_FLT_SID, ASS_SID_NASS, AMSR_PRED_SID) values
	(1,		1,		1);
	

-- AS9 -----------------------------------------------------------------------------------------------
INSERT INTO AGS_NASS_DIM_QUAL
	(NASS_DQ_SID, ASS_SID_NASS, DIM_SID, LVL_SID_DICELVL, NASS_DQ_DICE_NODE, LVL_SID_GRANLVL) values
	(5,		9,		1,		4,		'allDoctor',	4),
	(6,		9,		2,		8,		'allInsurant',	7),
	(7,		9,		4,		15,		'allMedServ',	17),
	(8,		9,		6,		25,		null,			26);

INSERT INTO AGS_NASS_DIM_QUAL_SLICE_COND
	(NASS_SC_SID, NASS_DQ_SID, DIM_PRED_SID) values
	(5,		5,		1),
	(6,		6,		2),
	(7,		7,		4),
	(8,		8,		6);
	
INSERT INTO AGS_NASS_BMSR_FILTER
	(NASS_BMSR_FLT_SID, ASS_SID_NASS, BMSR_PRED_SID) values
	(2,		9,		2);
	
INSERT INTO AGS_NASS_AGGREGATE_MEASSURE
	(NASS_AMSR_SID, ASS_SID_NASS, DAMSR_SID) values
	(2,		9,		1);
	
INSERT INTO AGS_NASS_AMSR_FILTER
	(NASS_AMSR_FLT_SID, ASS_SID_NASS, AMSR_PRED_SID) values
	(2,		9,		2);


-- AS1 -> AS9 ----------------------------------------------------------------------------------------	
	
INSERT INTO AGS_NAVSTEP_SCHEMA
	(NAVSS_SID, AGS_SID, ASS_SID_SOURCE, ASS_SID_TARGET, NAVSS_OPNAME, NAVSS_GRD_EXPR, NAVSS_USED_IN_CMPNAV, NAVSS_POS_X, NAVSS_POS_Y, NAVSS_POS_GRD_X, NAVSS_POS_GRD_Y) values
	(1,		1,		1,		9,		'drillAcrossToCube',		null,						0,		0,		50,		0,		0);
	

INSERT INTO AGS_NAVSS_DRILL_ACROSS_TO_CUBE
	(NAVSS_SID, CUBE_SID) values
	(1,		2);
	
INSERT INTO AGS_NAVSS_DRILL_ACROSS_TO_CUBE_BMSR_PRED
	(NAVSS_SID, BMSR_PRED_SID) values
	(1,		2);

INSERT INTO AGS_NAVSS_DRILL_ACROSS_TO_CUBE_DAMSR
	(NAVSS_SID, DAMSR_SID) values
	(1,		1);
	
INSERT INTO AGS_NAVSS_DRILL_ACROSS_TO_CUBE_AMSR_PRED
	(NAVSS_SID, AMSR_PRED_SID) values
	(1,		2);
	
	
-- AS2_COI ----------------------------------------------------------------------------------------	
INSERT INTO AGS_NASS_DIM_QUAL
	(NASS_DQ_SID, ASS_SID_NASS, DIM_SID, LVL_SID_DICELVL, NASS_DQ_DICE_NODE, LVL_SID_GRANLVL) values
	(9,			10,		1,		4,		'allDoctor',	4),
	(10,		10,		2,		8,		'allInsurant',	7),
	(11,		10,		3,		15,		'allDrug',		15),
	(12,		10,		6,		25,		null,			26);

INSERT INTO AGS_NASS_DIM_QUAL_SLICE_COND
	(NASS_SC_SID, NASS_DQ_SID, DIM_PRED_SID) values
	(9,			9,		1),
	(10,		10,		2),
	(11,		11,		3),
	(12,		12,		6);
	
INSERT INTO AGS_NASS_BMSR_FILTER
	(NASS_BMSR_FLT_SID, ASS_SID_NASS, BMSR_PRED_SID) values
	(3,		10,		1);
	
INSERT INTO AGS_NASS_AGGREGATE_MEASSURE
	(NASS_AMSR_SID, ASS_SID_NASS, DAMSR_SID) values
	(3,		10,		1);
	
INSERT INTO AGS_NASS_AMSR_FILTER
	(NASS_AMSR_FLT_SID, ASS_SID_NASS, AMSR_PRED_SID) values
	(3,		10,		1);
	

-- AS2_COC ----------------------------------------------------------------------------------------	
INSERT INTO AGS_NASS_DIM_QUAL
	(NASS_DQ_SID, ASS_SID_NASS, DIM_SID, LVL_SID_DICELVL, NASS_DQ_DICE_NODE, LVL_SID_GRANLVL) values
	(13,		11,		1,		4,		'allDoctor',	4),
	(14,		11,		2,		8,		'allInsurant',	7),
	(15,		11,		3,		15,		'allDrug',		15),
	(16,		11,		6,		25,		null,			26);

INSERT INTO AGS_NASS_DIM_QUAL_SLICE_COND
	(NASS_SC_SID, NASS_DQ_SID, DIM_PRED_SID) values
	(13,		13,		1),
	(14,		14,		2),
	(15,		15,		3),
	(16,		16,		6);
	
INSERT INTO AGS_NASS_BMSR_FILTER
	(NASS_BMSR_FLT_SID, ASS_SID_NASS, BMSR_PRED_SID) values
	(4,		11,		1);
	
INSERT INTO AGS_NASS_AGGREGATE_MEASSURE
	(NASS_AMSR_SID, ASS_SID_NASS, DAMSR_SID) values
	(4,		11,		1);
	
INSERT INTO AGS_NASS_AMSR_FILTER
	(NASS_AMSR_FLT_SID, ASS_SID_NASS, AMSR_PRED_SID) values
	(4,		11,		1);
	
	
-- AS2 ----------------------------------------------------------------------------------------	
INSERT INTO AGS_CASS_JOIN_COND
	(CASS_JOIN_SID, ASS_SID_CASS, CASS_JOIN_EXPR) values
	(1,		2,		'CoI.insProvince = CoC.insProvince');

INSERT INTO AGS_CASS_SCORE
	(CASS_SCORE_SID, ASS_SID_CASS, SCORE_SID) values
	(1,		2,		1);
	
INSERT INTO AGS_CASS_SCORE_FILTER
	(CASS_SCORE_FLT_SID, ASS_SID_CASS, SCORE_PRED_SID) values
	(1,		2,		1);



-- AS1 -> AS2 ---------------------------------------------------------------------------------
INSERT INTO AGS_NAVSTEP_SCHEMA
	(NAVSS_SID, AGS_SID, ASS_SID_SOURCE, ASS_SID_TARGET, NAVSS_OPNAME, NAVSS_GRD_EXPR, NAVSS_USED_IN_CMPNAV, NAVSS_POS_X, NAVSS_POS_Y, NAVSS_POS_GRD_X, NAVSS_POS_GRD_Y) values
	(2,		1,		1,		11,		'moveToPrevNode',		null,						1,		0,		50,		0,		0);

INSERT INTO AGS_NAVSS_MOVE_TO_PREV_NODE
	(NAVSS_SID, DIM_SID) values
	(2,		6);

INSERT INTO AGS_NAVSTEP_SCHEMA
	(NAVSS_SID, AGS_SID, ASS_SID_SOURCE, ASS_SID_TARGET, NAVSS_OPNAME, NAVSS_GRD_EXPR, NAVSS_USED_IN_CMPNAV, NAVSS_POS_X, NAVSS_POS_Y, NAVSS_POS_GRD_X, NAVSS_POS_GRD_Y) values
	(3,		1,		1,		2,		'relate',		null,						0,		0,		50,		0,		0);
	
INSERT INTO AGS_NAVSS_RELATE
	(NAVSS_SID, NAVSS_SID_NOP, NAVSS_OPNAME) values
	(3,		2,		'moveToPrevNode');
	
INSERT INTO AGS_NAVSS_RELATE_JOIN
	(NAVSS_JOIN_SID, NAVSS_SID, CASS_JOIN_EXPR) values
	(1,		3,		'CoI.insProvince = CoC.insProvince');

INSERT INTO AGS_NAVSS_RELATE_SCORE
	(NAVSS_SID, SCORE_SID) values
	(3,		1);

INSERT INTO AGS_NAVSS_RELATE_SCORE_FLT
	(NAVSS_SID, SCORE_PRED_SID) values
	(3,		1);


-- .... to continue ..... ---------------------------------------------------------------------
	
-- Analysis Graph -----------------------------------------------------------------------------	
INSERT INTO AG_ANALYSIS_GRAPH
	(AG_SID, AGS_SID, AG_NAME, AG_DESCRIPTION) values
	(1,		1,		'ag1',		'Analysis Graph Demo');

INSERT INTO AG_ANALYSIS_SITUATION
	(AS_SID,	ASS_SID, AS_NAME, AS_DESCRIPTION, AG_SID, AS_POS_X, AS_POS_Y) values
	(1,			1,		'as1',		'Show drug prescription costs of year 2016 per insurants‘ province',								1,	0, 0),
	(2,			2, 		'as2',		'Show ratio of drug prescription costs of year 2016 and 2015 listed per insurants‘ province',		1,	100, 	0),
	(3,			3,		'as3',		'Show ratio of drug prescription costs of year 2016 and 2015 listed per insurants‘ province having significant cost increase',		1,	100, 	100),
	(41,		4,		'as4-1',	'Show ratio of drug prescription costs of year 2016 and 2015 of insurants‘ province Upper Austria',			1,	100, 	200),
	(42,		4,		'as4-2',	'Show ratio of drug prescription costs of year 2016 and 2015 of insurants‘ province Vienna',				1,	100, 	200),
	(5,			5,		'as5',		'Show ratio of drug prescription costs of year 2016 and 2015 of rural districts of insurants‘ province Upper Austria',		1,	0, 		300),
	(6,			6,		'as6',		'Show ratio of drug prescription costs of year 2016 and 2015 of rural districts of insurants‘ province Upper Austria listed per districts',		1,	0, 		400),
	(71,		7,		'as7-1',	'Show ratio of drug prescription costs of year 2016 and 2015 of urban districts of insurants‘ province Upper Austria',		1,	100, 	300),
	(72,		7,		'as7-2',	'Show ratio of drug prescription costs of year 2016 and 2015 of urban districts of insurants‘ province Vienna',		1,	100, 	300),
	(81,		8,		'as8-1',	'Show ratio of drug prescription costs of year 2016 and 2015 of urban districts of insurants‘ province Upper Austria listed per districts',		1,	100, 	400),
	(82,		8,		'as8-2',	'Show ratio of drug prescription costs of year 2016 and 2015 of urban districts of insurants‘ province Vienna listed per districts',		1,	100, 	400),
	(9,			9, 		'as9',		'Show ambulant treatment costs of year 2016 per insurants‘ province',		1,	0, 		200),
	(10,		10,		'as2_coi',	'Context of Interst of as2',												1,		100,	0),
	(11,		11,		'as2_coc',	'Context of Comparison of as2',												1,		100,	0),
	(12,		12,		'as3_coi',	'Context of Interst of as3',												1,		100,	100),
	(13,		13,		'as3_coc',	'Context of Comparison of as3',												1,		100,	100),
	(4114,		14,		'as4-1_coi',	'Context of Interst of as4-1',											1,		100,	200),	
	(4214,		14,		'as4-2_coi',	'Context of Interst of as4-2',											1,		100,	200),
	(4115,		15,		'as4-1_coc',	'Context of Comparison of as4-1',										1,		100,	200),
	(4215,		15,		'as4-2_coc',	'Context of Comparison of as4-2',										1,		100,	200),
	(16,		16,		'as5_coi',	'Context of Interst of as5',												1,		0,		300),
	(17,		17,		'as5_coc',	'Context of Comparison of as5',												1,		0,		300),	
	(18,		18,		'as6_coi',	'Context of Interst of as6',												1,		0,		400),
	(19,		19,		'as6_coc',	'Context of Comparison of as6',												1,		0,		400),	
	(7120,		20,		'as7-1_coi',	'Context of Interst of as7-1',											1,		100,	300),
	(7220,		20,		'as7-2_coi',	'Context of Interst of as7-2',											1,		100,	300),
	(7121,		21,		'as7-1_coc',	'Context of Comparison of as7-1',										1,		100,	300),	
	(7221,		21,		'as7-2_coc',	'Context of Comparison of as7-2',										1,		100,	300),		
	(8122,		22,		'as8-1_coi',	'Context of Interst of as8-1',											1,		100,	400),
	(8222,		22,		'as8-2_coi',	'Context of Interst of as8-2',											1,		100,	400),
	(8123,		23,		'as8-1_coc',	'Context of Comparison of as8-1',										1,		100,	400),
	(8223,		23,		'as8-2_coc',	'Context of Comparison of as8-2',										1,		100,	400);
	
	
INSERT INTO AG_NON_CMP_AS
	(AS_SID_NAS, AS_USED_IN_CAS, CUBE_SID) values	
	(1,		0,	1),
	(9,		0,	2),
	(10,	1,	1),
	(11,	1,	1),
	(12,	1,	1),
	(13,	1,	1),
	(4114,	1,	1),
	(4214,	1,	1),
	(4115,	1,	1),
	(4215,	1,	1),
	(16,	1,	1),
	(17,	1,	1),
	(18,	1,	1),
	(19,	1,	1),
	(7120,	1,	1),
	(7220,	1,	1),
	(7121,	1,	1),
	(7221,	1,	1),	
	(8122,	1,	1),
	(8222,	1,	1),	
	(8123,	1,	1),
	(8223,	1,	1);

INSERT INTO AG_CMP_AS
	(AS_SID_CAS, AS_SID_NAS_COI, AS_SID_NAS_COC) values	
	(2, 10, 11),	
	(3, 12, 13),	
	(41, 4114, 4115),
	(42, 4214, 4215),	
	(5, 16, 17),	
	(6, 18, 19),	
	(71, 7120, 7121),
	(72, 7220, 7221),	
	(81, 8122, 8123),
	(82, 8222, 8223);
	

-- ..... to continue .... --------------------------------------------------


