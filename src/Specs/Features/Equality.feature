@Unit @Equality
Feature: Equality
	In order to remove duplicate items
	I need to compare items
	And have their equality evaluated correctly

Scenario: Compare two equal items
	Given I have 2 Dynamic Outputs
		And the first has the following columns and values:
			| Column      | Value     | ShouldRemoveDupes | ShouldAggregate | ShouldIncludeInOutput |
			| Name        | Name      | true              | false           | true                  |
			| Address     | Address   | true              | false           | true                  |
			| Address2    | Address2  | true              | false           | true                  |
			| DateOfBirth | 1/1/1981  | true              | false           | true                  |
			| ExtraColumn | ExtraText | false             | false           | true                  |
		And the second has the following columns and values:
			| Column      | Value     | ShouldRemoveDupes | ShouldAggregate | ShouldIncludeInOutput |
			| Name        | Name      | true              | false           | true                  |
			| Address     | Address   | true              | false           | true                  |
			| Address2    | Address2  | true              | false           | true                  |
			| DateOfBirth | 1/1/1981  | true              | false           | true                  |
			| ExtraColumn | ExtraText | false             | false           | true                  |
	When I compare the two items
	Then result of equality should be true

	Scenario: Compare two inequal items
		Given I have 2 Dynamic Outputs
		And the first has the following columns and values:
			| Column      | Value     | ShouldRemoveDupes | ShouldAggregate | ShouldIncludeInOutput |
			| Name        | Name      | true              | false           | true                  |
			| Address     | Address   | true              | false           | true                  |
			| Address2    | Address2  | true              | false           | true                  |
			| DateOfBirth | 1/1/1981  | true              | false           | true                  |
			| ExtraColumn | ExtraText | false             | false           | true                  |
		And the second has the following columns and values:
			| Column      | Value         | ShouldRemoveDupes | ShouldAggregate | ShouldIncludeInOutput |
			| Name        | NameDifferent | true              | false           | true                  |
			| Address     | Address       | true              | false           | true                  |
			| Address2    | Address2      | true              | false           | true                  |
			| DateOfBirth | 1/1/1981      | true              | false           | true                  |
			| ExtraColumn | ExtraText     | false             | false           | true                  |
	When I compare the two items
	Then result of equality should be false

Scenario: Compare two equal items with spaces in their names
	Given I have 2 Dynamic Outputs
		And the first has the following columns and values:
			| Column      | Value     | ShouldRemoveDupes | ShouldAggregate | ShouldIncludeInOutput |
			| Name        | First Last      | true              | false           | true                  |
			| Address     | Address   | true              | false           | true                  |
			| Address2    | Address2  | true              | false           | true                  |
			| DateOfBirth | 1/1/1981  | true              | false           | true                  |
			| ExtraColumn | ExtraText | false             | false           | true                  |
		And the second has the following columns and values:
			| Column      | Value        | ShouldRemoveDupes | ShouldAggregate | ShouldIncludeInOutput |
			| Name        | First   Last | true              | false           | true                  |
			| Address     | Address      | true              | false           | true                  |
			| Address2    | Address2     | true              | false           | true                  |
			| DateOfBirth | 1/1/1981     | true              | false           | true                  |
			| ExtraColumn | ExtraText    | false             | false           | true                  |
	When I compare the two items
	Then result of equality should be true

Scenario: Compare two inequal items where one item has a null
Given I have 2 Dynamic Outputs
	And the first has the following columns and values:
		| Column      | Value     | ShouldRemoveDupes | ShouldAggregate | ShouldIncludeInOutput |
		| Name        | Name      | true              | false           | true                  |
		| Address     | Address   | true              | false           | true                  |
		| Address2    | Address2  | true              | false           | true                  |
		| DateOfBirth | 1/1/1981  | true              | false           | true                  |
		| ExtraColumn | ExtraText | false             | false           | true                  |
	And the second has the following columns and values:
		| Column      | Value     | ShouldRemoveDupes | ShouldAggregate | ShouldIncludeInOutput |
		| Name        | null      | true              | false           | true                  |
		| Address     | Address   | true              | false           | true                  |
		| Address2    | Address2  | true              | false           | true                  |
		| DateOfBirth | 1/1/1981  | true              | false           | true                  |
		| ExtraColumn | ExtraText | false             | false           | true                  |
When I compare the two items
Then result of equality should be false
