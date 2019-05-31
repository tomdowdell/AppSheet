# AppSheet

I had way too much fun creating this sample and took it seriously. You are encouraged to dig around all files as there are comments and goodies throughout.

## Transparency
- Some rust removal was necessary as it's been three years since I was working on backend code (as my resume clearly shows). That said, it's like riding a bike!
- This project took six hours to complete, divided over three sessions.

## The Project
As you will see several liberties were taken:

- A Middle service was added as an intermediary between the AppSheet Sample service and the app. This allows for some interesting possibilities.
- Instead of focusing strictly on *"an app that displays the 5 youngest users with valid us telephone numbers sorted by name"*, the Middle service and UI allow for so much more!
- For extra credit, instead of discussing *"how the design of the end-to-end service + app should change if the dataset were three orders of magnitude larger"* the code actually does it. Comments in the code discuss alternative approaches.

## Testing
I opted to make the app more robust instead of discussing or adding test code. Great testing techniques and tools exist for the Microsoft stack but I have not used them, though do have some serious history doing rock solid testing. Perhaps the most relatable example:

I cut my teeth testing at Microsoft and, as my resume shows, I created Microsoft's *"first automated server-based UI test system"* called GUTS that caused quite a lot of excitement, including my promotion into product development and an entire team hired to run my system.

*Praveen, I bet my test system was still in use when you joined SQL Server.*

Enjoy!
