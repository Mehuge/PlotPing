== Redesign ==

Problem atm is timer drives the trace and rendering every second but if the trace takes longer than a second, it starts to stall, and then every sample isnt on a second boundary.

So, re-implement as follows:

Traces Array is popuplated by the timer.

   Trace {
     DateTime: timestamp
     Status: "Pending"                  // "Running", "Timed Out", "Completed"
     Hops[] = [] {
        Hop: {
            HOP: 1
            IP: "192.168.1.1"
            RTT: 100ms
            Status: "Pending"
        }
     }
   }

   Trace is passed to the Run method, and will be populated when the trace is complete.

   The run method has a Complete callback that will render the trace and graphs.
   (what do we do if the trace completes out of order?)

   The complete callback triggers a render of the latency history, which will need to deal with one or more of the
   traces not being complete.
