Bot made for use with Discord
https://discordapp.com/

Bot made using the unofficial C# .NET API
https://github.com/RogueException/Discord.Net

Discord official API
https://discordapp.com/developers/docs/intro

Bot made by: Tung Nguyen
https://github.com/tung362

Bot made for my discord server join if you have any questions:
https://discord.gg/qtmY7kJ


Requirements:
Microsoft .NET Framework 4.5.2
Windows 10 (Windows 7 untested)


Disclaimer:
This bot is made purely for my own personal discord server and serves as just an example template for other developers to structure around.
The bot was built as the core of my discord server so it will not work on any other discord server unless you change the settings mentioned below and structure your server similar to mine.


How to use:
Login credentials
-The bot uses 3 webhook tokens and 1 bot token to work.
-Open the folder of DiscordApeBot\DiscordApeBot\bin\Debug\Tokens
-Type your bot token in "Token.token"
-Type one of each webhook tokens in "Baboon.token", "Gorilla.token", and "Monkey.token" with any text editor for example notepad

-Open up the "Setup.cs" file in DiscordApeBot\DiscordApeBot
find the line "GorillaWebhookClient = new DiscordWebhookClient(497504740075241472, gorillaToken);" and change "49750474007524147" to your webhook1's ID
find the line "MonkeyWebhookClient = new DiscordWebhookClient(497506214767493120, monkeyToken);" and change "497506214767493120" to your webhook2's ID
find the line "BaboonWebhookClient = new DiscordWebhookClient(497506409026551810, baboonToken);" and change "497506409026551810" to your webhook3's ID
 
 Example:
 DiscordApeBot\DiscordApeBot\bin\Debug\Tokens\Token.token
 1st line Token
 
 
Changing settings:
-Open up the "Setup.cs" file in DiscordApeBot\DiscordApeBot and change the values to what you need