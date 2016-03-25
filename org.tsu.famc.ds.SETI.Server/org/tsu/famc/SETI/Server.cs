using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace org.tsu.famc.ds.SETI
{
    class Server
    {/*
        static void foo()
        {
            try
            {
                Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
                var kernel = new Ninject.StandardKernel();
                kernel.Load("SETI.xml");
                ISETI seti = kernel.Get<ISETI>();
                seti.AnalyzeData(null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        */

        static void Main(string[] args)
        {
            //foo();
            // Опрелеяем URI сервисов
            Uri receiverUri = new Uri("http://localhost:8000/Reciever/");
            Uri alarmUri = new Uri("http://localhost:8001/Alarm/");
            string restURL = "http://localhost:8002/";

            // Создаем сервисs
            ServiceHost receiverHost = new ServiceHost(typeof(Receiver), receiverUri);
            ServiceHost alarmHost = new ServiceHost(typeof(Alarm), alarmUri);

            try
            {
                // Связываем сервис с endpoint'ом
                receiverHost.AddServiceEndpoint(typeof(IReceiver), new WSHttpBinding(), "Reciever");

                WSDualHttpBinding binding = new WSDualHttpBinding();
                binding.SendTimeout = new TimeSpan(0, 0, 5);
                alarmHost.AddServiceEndpoint(typeof(IAlarm), binding, "Alarm");

                // Разрешаем считывать метаданные сервиса
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                receiverHost.Description.Behaviors.Add(smb);

                smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                alarmHost.Description.Behaviors.Add(smb);                

                // Стартуем сервис WCF
                receiverHost.Open();
                Console.WriteLine("The Reciever is ready.");
                // Стартуем сервис Rest
                RESTReciver.RunRESTReciverControler(restURL);
                Console.WriteLine("The RESTReciever is ready.");
                // Стартуем сервис WCF-Alarm
                alarmHost.Open();
                Console.WriteLine("The Alarm is ready.");
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.WriteLine();
                Console.ReadLine();

                // Останавливаем сервис
                receiverHost.Close();
                alarmHost.Close();
            }
            catch (Exception ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                receiverHost.Abort();
                alarmHost.Abort();
            }
        }
    }
}
