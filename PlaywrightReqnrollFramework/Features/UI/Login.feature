@test @web @login
Feature: Login Feature
As a user
I want to test log in with valid and invalid credentials
So that I can ensure the login functionality works as expected

@valid
Scenario: Visit the login page and log in with valid credentials
        Given I navigate to "https://www.saucedemo.com"
        When I login with username "standard_user" and password "secret_sauce"

        Then I should be redirected to the inventory page
        And I should see the products header

@invalid
Scenario: Test for locked out user
        Given I navigate to "https://www.saucedemo.com"
        When I login with username "locked_out_user" and password "secret_sauce"
        Then I should see the error message "Epic sadface: Sorry, this user has been locked out."
