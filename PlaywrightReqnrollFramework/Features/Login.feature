Feature: Login Feature


@test @web @login
Scenario: Visit the login page and log in with valid credentials
		Given I navigate to "https://www.saucedemo.com"
		When I login with username "standard_user" and password "secret_sauce"
        
        Then I should be redirected to the inventory page
        And I should see the products header
