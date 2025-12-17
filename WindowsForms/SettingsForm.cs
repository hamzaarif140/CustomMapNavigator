using NEA.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static NEA.Settings;

namespace NEA
{
    public partial class SettingsForm : Form
    {
        private Logger logger;
        public SettingsForm()
        {
            InitializeComponent();

        }
        // On form load, set the radio button state based on the current Settings.
        private void SettingsForm_Load(object sender, EventArgs e)
        {
            if (Settings.SelectedPathfindingAlgorithm == PathfindingAlgorithm.Dijkstra)
            {
                DijkstraRadioButton.Checked = true;
            }
            else if (Settings.SelectedPathfindingAlgorithm == PathfindingAlgorithm.AStar)
            {
                aStarRadioButton.Checked = true;
            }

        }

        // Apply button click saves and closes the form.
        private void applyButton_Click(object sender, EventArgs e)
        {
            // Settings are updated via radio button event handlers.
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // When the A star radio button becomes checked, update Settings.
        private void aStarRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (aStarRadioButton.Checked)
            {
                Settings.SelectedPathfindingAlgorithm = PathfindingAlgorithm.AStar;
            }
        }

        // When the Dijkstra radio button becomes checked, update Settings.
        private void DijkstraRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (DijkstraRadioButton.Checked)
            {
                Settings.SelectedPathfindingAlgorithm = PathfindingAlgorithm.Dijkstra;
            }
        }


    }
}
