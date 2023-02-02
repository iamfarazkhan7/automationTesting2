Feature: Booking a Property

Scenario: Finding a Property with Breakfast Included
	Given User is on the homepage of Browser
	When User navigates to homepage of Booking.com
	And User typed London in the search bar
	And User selected the Date
	And User clicked on the search button
	And User selected the 3star checkbox from Star Rating
	And List the total count of the properties which have Breakfast Included tag.
	And User selected the Breakfast Included checkbox from Meals
	And List the total count of the properties which have Breakfast Included tag while both checkbox are selected.
	Then Validate the results of both of the total counts
