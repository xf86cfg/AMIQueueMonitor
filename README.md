# AMIQueueMonitor
Simple Queue Monitor for Asterisk using AsterNET framework

<p align="center">
  <img src="https://github.com/xf86cfg/AMIQueueMonitor/blob/master/images/AMIQueueMonitorv2.png" width="480" title="AMIQueueMonitor screenshot">
</p>

## Features

* Displays queues statistics: current, abandoned, completed calls
* Displays current call in queue details: number, name, queue, position in queue, wait time
* Logs queue events to application console: caller joined, caller connected to agent, call abandoned, call completed
* Logs agents events to application console: agent logon, agent logoff

### Configurable settings
* Queue filtering
* Auto poll on startup
* Poll interval
* Always on top
* Pop up on new incoming call in queue
* Highlight queue if there are active calls in it
* Configurable UI settings (sizes, position, fonts, colors) 

### Prerequisites

* `.Net Framework 4.6.1` on client PC
* Asterisk AMI interface enabled and `manager.conf` configured on Asterisk
```
[general]
enabled = yes
port = 5038
bindaddr = 0.0.0.0
[ReadonlyAMIUser]
secret = MySecretPassword
read = all
write = 
```
### Installation and configuration

* Download `AMIQueueMonitor_Release.zip` and unpack it
* Open `AMI Queue Monitor.exe.config` in text editor
* Change `<!--Server settings-->` and `<!--Queues settings-->` accordingly to match your Asterisk AMI configuration and your queue settings.
* Note: Queues configuration is provided in format: 
`"Queue 1:Description for Queue 1,Queue 2:Description for Queue 2,etc..."`, see below
```
    <!--Server settings-->
    <add key="Server" value="192.168.0.254"/>
    <add key="ServerPort" value="5038"/>
    <add key="Username" value="ReadonlyAMIUser"/>
    <add key="Password" value="MySecretPassword"/>
    <!--Queues settings-->
    <add key="Queues" value="2880:Sales,2881:Marketing,2882:Helpdesk"/>
```
* Run `AMI Queue Monitor.exe`

### UI configuration
If you want to change look-and-feel of the application you can configure following settings:
```
    <!--Style settings-->
    <add key="WindowTitle" value="AMI Queue Monitor"/>
    <add key="ConsoleFont" value="Consolas, 8pt"/>
    <add key="TableHeadersFont" value="Consolas, 8pt, style=Bold"/>
    <add key="TableRowsFont" value="Consolas, 8pt"/>
    <add key="TableRowsFont" value="Consolas, 8pt"/>
    <add key="StateIndicatorsFont" value="Consolas, 8pt"/>
    <add key="TableHeadersHeight" value="22"/>
    <add key="FormOrigin" value="20,20"/>
    <add key="FormSize" value="640,640"/>
    <add key="ConsoleSplitterRatio" value="0.6"/>
    <add key="QueuesSplitterRatio" value="0.28"/>
    <add key="TableHeadersColor" value="#B0C4DE"/>
    <add key="TableGridsColor" value="#B0C4DE"/>
    <add key="ConsoleAreaColor" value="#B0C4DE"/>
    <add key="QueuesAreaColor" value="#F0FFFF"/>
    <add key="TableHeadersTextColor" value="#000000"/>
    <add key="TableRowsTextColor" value="#000000"/>
    <add key="ConsoleInfoTextColor" value="#333333"/>
    <add key="ConsoleErrorTextColor" value="#D20020"/>
    <add key="HighlightRowColor" value="#FF8C00"/>
```
