﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LINQconsole
{
    public delegate void ProcessStarting();
    public delegate void ProcessCompleted(int duration);

    class BusinessProcess
    {
        public event ProcessStarting Started;
        public event ProcessCompleted Completed;

        public event EventHandler StartedCore;
        // se si va a guardare la definition, si vede che è un delegate di System con la seguente firma
        // public delegate void EventHandler(object? sender, EventArgs e)
        //   e: an object that contains no event data.
        public event EventHandler<ProcessEndEventArgs> CompletedCore;
        // public delegate void EventHandler<TEventArgs>(object? sender, TEventArgs e);
        //   e: An object that contains the event data.
        //   TEventArgs: The type of the event data generated by the event.
        // il generic serve per dire che voglio creare una classe per gli event args


        public void ProcessDATA()
        {
            Console.WriteLine("=== Starting Process ===");
            Thread.Sleep(2000);
            Console.WriteLine("=== Process Started ===");

            //EventArgs args = new EventArgs()
            //{
            //};

            //ProcessEndEventArgs args1 = new ProcessEndEventArgs()
            //{
            //    Duration = 4500,
            //    ShipToCountry = "Italy"
            //};

            //sollevo l'evento started, lo comunico ai subscriber
            if (Started != null)
                Started();
            if (StartedCore != null)
                StartedCore(this, new EventArgs());
                //stessa cosa che scrivere new EventArgs()
                //StartedCore(this, EventArgs.Empty); 
                //Empty può essere utilizzato per inizializzare una classe 
                //StartedCore(this, args);

            Thread.Sleep(3000);
            Console.WriteLine("=== Process Completed ===");

            if (Completed != null)
                Completed(5000);
            if (CompletedCore != null)
                CompletedCore(this, new ProcessEndEventArgs { 
                    Duration = 4500,
                    ShipToCountry = "Italy"
                });
            //CompletedCore(this, args1);

            //se non ho handler non sollevo l'evento
            //altrimenti il programma termina.

        }
        public class ProcessEndEventArgs //: EventArgs    // EventArgs è una classe di System
        {
            public int Duration { get; set; }
            public string ShipToCountry { get; set; }
        }
    }
}