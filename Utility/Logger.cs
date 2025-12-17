using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NEA.Utility
{
    internal class Logger
    {
        private  List<string> logMessages; // Stores log history
        private  TextBox logTextBox; // Reference to the UI log display

        public Logger(TextBox textBox)
        {
            logMessages = new List<string>();
            logTextBox = textBox;
        }

        // Add a message to the log
        public void LogMessage(string message)
        {
            string timestampedMessage = $"[{DateTime.Now:HH:mm:ss}] {message}";
            logMessages.Add(timestampedMessage);

           
            UpdateLogTextBox();
        }

      // Update Log on the UI
        private void UpdateLogTextBox()
        {
            if (logTextBox.InvokeRequired == true)
            {
                logTextBox.Invoke(new Action(UpdateLogTextBox));
            }
            else
            {
                logTextBox.Text = string.Join(Environment.NewLine, logMessages);
                logTextBox.SelectionStart = logTextBox.Text.Length; // Auto-scrolls to latest message
                logTextBox.ScrollToCaret();
            }
        }
    }
}
