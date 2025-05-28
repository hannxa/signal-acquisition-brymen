using System;
using System.IO.Ports;

namespace signal_acquisition_brymen
{
    /// <summary>
    /// Zapis do csv, wywalic czestotliwosc, wykres np z 10 wartosci
    /// </summary>
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

        public string GetVoltageDC()
        {
            try
            {

                _port.DiscardInBuffer();
                _port.WriteLine(":MEASure:VOLTage:DC?");
                System.Threading.Thread.Sleep(300); // Daj Rigolowi czas na odpowiedź

                string response = _port.ReadLine().Trim();


                return response;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd: " + ex.Message); // debug
                return $"Błąd: {ex.Message}";
            }
        }



        public string GetVoltageAC()
        {
            try
            {
                _port.DiscardInBuffer();
                _port.WriteLine(":MEASure:VOLTage:AC?");
                System.Threading.Thread.Sleep(300);
                string response = _port.ReadLine().Trim();
                return response;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd: " + ex.Message);
                return $"Błąd: {ex.Message}";
            }
        }

        public string GetResistance()
        {
            try
            {
                _port.DiscardInBuffer();
                _port.WriteLine(":MEASure:RESistance?");
                System.Threading.Thread.Sleep(300);
                string response = _port.ReadLine().Trim();
                return response;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd: " + ex.Message);
                return $"Błąd: {ex.Message}";
            }
        }

        public string GetCurrent()
        {
            try
            {
                _port.DiscardInBuffer();
                _port.WriteLine(":MEASure:CURRent:DC?");
                System.Threading.Thread.Sleep(300);
                string response = _port.ReadLine().Trim();
                return response;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd: " + ex.Message);
                return $"Błąd: {ex.Message}";
            }
        }
        public string GetCapacitance()
        {
            try
            {
                _port.DiscardInBuffer();
                _port.WriteLine(":MEASure:CAPacitance?");
                System.Threading.Thread.Sleep(300);
                string response = _port.ReadLine().Trim();
                return response;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd: " + ex.Message);
                return $"Błąd: {ex.Message}";
            }
        }
    
    }
}
