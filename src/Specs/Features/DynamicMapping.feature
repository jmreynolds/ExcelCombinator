@Unit @Mapping
Feature: DynamicMapping
	In order to send postcards
	As a data processor
	I want to import any columns from excel

Scenario: Map data from excel to POCO list
	Given A sample generic input worksheet with 3 rows
	When I map them to a POCO list
	Then the input result should have 3 rows

Scenario: Map municipal data from excel
	Given a sample input worksheet with the municipal format with 3 rows
	When I map them to a POCO list
	Then the input result should have 3 rows

Scenario: Should Map From Input Class List Into Output Class List
	Given a sample list mapped from the municipal format
			And the input columns are:
			| Column          |
			| OffenseDate     |
			| CitationNumber  |
			| Name            |
			| Address         |
			| Address2        |
			| Offense         |
			| Juvenile        |
			| DispOper        |
			| DispositionDate |
			| LastHearingDate |
			| Court           |
			| LastHearingCode |
			| DateOfBirth     |
			| Final           |
		And the following fields are marked for inclusion:
			|Column|
			|Name|
			|Address|
			|Address2|
			|DispositionDate|
		And the following fields are marked as de-dupe fields:
			|Column|
			|Name|
			|Address|
			|Address2|
			|DateOfBirth|
		And the following fields are marked as aggregate fields:
			|Column|
			|CitationNumber|
			|Offense|
		And the list has 5 rows with 0 duplicate records
	When I process the list for output
	Then the result should be an output in the municipal format
		And the output result should have 5 rows
		And the column names should be:
			|Column|
			|Name|
			|Address|
			|Address2|
			|DispositionDate|
			|Citations|

Scenario: Correctly De-Duplicate the Input Data
	Given a sample list mapped from the municipal format
		And the input columns are:
			| Column          |
			| OffenseDate     |
			| CitationNumber  |
			| Name            |
			| Address         |
			| Address2        |
			| Offense         |
			| Juvenile        |
			| DispOper        |
			| DispositionDate |
			| LastHearingDate |
			| Court           |
			| LastHearingCode |
			| DateOfBirth     |
			| Final           |
		And the following fields are marked for inclusion:
			|Column|
			|Name|
			|Address|
			|Address2|
			|DispositionDate|
		And the following fields are marked as de-dupe fields:
			|Column|
			|Name|
			|Address|
			|Address2|
			|DateOfBirth|
		And the following fields are marked as aggregate fields:
			|Column|
			|CitationNumber|
			|Offense|
		And the list has 5 rows with 1 duplicate records
	When I process the list for output
	Then the result should be an output in the municipal format
		And the output result should have 4 rows
		And the column names should be:
			|Column|
			|Name|
			|Address|
			|Address2|
			|DispositionDate|
			|Citations|

Scenario: Correctly Evaluate De-Dupe Fields
	Given a sample list mapped from the municipal format
		And the input columns are:
			| Column          |
			| OffenseDate     |
			| CitationNumber  |
			| Name            |
			| Address         |
			| Address2        |
			| Offense         |
			| Juvenile        |
			| DispOper        |
			| DispositionDate |
			| LastHearingDate |
			| Court           |
			| LastHearingCode |
			| DateOfBirth     |
			| Final           |
		And the following fields are marked for inclusion:
			|Column|
			|Name|
			|Address|
			|Address2|
			|DispositionDate|
		And the following fields are marked as de-dupe fields:
			|Column|
			|Name|
			|Address|
			|Address2|
		And the following fields are marked as aggregate fields:
			|Column|
			|CitationNumber|
			|Offense|
		And the list has 5 rows with 1 duplicate records
	When I process the list for output
	Then the result should be an output in the municipal format
		And the output result should have 4 rows
		And the column names should be:
			|Column|
			|Name|
			|Address|
			|Address2|
			|DispositionDate|
			|Citations|

			Scenario: Correctly Evaluate Inclusion Fields
				Given a sample list mapped from the municipal format
		And the input columns are:
			| Column          |
			| OffenseDate     |
			| CitationNumber  |
			| Name            |
			| Address         |
			| Address2        |
			| Offense         |
			| Juvenile        |
			| DispOper        |
			| DispositionDate |
			| LastHearingDate |
			| Court           |
			| LastHearingCode |
			| DateOfBirth     |
			| Final           |
		And the following fields are marked for inclusion:
			|Column|
			|Name|
			|Address|
			|Address2|
		And the following fields are marked as de-dupe fields:
			|Column|
			|Name|
			|Address|
			|Address2|
			|DateOfBirth|
		And the following fields are marked as aggregate fields:
			|Column|
			|CitationNumber|
			|Offense|
		And the list has 5 rows with 1 duplicate records
	When I process the list for output
	Then the result should be an output in the municipal format
		And the output result should have 4 rows
		And the column names should be:
			|Column|
			|Name|
			|Address|
			|Address2|
			|Citations|
