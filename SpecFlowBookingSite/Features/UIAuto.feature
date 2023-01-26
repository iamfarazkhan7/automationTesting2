@contactus
Feature: UIAuto

Scenario: Complete contact us form
	Given I open the automation testing site
	And I wait for the site to load
	And I scroll down and go to Contact us form
	And I fill the Contact details
		| Key         | Value                                                  | Explanation |
		| FirstName   | Sara                                                   | name        |
		| Email       | sa@gmail.com                                           | email       |
		| Phone       | 0123456789111                                          | phone       |
		| Subject     | My Test Email                                          | subject     |
		| Description | Hello this is a test email for testing contact us form | description |
	And I Complete the details I click submit button
	Then I get an acknowledgement that the form is submitted
	Then I close the automation testing site