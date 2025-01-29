# PlotPing

Visual Network Trace

# TODO

[ ] Add display time window slider So for example need to be able to define the size (in time) of the window of samples to display and then use a slider to move that window around the available samples. 
Time window size can be anything from 5/10 minutes to several days.

[ ] Fix latency bar on over time graphs, at short time windows the bar width isn't quite right.

[ ] Better ICMP engine. Need to be able to send out ICMP to each hop simultaneously, and the MS one `System.Net.NetworkInformation.Ping.Ping()` throws an exception when that is attempted.

[ ] Trace more than one IP at a time (tabs?)

[ ] Export trace

[ ] MRU for host

[ ] Fix issue with selected hop graphs not being correct after starting ping to new host

[ ] Add ability to select which hops to show graphs for and auto-resize them (to a point) or allow graph panel to scroll