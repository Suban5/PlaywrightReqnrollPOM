@web @inventory @test @checkout
Feature: Checkout Item from Inventory

In order to purchase items from the inventory
As a user
I want to add items to the cart and complete the checkout process

@checkout
Scenario: Add Item to Cart and checkout
	Given I navigate to "https://www.saucedemo.com"
	When I login with username "standard_user" and password "secret_sauce"

	Then I should be redirected to the inventory page
	When I add following item to the cart
		| itemName            |
		| Sauce Labs Backpack |
		| Sauce Labs Onesie   |

	Then I navigate to the cart
	Then I should see following item in the inventory
		| itemName            | ItemPrice |
		| Sauce Labs Backpack | 29.99     |
		| Sauce Labs Onesie   | 7.99      |
	Then I clicked on the checkout button
	Then I should be redirected to the checkout page

	When I fill the checkout form with following details
		| firstName | lastName | postalCode |
		| Suban     | Dhyako   | 12345      |
	And I click on the continue button
	Then I should be redirected to the overview page

	Then I should see the overview page with following details
		| itemName            | ItemPrice |
		| Sauce Labs Backpack | 29.99     |
		| Sauce Labs Onesie   | 7.99      |
	Then The Item total should be 37.98
	Then The Tax should be 3.04
	Then The Total should be 41.02

	When I click on the finish button
	Then I should see the confirmation message "Thank you for your order!"

	When I click on the back home button
	Then I should be redirected to the inventory page



