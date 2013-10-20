TwitterClient
=============

Desktop twitter client for daily follow up. It is not official twitter application I wrote it just to follow the tweets on my wall.

How to install the app
----------------------
You can find an .msi here : [TwitterClient Setup][1].

Execute the .msi and follow the wizard to install, it is normaly a next-next-finish story.

How to configure the app
------------------------
Once you have installed the application you can find a Silverforge.TwitterClient.exe.config on the installed path. Open it and enter here the consumerKey, consumerSecret, accesToken, accessTokenSecret, what you created on [dev.twitter.com][2].

        <appSettings>
                <add key="consumerKey" value="" />
                <add key="consumerSecret" value="" />
                <add key="accessToken" value="" />
                <add key="accessTokenSecret" value="" />

                <add key="pollInterval" value="90"/>

                <add key="accentColor" value="#FF1E90FF" />
                <add key="textColor" value="" />
        </appSettings>

How to link an application with my twitter account
--------------------------------------------------
You can find a very useful post here : [Tokens from dev.twitter.com][3].

Any suggestions, comments are very welcome about the application.

[1]: https://github.com/silverforge/TwitterClient/blob/master/Silverforge.TwitterClient.Setup/Silverforge.TwitterClient.Setup-SetupFiles/Silverforge.TwitterClient.Setup.msi "TwitterClient.Setup"

[2]: https://dev.twitter.com/ "dev.twitter.com" 

[3]: https://dev.twitter.com/docs/auth/tokens-devtwittercom "dev.twitter.help"
