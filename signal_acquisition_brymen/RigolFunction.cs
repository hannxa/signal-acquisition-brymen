using System.IO.Ports;

namespace signal_acquisition_brymen
{
    /// <summary>
    /// Handles communication with a Rigol device over SerialPort to acquire measurements.
    /// </summary>
    public class RigolFunction : IDisposable
    {
        private SerialPort _port;
        private bool _disposed = false;

        // Constructor assigns externally managed SerialPort instance
        public RigolFunction(SerialPort port)
        {
            _port = port;
        }

        // Measures and returns DC voltage
        public string GetVoltageDC()
        {
            try
            {
                _port.DiscardInBuffer();                      // Clear input buffer
                _port.WriteLine(":MEASure:VOLTage:DC?");      // Send SCPI command
                System.Threading.Thread.Sleep(300);           // Wait for response
                return _port.ReadLine().Trim();               // Read and return response
            }
            catch (Exception ex)
            {
                return $"Błąd: {ex.Message}";                  // Return error
            }
        }

        // Measures and returns AC voltage
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
                MessageBox.Show("Błąd: " + ex.Message);       // Show error message
                return $"Błąd: {ex.Message}";
            }
        }

        // Measures and returns resistance
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

        // Measures and returns DC current
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

        // Measures and returns capacitance
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

        // IDisposable implementation
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Cleanup logic
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // No need to dispose _port as it's managed externally
                }
                _disposed = true;
            }
        }
    }
}
