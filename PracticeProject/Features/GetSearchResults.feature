Feature: Getting Search Result

Scenario: Getting Search Result
	Given User is on the homepage
	When User navigates to homepage of Google
	And User typed automation Training in the search bar
	Then The results counts are validated with expected counts

Scenario Outline: Getting More Search Results using more keywords
	Given User is on the homepage
	When User navigates to homepage of Google
	And User enters <SearchValue> in the search bar
	And The result counts are validated with these <ExpectedCounts>
Examples: 
	| SearchValue                       | ExpectedCounts |
	| automation training with selenium | 15,10,10,10,20 |
	| automation training benefits      | 14,11,10,10,10 |