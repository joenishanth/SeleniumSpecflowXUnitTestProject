Feature: Login Guru99 Application

A short summary of the feature

@tag1
Scenario: Login Success
	Given I am in the application Login page
	When I enter a valid login credentials
	Then I should be in the application home page

Scenario: Login failure
	Given I am in the application Login page
	When I enter a invalid login credentials
	Then I should invalid login alert

Scenario: Login Success - Failing test
	Given I am in the application Login page
	When I enter a invalid login credentials
	Then I should be in the application home page
