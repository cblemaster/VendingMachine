## Vending Machine Simulator
### About
+ Welcome to Vend-Tron 9000!!!
+ Vending machine customers can browse available snacks, deposit funds, purchase snacks, and receive any change due
+ Vending machine owners can run sales reports and transaction reports
### Objectives
+ Single project, concerns separated by folders (avoid needless complexity)
+ Consider logging as a way to implement the reporting use cases
+ Use OOP principles, even if doing so results in something contrived
### Domains/Contexts
+ The customer context is concerned with browsing snacks, depositing funds, purchasing snacks, and finalizing transactions
+ The owner context is concerned with running reports
+ The vending machine context is concerned with filling snack inventory, and processing and tracking transactions (funds deposited, snack(s) purchased, change returned)
+ The reporting context is concerned with generating reports as requested by the vending machine owner
### Use cases
|#|Context|Use case|Rules|
|-|-|-|-|
|1|Customer|Display available snacks|n/a|
|2|Customer|Make deposit|<ul><li>deposit must be greater than zero</li><li>must be snacks available</li></ul>|
|3|Customer|Purchase snack|<ul><li>must be a deposit</li><li>snack price cannot exceed deposit</li><li>snack must have at least one (1) available</li></ul>|
|4|Customer|Finalize transaction|change, if any, is returned in the smallest number of coins possible|
|5|Owner|View sales report|n/a|
|6|Owner|Run transaction report|n/a|
|7|Vending machine|Fill snack inventory|inventory is available as master data|
|8|Vending machine|Process funds deposited|transaction is logged for reporting|
|9|Vending machine|Process snack purchased|transaction is logged for reporting|
|10|Vending machine|Process change returned to customer|transaction is logged for reporting|
|11|Reporting|Generate sales report|<ul><li>runs automatically when the app is closing</li><li>shows snack name, quantity sold, sales amount, and total sales amount</li></ul>|
|12|Reporting|Generate transaction report|<ul><li>runs when requested by owner</li><li>shows funds deposited, snacks sold, change returned</li></ul>|

