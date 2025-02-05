# PlotPing

Visual Network Trace

![Plot Ping Screen Shot](documentation/images/ss.png?raw=true "Screen Shot")

# TODO
- [x] Add support for selectable time window

- [x] Add display time window slider So for example need to be able to define the size [in time] of the window of samples to display and then use a slider to move that window around the available samples. 
Time window size can be anything from 5/10 minutes to several days.

- [x] MRU for host [with persistance]

- [x] Add ability to select which hops to show graphs for and auto-resize them [to a point] or allow graph panel to scroll

- [ ] Latency graphs, when start time slider is not at 0, the displayed section should stay fixed, at the moment it moves as new samples are added

- [ ] add option of results graph to show min/max/ave/pl of time window rather than all samples [including start time, so showing historic data]

- [ ] add ability to click latency graph and results view shows that moment in time [and some way to reset to live]

- [ ] Fix latency bar on over time graphs, at very short time windows the bar width isn't quite right.

- [ ] Better ICMP engine. Need to be able to send out ICMP to each hop asynchronously, and the MS one `System.Net.NetworkInformation.Ping.Ping[]` throws an exception when that is attempted.

- [ ] Trace more than one IP at a time [tabs?]

- [ ] Export trace option

- [ ] Fix issue with selected hop graphs not being correct after starting ping to new host

- [ ] Add support for selectable ping frequency [min 5s due to technical limitations - default 5s]

- [ ] Add resolver for IP addresses.  IP Address list [min/max list] could include host name field, updated in background thread.
