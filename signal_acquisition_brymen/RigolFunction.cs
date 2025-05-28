using System;
using System.IO.Ports;

namespace signal_acquisition_brymen
{
    public class RigolFunction
    {
        private readonly SerialPort _port;
        private readonly object _lock = new();

        public RigolFunction(SerialPort port)
        {
            _port = port;
            _port.ReadTimeout = 2000;  // 2 sekundy na odpowiedź
            _port.WriteTimeout = 1000; // 1 sekunda na wysłanie
            Console.WriteLine("[DEBUG] RigolFunction initialized with SerialPort.");
        }

        public string GetVoltageDC()
        {
            Console.WriteLine("[DEBUG] GetVoltageDC called.");
            return Query(":MEASure:VOLTage:DC?");
        }

        public string GetVoltageAC()
        {
            Console.WriteLine("[DEBUG] GetVoltageAC called.");
            return Query(":MEASure:VOLTage:AC?");
        }

        public string GetCurrentDC()
        {
            Console.WriteLine("[DEBUG] GetCurrentDC called.");
            return Query(":MEASure:CURRent:DC?");
        }

        public string GetCurrentAC()
        {
            Console.WriteLine("[DEBUG] GetCurrentAC called.");
            return Query(":MEASure:CURRent:AC?");
        }

        public string GetResistance()
        {
            Console.WriteLine("[DEBUG] GetResistance called.");
            return Query(":MEASure:RESistance?");
        }

        private string Query(string command)
        {
            lock (_lock)
            {
                try
                {
                    Console.WriteLine($"[DEBUG] Query started for command: {command}");

                    if (!_port.IsOpen)
                    {
                        Console.WriteLine("[DEBUG] Serial port is not open.");
                        throw new InvalidOperationException("Port szeregowy nie jest otwarty.");
                    }

                    _port.DiscardInBuffer();
                    Console.WriteLine("[DEBUG] Input buffer discarded.");

                    _port.WriteLine(command);
                    Console.WriteLine($"[DEBUG] Command sent: {command}");

                    string response = _port.ReadLine();
                    Console.WriteLine($"[DEBUG] Response received: {response}");

                    return response.Trim();
                }
                catch (TimeoutException)
                {
                    Console.WriteLine("[DEBUG] TimeoutException occurred.");
                    return "[BŁĄD]: Przekroczono czas oczekiwania na odpowiedź.";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[DEBUG] Exception occurred: {ex.Message}");
                    return $"[BŁĄD]: {ex.Message}";
                }
            }
        }
    }
}
