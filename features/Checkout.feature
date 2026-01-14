Feature: Checkout Functionality
	
	As a user of the e-commerce platform
	I want to be able to complete the checkout process smoothly
	So that I can purchase items easily
	

Scenario: Successful Checkout of a single product
	Given User is on the e-commerce platform homepage
		| username      | password     |
		| standard_user | secret_sauce |
	And User add the required product to the shopping cart
		| itemname				  |
		| Sauce Labs Bolt T-Shirt |
	And User navigate to the shopping cart page
	When User proceed to checkout
	And User enter valid checkout information
		| firstname | lastname | postalcode |
		| John      | Doe      |      12345 |	
	Then User should see an order confirmation page
	And Order details and total price should be correct
		| itemname					| itemprice | quantity |
		| Sauce Labs Bolt T-Shirt	|     15.99 |        1 |
	When User finish checkout successfully
	Then User should see the success thank you message


	Scenario: Successful Checkout of multiple products
	Given User is on the e-commerce platform homepage
		| username      | password     |
		| standard_user | secret_sauce |
	And User add the required product to the shopping cart
		| itemname							|
		| Sauce Labs Bolt T-Shirt			|	
		| Test.allTheThings() T-Shirt (Red)	|
	And User navigate to the shopping cart page
	When User proceed to checkout
	And User enter valid checkout information
		| firstname | lastname | postalcode |
		| John      | Doe      |      12345 |	
	Then User should see an order confirmation page
	And Order details and total price should be correct
		| itemname							| itemprice	| quantity |
		| Sauce Labs Bolt T-Shirt			|     15.99	|        1 |
		| Test.allTheThings() T-Shirt (Red)	|     15.99	|        1 |
	When User finish checkout successfully
	Then User should see the success thank you message

