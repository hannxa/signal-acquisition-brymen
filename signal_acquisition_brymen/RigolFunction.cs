using System;
using System.IO.Ports;

namespace signal_acquisition_brymen
{
    public class RigolFunction
    {
        private SerialPort _port;

        public RigolFunction(SerialPort port)
        {
            _port = port;
        }

        private string Query(string command)
        {
            try
            {
                _port.DiscardInBuffer();
                _port.WriteLine(command);
                System.Threading.Thread.Sleep(300); // Rigol potrzebuje czasu
                return _port.ReadLine().Trim();
            }
            catch (Exception ex)
            {
                return $"Błąd: {ex.Message}";
            }
        }

        public string GetVoltageDC() => Query(":MEASure:VOLTage:DC?");
        public string GetVoltageAC() => Query(":MEASure:VOLTage:AC?");
        public string GetCurrentDC() => Query(":MEASure:CURRent:DC?");
        public string GetCurrentAC() => Query(":MEASure:CURRent:AC?");
        public string GetResistance() => Query(":MEASure:RESistance?");
        public string GetCapacitance() => Query(":MEASure:CAPacitance?");
    }
}
