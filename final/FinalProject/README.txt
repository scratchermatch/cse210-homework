Kernel scheduling algorithm comparisons - Alexander Hearn

All code is available under CC0
 
This program simulates a kernel - it has a memory manager, cpu cores,
processes, and most importantly a task scheduler. I implemented 3 different
scheduling algorithms: First come first served, Shortest job first, and
Round robin.

First come first served simply assigns the first process in a workload
to the first available core with no extra considerations. It's the most
fair, but has considerable tradeoffs with responsiveness. In computing,
you might have 100 processes that will take a microsecond each, and then a
long process that could take 2 seconds. Imagine waiting those 2 seconds for
your mouse to move accross the screen.

Shortest job first is another alternative, which will simply assign the job
with the smallest predictecd CPU burst length to the first avaiable core.
This should make the system super responsive, but what if we generate so
many short jobs that a long job never gets the chance to run? You could 
stall it forever.

Round robin is a good trade-off between the two. You define some time
quantum (just a fancy way of saying how much time each process gets before
its the next processes turn) and cycle all the waiting processes through.
While this seems like the obviously best solution, it incurs several
tradeoffs you should be aware of. For one, switching processes is not free
as the CPU has to cache its execution state. This takes time, and this time
is not spent executing code. Additionally, the RAM used by that process is
not necessarily freed - writing RAM to disk and back is very expensive and
would stall execution for a long time. Therefore, every process in a Round
Robin queue should expect to have its data in memory at the same time. If
you have enough, this is very good. But if you run out, the kernel will
have to kill a process to avoid a panic.

The program automatically runs several built-in test simulations, and is
designed to make manually adding simulations as easy as possible. Planned,
but not implemented in time (see rant below) was an event logger and viewer
to collect more detailed statistics about process wait time, response time,
etc. The program is designed in a way that would make adding this very easy
and intuitive - Simply pass a MetricsCalculator to every object, and 
instantiate and log Event objects inside of it. Then, it can iterate over
the events post simulation and extract the data with exact timings due to
the way that the simulation handles deltas.

---------------------------------------------------------------------------
Rant incoming - ignore if you are easily offended

Every assignment in the entire course is due on Friday, and then the final
is due on a random Tuesday?? Now that's an extremely mean-spirited move.

"Week 7" is the biggest culprit here. That's a misnomer if I ever saw one.
Should call it "Random extra day after Week 6" instead.

Luckily, it's not my first rodeo. I already failed a final in a previous
class because of this, and I learned my lesson. But many other students are
taking their first online class and are going to have the worst Tuesday of
their life. Enjoy the chaos.
