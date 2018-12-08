# GlobalClock
Simple Windows taskbar clock showing up to five different times calculated using NodaTime (https://nodatime.org/).

A strangly popular little app I knocked up quickly for people who needed to keep multiple timezones in mind. The standard windows clock
only allows two additional clocks. Rather than install a potential security risk app from the internet, this simple WPF based app puts
an icon in the task bar which when mouse overed has a popup containing a string like:

SFO 07:23 | NYC 10:23 | LON 15:23 | CPT 17:23 | TKY 00:23

The string is easily configured in the code and the NodaTime timezones can be found here:

https://gist.github.com/jrolstad/5ca7d78dbfe182d7c1be

Please note that NodaTime handles all daylight savings so once you've set up your preferred timezones, no further configuration is needed.
