Feature: Checkout Functionality
	
	As a user of the e-commerce platform
	I want to be able to complete the checkout process smoothly
	So that I can purchase items easily

@SmokeTest
Scenario: Successful Checkout of a single product
	Given User is on the e-commerce platform homepage
	And User add the required product to the shopping cart
	And User navigate to the shopping cart page
	When User proceed to checkout
	And User enter valid checkout information
	Then User should see an order confirmation page
	And Order details and total price should be correct 
	When User finish checkout successfully
	Then User should see the success thank you message
